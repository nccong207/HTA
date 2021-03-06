using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Plugins;
using CDTLib;
using CDTDatabase;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;

namespace KCGV
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        DataCustomFormControl _data;
        Database db = Database.NewDataDatabase();

        public XtraForm1(DataCustomFormControl data)
        {
            InitializeComponent();
            _data = data;
        }
        void LayTK()
        {
            string sql = "SELECT TK, TenTK FROM DMTK where TK not in (select  TK=case when TKMe is null then '' else TKMe end from DMTK group by TKMe)";
            DataTable dt = db.GetDataTable(sql);
            lookupDMTK.Properties.DataSource = dt;
            lookupDMTK.Properties.DisplayMember = "TK";
            lookupDMTK.Properties.ValueMember = "TK";
            lookupDMTK.Properties.BestFit();
            lookupDMTK.EditValue = "";

           
            lookupDMTKGV.Properties.DataSource = dt;
            lookupDMTKGV.Properties.DisplayMember = "TK";
            lookupDMTKGV.Properties.ValueMember = "TK";
            lookupDMTKGV.Properties.BestFit();
            lookupDMTKGV.EditValue = "";
        }
        bool KiemTra()
        {
            if (lookupDMTK.EditValue == null || lookupDMTK.EditValue.ToString().Equals(""))
            {
                XtraMessageBox.Show("Chọn tài khoản chi phí!", Config.GetValue("PackageName").ToString());
                return false;
            }
            if (lookupDMTKGV.EditValue == null || lookupDMTKGV.EditValue.ToString().Equals(""))
            {
                XtraMessageBox.Show("Chọn tài khoản giá vốn!", Config.GetValue("PackageName").ToString());
                return false;
            }
            if (dateEdit1.EditValue == null)
            {
                XtraMessageBox.Show("Chọn ngày đến!", Config.GetValue("PackageName").ToString());
                return false;
            }

            return true;
        }
        private void btxem_Click(object sender, EventArgs e)
        {
            if (!KiemTra())
                return;
            string[] pra = new string[] { "@tk","@tkdich","@ngayct1","@ngayct2","@mabp","@mavv","@maphi","@masp","@macongtrinh"};
            object[] value = new object[] { lookupDMTK.EditValue, lookupDMTKGV.EditValue, "01/01/2012 12:00:00 AM", dateEdit1.EditValue, "0", "0", "0", "0", "1" };

            DataTable tbsoduCk = db.GetDataSetByStore("soduCK", pra, value);
            //DataTable dt = new DataTable();
            //DataColumn dtColhm = new DataColumn("Hạng mục", typeof(string));
            //DataColumn dtColcp = new DataColumn("Chi phí", typeof(decimal));
            DataColumn dtColcheck = new DataColumn("check", typeof(Boolean));
            DataColumn dtColtl = new DataColumn("Tyle", typeof(decimal));
            DataColumn dtColgv = new DataColumn("Giavon", typeof(decimal));
            DataColumn dtColct = new DataColumn("MaCongTrinhMe", typeof(string));
            //dtColcheck.DefaultValue = false;
            //dt.Columns.Add(dtColhm);
            //dt.Columns.Add(dtColcp);
             dtColcheck.DefaultValue = false;
                tbsoduCk.Columns.Add(dtColcheck);
                tbsoduCk.Columns.Add(dtColtl);
               tbsoduCk.Columns.Add(dtColgv);
              tbsoduCk.Columns.Add(dtColct);
           // thức giá vốn = tỷ lệ % * chi phí / 100
            //dtColgv.
           // dt.Rows.Add("a","100","false","0","0","abx");


            foreach (DataRow dr in tbsoduCk.Rows)
            {
                string sql ="select top 1 MaCongTrinhMe from dmCongTrinh where MaCongTrinh='"+dr["macongtrinh"].ToString()+"'";
                DataTable tbhangmuc = db.GetDataTable(sql);

               // DataRow newrow = dt.NewRow();
               // newrow["Hạng mục"]=dr["macongtrinh"];
               // newrow["Chi phí"] = dr["sodu"];
                if(tbhangmuc.Rows.Count>0)
                    dr["MaCongTrinhMe"] = tbhangmuc.Rows[0][0];
                //dt.Rows.Add(newrow);


            }

            gcmain.DataSource = tbsoduCk;
            gvmain.BestFitColumns();
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            LayTK();
        }

        private void btdongy_Click(object sender, EventArgs e)
        {

        }

        private void btthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btdongy_Click_1(object sender, EventArgs e)
        {
            if (!KiemTra())
                return;
           
            DataTable dt = gcmain.DataSource as DataTable;
            GridView gvMain = (_data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl).MainView as GridView;
            DataRow drCurrent = (_data.BsMain.Current as DataRowView).Row;
           // string mact = dk.Replace("(", "").Replace(")", "");
           // drCurrent["RefValue"] = "NCT_" + mact.Replace("'", "_");
            drCurrent["DienGiai"] = "Kết chuyển giá vốn công trình ";
            if (dt==null)
            {
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Check"].ToString() == "True")
                {
                    gvMain.AddNewRow();
                    gvMain.UpdateCurrentRow();
                    // gvMain.SetFocusedRowCellValue(gvMain.Columns["MaKho"], lookupDMKho.EditValue);
                    gvMain.SetFocusedRowCellValue(gvMain.Columns["Ps"], dr["Giavon"]);
                    gvMain.SetFocusedRowCellValue(gvMain.Columns["TkNo"],lookupDMTKGV.EditValue );
                    gvMain.SetFocusedRowCellValue(gvMain.Columns["TkCo"], lookupDMTK.EditValue);
                    gvMain.SetFocusedRowCellValue(gvMain.Columns["DienGiaiCT"], "kết chuyển giá vốn hạng mục " + dr["macongtrinh"] + " (" + dr["Tyle"] + "%)");
                    gvMain.SetFocusedRowCellValue(gvMain.Columns["MaCongTrinh"], dr["macongtrinh"]);
                }
            }
            this.Close();
        }

        private void gvmain_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Caption == "Tỷ lệ %")
            {
                (gcmain.DataSource as DataTable).Rows[e.RowHandle]["Giavon"] = (float.Parse(e.Value.ToString()) * float.Parse((gcmain.DataSource as DataTable).Rows[e.RowHandle]["sodu"].ToString())) / 100;
            }
        }

    }
}