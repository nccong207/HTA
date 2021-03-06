using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraLayout;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Plugins;
using DataFactory;
using FormFactory;

namespace Vat
{
    public class Vat : ICControl
    {
        private DataCustomFormControl _data;
        private InfoCustomControl _info = new InfoCustomControl(IDataType.MasterDetailDt);
        GridView gvVat;
        GridView gvKIT;
        GridView gvDetail;
        GridLookUpEdit glueMaThue;

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
            string table = _data.DrTableMaster["TableName"].ToString();
            string[] tmp = new string[] { "MT21", "MT22", "MT23", "MT24", "MT25", 
                "MT31", "MT32", "MT33"};
            List<string> lstTable = new List<string>();
            lstTable.AddRange(tmp);
            if (!lstTable.Contains(table))
                return; 
            string vatName;
            if (table.Contains("MT3"))
                vatName = "VATOUT";
            else
                vatName = "VATIn";
            if (table == "MT32")
                gvKIT = (_data.FrmMain.Controls.Find("DT32KIT", true)[0] as GridControl).MainView as GridView;
            gvVat = (_data.FrmMain.Controls.Find(vatName, true)[0] as GridControl).MainView as GridView;
            gvVat.Columns["NgayCt"].Visible = false;
            gvVat.Columns["DienGiai"].Visible = true;
            GetLookUpData();
            if (table.Contains("MT2") || table.Contains("MT3"))
            {
                if (table != "MT21" && table != "MT31")
                    gvVat.Columns["MaVT"].Visible = true;
                else
                    gvVat.Columns["MaVT"].Visible = false;

                //string detailName = _data.DrTable["TableName"].ToString();
                gvDetail = (_data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl).MainView as GridView;
                gvDetail.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(gvDetail_ValidateRow);
                if (gvKIT != null)
                    gvKIT.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(gvDetail_ValidateRow);
                _data.BsMain.DataSourceChanged += new EventHandler(BsMain_DataSourceChanged);
                BsMain_DataSourceChanged(_data.BsMain, new EventArgs());
                glueMaThue = _data.FrmMain.Controls.Find("MaThue", true)[0] as GridLookUpEdit;
                glueMaThue.EditValueChanged += new EventHandler(glueMaThue_EditValueChanged);
            }
            else
            {
                gvVat.Columns["MaVT"].Visible = false;
                gvVat.Columns["SoLuong"].Visible = false;
                gvVat.Columns["DonGia"].Visible = false;
            }
            gvVat.BestFitColumns();
        }
        #endregion
        private void GetLookUpData()
        {
            string table = _data.DrTableMaster["TableName"].ToString();
            if ((table == "MT24" || table == "MT32") && gvVat.Columns["MaVT"] != null && gvVat.Columns["MaVT"].ColumnEdit != null)
            {
                RepositoryItemGridLookUpEdit riGlu = gvVat.Columns["MaVT"].ColumnEdit as RepositoryItemGridLookUpEdit;
                CDTData data = (riGlu.Tag as GrdSingle).Data;
                //CDTData data = riGlu.Tag as CDTData;
                if (data.FullData)
                    return;
                data.GetData();
                data.FullData = true;
                BindingSource bs = riGlu.DataSource as BindingSource;
                bs.DataSource = data.DsData.Tables[0];
            }
            if ((table.Contains("MT1") || table == "MT51") && gvVat.Columns["MaKH"] != null && gvVat.Columns["MaKH"].ColumnEdit != null)
            {
                RepositoryItemGridLookUpEdit riGlu = gvVat.Columns["MaKH"].ColumnEdit as RepositoryItemGridLookUpEdit;
                CDTData data = (riGlu.Tag as GrdSingle).Data;
                //CDTData data = riGlu.Tag as CDTData;
                if (data.FullData)
                    return;
                data.GetData();
                data.FullData = true;
                BindingSource bs = riGlu.DataSource as BindingSource;
                bs.DataSource = data.DsData.Tables[0];
            }
        }

        void glueMaThue_EditValueChanged(object sender, EventArgs e)
        {
            //xu ly bo sung trong truong hop chon hoac xoa ma thue sau khi da nhap chi tiet
            if (_data.BsMain.Current == null || _data.BsMain.Current.GetType() != typeof(DataRowView))
                return;
            DataRow drMaster = (_data.BsMain.Current as DataRowView).Row;
            if (drMaster.RowState == DataRowState.Deleted || drMaster.RowState == DataRowState.Detached)
                return; 
            GridLookUpEdit glueMaThue = sender as GridLookUpEdit;
            if (glueMaThue.EditValue != null && glueMaThue.EditValue.ToString() != "")  //truong hop chon ma thue
            {
                if (gvVat.DataRowCount == 0) //da nhap chi tiet nhung chua phat sinh thue
                {
                    for (int i = 0; i < gvDetail.DataRowCount; i++)
                    {
                        DataRow drSource = gvDetail.GetDataRow(i); 
                        DataSet dsData = _data.BsMain.DataSource as DataSet;
                        DataRow drDes = dsData.Tables[2].NewRow();
                        string mtPk = _data.DrTableMaster["Pk"].ToString();
                        drDes["MTID"] = drSource[mtPk];
                        string dtPk = _data.DrTable["Pk"].ToString();
                        drDes["MTIDDT"] = drSource[dtPk];
                        dsData.Tables[2].Rows.Add(drDes);
                        CopyData(drSource, drDes, -1);
                    }
                }
            }
            if (glueMaThue.EditValue == null || glueMaThue.EditValue.ToString() == "")  //truong hop xoa ma thue
            {
                if (gvVat.DataRowCount > 0) //da phat sinh thue -> can xoa di
                {
                    for (int i = gvVat.DataRowCount - 1; i >= 0; i--)
                    {
                        gvVat.DeleteRow(i);
                    }
                }
            }
        }

        private object LayThueSuat(string maThue)
        {
            if (glueMaThue == null)
                return null;
            DataTable dtDMTS = (glueMaThue.Properties.DataSource as BindingSource).DataSource as DataTable;
            DataRow[] drs = dtDMTS.Select("MaThue = '" + maThue + "'");
            if (drs.Length == 0)
                return null;
            return drs[0]["ThueSuat"];
        }

        private DataRow GetDesFromSource(DataRow drSource)
        {
            DataSet dsData = _data.BsMain.DataSource as DataSet;
            //DataView dv = new DataView(dsData.Tables[2]);
            string dtPk = drSource.Table.Columns.Contains("DT32KITID") ? "DT32KITID" : _data.DrTable["Pk"].ToString();
            DataRow[] drs = dsData.Tables[2].Select("MTIDDT = '" + drSource[dtPk].ToString() + "'");
            if (drs.Length > 0)
                return drs[0];
            else
                return null;
            //dv.RowStateFilter = DataViewRowState.CurrentRows;
            //dv.RowFilter = "MTIDDT = '" + drSource[dtPk].ToString() + "'";
            //if (dv.Count > 0)
            //    return dv[0].Row;
            //else
            //    return null;
        }

        void ChiTiet_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            string t = _data.DrTableMaster["TableName"].ToString();
            if (t == "MT32" && (sender as DataTable).TableName == "Table1" 
                && gvKIT != null && gvKIT.DataRowCount > 0)    //hoa don ban hang nhap so lieu tren ban hang theo bo
                        return;
            if (_data.BsMain.Current == null)
                return;
            DataRow drCurMaster = (_data.BsMain.Current as DataRowView).Row;
            if (drCurMaster.RowState != DataRowState.Deleted
                && drCurMaster.Table.Columns.Contains("MaThue")
                && drCurMaster["MaThue"].ToString() == "")
                return; 
            if (e.Action == DataRowAction.Add)
            {
                DataSet dsData = _data.BsMain.DataSource as DataSet;
                DataRow drDes = dsData.Tables[2].NewRow();
                string mtPk = _data.DrTableMaster["Pk"].ToString();
                drDes["MTID"] = e.Row[mtPk];
                string dtPk = (sender as DataTable).Columns.Contains("DT32KITID") ? "DT32KITID" : _data.DrTable["Pk"].ToString();
                drDes["MTIDDT"] = e.Row[dtPk];
                dsData.Tables[2].Rows.Add(drDes);
                CopyData(e.Row, drDes, -1);
            }
            //phan mem chay rat nang - chi chay tren chung tu chi phi mua hang
            string table = _data.DrTableMaster["TableName"].ToString();
            if (table == "MT25" && e.Action == DataRowAction.Change)
            {
                DataRow drDes = GetDesFromSource(e.Row);
                if (drDes != null)
                {
                    string mtPk = _data.DrTableMaster["Pk"].ToString();
                    if (drDes["MTID"].ToString() == "")
                        drDes["MTID"] = e.Row[mtPk];
                    CopyData(e.Row, drDes, -1);
                }
            }
        }

        private int FindRowHandleByDataRow(GridView view, DataRow row)
        {
            for (int i = 0; i < view.DataRowCount; i++)
                if (view.GetDataRow(i) == row)
                    return i;
            return GridControl.InvalidRowHandle;
        }

        private void CopyData(DataRow drSource, DataRow drDes, int rowHandle)
        {
            if (drSource.Table.Columns.Contains("Gia") && drDes.Table.Columns.Contains("DonGia"))
                drDes["DonGia"] = drSource["Gia"];
            if (drSource.Table.Columns.Contains("MaVT"))
                drDes["MaVT"] = drSource["MaVT"];
            //if (drSource.Table.Columns.Contains("TenVT"))
            //    drDes["DienGiai"] = drSource["TenVT"];
            if (drSource.Table.Columns.Contains("SoLuong") && drDes.Table.Columns.Contains("SoLuong"))
                drDes["SoLuong"] = drSource["SoLuong"];
            //dung cho MT32 - ban hang theo bo
            if (drSource.Table.Columns.Contains("Gia1") && drDes.Table.Columns.Contains("DonGia"))
                drDes["DonGia"] = drSource["Gia1"];
            if (drSource.Table.Columns.Contains("TenKIT"))
                drDes["DienGiai"] = drSource["TenKIT"];
            if (drSource.Table.Columns.Contains("SoLuong1") && drDes.Table.Columns.Contains("SoLuong"))
                drDes["SoLuong"] = drSource["SoLuong1"];

            DataRow drCurMaster = (_data.BsMain.Current as DataRowView).Row;
            if (rowHandle == -1)
                rowHandle = FindRowHandleByDataRow(gvVat, drDes);
            if (rowHandle != GridControl.InvalidRowHandle)
                gvVat.SetRowCellValue(rowHandle, "MaKH", drCurMaster["MaKH"]);
            if (drCurMaster.Table.Columns.Contains("MaThue"))
            {
                if (rowHandle != GridControl.InvalidRowHandle)
                    gvVat.SetRowCellValue(rowHandle, "MaThue", drCurMaster["MaThue"]);
            }
            drDes["NgayCT"] = drCurMaster["NgayCT"];
            drDes["NgayHd"] = drCurMaster["NgayHD"];
            drDes["Sohoadon"] = drCurMaster["SoHoaDon"];
            drDes["MaKH"] = drCurMaster["MaKH"];
            drDes["TenKH"] = drCurMaster["TenKH"];
            drDes["DiaChi"] = drCurMaster["DiaChi"];
            drDes["MaThue"] = drCurMaster["MaThue"];
            drDes["ThueSuat"] = LayThueSuat(drCurMaster["MaThue"].ToString());
            if (drDes.Table.Columns.Contains("SoSeries"))
                drDes["SoSeries"] = drCurMaster["SoSeri"];
            if (drDes.Table.Columns.Contains("SoSerie"))
                drDes["SoSerie"] = drCurMaster["SoSeri"];
            if (!drCurMaster.Table.Columns.Contains("NgayCT") && drCurMaster.Table.Columns.Contains("NgayHD"))
                drDes["NgayCT"] = drCurMaster["NgayHD"];
            decimal ttien = 0, ttiennt = 0;
            if (drSource.Table.Columns.Contains("Ps"))
                ttien = Decimal.Parse(drSource["Ps"].ToString());
            if (drSource.Table.Columns.Contains("Psnt"))
                ttiennt = Decimal.Parse(drSource["Psnt"].ToString());
            //dung cho MT32 - ban hang theo bo
            if (drSource.Table.Columns.Contains("Ps1"))
                ttien = Decimal.Parse(drSource["Ps1"].ToString());
            if (drSource.Table.Columns.Contains("Ps1nt"))
                ttiennt = Decimal.Parse(drSource["Ps1nt"].ToString());

            if (drSource.Table.Columns.Contains("CtThueNk"))
                ttien += Decimal.Parse(drSource["CtThueNk"].ToString());
            if (drSource.Table.Columns.Contains("CtThueNknt"))
                ttiennt += Decimal.Parse(drSource["CtThueNknt"].ToString());
            //if (drSource.Table.Columns.Contains("CPCt"))
            //    ttien += Decimal.Parse(drSource["CPCt"].ToString());
            //if (drSource.Table.Columns.Contains("CPCtnt"))
            //    ttiennt += Decimal.Parse(drSource["CPCtnt"].ToString());
            if (drSource.Table.Columns.Contains("CK"))
                ttien -= Decimal.Parse(drSource["CK"].ToString());
            if (drSource.Table.Columns.Contains("CKNT"))
                ttiennt -= Decimal.Parse(drSource["CKNT"].ToString());
            string tableName = _data.DrTableMaster["TableName"].ToString();
            if (tableName == "MT24")
            {
                ttien = -ttien;
                ttiennt = -ttiennt;
                drDes["TkDu"] = drCurMaster["TkNo"];
            }
            if (tableName == "MT33")
            {
                ttien = -ttien;
                ttiennt = -ttiennt;
                drDes["TkDu"] = drCurMaster["TkCo"];
            }
            drDes["TTien"] = ttien;
            drDes["TTienNT"] = ttiennt;
            if (tableName == "MT23")
                drDes["TkDu"] = drCurMaster["TkThue1"];
            if (drDes.Table.Columns.Contains("MaBP"))
                drDes["MaBP"] = drSource["MaBP"];
        }

        void BsMain_DataSourceChanged(object sender, EventArgs e)
        {
            DataSet dsData = _data.BsMain.DataSource as DataSet;
            if (dsData == null)
                return;
            string table = _data.DrTableMaster["TableName"].ToString();
            //if (table == "MT21" || table == "MT32")
                dsData.Tables[1].RowChanged += new DataRowChangeEventHandler(ChiTiet_RowChanged);
            dsData.Tables[1].RowDeleting += new DataRowChangeEventHandler(ChiTiet_RowDeleting);
            if (table == "MT32")
            {
                dsData.Tables[3].RowChanged += new DataRowChangeEventHandler(ChiTiet_RowChanged);
                dsData.Tables[3].RowDeleting += new DataRowChangeEventHandler(ChiTiet_RowChanged);
            }
        }

        void ChiTiet_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            string t = _data.DrTableMaster["TableName"].ToString();
            if (t == "MT32" && (sender as DataTable).TableName == "Table1"
                && gvKIT != null && gvKIT.DataRowCount > 0)    //hoa don ban hang nhap so lieu tren ban hang theo bo
                return;
            DataRow drDes = GetDesFromSource(e.Row);
            if (drDes != null)
                drDes.Delete();
        }

        void gvDetail_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (_data.BsMain.Current == null)
                return;
            DataRow drCurMaster = (_data.BsMain.Current as DataRowView).Row;
            if (drCurMaster.RowState != DataRowState.Deleted
                && drCurMaster.Table.Columns.Contains("MaThue")
                && drCurMaster["MaThue"].ToString() == "")
                return;
            GridView gvDetail = sender as GridView;
            DataSet dsData = _data.BsMain.DataSource as DataSet;
            DataRow drSource = gvDetail.GetDataRow(e.RowHandle);
            string t = _data.DrTableMaster["TableName"].ToString();
            if (t == "MT32" && drSource.Table.TableName == "Table1"
                && gvKIT != null && gvKIT.DataRowCount > 0)    //hoa don ban hang nhap so lieu tren ban hang theo bo
                return;
            if (drSource == null)
                return;
            switch (drSource.RowState)
            {
                case DataRowState.Added:
                    DataRow drDes = GetDesFromSource(drSource);
                    if (drDes == null)
                    {
                        drDes = dsData.Tables[2].NewRow();
                        string mtPk = _data.DrTableMaster["Pk"].ToString();
                        drDes["MTID"] = drSource[mtPk];
                        string dtPk = drSource.Table.Columns.Contains("DT32KITID") ? "DT32KITID" : _data.DrTable["Pk"].ToString();
                        drDes["MTIDDT"] = drSource[dtPk];
                        dsData.Tables[2].Rows.Add(drDes);
                    }
                    if (drDes != null)
                    {
                        string mtPk = _data.DrTableMaster["Pk"].ToString();
                        if (drDes["MTID"].ToString() == "")
                            drDes["MTID"] = drSource[mtPk];
                        CopyData(drSource, drDes, e.RowHandle);
                    }
                    break;
                case DataRowState.Unchanged:
                case DataRowState.Modified:
                    drDes = GetDesFromSource(drSource);
                    if (drDes != null)
                    {
                        string mtPk = _data.DrTableMaster["Pk"].ToString();
                        if (drDes["MTID"].ToString() == "")
                            drDes["MTID"] = drSource[mtPk];
                        CopyData(drSource, drDes, e.RowHandle);
                    }
                    break;
            }
        }
    }
}
