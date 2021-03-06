using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using CDTDatabase;
using CDTLib;
using DevExpress;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;
using System.Data;
using DevExpress.Utils;

namespace LapChungTu
{
    public class LapChungTu:ICControl
    {
        private DataCustomFormControl data;
        private InfoCustomControl info = new InfoCustomControl(IDataType.MasterDetailDt);
        Database db = Database.NewDataDatabase();        

        #region ICControl Members

        public void AddEvent()
        {
            if (data.DrTable.Table.Columns.Contains("ExtraSql")
                && data.DrTable["ExtraSql"].ToString().ToUpper() == "REFVALUE LIKE 'CPL_%'")
            {            
                data.FrmMain.Shown += new EventHandler(FrmMain_Shown);
            }
        }

        //Tạo bảng dữ liệu
        DataTable createTable(string id)
        {
            DataTable dtChungTu = new DataTable();
            dtChungTu.Columns.Add("DienGiai");
            dtChungTu.Columns.Add("TKNo");
            dtChungTu.Columns.Add("TKCo");
            dtChungTu.Columns.Add("SoTien");
            dtChungTu.AcceptChanges();

            //Khai bao
            DataTable dt = db.GetDataTable("select * from BangLuong Where IDBL='" + id + "'");
            DataRow row;
            double TongBHXH = 0;
            double TongBHTN = 0;
            double TongBHYT = 0;
            double TongTNCN = 0;
            double TongLuong = 0;
            string sql = "";
            if (dt.Rows.Count > 0)
            {
                row = dt.Rows[0];
                TongBHXH = Convert.ToDouble(row["TBHXH"].ToString());
                TongBHTN = Convert.ToDouble(row["TBHTN"].ToString());
                TongBHYT = Convert.ToDouble(row["TBHYT"].ToString()); 
                TongTNCN = Convert.ToDouble(row["TongThueTNCN"].ToString());
            }
            DataRow newRow;
            //BHXH Nhan vien
            if (TongBHXH > 0)
            {
                newRow = dtChungTu.NewRow();
                newRow["DienGiai"] = "BHXH nhân viên đóng";
                newRow["TKNo"] = "3341";
                newRow["TKCo"] = "3383";
                newRow["SoTien"] = TongBHXH.ToString();
                dtChungTu.Rows.Add(newRow);
            }
            //lay cac so lieu can thiet theo tai khoan chi phi cac phong ban
            sql = " select PB.TKCP,sum(BLCT.BHXH) as BHXH, sum(BLCT.BHTN) as BHTN, sum(BLCT.BHYT) as BHYT, sum(BLCT.TongLuong) as TongLuong " +
                " from BangLuongCT BLCT inner join DMNhanVien NV on NV.MaNV=BLCT.MaNV " +
                " inner join DMBoPhan PB on PB.MaBP=NV.PhongBan where IDBL='" + id + "' group by PhongBan,PB.TKCP ";
            dt = db.GetDataTable(sql);
            //BHXH Cong ty
            foreach (DataRow dr in dt.Rows)
            {
                double bhxh = Convert.ToDouble(dr["BHXH"].ToString());
                if (bhxh == 0)
                    continue;
                newRow = dtChungTu.NewRow();
                newRow["DienGiai"] = "BHXH công ty đóng";
                newRow["TKNo"] = dr["TKCP"].ToString();
                newRow["TKCo"] = "3383";
                newRow["SoTien"] = Math.Round(bhxh * 18 / 8);
                dtChungTu.Rows.Add(newRow);
            }
            
            //BHTN Nhan vien
            if (TongBHTN > 0)
            {
                newRow = dtChungTu.NewRow();
                newRow["DienGiai"] = "BHTN nhân viên đóng";
                newRow["TKNo"] = "3341";
                newRow["TKCo"] = "3389";
                newRow["SoTien"] = TongBHTN.ToString();
                dtChungTu.Rows.Add(newRow);
            }

            //BHTN Cong ty
            foreach (DataRow dr in dt.Rows)
            {
                double bhtn = Convert.ToDouble(dr["BHTN"].ToString());
                if (bhtn == 0)
                    continue;
                newRow = dtChungTu.NewRow();
                newRow["DienGiai"] = "BHTN công ty đóng";
                newRow["TKNo"] = dr["TKCP"].ToString();
                newRow["TKCo"] = "3389";
                newRow["SoTien"] = Convert.ToDouble(bhtn).ToString();
                dtChungTu.Rows.Add(newRow);
            }

            //BHYT Nhan vien
            if (TongBHYT > 0)
            {
                newRow = dtChungTu.NewRow();
                newRow["DienGiai"] = "BHYT nhân viên đóng";
                newRow["TKNo"] = "3341";
                newRow["TKCo"] = "3384";
                newRow["SoTien"] = TongBHYT.ToString();
                dtChungTu.Rows.Add(newRow);
            }

            //BHYT Cong ty
            foreach (DataRow dr in dt.Rows)
            {
                double bhyt = Convert.ToDouble(dr["BHYT"].ToString());
                if (bhyt == 0)
                    continue;
                newRow = dtChungTu.NewRow();
                newRow["DienGiai"] = "BHYT công ty đóng";
                newRow["TKNo"] = dr["TKCP"].ToString();
                newRow["TKCo"] = "3384";
                newRow["SoTien"] = Math.Round(bhyt * 3 / 1.5);
                dtChungTu.Rows.Add(newRow);
            }
            
            //Luong Nhan vien
            foreach (DataRow dr in dt.Rows)
            {
                double tl = Convert.ToDouble(dr["TongLuong"].ToString());
                if (tl == 0)
                    continue;
                newRow = dtChungTu.NewRow();
                newRow["DienGiai"] = "Lương nhân viên";
                newRow["TKNo"] = dr["TKCP"].ToString();
                newRow["TKCo"] = "3341";
                newRow["SoTien"] = tl;
                dtChungTu.Rows.Add(newRow);
            }
            //Thue TNCN
            if (TongTNCN > 0)
            {
                newRow = dtChungTu.NewRow();
                newRow["DienGiai"] = "Thuế TNCN phải nộp";
                newRow["TKNo"] = "3341";
                newRow["TKCo"] = "3335";
                newRow["SoTien"] = TongTNCN.ToString();
                dtChungTu.Rows.Add(newRow);
            }
            dtChungTu.AcceptChanges();
            return dtChungTu;
        }

        //Add row cho girdview
        void addRows(DataTable dt, GridView gvChiTiet)
        {
            foreach (DataRow row in dt.Rows)
            {
                gvChiTiet.AddNewRow();
                gvChiTiet.UpdateCurrentRow();
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["DienGiaiCT"], row["DienGiai"].ToString());
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["TkNo"], row["TKNo"].ToString());
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["TkCo"], row["TKCo"].ToString());
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["Ps"], row["SoTien"].ToString());                                                
            }
        }

        void setViews(GridView gvChiTiet)
        {
            gvChiTiet.Columns["DienGiaiCT"].OptionsColumn.AllowEdit=false;
            gvChiTiet.Columns["TkNo"].OptionsColumn.AllowEdit=false;
            gvChiTiet.Columns["TkCo"].OptionsColumn.AllowEdit=false;
            gvChiTiet.Columns["Ps"].OptionsColumn.AllowEdit = false;
        }

        void FrmMain_Shown(object sender, EventArgs e)
        {            
            DataRowView drv = data.BsMain.Current as DataRowView;
            if (drv.Row.RowState == DataRowState.Added)
            {
                frmBangLuong frm = new frmBangLuong();
                frm.ShowDialog();
                string id = frm.id;
                int thang = frm.thang;
                int nam = frm.nam;
                GridView gvChiTiet = (data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl).MainView as GridView;

                if (id != "" && thang!=0 && nam!=0)
                {
                    //ghi dữ liệu
                    int numberDayInMonth = DateTime.DaysInMonth(nam,thang);
                    drv["NgayCT"] = new DateTime(nam,thang,numberDayInMonth);
                    drv["RefValue"]="CPL_"+id;
                    drv["DienGiai"] = "Hạch toán chi phí lương tháng "+thang.ToString()+" năm "+nam.ToString();
                    DataTable dt = createTable(id);
                    addRows(dt, gvChiTiet);
                    setViews(gvChiTiet);
                }
            }
        }

        public DataCustomFormControl Data
        {
            set { data=value; }
        }

        public InfoCustomControl Info
        {
            get { return info; }
        }

        #endregion
    }
}
