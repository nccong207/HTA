using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Plugins;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;

namespace Default
{
    public class Default : ICControl
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
            _data.BsMain.DataSourceChanged += new EventHandler(BsMain_DataSourceChanged);
            BsMain_DataSourceChanged(_data.BsMain, new EventArgs());
        }

        void BsMain_DataSourceChanged(object sender, EventArgs e)
        {
            DataTable dsData = _data.BsMain.DataSource as DataTable;
            if (dsData == null)
                return;
            dsData.TableNewRow += new DataTableNewRowEventHandler(LoaiKH_TableNewRow);
            if (_data.BsMain.Current != null)
                LoaiKH_TableNewRow(dsData, new DataTableNewRowEventArgs((_data.BsMain.Current as DataRowView).Row));
        }

        void LoaiKH_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            string s = _data.DrTable["ExtraSql"].ToString();
            if (s.ToUpper().Contains("ISKH"))
                e.Row["IsKH"] = true;
            if (s.ToUpper().Contains("ISNCC"))
                e.Row["IsNcc"] = true;
            if (s.ToUpper().Contains("ISNV"))
                e.Row["IsNV"] = true;
        }

        #endregion
    }
}
