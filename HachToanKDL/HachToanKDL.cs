using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System.Data;
using System.Windows.Forms;

namespace HachToanKDL
{
    public class HachToanKDL : ICControl
    {
        private DataCustomFormControl _data;
        private InfoCustomControl _info = new InfoCustomControl(IDataType.MasterDetailDt);
        GridControl gcMain;
        GridView gvMain;
        RepositoryItemGridLookUpEdit riMaVT;
        RepositoryItemGridLookUpEdit riMaKho;

        #region ICControl Members

        public void AddEvent()
        { 
            string tb = _data.DrTableMaster["TableName"].ToString();
            if (tb != "MT32" && tb != "MT33")
                return;
            gcMain = _data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl;
            gvMain = (gcMain.MainView) as GridView;
            gvMain.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gvMain_CellValueChanged);
            riMaVT = gcMain.RepositoryItems["MaVT"] as RepositoryItemGridLookUpEdit;
            riMaKho = gcMain.RepositoryItems["MaKho"] as RepositoryItemGridLookUpEdit;
        }

        void gvMain_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "MaKho" || e.Column.FieldName == "MaVT")
                SetTkKho();
        }

        private void SetTkKho()
        {
            object makho = gvMain.GetFocusedRowCellValue("MaKho");
            if (makho == null || makho.ToString() == "")
                return;
            object mavt = gvMain.GetFocusedRowCellValue("MaVT");
            if (mavt == null || mavt.ToString() == "")
                return;
            DataTable dtKho = (riMaKho.DataSource as BindingSource).DataSource as DataTable;
            DataRow[] drsKho = dtKho.Select("MaKho = '" + makho.ToString() + "'");
            if (drsKho.Length == 0)
                return;
            DataRow drKho = drsKho[0];
            if (Boolean.Parse(drKho["KDL"].ToString()) && drKho["TKKDL"].ToString() != "")
                gvMain.SetFocusedRowCellValue(gvMain.Columns["TKKho"], drKho["TKKDL"]);
            else
            {
                DataTable dtVT = (riMaVT.DataSource as BindingSource).DataSource as DataTable;
                DataRow[] drsVT = dtVT.Select("MaVT = '" + mavt.ToString() + "'");
                if (drsVT.Length == 0)
                    return;
                DataRow drVT = drsVT[0];
                gvMain.SetFocusedRowCellValue(gvMain.Columns["TKKho"], drVT["TkKho"]);
            }
        }

        public DataCustomFormControl Data
        {
            set { _data = value; }
        }

        public InfoCustomControl Info
        {
            get { return _info; }
        }

        #endregion
    }
}
