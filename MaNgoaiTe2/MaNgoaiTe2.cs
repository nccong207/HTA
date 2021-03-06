using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraLayout;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Plugins;

namespace MaNgoaiTe2
{
    public class MaNgoaiTe2 : ICControl
    {
        private DataCustomFormControl _data;
        private InfoCustomControl _info = new InfoCustomControl(IDataType.SingleDt);

        #region ICControl Members

        public DataCustomFormControl Data
        {
            set { _data = value; }
        }

        InfoCustomControl ICControl.Info
        {
            get { return _info; }
        }

        public void AddEvent()
        {
            Control[] maNT = _data.FrmMain.Controls.Find("MaNT", true);
            if (maNT.Length == 0)   //truong hop dung tham so de khong dung MaNT
            {
                ScanNTField(DevExpress.XtraLayout.Utils.LayoutVisibility.Never);
                return;
            }
            GridLookUpEdit glu = _data.FrmMain.Controls.Find("MaNT", true)[0] as GridLookUpEdit;
            if (glu == null)
                return;
            glu.EditValueChanged += new EventHandler(be_EditValueChanged);
        }
        #endregion

        void be_EditValueChanged(object sender, EventArgs e)
        {
            bool isNT = (sender as Control).Text == "VND" ? false : true;
            DevExpress.XtraLayout.Utils.LayoutVisibility lv = isNT ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            ScanNTField(lv);
        }

        private void ScanNTField(DevExpress.XtraLayout.Utils.LayoutVisibility lv)
        {
            LayoutControl lcMain = _data.FrmMain.Controls.Find("lcMain", true)[0] as LayoutControl;
            foreach (BaseLayoutItem lci in lcMain.Items)
                if (lci.GetType() == typeof(LayoutControlItem) &&
                    (lci.Text.ToUpper().Contains("NGUYÊN TỆ") || lci.Text.ToUpper().Contains("TỶ GIÁ"))
                    || (lci.Text.ToUpper().Contains("ORIGINAL") || lci.Text.ToUpper().Contains("RATE OF EXCHANGE")))
                    lci.Visibility = lv;
        }
    }
}
