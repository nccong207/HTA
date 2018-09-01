using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using DevExpress;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using CDTDatabase;
using Plugins;
using CDTLib;
namespace XNLDMCT
{
    public class XNLDMCT:ICControl
    {
        private DataCustomFormControl data ;
        private InfoCustomControl info = new InfoCustomControl(IDataType.MasterDetailDt);
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
            DataRowView drv = data.BsMain.Current as DataRowView;
            if (drv == null)
                return;
            if (drv.Row.RowState == DataRowState.Added)
            {
                frmDMCongTrinh frm = new frmDMCongTrinh(data);
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
