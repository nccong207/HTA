using System;
using System.Collections.Generic;
using System.Text;
using CDTDatabase;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTLib;
namespace Fa
{
    public class FaKHTSCD
    {
        string _mact = "Z40"; 
        private DateTime _ngayCt1;
        private DateTime _ngayCt2;
        public Database _dbData = Database.NewDataDatabase();
        public DataTable TongTien;
        public FaKHTSCD(int i, string namlv)//i tháng cần tính
        {
            string str = i.ToString() +"/01/"  +  namlv;
            _ngayCt1 = DateTime.Parse(str);
            _ngayCt2 = _ngayCt1.AddMonths(1).AddDays(-1);
            TongTien= getData();
        }
        private DataTable getData()
        {
            string sql;
            DataTable dt45 = new DataTable("Fa");
            sql = "GetFaData";
            string[] paranames = new string[] { "@ngayCt"};
            object[] paraValues = new object[] { _ngayCt2};

            try
            {
                dt45 = _dbData.GetDataSetByStore(sql, paranames, paraValues);
            }
            catch
            {
                return null;
            }
            return dt45;


        }
        public void calculate()
        {
                        
            _dbData.BeginMultiTrans();
            deleteBt();
            foreach (DataRow dr in TongTien.Rows)
            {
                if (!createBt(dr))
                {
                    return;
                }
            }
            if (!_dbData.HasErrors)
            {
                _dbData.EndMultiTrans();
                string msg = "Đã tạo bút toán khấu hao!";
                if (Config.GetValue("Language").ToString() == "1")
                    msg = UIDictionary.Translate(msg);
                XtraMessageBox.Show(msg);
            }
        }

        public bool createBt(DataRow dr)
        {
            try
            {
                string tableName = "bltk";
                List<string> fieldName = new List<string>();
                List<string> Values = new List<string>();
                Guid id = new Guid();
                Guid iddt = new Guid();
                fieldName.Add("MTID");
                fieldName.Add("MTIDDT");
                fieldName.Add("Nhomdk");//KHTS
                fieldName.Add("MaCT");//DMTS
                fieldName.Add("SoCT");//MaTS
                fieldName.Add("NgayCT");
                fieldName.Add("Ongba");//' '
                fieldName.Add("DienGiai");//Khấu hao tài sản cố định tháng//
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
                Values.Add("'KHTS'");
                Values.Add( "'" + _mact + "'");
                Values.Add("'" + dr["mats"].ToString() + "'");
                Values.Add("cast('" + _ngayCt2.ToString() + "' as datetime)");
                //Values.Add("''");
                Values.Add("''");
                Values.Add("N'Khấu hao tài sản cố định tháng : " + _ngayCt2.Month.ToString() + "/" +  _ngayCt2.Year.ToString() + "'");
                Values.Add("'" + dr["Tkcp"].ToString() + "'");
                Values.Add("'" + dr["Tkkh"].ToString() + "'");
                Values.Add(dr["tienpb"].ToString().Replace(",","."));
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
                Values.Add("'KHTS'");
                Values.Add("'" + _mact + "'");
                Values.Add("'" + dr["mats"].ToString() + "'");
                Values.Add("cast('" + _ngayCt2.ToString() + "' as datetime)");
                //Values.Add("''");
                Values.Add("''");
                Values.Add("N'Khấu hao tài sản cố định tháng : " + _ngayCt2.Month.ToString() + "/" + _ngayCt2.Year.ToString() + "'");
                Values.Add("'" + dr["TkKH"].ToString() + "'");
                Values.Add("'" + dr["Tkcp"].ToString() + "'");
                Values.Add("0");
                Values.Add(dr["tienpb"].ToString().Replace(",", "."));
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
        public bool deleteBt()
        {
            string sql = "delete bltk where nhomdk='KHTS' and NgayCt=cast('" + _ngayCt2.ToString() + "' as datetime)";
            _dbData.UpdateByNonQuery(sql);
            return true;
            
        }
    }
}
