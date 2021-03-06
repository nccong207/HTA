using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTLib;

namespace TMBCTC
{
    public partial class FrmFilter : DevExpress.XtraEditors.XtraForm
    {
        private string _file = Application.StartupPath + @"\Reports\" + Config.GetValue("Package").ToString() + @"\TMBCTCtmp";
        public FrmFilter()
        {
            InitializeComponent();
            object o = Config.GetValue("SoQD");
            bool isQD15 = (o == null || o.ToString().StartsWith("15"));
            _file += (isQD15) ? (".xls") : ("48.xls");
            deFromDate.EditValue = DateTime.Parse("01/01/" + Config.GetValue("NamLamViec").ToString());
            deToDate.EditValue = DateTime.Parse("12/31/" + Config.GetValue("NamLamViec").ToString());
            if (Config.GetValue("Language").ToString() == "1")
                FormFactory.DevLocalizer.Translate(this);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void btnSuaMau_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(_file))
                System.Diagnostics.Process.Start(_file);
            else
            {
                string msg = "Không tìm thấy file mẫu!";
                if (Config.GetValue("Language").ToString() == "1")
                    msg = UIDictionary.Translate(msg);
                XtraMessageBox.Show(msg);
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            SaveFileDialog frm = new SaveFileDialog();
            frm.Filter = "Excel file (*.xls)|*.xls";
            frm.AddExtension = true;
            frm.ShowDialog();
            string newFile = frm.FileName;
            if (newFile != string.Empty)
            {
                ExcelReport er = new ExcelReport(_file, newFile, deFromDate.DateTime, deToDate.DateTime);
                er.FillData();
                string msg = "Chưa hoàn thành lập thuyết minh báo cáo tài chính! \nVui lòng kiểm tra lại số liệu trên file đã xuất ra!";
                if (Config.GetValue("Language").ToString() == "1")
                    msg = UIDictionary.Translate(msg);
                if (er.IsError)
                    XtraMessageBox.Show(msg);
                System.Diagnostics.Process.Start(newFile);
                this.Close();
            }
        }
    }
}