using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CDTDatabase;
using CDTLib;

namespace Inventory
{
    public class GiaTrungBinh
    {
        int Nam = int.Parse(Config.GetValue("NamLamViec").ToString());
        private Database _dbData;
        private Database _dbStruct = Database.NewStructDatabase();
        private DataTable _dtVatTu;
        private int _tuThang;
        private int _denThang;
        private string _condition;
        DateTime TuNgay ;
        DateTime DenNgay;
        string[] Paras;
        object[] Values;
        private string makholaygia=null;
        public DataTable DtVatTu
        {
            get { return _dtVatTu; }
            set { _dtVatTu = value; }
        }

        public GiaTrungBinh(Database dbData, int tuThang, int denThang, string makho, string condition)
        {
            _tuThang = tuThang;
            _denThang = denThang;
            _condition = condition;
            if (makho != null)
                makholaygia = makho;
            _dbData = dbData;
        }
        //public int solantinh()
        //{
        //    string str = _tuThang.ToString() + "/" + "01" + "/" + Nam.ToString();
        //    DateTime TuNgay = Convert.ToDateTime(str);
        //    str = (_denThang).ToString() + "/" + "01" + "/" + Nam.ToString();
        //    DateTime DenNgay = Convert.ToDateTime(str).AddMonths(1).AddDays(-1);
        //    string sql = "select count(makho) from blvt where soluong>0 and maCt='PDC'  and ngayct between cast('" + TuNgay.ToString() + "' as datetime) and cast('" + DenNgay.ToString() + "' as datetime) group by makho";
        //    DataTable solan = _dbData.GetDataTable(sql);
        //    return solan.Rows.Count;
        //}
        public void TinhGia()
        {
            string str = _tuThang.ToString() + "/" + "01" + "/" + Nam.ToString();
            TuNgay = Convert.ToDateTime(str);
            str = (_denThang).ToString() + "/" + "01" + "/" + Nam.ToString();
            DenNgay = Convert.ToDateTime(str).AddMonths(1).AddDays(-1);
            string dk = _condition + " and MaKho = '" + makholaygia + "'";
            Paras = new string[] {"@tungay","@denngay", "@dk"};
            Values = new object[] { TuNgay, DenNgay, dk };

            _dtVatTu = _dbData.GetDataSetByStore("TinhgiaTB", Paras, Values);
            ApGia();
        }

        public void ApGia()
        {
            string blvtid;

            string sql = "select systableid from systable where TableName='BLVT' and syspackageid=" + Config.GetValue("sysPackageID").ToString();
            blvtid = _dbStruct.GetValue(sql).ToString();
            sql = "select sysfieldid from sysfield where systableid=" + blvtid + " and FieldName='Dongia'";
            string dongiaID = _dbStruct.GetValue(sql).ToString();
            //sql = "select sysfieldid from sysfield where systableid=" + blvtid + " and FieldName='Soluong_x'";
            //string Soluong_xID=_dbStruct.GetValue(sql).ToString();
            //sql = "select sysfieldid from sysfield where systableid=" + blvtid + " and FieldName='Soluong'";
            //string Soluong_nID = _dbStruct.GetValue(sql).ToString();
            sql= "select b.mttableid,b.dttableid,a.*,c.Pk from sysdataconfigdt a,sysdataconfig b, systable c ";
            sql += "where b.mttableid = c.systableid and b.blconfigid=a.blconfigid and c.tablename in ('MT24','MT32','MT33','MT42','MT43','MT44','MT45','MTGiaCong')";
            //sql +="where b.blconfigid=a.blconfigid  ";
            sql += "and b.systableid=" + blvtid + " AND blfieldid=" + dongiaID;
            //sql += " AND (a.blconfigid in(select blconfigid from sysdataconfigdt where   blfieldid=" + Soluong_xID + "))";
            DataTable dsMtdt = _dbStruct.GetDataTable(sql);
            string mt;
            string dt;
            //string GiaField = dongiaID;//
            string tygiaField = _dbStruct.GetValue("select sysfieldid from sysfield where sysTableid=" + blvtid + " and FieldName='TyGia'").ToString();//trong blvt là 1795 - rồi tìm trong bảng nguồn tương ứng
            //string GiaNTField = _dbStruct.GetValue("select sysfieldid from sysfield where sysTableid=" + blvtid + " and FieldName='DonGiaNT'").ToString();//trong blvt là 1792 - 
            string mavtField = _dbStruct.GetValue("select sysfieldid from sysfield where sysTableid=" + blvtid + " and FieldName='MaVT'").ToString();//trong blvt là 1796 - rồi tìm trong bảng nguồn tương ứng
            string makhoField = _dbStruct.GetValue("select sysfieldid from sysfield where sysTableid=" + blvtid + " and FieldName='MaKho'").ToString();//trong blvt là 1797 - rồi tìm trong bảng nguồn tương ứng
            string ngayctField = _dbStruct.GetValue("select sysfieldid from sysfield where sysTableid=" + blvtid + " and FieldName='NgayCt'").ToString();//trong blvt là 1782 - rồi tìm trong bảng nguồn tương ứng
            UpdateFormula formula;
            PostBl post;
            try
            {
                foreach (DataRow dr in dsMtdt.Rows)//duyệt qua từng loại chứng từ
                {
                  
                    mt = _dbStruct.GetValue("select tableName from systable where systableid=" + dr["mttableid"].ToString()).ToString();
                    dt = _dbStruct.GetValue("select tableName from systable where systableid=" + dr["dttableid"].ToString()).ToString();
                    string GiaFieldName;
                    string tygiaFieldName="";
                    string GiaNTFieldName;
                    string mavtFieldName;
                    string makhoFieldName;
                    string ngayctFieldName;
                    if (!(dr["dtfieldid"] is DBNull) || !(dr["formula"] is DBNull))   
                    {//Lấy thông tin các trường cần dùng
                        //GiaFieldName = _dbStruct.GetValue("  select dtfieldid from sysdataconfigdt where blfieldid=" + GiaField + "  and blconfigid=" + dr["blconfigid"].ToString()).ToString();
                        //GiaFieldName = _dbStruct.GetValue("  select fieldName from sysfield where sysfieldid=" + GiaFieldName).ToString();
                        GiaFieldName = "DGQuyDoi";
                        mavtFieldName = _dbStruct.GetValue("  select dtfieldid from sysdataconfigdt where blfieldid=" + mavtField + "  and blconfigid=" + dr["blconfigid"].ToString()).ToString();
                        mavtFieldName = _dbStruct.GetValue("  select fieldName from sysfield where sysfieldid=" + mavtFieldName).ToString();
                        //xét trường hợp mã kho nằm trong dt hay mt
                        bool khoinMt = false;
                        makhoFieldName = _dbStruct.GetValue("  select dtfieldid from sysdataconfigdt where blfieldid=" + makhoField + "  and blconfigid=" + dr["blconfigid"].ToString()).ToString();
                        if (makhoFieldName == null || makhoFieldName == "")
                        {
                            makhoFieldName = _dbStruct.GetValue("  select mtfieldid from sysdataconfigdt where blfieldid=" + makhoField + "  and blconfigid=" + dr["blconfigid"].ToString()).ToString();
                            if (makhoFieldName != "")
                                khoinMt = true;
                        }
                        makhoFieldName = makhoFieldName == "" ? "MaKho" : _dbStruct.GetValue("  select fieldName from sysfield where sysfieldid=" + makhoFieldName).ToString();
                        ngayctFieldName = _dbStruct.GetValue("  select mtfieldid from sysdataconfigdt where blfieldid=" + ngayctField + " and blconfigid=" + dr["blconfigid"].ToString()).ToString();
                        ngayctFieldName = _dbStruct.GetValue("  select fieldName from sysfield where sysfieldid=" + ngayctFieldName).ToString();
                        //GiaNTFieldName = _dbStruct.GetValue("  select dtfieldid from sysdataconfigdt where blfieldid=" + GiaNTField + "  and blconfigid=" + dr["blconfigid"].ToString()).ToString();
                        //GiaNTFieldName = _dbStruct.GetValue("  select fieldName from sysfield where sysfieldid=" + GiaNTFieldName).ToString();

                        //tygiaFieldName = _dbStruct.GetValue("  select mtfieldid from sysdataconfigdt where blfieldid=" + tygiaField + "  and blconfigid=" + dr["blconfigid"].ToString()).ToString();
                        //if (tygiaFieldName != "")
                        //{
                        //    tygiaFieldName = _dbStruct.GetValue("  select fieldName from sysfield where sysfieldid=" + tygiaFieldName).ToString();
                        //}
                        if (mt == "MT44" && makhoFieldName.ToUpper() == "MAKHON")
                            continue;
                        updateMTDT(mt, dt, GiaFieldName, mavtFieldName, makhoFieldName, ngayctFieldName,khoinMt, dr["Pk"].ToString());
                        formula = new UpdateFormula(_dbData, dr["mttableid"].ToString(), dr["dttableid"].ToString(), GiaFieldName, TuNgay, DenNgay, ngayctFieldName, _condition.Replace("MaVT in", mavtFieldName + " in"), dr["Pk"].ToString());
                        formula.Update();
                        string timeCon = mt + "." + ngayctFieldName + " between cast('" + TuNgay.ToString() + "' as datetime) and cast('" + DenNgay.ToString() + "' as datetime)";
                        post = new PostBl(_dbData, dr["mttableid"].ToString(), timeCon, _condition.Replace("MaVT in", mavtFieldName + " in"), dr["Pk"].ToString());
                        post.Post();
                    }
                    

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                _dbData.RollbackMultiTrans();
            }
        }

        private void updateMTDT(string mt, string dt,string Giafield,string mavtField,string makhoField,string ngayctFiedl,bool khoinMt, string pk)
        {
            string sql;
            //if (makholaygia == null)
            //{
            //    if (khoinMt)
            //    {
            //        sql = "update " + dt + " set " + Giafield + " = dongia from " + dt + " y," + mt.Trim() + " a, banggiatb x  ";
            //        sql += " where  y." + mavtField + "=x.mavt  ";
            //        sql += " and y." + mt.Trim() + "id = a." + mt.Trim() + "id and  a." + makhoField + "=x.makho and ";
            //        sql += "a." + ngayctFiedl + " between cast('" + TuNgay.ToString() + "' as datetime) and cast('" + DenNgay.ToString() + "' as datetime)";
            //        sql += " and x.ngayct=cast('" + DenNgay.ToString() + "' as datetime)";
            //        if (_condition != "")
            //            sql += " and (" + _condition.Replace("MaVT in","x.MaVT in") + ")";

            //        if (mt == "MT24" || mt == "MT33" || mt == "MT42")
            //        {
            //            sql += " and a.NhapTb=1";
            //        }

            //    }
            //    else
            //    {
            //        sql = "update " + dt + " set " + Giafield + " = dongia from " + dt + " y, banggiatb x  ";
            //        sql += " where  y." + mavtField + "=x.mavt and y." + makhoField + "=x.makho ";
            //        sql += " and y." + mt.Trim() + "id in  (select " + mt.Trim() + "id from " + mt + " where ";
            //        sql += mt.Trim() + "." + ngayctFiedl + " between cast('" + TuNgay.ToString() + "' as datetime) and cast('" + DenNgay.ToString() + "' as datetime)";
            //        sql += " and x.ngayct=cast('" + DenNgay.ToString() + "' as datetime)";
            //        if (_condition != "")
            //            sql += " and (" + _condition.Replace("MaVT in", "x.MaVT in") + ")";

            //        if (mt == "MT24" || mt == "MT33" || mt == "MT42")
            //        {
            //            sql += " and " + mt.Trim() + ".NhapTb=1";

            //        }
            //        sql += ")";
            //    }
            //}
            //else
            //{
            //    if (khoinMt)
            //    {
                    sql = "update " + dt + " set " + Giafield + " = dongia from " + dt + " y," + mt.Trim() + " a, banggiatb x  ";
                    sql += " where  y." + mavtField + "=x.mavt  ";
                    sql += " and y." + pk + " = a." + pk + " and ";
                    sql += "a." + ngayctFiedl + " between cast('" + TuNgay.ToString() + "' as datetime) and cast('" + DenNgay.ToString() + "' as datetime)";
                    sql += " and x.ngayct=cast('" + DenNgay.ToString() + "' as datetime)";
                    if (_condition != "")
                        sql += " and (" + _condition.Replace("MaVT in","x.MaVT in") + ")";

                    if (mt == "MT24" || mt == "MT33" || mt == "MT42")
                    {
                        sql += " and a.NhapTb=1";
                    }

                //}
                //else
                //{
                //    sql = "update " + dt + " set " + Giafield + " = dongia from " + dt + " y, banggiatb x  ";
                //    sql += " where  y." + mavtField + "=x.mavt and y." + makhoField + "=x.makho and x.makho='" + makholaygia + "'";
                //    sql += " and y." + mt.Trim() + "id in  (select " + mt.Trim() + "id from " + mt + " where ";
                //    sql += mt.Trim() + "." + ngayctFiedl + " between cast('" + TuNgay.ToString() + "' as datetime) and cast('" + DenNgay.ToString() + "' as datetime)";
                //    sql += " and x.ngayct=cast('" + DenNgay.ToString() + "' as datetime)";
                //    if (_condition != "")
                //        sql += " and (" + _condition.Replace("MaVT in", "x.MaVT in") + ")";

                //    if (mt == "MT24" || mt == "MT33" || mt == "MT42")
                //    {
                //        sql += " and " + mt.Trim() + ".NhapTb=1";

                //    }
                //    sql += ")";
                //}
            //}
            _dbData.UpdateByNonQuery(sql);
            //if (tygiaField != "")
            //{
            //    sql="update " + dt + " set " + GiaNTfield + " = a." + Giafield + "/b." + tygiaField + " from ";
            //    sql += dt + " a," + mt + " b  where a." + mt.Trim() + "id = b." + mt.Trim() + "id and b.MaNT <> 'VND' and ";
            //    sql += "b." + ngayctFiedl + " between cast('" + TuNgay.ToString() + "' as datetime) and cast('" + DenNgay.ToString() + "' as datetime)";
            //    if (_condition != "")
            //        sql += " and (a." + _condition.Replace("MaVT in", mavtField + " in") + ")";
            //    if (mt == "MT33" || mt == "MT42")
            //    {
            //        sql += " and b.NhapTb=1";
            //    }
            //    _dbData.UpdateByNonQuery(sql);
            //}
        }
       
    }
}
