using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Plugins;
using CDTDatabase;
using DevExpress.XtraEditors;
using CDTLib;

namespace ThuHoaDon
{
    public class ThuHoaDon : ICData
    {
        public ThuHoaDon()
        {
            _info = new InfoCustomData(IDataType.MasterDetailDt);
        }
        private InfoCustomData _info;
        private DataCustomData _data;
        Database db = Database.NewDataDatabase();
        #region ICData Members

        public DataCustomData Data
        {
            set { _data = value; }
        }

        public void ExecuteAfter()
        {
            
        }

        public void ExecuteBefore()
        {
            string tableName = _data.DrTableMaster["TableName"].ToString();

            if (tableName == "MT31" || tableName == "MT32" || tableName == "MT33")
            {
                CheckInvoice();
            }
            if (tableName == "MT34")
            {
                UpdateInvoices();
            }
        }

        public InfoCustomData Info 
        {
            get { return _info; }
        }

        #endregion

        private void UpdateInvoices()
        {
            Database dbData = _data.DbData;

            DataView dvMT34 = new DataView(_data.DsData.Tables[0]);
            dvMT34.RowStateFilter = DataViewRowState.Deleted | DataViewRowState.CurrentRows;
            DataRowView drvMaster = dvMT34[_data.CurMasterIndex];
            string MT34ID = drvMaster["MT34ID"].ToString();
            string MaKH = drvMaster["MaKH"].ToString();

            DataView dvDT34 = new DataView(_data.DsData.Tables[1]);
            dvDT34.RowStateFilter = DataViewRowState.Deleted | DataViewRowState.CurrentRows;
            dvDT34.RowFilter = "MT34ID = '" + MT34ID + "'";
            //Kiểm tra các chứng từ hợp lệ mới xử lý tiếp
            bool flag = true;
            if (drvMaster.Row.RowState != DataRowState.Deleted)
                flag = CheckCorrect(dvDT34, MaKH);
            if (!flag)
            {
                _info.Result = false;
                return;
            }
            foreach (DataRowView drvData in dvDT34)
            {
                if (drvData.Row.RowState == DataRowState.Unchanged)
                    continue;

                string MT31ID = drvData["MT31ID"].ToString();
                string TTNT = "0", TT = "0";
                string TTNT1 = "0", TT1 = "0";
                if (drvData.Row.RowState == DataRowState.Added)
                {
                    TTNT = drvData["TTNT"].ToString().Replace(",", ".");
                    TT = drvData["TT"].ToString().Replace(",", ".");
                    UpdateSource(dbData, TTNT1, TTNT, TT1, TT, MT31ID);
                    if (_info.Result == false)
                        break;
                }

                if (drvData.Row.RowState == DataRowState.Modified)
                {
                    string OrgID = "";
                    OrgID = drvData.Row["MT31ID", DataRowVersion.Original].ToString();
                    //sửa giá
                    if (OrgID == MT31ID)
                    {
                        //giá trị thanh toán trước khi sửa
                        TTNT1 = drvData.Row["TTNT", DataRowVersion.Original].ToString().Replace(",", ".");
                        TT1 = drvData.Row["TT", DataRowVersion.Original].ToString().Replace(",", ".");
                        // giá trị thanh toán sau khi sửa
                        TTNT = drvData.Row["TTNT", DataRowVersion.Current].ToString().Replace(",", ".");
                        TT = drvData.Row["TT", DataRowVersion.Current].ToString().Replace(",", ".");
                        UpdateSource(dbData, TTNT1, TTNT, TT1, TT, MT31ID);
                        if (_info.Result == false)
                            break;
                    } // sửa hóa đơn
                    else if (OrgID != MT31ID)
                    {
                        //Cập nhật cho Hóa Đơn mới
                        TTNT = drvData["TTNT"].ToString().Replace(",", ".");
                        TT = drvData["TT"].ToString().Replace(",", ".");
                        TTNT1 = TT1 = "0";
                        UpdateSource(dbData, TTNT1, TTNT, TT1, TT, MT31ID);
                        //Cập nhật cho hóa đơn trước
                        TTNT1 = drvData.Row["TTNT", DataRowVersion.Original].ToString().Replace(",", ".");
                        TT1 = drvData.Row["TT", DataRowVersion.Original].ToString().Replace(",", ".");
                        TTNT = TT = "0";
                        UpdateSource(dbData, TTNT1, TTNT, TT1, TT, OrgID);

                        if (_info.Result == false)
                            break;
                    }
                    
                }

                if (drvData.Row.RowState == DataRowState.Deleted)
                {
                    TTNT1 = drvData.Row["TTNT", DataRowVersion.Original].ToString().Replace(",", ".");
                    TT1 = drvData.Row["TT", DataRowVersion.Original].ToString().Replace(",", ".");
                    UpdateSource(dbData, TTNT1, TTNT, TT1, TT, MT31ID);
                    if (_info.Result == false)
                        break;
                }                              
            }
        }

        private void UpdateSource( Database dbData, string TTNT1, string TTNT, string TT1, string TT, string MT31ID)
        {
            string sql = "update MT31 set DaTTNT = DaTTNT - (" + TTNT1 + ")+(" + TTNT + "), DaTT = DaTT - (" + TT1 + ")+(" + TT + ") where MT31ID = '" + MT31ID + "'";
            int rec = 0;
            dbData.UpdateByNonQuery(sql, ref rec);
            if (rec == 0)
                dbData.UpdateByNonQuery(sql.Replace("MT31", "MT32"), ref rec);
            if (rec == 0)
                dbData.UpdateByNonQuery(sql.Replace("MT31", "MT33"), ref rec);
            _info.Result = (rec == 1);            
        }

        private void CheckInvoice()
        {
            DataView dvMT = new DataView(_data.DsData.Tables[0]);
            dvMT.RowStateFilter = DataViewRowState.Deleted | DataViewRowState.CurrentRows;
            DataRowView drvMaster = dvMT[_data.CurMasterIndex];
            string MTID = drvMaster[_data.DrTableMaster["Pk"].ToString()].ToString();
            string sql = "select MT31ID from DT34 where MT31ID = '" + MTID + "' and TT > 0";
            Database dbData = _data.DbData;
            if (dbData.GetDataTable(sql).Rows.Count == 0)
            {
                _info.Result = true;
                return;
            }

            if (drvMaster.Row.RowState == DataRowState.Deleted)
            {
                string msg = "Không thể xóa vì hóa đơn này đã được thanh toán. Cần xóa chứng từ thu hồi công nợ trước!";
                if (Config.GetValue("Language").ToString() == "1")
                    msg = UIDictionary.Translate(msg);
                XtraMessageBox.Show(msg);
                _info.Result = false;
                return;
            }

            if (drvMaster.Row.RowState == DataRowState.Modified)
            {
                if (!drvMaster.Row["TTien", DataRowVersion.Current].Equals(drvMaster.Row["TTien", DataRowVersion.Original])
                    || !drvMaster.Row["MaKH", DataRowVersion.Current].Equals(drvMaster.Row["MaKH", DataRowVersion.Original])
                    || !drvMaster.Row["NgayCT", DataRowVersion.Current].Equals(drvMaster.Row["NgayCT", DataRowVersion.Original])
                    || !drvMaster.Row["TkNo", DataRowVersion.Current].Equals(drvMaster.Row["TkNo", DataRowVersion.Original]))
                {
                    string msg = "Không thể điều chỉnh vì hóa đơn này đã được thanh toán. Cần xóa chứng từ thu hồi công nợ trước!";
                    if (Config.GetValue("Language").ToString() == "1")
                        msg = UIDictionary.Translate(msg);
                    XtraMessageBox.Show(msg);
                    _info.Result = false;
                    return;
                }
            }
            //if (drvMaster.Row.RowState == DataRowState.Unchanged)
            //{
            //    DataView dvDT = new DataView(_data.DsData.Tables[1]);
            //    dvDT.RowStateFilter = DataViewRowState.ModifiedCurrent;
            //    dvDT.RowFilter = _data.DrTableMaster["Pk"].ToString() + " = '" + MTID + "'";
            //    if (dvDT.Count > 0)
            //    {
            //        string msg = "Không thể điều chỉnh vì hóa đơn này đã được thanh toán. Cần điều chỉnh chứng từ thu hồi công nợ trước!";
            //        if (Config.GetValue("Language").ToString() == "1")
            //            msg = UIDictionary.Translate(msg);
            //        XtraMessageBox.Show(msg); 
            //        _info.Result = false;
            //        return;
            //    }
            //}
            _info.Result = true;
        }

        private bool CheckCorrect(DataView dvChungTu, string MaKH)
        {
            string sql = "", soct = "";
            DataTable dt = new DataTable();
            foreach (DataRowView drv in dvChungTu)
            {
                if (drv.Row.RowState == DataRowState.Deleted)
                    continue;
                sql = "select * from whoadonban where MT31ID = '" + drv.Row["MT31ID", DataRowVersion.Current].ToString() + "' and MaKh = '" + MaKH + "'";
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    sql = "select * from whoadonban where MT31ID = '" + drv.Row["MT31ID", DataRowVersion.Current].ToString() + "'";
                    dt = db.GetDataTable(sql);
                    if (dt.Rows.Count > 0)
                        soct = dt.Rows[0]["SoCT"].ToString();
                    XtraMessageBox.Show("Hóa đơn có số chứng từ " + soct + " không phải của khách hàng đã chọn.", Config.GetValue("PackageName").ToString());
                    return false;                    
                }
            }
            return true;
        }    
    }
}
