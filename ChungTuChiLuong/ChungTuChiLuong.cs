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

namespace ChungTuChiLuong
{
    public class ChungTuChiLuong:ICControl
    {
        #region ICControl Members

        private DataCustomFormControl data;
        private InfoCustomControl info = new InfoCustomControl(IDataType.MasterDetailDt);
        Database db = Database.NewDataDatabase();   

        public void AddEvent()
        {
            if ((data.DrTable["TableName"].ToString().ToUpper() == "DT12" 
                || data.DrTable["TableName"].ToString().ToUpper() == "DT16") &&
                (data.DrTable.Table.Columns.Contains("ExtraSql")
                && data.DrTable["ExtraSql"].ToString().ToUpper() == "REFVALUE LIKE 'CTL_%'"))
            {
                data.FrmMain.Shown += new EventHandler(FrmMain_Shown);
            }
        }

        void FrmMain_Shown(object sender, EventArgs e)
        {
            DataRowView drv = data.BsMain.Current as DataRowView;
            string value = data.DrTable["ExtraSql"].ToString();
            if (drv.Row.RowState == DataRowState.Added)
            {
                frmBangLuong frm = new frmBangLuong();
                frm.ShowDialog();
                string id = frm.id;
                int thang = frm.thang;
                int nam = frm.nam;
                double tongluong = frm.tongtien;
                GridView gvChiTiet = (data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl).MainView as GridView;

                if (id != "" && thang != 0 && nam != 0)
                {
                    //ghi dữ liệu Master
                    int numberDayInMonth = DateTime.DaysInMonth(nam, thang);
                    drv["NgayCT"] = new DateTime(nam, thang, numberDayInMonth);
                    drv["RefValue"] = "CTL_" + id;
                    drv["DienGiai"] = "Chi lương tháng " + thang.ToString() + " năm " + nam.ToString();                        
                    //ghi dữ liệu detail
                    gvChiTiet.AddNewRow();
                    gvChiTiet.UpdateCurrentRow();
                    gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["TkNo"], "3341");
                    gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["Ps"], tongluong.ToString());

                    //khong cho edit
                    gvChiTiet.Columns["TkNo"].OptionsColumn.AllowEdit = false;
                    gvChiTiet.Columns["Ps"].OptionsColumn.AllowEdit = false;
                }
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
