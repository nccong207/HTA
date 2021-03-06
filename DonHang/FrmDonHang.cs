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

namespace DonHang
{
    public partial class FrmDonHang : DevExpress.XtraEditors.XtraForm
    {
        private enum LoaiDH { PO, SO };
        private LoaiDH _loaiDH;
        private BindingSource _bsMain;
        private bool _first = true;

        private Database _db = Database.NewDataDatabase();

        public FrmDonHang(string loaiDH)
        {
            InitializeComponent();
            gvMT62.GroupPanelText = "Dùng chuột kéo cột số liệu vào đây để nhóm số liệu";
            if (loaiDH == "SO")
                _loaiDH = LoaiDH.SO;
            else
                _loaiDH = LoaiDH.PO;
        }

        private void FrmDonHang_Load(object sender, EventArgs e)
        {
            TaoForm();
            LayPhieuYC();
            LayKH();
            LayTTDH();
            LayNguoiDuyet();
        }

        private void TaoForm()
        {
            lciPYC.Text = _loaiDH == LoaiDH.PO ? "Phiếu yêu cầu" : "Phiếu chào giá";
            lciDoiTuong.Text = _loaiDH == LoaiDH.PO ? "Nhà cung cấp" : "Khách hàng";
            if (Config.GetValue("KyKeToan") != null)
            {
                seKy1.EditValue = Config.GetValue("KyKeToan");
                seKy2.EditValue = Config.GetValue("KyKeToan");
            }
        }

        private void LayPhieuYC()
        {
            string t = _loaiDH == LoaiDH.PO ? "MT61" : "MT63";
            string sql = "select SoCT, NgayCT, MaKH, TenKH, DienGiai, NguoiLap, TenTT " +
                "from " + t + " inner join DMTTDH m on " + t + ".TinhTrang = m.MaTT " +
                "where (month(NgayCT) between " + seKy1.Text + " and " + seKy2.Text + ") and year(NgayCT) = " + Config.GetValue("NamLamViec").ToString();
            DataTable dt = _db.GetDataTable(sql);
            gluMT61.Properties.DataSource = dt;
            gluMT61.Properties.View.Columns["SoCT"].Caption = "Số phiếu";
            gluMT61.Properties.View.Columns["NgayCT"].Caption = "Ngày lập";
            gluMT61.Properties.View.Columns["MaKH"].Caption = _loaiDH == LoaiDH.PO ? "Nhà cung cấp" : "Khách hàng";
            gluMT61.Properties.View.Columns["TenKH"].Caption = "Tên";
            gluMT61.Properties.View.Columns["DienGiai"].Caption = "Diễn giải";
            gluMT61.Properties.View.Columns["NguoiLap"].Caption = "Người lập";
            gluMT61.Properties.View.Columns["TenTT"].Caption = "Tình trạng";
            gluMT61.Properties.View.BestFitColumns();
            gluMT61.Properties.DisplayMember = "SoCT";
            gluMT61.Properties.ValueMember = "SoCT";
        }

        private void LayKH()
        {
            string sql = "select MaKH, TenKH, DiaChi, Nhom1, Nhom2, Nhom3 from DMKH where ";
            string t = _loaiDH == LoaiDH.PO ? "isNCC = 1" : "isKH = 1";
            sql += t;
            DataTable dt = _db.GetDataTable(sql);
            gluMaKH.Properties.DataSource = dt;
            gluMaKH.Properties.View.Columns["MaKH"].Caption = "Mã";
            gluMaKH.Properties.View.Columns["TenKH"].Caption = "Tên";
            gluMaKH.Properties.View.Columns["DiaChi"].Caption = "Địa chỉ";
            gluMaKH.Properties.View.Columns["Nhom1"].Caption = "Nhóm 1";
            gluMaKH.Properties.View.Columns["Nhom2"].Caption = "Nhóm 2";
            gluMaKH.Properties.View.Columns["Nhom3"].Caption = "Nhóm 3";
            gluMaKH.Properties.View.BestFitColumns();
            gluMaKH.Properties.DisplayMember = "MaKH";
            gluMaKH.Properties.ValueMember = "MaKH";
        }

        private void LayTTDH()
        {
            string sql = "select MaTT, TenTT from DMTTDH";
            DataTable dt = _db.GetDataTable(sql);
            lueTinhTrang.Properties.DataSource = dt;
            lueTinhTrang.Properties.PopulateColumns();
            lueTinhTrang.Properties.Columns["MaTT"].Visible = false;
            lueTinhTrang.Properties.ShowHeader = false;
            lueTinhTrang.Properties.DisplayMember = "TenTT";
            lueTinhTrang.Properties.ValueMember = "MaTT";
            lueTinhTrang.Properties.BestFit();
            lueTinhTrang1.Properties.DataSource = dt;
            lueTinhTrang1.Properties.PopulateColumns();
            lueTinhTrang1.Properties.Columns["MaTT"].Visible = false;
            lueTinhTrang1.Properties.ShowHeader = false;
            lueTinhTrang1.Properties.DisplayMember = "TenTT";
            lueTinhTrang1.Properties.ValueMember = "MaTT";
            lueTinhTrang1.Properties.BestFit();
        }

        private void LayNguoiDuyet()
        {
            string sql = "select MaKH, TenKH from DMKH where isNV = 1";
            DataTable dt = _db.GetDataTable(sql);
            lueNguoiDuyet.Properties.DataSource = dt;
            lueNguoiDuyet.Properties.PopulateColumns();
            lueNguoiDuyet.Properties.Columns["MaKH"].Visible = false;
            lueNguoiDuyet.Properties.ShowHeader = false;
            lueNguoiDuyet.Properties.DisplayMember = "TenKH";
            lueNguoiDuyet.Properties.ValueMember = "MaKH";
            lueNguoiDuyet.Properties.BestFit();
        }

        private DataSet TimKiem()
        {
            string t = _loaiDH == LoaiDH.PO ? "MT62" : "MT64";
            string sql = "select * from " + t + " where (month(NgayCT) between " + seKy1.Text + " and " + seKy2.Text + ")";
            if (gluMT61.Text != "")
                sql += " and SoPhieuNguon = '" + gluMT61.Text + "'";
            if (teSoDonHang.Text != "")
                sql += " and SoCT = '" + teSoDonHang.Text + "'";
            if (gluMaKH.Text != "")
                sql += " and MaKH = '" + gluMaKH.Text + "'";
            if (lueTinhTrang.Text != "")
                sql += " and TinhTrang = '" + lueTinhTrang.EditValue + "'";
            DataTable dtDH = _db.GetDataTable(sql);
            dtDH.TableName = "DonHang";
            DataSet ds = new DataSet();
            ds.Tables.Add(dtDH);

            t += "ID";
            string t1 = _loaiDH == LoaiDH.PO ? "DT62" : "DT64";
            string sql1 = "select * from " + t1 + " where " + t + " in (" + sql.Replace("*", t) + ")";
            DataTable dtDHCT = _db.GetDataTable(sql1);
            dtDHCT.TableName = "DonHangCT";
            ds.Tables.Add(dtDHCT);
            DataRelation dr1 = new DataRelation("DonHang_DonHangCT", dtDH.Columns[t], dtDHCT.Columns[t], true);
            ds.Relations.Add(dr1);

            t1 = _loaiDH == LoaiDH.PO ? "wHDMCT" : "wHDBCT";
            sql1 = "select * from " + t1 + " where SoDonHang in (" + sql.Replace("*", "SoCT") + ")";
            DataTable dtHoaDon = _db.GetDataTable(sql1);
            dtHoaDon.TableName = "HoaDon";
            ds.Tables.Add(dtHoaDon);
            DataRelation dr2 = new DataRelation("DonHang_HoaDon", dtDH.Columns["SoCT"], dtHoaDon.Columns["SoDonHang"], true);
            ds.Relations.Add(dr2);

            t1 = _loaiDH == LoaiDH.PO ? "TienDoNhanHang" : "TienDoGiaoHang";
            sql1 = "select t.*, d.TenTT from " + t1 + " t, DMTTGNH d where t.TinhTrang = d.MaTT and " + t + " in (" + sql.Replace("*", t) + ")";
            DataTable dtGiaoNhan = _db.GetDataTable(sql1);
            dtGiaoNhan.TableName = "GiaoNhan";
            ds.Tables.Add(dtGiaoNhan);
            DataRelation dr3 = new DataRelation("DonHang_GiaoNhan", dtDH.Columns[t], dtGiaoNhan.Columns[t], true);
            ds.Relations.Add(dr3);
            return ds;
        }

        private void HienThiSoLieu(DataSet ds)
        {
            _bsMain = new BindingSource(ds, ds.Tables[0].TableName);
            gcMT62.DataSource = _bsMain;

            BindingSource bs1 = new BindingSource(_bsMain, ds.Relations[0].RelationName);
            gcDT62.DataSource = bs1;

            BindingSource bs2 = new BindingSource(_bsMain, ds.Relations[1].RelationName);
            gcHDMCT.DataSource = bs2;

            BindingSource bs3 = new BindingSource(_bsMain, ds.Relations[2].RelationName);
            gcTienDoNhanHang.DataSource = bs3;

            teSoDonHang1.DataBindings.Clear();
            lueTinhTrang1.DataBindings.Clear();
            lueNguoiDuyet.DataBindings.Clear();
            deNgayDuyet.DataBindings.Clear();
            teGhiChu.DataBindings.Clear();
            teSoDonHang1.DataBindings.Add("EditValue", _bsMain, "SoCT");
            lueTinhTrang1.DataBindings.Add("EditValue", _bsMain, "TinhTrang");
            lueNguoiDuyet.DataBindings.Add("EditValue", _bsMain, "NguoiDuyet");
            deNgayDuyet.DataBindings.Add("EditValue", _bsMain, "NgayDuyet");
            teGhiChu.DataBindings.Add("EditValue", _bsMain, "GhiChu");

            gvMT62_FocusedRowChanged(gvMT62, new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs(-1, 0));
        }

        private void DinhDangSoLieu()
        {
            gvMT62.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gvMT62_FocusedRowChanged);

            string t = _loaiDH == LoaiDH.PO ? "MT62ID" : "MT64ID";
            gvMT62.Columns[t].Visible = false;
            gvMT62.Columns["NgayCT"].Caption = "Ngày lập";
            gvMT62.Columns["SoCT"].Caption = "Số đơn hàng";
            gvMT62.Columns["NgayCT"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvMT62.Columns["SoCT"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvMT62.Columns["MaCT"].Visible = false;
            gvMT62.Columns["MaKH"].Caption = _loaiDH == LoaiDH.PO ? "Nhà cung cấp" : "Khách hàng";
            gvMT62.Columns["TenKH"].Caption = "Tên";
            gvMT62.Columns["DiaChi"].Caption = "Địa chỉ";
            gvMT62.Columns["DienGiai"].Caption = "Diễn giải";
            gvMT62.Columns["MaNT"].Caption = "Loại tiền";
            gvMT62.Columns["TyGia"].Caption = "Tỷ giá";
            gvMT62.Columns["TtienHNT"].Visible = false;
            gvMT62.Columns["TtienH"].Visible = false;
            gvMT62.Columns["TThueNT"].Visible = false;
            gvMT62.Columns["TThue"].Visible = false;
            gvMT62.Columns["Ttien"].Caption = "Tổng tiền";
            gvMT62.Columns["TtienNT"].Caption = "Tổng tiền nguyên tệ";
            gvMT62.Columns["TinhTrang"].Visible = false;
            gvMT62.Columns["GhiChu"].Caption = "Ghi chú";
            gvMT62.Columns["NguoiLap"].Caption = "Người lập";
            gvMT62.Columns["NgayDuyet"].Visible = false;
            gvMT62.Columns["NguoiDuyet"].Visible = false;
            gvMT62.Columns["DKTT"].Caption = "Điều khoản thanh toán";
            gvMT62.Columns["HanTT"].Caption = "Hạn thanh toán";
            gvMT62.Columns["NgayGiaoHang"].Caption = "Ngày giao hàng";
            gvMT62.Columns["PTVC"].Caption = "Phương thức vận chuyển";
            gvMT62.Columns["Stt"].Visible = false;
            gvMT62.Columns["SoPhieuNguon"].Caption = _loaiDH == LoaiDH.PO ? "Phiếu yêu cầu" : "Phiếu chào giá";
            for (int i = 0; i < gvMT62.Columns.Count; i++)
            {
                if (gvMT62.Columns[i].ColumnType == typeof(Decimal) ||
                    gvMT62.Columns[i].ColumnType == typeof(Int32))
                {
                    gvMT62.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvMT62.Columns[i].DisplayFormat.FormatString = "### ### ##0.######";
                }
                if (gvMT62.Columns[i].ColumnType == typeof(DateTime))
                {
                    gvMT62.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    gvMT62.Columns[i].DisplayFormat.FormatString = "dd/MM/yyyy";
                }
            }

            gvDT62.Columns[t.Replace("MT", "DT")].Visible = false;
            gvDT62.Columns["MaVT"].Caption = "Mã vật tư";
            gvDT62.Columns["TenVT"].Caption = "Tên vật tư";
            gvDT62.Columns["MaVT"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvDT62.Columns["TenVT"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvDT62.Columns[t].Visible = false;
            gvDT62.Columns["MaDVT"].Caption = "Đơn vị tính";
            gvDT62.Columns["SLDatHang"].Caption = "SL đặt hàng";
            gvDT62.Columns["SLGiaoHang"].Caption = "SL đã giao";
            gvDT62.Columns["GiaNT"].Caption = "Giá nguyên tệ";
            gvDT62.Columns["Gia"].Caption = "Giá";
            gvDT62.Columns["PsNT"].Caption = "Thành tiền nguyên tệ";
            gvDT62.Columns["Ps"].Caption = "Thành tiền";
            gvDT62.Columns["LoaiThue"].Caption = "Loại thuế";
            gvDT62.Columns["ThueSuat"].Caption = "Thuế suất";
            gvDT62.Columns["ThueNT"].Caption = "Tiền thuế nguyên tệ";
            gvDT62.Columns["Thue"].Caption = "Tiền thuế";
            gvDT62.Columns["MaBP"].Visible = false;
            gvDT62.Columns["MaPhi"].Visible = false;
            gvDT62.Columns["MaVV"].Visible = false;
            gvDT62.Columns["MaCongTrinh"].Visible = false;
            gvDT62.Columns["NgayGiaoHangCT"].Caption = "Ngày giao hàng";
            gvDT62.Columns["QuyCach"].Visible = false;
            gvDT62.Columns["NhanHieu"].Visible = false;
            gvDT62.Columns["Stt"].Visible = false;
            gvDT62.Columns["SLConLai"].Caption = "SL chưa giao";
            for (int i = 0; i < gvDT62.Columns.Count; i++)
            {
                if (gvDT62.Columns[i].ColumnType == typeof(Decimal) ||
                    gvDT62.Columns[i].ColumnType == typeof(Int32))
                {
                    gvDT62.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvDT62.Columns[i].DisplayFormat.FormatString = "### ### ##0.######";
                }
                if (gvDT62.Columns[i].ColumnType == typeof(DateTime))
                {
                    gvDT62.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    gvDT62.Columns[i].DisplayFormat.FormatString = "dd/MM/yyyy";
                }
            }

            gvHDMCT.Columns["SoDonHang"].Visible = false;
            gvHDMCT.Columns["NgayCT"].Caption = "Ngày chứng từ";
            gvHDMCT.Columns["SoCT"].Caption = "Số chứng từ";
            gvHDMCT.Columns["NgayHd"].Caption = "Ngày hóa đơn";
            gvHDMCT.Columns["SoHoaDon"].Caption = "Số hóa đơn";
            gvHDMCT.Columns["NgayHd"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvHDMCT.Columns["SoHoaDon"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvHDMCT.Columns["MaKH"].Caption = _loaiDH == LoaiDH.PO ? "Nhà cung cấp" : "Khách hàng";
            gvHDMCT.Columns["TenKH"].Caption = "Tên";
            gvHDMCT.Columns["DienGiai"].Caption = "Diễn giải";
            gvHDMCT.Columns["MaNT"].Caption = "Loại tiền";
            gvHDMCT.Columns["TyGia"].Caption = "Tỷ giá";
            gvHDMCT.Columns["HanTT"].Caption = "Hạn thanh toán";
            gvHDMCT.Columns["MaVT"].Caption = "Mã vật tư";
            gvHDMCT.Columns["TenVT"].Caption = "Tên vật tư";
            gvHDMCT.Columns["MaVT"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvHDMCT.Columns["TenVT"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvHDMCT.Columns["MaDVT"].Caption = "Đơn vị tính";
            gvHDMCT.Columns["MaKho"].Caption = "Kho hàng";
            gvHDMCT.Columns["SoLuong"].Caption = "Số lượng";
            gvHDMCT.Columns["GiaNT"].Caption = "Giá nguyên tệ";
            gvHDMCT.Columns["Gia"].Caption = "Giá";
            gvHDMCT.Columns["PsNT"].Caption = "Tiền nguyên tệ";
            gvHDMCT.Columns["Ps"].Caption = "Thành tiền";
            if (gvHDMCT.Columns.ColumnByFieldName("CPCtNT") != null)
                gvHDMCT.Columns["CPCtNT"].Caption = "Chi phí nguyên tệ";
            if (gvHDMCT.Columns.ColumnByFieldName("CPCt") != null)
                gvHDMCT.Columns["CPCt"].Caption = "Chi phí";
            if (gvHDMCT.Columns.ColumnByFieldName("TPsNTCP") != null)
                gvHDMCT.Columns["TPsNTCP"].Caption = "Tổng tiền nguyên tệ";
            if (gvHDMCT.Columns.ColumnByFieldName("TPsCP") != null)
                gvHDMCT.Columns["TPsCP"].Caption = "Tổng tiền";
            for (int i = 0; i < gvHDMCT.Columns.Count; i++)
            {
                if (gvHDMCT.Columns[i].ColumnType == typeof(Decimal) ||
                    gvHDMCT.Columns[i].ColumnType == typeof(Int32))
                {
                    gvHDMCT.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvHDMCT.Columns[i].DisplayFormat.FormatString = "### ### ##0.######";
                }
                if (gvHDMCT.Columns[i].ColumnType == typeof(DateTime))
                {
                    gvHDMCT.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    gvHDMCT.Columns[i].DisplayFormat.FormatString = "dd/MM/yyyy";
                }
            }
            
            gvTienDoNhanHang.Columns["ID"].Visible = false;
            gvTienDoNhanHang.Columns[t.Replace("MT", "DT")].Visible = false;
            gvTienDoNhanHang.Columns["TenVT"].Caption = "Tên vật tư";
            gvTienDoNhanHang.Columns["TenVT"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gvTienDoNhanHang.Columns[t].Visible = false;
            gvTienDoNhanHang.Columns["MaDVT"].Caption = "Đơn vị tính";
            gvTienDoNhanHang.Columns["SLConLai"].Caption = "SL chưa giao";
            gvTienDoNhanHang.Columns["SLGiaoHang"].Caption = "SL giao";
            gvTienDoNhanHang.Columns["NgayGiao"].Caption = "Ngày giao";
            if (gvTienDoNhanHang.Columns.ColumnByFieldName("NoiGiao") != null)
                gvTienDoNhanHang.Columns["NoiGiao"].Caption = "Nơi giao";
            gvTienDoNhanHang.Columns["NguoiGiao"].Caption = "Người giao";
            gvTienDoNhanHang.Columns["NguoiNhan"].Caption = "Người nhận";
            gvTienDoNhanHang.Columns["NgayNhan"].Caption = "Ngày nhận";
            gvTienDoNhanHang.Columns["XeVC"].Caption = "Xe vận chuyển";
            gvTienDoNhanHang.Columns["TinhTrang"].Visible = false;
            gvTienDoNhanHang.Columns["TenTT"].Caption = "Tình trạng";
            for (int i = 0; i < gvTienDoNhanHang.Columns.Count; i++)
            {
                if (gvTienDoNhanHang.Columns[i].ColumnType == typeof(Decimal) ||
                    gvTienDoNhanHang.Columns[i].ColumnType == typeof(Int32))
                {
                    gvTienDoNhanHang.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvTienDoNhanHang.Columns[i].DisplayFormat.FormatString = "### ### ##0.######";
                }
                if (gvTienDoNhanHang.Columns[i].ColumnType == typeof(DateTime))
                {
                    gvTienDoNhanHang.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    gvTienDoNhanHang.Columns[i].DisplayFormat.FormatString = "dd/MM/yyyy";
                }
            }
        }

        void gvMT62_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            object o = gvMT62.GetFocusedRowCellValue("TinhTrang");
            if (o == null || o.ToString() == "")
                return;
            int i = Int32.Parse(o.ToString());
            if (i == 1 || i == 5)
                rgPheDuyet.SelectedIndex = 1;
            else
                if (i == 3 || i == 4)
                    rgPheDuyet.SelectedIndex = 2;
                else
                    rgPheDuyet.SelectedIndex = 0;
        }

        private void gluMT61_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                (sender as GridLookUpEdit).EditValue = null;
        }

        private void lueTinhTrang_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && !(sender as LookUpEdit).Properties.ReadOnly)
                (sender as LookUpEdit).EditValue = null;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataSet ds = TimKiem();
            HienThiSoLieu(ds);
            if (_first)
                DinhDangSoLieu();
            _first = false;
            gvMT62.BestFitColumns();
            gvDT62.BestFitColumns();
            gvHDMCT.BestFitColumns();
            gvTienDoNhanHang.BestFitColumns();
        }

        private void rgPheDuyet_MouseDown(object sender, MouseEventArgs e)
        {
            if (gvMT62.DataRowCount == 0)
                return;
            if (rgPheDuyet.SelectedIndex == 1)
            {
                if (gvHDMCT.DataRowCount > 0)
                {
                    XtraMessageBox.Show("Đã xuất hóa đơn cho đơn hàng này, không thể thay đổi thông tin phê duyệt!");
                    return;
                }
                if (gvTienDoNhanHang.DataRowCount > 0)
                {
                    XtraMessageBox.Show("Đã tiến hành giao nhận hàng cho đơn hàng này, không thể thay đổi thông tin phê duyệt!");
                    return;
                }
            }
            btnUpdate.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (gvMT62.FocusedRowHandle < 0)
                return;
            if (rgPheDuyet.SelectedIndex == 1)
            {
                if (lueNguoiDuyet.EditValue == DBNull.Value)
                {
                    XtraMessageBox.Show("Vui lòng nhập thông tin người phê duyệt!");
                    return;
                }
                if (deNgayDuyet.EditValue == DBNull.Value)
                {
                    XtraMessageBox.Show("Vui lòng nhập thông tin ngày phê duyệt!");
                    return;
                }
            }
            string t = _loaiDH == LoaiDH.PO ? "MT62" : "MT64";
            string id = gvMT62.GetFocusedRowCellValue(t + "ID").ToString();
            int TT = rgPheDuyet.SelectedIndex == 0 ? 2 : (rgPheDuyet.SelectedIndex == 1 ? 1 : 3);
            string sql = "update " + t + " set TinhTrang = " + TT.ToString();
            if (deNgayDuyet.EditValue == DBNull.Value)
                sql += ", NgayDuyet = null";
            else
                sql += ", NgayDuyet = '" + deNgayDuyet.DateTime.ToString("MM/dd/yyyy") + "'";
            if (lueNguoiDuyet.EditValue == DBNull.Value)
                sql += ", NguoiDuyet = null";
            else
                sql += ", NguoiDuyet = '" + lueNguoiDuyet.EditValue.ToString() + "'";
            if (teGhiChu.EditValue == DBNull.Value)
                sql += ", GhiChu = null";
            else
                sql += ", GhiChu = N'" + teGhiChu.Text + "'";
            sql += " where " + t + "ID = '" + id + "'";
            if (_db.UpdateByNonQuery(sql))
            {
                lueTinhTrang1.EditValue = TT;
                btnUpdate.Enabled = false;
                XtraMessageBox.Show("Đã cập nhật thông tin phê duyệt đơn hàng!");
            }
        }
    }
}