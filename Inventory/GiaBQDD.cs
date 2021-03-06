using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CDTDatabase;
using CDTLib;
using DevExpress.XtraEditors;
using System.Globalization;

namespace Inventory
{
    class GiaBQDD
    {
        NumberFormatInfo nfi = new NumberFormatInfo();
        string sqlQueryXTrongKy = "Select MTIDDT, NgayCT, SoCT, MaCT, makho, mavt, SoLuong_x = SoLuong + SoLuong_x, DonGia, PsCo = PsNo + PsCo"
            + " from blvt where (SoLuong_x > 0 or (SoLuong > 0 and KT = 1)) and mavt in (select mavt from dmvt where Tonkho = 4) and Ngayct between ";
        string sqlSoLieuNX = "select b.MTIDDT, b.NgayCT, b.makho, b.mavt, b.soluong, b.psno, b.SoLuong_x, b.Psco from blvt b " +
		    "where b.ngayct between '{0}' and '{1}' " +
		    "union all " +
		    "(	" +
			"   select null, null, makho, mavt, (Sum(Soluong) - Sum(Soluong_x)) as Soluong, (Sum(Psno)-Sum(Psco))as PSno, 0, 0 " +
			"    from " +
			"    ( " +
			"    select makho,mavt,soluong, 0.0 as soluong_x, dudau as psno,0.0 as psco " +
			"    from obvt " +
			"    union all " +
			"    select b.makho, b.mavt, b.soluong, b.soluong_x, b.psno,b.psco " +
			"    from blvt b " +
			"    where b.ngayct < '{0}' " +
            "    )x group by makho, mavt " +
            ")";
        private DataTable _dtNX;
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

        public GiaBQDD(Database dbData, int tuThang, int denThang, string condition)
        {
            _tuThang = tuThang; 
            _denThang = denThang;
            _condition = condition;
            _dbData = dbData;
            nfi.CurrencyDecimalSeparator = ".";
            nfi.CurrencyGroupSeparator = ",";
            Application.CurrentCulture.NumberFormat = nfi;
        }

        private void CapNhatDGX(string MTIDDT, decimal DG)
        {
            DataRow[] drs = _dtNX.Select("MTIDDT = '" + MTIDDT + "'");
            if (drs != null && drs.Length > 0)
            {
                if (decimal.Parse(drs[0]["SoLuong"].ToString()) > 0)
                    drs[0]["PsNo"] = decimal.Parse(drs[0]["SoLuong"].ToString(), nfi) * DG;
                else
                    drs[0]["PsCo"] = decimal.Parse(drs[0]["SoLuong_x"].ToString(), nfi) * DG;
                drs[0]["X"] = true;
            }
        }

        private decimal LayDonGia(string MTIDDT, string DenNgay, string MaKho, string MaVT)
        {
            //DataRowView drv = null;
            decimal t = 0;
            decimal ps = 0, sl = 0;
            try
            {
                string filter = "(NgayCT is null or NgayCT <= #" + DateTime.Parse(DenNgay).ToShortDateString() + "#) " +
                    "and (MTIDDT is null or MTIDDT <> '" + MTIDDT + "') and MaKho = '" + MaKho + "' and MaVT = '" + MaVT + "' " +
                    "and (PsCo = 0 or (PsCo > 0 and X = 1))";
                object ops = _dtNX.Compute("Sum(PS)", filter);
                object osl = _dtNX.Compute("Sum(SL)", filter);
                //DataView dv = new DataView(_dtNX);
                //dv.RowFilter = filter;
                //for (int i = 0; i < dv.Count; i++)
                //{
                //    drv = dv[i];
                //    ps += decimal.Parse(drv["PsNo"].ToString(), nfi) - decimal.Parse(drv["PsCo"].ToString(), nfi);
                //    sl += decimal.Parse(drv["SoLuong"].ToString(), nfi) - decimal.Parse(drv["SoLuong_x"].ToString(), nfi);
                //}
                if (ops != null && osl != null)
                {
                    ps = decimal.Parse(ops.ToString(), nfi);
                    sl = decimal.Parse(osl.ToString(), nfi);
                    if (sl != 0)
                        t = ps / sl;
                }
                return t;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi khi tính đơn giá của '" + MaVT + "'\n" + ex.Message);
                return 0;
            }
        }

        public void TinhGia()
        {
            string str = _tuThang.ToString() + "/" + "01" + "/" + Nam.ToString();
            TuNgay = Convert.ToDateTime(str);

            str = (_denThang).ToString() + "/" + "01" + "/" + Nam.ToString();
            DenNgay = Convert.ToDateTime(str).AddMonths(1).AddDays(-1);

            sqlQueryXTrongKy += "'" + TuNgay.ToShortDateString() + "' and '" + DenNgay.ToShortDateString() + "'";
            if (_condition != "")
                sqlQueryXTrongKy += " and " + _condition;
            sqlQueryXTrongKy += " order by NgayCT, SoCT";
            DataTable dtXuat = _dbData.GetDataTable(sqlQueryXTrongKy);
            _dtNX = _dbData.GetDataTable(String.Format(sqlSoLieuNX, TuNgay.ToShortDateString(), DenNgay.ToShortDateString()));
            _dtNX.Columns.Add("SL", typeof(Decimal), "SoLuong - SoLuong_x");
            _dtNX.Columns.Add("PS", typeof(Decimal), "PsNo - PsCo");
            _dtNX.Columns.Add("X", typeof(Boolean));
            _dtNX.Columns["X"].DefaultValue = false;

            foreach (DataRow dr in dtXuat.Rows)
            {
                decimal dg = LayDonGia(dr["MTIDDT"].ToString(), dr["NgayCT"].ToString(), dr["MaKho"].ToString(), dr["MaVT"].ToString());
                dr["DonGia"] = dg;
                dr["PsCo"] = dg * decimal.Parse(dr["SoLuong_x"].ToString(), nfi);
                CapNhatDGX(dr["MTIDDT"].ToString(), dg);
            }
            _dtVatTu = dtXuat;
            this.Apgia(dtXuat);
        }

        public void Apgia(DataTable dtXuat)
        {
            string blvtid;

            string sql = "select systableid from systable where TableName='BLVT' and syspackageid=" + Config.GetValue("sysPackageID").ToString();
            blvtid = _dbStruct.GetValue(sql).ToString();
            sql = "select sysfieldid from sysfield where systableid=" + blvtid + " and FieldName='Dongia'";
            string dongiaID = _dbStruct.GetValue(sql).ToString();
            sql = "select d.MaCT,b.mttableid,b.dttableid,a.*, c.Pk from sysdataconfigdt a,sysdataconfig b, systable c, systable d ";
            sql += "where b.mttableid = c.systableid and b.blconfigid=a.blconfigid and c.tablename in ('MT24','MT32','MT33','MT42','MT43','MT44','MT45') ";
            sql += "and  b.dttableid = d.systableid ";
            sql += "and b.systableid=" + blvtid + " AND blfieldid=" + dongiaID;
            DataTable dsMtdt = _dbStruct.GetDataTable(sql);
            string mt;
            string dt;
            string tygiaField = _dbStruct.GetValue("select sysfieldid from sysfield where sysTableid=" + blvtid + " and FieldName='TyGia'").ToString();//trong blvt là 1795 - rồi tìm trong bảng nguồn tương ứng
            string mavtField = _dbStruct.GetValue("select sysfieldid from sysfield where sysTableid=" + blvtid + " and FieldName='MaVT'").ToString();//trong blvt là 1796 - rồi tìm trong bảng nguồn tương ứng
            string makhoField = _dbStruct.GetValue("select sysfieldid from sysfield where sysTableid=" + blvtid + " and FieldName='MaKho'").ToString();//trong blvt là 1797 - rồi tìm trong bảng nguồn tương ứng
            string ngayctField = _dbStruct.GetValue("select sysfieldid from sysfield where sysTableid=" + blvtid + " and FieldName='NgayCt'").ToString();//trong blvt là 1782 - rồi tìm trong bảng nguồn tương ứng
            string mtiddtField = _dbStruct.GetValue("select sysfieldid from sysfield where sysTableid=" + blvtid + " and FieldName='MTIDDT'").ToString();//trong blvt là 1782 - rồi tìm trong bảng nguồn tương ứng
            UpdateFormula formula;
            PostBl post;
            try
            {
                foreach (DataRow dr in dsMtdt.Rows)
                {
                    string MaCT = dr["MaCT"].ToString();
                    mt = _dbStruct.GetValue("select tableName from systable where systableid=" + dr["mttableid"].ToString()).ToString();
                    dt = _dbStruct.GetValue("select tableName from systable where systableid=" + dr["dttableid"].ToString()).ToString();
                    string GiaFieldName;
                    string mavtFieldName;
                    string ngayctFieldName;
                    if (!(dr["dtfieldid"] is DBNull) || !(dr["formula"] is DBNull))
                    {//Lấy thông tin các trường cần dùng
                        string MTIDDT = _dbStruct.GetValue("  select dtfieldid from sysdataconfigdt where blfieldid=" + mtiddtField + "  and blconfigid=" + dr["blconfigid"].ToString()).ToString();
                        MTIDDT = _dbStruct.GetValue("  select fieldName from sysfield where sysfieldid=" + MTIDDT).ToString();
                        GiaFieldName = "DGQuyDoi";
                        mavtFieldName = _dbStruct.GetValue("  select dtfieldid from sysdataconfigdt where blfieldid=" + mavtField + "  and blconfigid=" + dr["blconfigid"].ToString()).ToString();
                        mavtFieldName = _dbStruct.GetValue("  select fieldName from sysfield where sysfieldid=" + mavtFieldName).ToString();
                        ngayctFieldName = _dbStruct.GetValue("  select mtfieldid from sysdataconfigdt where blfieldid=" + ngayctField + " and blconfigid=" + dr["blconfigid"].ToString()).ToString();
                        ngayctFieldName = _dbStruct.GetValue("  select fieldName from sysfield where sysfieldid=" + ngayctFieldName).ToString();

                        DataRow[] drs = dtXuat.Select("MaCT='" + MaCT + "'");
                        if (drs.Length == 0)
                            continue; 
                        foreach (DataRow drXuat in drs)
                            updateMTDT(mt, dt, GiaFieldName, drXuat, MTIDDT); 
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
            string sql = "update " + dt + " set " + Giafield + " = " + dr["DonGia"].ToString().Replace(',', '.');
            sql += " where  " + IDField + "=convert(uniqueidentifier,'" + dr["MTIDDT"].ToString()+ "')";
            _dbData.UpdateByNonQuery(sql);
        }
    }
}
