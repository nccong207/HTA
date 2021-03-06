using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using DevExpress;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using Plugins;
using CDTDatabase;

namespace ChinhSachGia
{
    public class ChinhSachGia:ICForm
    {
        private List<InfoCustomForm> _lstInfo = new List<InfoCustomForm>();
        private DataCustomFormControl _data;

        #region ICForm Members
       
         public ChinhSachGia()
        {
            InfoCustomForm info = new InfoCustomForm(IDataType.MasterDetailDt, 1002, "Lấy danh sách vật tư",
                "", "ChinhSachGia");
            _lstInfo.Add(info);
            info = new InfoCustomForm(IDataType.MasterDetailDt, 1003, "Lấy danh sách khách hàng",
               "", "ChinhSachGia");
            _lstInfo.Add(info);
        }

        public DataCustomFormControl Data
        {
            set { _data=value; }
        }

        public void Execute(int menuID)
        {
            if (menuID == _lstInfo[0].MenuID)
            {
                GridView gvDetail = (_data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl).MainView as GridView;
                string mavt = string.Empty;
                for (int i = 0; i < gvDetail.DataRowCount; i++)
                {
                    DataRow row = gvDetail.GetDataRow(i);                    
                    mavt+="'"+row["MaVT"].ToString()+"',";
                }
                if (mavt != string.Empty)
                    mavt = mavt.Substring(0,mavt.Length-1);
                frmVatTu frm = new frmVatTu(mavt);
                frm.Text = _lstInfo[0].MenuName.ToString();
                frm.ShowDialog();                
                if (frm.dvVattu != null)
                {                    
                    foreach (DataRowView drv in frm.dvVattu)
                    {
                        gvDetail.AddNewRow();
                        gvDetail.UpdateCurrentRow();
                        gvDetail.SetFocusedRowCellValue(gvDetail.Columns["MaVT"], drv.Row["MaVT"].ToString());
                        if (drv.Row["GiaBanNT"].ToString() != "" || drv.Row["GiaBanNT"] !=null)
                            gvDetail.SetFocusedRowCellValue(gvDetail.Columns["GiaBanGocNT"], drv.Row["GiaBanNT"].ToString());
                        if (drv.Row["GiaBan"].ToString() != "" || drv.Row["GiaBan"] != null)
                            gvDetail.SetFocusedRowCellValue(gvDetail.Columns["GiaBanGoc"], drv.Row["GiaBan"].ToString());
                    }
                }
            }
            else
            {
                GridView gvDetail = (_data.FrmMain.Controls.Find("GiaKH", true)[0] as GridControl).MainView as GridView;
                string makh = string.Empty;
                for (int i = 0; i < gvDetail.DataRowCount; i++)
                {
                    DataRow row = gvDetail.GetDataRow(i);                    
                    makh += "'" + row["MaKH"].ToString() + "',";
                }
                if (makh != string.Empty)
                    makh = makh.Substring(0, makh.Length - 1);
                frmKhachHang frm = new frmKhachHang(makh);
                frm.Text = _lstInfo[1].MenuName.ToString();
                frm.ShowDialog();                
                if (frm.dvKh != null)
                {                    
                    foreach (DataRowView drv in frm.dvKh)
                    {
                        gvDetail.AddNewRow();
                        gvDetail.UpdateCurrentRow();
                        gvDetail.SetFocusedRowCellValue(gvDetail.Columns["NhomKH"], drv.Row["Nhom1"].ToString());
                        gvDetail.SetFocusedRowCellValue(gvDetail.Columns["MaKH"], drv.Row["MaKH"].ToString());
                        gvDetail.SetFocusedRowCellValue(gvDetail.Columns["TenKH"], drv.Row["TenKH"].ToString());
                    }
                }
            }
        }

        public List<InfoCustomForm> LstInfo
        {
            get { return _lstInfo; }
        }

        #endregion
    }
}
