using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraLayout;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using Plugins;
using CDTDatabase;
using DataFactory;
using FormFactory;

namespace DieuChinhCongNo
{
    public class DieuChinhCongNo : ICControl
    {
        Database _dbData = Database.NewDataDatabase();
        private DataCustomFormControl _data;
        private InfoCustomControl _info = new InfoCustomControl(IDataType.MasterDetailDt);
        GridView gvMain;
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
            GridControl gcMain = _data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl;
            gvMain = gcMain.MainView as GridView;
            gvMain.OptionsBehavior.Editable = false;

            GridLookUpEdit glu = _data.FrmMain.Controls.Find("DMCNID", true)[0] as GridLookUpEdit;
            if (glu == null)
                return;
            FilterDMCN(glu);
            RefreshColumnData();
            glu.EditValueChanged += new EventHandler(glu_EditValueChanged);
        }
        #endregion

        private void FilterDMCN(GridLookUpEdit gluDMCN)
        {
            if (!_data.DrTable.Table.Columns.Contains("ExtraSQL"))
                return;
            string filter = _data.DrTable["ExtraSQL"].ToString();
            RepositoryItemGridLookUpEdit ri = gluDMCN.Properties;
            if ((ri.Tag as GrdSingle) != null)
            {
                CDTData data = (ri.Tag as GrdSingle).Data;
                if (data.FullData)
                    return;
                if (data.Condition != string.Empty)
                    data.Condition = data.Condition + " and " + filter;
                else
                    data.Condition = filter;
                data.GetData();
                data.FullData = true;
                BindingSource bs = ri.DataSource as BindingSource;
                bs.DataSource = data.DsData.Tables[0];
                ri.View.BestFitColumns();
            }
        }

        private void VisibleSoHD(bool visible)
        {
            if (gvMain.Columns["SoHD"] != null)
            {
                gvMain.Columns["SoHD"].Visible = visible;
                if (visible)
                    gvMain.BestFitColumns();
            }
        }

        private void RefreshColumnData()
        {
            foreach (GridColumn gc in gvMain.Columns)
            {
                if (gc.ColumnEdit != null && gc.ColumnEdit.GetType() == typeof(RepositoryItemGridLookUpEdit))
                {
                    RepositoryItemGridLookUpEdit ri = gc.ColumnEdit as RepositoryItemGridLookUpEdit;
                    if ((ri.Tag as GrdSingle) != null)
                    {
                        CDTData data = (ri.Tag as GrdSingle).Data;
                        if (data.FullData)
                            continue;
                        data.GetData();
                        data.FullData = true;
                        BindingSource bs = ri.DataSource as BindingSource;
                        bs.DataSource = data.DsData.Tables[0];
                    }
                }
            }
        }

        private void ClearOldRows()
        {
            DataView dvDetail = (_data.BsMain.DataSource as DataSet).Tables[1].DefaultView;
            dvDetail.RowFilter = "MT35ID = '" + (_data.BsMain.Current as DataRowView)["MT35ID"].ToString() + "'";
            for (int i = dvDetail.Count - 1; i >= 0; i--)
                dvDetail[i].Delete();
            dvDetail.RowFilter = "";
        }

        void glu_EditValueChanged(object sender, EventArgs e)
        {
            if (_data.BsMain.Current == null)
                return;
            GridLookUpEdit glu = sender as GridLookUpEdit;
            if (glu.EditValue == null || glu.EditValue.ToString() == string.Empty)
                return;
            DataRow dr = (_data.BsMain.Current as DataRowView).Row;
            if (dr["NgayCT"].ToString() == "" || dr["MaNT"].ToString() == ""
                || dr["GioiHan"].ToString() == "")
                return;
            dr["DMCNID"] = glu.EditValue;   //nhat dinh phai co dong nay de khi dr.EndEdit() khong bi mat gia tri
            ClearOldRows();
            string ctCNID = glu.EditValue.ToString();
            DataTable dtData;
            switch (ctCNID)
            {
                case "1":
                    VisibleSoHD(false);
                    dtData = _dbData.GetDataSetByStore("GetCNtheoKH", new string[] { "NgayCT", "MaNT", "GioiHan", "DK" }, new object[] { dr["NgayCT"], dr["MaNT"], dr["GioiHan"], "1" });
                    break;
                case "2":
                    VisibleSoHD(true);
                    string dk = (dr["MaNT"].ToString() == "VND") ? "ConLai < " + dr["GioiHan"].ToString() : "ConLaiNT < " + dr["GioiHan"].ToString();
                    dtData = _dbData.GetDataTable("SELECT [TKNO] as TK, [SoHoaDon], [MaKH], DuNoNT = case when [ConLaiNT]>0 then [ConLaiNT] else 0 end, DuNo = case when [ConLai]>0 then [ConLai] else 0 end, " +
                        "DuCoNT = case when [ConLaiNT]<0 then -[ConLaiNT] else 0 end, DuCo = case when [ConLai]<0 then -[ConLai] else 0 end, MaBP, MaVV, MaPhi FROM [wHoaDonBan] " +
                        "where TKNO in (select tk from dmtk where tkcongno = 1) and ConLaiNT + ConLai <> 0 and NgayCT <= '" + dr["NgayCT"].ToString() + "' and MaNT = '" + dr["MaNT"].ToString() + "' and " + dk);
                    break;
                case "3":
                    VisibleSoHD(false);
                    dtData = _dbData.GetDataSetByStore("GetCNtheoKH", new string[] { "NgayCT", "MaNT", "GioiHan", "DK" }, new object[] { dr["NgayCT"], dr["MaNT"], dr["GioiHan"], "3" });
                    break;
                case "4":
                    VisibleSoHD(true);
                    dk = (dr["MaNT"].ToString() == "VND") ? "ConLai < " + dr["GioiHan"].ToString() : "ConLaiNT < " + dr["GioiHan"].ToString();
                    dtData = _dbData.GetDataTable("SELECT [TKCO] as TK, [SoHoaDon], [MaKH], DuNoNT = case when [ConLaiNT]<0 then -[ConLaiNT] else 0 end, DuNo = case when [ConLai]<0 then -[ConLai] else 0 end, " +
                        "DuCoNT = case when [ConLaiNT]>0 then [ConLaiNT] else 0 end, DuCo = case when [ConLai]>0 then [ConLai] else 0 end, MaBP, MaVV, MaPhi FROM [wHoaDonMua] " +
                        "where TKCO in (select tk from dmtk where tkcongno = 1) and ConLaiNT + ConLai <> 0 and NgayCT <= '" + dr["NgayCT"].ToString() + "' and MaNT = '" + dr["MaNT"].ToString() + "' and " + dk);
                    break;
                default:
                    dtData = new DataTable();
                    break;
            }
            if (dtData.Rows.Count == 0)
                return;
            foreach (DataRow drData in dtData.Rows)
            {
                DataRow drNew =(_data.BsMain.DataSource as DataSet).Tables[1].NewRow();
                if (drNew.RowState == DataRowState.Detached)
                    (_data.BsMain.DataSource as DataSet).Tables[1].Rows.Add(drNew);
                drNew["MT35ID"] = dr["MT35ID"];
                drNew["MaKH"] = drData["MaKH"];
                if (drData.Table.Columns.Contains("SoHoaDon"))
                    drNew["SoHD"] = drData["SoHoaDon"];
                drNew["TienThuNT"] = drData["DuCoNT"];
                drNew["TienThu"] = drData["DuCo"];
                drNew["TienChiNT"] = drData["DuNoNT"];
                drNew["TienChi"] = drData["DuNo"];
                drNew["TKCN"] = drData["TK"];
                drNew["MaBP"] = drData["MaBP"];
                drNew["MaPhi"] = drData["MaPhi"];
                drNew["MaVV"] = drData["MaVV"];
            }
        }
    }
}
