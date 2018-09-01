using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Plugins;
using CDTDatabase;
using CDTLib;

namespace NhapBangLuong
{
    public partial class frmShow : DevExpress.XtraEditors.XtraForm
    {
        public frmShow()
        {
            InitializeComponent();
        }
        public int thang;
        public int ngaycong;
        private void frmShow_Load(object sender, EventArgs e)
        {
            if (Config.GetValue("KyKeToan") != null)
                spinThang.EditValue = Config.GetValue("KyKeToan");            
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            thang = Convert.ToInt16(spinThang.EditValue);
            ngaycong =Convert.ToInt16(spinNgayCong.EditValue);
            this.Close();
        }
                 
    }
}