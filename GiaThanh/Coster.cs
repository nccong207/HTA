using System;
using System.Collections.Generic;
using System.Text;
using CDTLib;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
namespace GiaThanh
{
    public class Coster
    {
        private DateTime _Tungay;
        private DateTime _Denngay;
        private DataTable _Gia;
        //private List<NhomGt> lstNhom;
        private NhomGt NhomGia;
        string Manhom = "";
        public Coster(string nhomGT)
        {
            Filter f = new Filter(nhomGT);
            if (f.ShowDialog() == DialogResult.OK)
            {
                _Tungay = f.Tungay;
                _Denngay = f.DenNgay;
                Manhom = f.manhom;
                TinhGia();
            }
        }
        
        private void TinhGia()
        {
            //Lấy các nhóm giá thành
            
            NhomGia =new NhomGt(_Tungay,_Denngay,Manhom);
            //NhomGia.TinhGiaThanh();
            _Gia = NhomGia.BangGia;
            //Preview
            GTPreview GTPre = new GTPreview(_Gia);            
            GTPre.UpdateGia += new EventHandler(this.UpdateGia);
            GTPre.ShowDialog();
        }
        private void UpdateGia(object ob, EventArgs e )
        {
            bool kqUpdate= NhomGia.UpdateGia();
            if (!kqUpdate)
            {
                string msg = "Không cập nhật được giá thành!";
                if (Config.GetValue("Language").ToString() == "1")
                    msg = UIDictionary.Translate(msg);
                XtraMessageBox.Show(msg);
            }
            else
            {
                string msg = "Cập nhật thành công!";
                if (Config.GetValue("Language").ToString() == "1")
                    msg = UIDictionary.Translate(msg);
                XtraMessageBox.Show(msg);
            }

        }

    }
}
