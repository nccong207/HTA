using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Windows.Forms;
using System.Data;
using CDTDatabase;
using CDTLib;

namespace ChiPhi
{
    public class ChiPhi : ICData
    {
        #region ICData Members
        private DataCustomData _data;
        private InfoCustomData _info;
        public ChiPhi()
        {
            _info = new InfoCustomData(IDataType.MasterDetailDt);
        }

        public DataCustomData Data
        {
            set { _data = value; }
        }

        public void ExecuteAfter()
        {
            string tableName = _data.DrTableMaster["TableName"].ToString();
            if (tableName != "MT22" && tableName != "MT23")
                return;
            string pk = _data.DrTableMaster["Pk"].ToString();
            DataRow drMaster = _data.DsData.Tables[0].Rows[_data.CurMasterIndex];
            if (drMaster.RowState != DataRowState.Deleted)
            {
                string sql = "update blvt set psno = psno + cp, psnont = psnont + cpnt, dongia = (psno + cp)/soluong, dongiant = (psnont + cpnt)/soluong ";
                sql += " where MTID = '" + drMaster[pk].ToString() + "' and soluong > 0";
                _info.Result = _data.DbData.UpdateByNonQuery(sql);
            }
            else
                _info.Result = true;
        }

        public void ExecuteBefore()
        {
            string tableName = _data.DrTableMaster["TableName"].ToString();
            _dbData = _data.DbData;
            if (tableName == "MT22" || tableName == "MT23")
            {
                DataView dvDt = new DataView(_data.DsData.Tables[1]);
                dvDt.RowStateFilter = DataViewRowState.Deleted;
                if (dvDt.Count > 0)
                {
                    string pkDt = _data.DrTable["Pk"].ToString();
                    foreach (DataRowView drv in dvDt)
                    {
                        string dtid = drv[pkDt].ToString();
                        string sql = "select * from DT25 where dt22ID = '" + dtid + "'";
                        DataTable dt = _dbData.GetDataTable(sql);
                        if (dt.Rows.Count > 0)
                        {
                            _info.Result = false;
                            string msg = "Không thể xóa vì chứng từ này đã phân bổ chi phí mua hàng!" +
                                "\nNếu thực sự muốn xóa, vui lòng tìm và xóa tất cả chứng từ chi phí mua hàng liên quan!";
                            if (Config.GetValue("Language").ToString() == "1")
                                msg = UIDictionary.Translate(msg);
                            MessageBox.Show(msg);
                            return;
                        }
                    }
                }
            }
            if (tableName != "MT25")
                return;
            try
            {
                _drMaster = _data.DsData.Tables[0].Rows[_data.CurMasterIndex];
                DataView dvDt = new DataView(_data.DsData.Tables[1]);
                DataView dvDtOrg = new DataView(_data.DsData.Tables[1]);
                dvDt.RowStateFilter = DataViewRowState.ModifiedCurrent | DataViewRowState.Deleted | DataViewRowState.Added;
                if (_drMaster.RowState == DataRowState.Added)
                {//
                    _action = 0;
                    dvDt.RowStateFilter = DataViewRowState.Added;
                    mt25id = _drMaster["MT25ID"].ToString();
                    foreach (DataRowView drview in dvDt)
                    {
                        _drDetail = drview;
                        if (_drDetail["MT25ID"].ToString() == mt25id)
                        {
                            dt25id = _drDetail["DT25ID"].ToString();
                            UpdateChiPhi();
                        }
                    }
                }
                else if (_drMaster.RowState == DataRowState.Deleted)
                {
                    _action = 2;//Xóa
                    _data.DsData.Tables[0].DefaultView.RowStateFilter = DataViewRowState.Deleted;
                    mt25id = _data.DsData.Tables[0].DefaultView[0]["MT25ID"].ToString();
                    dvDt.RowStateFilter = DataViewRowState.Deleted;
                    foreach (DataRowView drview in dvDt)
                    {
                        _drDetail = drview;
                        if (_drDetail["MT25ID"].ToString() == mt25id)
                        {
                            dt25id = _drDetail["DT25ID"].ToString();
                            UpdateChiPhi();
                        }
                    }
                    _data.DsData.Tables[0].DefaultView.RowStateFilter = DataViewRowState.CurrentRows;
                }
                else if (_drMaster.RowState == DataRowState.Modified || dvDt.Count > 0)
                {
                    mt25id = _drMaster["MT25ID"].ToString();

                    foreach (DataRow dr in _data.DsData.Tables[1].Rows)
                    {
                        if (dr.RowState == DataRowState.Deleted)
                        { continue; }
                        if (dr.RowState == DataRowState.Added)
                        {
                            if (dr["MT25ID"].ToString() != mt25id)
                            { continue; }
                            _action = 0;//xem như thêm mới
                            dvDt.RowStateFilter = DataViewRowState.Added;
                            dvDt.Sort = "DT25ID";
                            _drDetail = dvDt.FindRows(dr["DT25ID"])[0];
                            UpdateChiPhi();
                        }
                        else if (dr.RowState == DataRowState.Modified)
                        {
                            if (dr["MT25ID"].ToString() != mt25id)
                            { continue; }
                            _action = 1;//chỉnh sửa
                            dvDt.RowStateFilter = DataViewRowState.ModifiedCurrent;
                            dvDtOrg.RowStateFilter = DataViewRowState.ModifiedOriginal;
                            dvDt.Sort = "DT25ID";
                            dvDtOrg.Sort = "DT25ID";
                            _drDetail = dvDt.FindRows(dr["DT25ID"])[0];
                            _drDetailOrg = dvDtOrg.FindRows(dr["DT25ID"])[0];
                            UpdateChiPhi();
                        }

                    }
                    _action = 2;//Xóa
                    dvDt.RowStateFilter = DataViewRowState.Deleted;
                    foreach (DataRowView drview in dvDt)
                    {
                        _drDetail = drview;
                        if (_drDetail["MT25ID"].ToString() == mt25id)
                        {
                            dt25id = _drDetail["DT25ID"].ToString();
                            UpdateChiPhi();
                        }
                    }

                }

                _info.Result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                _info.Result = false;
            }
        }

        public InfoCustomData Info
        {
            get { return _info; }
        }

        #endregion

        DataRow _drMaster;
        DataRowView _drDetail;
        DataRowView _drDetailOrg;
        int _action = 0;
        string mt25id;
        string dt25id;
        Database _dbData;


        private void UpdateChiPhi()
        {
            string sql = "";
            if (_action == 0)
            {
                sql = "update blvt set ";
                sql += " CPNT=CPNT + " + _drDetail["PsNT"].ToString().Replace(",", ".") + ",";
                sql += " CP=CP + " + _drDetail["Ps"].ToString().Replace(",", ".") + ",";
                sql += " PsNoNT=PsNoNT + " + _drDetail["PsNT"].ToString().Replace(",", ".") + ",";
                sql += " PsNo=PsNo + " + _drDetail["Ps"].ToString().Replace(",", ".");
                sql += " where MTID=cast('" + _drDetail["MT22ID"].ToString() + "' as uniqueidentifier) and MTIDDT =cast('" + _drDetail["DT22ID"].ToString() + "' as uniqueidentifier)";
                //MessageBox.Show(sql + "  action = " + _action.ToString());
                _dbData.UpdateByNonQuery(sql);
            }
            else if (_action == 1)
            {

                sql = "update blvt set ";
                sql += " CPNT=CPNT + " + _drDetail["PsNT"].ToString().Replace(",", ".") + ",";
                sql += " CP=CP + " + _drDetail["Ps"].ToString().Replace(",", ".") + ",";
                sql += " PsNoNT=PsNoNT + " + _drDetail["PsNT"].ToString().Replace(",", ".") + ",";
                sql += " PsNo=PsNo + " + _drDetail["Ps"].ToString().Replace(",", ".");
                sql += " where MTID=cast('" + _drDetail["MT22ID"].ToString() + "' as uniqueidentifier) and MTIDDT =cast('" + _drDetail["DT22ID"].ToString() + "' as uniqueidentifier)";

                _dbData.UpdateByNonQuery(sql);
                //MessageBox.Show(sql + "  action = " + _action.ToString() );
                sql = "update blvt set ";
                sql += " CPNT=CPNT  " + " - " + _drDetailOrg["PsNT"].ToString().Replace(",", ".") + ",";
                sql += " CP=CP  " + " - " + _drDetailOrg["Ps"].ToString().Replace(",", ".") + ",";
                sql += " PsNoNT=PsNoNT  " + " - " + _drDetailOrg["PsNT"].ToString().Replace(",", ".") + ",";
                sql += " PsNo=PsNo  " + " - " + _drDetailOrg["Ps"].ToString().Replace(",", ".");
                sql += " where MTID=cast('" + _drDetailOrg["MT22ID"].ToString() + "' as uniqueidentifier) and MTIDDT =cast('" + _drDetailOrg["DT22ID"].ToString() + "' as uniqueidentifier)";
                // MessageBox.Show(sql + "  action = " + _action.ToString() + "  " + Name);
                _dbData.UpdateByNonQuery(sql);
                //sql = "update blvt set dongiaNT=PsNoNT/Soluong, Dongia=PsNo/Soluong ";
                //sql += " where Soluong>0 and MTID=cast('" + _drDetailOrg["MT" + Name + "ID"].ToString() + "' as uniqueidentifier) and MTIDDT =cast('" + _drDetailOrg["DT" + Name + "ID"].ToString() + "' as uniqueidentifier)";
                //_dbData.UpdateByNonQuery(sql);
                //MessageBox.Show(sql + "  action = " + _action.ToString());
            }
            else if (_action == 2)
            {
                sql = "update blvt set ";
                sql += " CPNT=CPNT - " + _drDetail["PsNT"].ToString().Replace(",", ".") + ",";
                sql += " CP=CP - " + _drDetail["Ps"].ToString().Replace(",", ".") + ",";
                sql += " PsNoNT=PsNoNT - " + _drDetail["PsNT"].ToString().Replace(",", ".") + ",";
                sql += " PsNo=PsNo - " + _drDetail["Ps"].ToString().Replace(",", ".");
                sql += " where MTID=cast('" + _drDetail["MT22ID"].ToString() + "' as uniqueidentifier) and MTIDDT =cast('" + _drDetail["DT22ID"].ToString() + "' as uniqueidentifier)";
                _dbData.UpdateByNonQuery(sql);
                //MessageBox.Show(sql + "  action = " + _action.ToString());
            }


            sql = "update blvt set dongia=psno/soluong, dongiaNT=psnoNT/soluong ";
            sql += " where soluong >0  and MTID=cast('" + _drDetail["MT22ID"].ToString() + "' as uniqueidentifier) and MTIDDT =cast('" + _drDetail["DT22ID"].ToString() + "' as uniqueidentifier)";
            _dbData.UpdateByNonQuery(sql);



        }
    }
}

