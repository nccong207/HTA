using System;
using System.Collections.Generic;
using System.Text;
using DevExpress;
using DevExpress.XtraEditors;
using System.Data;
using CDTDatabase;
using CDTLib;
using Plugins;
using System.Windows.Forms;

namespace KiemTraHD
{
    public class KiemTraHD:ICData
    {
        #region ICData Members
        private InfoCustomData _info;
        private DataCustomData _data;
        Database db = Database.NewDataDatabase();

        public KiemTraHD()
        {
            _info = new InfoCustomData(IDataType.MasterDetailDt);
        }

        public DataCustomData Data
        {
            set { _data = value; }
        }

        public void ExecuteAfter()
        {
            
        }        

        public void ExecuteBefore()
        {
            KiemTra();
        }

        void KiemTra()
        {
            string tableName = _data.DrTableMaster["TableName"].ToString().Trim().ToUpper();            
            if (tableName.Equals("MT26") || tableName.Equals("MT34"))
            {
                if (_data.CurMasterIndex < 0)
                    return;

                string sql = "";
                string soct = "";

                DataRow drMaster = _data.DsData.Tables[0].Rows[_data.CurMasterIndex];
                if (drMaster.RowState == DataRowState.Deleted)
                    return;
                string MaKH = drMaster.Table.Columns.Contains("MaKH") == true ? drMaster["MaKH"].ToString() : "";
                if( MaKH == "" )
                    return;
                
                DataTable dt = new DataTable();
                DataTable dtHoaDon = _data.DsData.Tables[1];
                DataView dvHoaDon = new DataView(dtHoaDon);
                if(tableName.Equals("MT26"))
                    dvHoaDon.RowFilter = "MT26ID = '"+drMaster["MT26ID"].ToString()+"'";
                else
                    dvHoaDon.RowFilter = "MT34ID = '" + drMaster["MT34ID"].ToString() + "'";
                foreach (DataRowView drv in dvHoaDon)
                {
                    if (drv.Row.RowState == DataRowState.Deleted)
                        return;
                    if (tableName.Equals("MT26"))
                    {
                        sql = "select * from whoadonmua where MT21ID = '" + drv.Row["MT21ID", DataRowVersion.Current].ToString() + "' and MaKh = '"+MaKH+"'";
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
                    else
                    {
                        sql = "select * from whoadonban where MT31ID = '" + drv.Row["MT31ID", DataRowVersion.Current].ToString() + "' and MaKh = '" + MaKH + "'";
                        dt = db.GetDataTable(sql);
                        if (dt.Rows.Count == 0)
                        {
                            sql = "select * from whoadonban where MT31ID = '" + drv.Row["MT31ID", DataRowVersion.Current].ToString() + "'";
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

        public InfoCustomData Info
        {
            get { return _info ; }
        }

        #endregion
    }
}
