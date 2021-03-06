using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Plugins;
using CDTDatabase;
using System.Globalization;
using CDTLib;

namespace GiaVon
{
    public class GiaVon : ICData
    {
        public GiaVon()
        {
            _info = new InfoCustomData(IDataType.MasterDetailDt);
        }
        private InfoCustomData _info;
        private DataCustomData _data;
        NumberFormatInfo nfi = new NumberFormatInfo();

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
            string table = _data.DrTableMaster["TableName"].ToString();
            if (table == "MT24" || table == "MT32" || table == "MT33" || table == "MT41"
                || table == "MT42" || table == "MT43" || table == "MT44" || table == "MT45" || table == "MT36")
            {              
                ApGiaPhieuXuat();
            }
        }

        public InfoCustomData Info
        {
            get { return _info; }
        }

        #endregion

        private void ApGiaPhieuXuat()
        {
            nfi.CurrencyDecimalSeparator = ".";
            nfi.CurrencyGroupSeparator = ",";

            string table = _data.DrTableMaster["TableName"].ToString();
            DataRow drMaster = _data.DsData.Tables[0].Rows[_data.CurMasterIndex];
            if (drMaster.RowState == DataRowState.Deleted)
                return;
            if ((table == "MT24" || table == "MT33" || table == "MT41" || table == "MT42") && !Boolean.Parse(drMaster["NhapTB"].ToString()))
                return;
            if (Config.GetValue("GiaTheoKho") != null && Config.GetValue("GiaTheoKho").ToString() == "0")   //chỉ áp giá nếu tính giá theo từng kho
                return;
                
            Database dbData = _data.DbData;
            string pk = _data.DrTableMaster["PK"].ToString();
            string pkDT = _data.DrTable["PK"].ToString();
            string mtID = drMaster[pk].ToString();
            DateTime denNgay = DateTime.Parse(drMaster["NgayCT"].ToString());
            DateTime tuNgay = denNgay.AddDays(-denNgay.Day + 1);
            DataView dvDetail = _data.DsData.Tables[1].DefaultView;
            dvDetail.RowFilter = pk + " = '" + mtID + "'";
            foreach (DataRowView drv in dvDetail)
            {
                string maKho = table == "MT44" ? drMaster["MaKho"].ToString() : drv["MaKho"].ToString();
                string maVT = drv["MaVT"].ToString();
                decimal slXuat = decimal.Parse(drv["SoLuong"].ToString(),nfi);
                object[] os;
                if ((table == "MT24" || table == "MT33" || table == "MT41" || table == "MT42") && Boolean.Parse(drMaster["NhapTB"].ToString()))
                {
                    if (drv.Row.RowState == DataRowState.Modified || drv.Row.RowState == DataRowState.Unchanged)
                        os = dbData.GetValueByStore("TinhGiaBQDD",
                            new string[] { "MTIDDT", "TuNgay", "DenNgay", "MaKho", "MaVT", "DonGia" },
                            new object[] { drv[pkDT], tuNgay, denNgay, maKho, maVT, null },
                            new SqlDbType[] { SqlDbType.UniqueIdentifier, SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.NVarChar, SqlDbType.NVarChar, SqlDbType.Decimal },
                            new ParameterDirection[] { ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Output });
                    else
                        os = dbData.GetValueByStore("TinhGiaBQDD",
                            new string[] { "MTIDDT", "TuNgay", "DenNgay", "MaKho", "MaVT", "DonGia" },
                            new object[] { null, tuNgay, denNgay, maKho, maVT, null },
                            new SqlDbType[] { SqlDbType.UniqueIdentifier, SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.NVarChar, SqlDbType.NVarChar, SqlDbType.Decimal },
                            new ParameterDirection[] { ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Output });
                }
                else
                {
                    if (drv.Row.RowState == DataRowState.Modified || drv.Row.RowState == DataRowState.Unchanged)
                        os = dbData.GetValueByStore("TinhGia",
                            new string[] { "MTIDDT", "TuNgay", "DenNgay", "MaKho", "MaVT", "SlXuat", "DonGia" },
                            new object[] { drv[pkDT], tuNgay, denNgay, maKho, maVT, slXuat, null },
                            new SqlDbType[] { SqlDbType.UniqueIdentifier, SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.NVarChar, SqlDbType.NVarChar, SqlDbType.Decimal, SqlDbType.Decimal },
                            new ParameterDirection[] { ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Output });
                    else            
                       
                        os = dbData.GetValueByStore("TinhGia",
                            new string[] { "MTIDDT", "TuNgay", "DenNgay", "MaKho", "MaVT", "SlXuat", "DonGia" },
                            new object[] { null, tuNgay, denNgay, maKho, maVT, slXuat, null },
                            new SqlDbType[] { SqlDbType.UniqueIdentifier, SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.NVarChar, SqlDbType.NVarChar, SqlDbType.Decimal, SqlDbType.Decimal },
                            new ParameterDirection[] { ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Output });                                            
                }               

                if (os == null || os.Length == 0)
                    continue;
                decimal giaVon = decimal.Parse(os[0].ToString(), nfi);
                if (giaVon == -1 || giaVon > 10000000000)
                    continue;
                if (drv.Row.Table.Columns.Contains("DGQuyDoi"))
                    drv.Row["DGQuyDoi"] = giaVon;
                //else
                //    if (drv.Row.Table.Columns.Contains("Gia"))
                //        drv.Row["Gia"] = giaVon;
            }
            dvDetail.RowFilter = "";
            dvDetail.RowStateFilter = DataViewRowState.CurrentRows;
        }
    }
}
