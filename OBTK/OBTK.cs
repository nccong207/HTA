using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Data;
using DevExpress.XtraEditors;
using CDTLib;

namespace OBTK
{
    public class OBTK : ICData
    {
        #region ICData Members
        private DataCustomData _data;
        private InfoCustomData _info;
        public OBTK()
        {
            _info = new InfoCustomData(IDataType.Single);
        }

        public DataCustomData Data
        {
            set { _data = value; }
        }

        public void ExecuteAfter()
        {
        }

        public void ExecuteBefore()
        {
            string tableName = _data.DrTable["TableName"].ToString();
            if (tableName != "OBTK")
                return;
            DataView dv = new DataView(_data.DsData.Tables[0]);
            dv.RowStateFilter = DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedCurrent;
            bool result = true;
            foreach (DataRowView drv in dv)
            {
                string tk = drv["Tk"].ToString();
                //kiem tra tai khoan ton kho
                if (tk.StartsWith("15") && !tk.StartsWith("154") && !tk.StartsWith("158") && !tk.StartsWith("159"))
                {
                    result = false;
                    string msg = "Tài khoản vật tư hàng hóa phải nhập số dư chi tiết ở phần số dư vật tư!";
                    if (Config.GetValue("Language").ToString() == "1")
                        msg = UIDictionary.Translate(msg);
                    XtraMessageBox.Show(msg);
                    break;
                }
                //kiem tra tai khoan cong no
                string sql = "select * from DMTK where Tk = '" + tk + "' and tkCongNo = 1";
                DataTable dt = _data.DbData.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    result = false;
                    string msg = "Tài khoản công nợ phải nhập số dư chi tiết ở phần số dư công nợ!";
                    if (Config.GetValue("Language").ToString() == "1")
                        msg = UIDictionary.Translate(msg);
                    XtraMessageBox.Show(msg);
                    break;
                }
            }
            _info.Result = result;
        }

        public InfoCustomData Info
        {
            get { return _info; }
        }

        #endregion
    }
}
