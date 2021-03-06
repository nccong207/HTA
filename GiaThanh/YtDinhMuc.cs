using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace GiaThanh
{
    class YtDinhMuc :Ytgt
    {
        private DataTable tbDM;
        private DataTable tbNVLXuat;
        private DataTable tbTongNVLDM;  
        public YtDinhMuc(DataRow dr, DataTable TPNhapkho, DateTime tungay, DateTime denngay)
        {
            dryt = dr;
            drTPNhapkho = TPNhapkho;
            _Tungay = tungay;
            _Denngay = denngay;
            dtkq = TPNhapkho.Copy(); 
            dtkq.Columns.RemoveAt(1);
            dtkq.Columns.Add(createCol());
            dtkq.PrimaryKey = new DataColumn[] { dtkq.Columns[0] };
            Name = dr["MaYT"].ToString();

        }
        public override void TinhGiatri()
        {
            //So luong thanh phan phap kho            dtkq
            //Soluong nguyen vat lieu xuat san xuat
            tbNVLXuat = DbData.GetDataSetByStore("GetNVLXuat", new string[] { "@tungay", "@denngay", "@tk" }, new object[] { _Tungay, _Denngay, tk });
            tbNVLXuat.PrimaryKey = new DataColumn[] { tbNVLXuat.Columns["Mavt"] };
            // Số lượng nvl định mức cho từng sản phẩm
            tbDM = DbData.GetDataSetByStore("GetsldmNVL", new string[] { "@tungay", "@denngay", "@manhom", "@BangDM", "@TruongSP", "@Mavt", "@Heso" },
                    new object[] { _Tungay, _Denngay, Manhom, dryt["BangDM"].ToString(), dryt["TruongSP"].ToString(), dryt["Mavt"].ToString(), dryt["Heso"].ToString() });
            DataColumn coltmp = new DataColumn("tien");
            coltmp.DataType = typeof(double);
            coltmp.DefaultValue = 0.0;
            tbDM.Columns.Add(coltmp);
            //try   
            //{
            tbDM.PrimaryKey = new DataColumn[] { tbDM.Columns["MaSP"], tbDM.Columns["mavt"] };
            //Phân bổ tiền vào bảng định mức
            tbTongNVLDM = DbData.GetDataSetByStore("GetTongDMNVL", new string[] { "@tungay", "@denngay", "@manhom", "@BangDM", "@TruongSP", "@Mavt", "@Heso" },
                        new object[] { _Tungay, _Denngay, Manhom, dryt["BangDM"].ToString(), dryt["TruongSP"].ToString(), dryt["Mavt"].ToString(), dryt["Heso"].ToString() });

            tbTongNVLDM.PrimaryKey = new DataColumn[] { tbTongNVLDM.Columns["Mavt"] };
            //Đối với từng loại vật tư - sản phẩm  và tính tiền đã phân bổ
            //foreach (DataRow drxuat in tbNVLXuat.Rows)
            //{
            //    double tongDM = 0;
            //    double slXuat = 0;
            //    double tienXuat = 0;
            //    double tiendaPb = 0;

            //    string mavt = drxuat["mavt"].ToString().Trim();
            //    tienXuat = double.Parse(drxuat["psco"].ToString());
            //    DataRow [] arrDr = tbDM.Select("mavt='" + drxuat["mavt"].ToString().Trim() + "'");
            //    if (tbTongNVLDM.Rows.Contains(mavt))
            //    {
            //        tongDM = double.Parse(tbTongNVLDM.Rows.Find(mavt)["tongNVL"].ToString());
            //    }
            //    double TSLDM = 0;
            //    foreach (DataRow dr in arrDr)
            //    {
            //        double slDM = double.Parse(dr["soluongxDM"].ToString());
            //        tiendaPb = tienXuat * slDM / tongDM;
            //        dr["tien"] = tiendaPb;
            //        Tongtien += tiendaPb;
            //        TSLDM += slDM;
            //    }
                
            //}
            foreach (DataRow dr in tbDM.Rows)
            {
                string mavt = dr["mavt"].ToString().Trim();
                string masp = dr["masp"].ToString();
                double slDM = double.Parse(dr["soluongxDM"].ToString());
                double tongDM = 0;
                double slXuat = 0;
                double tienXuat = 0;
                double tiendaPb = 0;
                if (tbTongNVLDM.Rows.Contains(mavt))
                {
                    tongDM = double.Parse(tbTongNVLDM.Rows.Find(mavt)["tongNVL"].ToString());
                }
                if (tbNVLXuat.Rows.Contains(mavt))
                {
                    DataRow drxuat = tbNVLXuat.Rows.Find(mavt);
                    slXuat = double.Parse(drxuat["soluongx"].ToString());
                    tienXuat = double.Parse(drxuat["psco"].ToString());
                }
                else
                {
                    //tienhh += tienhh;
                }
                if (tongDM > 0 && slXuat > 0)
                {
                    tiendaPb = tienXuat * slDM / tongDM;
                }
                dr["tien"] = tiendaPb;
                Tongtien += tiendaPb;

            }
            //MessageBox.Show(tienhh.ToString());
            //Đưa vào bảng kết quả
            foreach (DataRow dr in dtkq.Rows)
            {
                string masp = dr["masp"].ToString().Trim();
                dr[Name] = getSum(tbDM.Select("Masp='" + masp + "'"), "tien");
            }

            //}
            //catch
            //{ 
            //}

        }
        
    }
}
