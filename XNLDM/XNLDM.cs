using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Windows.Forms;
using System.Data;

namespace XNLDM
{
    public class XNLDM : ICControl
    { 
        private DataCustomFormControl data;
        private InfoCustomControl _info = new InfoCustomControl(IDataType.MasterDetailDt);
        #region ICControl Members

        public void AddEvent()
        {
            if (data.DrTable.Table.Columns.Contains("ExtraSql")
                && data.DrTable["ExtraSql"].ToString().ToUpper() == "REFVALUE LIKE 'NSX_%'")
            {
                data.FrmMain.Shown += new EventHandler(FrmMain_Shown);
            }
        }

        void FrmMain_Shown(object sender, EventArgs e)
        {
            if (data.BsMain.Current == null)
                return;
            if ((data.BsMain.Current as DataRowView).Row.RowState == DataRowState.Added)
            {
                FrmXuatNL frm = new FrmXuatNL(data);
                frm.ShowDialog();
            }
        }

        public DataCustomFormControl Data
        {
            set { data = value; }
        }

        public InfoCustomControl Info
        {
            get { return _info; }
        }

        #endregion
    }
}
