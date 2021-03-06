using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CDTDatabase;
using DevExpress.XtraEditors;
using CDTLib;

namespace Inventory
{
    class ChenhLechTonKho
    {
        string _mact = "Z20";
        private Database _dbData;
        //private string _condition = "";
        private DateTime _ngayCT;
        private string _tkCL = "";

        public ChenhLechTonKho(Database dbData, string condition, string tkCL, int denThang)
        {
            _ngayCT = DateTime.Parse(denThang.ToString() + "/01/" + Config.GetValue("NamLamViec").ToString()).AddMonths(1).AddDays(-1);
            //_condition = condition;
            _tkCL = tkCL;
            _dbData = dbData;
        }

        private DataTable LayCLChoSoKho()
        {
            string sql = "select * from"
                + " (select makho, mavt, sum(psno - psco) as giatri, sum(soluong - soluong_x) as soluong, maphi, mavv, mabp, macongtrinh"
                + " from (select makho, mavt, psno, psco, soluong, soluong_x, maphi, mavv, mabp, macongtrinh from blvt "
                + " where NgayCT <= cast('" + _ngayCT.ToString() + "' as datetime)"
                + " union all select makho, mavt, dudau, 0.0, soluong, 0.0, null, null, null, null from obvt"
                + " union all select makho, mavt, dudau, 0.0, soluong, 0.0, null, null, null, null from obntxt) x"
                //+ " where " + _condition 
                + " group by makho, mavt, maphi, mavv, mabp, macongtrinh) a"
                + " where giatri <> 0 and soluong = 0";
            return (_dbData.GetDataTable(sql));
        }

        private DataTable LayCLChoSoCai()
        {
            string sql = "select tkkho,maphi,mavv,mabp,macongtrinh,sum(giatri) as giatri from"
                + " (select a.*, b.tkkho from"
                + " (select makho, mavt, sum(psno - psco) as giatri, sum(soluong - soluong_x) as soluong, maphi, mavv, mabp, macongtrinh"
                + " from (select makho, mavt, psno, psco, soluong, soluong_x, maphi, mavv, mabp, macongtrinh from blvt "
                + " where NgayCT <= cast('" + _ngayCT.ToString() + "' as datetime)"
                + " union all select makho, mavt, dudau, 0.0, soluong, 0.0, null, null, null, null from obvt"
                + " union all select makho, mavt, dudau, 0.0, soluong, 0.0, null, null, null, null from obntxt) x"
                //+ " where " + _condition 
                + " group by makho, mavt, maphi, mavv, mabp, macongtrinh) a, dmvt b"
                + " where giatri <> 0 and soluong = 0 and a.mavt = b.mavt) c"
                + " group by tkkho,maphi,mavv,mabp,macongtrinh";
            return (_dbData.GetDataTable(sql));
        }

        public void XuLy()
        {
            DataTable dtData1 = LayCLChoSoKho();
            DataTable dtData2 = LayCLChoSoCai();
            if (dtData1.Rows.Count == 0)
                return;
            foreach (DataRow dr in dtData1.Rows)
            {
                VaoSoKho(dr);
            }
            foreach (DataRow dr in dtData2.Rows)
            {
                VaoSoCai(dr);
            }
        }

        private bool VaoSoKho(DataRow dr)
        {
            string soct = "'CLTK" + _ngayCT.Month.ToString("00") + "/" + dr.Table.Rows.IndexOf(dr).ToString() + "'";
            try
            {
                string tableName = "blvt";
                List<string> fieldName = new List<string>();
                List<string> Values = new List<string>();
                Guid id = new Guid();
                Guid iddt = new Guid();
                fieldName.Add("MTID");
                fieldName.Add("MTIDDT");
                fieldName.Add("Nhomdk");
                fieldName.Add("MaCT");
                fieldName.Add("SoCT");
                fieldName.Add("NgayCT");
                fieldName.Add("DienGiai");
                fieldName.Add("MaVT");
                fieldName.Add("MaKho");
                fieldName.Add("Psno");
                fieldName.Add("Psco");
                if (!(dr["MaPhi"] is DBNull))
                {
                    fieldName.Add("Maphi");
                }
                if (!(dr["MaVV"] is DBNull))
                {
                    fieldName.Add("MaVV");
                }
                if (!(dr["MaBP"] is DBNull))
                {
                    fieldName.Add("MaBP");
                }
                if (!(dr["MaCongtrinh"] is DBNull))
                {
                    fieldName.Add("MaCongtrinh");
                }
                Values.Add("convert( uniqueidentifier,'" + id.ToString() + "')");
                Values.Add("convert( uniqueidentifier,'" + iddt.ToString() + "')");
                Values.Add("'" + _mact + "'");
                Values.Add("'" + _mact + "'");
                Values.Add(soct);
                Values.Add("cast('" + _ngayCT.ToString() + "' as datetime)");
                Values.Add("N'Chênh lệch tồn kho tháng " + _ngayCT.Month.ToString() + "/" + _ngayCT.Year.ToString() + "'");
                Values.Add("'" + dr["MaVT"].ToString() + "'");
                Values.Add("'" + dr["MaKho"].ToString() + "'");
                decimal giaTri = Decimal.Parse(dr["GiaTri"].ToString());
                if (giaTri < 0)
                {
                    Values.Add((-giaTri).ToString().Replace(",", "."));
                    Values.Add("0");
                }
                else
                {
                    Values.Add("0");
                    Values.Add(giaTri.ToString().Replace(",", "."));
                }
                if (!(dr["MaPhi"] is DBNull))
                {
                    Values.Add("'" + dr["MaPhi"].ToString() + "'");
                }
                if (!(dr["MaVV"] is DBNull))
                {
                    Values.Add("'" + dr["MaVV"].ToString() + "'");
                }
                if (!(dr["MaBP"] is DBNull))
                {
                    Values.Add("'" + dr["MaBP"].ToString() + "'");
                }
                if (!(dr["MaCongtrinh"] is DBNull))
                {
                    Values.Add("'" + dr["MaCongtrinh"].ToString() + "'");
                }

                if (!_dbData.insertRow(tableName, fieldName, Values))
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.Message);
                return false;

            }
            return true;
        }

        private bool VaoSoCai(DataRow dr)
        {
            string soct = "'CLTK" + _ngayCT.Month.ToString("00") + "/" + dr.Table.Rows.IndexOf(dr).ToString() + "'";
            try
            {
                string tableName = "bltk";
                List<string> fieldName = new List<string>();
                List<string> Values = new List<string>();
                Guid id = new Guid();
                Guid iddt = new Guid();
                fieldName.Add("MTID");
                fieldName.Add("MTIDDT");
                fieldName.Add("Nhomdk");
                fieldName.Add("MaCT");
                fieldName.Add("SoCT");
                fieldName.Add("NgayCT");
                fieldName.Add("DienGiai");
                fieldName.Add("TK");
                fieldName.Add("TKdu");
                fieldName.Add("Psno");
                fieldName.Add("Psco");
                if (!(dr["MaPhi"] is DBNull))
                {
                    fieldName.Add("Maphi");
                }
                if (!(dr["MaVV"] is DBNull))
                {
                    fieldName.Add("MaVV");
                }
                if (!(dr["MaBP"] is DBNull))
                {
                    fieldName.Add("MaBP");
                }
                if (!(dr["MaCongtrinh"] is DBNull))
                {
                    fieldName.Add("MaCongtrinh");
                }
                Values.Add("convert( uniqueidentifier,'" + id.ToString() + "')");
                Values.Add("convert( uniqueidentifier,'" + iddt.ToString() + "')");
                Values.Add("'" + _mact + "'");
                Values.Add("'" + _mact + "'");
                Values.Add(soct);
                Values.Add("cast('" + _ngayCT.ToString() + "' as datetime)");
                Values.Add("N'Chênh lệch tồn kho tháng " + _ngayCT.Month.ToString() + "/" + _ngayCT.Year.ToString() + "'");
                decimal giaTri = Decimal.Parse(dr["GiaTri"].ToString());
                decimal tmp = giaTri;
                if (giaTri > 0)
                {
                    Values.Add("'" + _tkCL + "'");
                    Values.Add("'" + dr["TkKho"].ToString() + "'");
                }
                else
                {
                    Values.Add("'" + dr["TkKho"].ToString() + "'");
                    Values.Add("'" + _tkCL + "'");
                    tmp = -giaTri;
                }
                Values.Add(tmp.ToString().Replace(",", "."));
                Values.Add("0");

                if (!(dr["MaPhi"] is DBNull))
                {
                    Values.Add("'" + dr["MaPhi"].ToString() + "'");
                }
                if (!(dr["MaVV"] is DBNull))
                {
                    Values.Add("'" + dr["MaVV"].ToString() + "'");
                }
                if (!(dr["MaBP"] is DBNull))
                {
                    Values.Add("'" + dr["MaBP"].ToString() + "'");
                }
                if (!(dr["MaCongtrinh"] is DBNull))
                {
                    Values.Add("'" + dr["MaCongtrinh"].ToString() + "'");
                }

                if (!_dbData.insertRow(tableName, fieldName, Values))
                {
                    return false;
                }
                Values.RemoveRange(0, Values.Count);
                Values.Add("convert( uniqueidentifier,'" + id.ToString() + "')");
                Values.Add("convert( uniqueidentifier,'" + iddt.ToString() + "')");
                Values.Add("'" + _mact + "'");
                Values.Add("'" + _mact + "'");
                Values.Add(soct);
                Values.Add("cast('" + _ngayCT.ToString() + "' as datetime)");
                Values.Add("N'Chênh lệch tồn kho tháng " + _ngayCT.Month.ToString() + "/" + _ngayCT.Year.ToString() + "'");
                if (giaTri > 0)
                {
                    Values.Add("'" + dr["TkKho"].ToString() + "'");
                    Values.Add("'" + _tkCL + "'");
                }
                else
                {
                    Values.Add("'" + _tkCL + "'");
                    Values.Add("'" + dr["TkKho"].ToString() + "'");
                    //giaTri = -giaTri;
                }
                Values.Add("0");
                Values.Add(tmp.ToString().Replace(",", "."));
                if (!(dr["MaPhi"] is DBNull))
                {
                    Values.Add("'" + dr["MaPhi"].ToString() + "'");
                }
                if (!(dr["MaVV"] is DBNull))
                {
                    Values.Add("'" + dr["MaVV"].ToString() + "'");
                }
                if (!(dr["MaBP"] is DBNull))
                {
                    Values.Add("'" + dr["MaBP"].ToString() + "'");
                }
                if (!(dr["MaCongtrinh"] is DBNull))
                {
                    Values.Add("'" + dr["MaCongtrinh"].ToString() + "'");
                }
                if (!_dbData.insertRow(tableName, fieldName, Values))
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.Message);
                return false;

            }
            return true;
        }

        public bool XoaButToan()
        {
            string sql = "delete bltk where mact='CLTK' and NgayCt=cast('" + _ngayCT.ToString() + "' as datetime)";
            if (!_dbData.UpdateByNonQuery(sql))
                return false;
            sql = "delete blvt where mact='CLTK' and NgayCt=cast('" + _ngayCT.ToString() + "' as datetime)";
            if (!_dbData.UpdateByNonQuery(sql))
                return false;
            //kieu moi
            sql = "delete bltk where mact='" + _mact + "' and NgayCt=cast('" + _ngayCT.ToString() + "' as datetime)";
            if (!_dbData.UpdateByNonQuery(sql))
                return false;
            sql = "delete blvt where mact='" + _mact + "' and NgayCt=cast('" + _ngayCT.ToString() + "' as datetime)";
            return (_dbData.UpdateByNonQuery(sql));

        }
    }
}
