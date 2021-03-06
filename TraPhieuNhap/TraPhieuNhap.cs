using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraLayout;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using Plugins;
using System.Data;
using CDTLib;

namespace TraPhieuNhap
{
    public class TraPhieuNhap : ICForm
    {
        private List<InfoCustomForm> _lstInfo = new List<InfoCustomForm>();
        private DataCustomFormControl _data;

        public TraPhieuNhap()
        {
            InfoCustomForm info = new InfoCustomForm(IDataType.MasterDetailDt, 1001, "Xem phiếu nhập để lấy thông tin xuất hàng",
                "View receip of storing to get data for delivering", "MT32,MT43,MT44,MT45");
            _lstInfo.Add(info);
        }

        #region ICForm Members

        public DataCustomFormControl Data
        {
            set { _data = value; }
        }

        public List<InfoCustomForm> LstInfo
        {
            get { return _lstInfo; }
        }

        #endregion

        public void Execute(int menuID)
        {
            if (menuID == _lstInfo[0].MenuID)
            {
                GridView gvDetail = (_data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl).MainView as GridView;
                if (_data.BsMain.Current == null)
                {
                    string msg = "Chưa đủ số liệu để tra phiếu nhập";
                    if (Config.GetValue("Language").ToString() == "1")
                        msg = UIDictionary.Translate(msg);
                    XtraMessageBox.Show(msg);
                    return;
                }
                DataRow drCurMaster = (_data.BsMain.Current as DataRowView).Row;
                DataRow drCurDetail = gvDetail.GetDataRow(gvDetail.FocusedRowHandle);
                if (drCurMaster["NgayCT"].ToString() == "")
                {
                    string msg = "Cần có ngày chứng từ để tra phiếu nhập";
                    if (Config.GetValue("Language").ToString() == "1")
                        msg = UIDictionary.Translate(msg);
                    XtraMessageBox.Show(msg);
                    return;
                }
                FrmPhieuNhap frm = new FrmPhieuNhap(drCurMaster, drCurDetail);
                frm.ShowDialog();
                if (frm.DrCurPhieuNhap != null)
                {
                    if (_data.DrTableMaster["TableName"].ToString() == "MT32")
                        gvDetail.SetFocusedRowCellValue(gvDetail.Columns["GiaVon"], frm.DrCurPhieuNhap["DonGia"]);
                    else
                    {
                        gvDetail.SetFocusedRowCellValue(gvDetail.Columns["GiaNT"], frm.DrCurPhieuNhap["DonGiaNT"]);
                        gvDetail.SetFocusedRowCellValue(gvDetail.Columns["Gia"], frm.DrCurPhieuNhap["DonGia"]);
                    }
                    if (drCurDetail == null || (drCurDetail.Table.Columns.Contains("MaKho") && drCurDetail["MaKho"].ToString() == ""))
                        gvDetail.SetFocusedRowCellValue(gvDetail.Columns["MaKho"], frm.DrCurPhieuNhap["MaKho"]);
                    if (drCurDetail == null || drCurDetail["MaVT"].ToString() == "")
                        gvDetail.SetFocusedRowCellValue(gvDetail.Columns["MaVT"], frm.DrCurPhieuNhap["MaVT"]);
                }
            }
        }
    }
}
