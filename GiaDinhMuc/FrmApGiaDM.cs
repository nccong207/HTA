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

namespace GiaDinhMuc
{
    public partial class FrmApGiaDM : DevExpress.XtraEditors.XtraForm
    {
        private Database _dbData = Database.NewDataDatabase();
        public FrmApGiaDM()
        {
            InitializeComponent();
            deNgayCT.EditValue = DateTime.Parse(Config.GetValue("KyKeToan").ToString() +
                "/01/" + Config.GetValue("NamLamViec").ToString()).AddMonths(1).AddDays(-1);
            grKho.Properties.DataSource = _dbData.GetDataTable("select * from DMKho");
            grKho.Properties.DisplayMember = "MaKho";
            grKho.Properties.ValueMember = "MaKho";
            grKho.Properties.View.BestFitColumns();
            if (Config.GetValue("Language").ToString() == "1")
                FormFactory.DevLocalizer.Translate(this);
        }

        private void btnApGia_Click(object sender, EventArgs e)
        {
            if (deNgayCT.EditValue == null || deNgayCT.EditValue.ToString() == ""
                || grKho.EditValue == null || grKho.EditValue.ToString() == "")
            {
                string msg = "Vui lòng cung cấp đầy đủ thông tin!";
                if (Config.GetValue("Language").ToString() == "1")
                    msg = UIDictionary.Translate(msg);
                XtraMessageBox.Show(msg);
                return;
            }
            string sql = "update DFNVL set Gia = BangGiaTB.DonGia, Tien = DFNVL.SoLuong * BangGiaTB.DonGia" +
                " from BangGiaTB where BangGiaTB.NgayCT = cast(convert(nvarchar,'" + deNgayCT.DateTime.ToString("MM/dd/yyyy") + "') as datetime)" +
                " and BangGiaTB.MaKho = '" + grKho.EditValue.ToString() + "' and DFNVL.MaNVL = BangGiaTB.MaVT";
            if (_dbData.UpdateByNonQuery(sql))
            {
                string msg = "Đã cập nhật giá định mức thành công!\n" +
                    "Vui lòng thực hiện chức năng tìm kiếm để xem giá định mức đã áp!";
                if (Config.GetValue("Language").ToString() == "1")
                    msg = UIDictionary.Translate(msg);
                XtraMessageBox.Show(msg);
                this.Close();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmApGiaDM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}