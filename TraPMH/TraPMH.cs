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

namespace TraPMH
{
    public class TraPMH : ICForm
    {
        private List<InfoCustomForm> _lstInfo = new List<InfoCustomForm>();
        private DataCustomFormControl _data;

        public TraPMH()
        {
            InfoCustomForm info = new InfoCustomForm(IDataType.MasterDetailDt, 1002, "Xem hóa đơn mua để lấy thông tin xuất hàng trả lại", 
                "View purchasing invoices to get data for delivering", "MT24");
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
                    string msg = "Chưa đủ số liệu để tra hóa đơn";
                    if (Config.GetValue("Language").ToString() == "1")
                        msg = UIDictionary.Translate(msg);
                    XtraMessageBox.Show(msg);
                    return;
                }
                DataRow drCurMaster = (_data.BsMain.Current as DataRowView).Row;
                DataRow drCurDetail = gvDetail.GetDataRow(gvDetail.FocusedRowHandle);
                if (drCurMaster["NgayCT"].ToString() == "")
                {
                    string msg = "Cần có ngày chứng từ để tra hóa đơn";
                    if (Config.GetValue("Language").ToString() == "1")
                        msg = UIDictionary.Translate(msg);
                    XtraMessageBox.Show(msg);
                    return;
                }
                if (drCurMaster["MaKH"].ToString() == "")
                {
                    string msg = "Cần có khách hàng để tra hóa đơn";
                    if (Config.GetValue("Language").ToString() == "1")
                        msg = UIDictionary.Translate(msg);
                    XtraMessageBox.Show(msg);
                    return;
                }
                FrmPhieuNhap frm = new FrmPhieuNhap(drCurMaster, drCurDetail);
                frm.ShowDialog();
                if (frm.DrCurPhieuNhap != null)
                {
                    gvDetail.SetFocusedRowCellValue(gvDetail.Columns["GiaNT"], frm.DrCurPhieuNhap["GiaNT"]);
                    gvDetail.SetFocusedRowCellValue(gvDetail.Columns["Gia"], frm.DrCurPhieuNhap["Gia"]);
                    if (drCurDetail == null || drCurDetail["MaKho"].ToString() == "")
                        gvDetail.SetFocusedRowCellValue(gvDetail.Columns["MaKho"], frm.DrCurPhieuNhap["MaKho"]);
                    if (drCurDetail == null || drCurDetail["MaVT"].ToString() == "")
                        gvDetail.SetFocusedRowCellValue(gvDetail.Columns["MaVT"], frm.DrCurPhieuNhap["MaVT"]);
                }
            }
        }
    }
}
