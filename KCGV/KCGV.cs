using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Data;

namespace KCGV
{
    public class KCGV:ICControl
    {
        private DataCustomFormControl data;
        private InfoCustomControl info = new InfoCustomControl(IDataType.MasterDetailDt);
        #region ICControl Members

        public void AddEvent()
        {
            if (data.DrTable.Table.Columns.Contains("ExtraSql")
               && data.DrTable["ExtraSql"].ToString().ToUpper().Equals("1=1"))
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
                    XtraForm1 frm = new XtraForm1(data);
                    frm.Text = "Tự động lập chứng từ giá vốn công trình";
                    
                frm.ShowDialog();
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
