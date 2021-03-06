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

namespace TraHDB
{
    public class TraHDB : ICForm
    {
        private List<InfoCustomForm> _lstInfo = new List<InfoCustomForm>(); //chứa thông tin của plugin
        private DataCustomFormControl _data;    //chứa các dữ liệu cần thiết của màn hình nhập liệu mà plugin đang chạy

        public TraHDB()
        {
            InfoCustomForm info = new InfoCustomForm(IDataType.MasterDetailDt, 1002, "Xem hóa đơn để lấy thông tin nhập hàng bán bị trả lại",
                "View invoices to get data for return goods", "MT33");  //khai báo thông tin của plugin
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
            if (menuID == _lstInfo[0].MenuID)   //nếu plugin này chỉ có 1 menu chức năng thì không cần dòng này
            {
                //hàm dùng để lấy grid view trên màn hình mà plugin đang chạy
                //nếu màn hình có 2 grid thì grid trên là gcMain, grid dưới là gcDetail
                //nếu màn hình có 1 grid thì tên là gcMain
                GridView gvDetail = (_data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl).MainView as GridView;
                if (_data.BsMain.Current == null)   //dòng đang chọn của bindingsource chính
                {
                    string msg = "Chưa đủ số liệu để tra hóa đơn";
                    if (Config.GetValue("Language").ToString() == "1")  //dùng để chuyển ngôn ngữ nếu ngôn ngữ đang chọn không phải là tiếng việt
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
                    if (drCurDetail == null || drCurDetail["MaKho"].ToString() == "")
                        gvDetail.AddNewRow();
                    gvDetail.SetFocusedRowCellValue(gvDetail.Columns["DGQuyDoi"], frm.DrCurPhieuNhap["GiaVon"]);
                    gvDetail.SetFocusedRowCellValue(gvDetail.Columns["GiaVon"], frm.DrCurPhieuNhap["GiaVon"]);
                    gvDetail.SetFocusedRowCellValue(gvDetail.Columns["GiaNT"], frm.DrCurPhieuNhap["GiaNT"]);
                    gvDetail.SetFocusedRowCellValue(gvDetail.Columns["Gia"], frm.DrCurPhieuNhap["Gia"]);
                    gvDetail.SetFocusedRowCellValue(gvDetail.Columns["MaKho"], frm.DrCurPhieuNhap["MaKho"]);
                    gvDetail.SetFocusedRowCellValue(gvDetail.Columns["MaVT"], frm.DrCurPhieuNhap["MaVT"]);
                }
            }
        }
    }
}
