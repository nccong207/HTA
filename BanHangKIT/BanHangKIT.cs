using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using DevExpress.XtraTab;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using System.Data;
using CDTDatabase;

namespace BanHangKIT
{
    public class BanHangKIT : ICControl
    {
        private DataCustomFormControl _data;
        private InfoCustomControl _info = new InfoCustomControl(IDataType.MasterDetailDt);
        #region ICControl Members

        public void AddEvent()
        {
            XtraTabControl tcMain = _data.FrmMain.Controls.Find("tcMain", true)[0] as XtraTabControl;
            tcMain.SelectedPageChanged += new TabPageChangedEventHandler(tcMain_SelectedPageChanged);
        }

        void tcMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            XtraTabControl tcMain = sender as XtraTabControl;
            if (tcMain.TabPages[0] != e.Page)
                return;
            GridView gvKIT = (_data.FrmMain.Controls.Find("DT32KIT", true)[0] as GridControl).MainView as GridView;
            if (gvKIT.OptionsBehavior.Editable && gvKIT.DataRowCount > 0)
            {
                GridView gvDetail = (_data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl).MainView as GridView;
                if (gvDetail.DataRowCount > 0)
                {
                    if (XtraMessageBox.Show("Bạn có muốn chuyển thông tin hàng bán theo bộ thành chi tiết hàng xuất bán không?\n(Chi tiết hàng xuất bán hiện tại sẽ bị xóa đi trước khi chuyển lại!)", "Xác nhận", MessageBoxButtons.YesNo)
                        == DialogResult.No)
                        return;
                    for (int i = gvDetail.DataRowCount - 1; i >= 0; i--)
                        gvDetail.DeleteRow(i);
                }
                Database db = Database.NewDataDatabase();
                for (int i = 0; i < gvKIT.DataRowCount; i++)
                {
                    DataRow drKIT = gvKIT.GetDataRow(i);
                    string makit = drKIT["MaKIT"].ToString();
                    string sql = "select * from DMKITCT where MaKIT = N'" + makit + "'";
                    DataTable dt = db.GetDataTable(sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        decimal sl = Decimal.Parse(drKIT["SoLuong1"].ToString());
                        foreach (DataRow drVT in dt.Rows)
                        {
                            gvDetail.AddNewRow();
                            gvDetail.UpdateCurrentRow();
                            gvDetail.SetFocusedRowCellValue(gvDetail.Columns["MaVT"], drVT["MaVT"]);
                            gvDetail.SetFocusedRowCellValue(gvDetail.Columns["TenVT"], drVT["TenVT"]);
                            gvDetail.SetFocusedRowCellValue(gvDetail.Columns["MaDVT"], drVT["DVT"]);
                            decimal slKIT = Decimal.Parse(drVT["SoLuong"].ToString());
                            gvDetail.SetFocusedRowCellValue(gvDetail.Columns["SoLuong"], sl * slKIT);
                            gvDetail.SetFocusedRowCellValue(gvDetail.Columns["MaCongTrinh"], drKIT["MaCongTrinh"]);
                            gvDetail.SetFocusedRowCellValue(gvDetail.Columns["MaBP"], drKIT["MaBP"]);
                            gvDetail.SetFocusedRowCellValue(gvDetail.Columns["MaPhi"], drKIT["MaPhi"]);
                            gvDetail.SetFocusedRowCellValue(gvDetail.Columns["MaVV"], drKIT["MaVV"]);
                        }
                    }
                }
            }
        }

        public DataCustomFormControl Data
        {
            set { _data = value; }
        }

        public InfoCustomControl Info
        {
            get { return _info; }
        }

        #endregion
    }
}
