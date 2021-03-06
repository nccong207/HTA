using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using CDTDatabase;
using CDTLib;
using System.Globalization;

namespace CLTG
{
    public partial class frmCLTG : DevExpress.XtraEditors.XtraForm
    {
        string _mact = "Z10";
        private Database dbData = Database.NewDataDatabase();
        public frmCLTG()
        {
            InitializeComponent();
        }

        private void frmCLTG_Load(object sender, EventArgs e)
        {
            MaNT.Properties.DataSource = dbData.GetDataTable("select * from DMNT");
            MaNT.Properties.DisplayMember = "MaNT";
            MaNT.Properties.ValueMember = "MaNT";
            tkLai.Properties.DataSource = dbData.GetDataTable("select * from DMTK where TK not in (select  TK=case when TKMe is null then '' else TKMe end from DMTK group by TKMe)");
            tkLai.Properties.DisplayMember = "TK";
            tkLai.Properties.ValueMember = "TK";
            tkLai.Properties.PopupFormMinSize = new Size(600, 100);
            tkLai.Properties.View.BestFitColumns();
            tkLo.Properties.DataSource = tkLai.Properties.DataSource;
            tkLo.Properties.DisplayMember = "TK";
            tkLo.Properties.ValueMember = "TK";
            tkLo.Properties.PopupFormMinSize = new Size(600, 100);
            tkLo.Properties.View.BestFitColumns();

            Month.EditValue = Config.GetValue("KyKeToan") == null ? DateTime.Today.Month : Int32.Parse(Config.GetValue("KyKeToan").ToString());
            MaNT.EditValue = "USD";
            tkLai.EditValue = "515";
            tkLo.EditValue = "635";
            TyGiaCK.EditValue = 0;
            if (Config.GetValue("Language").ToString() == "1")
                FormFactory.DevLocalizer.Translate(this);
        }

        private void TinhCLTG_Click(object sender, EventArgs e)
        {
            int Thang = int.Parse(Month.EditValue.ToString());
            if (KhoaSo(Thang))
                return;
            grData.DataSource = dbData.GetDataSetByStore("TinhCLTG",
                new string[] { "Month", "MaNT", "TyGiaCK" },
                new object[] { Thang, MaNT.EditValue, TyGiaCK.EditValue });
        }

        private void HachToan_Click(object sender, EventArgs e)
        {
            int Nam = int.Parse(Config.GetValue("NamLamViec").ToString());
            int Thang = int.Parse(Month.EditValue.ToString());
            if (KhoaSo(Thang))
                return;
            dbData.BeginMultiTrans();

            string str = (Thang).ToString() + "/" + "01" + "/" + Nam.ToString();
            DateTime NgayCT = Convert.ToDateTime(str).AddMonths(1).AddDays(-1);
            bool kq = false;

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                DataRow dr = gridView1.GetDataRow(i);
                kq = CreateButToan(dr, NgayCT);
                if (kq == false)
                    break;
            }
            if (kq)
            {
                MessageBox.Show("Hạch toán thành công!");
                dbData.EndMultiTrans();
            }
        }

        private bool CreateButToan(DataRow dr, DateTime NgayCT)
        {
            string soct = "'" + _mact + NgayCT.Month.ToString("00") + "/" + MaNT.EditValue.ToString() + "'";
            try
            {
                string tableName = "bltk";
                List<string> fieldName = new List<string>();
                List<string> Values = new List<string>();
                Guid id = new Guid();
                Guid iddt = new Guid();
                fieldName.Add("MTID");
                fieldName.Add("MTIDDT");
                fieldName.Add("Nhomdk");
                fieldName.Add("MaCT");
                fieldName.Add("SoCT");
                fieldName.Add("NgayCT");
                fieldName.Add("MaKH");
                fieldName.Add("MaNT");
                fieldName.Add("TyGia");
                fieldName.Add("DienGiai");
                fieldName.Add("TK");
                fieldName.Add("TKdu");
                fieldName.Add("Psno");
                fieldName.Add("Psco");
                fieldName.Add("MaBP");
                fieldName.Add("MaVV");
                fieldName.Add("MaPhi");

                Values.Clear();
                Values.Add("convert( uniqueidentifier,'" + id.ToString() + "')");
                Values.Add("convert( uniqueidentifier,'" + iddt.ToString() + "')");
                Values.Add("'" + _mact + MaNT.EditValue.ToString() + "'");
                Values.Add("'" + _mact + "'");
                Values.Add(soct);
                Values.Add("cast('" + NgayCT.Date.ToString() + "' as datetime)");
                if (dr["MaKH"].ToString() != string.Empty)
                    Values.Add("'" + dr["MaKH"].ToString() + "'");
                else
                    Values.Add("null");
                Values.Add("'" + MaNT.EditValue.ToString() + "'");
                Values.Add("'" + TyGiaCK.EditValue.ToString() + "'");
                Values.Add("N'Chênh lệch tỷ giá tháng " + Month.EditValue.ToString() + "/" + NgayCT.Year.ToString() + " ngoại tệ " + MaNT.EditValue.ToString() + " tài khoản " + dr["TK"].ToString() + "'");
                
                if (double.Parse(dr["tienLai"].ToString()) > 0)//Dư nợ tk / Dư có tkLãi
                {
                    Values.Add("'" + dr["TK"].ToString() + "'");
                    Values.Add("'" + tkLai.EditValue.ToString() + "'");

                    Values.Add(double.Parse(dr["tienLai"].ToString()).ToString("###########0.#######"));
                    Values.Add("0");

                }
                else //Dư nợ tkLỗ / Dư có tk
                {
                    Values.Add("'" + tkLo.EditValue.ToString() + "'");
                    Values.Add("'" + dr["TK"].ToString() + "'");
                
                    Values.Add(double.Parse(dr["tienLo"].ToString()).ToString("###########0.#######"));
                    Values.Add("0");

                }
                if (dr["MaBP"].ToString() != string.Empty)
                    Values.Add("'" + dr["MaBP"].ToString() + "'");
                else
                    Values.Add("null");
                if (dr["MaVV"].ToString() != string.Empty)
                    Values.Add("'" + dr["MaVV"].ToString() + "'");
                else
                    Values.Add("null");
                if (dr["MaPhi"].ToString() != string.Empty)
                    Values.Add("'" + dr["MaPhi"].ToString() + "'");
                else
                    Values.Add("null");

                if (!dbData.insertRow(tableName, fieldName, Values))
                {
                    return false;
                }
                //bút toán đảo ngược
                id = new Guid();
                Values.Clear();
                Values.Add("convert( uniqueidentifier,'" + id.ToString() + "')");
                Values.Add("convert( uniqueidentifier,'" + iddt.ToString() + "')");
                Values.Add("'" + _mact + MaNT.EditValue.ToString() + "'");
                Values.Add("'" + _mact + "'");
                Values.Add(soct);
                Values.Add("cast('" + NgayCT.Date.ToString() + "' as datetime)");
                if (dr["MaKH"].ToString() != string.Empty)
                    Values.Add("'" + dr["MaKH"].ToString() + "'");
                else
                    Values.Add("null");
                Values.Add("'" + MaNT.EditValue.ToString() + "'");
                Values.Add("'" + TyGiaCK.EditValue.ToString() + "'");
                Values.Add("N'Chênh lệch tỷ giá tháng " + Month.EditValue.ToString() + "/" + NgayCT.Year.ToString() + " ngoại tệ " + MaNT.EditValue.ToString() + " tài khoản " + dr["TK"].ToString() + "'");

                if (double.Parse(dr["tienLai"].ToString()) > 0)//Dư nợ tk / Dư có tkLãi
                {
                    Values.Add("'" + tkLai.EditValue.ToString() + "'");
                    Values.Add("'" + dr["TK"].ToString() + "'");

                    Values.Add("0");
                    Values.Add(double.Parse(dr["tienLai"].ToString()).ToString("###########0.#######"));

                }
                else //Dư nợ tkLỗ / Dư có tk
                {
                    Values.Add("'" + dr["TK"].ToString() + "'");
                    Values.Add("'" + tkLo.EditValue.ToString() + "'");

                    Values.Add("0");
                    Values.Add(double.Parse(dr["tienLo"].ToString()).ToString("###########0.#######"));

                }
                if (dr["MaBP"].ToString() != string.Empty)
                    Values.Add("'" + dr["MaBP"].ToString() + "'");
                else
                    Values.Add("null");
                if (dr["MaVV"].ToString() != string.Empty)
                    Values.Add("'" + dr["MaVV"].ToString() + "'");
                else
                    Values.Add("null");
                if (dr["MaPhi"].ToString() != string.Empty)
                    Values.Add("'" + dr["MaPhi"].ToString() + "'");
                else
                    Values.Add("null");
                if (!dbData.insertRow(tableName, fieldName, Values))
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;

            }
            return true;
        }

        private void XoaHachToan_Click(object sender, EventArgs e)
        {
            int Nam = int.Parse(Config.GetValue("NamLamViec").ToString());
            int Thang = int.Parse(Month.EditValue.ToString());
            if (KhoaSo(Thang))
                return;
            string str = (Thang).ToString() + "/" + "01" + "/" + Nam.ToString();
            DateTime NgayCT = Convert.ToDateTime(str).AddMonths(1).AddDays(-1);
            string msg = "Đã xóa hạch toán chênh lệch tỷ giá tháng ";
            if (Config.GetValue("Language").ToString() == "1")
                msg = UIDictionary.Translate(msg);
            if (dbData.UpdateByNonQuery("delete from bltk where NgayCT = '" + NgayCT.Date.ToString() + "' and MaCT = '" + _mact + "' and NhomDK = '" + _mact + MaNT.EditValue.ToString() + "'"))
                XtraMessageBox.Show(msg + Month.EditValue.ToString());
        }

        private void gridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                (sender as GridView).DeleteSelectedRows();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCLTG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
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
    }
}