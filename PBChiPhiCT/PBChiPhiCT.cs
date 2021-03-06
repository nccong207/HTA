using System;
using System.Collections.Generic;
using System.Text;
using DevExpress;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using CDTLib;
using System.Windows.Forms;
using System.Data;
using CDTDatabase;
using Plugins;

namespace PBChiPhiCT
{
    public class PBChiPhiCT:ICControl
    {
        DataCustomFormControl data;
        InfoCustomControl info = new InfoCustomControl(IDataType.MasterDetailDt);
        Database db = Database.NewDataDatabase();
        #region ICControl Members
                    
        public void AddEvent()
        {
            if (data.DrTable.Table.Columns.Contains("ExtraSql")
                && data.DrTable["ExtraSql"].ToString().ToUpper().Equals("REFVALUE LIKE 'NCT_%'"))
            {
                data.FrmMain.Shown += new EventHandler(FrmMain_Shown);
            }
        }

        void FrmMain_Shown(object sender, EventArgs e)
        {
            DataRow drMaster = (data.BsMain.Current as DataRowView).Row;
            string month=Config.GetValue("KyKeToan")!=null?Config.GetValue("KyKeToan").ToString():"";
            if (month == "")
            {
                XtraMessageBox.Show("Chưa có kỳ kế toán.",Config.GetValue("PackageName").ToString());
                return;
            }
            string nam=Config.GetValue("Namlamviec")!=null?Config.GetValue("Namlamviec").ToString():DateTime.Now.Year.ToString();
            if (drMaster.RowState == DataRowState.Added)
            { 
                //string sql="select * from mt51 where month(ngayct)='"+month+"' and year(ngayct)='"+nam+"'";
                //DataTable dt = db.GetDataTable(sql);
                //if (dt.Rows.Count > 0)
                //    return;
                frmCongTrinh frm = new frmCongTrinh(data);
                frm.Text = "Danh sách công trình";
                frm.ShowDialog();
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
