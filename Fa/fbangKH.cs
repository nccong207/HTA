using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTLib;

namespace Fa
{
    public partial class fbangKH : DevExpress.XtraEditors.XtraForm
    {
        FaKHTSCD _bangKH ;

        public fbangKH(FaKHTSCD bangKH)
        {
            InitializeComponent();
            _bangKH = bangKH;
        }

        private void fbangPB_Load(object sender, EventArgs e)
        {
            this.gridControl1.DataSource = _bangKH.TongTien.DefaultView;
            if (Config.GetValue("Language").ToString() == "1")
                FormFactory.DevLocalizer.Translate(this);

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //_bangKH.deleteBt();
            _bangKH.calculate();
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}