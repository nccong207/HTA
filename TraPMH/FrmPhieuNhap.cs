using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using CDTDatabase;
using CDTLib;

namespace TraPMH
{
    public partial class FrmPhieuNhap : DevExpress.XtraEditors.XtraForm
    {
        public DataRow DrCurPhieuNhap;
        private DataRow _drCurMaster;
        private DataRow _drCurDetail;
        private Database _dbStruct = Database.NewStructDatabase();
        private Database _dbData = Database.NewDataDatabase();

        public FrmPhieuNhap(DataRow drCurMaster, DataRow drCurDetail)
        {
            InitializeComponent();
            _drCurDetail = drCurDetail;
            _drCurMaster = drCurMaster;
        }

        private void FrmPhieuNhap_Load(object sender, EventArgs e)
        {
            //InitStruct();
            InitData();
            if (Config.GetValue("Language").ToString() == "1")
                FormFactory.DevLocalizer.Translate(this);
        }

        private void FrmPhieuNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void FrmPhieuNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (gvPN.DataRowCount > 0 && gvPN.IsDataRow(gvPN.FocusedRowHandle))
                DrCurPhieuNhap = gvPN.GetDataRow(gvPN.FocusedRowHandle);
        }

        private void InitData()
        {
            string sql = "select * from (SELECT MaCT, [SoCT], [SoHoaDon], [NgayCT], [MaKH], [DienGiai], [MaNT], [TyGia], MaVT, MaKho, SoLuong, Gia, GiaNT, Ps, PsNT " +
                    "FROM [MT22] inner join [DT22] on MT22.MT22ID = DT22.MT22ID " +
                    "union all " +
                    "SELECT MaCT, [SoCT], [SoHoaDon], [NgayCT], [MaKH], [DienGiai], [MaNT], [TyGia], MaVT, MaKho, SoLuong, Gia, GiaNT, Ps, PsNT " +
                    "FROM [MT23] inner join [DT23] on MT23.MT23ID = DT23.MT23ID) x";
            sql += " where MaKH = '" + _drCurMaster["MaKH"].ToString() + "'" + 
                " and NgayCT <= '" + _drCurMaster["NgayCT"].ToString() + "'" +
                " and year(NgayCT) = year('" + _drCurMaster["NgayCT"].ToString() + "')";
            if (_drCurDetail != null)
            {
                if (_drCurDetail["MaKho"].ToString() != "")
                    sql += " and MaKho = '" + _drCurDetail["MaKho"].ToString() + "'";
                if (_drCurDetail["MaVT"].ToString() != "")
                    sql += " and MaVT = '" + _drCurDetail["MaVT"].ToString() + "'";
            }
            sql += " order by NgayCT";
            gcPN.DataSource = _dbData.GetDataTable(sql);
            gvPN.ExpandAllGroups();
            gvPN.BestFitColumns();
        }
    }
}