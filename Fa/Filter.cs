using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTLib;
using System.Globalization;

namespace Fa
{
    public partial class Filter : DevExpress.XtraEditors.XtraForm
    {
        private string namLv;
        public Filter()
        {
            InitializeComponent();
            namLv = Config.GetValue("NamLamViec").ToString();
            spinEdit1.EditValue = Config.GetValue("KyKeToan") == null ? DateTime.Today.Month : Int32.Parse(Config.GetValue("KyKeToan").ToString());
            spinEdit2.EditValue = Config.GetValue("KyKeToan") == null ? DateTime.Today.Month : Int32.Parse(Config.GetValue("KyKeToan").ToString());
        }

        private bool KhoaSo(int newMonth)
        {
            if (Config.GetValue("NgayKhoaSo") == null)
                return false;
            if (Config.GetValue("NamLamViec") == null)
                return false;
            string tmp = Config.GetValue("NgayKhoaSo").ToString();
            int nam = Int32.Parse(Config.GetValue("NamLamViec").ToString());
            DateTime ngayKhoa;
            DateTimeFormatInfo dtInfo = new DateTimeFormatInfo();
            dtInfo.ShortDatePattern = "dd/MM/yyyy";
            if (DateTime.TryParse(tmp, dtInfo, DateTimeStyles.None, out ngayKhoa))
            {
                if (nam == ngayKhoa.Year && newMonth <= ngayKhoa.Month)
                {
                    string msg = "Kỳ kế toán đã khóa! Không thể chỉnh sửa số liệu!";
                    if (Config.GetValue("Language").ToString() == "1")
                        msg = UIDictionary.Translate(msg);
                    XtraMessageBox.Show(msg);
                    return true;
                }
                else
                    return false;
            }
            return false;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (spinEdit1.Value > spinEdit2.Value)
                return;
            if (KhoaSo(Int32.Parse(spinEdit2.Value.ToString())))
                return;
            FaKHTSCD cal;
            for( int i=int.Parse(spinEdit1.Value.ToString()); i<=spinEdit2.Value; i++)
            {   
                try
                {
                    cal = new FaKHTSCD(i,namLv);
                    fbangKH f=new fbangKH(cal);
                    f.Text = this.Text;
                    f.ShowDialog();
                }
                catch
                {
                }
                
            }
        }


        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FaKHTSCD cal;
            if (KhoaSo(Int32.Parse(spinEdit2.Value.ToString())))
                return;
            for (int i = int.Parse(spinEdit1.Value.ToString()); i <= spinEdit2.Value; i++)
            {
                try
                {
                    cal = new FaKHTSCD(i, namLv);
                    if (cal.deleteBt())
                    {
                        string msg = "Đã xóa bút toán khấu hao!";
                        if (Config.GetValue("Language").ToString() == "1")
                            msg = UIDictionary.Translate(msg);
                        XtraMessageBox.Show(msg);
                    }
                }
                catch
                {
                }

            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
        }

        private void Filter_Load(object sender, EventArgs e)
        {
            if (Config.GetValue("Language").ToString() == "1")
                FormFactory.DevLocalizer.Translate(this);
        }

        private void Filter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

    }
}