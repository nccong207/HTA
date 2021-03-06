using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Plugins;
using CDTDatabase;
using DevExpress.XtraEditors;
using CDTLib;

namespace TraHoaDon
{
    public class TraHoaDon : ICData
    {
        public TraHoaDon()
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
            if (tableName == "MT21" || tableName == "MT22" || tableName == "MT23"
                || tableName == "MT24" || tableName == "MT25")
            {
                CheckInvoice();
            }
            if (tableName == "MT26")
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
            
            DataView dvMT26 = new DataView(_data.DsData.Tables[0]);
            dvMT26.RowStateFilter = DataViewRowState.Deleted | DataViewRowState.CurrentRows;
            DataRowView drvMaster = dvMT26[_data.CurMasterIndex];
            string mt26ID = drvMaster["MT26ID"].ToString();
            string MaKH = drvMaster["MaKH"].ToString();

            DataView dvDT26 = new DataView(_data.DsData.Tables[1]);
            dvDT26.RowStateFilter = DataViewRowState.Deleted | DataViewRowState.CurrentRows;
            dvDT26.RowFilter = "MT26ID = '" + mt26ID + "'";
            //Kiểm tra các chứng từ hợp lệ mới xử lý tiếp
            if (drvMaster.Row.RowState != DataRowState.Deleted)
                CheckCorrect(dvDT26, MaKH);

            foreach (DataRowView drvData in dvDT26)
            {
                if (drvData.Row.RowState == DataRowState.Unchanged)
                    continue; 

                string mt21ID = drvData["MT21ID"].ToString();
                string TTNT = "0", TT = "0";
                string TTNT1 = "0", TT1 = "0";

                if (drvData.Row.RowState == DataRowState.Added)
                {
                    TTNT = drvData["TTNT"].ToString().Replace(",", ".");
                    TT = drvData["TT"].ToString().Replace(",", ".");
                    UpdateSource(dbData, TTNT1, TTNT, TT1, TT, mt21ID);
                    if (_info.Result == false)
                        break;
                }

                if (drvData.Row.RowState == DataRowState.Modified)
                {
                    string OrgID = "";
                    OrgID = drvData.Row["MT21ID", DataRowVersion.Original].ToString();
                    if (OrgID == mt21ID)
                    {
                        //giá trị thanh toán trước khi sửa
                        TTNT1 = drvData.Row["TTNT", DataRowVersion.Original].ToString().Replace(",", ".");
                        TT1 = drvData.Row["TT", DataRowVersion.Original].ToString().Replace(",", ".");
                        // giá trị thanh toán sau khi sửa
                        TTNT = drvData.Row["TTNT", DataRowVersion.Current].ToString().Replace(",", ".");
                        TT = drvData.Row["TT", DataRowVersion.Current].ToString().Replace(",", ".");
                        UpdateSource(dbData, TTNT1, TTNT, TT1, TT, mt21ID);
                        if (_info.Result == false)
                            break;
                    }
                    else if (OrgID != mt21ID)
                    {
                        //Cập nhật cho hóa đơn mới
                        TTNT = drvData["TTNT"].ToString().Replace(",", ".");
                        TT = drvData["TT"].ToString().Replace(",", ".");
                        TTNT1 = TT1 = "0";
                        UpdateSource(dbData, TTNT1, TTNT, TT1, TT, mt21ID);
                        //cập nhật cho hóa đơn cũ
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
                    UpdateSource(dbData, TTNT1, TTNT, TT1, TT, mt21ID);
                    if (_info.Result == false)
                        break;
                }

                               
            }
        }

        private void UpdateSource(Database dbData, string TTNT1, string TTNT, string TT1, string TT, string mt21ID)
        {
            string sql = "update MT21 set DaTTNT = DaTTNT - (" + TTNT1 + ")+(" + TTNT + "), DaTT = DaTT - (" + TT1 + ")+(" + TT +
                    ") where MT21ID = '" + mt21ID + "'";
            int rec = 0;
            dbData.UpdateByNonQuery(sql, ref rec);
            if (rec == 0)
                dbData.UpdateByNonQuery(sql.Replace("MT21", "MT22"), ref rec);
            if (rec == 0)
                dbData.UpdateByNonQuery(sql.Replace("MT21", "MT23"), ref rec);
            if (rec == 0)
                dbData.UpdateByNonQuery(sql.Replace("MT21", "MT24"), ref rec);
            if (rec == 0)
                dbData.UpdateByNonQuery(sql.Replace("MT21", "MT25"), ref rec);
            _info.Result = (rec == 1);            
        }

        private void CheckInvoice()
        {
            DataView dvMT = new DataView(_data.DsData.Tables[0]);
            dvMT.RowStateFilter = DataViewRowState.Deleted | DataViewRowState.CurrentRows;
            DataRowView drvMaster = dvMT[_data.CurMasterIndex];
            string MTID = drvMaster[_data.DrTableMaster["Pk"].ToString()].ToString();
            string sql = "select MT21ID from DT26 where MT21ID = '" + MTID + "'";
            Database dbData = _data.DbData;
            if (dbData.GetDataTable(sql).Rows.Count == 0)
            {
                _info.Result = true;
                return;
            }

            if (drvMaster.Row.RowState == DataRowState.Deleted)
            {
                string msg = "Không thể xóa vì hóa đơn này đã được thanh toán. Cần xóa chứng từ thanh toán trước!";
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
                    || !drvMaster.Row["TkCo", DataRowVersion.Current].Equals(drvMaster.Row["TkCo", DataRowVersion.Original]))
                {
                    string msg = "Không thể điều chỉnh vì hóa đơn này đã được thanh toán. Cần xóa chứng từ thanh toán trước!";
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
            //        string msg = "Không thể điều chỉnh vì hóa đơn này đã được thanh toán. Cần điều chỉnh chứng từ thanh toán trước!";
            //        if (Config.GetValue("Language").ToString() == "1")
            //            msg = UIDictionary.Translate(msg);
            //        XtraMessageBox.Show(msg);
            //        _info.Result = false;
            //        return;
            //    }
            //}
            _info.Result = true;
        }

        private void CheckCorrect(DataView dvChungTu, string MaKH)
        {
            string sql = "", soct = "";
            DataTable dt = new DataTable();
            foreach (DataRowView drv in dvChungTu)
            {
                if (drv.Row.RowState == DataRowState.Deleted)
                    return;
                sql = "select * from whoadonmua where MT21ID = '" + drv.Row["MT21ID", DataRowVersion.Current].ToString() + "' and MaKh = '" + MaKH + "'";
                dt = db.GetDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    sql = "select * from whoadonmua where MT21ID = '" + drv.Row["MT21ID", DataRowVersion.Current].ToString() + "'";
                    dt = db.GetDataTable(sql);
                    if (dt.Rows.Count > 0)
                        soct = dt.Rows[0]["SoCT"].ToString();
                    XtraMessageBox.Show("Hóa đơn có số chứng từ " + soct + " không phải của khách hàng đã chọn.", Config.GetValue("PackageName").ToString());
                    _info.Result = false;
                    return;
                }
            }
        }     
    }
}
