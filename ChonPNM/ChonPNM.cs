using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using Plugins;
using CDTDatabase;
using DataFactory;
using FormFactory;

namespace ChonPNM
{
    public class ChonPNM : ICControl
    {
        private Database _dbData = Database.NewDataDatabase();
        private DataCustomFormControl _data;
        private InfoCustomControl _info = new InfoCustomControl(IDataType.MasterDetailDt);

        #region ICControl Members

        public DataCustomFormControl Data
        {
            set { _data = value; }
        }

        public InfoCustomControl Info
        {
            get { return _info; }
        }

        public void AddEvent()
        {
            GridLookUpEdit glu = _data.FrmMain.Controls.Find("MT22ID", true)[0] as GridLookUpEdit;
            if (glu == null)
                return;
            RefreshColumnData();
            glu.EditValueChanged += new EventHandler(glu_EditValueChanged);
        }
        #endregion

        private void RefreshColumnData()
        {
            GridControl gcMain = _data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl;
            if (gcMain == null)
                return;
            foreach (GridColumn gc in (gcMain.MainView as GridView).Columns)
            {
                string f = gc.FieldName.ToUpper();
                if (f != "PS" && f != "PSNT")
                    gc.OptionsColumn.AllowEdit = false;
            }
        }

        void glu_EditValueChanged(object sender, EventArgs e)
        {
            GridLookUpEdit glu = sender as GridLookUpEdit;
            if (glu.EditValue == null || glu.EditValue.ToString() == string.Empty)
                return;
            DataRow drMaster = (_data.BsMain.Current as DataRowView).Row;
            if (drMaster == null)
                return;
            drMaster["MT22ID"] = glu.EditValue;   //nhat dinh phai co dong nay de khi drMaster.EndEdit() khong bi mat gia tri
            string mt22ID = glu.EditValue.ToString();
            DataTable dt = _dbData.GetDataTable("select *, Ps + CPCt as TienHang from DT22 where MT22ID = '" + mt22ID + "'");
            if (dt.Rows.Count == 0)
                dt = _dbData.GetDataTable("select DT23ID as DT22ID, *, Ps + CPCt + CtThueNk as TienHang from DT23 where MT23ID = '" + mt22ID + "'");
            if (dt.Rows.Count == 0)
                return;
            string mt25ID = drMaster["MT25ID"].ToString();
            DataTable dtDetail = (_data.BsMain.DataSource as DataSet).Tables[1];
            DataView dvOld = new DataView(dtDetail);
            GridView gvMain = (_data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl).Views[0] as GridView;
            foreach (DataRow dr in dt.Rows)
            {
                dvOld.RowFilter = "MT25ID = '" + mt25ID + "' and MT22ID = '" + mt22ID + "' and DT22ID = '" + dr[0].ToString() + "'";
                if (dvOld.Count > 0)
                    continue;
                //them vao Detail
                gvMain.AddNewRow();
                gvMain.UpdateCurrentRow();
                int n = gvMain.DataRowCount - 1;
                gvMain.SetRowCellValue(n, "MT22ID", mt22ID);
                gvMain.SetRowCellValue(n, "DT22ID", dr["DT22ID"]);
                gvMain.SetRowCellValue(n, "SL", double.Parse(dr["SoLuong"].ToString()));
                gvMain.SetRowCellValue(n, "TienHang", double.Parse(dr["TienHang"].ToString()));
                gvMain.SetRowCellValue(n, "TKNo", dr["TKNo"]);
                gvMain.SetRowCellValue(n, "MaBP", dr["MaBP"]);
                gvMain.SetRowCellValue(n, "MaPhi", dr["MaPhi"]);
                gvMain.SetRowCellValue(n, "MaVV", dr["MaVV"]);
                gvMain.SetRowCellValue(n, "GhiChu", dr["MaVT"]);
            }
        }
    }
}
