using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Windows.Forms;
using System.Data;
using CDTDatabase;

namespace SoDuHoaDon
{
    public class SoDuHoaDon : ICData
    {
        private Database _dbData;
        private string[] _lstMaCT = new string[] { "MDV", "PNM", "PNK", "PXT", "MCP", "HDV", "HDB", "HTL" };
        private string[] _lstTableName = new string[] { "MT21", "MT22", "MT23", "MT24", "MT25", "MT31", "MT32", "MT33" };
        public SoDuHoaDon()
        {
            _info = new InfoCustomData(IDataType.MasterDetailDt);
        }
        private InfoCustomData _info;
        private DataCustomData _data;
        #region ICData Members

        public DataCustomData Data
        {
            set { _data = value; }
        }

        public void ExecuteAfter()
        {
            _data.DsData.Tables[0].DefaultView.RowStateFilter = DataViewRowState.CurrentRows | DataViewRowState.Deleted;
            DataRowView drvMaster = _data.DsData.Tables[0].DefaultView[_data.CurMasterIndex];
            if (drvMaster.Row.RowState == DataRowState.Modified || drvMaster.Row.RowState == DataRowState.Unchanged)
                return;
            string MT35ID = drvMaster["MT35ID"].ToString();
            string LoaiCN = drvMaster["DMCNID"].ToString();
            _data.DsData.Tables[0].DefaultView.RowStateFilter = DataViewRowState.CurrentRows;
            if (LoaiCN == "1" || LoaiCN == "3")
            {
                _info.Result = true;
                return;
            }
            _dbData = _data.DbData;
            DataView dvDetail = new DataView(_data.DsData.Tables[1]);
            dvDetail.RowFilter = "MT35ID = '" + MT35ID + "'";
            dvDetail.RowStateFilter = DataViewRowState.CurrentRows | DataViewRowState.Deleted;
            foreach (DataRowView drv in dvDetail)
            {
                string sql = (LoaiCN == "2") ? "select MT31ID,MaCT from wHoaDonBan" : "select MT21ID,MaCT from wHoaDonMua";
                string SoHD = drv["SoHD"].ToString();
                string MaKH = drv["MaKH"].ToString();
                double Tien = double.Parse(drv["TienChi"].ToString()) - double.Parse(drv["TienThu"].ToString());
                double TienNT = double.Parse(drv["TienChiNT"].ToString()) - double.Parse(drv["TienThuNT"].ToString());
                if (LoaiCN == "4")
                {
                    Tien = -Tien;
                    TienNT = -TienNT;
                }
                sql += " where SoHoaDon = '" + SoHD + "' and MaKH = '" + MaKH + "'";
                DataTable dtData = _dbData.GetDataTable(sql);
                if (dtData.Rows.Count == 0)
                    continue;
                string MTID = dtData.Rows[0][0].ToString();
                string MaCT = dtData.Rows[0][1].ToString();
                int i = -1;
                for (i = 0; i < _lstMaCT.Length; i++)
                    if (_lstMaCT[i] == MaCT)
                        break;
                string TableName = _lstTableName[i];
                string sign = (drv.Row.RowState == DataRowState.Added) ? " + " : " - ";
                sql = "update " + TableName + " set DaTT = DaTT" + sign + Tien + ", DaTTNT = DaTTNT" + sign + TienNT +
                    " where " + TableName + "ID = '" + MTID + "'";
                _info.Result = _dbData.UpdateByNonQuery(sql);
                if (_info.Result == false)
                    break;
            }
        }

        public void ExecuteBefore()
        {
            
        }

        public InfoCustomData Info
        {
            get { return _info; }
        }

        #endregion

        //#region ICustomData Members
        //private string[] _lstMaCT = new string[] {"MDV","PNM","PNK","PXT","MCP","HDV","HDB","HTL"};
        //private string[] _lstTableName = new string[] {"MT21","MT22","MT23","MT24","MT25","MT31","MT32","MT33" };
        //private bool _result = true;
        //private StructPara _info;
        //public bool Result
        //{
        //    get
        //    {
        //        return _result;
        //    }
        //}

        //public StructPara Info
        //{
        //    set
        //    {
        //        _info = value;
        //    }
        //}

        //public void ExecuteBefore()
        //{

        //}

        //public void ExecuteAfter()
        //{
        //    if (_info.TableName == "MT35")
        //    {
        //        _data.DsData.Tables[0].DefaultView.RowStateFilter = DataViewRowState.CurrentRows | DataViewRowState.Deleted;
        //        DataRowView drvMaster = _data.DsData.Tables[0].DefaultView[_data.CurMasterIndex];
        //        if (drvMaster.Row.RowState == DataRowState.Modified || drvMaster.Row.RowState == DataRowState.Unchanged)
        //            return;
        //        string MT35ID = drvMaster["MT35ID"].ToString();
        //        string LoaiCN = drvMaster["DMCNID"].ToString();
        //        _data.DsData.Tables[0].DefaultView.RowStateFilter = DataViewRowState.CurrentRows;
        //        if (LoaiCN == "1" || LoaiCN == "3")
        //        {
        //            _result = true;
        //            return;
        //        }
        //        DataView dvDetail = new DataView(_data.DsData.Tables[1]);
        //        dvDetail.RowFilter = "MT35ID = '" + MT35ID + "'";
        //        dvDetail.RowStateFilter = DataViewRowState.CurrentRows | DataViewRowState.Deleted;
        //        foreach (DataRowView drv in dvDetail)
        //        {
        //            string sql = (LoaiCN == "2") ? "select MT31ID,MaCT from wHoaDonBan" : "select MT21ID,MaCT from wHoaDonMua";
        //            string SoHD = drv["SoHD"].ToString();
        //            string MaKH = drv["MaKH"].ToString();
        //            double Tien = double.Parse(drv["TienChi"].ToString()) - double.Parse(drv["TienThu"].ToString());
        //            double TienNT = double.Parse(drv["TienChiNT"].ToString()) - double.Parse(drv["TienThuNT"].ToString());
        //            if (LoaiCN == "4")
        //            {
        //                Tien = -Tien;
        //                TienNT = -TienNT;
        //            }
        //            sql += " where SoHoaDon = '" + SoHD + "' and MaKH = '" + MaKH + "'";
        //            DataTable dtData = _info.DbData.GetDataTable(sql);
        //            if (dtData.Rows.Count == 0)
        //                continue;
        //            string MTID = dtData.Rows[0][0].ToString();
        //            string MaCT = dtData.Rows[0][1].ToString();
        //            int i = -1;
        //            for (i = 0; i < _lstMaCT.Length; i++)
        //                if (_lstMaCT[i] == MaCT)
        //                    break;
        //            string TableName = _lstTableName[i];
        //            string sign = (drv.Row.RowState == DataRowState.Added) ? " + " : " - ";
        //            sql = "update " + TableName + " set DaTT = DaTT" + sign + Tien + ", DaTTNT = DaTTNT" + sign + TienNT +
        //                " where " + TableName + "ID = '" + MTID + "'";
        //            _result = _info.DbData.UpdateByNonQuery(sql);
        //            if (_result == false)
        //                break;
        //        }
        //    }
        //}


        //#endregion
    }
}

