using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTLib;
using CDTDatabase;
using System.Globalization;
namespace GiaThanh
{
    public partial class Filter : DevExpress.XtraEditors.XtraForm
    {
        CDTDatabase.Database dt = Database.NewDataDatabase();
        private int namLv=int.Parse(Config.GetValue("NamLamViec").ToString());
        public DateTime Tungay;
        public DateTime DenNgay;
        public string manhom = "";
        public Filter(string nhomGT)
        {
            InitializeComponent();
            manhom = nhomGT;
        }

        private void Filter_Load(object sender, EventArgs e)
        {
            string sql = "select * from dmnhomgt";
            DataTable tb = dt.GetDataTable(sql);
            gridLookUpEdit1.Properties.DataSource = tb;
            gridLookUpEdit1.Properties.ValueMember = "MaNhom";
            gridLookUpEdit1.Properties.DisplayMember = "TenNhom";
            gridLookUpEdit1.EditValue = manhom;
            gridLookUpEdit1.Properties.ReadOnly = true;
            spinEdit1.EditValue = Config.GetValue("KyKeToan") == null ? DateTime.Today.Month : Int32.Parse(Config.GetValue("KyKeToan").ToString());
            spinEdit2.EditValue = Config.GetValue("KyKeToan") == null ? DateTime.Today.Month : Int32.Parse(Config.GetValue("KyKeToan").ToString());
            if (Config.GetValue("Language").ToString() == "1")
                FormFactory.DevLocalizer.Translate(this);
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
            { return; }
            if (KhoaSo(Int32.Parse(spinEdit2.Value.ToString())))
                return;
            string str = spinEdit1.Value.ToString() + "/01/" + namLv.ToString();
            Tungay = DateTime.Parse(str);
            str = spinEdit2.Value.ToString() + "/01/" + namLv.ToString();
            DenNgay = DateTime.Parse(str);
            DenNgay = DenNgay.AddMonths(1).AddDays(-1);
            if (this.gridLookUpEdit1.EditValue != null) manhom = this.gridLookUpEdit1.EditValue.ToString();
            this.DialogResult = DialogResult.OK;
        }

        private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}