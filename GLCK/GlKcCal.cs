using System;
using System.Collections.Generic;
using System.Text;
using CDTDatabase;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Globalization;
namespace GLKC
{
    class GlKcCal
    {
        NumberFormatInfo nfi = new NumberFormatInfo();
        string _mact = "Z90";
        private DateTime _ngayCt1;
        private DateTime _ngayCt2;
        public Database _dbData = Database.NewDataDatabase();
        public DataTable TienCK;
        public DataRow _TransRow;
        public GlKcCal(int i, string namlv,DataRow TransRow)//i tháng cần tính
        {
            string str = i.ToString() +"/01/"  +  namlv;
            _ngayCt1 = DateTime.Parse(str);
            _ngayCt2 = _ngayCt1.AddMonths(1).AddDays(-1);
            _TransRow = TransRow;
            nfi.CurrencyDecimalSeparator = ".";
            nfi.CurrencyGroupSeparator = ",";
            Application.CurrentCulture.NumberFormat = nfi;
        }
        private DataTable getData()
        {
            DeleteBt();
            string sql;
            DataTable dt45 = new DataTable("GLCK");
            sql = "soduCK";
            string[] paranames;
            object[] paraValues;
            if (!_TransRow.Table.Columns.Contains("MaCongtrinh"))
            {
                paranames = new string[] { "@tk", "@tkdich","@ngayCt1", "@ngayCt2", "@mabp", "@mavv", "@maphi", "@macongtrinh" };
                paraValues = new object[] { _TransRow["TkNguon"].ToString(), _TransRow["TkDich"].ToString(), _ngayCt1, _ngayCt2, bool.Parse(_TransRow["MaBP"].ToString()) ? 1 : 0, bool.Parse(_TransRow["MaVV"].ToString()) ? 1 : 0, bool.Parse(_TransRow["MaPhi"].ToString()) ? 1 : 0, 0 };
            }
            else
            {
                paranames = new string[] { "@tk", "@tkdich", "@ngayCt1", "@ngayCt2", "@mabp", "@mavv", "@maphi", "@macongtrinh" };
                paraValues = new object[] { _TransRow["TkNguon"].ToString(), _TransRow["TkDich"].ToString(), _ngayCt1, _ngayCt2, bool.Parse(_TransRow["MaBP"].ToString()) ? 1 : 0, bool.Parse(_TransRow["MaVV"].ToString()) ? 1 : 0, bool.Parse(_TransRow["MaPhi"].ToString()) ? 1 : 0, bool.Parse(_TransRow["MaCongtrinh"].ToString()) ? 1 : 0 };
            }
            try
            {
                dt45 = _dbData.GetDataSetByStore(sql, paranames, paraValues);
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                return null;
            }
            return dt45;


        }
        public bool Createbt(ref decimal ps)
        {
            ps = 0;
            TienCK = getData();
            _dbData.BeginMultiTrans();
            try
            {
                //MessageBox.Show(TienCK.Rows.Count.ToString());
                foreach (DataRow dr in TienCK.Rows)
                {
                    ps += createBtdt(dr);
                }
                _dbData.EndMultiTrans();
            }
            catch (Exception ex)
            {
               
                _dbData.RollbackMultiTrans();
                XtraMessageBox.Show(ex.Message);
                return false;
            }
            return true; 
        }
        public decimal createBtdt(DataRow dr)
        {
            Guid iddt = Guid.NewGuid();
            string s = _TransRow["PhanLoai"].ToString().ToUpper() == "LÃI LỖ" ? "KCLL" : "KCCP";
            string soct = "'" + s + _ngayCt2.Month.ToString("00") + "/" + (Int32.Parse(_TransRow["TTKC"].ToString())).ToString("00") + "'";
            decimal tmp = 0;
            try
            {
                string tableName = "bltk";
                List<string> fieldName = new List<string>();
                List<string> Values = new List<string>();
                Guid id = new Guid();
                fieldName.Add("MTID");
                fieldName.Add("MTIDDT");
                fieldName.Add("Nhomdk");
                fieldName.Add("MaCT");
                fieldName.Add("SoCT");
                fieldName.Add("NgayCT");
                fieldName.Add("Ongba");
                fieldName.Add("DienGiai");
                fieldName.Add("TK");
                fieldName.Add("TKdu");
                fieldName.Add("Psno");
                fieldName.Add("Psco");
                if (bool.Parse(_TransRow["Maphi"].ToString()))
                {
                    if (!(dr["MaPhi"] is DBNull))
                    {
                        fieldName.Add("Maphi");
                    }
                }
                if (bool.Parse(_TransRow["MaVV"].ToString()))
                {
                    if (!(dr["MaVV"] is DBNull))
                    {
                        fieldName.Add("MaVV");
                    }
                }
                if (bool.Parse(_TransRow["MaBP"].ToString()))
                {
                    if (!(dr["MaBP"] is DBNull))
                    {
                        fieldName.Add("MaBP");
                    }
                }
                if (!(_TransRow["MaSP"] is DBNull))
                {
                    if (bool.Parse(_TransRow["MaSP"].ToString()))
                    {
                        if (!(dr["MaSP"] is DBNull))
                        {
                            fieldName.Add("MaSP");
                        }
                    }
                }
                if (_TransRow.Table.Columns.Contains("MaCongtrinh"))
                {
                    if (!(_TransRow["MaCongtrinh"] is DBNull))
                    {
                        if (bool.Parse(_TransRow["MaCongtrinh"].ToString()))
                        {
                            if (!(dr["MaCongtrinh"] is DBNull))
                            {
                                fieldName.Add("MaCongtrinh");
                            }
                        }
                    }
                }
                Values.Add("convert( uniqueidentifier,'" + id.ToString() + "')");
                Values.Add("convert( uniqueidentifier,'" + iddt.ToString() + "')");
                Values.Add("'" + _TransRow["MaCT"].ToString() + "'");
                Values.Add("'" + _mact + "'");
                Values.Add(soct);
                Values.Add("cast('" + _ngayCt2.ToString() + "' as datetime)");
                //Values.Add("''");
                Values.Add("''");
                Values.Add("N' " + _TransRow["DienGiai"] + " tháng " + _ngayCt2.Month.ToString() + "/" + _ngayCt2.Year.ToString() + "'");
                int kc = Int32.Parse(_TransRow["LoaiKC"].ToString());
                decimal tien = decimal.Parse(dr["Sodu"].ToString(), nfi);
                if (kc == 1 || (kc == 2 && tien > 0))
                {
                    Values.Add("'" + _TransRow["TKDich"].ToString() + "'");
                    Values.Add("'" + dr["Tk"].ToString() + "'");
                }
                else
                {
                    Values.Add("'" + dr["Tk"].ToString() + "'");
                    Values.Add("'" + _TransRow["TKDich"].ToString() + "'");
                }
                if ((kc == 2 && tien < 0) || kc == 0)
                {
                    decimal tien1 = -tien;
                    tmp = tien1;
                    Values.Add(tien1.ToString("###########0.######"));
                }
                else
                {
                    tmp = tien;
                    Values.Add(tien.ToString("###########0.######"));
                }
                Values.Add("0");

                if (bool.Parse(_TransRow["Maphi"].ToString()))
                {
                    if (!(dr["MaPhi"] is DBNull))
                    {
                        Values.Add("'" + dr["MaPhi"].ToString() + "'");
                    }
                }
                if (bool.Parse(_TransRow["MaVV"].ToString()))
                {
                    if (!(dr["MaVV"] is DBNull))
                    {
                        Values.Add("'" + dr["MaVV"].ToString() + "'");
                    }
                }
                if (bool.Parse(_TransRow["MaBP"].ToString()))
                {
                    if (!(dr["MaBP"] is DBNull))
                    {
                        Values.Add("'" + dr["MaBP"].ToString() + "'");
                    }
                }
                if (!(_TransRow["MaSP"] is DBNull))
                {
                    if (bool.Parse(_TransRow["MaSP"].ToString()))
                    {
                        if (!(dr["MaSP"] is DBNull))
                        {
                            Values.Add("'" + dr["MaSP"].ToString() + "'");
                        }
                    }
                }
                if (_TransRow.Table.Columns.Contains("MaCongtrinh"))
                {
                    if (!(_TransRow["MaCongtrinh"] is DBNull))
                    {
                        if (bool.Parse(_TransRow["MaCongtrinh"].ToString()))
                        {
                            if (!(dr["MaCongtrinh"] is DBNull))
                            {
                                Values.Add("'" + dr["MaCongtrinh"].ToString() + "'");
                            }
                        }
                    }
                }
                if (!_dbData.insertRow(tableName, fieldName, Values))
                {
                    return 0;
                }
                Values.Clear();
                Values.Add("convert( uniqueidentifier,'" + id.ToString() + "')");
                Values.Add("convert( uniqueidentifier,'" + iddt.ToString() + "')");
                Values.Add("'" + _TransRow["MaCT"].ToString() + "'");
                Values.Add("'" + _mact + "'");
                Values.Add(soct);
                Values.Add("cast('" + _ngayCt2.ToString() + "' as datetime)");
                //Values.Add("''");
                Values.Add("''");
                Values.Add("N' " + _TransRow["DienGiai"] + " tháng " + _ngayCt2.Month.ToString() + "/" + _ngayCt2.Year.ToString() + "'");
                if (kc == 1 || (kc == 2 && tien > 0))
                {
                    
                    Values.Add("'" + dr["Tk"].ToString() + "'");
                    Values.Add("'" + _TransRow["TKDich"].ToString() + "'");
                }
                else
                {                    
                    Values.Add("'" + _TransRow["TKDich"].ToString() + "'");
                    Values.Add("'" + dr["Tk"].ToString() + "'");
                }

                Values.Add("0");
                if ((kc == 2 && tien < 0) || kc == 0)
                {
                    decimal tien1 = -tien;
                    Values.Add(tien1.ToString("###########0.######"));
                }
                else
                    Values.Add(tien.ToString("###########0.######"));
                
                if (bool.Parse(_TransRow["Maphi"].ToString()))
                {
                    if (!(dr["MaPhi"] is DBNull))
                    {
                        Values.Add("'" + dr["MaPhi"].ToString() + "'");
                    }
                }
                if (bool.Parse(_TransRow["MaVV"].ToString()))
                {
                    if (!(dr["MaVV"] is DBNull))
                    {
                        Values.Add("'" + dr["MaVV"].ToString() + "'");
                    }
                }
                if (bool.Parse(_TransRow["MaBP"].ToString()))
                {
                    if (!(dr["MaBP"] is DBNull))
                    {
                        Values.Add("'" + dr["MaBP"].ToString() + "'");
                    }
                }
                if (!(_TransRow["MaSP"] is DBNull))
                {
                    if (bool.Parse(_TransRow["MaSP"].ToString()))
                    {
                        if (!(dr["MaSP"] is DBNull))
                        {
                            Values.Add("'" + dr["MaSP"].ToString() + "'");
                        }
                    }
                }
                if (_TransRow.Table.Columns.Contains("MaCongtrinh"))
                {
                    if (!(_TransRow["MaCongtrinh"] is DBNull))
                    {
                        if (bool.Parse(_TransRow["MaCongtrinh"].ToString()))
                        {
                            if (!(dr["MaCongtrinh"] is DBNull))
                            {
                                Values.Add("'" + dr["MaCongtrinh"].ToString() + "'");
                            }
                        }
                    }
                }
                if (!_dbData.insertRow(tableName, fieldName, Values))
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.Message);
                return 0;

            } 
            return tmp;
        }
        public bool DeleteBt()
        {
            string sql = "delete bltk where MaCT = '" + _mact + "' and NgayCt=cast('" + _ngayCt2.ToString() + "' as datetime) and NhomDK = '" + _TransRow["MaCT"].ToString() + "'";
            _dbData.UpdateByNonQuery(sql);
            sql = "delete bltk where nhomdk='KC' and NgayCt=cast('" + _ngayCt2.ToString() + "' as datetime) and Mact='" + _TransRow["MaCT"].ToString() + "'";
            _dbData.UpdateByNonQuery(sql);//vẫn giữ cách xóa cũ để xử lý trường hợp cập nhật plugins mới với số liệu vẫn là số liệu cũ
            return true;
        }
    }
}
