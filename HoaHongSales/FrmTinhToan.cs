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
using CDTDatabase;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraPrinting;

namespace HoaHongSales
{
    public partial class FrmTinhToan : DevExpress.XtraEditors.XtraForm
    {
        private Database _db = Database.NewDataDatabase();
        private bool _first = true;
        public FrmTinhToan()
        {
            InitializeComponent();
            if (Config.GetValue("KyKeToan") != null)
                seThang.Text = Config.GetValue("KyKeToan").ToString();
        }

        private void ceTNDN_CheckedChanged(object sender, EventArgs e)
        {
            lciTNDN.Visibility = (ceTNDN.Checked) ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private int GetColumnWidth(DataRow drvField)
        {
            int type = Int32.Parse(drvField["Type"].ToString());
            if ((type == 2 && !Boolean.Parse(drvField["IsUnique"].ToString())) || type == 4 || type == 7)
                return 200;
            else
            {
                int words = Math.Max(drvField["LabelName"].ToString().Length, drvField["EditMask"].ToString().Split('.')[0].Replace(" ", "").Length);
                int t;
                if (words <= 4)
                    t = 50;
                else
                    if (words <= 8)
                        t = 70;
                    else
                        t = words * 8;
                return t;
            }
        }

        private void TaoLuoi()
        {
            Database dbStruct = Database.NewStructDatabase();
            string s = "select f.* from sysField f inner join sysTable t on f.sysTableID = t.sysTableID where TableName = 'HoaHongSales'";
            DataTable dts = dbStruct.GetDataTable(s);
            if (dts == null)
                return;
            foreach (DataRow dr in dts.Rows)
            {
                string f = dr["FieldName"].ToString();
                GridColumn gc = gvHoaHong.Columns.ColumnByFieldName(f);
                if (gc == null)
                    continue;
                gc.Visible = Boolean.Parse(dr["Visible"].ToString());
                gc.Caption = dr["LabelName"].ToString();
                gc.Width = GetColumnWidth(dr);
                if (f.ToUpper() == "GHICHU" || f.ToUpper() == "HOAHONG" ||
                    f.ToUpper() == "HANMUCDS" || f.ToUpper() == "TYLEHOAHONG")
                    gc.OptionsColumn.AllowEdit = true;
                else
                    gc.OptionsColumn.AllowEdit = false;
                if (Boolean.Parse(dr["IsFixCol"].ToString()))
                    gc.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left; 
                int pType = Int32.Parse(dr["Type"].ToString());
                if (pType == 8 && dr["EditMask"].ToString() != string.Empty)
                {
                    string m = dr["EditMask"].ToString();
                    gc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gc.DisplayFormat.FormatString = m;
                }
                if (gc.ColumnType == typeof(System.Decimal) || gc.ColumnType == typeof(System.Double))
                {
                    string m = "### ### ### ##0.##";
                    gc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gc.DisplayFormat.FormatString = m;
                    gc.SummaryItem.Assign(new DevExpress.XtraGrid.GridSummaryItem(DevExpress.Data.SummaryItemType.Sum, gc.FieldName, "{0:" + m + "}"));
                }
            }
        }

        private void btnTinh_Click(object sender, EventArgs e)
        {
            if (Config.GetValue("NamLamViec") == null)
            {
                XtraMessageBox.Show("Chưa có số liệu!");
                return;
            }
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "dd/MM/yyyy";
            decimal m = seThang.Value < 12 ? seThang.Value + 1 : 1;
            DateTime ngay = DateTime.Parse("01/" + m.ToString() + "/" + Config.GetValue("NamLamViec").ToString(), dtfi);
            ngay = ngay.AddDays(-1);
            string sql = "select * from HoaHongSales where NgayCT = '" + ngay.ToString("MM/dd/yyyy") + "'";
            DataTable dt = _db.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                gcHoaHong.DataSource = dt;
                if (_first)
                    TaoLuoi();
                if (XtraMessageBox.Show("Hoa hồng kỳ này đã được tính,\nnhấn Có để tính lại hoặc nhấn Không để xem!",
                    "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    TinhHoaHong(ngay);
            }
            else
                TinhHoaHong(ngay);
        }

        private void TinhHoaHong(DateTime ngay)
        {
            DataTable dt = _db.GetDataSetByStore("TinhHoaHong", new string[] { "Ngay", "Ck", "HTL", "GTGT", "TNDN", "TyLeTNDN" },
                new object[] { ngay, ceCK.Checked ? 1 : 0, ceHTL.Checked ? 1 : 0, ceGTGT.Checked ? 1 : 0, ceTNDN.Checked ? 1 : 0, seTyLe.Value });
            gcHoaHong.DataSource = dt;
            if (_first)
                TaoLuoi();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            ShowCustomizePreview(gcHoaHong, true);
        }

        private void ShowCustomizePreview(IPrintable ctrl, bool isLanscape)
        {
            // Create a PrintingSystem component. 
            PrintingSystem ps = new PrintingSystem();
            // Create a link that will print a control. 
            PrintableComponentLink Print = new PrintableComponentLink(ps);
            // Specify the control to be printed. 
            Print.Component = ctrl;
            // Set the paper format. 
            Print.PaperKind = System.Drawing.Printing.PaperKind.A4;
            Print.Landscape = isLanscape;
            Print.Margins = new System.Drawing.Printing.Margins(40, 40, 40, 40);
            string dbName = Config.GetValue("DbName").ToString();
            if (dbName.Contains("DEMO"))
            {
                ps.Watermark.Text = "Hoa Tiêu Demo";
                ps.Watermark.TextTransparency = 150;
            }
            // Subscribe to the CreateReportHeaderArea event used to generate the report header. 
            Print.CreateReportHeaderArea +=
              new CreateAreaEventHandler(Print_CreateReportHeaderArea);
            Print.CreateReportFooterArea += new CreateAreaEventHandler(Print_CreateReportFooterArea);
            // Generate the report. 
            Print.CreateDocument();
            // Show the report. 
            Print.ShowPreview();

        }

        void Print_CreateReportFooterArea(object sender, CreateAreaEventArgs e)
        {
            string reportFooter = "Ngày " + DateTime.Today.Day.ToString("00") +
                " tháng " + DateTime.Today.Month.ToString("00") +
                " năm " + DateTime.Today.Year.ToString("0000") +
                "\nNgười lập            ";
            e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
            e.Graph.Font = new Font("Times New Roman", 10, FontStyle.Italic);
            e.Graph.BackColor = Color.Transparent;
            int x = (sender as PrintableComponentLink).Landscape ? 800 : 500;
            RectangleF rec = new RectangleF(x, 20, 200, 50);
            e.Graph.DrawString(reportFooter, Color.Black, rec, BorderSide.None);
        }

        private void Print_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            if (Config.GetValue("TenCongTy") != null)
            {
                string info = Config.GetValue("TenCongTy").ToString();
                if (Config.GetValue("DiaChi") != null)
                    info += "\n" + Config.GetValue("DiaChi").ToString();
                e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Near);
                e.Graph.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                e.Graph.BackColor = Color.Transparent;
                RectangleF rec1 = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
                e.Graph.DrawString(info, Color.Black, rec1, BorderSide.None);
            }

            string reportHeader = "BẢNG HOA HỒNG NHÂN VIÊN KINH DOANH";
            e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
            e.Graph.Font = new Font("Times New Roman", 20, FontStyle.Bold);
            e.Graph.BackColor = Color.Transparent;
            RectangleF rec2 = new RectangleF(0, 50, e.Graph.ClientPageSize.Width, 50);
            e.Graph.DrawString(reportHeader, Color.Black, rec2, BorderSide.None);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "dd/MM/yyyy";
            decimal m = seThang.Value < 12 ? seThang.Value + 1 : 1;
            DateTime ngay = DateTime.Parse("01/" + m.ToString() + "/" + Config.GetValue("NamLamViec").ToString(), dtfi);
            ngay = ngay.AddDays(-1);
            string sql = "select * from HoaHongSales where NgayCT = '" + ngay.ToString("MM/dd/yyyy") + "'";
            if (_db.UpdateDataTable(sql, gcHoaHong.DataSource as DataTable))
                XtraMessageBox.Show("Đã lưu bảng hoa hồng nhân viên kỳ này!");
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            if (Config.GetValue("NamLamViec") == null)
            {
                XtraMessageBox.Show("Chưa có số liệu!");
                return;
            }
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "dd/MM/yyyy";
            decimal m = seThang.Value < 12 ? seThang.Value + 1 : 1;
            DateTime ngay = DateTime.Parse("01/" + m.ToString() + "/" + Config.GetValue("NamLamViec").ToString(), dtfi);
            ngay = ngay.AddDays(-1);
            string sql = "select * from HoaHongSales where NgayCT = '" + ngay.ToString("MM/dd/yyyy") + "'";
            DataTable dt = _db.GetDataTable(sql);
            if (dt.Rows.Count == 0)
            {
                if (XtraMessageBox.Show("Chưa có số liệu hoa hồng kỳ này,\nBạn có muốn tính hoa hồng kỳ này không?",
                    "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    TinhHoaHong(ngay);
            }
            else
            {
                gcHoaHong.DataSource = dt;
                if (_first)
                    TaoLuoi();
            }
        }
    }
}