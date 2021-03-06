using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using CDTDatabase;
using CDTLib;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using System.Data;
namespace ChungTuBHThue
{
    public class ChungTuBHThue:ICControl
    {
        private DataCustomFormControl data;
        private InfoCustomControl info = new InfoCustomControl(IDataType.MasterDetailDt);
        Database db = Database.NewDataDatabase();   

        #region ICControl Members

        public void AddEvent()
        {
            if ((data.DrTable["TableName"].ToString().ToUpper() == "DT12"
                || data.DrTable["TableName"].ToString().ToUpper() == "DT16") &&
                (data.DrTable.Table.Columns.Contains("ExtraSql")
                && data.DrTable["ExtraSql"].ToString().ToUpper() == "REFVALUE LIKE 'NBH_%'"))
            {
                data.FrmMain.Shown += new EventHandler(FrmMain_Shown);
            }
        }

        //Tạo bảng dữ liệu
        DataTable createTable(string id)
        {
            DataTable dtChiTiet = new DataTable();
            dtChiTiet.Columns.Add("DienGiai");
            dtChiTiet.Columns.Add("TKNo");
            dtChiTiet.Columns.Add("SoTien");
            dtChiTiet.AcceptChanges();

            //Khai bao
            DataTable dt = db.GetDataTable("select * from BangLuong Where IDBL='" + id + "'");
            DataRow row;
            double TongBHXH = 0;
            double TongBHTN = 0;
            double TongBHYT = 0;
            double TongTNCN = 0;
            if (dt.Rows.Count > 0)
            {
                row = dt.Rows[0];
                TongBHXH = Convert.ToDouble(row["TBHXH"].ToString());
                TongBHTN = Convert.ToDouble(row["TBHTN"].ToString());
                TongBHYT = Convert.ToDouble(row["TBHYT"].ToString());
                TongTNCN = Convert.ToDouble(row["TongThueTNCN"].ToString());                
            }
            DataRow newRow;
            //Nop BHXH 
            if (TongBHXH > 0)
            {
                newRow = dtChiTiet.NewRow();
                newRow["DienGiai"] = "Nộp bảo hiểm xã hội ";
                newRow["TKNo"] = "3383";
                newRow["SoTien"] = (Math.Round(TongBHXH * 26 / 8)).ToString();
                dtChiTiet.Rows.Add(newRow);
            }
            //Nộp BH thất nghiệp
            if (TongBHTN > 0)
            {
                newRow = dtChiTiet.NewRow();
                newRow["DienGiai"] = "Nộp bảo hiểm thất nghiệp";
                newRow["TKNo"] = "3389";
                newRow["SoTien"] = (TongBHTN * 2).ToString();
                dtChiTiet.Rows.Add(newRow);
            }
            //Nộp BHYT 
            if (TongBHYT > 0)
            {
                newRow = dtChiTiet.NewRow();
                newRow["DienGiai"] = "Nộp bảo hiểm y tế";
                newRow["TKNo"] = "3384";
                newRow["SoTien"] = (Math.Round(TongBHYT * 4.5 / 1.5)).ToString();
                dtChiTiet.Rows.Add(newRow);
            }         
            //Nộp Thue TNCN
            if (TongTNCN > 0)
            {
                newRow = dtChiTiet.NewRow();
                newRow["DienGiai"] = "Nộp thuế TNCN";
                newRow["TKNo"] = "3385";
                newRow["SoTien"] = TongTNCN.ToString();
                dtChiTiet.Rows.Add(newRow);
            }
            dtChiTiet.AcceptChanges();
            return dtChiTiet;
        }

        //Add row cho girdview
        void addRows(DataTable dt, GridView gvChiTiet)
        {
            foreach (DataRow row in dt.Rows)
            {
                gvChiTiet.AddNewRow();
                gvChiTiet.UpdateCurrentRow();
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["DienGiaiCt"], row["DienGiai"].ToString());
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["TkNo"], row["TKNo"].ToString());                
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["Ps"], row["SoTien"].ToString());
            }
        }

        //Khong cho edit
        void setViews(GridView gvChiTiet)
        {
            gvChiTiet.Columns["DienGiaiCt"].OptionsColumn.AllowEdit = false;
            gvChiTiet.Columns["TkNo"].OptionsColumn.AllowEdit = false;            
            gvChiTiet.Columns["Ps"].OptionsColumn.AllowEdit = false;
        }

        void FrmMain_Shown(object sender, EventArgs e)
        {
            DataRowView drv = data.BsMain.Current as DataRowView;
            string value = data.DrTable["ExtraSql"].ToString();
            if ((data.DrTable["TableName"].ToString().ToUpper() == "DT12" || data.DrTable["TableName"].ToString().ToUpper() == "DT16") && data.DrTable["ExtraSql"].ToString().ToUpper() == "REFVALUE LIKE 'NBH_%'")
            {
                if (drv.Row.RowState == DataRowState.Added)
                {
                    frmBangLuong frm = new frmBangLuong();
                    frm.ShowDialog();
                    string id = frm.id;
                    int thang = frm.thang;
                    int nam = frm.nam;                    
                    GridView gvChiTiet = (data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl).MainView as GridView;

                    if (id != "" && thang != 0 && nam != 0)
                    {
                        //ghi dữ liệu Master
                        int numberDayInMonth = DateTime.DaysInMonth(nam, thang);
                        drv["NgayCT"] = new DateTime(nam, thang, numberDayInMonth);
                        drv["RefValue"] = "NBH_" + id;
                        drv["DienGiai"] = "Nộp bảo hiểm và thuế TNCN tháng " + thang.ToString() + " năm " + nam.ToString();
                        DataTable dt = createTable(id);
                        addRows(dt,gvChiTiet);
                        setViews(gvChiTiet);
                    }
                }
                //else
                //{
                //    GridView gvDetail = (data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl).MainView as GridView;
                //    if (gvDetail.OptionsBehavior.Editable && gvDetail.DataRowCount > 0)
                //    {
                //        DialogResult result = XtraMessageBox.Show("Bạn có muốn cập nhật lại chứng từ chi lương này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //        if (result == DialogResult.Yes)
                //        {
                //            for (int i = 0; i < gvDetail.DataRowCount; i++)
                //                gvDetail.DeleteRow(i);
                //        }
                //    }
                //}
            }
        }

        public DataCustomFormControl Data
        {
            set { data = value; }
        }

        public InfoCustomControl Info
        {
            get { return info; }
        }

        #endregion
    }
}
