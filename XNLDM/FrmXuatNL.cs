using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Plugins;
using CDTLib;
using CDTDatabase;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;

namespace XNLDM
{
    public partial class FrmXuatNL : DevExpress.XtraEditors.XtraForm
    {
        DataCustomFormControl _data;
        public FrmXuatNL(DataCustomFormControl data)
        {
            InitializeComponent();
            _data = data;
        }
        public Database _dbData = Database.NewDataDatabase();

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmXuatNL_Load(object sender, EventArgs e)
        {
            LayPhieuNhap();
            LayKho();
            LayTK();
        }

        private void LayPhieuNhap()
        {
            string sql = "SELECT SoCT,NgayCT,DienGiai FROM MT41 " +
                "WHERE not exists (select RefValue from MT43 where RefValue like 'NSX_%' and RefValue like '%' + MT41.SoCT + '%')";
            DataTable dt = _dbData.GetDataTable(sql);
            dt.Columns.Add("check", typeof(System.Boolean));
            if (dt.Rows.Count == 0)
            {
                XtraMessageBox.Show("Chưa có phiếu nhập thành phẩm hoặc các phiếu nhập đều đã xuất nguyên liệu!");
                this.Close();
            }
            gridControl1.DataSource = dt;
        }

        void LayKho()
        {
            string sql = "SELECT * FROM DMKho";
            DataTable dt = _dbData.GetDataTable(sql);
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
            DataTable dt = _dbData.GetDataTable(sql);
            lookupDMTK.Properties.DataSource = dt;
            lookupDMTK.Properties.DisplayMember = "TK";
            lookupDMTK.Properties.ValueMember = "TK";
            lookupDMTK.Properties.BestFit();            
        }

        bool KiemTra()
        {
            if (lookupDMTK.EditValue == null)
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

        private void btnXuatNL_Click(object sender, EventArgs e)
        {
            if (!KiemTra())
                return;
            DataView dtView = new DataView(gridControl1.DataSource as DataTable);
            dtView.RowFilter = "check=true";
            if (dtView.Count == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn phiếu nhập thành phẩm cần xuất nguyên liệu!", Config.GetValue("PackageName").ToString());
                return;
            }
            //xây dựng điều kiện các phiếu nhập đã chọn
            string dk = "(";
            foreach (DataRowView drv in dtView)
                dk += "'" + drv["SoCT"].ToString() + "',";
            dk = dk.Remove(dk.Length - 1);
            dk += ")";
            //câu sql lấy dữ liệu tạo phiếu xuất
            string sql = " select DFNVL.MaNVL,DMVT.TenVT, DMVT.TkKho,sum(DT.SoLuong*DFNVL.SoLuong) as SoLuong,DFNVL.DVT,MT.SoCT, " +
                         " isnull((select Top 1 (BG.DonGia*HSQuyDoi) from BangGiaTB  as BG inner join wVTDVT wDVT ON BG.MaVT=wDVT.MaVT  where DFNVL.DVT=wDVT.MaDVT and BG.MaVT=DFNVL.MaNVL ORDER BY NgayCT DESC),0) as Dongia, " + //Tho thêm điều kiện 'and BG.MaVT=DFNVL.MaNVL' -ngày 14/12/2011
                         " (sum(DT.SoLuong*DFNVL.SoLuong) * isnull((select Top 1 BG.DonGia from BangGiaTB as BG where BG.MaVT=DFNVL.MaNVL ORDER BY NgayCT DESC),0)) as ThanhTien, " +
                         " (select HSQuyDoi from wVTDVT WHERE wVTDVT.MaVT=DFNVL.MaNVL and DFNVL.DVT=wVTDVT.MaDVT) as HSQuyDoi, " +
                         " (sum(DT.SoLuong*DFNVL.SoLuong)*(select HSQuyDoi from wVTDVT WHERE wVTDVT.MaVT=DFNVL.MaNVL and DFNVL.DVT=wVTDVT.MaDVT)) as SLQuyDoi, " +
                         " isnull((select Top 1 BG.DonGia from BangGiaTB as BG where BG.MaVT=DFNVL.MaNVL order by NgayCT DESC),0) as DGQuyDoi " +
                         " from DT41 DT inner join DFNVL ON DT.MaVT=DFNVL.MaVT " +
                         " inner join MT41 MT ON MT.MT41ID=DT.MT41ID " +
                         " inner join DMVT ON DMVT.MaVT=DFNVL.MaNVL " +
                         " where MT.SoCT in " + dk +
                         " group by DFNVL.MaNVL, DMVT.TenVT, MT.SoCT, DFNVL.DVT, DMVT.TkKho";
            DataTable dtXuat = _dbData.GetDataTable(sql);
            //tạo phiếu xuất
            GridView gvChiTiet = (_data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl).MainView as GridView;
            //ghi dữ liệu Master
            DataRow drCurMas = (_data.BsMain.Current as DataRowView).Row;
            string socts = dk.Replace("(", "").Replace(")", "");
            drCurMas["RefValue"] = "NSX_" + socts.Replace("'","").Replace(",","_");
            drCurMas["DienGiai"] = "Xuất nguyên liệu cho phiếu nhập " + socts;                        
            //ghi dữ liệu detail
            foreach (DataRow dr in dtXuat.Rows)
            {
                gvChiTiet.AddNewRow();
                gvChiTiet.UpdateCurrentRow();
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["MaKho"], lookupDMKho.EditValue);
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["MaVT"], dr["MaNVL"]);
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["TenVT"], dr["TenVT"]);
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["MaDVT"], dr["DVT"]);
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["SoLuong"], dr["SoLuong"]);
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["Gia"], dr["Dongia"]);
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["TkCo"], dr["TkKho"]);
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["TkNo"], lookupDMTK.EditValue);
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["HSQuyDoi"], dr["HSQuyDoi"]);
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["SLQuyDoi"], dr["SLQuyDoi"]);
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["DGQuyDoi"], dr["DGQuyDoi"]);
            }
            this.Close();
        }
    }
}