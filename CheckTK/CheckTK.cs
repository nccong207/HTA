using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Data;
using DevExpress.XtraEditors;
using CDTLib;
using System.Windows.Forms;
using CDTDatabase;

namespace CheckTK
{
    public class CheckTK: ICData
    {
        private InfoCustomData _info;
        private DataCustomData _data;
        #region ICData Members

        public DataCustomData Data
        {
            set { _data = value; }
        }

        public void ExecuteAfter()
        {
            if (_data.CurMasterIndex < 0)
                return;
            _info.Result = true;
            DataRow drMaster = _data.DsDataCopy.Tables[0].Rows[_data.CurMasterIndex];
            if (drMaster.RowState == DataRowState.Deleted)
                return;
            string tk = drMaster["Tk"].ToString().Trim();
            string tkme = drMaster["TkMe"].ToString().Trim();
            //kiem tra tao tai khoan con khi da phat sinh
            if (drMaster.RowState == DataRowState.Added && tkme != "")
            {
                string msg = "";
                object o = Config.GetValue("NgayKhoaSo");
                DateTime ngayKS = DateTime.MinValue;
                System.Globalization.DateTimeFormatInfo dtInfo = new System.Globalization.DateTimeFormatInfo();
                dtInfo.ShortDatePattern = "dd/MM/yyyy";
                if (o != null)
                    DateTime.TryParse(o.ToString(), dtInfo, System.Globalization.DateTimeStyles.None, out ngayKS);
                if (PhatSinh(tkme, ref msg, ngayKS))
                {
                    XtraMessageBox.Show(msg + " trên tài khoản mẹ " + tkme +
                        "\nPhần mềm sẽ tự động chuyển số liệu từ tài khoản mẹ sang tài khoản con",
                        Config.GetValue("PackageName").ToString());
                    _data.DbData.EndMultiTrans();       //can hoan thanh viec tao tk moi de co khoa ngoai khi cap nhat phat sinh
                    //cap nhat tat ca cac bang lien quan
                    _data.DbData.BeginMultiTrans();
                    CapNhatPS(tk, tkme, ngayKS);
                    if (!_data.DbData.HasErrors)
                        _data.DbData.EndMultiTrans();
                }
            }
        }

        public void ExecuteBefore()
        {
            if (_data.CurMasterIndex < 0)
                return;
            DataRow drMaster = _data.DsData.Tables[0].Rows[_data.CurMasterIndex];
            //kiem tra xoa tai khoan
            if (drMaster.RowState == DataRowState.Deleted)
            {
                string msg = "";
                string otk = drMaster["Tk", DataRowVersion.Original].ToString().Trim();
                if (PhatSinh(otk, ref msg, DateTime.MinValue))
                {
                    if (otk.Length == 3)    //xoa tai khoan cap 1
                    {
                        XtraMessageBox.Show("Tài khoản này không thể xóa do " + msg,
                            Config.GetValue("PackageName").ToString());
                        _info.Result = false;
                    }
                    else
                    {
                        string otkme = drMaster["TkMe", DataRowVersion.Original].ToString().Trim();
                        string tktt = LayTkThayThe(otk, otkme);
                        if (XtraMessageBox.Show(msg + " của tài khoản " + otk +
                            " sẽ được thay thế bằng tài khoản " + tktt + ".\nBạn có muốn xóa tài khoản " + otk + " không?",
                            Config.GetValue("PackageName").ToString(), MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            CapNhatPS(tktt, otk, DateTime.MinValue);
                            _info.Result = true;
                        }
                        else
                            _info.Result = false;
                    }
                }
                else
                    _info.Result = true;
                return;
            }
            string tk = drMaster["Tk"].ToString().Trim();
            string tkme = drMaster["TkMe"].ToString().Trim();
            //kiem tra neu nhieu hon 3 ky tu can chon tai khoan me
            if (tk.Length > 3 && tkme == "")
            {
                XtraMessageBox.Show("Chưa chọn tài khoản mẹ cho tài khoản này",
                    Config.GetValue("PackageName").ToString());
                _info.Result = false;
                return;
            }
            else
                _info.Result = true;
            //kiem tra tai khoan cong no
            if (drMaster.RowState == DataRowState.Added && tkme != "")    //kiem tra khi tao tai khoan con
            {
                DataRow[] drs = _data.DsData.Tables[0].Select("Tk = '" + tkme + "'");
                if (drs.Length > 0)
                {
                    bool tkcn = Boolean.Parse(drs[0]["TkCongNo"].ToString());
                    if (tkcn)   //tk me la tk cong no thi tk con bat buoc cung la tk cong no
                        drMaster["TkCongNo"] = true;
                    else   //tk me khong phai la tk cong no thi tk con cung vay
                    {
                        if (Boolean.Parse(drMaster["TkCongNo"].ToString()))
                            XtraMessageBox.Show("Hiện tại không thể thiết lập tài khoản này là tài khoản công nợ!\n" +
                                "Cần điều chỉnh tài khoản cấp 1 (" + tkme.Substring(0, 3) + ") là tài khoản công nợ", Config.GetValue("PackageaName").ToString());
                        drMaster["TkCongNo"] = false;
                    }
                }
            }
            if (drMaster.RowState == DataRowState.Modified      //kiem tra khi sua thuoc tinh tkcongno
                && drMaster["TkCongNo", DataRowVersion.Original].ToString() != drMaster["TkCongNo", DataRowVersion.Current].ToString())
            {
                bool cur = Boolean.Parse(drMaster["TkCongNo", DataRowVersion.Current].ToString());
                bool org = Boolean.Parse(drMaster["TkCongNo", DataRowVersion.Original].ToString());
                if (tkme == "") //sua tren tai khoan cap 1
                {
                    if (cur)    //chuyen tu "khong phai tkcn" sang "tkcn"
                    {
                        if (XtraMessageBox.Show("Cần phải nhập số dư đầu kỳ của tài khoản này chi tiết theo đối tượng công nợ!\n" +
                                "Vậy bạn có muốn chuyển tài khoản này và toàn bộ tài khoản con (nếu có) thành tài khoản công nợ không?",
                                Config.GetValue("PackageName").ToString(), MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            XoaSoDuTaiKhoan(tk);
                            CapNhatTKCN(tk, true);
                        }
                        else
                            drMaster["TkCongNo"] = false;
                    }
                    else    //chuyen tu "tkcn" sang "khong phai tkcn"
                    {
                        if (XtraMessageBox.Show("Số dư đầu kỳ chi tiết theo đối tượng công nợ của tài khoản này (nếu đã nhập) sẽ bị xóa!\n" +
                                "Vậy bạn có muốn thay đổi thiết lập công nợ cho tài khoản này và toàn bộ tài khoản con (nếu có) không?",
                                Config.GetValue("PackageName").ToString(), MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            XoaSoDuCongNo(tk);
                            CapNhatTKCN(tk, false);
                        }
                        else
                            drMaster["TkCongNo"] = true;
                    }
                }
                else
                {
                    XtraMessageBox.Show("Không thể thay đổi thiết lập công nợ trên tài khoản này!\n" +
                        "Cần thay đổi thiết lập công nợ trên tài khoản cấp 1 (" + tk.Substring(0, 3) + ")",
                        Config.GetValue("PackageName").ToString());
                    drMaster["TkCongNo"] = org;
                }
            }
        }

        private string LayTkThayThe(string tk, string tkme)
        {
            DataRow[] drs = _data.DsData.Tables[0].Select("TkMe = '" + tkme + "' and Tk <> '" + tk + "'");
            if (drs.Length > 0)                 //ưu tiên thay thế bằng tk cùng cấp trước
                return drs[0]["Tk"].ToString();
            return tkme;                        //nếu ko có tk cùng cấp thì thay thế bằng tk mẹ
        }

        private void CapNhatTKCN(string tk, bool tkcn)
        {
            DataRow[] drs = _data.DsData.Tables[0].Select("Tk like '" + tk + "%'");
            foreach (DataRow dr in drs)
            {
                if (dr["Tk"].ToString() == tk)
                    continue;
                dr["TkCongNo"] = tkcn;
                dr.AcceptChanges();
                _data.DbData.UpdateByNonQuery(String.Format("update DMTK set TkCongNo = '{0}' where Tk = '{1}'", tkcn, dr["Tk"]));
            }
        }

        private bool PhatSinh(string tk, ref string msg, DateTime ngayKS)
        {
            string sql = "select tk from BLTK where tk = '" + tk + "'";
            if (ngayKS != null && ngayKS != DateTime.MinValue)
                sql += " and NgayCT > '" + ngayKS.ToString("MM/dd/yyyy") + "'";
            DataTable dt = _data.DbData.GetDataTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                msg = "Đã phát sinh định khoản";
                return true;
            }
            if (ngayKS == null || ngayKS == DateTime.MinValue)
            {
                dt = _data.DbData.GetDataTable("select tk from OBTK where tk = '" + tk + "'");
                if (dt != null && dt.Rows.Count > 0)
                {
                    msg = "Đã khai báo số dư đầu kỳ";
                    return true;
                }
                dt = _data.DbData.GetDataTable(string.Format(@"select MaNV 
                                                from DMNV
                                                where TK1 = '{0}' or TKDU1 = '{0}' or TK2 = '{0}' or TKDU2 = '{0}' or TK3 = '{0}' or TKDU3 = '{0}'", tk));
                if (dt != null && dt.Rows.Count > 0)
                {
                    msg = "Đã khai báo nghiệp vụ";
                    return true;
                }
            }
            return false;
        }

        private void CapNhatPS(string newCode, string oldCode, DateTime ngayKS)
        {
            Database dbStruct = Database.NewStructDatabase();
            Database dbData = _data.DbData;
            string sysPackageID = Config.GetValue("sysPackageID").ToString();
            string tableName = "DMTK";
            string s = "select f.*,t.TableName from sysfield f, sysTable t" +
                " where f.refTable = '" + tableName + "' and f.RefName is not null and f.sysTableID = t.sysTableID and t.CollectType <> -1 and t.sysPackageID = " + sysPackageID +
                " union all select f.*,t.TableName from sysfield f, sysTable t" +
                " where f.refTable in (select TableName from sysTable st inner join sysField sf on st.sysTableID = sf.sysTableID where sf.refTable = '" + tableName + "' and sf.Type = 0 and st.CollectType = -1)" +
                " and f.sysTableID = t.sysTableID and t.CollectType <> -1 and t.sysPackageID = " + sysPackageID;
            DataTable dtRef = dbStruct.GetDataTable(s);
            foreach (DataRow dr in dtRef.Rows)
            {
                if (dr["TableName"].ToString() == tableName)    //khong sua tai khoan me trong bang DMTK
                    continue;
                if (ngayKS != null && ngayKS != DateTime.MinValue && dr["TableName"].ToString() == "OBTK")    //ko sua tk trong so du dau ky neu co ngay khoa so
                    continue;
                if (dr["RefName"] != DBNull.Value)      //khong phai view
                {
                    //vô hiệu toàn bộ ràng buộc khóa ngoại có liên quan
                    s = "alter table " + dr["TableName"].ToString() + " nocheck constraint " + dr["RefName"].ToString();
                    if (!dbData.UpdateByNonQuery(s))
                        break;
                }
                //cập nhật cho các bảng tham chiếu
                s = "update " + dr["TableName"].ToString() + " set " + dr["FieldName"].ToString() + " = '" + newCode +
                    "' where " + dr["FieldName"].ToString() + " = '" + oldCode + "'";
                if (ngayKS != DateTime.MinValue && dbStruct.GetDataTable(string.Format("select sysFieldID from sysField where sysTableID = {0} and FieldName = 'NgayCT'", dr["sysTableID"])).Rows.Count > 0)
                    s += " and NgayCT > '" + ngayKS.ToString("MM/dd/yyyy") + "'";
                if (!dbData.UpdateByNonQuery(s))
                    break;
            }
        }

        private void XoaSoDuTaiKhoan(string tk)
        {
            _data.DbData.UpdateByNonQuery("delete from OBTK where tk like '" + tk + "%'");
        }

        private void XoaSoDuCongNo(string tk)
        {
            _data.DbData.GetDataTable("delete from OBKH where tk like '" + tk + "%'");
        }

        public InfoCustomData Info
        {
            get { return _info; }
        }

        #endregion
    }
}
