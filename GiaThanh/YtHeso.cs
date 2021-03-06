using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace GiaThanh
{
    class YtHeso :Ytgt
    {
        public YtHeso(DataRow dr, DataTable TPNhapkho, DateTime tungay, DateTime denngay)
        {
            dryt = dr;
            drTPNhapkho = TPNhapkho;
            _Tungay = tungay;
            _Denngay = denngay;
            dtkq = TPNhapkho.Copy();
            dtkq.Columns.Add(createCol());
            Name = dr["MaYT"].ToString();


        }
        public override void TinhGiatri()
        {
            string sql;
            //Lấy Tổng giá trị cần phân bổ
            object[] os = DbData.GetValueByStore("sopsKetChuyen",
                new string[] { "@tk", "@ngayct", "@ngayct1", "@dk", "@psno" },
                new object[] { tk, _Tungay, _Denngay, null, null },
                new SqlDbType[] { SqlDbType.NVarChar, SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.NVarChar, SqlDbType.Float },
                new ParameterDirection[] { ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Output });
            if (os == null || os.Length == 0)
                return;
            Tongtien = Double.Parse(os[0].ToString());
            //Lấy hệ số phân bổ
            sql = "select " + dryt["TruongSP"].ToString() + " as Masp," + dryt["Heso"].ToString() + " as Heso from " + dryt["BangDM"].ToString();
            //sql += " where ngayct between cast('" + _Tungay.ToShortDateString() + "'as datetime) and cast('" + _Denngay.ToShortDateString() + "'as datetime) ";
            sql += " where " + dryt["TruongSP"].ToString() + " in (select mavt from dmvt where nhomgt ='" + this.Manhom + "') and Thang = " + _Tungay.Month.ToString() + " and Nam = " + _Tungay.Year.ToString();
            
            DataTable tbHeso = DbData.GetDataTable(sql);            
            tbHeso.PrimaryKey = new DataColumn[] { tbHeso.Columns["MaSP"]};
            //Lấy tổng hệ số phân bổ
            double TongHeso=0;
            DataColumn col = new DataColumn("Heso", typeof(double));
            col.DefaultValue = 0;
            dtkq.Columns.Add(col);
            foreach (DataRow dr in dtkq.Rows)
            {
                string Masp = dr["MaSP"].ToString();

                DataRow drHeso ;
                if (!tbHeso.Rows.Contains(Masp))
                    continue;
                    drHeso= tbHeso.Rows.Find(Masp);
                    //double heso=double.Parse(dr["soluong"].ToString()) * double.Parse(drHeso["Heso"].ToString());
                    double heso = double.Parse(drHeso["Heso"].ToString());
                    dr["Heso"] = heso;
                    TongHeso += heso;
            }
            //Phân bổ
            if (TongHeso==0) return;
            foreach (DataRow dr in dtkq.Rows)
            {
                dr[Name] = double.Parse(dr["Heso"].ToString()) * Tongtien / TongHeso;
            }
            dtkq.Columns.Remove("Heso");
            dtkq.Columns.Remove("Soluong");
        }
    }
}
