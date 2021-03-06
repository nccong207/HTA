using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Plugins;
using CDTDatabase;
using System.Windows.Forms;

namespace UpdateThue
{
    public class UpdateThue : ICData
    {
        #region ICData Members
        private DataCustomData _data;
        private InfoCustomData _info;
        public UpdateThue()
        {
            _info = new InfoCustomData(IDataType.Single);
        }

        public DataCustomData Data
        {
            set { _data = value; }
        }

        public void ExecuteAfter()
        {
            FrmThang frm = new FrmThang(_data.DbData);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataTable dtNew = _data.DbData.GetDataTable("select * from ToKhai order by SortOrder");
                _data.DsData.Tables[0].Rows.Clear();
                foreach (DataRow dr in dtNew.Rows)
                    _data.DsData.Tables[0].ImportRow(dr);
                _data.DsData.Tables[0].AcceptChanges();
            }
        }

        public void ExecuteBefore()
        {
        }

        public InfoCustomData Info
        {
            get { return _info; }
        }

        #endregion
    }
}
