using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CDTDatabase;
using CDTLib;
using DevExpress.XtraEditors;

namespace Inventory
{
     
    class GiaNTXT
    {
        string sqlQueryNXTruocKy = "Select * from ((Select '' as MTIDDT, NgayCT, SoCT, '' as MaCT, Makho, MaVT, SoLuong, 0.0 as SoLuong_x, DuDau/SoLuong as DonGia, DuDau as PsNo, null as Psco "
            + " from obntxt where mavt in(select mavt	from dmvt where Tonkho = 2)) union all"
            + " (Select '' as MTIDDT, NgayCT, SoCT, MaCT, Makho, MaVT, SoLuong, SoLuong_x, DonGia, PsNo, PsCo"
            + " from blvt where mavt in(select mavt	from dmvt where Tonkho = 2))) as z where Ngayct < ";

        string sqlQueryNXTrongKy = "Select * from ((Select null as MTIDDT, NgayCT, SoCT, '' as MaCT, makho, mavt, Soluong, 0.0 as SoLuong_x, DuDau/SoLuong as DonGia, DuDau as PsNo, null as PsCo "
            + " from obntxt where mavt in(select mavt	from dmvt where Tonkho = 2)) union all"
            + " (Select MTIDDT, NgayCT, SoCT, MaCT, Makho, MaVT, SoLuong, SoLuong_x, DonGia, PsNo, PsCo"
            + " from blvt where mavt in(select mavt	from dmvt where Tonkho = 2))) as z where Ngayct between ";

        string sqlQuerySoDuDKVT = "";

        string sqlQuerySoDuVatTuTrongKho = "Select case when Sum(SoLuong) - Sum(SoLuong_x) is not null then Sum(Soluong) - Sum(SoLuong_x) else 0.0 end" +
            " from ((Select null as MTIDDT, NgayCT, MaVT, MaKho, SoLuong, 0.0 SoLuong_x from obntxt) " +
            " union all (Select MTIDDT, NgayCT, MaVT, MaKho, SoLuong, SoLuong_x from blvt))x where ";


        private string _condition;
        int Nam = int.Parse(Config.GetValue("NamLamViec").ToString());
        private Database _dbData;
        private Database _dbStruct = Database.NewStructDatabase();
        private DataTable _dtVatTu;
        private int _tuThang;
        private int _denThang;
        private DateTime TuNgay;
        private DateTime DenNgay;
        public DataTable DtVatTu
        {
            get { return _dtVatTu; }
            set { _dtVatTu = value; }
        }

        public GiaNTXT(Database dbData, int tuThang, int denThang, string condition)
        {
            _tuThang = tuThang; 
            _denThang = denThang;
            _condition = condition;
            _dbData = dbData;
        }

        public void TinhGia()
        {
            string str = _tuThang.ToString() + "/" + "01" + "/" + Nam.ToString();
            TuNgay = Convert.ToDateTime(str);

            str = (_denThang).ToString() + "/" + "01" + "/" + Nam.ToString();
            DenNgay = Convert.ToDateTime(str).AddMonths(1).AddDays(-1);

            sqlQueryNXTruocKy = sqlQueryNXTruocKy + "'" + TuNgay.ToShortDateString() + "'";
            if (_condition != "")
                sqlQueryNXTruocKy += " and " + _condition;
            DataTable dtNXTruocKy = _dbData.GetDataTable(sqlQueryNXTruocKy);


            sqlQuerySoDuDKVT = "Select MaVT, MaKho, SoLuong = Sum(soluong) - Sum(Soluong_x) from (" +
                sqlQueryNXTruocKy + ")x group by MaVT, MaKho having Sum(SoLuong) - Sum(SoLuong_x) <> 0";
            DataTable dtSoDuDKVT = _dbData.GetDataTable(sqlQuerySoDuDKVT);

            DataTable dtChiTietSDDKVT = dtNXTruocKy.Clone();
            
            dtNXTruocKy.DefaultView.Sort = "NgayCT, SoCT";
            for (int i = 0; i < dtSoDuDKVT.Rows.Count; i++)
            {
                double slTon = double.Parse(dtSoDuDKVT.Rows[i]["SoLuong"].ToString());
                for (int j = dtNXTruocKy.DefaultView.Count - 1; j >= 0; j--)
                {
                    if (slTon > 0)
                    {
                        if (dtNXTruocKy.DefaultView[j]["MaVT"].ToString().ToUpper() == dtSoDuDKVT.Rows[i]["MaVT"].ToString().ToUpper() &&
                            dtNXTruocKy.DefaultView[j]["MaKho"].ToString().ToUpper() == dtSoDuDKVT.Rows[i]["MaKho"].ToString().ToUpper() &&
                            double.Parse(dtNXTruocKy.DefaultView[j]["SoLuong"].ToString()) > 0)
                        {
                            slTon = slTon - double.Parse(dtNXTruocKy.DefaultView[j]["SoLuong"].ToString());
                            DataRow drTemp = dtChiTietSDDKVT.NewRow();
                            drTemp.ItemArray = (object[])dtNXTruocKy.DefaultView[j].Row.ItemArray.Clone();
                            if (slTon <= 0)
                            {
                                double slt = slTon + double.Parse(dtNXTruocKy.DefaultView[j]["SoLuong"].ToString());
                                drTemp["SoLuong"] = slt;
                                drTemp["PsNo"] = slt * double.Parse(dtNXTruocKy.DefaultView[j]["DonGia"].ToString());
                                drTemp["DonGia"] = double.Parse(dtNXTruocKy.DefaultView[j]["DonGia"].ToString());
                                dtChiTietSDDKVT.Rows.Add(drTemp);
                                break;
                            }
                            dtChiTietSDDKVT.Rows.Add(drTemp);
                        }
                    }
                }
            }
            dtChiTietSDDKVT.DefaultView.Sort = "NgayCT, SoCT";

            sqlQueryNXTrongKy = sqlQueryNXTrongKy + "'" + TuNgay.ToShortDateString() + "'" + " and '" + DenNgay.ToShortDateString() +"' and MaVT in (Select MaVT from dmvt where TonKho = 2)";
            if (_condition != "")
                sqlQueryNXTrongKy += " and " + _condition;
            DataTable dtNXTrongKy = _dbData.GetDataTable(sqlQueryNXTrongKy);
            dtNXTrongKy.DefaultView.Sort = "NgayCT, SoCT";

            for (int i = 0; i < dtNXTrongKy.DefaultView.Count; i++)
            {
                if (double.Parse(dtNXTrongKy.DefaultView[i]["SoLuong"].ToString()) > 0)
                {
                    DataRow drTemp = dtChiTietSDDKVT.NewRow();
                    drTemp.ItemArray = (object[])dtNXTrongKy.DefaultView[i].Row.ItemArray.Clone();
                    dtChiTietSDDKVT.Rows.Add(drTemp);
                    dtNXTrongKy.DefaultView[i].Delete();
                    i--;
                }
                else
                {
                    double slx = double.Parse(dtNXTrongKy.DefaultView[i]["SoLuong_x"].ToString());
                    string mtiddt = dtNXTrongKy.DefaultView[i]["MTIDDT"].ToString();
                    string mavt = dtNXTrongKy.DefaultView[i]["MaVT"].ToString();
                    DateTime ngayct = Convert.ToDateTime(dtNXTrongKy.DefaultView[i]["NgayCT"].ToString());
                    string makho = dtNXTrongKy.DefaultView[i]["MaKho"].ToString();

                    string sqlTemp = "";
                    sqlTemp = sqlQuerySoDuVatTuTrongKho + " MaVT = " + "'" + mavt + "' and makho = '" + makho + "' and NgayCT <= '" + ngayct.ToShortDateString() + "'"
                        + " and (MTIDDT is null or MTIDDT <> '" + mtiddt + "')";
                    if (_condition != "")
                        sqlTemp += " and " + _condition;
                    object vattuTonKhoT = _dbData.GetValue(sqlTemp);

                    if (slx > double.Parse(vattuTonKhoT.ToString()))
                    {
                        string msg = "Lỗi xuất hàng vượt số lượng tồn kho! Mã vật tư :";
                        if (Config.GetValue("Language").ToString() == "1")
                            msg = UIDictionary.Translate(msg);
                        XtraMessageBox.Show(msg + dtNXTrongKy.DefaultView[i]["MaVT"].ToString() );
                        continue;
                    }
                    if (slx > 0)
                    {
                        double slxt = slx;
                        double PsCo = 0;
                        for (int j = 0; j < dtChiTietSDDKVT.DefaultView.Count; j++)
                        {
                            if (dtChiTietSDDKVT.DefaultView[j]["MaVT"].ToString().ToUpper() == dtNXTrongKy.DefaultView[i]["MaVT"].ToString().ToUpper() &&
                                dtChiTietSDDKVT.DefaultView[j]["MaKho"].ToString().ToUpper() == dtNXTrongKy.DefaultView[i]["MaKho"].ToString().ToUpper())
                            {
                                double sl = double.Parse(dtChiTietSDDKVT.DefaultView[j]["SoLuong"].ToString());
                                if (sl > 0.0)
                                {
                                    if (sl - slx >= 0)
                                    {
                                        PsCo = PsCo + slx * double.Parse(dtChiTietSDDKVT.DefaultView[j]["DonGia"].ToString());
                                        dtNXTrongKy.DefaultView[i]["DonGia"] = PsCo / slxt;
                                        dtNXTrongKy.DefaultView[i]["PsCo"] = PsCo;
                                        if (sl - slx == 0)
                                        {
                                            dtChiTietSDDKVT.DefaultView[j].Delete();
                                            j = j - 1;
                                            break;
                                        }
                                        else
                                            dtChiTietSDDKVT.DefaultView[j]["SoLuong"] = sl - slx;
                                        break;
                                    }
                                    else
                                    {
                                        PsCo = PsCo + sl * double.Parse(dtChiTietSDDKVT.DefaultView[j]["DonGia"].ToString());
                                        slx = slx - sl;
                                        dtChiTietSDDKVT.DefaultView[j].Delete();
                                        j = j - 1;
                                    }
                                }
                            } 

                        }
                    }
                }
            }
            

            _dtVatTu = dtNXTrongKy.Copy();
            this.Apgia();
        }
        public void Apgia()
        {
            string blvtid;

            string sql = "select systableid from systable where TableName='BLVT' and syspackageid=" + Config.GetValue("sysPackageID").ToString();
            blvtid = _dbStruct.GetValue(sql).ToString();
            sql = "select sysfieldid from sysfield where systableid=" + blvtid + " and FieldName='Dongia'";
            string dongiaID = _dbStruct.GetValue(sql).ToString();
            sql = "select sysfieldid from sysfield where systableid=" + blvtid + " and FieldName='Soluong_x'";
            string Soluong_xID = _dbStruct.GetValue(sql).ToString();
            sql = "select sysfieldid from sysfield where systableid=" + blvtid + " and FieldName='Soluong'";
            string Soluong_nID = _dbStruct.GetValue(sql).ToString();
            sql = "select b.mttableid,b.dttableid,a.*, c.Pk from sysdataconfigdt a,sysdataconfig b,systable c ";
            sql += "where b.blconfigid=a.blconfigid  ";
            sql += "and b.systableid=" + blvtid + "and c.systableid=" + blvtid + " AND blfieldid=" + dongiaID;
            sql += " AND (a.blconfigid in(select blconfigid from sysdataconfigdt where   blfieldid=" + Soluong_xID + "))";
            DataTable dsMtdt = _dbStruct.GetDataTable(sql);
            string mt;
            string dt;
            string GiaField = dongiaID;//
            string tygiaField = _dbStruct.GetValue("select sysfieldid from sysfield where sysTableid=" + blvtid + " and FieldName='TyGia'").ToString();//trong blvt là 1795 - rồi tìm trong bảng nguồn tương ứng
            string GiaNTField = _dbStruct.GetValue("select sysfieldid from sysfield where sysTableid=" + blvtid + " and FieldName='DonGiaNT'").ToString();//trong blvt là 1792 - 
            string mavtField = _dbStruct.GetValue("select sysfieldid from sysfield where sysTableid=" + blvtid + " and FieldName='MaVT'").ToString();//trong blvt là 1796 - rồi tìm trong bảng nguồn tương ứng
            string makhoField = _dbStruct.GetValue("select sysfieldid from sysfield where sysTableid=" + blvtid + " and FieldName='MaKho'").ToString();//trong blvt là 1797 - rồi tìm trong bảng nguồn tương ứng
            string ngayctField = _dbStruct.GetValue("select sysfieldid from sysfield where sysTableid=" + blvtid + " and FieldName='NgayCt'").ToString();//trong blvt là 1782 - rồi tìm trong bảng nguồn tương ứng
            string mtiddtField = _dbStruct.GetValue("select sysfieldid from sysfield where sysTableid=" + blvtid + " and FieldName='MTIDDT'").ToString();//trong blvt là 1782 - rồi tìm trong bảng nguồn tương ứng
            UpdateFormula formula;
            PostBl post; 
            try
            {
                foreach (DataRow dr in dsMtdt.Rows)//duyệt qua từ loại chứng từ
                {
                    string MaCT = _dbStruct.GetValue("select MaCT from systable where systableid=" + dr["dttableid"].ToString()).ToString();
                    mt = _dbStruct.GetValue("select tableName from systable where systableid=" + dr["mttableid"].ToString()).ToString();
                    dt = _dbStruct.GetValue("select tableName from systable where systableid=" + dr["dttableid"].ToString()).ToString();
                    string GiaFieldName;
                    string tygiaFieldName = "";
                    string GiaNTFieldName;
                    string mavtFieldName;
                    string ngayctFieldName;
                    if (!(dr["dtfieldid"] is DBNull) || !(dr["formula"] is DBNull))
                    {//Lấy thông tin các trường cần dùng
                        string MTIDDT = _dbStruct.GetValue("  select dtfieldid from sysdataconfigdt where blfieldid=" + mtiddtField + "  and blconfigid=" + dr["blconfigid"].ToString()).ToString();
                        MTIDDT = _dbStruct.GetValue("  select fieldName from sysfield where sysfieldid=" + MTIDDT).ToString();
                        //GiaFieldName = _dbStruct.GetValue("  select dtfieldid from sysdataconfigdt where blfieldid=" + GiaField + "  and blconfigid=" + dr["blconfigid"].ToString()).ToString();
                        //GiaFieldName = _dbStruct.GetValue("  select fieldName from sysfield where sysfieldid=" + GiaFieldName).ToString();
                        GiaFieldName = "DGQuyDoi";
                        mavtFieldName = _dbStruct.GetValue("  select dtfieldid from sysdataconfigdt where blfieldid=" + mavtField + "  and blconfigid=" + dr["blconfigid"].ToString()).ToString();
                        mavtFieldName = _dbStruct.GetValue("  select fieldName from sysfield where sysfieldid=" + mavtFieldName).ToString();
                        ngayctFieldName = _dbStruct.GetValue("  select mtfieldid from sysdataconfigdt where blfieldid=" + ngayctField + " and blconfigid=" + dr["blconfigid"].ToString()).ToString();
                        ngayctFieldName = _dbStruct.GetValue("  select fieldName from sysfield where sysfieldid=" + ngayctFieldName).ToString();
                        //GiaNTFieldName = _dbStruct.GetValue("  select dtfieldid from sysdataconfigdt where blfieldid=" + GiaNTField + "  and blconfigid=" + dr["blconfigid"].ToString()).ToString();
                        //GiaNTFieldName = _dbStruct.GetValue("  select fieldName from sysfield where sysfieldid=" + GiaNTFieldName).ToString();

                        //tygiaFieldName = _dbStruct.GetValue("  select mtfieldid from sysdataconfigdt where blfieldid=" + tygiaField + "  and blconfigid=" + dr["blconfigid"].ToString()).ToString();
                        //if (tygiaFieldName != "")
                        //{
                        //    tygiaFieldName = _dbStruct.GetValue("  select fieldName from sysfield where sysfieldid=" + tygiaFieldName).ToString();
                        //}
                        foreach (DataRow rGia in _dtVatTu.Select("MaCT='" + MaCT + "'"))//duyệt qua bảng giá tương ứng với bảng dữ liệu
                        {
                            updateMTDT(mt, dt, GiaFieldName, rGia, MTIDDT);
                        } 
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
                XtraMessageBox.Show(ex.Message);
                _dbData.RollbackMultiTrans();
            }
        }
        public void updateMTDT(string mt, string dt, string Giafield, DataRow dr, string IDField)
        {
            string sql;
            sql = "update " + dt + " set " + Giafield + " = " + dr["DonGia"].ToString().Replace(',', '.');
            sql += " where  " + IDField + "=convert(uniqueidentifier,'" + dr["MTIDDT"].ToString()+ "')";
            _dbData.UpdateByNonQuery(sql);
            //if (tygiaField != "")
            //{
            //    sql = "update " + dt + " set " + GiaNTfield + " = a." + Giafield + "/b." + tygiaField + " from ";
            //    sql += dt + " a," + mt + " b  where a." + mt.Trim() + "id = b." + mt.Trim() + "id and b.MaNT <> 'VND' and ";
            //    sql += IDField  + "=convert(uniqueidentifier,'" + dr["MTIDDT"].ToString() + "')";
            //    _dbData.UpdateByNonQuery(sql);
            //}
        }
    }
}
