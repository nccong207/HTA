using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Windows.Forms;
using System.Data;
using CDTDatabase;
using DevExpress.XtraEditors;
using CDTLib;

namespace XuatAm
{
    public class XuatAm: ICData
    {
        private Database _dbData;
        public XuatAm()
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
            
        }

        public void ExecuteBefore()
        {
            //Tho chỉnh sửa ngày 20/03/2012 - thêm ngăn, kệ, kích thước
            if (CDTLib.Config.GetValue("XuatAm") == null || CDTLib.Config.GetValue("XuatAm").ToString() == "1" || _data.CurMasterIndex < 0)
                return;
            string tableName = _data.DrTableMaster["TableName"].ToString();
            DataRow drCurMaster = _data.DsData.Tables[0].Rows[_data.CurMasterIndex];
            if ((((tableName == "MT24") || (tableName == "MT43")) || (((tableName == "MT44") || (tableName == "MT45")) || (tableName == "MT32"))) 
                && (drCurMaster.RowState != DataRowState.Deleted))
            {
                _dbData = _data.DbData;
                //DataRow row1 = _data.DsData.Tables[0].Rows[_data.CurMasterIndex];
                double slx = 0;
                string mavt = "";
                string makho = "";

                string lo = ""; //Tho them 15/12/2011
                DateTime hansd=DateTime.Now; //Tho them 15/12/2011
                string mangan = "";
                string make = "";
                string makt = "";
                  
                DateTime ngayct = DateTime.Parse(drCurMaster["ngayct"].ToString());
                string[] values = new string[] { "@ngayct" };
                DataTable table1 = _dbData.GetDataSetByStore("tonkhotucthoi", values, new object[] { ngayct });
                string str0 = _data.DrTableMaster["Pk"].ToString();
                DataView v = new DataView(_data.DsData.Tables[1]);
                v.RowFilter = str0 + "='" + drCurMaster[str0].ToString() + "'";
                v.RowStateFilter = DataViewRowState.OriginalRows | DataViewRowState.ModifiedOriginal;
                for (int i = 0; i < v.Count; i++)
                {
                    double slxOrg = 0;
                    mavt = v[i]["mavt"].ToString().Trim();
                    makho = tableName == "MT44" ? drCurMaster["MaKho"].ToString() : v[i]["MaKho"].ToString().Trim();
                    slxOrg = double.Parse(v[i]["soluong"].ToString());

                    //Tho them 15/12/2011 tính  thêm lô, date
                    lo = v[i].Row.Table.Columns.Contains("Lo") ? v[i]["Lo"].ToString() : "";
                    mangan = v[i].Row.Table.Columns.Contains("MaNgan") ? v[i]["MaNgan"].ToString() : "";
                    make = v[i].Row.Table.Columns.Contains("MaKe") ? v[i]["MaKe"].ToString() : "";
                    makt = v[i].Row.Table.Columns.Contains("MaKT") ? v[i]["MaKT"].ToString() : "";

                    if (v[i].Row.Table.Columns.Contains("HanSuDung") && v[i]["HanSuDung"].ToString() != "")
                        hansd = DateTime.Parse(v[i]["HanSuDung"].ToString());

                    if (lo == "")
                        values = new string[] { "makho='", makho.Trim(), "' and mavt ='", mavt.Trim(), "'" };
                    else
                        values = new string[] { "makho='", makho.Trim(), "' and mavt ='", mavt.Trim(), "' and Lo ='", lo.Trim(), "' and HanSuDung ='", hansd.ToString(), "' and MaNgan = '",mangan,"' and MaKe = '",make,"' and MaKT = '",makt,"'" };

                    DataRow[] rowArray1Org = table1.Select(string.Concat(values));
                    if (rowArray1Org.Length > 0)
                    {
                        rowArray1Org[0]["slTon"] = double.Parse(rowArray1Org[0]["slton"].ToString()) + slxOrg;
                    }
                }
                 
                v.RowStateFilter = DataViewRowState.CurrentRows;
                for (int i = 0; i < v.Count; i++)
                {                    
                    slx = double.Parse(v[i]["SLQuyDoi"].ToString());
                    mavt = v[i]["mavt"].ToString().Trim();
                    makho = tableName == "MT44" ? drCurMaster["MaKho"].ToString() : v[i]["MaKho"].ToString().Trim();
                    //Tho add
                    lo = v[i].Row.Table.Columns.Contains("Lo") ? v[i]["Lo"].ToString() : "";
                    mangan = v[i].Row.Table.Columns.Contains("MaNgan") ? v[i]["MaNgan"].ToString() : "";
                    make = v[i].Row.Table.Columns.Contains("MaKe") ? v[i]["MaKe"].ToString() : "";
                    makt = v[i].Row.Table.Columns.Contains("MaKT") ? v[i]["MaKT"].ToString() : "";

                    if (v[i].Row.Table.Columns.Contains("HanSuDung") && v[i]["HanSuDung"].ToString() != "")
                        hansd = DateTime.Parse(v[i]["HanSuDung"].ToString());
                    //end
                    if (makho == "" || mavt == "" )
                        continue;
                    //if(lo == "")
                    //    values = new string[] { "makho='", makho.Trim(), "' and mavt ='", mavt.Trim(), "'" };
                    //else
                    values = new string[] { "makho='", makho.Trim(), "' and mavt ='", mavt.Trim(), "' and Lo ='", lo.Trim(), "' and HanSuDung ='", hansd.ToString(), "' and MaNgan = '", mangan, "' and MaKe = '", make, "' and MaKT = '", makt, "'" };
                    DataRow[] rowArray1 = table1.Select(string.Concat(values));
                    if (rowArray1.Length > 0)
                    {
                        double slTon = double.Parse(rowArray1[0]["slTon"].ToString());
                        if (slx > slTon)
                        {
                            string msg = " không đủ số lượng";
                            if (Config.GetValue("Language").ToString() == "1")
                                msg = UIDictionary.Translate(msg);
                            XtraMessageBox.Show(mavt + msg);
                            _info.Result = false;
                            goto Label_0487;
                        }
                        _data.DsData.Tables[1].Rows[i].SetColumnError("soluong", "");
                        rowArray1[0]["slTon"] = (double.Parse(rowArray1[0]["slTon"].ToString()) - slx);
                    }
                    else
                    {
                        string msg = "Không có tồn kho";
                        if (Config.GetValue("Language").ToString() == "1")
                            msg = UIDictionary.Translate(msg);
                        XtraMessageBox.Show(msg);
                        _info.Result = false;
                    }

                }
            Label_0487:
                v.RowFilter = string.Empty;
                v.RowStateFilter = DataViewRowState.CurrentRows;
                if (_info.Result)
                {
                }
            }
        }

        public InfoCustomData Info
        {
            get { return _info; }
        }

        #endregion
    }
}
