using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTDatabase;
using CDTLib;

namespace UpdateThue
{
    public partial class FrmThang : DevExpress.XtraEditors.XtraForm
    {
        Database _dbData;
        public FrmThang(Database dbData)
        {
            InitializeComponent();
            _dbData = dbData;
            thang.Value = DateTime.Today.Month;
            if (Config.GetValue("Language").ToString() == "1")
                FormFactory.DevLocalizer.Translate(this);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string msg = "Đã cập nhật thông tin tờ khai!";
            if (Config.GetValue("Language").ToString() == "1")
                msg = UIDictionary.Translate(msg);
            if (_dbData.UpdateDatabyStore("UpdateToKhai", new string[] { "@Thang" }, new object[] { thang.Value }))
                XtraMessageBox.Show(msg);
            this.DialogResult = DialogResult.OK;
        }
    }
}