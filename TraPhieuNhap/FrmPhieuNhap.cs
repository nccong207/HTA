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

namespace TraPhieuNhap
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
            if (Config.GetValue("Language").ToString() != "0")
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
            string sql = "select * from (select 'DKNTXT' as MaCT, [SoCT], [NgayCT], [MaKho], [MaVT], [SoLuong], DuDauNT/SoLuong as DonGiaNT, DuDau/SoLuong as DonGia, [DuDauNT] as PsNoNT, [DuDau] as PsNo from OBNTXT union all ";
            sql += "select 'DKBQ', '', '', [MaKho], [MaVT], [SoLuong], DuDauNT/SoLuong as DonGiaNT, DuDau/SoLuong as DonGia, [DuDauNT], [DuDau] from OBVT union all ";
            sql += "select MaCT, [SoCT], [NgayCT], [MaKho], [MaVT], [SoLuong], DonGiaNT, DonGia, PsNoNT, PsNo " +
                " from BLVT where SoLuong > 0 and NgayCT <= '" + _drCurMaster["NgayCT"].ToString() + "'" +
                " and year(NgayCT) = year('" + _drCurMaster["NgayCT"].ToString() + "')) x";
            string tmp = " where ";
            if (_drCurDetail != null)
            {
                if (_drCurDetail.Table.Columns.Contains("MaKho") && _drCurDetail["MaKho"].ToString() != "")
                    tmp += "MaKho = '" + _drCurDetail["MaKho"].ToString() + "'";
                if (_drCurMaster.Table.Columns.Contains("MaKho") && _drCurMaster["MaKho"].ToString() != "")
                    tmp += "MaKho = '" + _drCurMaster["MaKho"].ToString() + "'";
                if (_drCurDetail["MaVT"].ToString() != "")
                {
                    if (tmp != " where ")
                        tmp += " and ";
                    tmp += "MaVT = '" + _drCurDetail["MaVT"].ToString() + "'";
                }
            }
            if (tmp != " where ")
                sql += tmp;
            sql += " order by NgayCT";
            gcPN.DataSource = _dbData.GetDataTable(sql);
            gvPN.ExpandAllGroups();
            gvPN.BestFitColumns();
        }
    }
}