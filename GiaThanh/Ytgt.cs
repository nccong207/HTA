using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CDTDatabase;
using Formula;

namespace GiaThanh
{
    abstract class Ytgt
    {
        protected DateTime _Tungay;
        protected DateTime _Denngay;
        public DataRow dryt;
        protected DataTable dtkq;//Cấu trúc của bảng kết quả là : cột 0: mã sản phẩm, cột 1 : số lượng thành phẩm, Cột 2:Tiền chi phí (cần tính)
        public double Tongtien=0;
        protected DataTable drTPNhapkho;
        //protected Database _dbData = Database.NewCustomDatabase("server = kythuat1; database = CBA12; user = sa; pwd = sa");
        //protected Database _dbStruct = Database.NewCustomDatabase("server = kythuat1; database = CDT; user = sa; pwd = sa");

        private Database _dbData;

        public Database DbData
        {
            get { return _dbData; }
            set { _dbData = value; }
        }
        protected Database _dbStruct = Database.NewStructDatabase();
        public string Manhom = "";
        public string Name;
        public string tk="";
        public bool Giamtru = false;
        public abstract  void TinhGiatri();


        protected double GetTongtien()
        {
            BieuThuc bt = new BieuThuc(tk);
            Hashtable h = new Hashtable();
            foreach (string tk1 in bt.variables)
            {
                double d = 0.0;
                string sql = "select sum(psco) from bltk where (ngayct between '" + _Tungay.ToShortDateString() + "' and '" + _Denngay.ToShortDateString() + "') and tk like '" + tk1 + "' and tkdu like '154%'";
                if (_dbData.GetValue(sql) != DBNull.Value)
                    d = double.Parse(_dbData.GetValue(sql).ToString());
                h.Add(tk1, d);
            }
            return bt.Evaluate(h);
        }
        protected DataColumn createCol()
        {
            DataColumn col =new DataColumn(dryt["MaYT"].ToString());
            col.DataType=typeof(double);
            col.DefaultValue = 0.0;
            col.Caption = dryt["TenYT"].ToString();
            return col;
        }

        public DataTable Kq
        {
            get {return dtkq;}
            set { dtkq = value; }
        }
        protected double getSum(DataTable tb, string colName)
        {
            double tien = 0;
            try
            {
                foreach (DataRow dr in tb.Rows)
                {
                    tien += double.Parse(dr[colName].ToString());
                }
            }
            catch
            {
                return 0;
            }
            return tien;
        }
        protected double getSum(DataRow[] tb, string colName)
        {
            double tien = 0;
            try
            {
                foreach (DataRow dr in tb)
                {
                    tien += double.Parse(dr[colName].ToString());
                }
            }
            catch
            {
                return 0;
            }
            return tien;
        }

    }
}
