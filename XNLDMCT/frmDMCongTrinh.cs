using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTDatabase;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using CDTLib;
using Plugins;

namespace XNLDMCT
{
    public partial class frmDMCongTrinh : DevExpress.XtraEditors.XtraForm
    {
        DataCustomFormControl _data;
        Database db = Database.NewDataDatabase();

        public frmDMCongTrinh(DataCustomFormControl data)
        {
            InitializeComponent();
            _data = data;
        }      

        private void frmDMCongTrinh_Load(object sender, EventArgs e)
        {
            LayCongTrinh();
            LayKho();
            LayTK();
        }

        void LayCongTrinh()
        {
            string sql = "select * from dmcongtrinh where isHT='0' and macongtrinh in (select distinct(macongtrinh) from dfnvlct) ";
            DataTable dt = db.GetDataTable(sql);
            DataColumn dtCol = new DataColumn("check", typeof(Boolean));
            dtCol.DefaultValue = false;
            if (dt.Rows.Count == 0)
            {
                XtraMessageBox.Show("Chưa thiết lập định mức nguyên vật liệu cho công trình, hoặc đã xuất nguyên vật liệu rồi.",Config.GetValue("PackageName").ToString());
                this.Close();
            }
            dt.Columns.Add(dtCol);            
            gcDMCongTrinh.DataSource = dt;
            gvDMCongTrinh.BestFitColumns();
        }

        void LayKho()
        {
            string sql = "SELECT * FROM DMKho";
            DataTable dt = db.GetDataTable(sql);
            lookupDMKho.Properties.DataSource = dt;
            lookupDMKho.Properties.DisplayMember = "TenKho";
            lookupDMKho.Properties.ValueMember = "MaKho";
            if (dt.Rows.Count > 0)
            {
                lookupDMKho.EditValue = dt.Rows[0]["MaKho"].ToString();
            }
        }

        void LayTK()
        {
            string sql = "SELECT TK, TenTK FROM DMTK where TK not in (select  TK=case when TKMe is null then '' else TKMe end from DMTK group by TKMe)";
            DataTable dt = db.GetDataTable(sql);
            lookupDMTK.Properties.DataSource = dt;
            lookupDMTK.Properties.DisplayMember = "TK";
            lookupDMTK.Properties.ValueMember = "TK";
            lookupDMTK.Properties.BestFit();
            lookupDMTK.EditValue = "";
        }

        bool KiemTra()
        {
            if (lookupDMTK.EditValue == null || lookupDMTK.EditValue.ToString().Equals(""))
            {
                XtraMessageBox.Show("Chọn tài khoản chi phí!", Config.GetValue("PackageName").ToString());
                return false;
            }
            if (lookupDMKho.EditValue == null)
            {
                XtraMessageBox.Show("Chọn kho xuất!", Config.GetValue("PackageName").ToString());
                return false;
            }
            return true;
        }      

        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXuatNL_Click(object sender, EventArgs e)
        {
            if (!KiemTra())
                return;
            DataView dvCongTrinh = new DataView(gcDMCongTrinh.DataSource as DataTable);
            dvCongTrinh.RowFilter = "check = True";
            if (dvCongTrinh.Count == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn công trình để xuất nguyên liệu.",Config.GetValue("PackageName").ToString());
                return;
            }
            string dk = "";
            foreach(DataRowView drv in dvCongTrinh)
            {
                dk += "'"+drv.Row["MaCongTrinh"].ToString()+"',";
            }
            dk = dk.Substring(0,dk.Length-1);
            dk = "(" + dk + ")";
            string sql = "select DF.MaCongTrinh,DF.MaVT,VT.TenVT,VT.TkKho,DF.DVT, (sum(DF.SoLuong )/sum(isnull(sothang,1))) as soluong, "+
                         "isnull((select Top 1 (BG.DonGia*HSQuyDoi) from BangGiaTB  as BG inner join wVTDVT wDVT ON BG.MaVT=wDVT.MaVT "+
                         "where DF.MaVT=BG.MaVT and DF.DVT=wDVT.MaDVT ORDER BY NgayCT DESC),0) as Dongia, "+
                         "sum(DF.SoLuong) * isnull((select Top 1 BG.DonGia from BangGiaTB as BG  "+
                         "where BG.MaVT=DF.MaVT ORDER BY NgayCT DESC),0) as ThanhTien,  "+
                         "((select HSQuyDoi from wVTDVT WHERE wVTDVT.MaVT=DF.MaVT and DF.DVT=wVTDVT.MaDVT)) as HSQuyDoi ,  "+
                         " ((sum(DF.SoLuong) *(select HSQuyDoi from wVTDVT WHERE wVTDVT.MaVT=DF.MaVT and DF.DVT=wVTDVT.MaDVT))/sum(isnull(sothang,1))) as SLQuyDoi,  "+
                         " (isnull((select Top 1 BG.DonGia from BangGiaTB as BG   "+
                         "where BG.MaVT=DF.MaVT order by NgayCT DESC),0)) as DGQuyDoi   "+
                         " from DMVT VT inner join DFNVLCT DF on VT.MaVT=DF.MaVT  " +
                         "inner join dmCongTrinh CT on CT.MaCongTrinh=DF.MaCongTrinh "+
                         "where DF.Macongtrinh in " + dk+" "+
                         "group by DF.MaCongTrinh,DF.MaVT,VT.TenVT,VT.TkKho, DF.DVT";
            DataTable dt = db.GetDataTable(sql);            
            GridView gvMain = (_data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl).MainView as GridView;
            DataRow drCurrent = (_data.BsMain.Current as DataRowView).Row;
            string mact = dk.Replace("(", "").Replace(")", "");
            drCurrent["RefValue"] = "NCT_"+mact.Replace("'","_");
            drCurrent["DienGiai"] = "Xuất nguyên liệu cho công trình "+mact;
            foreach (DataRow dr in dt.Rows)
            {
                gvMain.AddNewRow();
                gvMain.UpdateCurrentRow();
                gvMain.SetFocusedRowCellValue(gvMain.Columns["MaKho"], lookupDMKho.EditValue);
                gvMain.SetFocusedRowCellValue(gvMain.Columns["MaVT"], dr["MaVT"]);
                gvMain.SetFocusedRowCellValue(gvMain.Columns["TenVT"], dr["TenVT"]);
                gvMain.SetFocusedRowCellValue(gvMain.Columns["MaDVT"], dr["DVT"]);
                gvMain.SetFocusedRowCellValue(gvMain.Columns["SoLuong"], dr["SoLuong"]);
                gvMain.SetFocusedRowCellValue(gvMain.Columns["Gia"], dr["Dongia"]);
                gvMain.SetFocusedRowCellValue(gvMain.Columns["TkCo"], dr["TkKho"]);
                gvMain.SetFocusedRowCellValue(gvMain.Columns["TkNo"], lookupDMTK.EditValue);
                gvMain.SetFocusedRowCellValue(gvMain.Columns["HSQuyDoi"], dr["HSQuyDoi"]);
                gvMain.SetFocusedRowCellValue(gvMain.Columns["SLQuyDoi"], dr["SLQuyDoi"]);
                gvMain.SetFocusedRowCellValue(gvMain.Columns["DGQuyDoi"], dr["DGQuyDoi"]);
                gvMain.SetFocusedRowCellValue(gvMain.Columns["MaCongTrinh"], dr["MaCongTrinh"]);
            }
            this.Close();
        }
    }
}