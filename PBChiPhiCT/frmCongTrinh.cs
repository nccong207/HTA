using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Plugins;
using CDTLib;
using CDTDatabase;

namespace PBChiPhiCT
{
    public partial class frmCongTrinh : DevExpress.XtraEditors.XtraForm
    {
        DataCustomFormControl _data;
        Database db = Database.NewDataDatabase();
        public frmCongTrinh(DataCustomFormControl data)
        {
            _data = data;
            InitializeComponent();
            LayCongTrinh();
        }
        void LayCongTrinh()
        {
            string sql = "select * from dmcongtrinh ct inner join DFNCCT DT on ct.MaCongTrinh=DT.MaCongTrinh where isHT='0' ";
            DataTable dt = db.GetDataTable(sql);
            DataColumn dtCol = new DataColumn("check", typeof(Boolean));
            dtCol.DefaultValue = false;
            if (dt.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không có công trình nào chưa hoàn thành.", Config.GetValue("PackageName").ToString());
                this.Close();
            }
            dt.Columns.Add(dtCol);
            gcCongTrinh.DataSource = dt;
            gvCongTrinh.BestFitColumns();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView(gcCongTrinh.DataSource as DataTable);
            dv.RowFilter = "check=true";
            if (dv.Count == 0)
            {
                XtraMessageBox.Show("Chọn công trình để hoạch toán.", Config.GetValue("PackageName").ToString());
                return;  
            }
            GridView gvChitiet = (_data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl).MainView as GridView;
            DataRow drMaster = (_data.BsMain.Current as DataRowView).Row;
            drMaster["DienGiai"] = "Hạch toán chi phi nhân công công trình";
            drMaster["RefValue"] = "NCT_";
            foreach (DataRowView drv in dv)
            {
                gvChitiet.AddNewRow();
                gvChitiet.UpdateCurrentRow();
                gvChitiet.SetFocusedRowCellValue(gvChitiet.Columns["Ps"],drv.Row["Luong"].ToString());
                gvChitiet.SetFocusedRowCellValue(gvChitiet.Columns["TkNo"], "622");
                gvChitiet.SetFocusedRowCellValue(gvChitiet.Columns["TkCo"], "3341");
                gvChitiet.SetFocusedRowCellValue(gvChitiet.Columns["DienGiaiCT"], "Hạch toán chi phí nhân công công trình "+drv.Row["MaCongTrinh"].ToString());
                gvChitiet.SetFocusedRowCellValue(gvChitiet.Columns["MaCongTrinh"], drv.Row["MaCongTrinh"].ToString());
            }
            gvChitiet.BestFitColumns();
            this.Close();
        }
    }
}