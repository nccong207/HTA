using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using CDTLib;
using System.Globalization;
using System.Data;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace BlockData
{
    public class BlockData : ICData
    {
        public BlockData()
        {
            _info = new InfoCustomData(IDataType.MasterDetailDt);
        }
        private InfoCustomData _info;
        private DataCustomData _data;
        #region ICData Members

        public DataCustomData Data
        {
            set { _data = value; }
        }

        public void ExecuteAfter()
        {
            
        }

        public void ExecuteBefore()
        {
            KiemTraKyKeToan();
            if (_info.Result)
                KiemTraKhoaSo();
        }

        public InfoCustomData Info
        {
            get { return _info; }
        }

        #endregion

        private void KiemTraKyKeToan()
        {
            if (Config.GetValue("KyKeToan") == null || Config.GetValue("KyKeToan").ToString() == "" || 
                Config.GetValue("NamLamViec") == null || Config.GetValue("NamLamViec").ToString() == "" || !_data.DsData.Tables[0].Columns.Contains("NgayCT"))
                return;
            if (_data.CurMasterIndex < 0)
                return;
            DataRow drCur = _data.DsData.Tables[0].Rows[_data.CurMasterIndex];
            if (drCur.RowState == DataRowState.Deleted)
                return;
            int ky = Int32.Parse(Config.GetValue("KyKeToan").ToString());
            int nam = Int32.Parse(Config.GetValue("NamLamViec").ToString());
            DateTime ngayCT = DateTime.Parse(drCur["NgayCT"].ToString());
            if (ngayCT.Year != nam)
            {
                XtraMessageBox.Show("Bạn đang nhập số liệu không thuộc năm tài chính " + nam.ToString() + " hiện tại.\nNếu thực sự muốn nhập số liệu vào năm tài chính này,\n" +
                    "nhấp chuột vào năm tài chính ở góc trên bên phải màn hình\nđể chuyển sang năm tài chính mới trước khi nhập!");
                _info.Result = false;
                return;
            }
            if (ngayCT.Month != ky)
            {
                if (XtraMessageBox.Show("Kỳ kế toán hiện tại là kỳ tháng " + ky.ToString() + ". Bạn đang nhập số liệu kỳ tháng " + ngayCT.Month.ToString() + ".\n" +
                    "Bạn có muốn chuyển sang kỳ kế toán tháng " + ngayCT.Month.ToString() + " để lưu số liệu không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Config.NewKeyValue("KyKeToan", ngayCT.Month);
                    _info.Result = true;
                }
                else
                    _info.Result = false;
            }
            else
                _info.Result = true;
        }

        private void KiemTraKhoaSo()
        {
            if (Config.GetValue("NgayKhoaSo") == null || !_data.DsData.Tables[0].Columns.Contains("NgayCT"))
                return;
            string tmp = Config.GetValue("NgayKhoaSo").ToString();
            DateTime ngayKhoa;
            DateTimeFormatInfo dtInfo = new DateTimeFormatInfo();
            dtInfo.ShortDatePattern = "dd/MM/yyyy";
            if (DateTime.TryParse(tmp, dtInfo, DateTimeStyles.None, out ngayKhoa))
            {
                DataView dv = new DataView(_data.DsData.Tables[0]);
                dv.RowStateFilter = DataViewRowState.Added | DataViewRowState.ModifiedOriginal | DataViewRowState.Deleted;
                if (dv.Count == 0)
                {
                    if (_data.CurMasterIndex < 0)
                        return;
                    DataRow drMaster = _data.DsData.Tables[0].Rows[_data.CurMasterIndex];
                    string pk = _data.DrTableMaster["Pk"].ToString();
                    DataView dvdt = new DataView(_data.DsData.Tables[1]);
                    dvdt.RowFilter = pk + " = '" + drMaster[pk].ToString() + "'";
                    dvdt.RowStateFilter = DataViewRowState.Added | DataViewRowState.ModifiedCurrent | DataViewRowState.Deleted;
                    if (dvdt.Count == 0)
                        return;
                    else
                    {
                        dv.RowStateFilter = DataViewRowState.CurrentRows;
                        dv.RowFilter = pk + " = '" + drMaster[pk].ToString() + "'";
                    }
                }
                DateTime ngayCT = DateTime.Parse(dv[0]["NgayCT"].ToString());
                if (ngayCT <= ngayKhoa)
                {
                    string msg = "Kỳ kế toán đã khóa! Không thể chỉnh sửa số liệu!";
                    if (Config.GetValue("Language").ToString() == "1")
                        msg = UIDictionary.Translate(msg);
                    XtraMessageBox.Show(msg);
                    _info.Result = false;
                }
                else
                    _info.Result = true;
            }
        }
    }
}
