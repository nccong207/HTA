using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTLib;
using CDTControl;
using CDTSystem;
using FormFactory;
using DataFactory;
using ErrorManager;

namespace CDT
{
    public class Command
    {
        private FrmVisualUI visualUI;

        public FrmVisualUI VisualUI
        {
            get { return visualUI; }
            set { visualUI = value; }
        }
        private PluginManager _pm;
        private Form _parentForm;
        private SysMenu _sysMenu;
        private List<Char> _lstHotKey = new List<char>();
        private List<Char> _lstCurHotKey = new List<char>();

        public Command(PluginManager pm, Form parentForm, SysMenu sysMenu)
        {
            _pm = pm;
            _parentForm = parentForm;
            _sysMenu = sysMenu;
            _lstCurHotKey.AddRange("S-,. " +
                "áàạảãâấầậẩẫăắằặẳẵÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ" +
                "éèẹẻẽêếềệểễÉÈẸẺẼÊẾỀỆỂỄ" +
                "óòọỏõôốồộổỗơớờợởỡÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ" +
                "úùụủũưứừựửữÚÙỤỦŨƯỨỪỰỬỮ" +
                "íìịỉĩÍÌỊỈĨđĐýỳỵỷỹÝỲỴỶỸ".ToCharArray());
        }

        public void ExecuteCommand(DataRow dr, DataTable dtModule)
        {
            if (dr == null)
                return;
            Config.NewKeyValue("sysMenuID", dr["SysMenuID"]);
            Config.NewKeyValue("MenuName", dr["MenuName"]);
            try
            {
                if (dr["sysTableID"].ToString() != string.Empty)
                    ShowTable(dr, FormAction.Default);
                else
                {
                    if (dr["sysReportID"].ToString() != string.Empty)
                        ShowReport(dr);
                    else
                    {
                        if (dr["MenuPluginID"].ToString() != string.Empty)
                            ExecutePlugin(dr);
                        else
                            ShowModule(dr, dtModule);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.UnknowError(ex);
            }
        }

        private void ExecutePlugin(DataRow drData)
        {
            _pm.ExecuteIC(drData);
        }

        public void ShowTable(DataRow drTable, FormAction frmAct)
        {
            if (drTable == null)
                return;
            if (drTable["TableName"].ToString().ToUpper() == "SYSUSERMENU") //hook sysUserMenu by FrmDecentralization
            {
                FrmDecentralization f = new FrmDecentralization();
                f.ShowDialog();
                return;
            }
            int bType = Int32.Parse(drTable["Type"].ToString());
            FormType formType;
            switch (bType)
            {
                case 1:
                case 2:
                    formType = FormType.Single;
                    break;
                case 3:
                    formType = FormType.MasterDetail;
                    break;
                case 4:
                case 5:
                    formType = FormType.Detail;
                    break; ;
                case 7:
                    formType = FormType.MultiDetail;
                    break;
                default:
                    formType = FormType.Single;
                    break;
            }
            Form frm = MdiExists(Config.GetValue("Language").ToString() == "0" ? drTable["MenuName"].ToString() : drTable["MenuName2"].ToString());
            if (frm != null)
                frm.Close();
            frm = FormFactory.FormFactory.Create(formType, frmAct, drTable);
            frm.MdiParent = _parentForm;
            frm.Show();
        }

        public void ShowReport(DataRow drReport)
        {
            Form frm = MdiExists(Config.GetValue("Language").ToString() == "0" ? drReport["MenuName"].ToString() : drReport["MenuName2"].ToString());
            if (frm != null)
                frm.Activate();
            else
            {
                frm = FormFactory.FormFactory.Create(FormType.Report, drReport);
                frm.MdiParent = _parentForm;
                frm.Show();
            }
        }

        public void ShowModule(DataRow drModule, DataTable dtModule)
        {
            if (drModule["MenuName"].ToString() == "Xem toàn bộ quyền sử dụng") //hook menu Xem quyen su dung by FrmXemQSD
            {
                FrmXemQSD frm = new FrmXemQSD();
                frm.MdiParent = _parentForm;
                frm.Show();
                return;
            }
            _lstHotKey.Clear();
            _lstHotKey.AddRange(_lstCurHotKey.ToArray());
            bool first = false;
            if (visualUI == null || !visualUI.Visible)
            {
                first = true;
            }
            if (visualUI == null)
            {
                visualUI = new FrmVisualUI(_sysMenu, _pm);
                visualUI.Cmd = this;
                visualUI.MdiParent = _parentForm;
            }
            if (drModule != null)
            {
                visualUI.SysMenuParent = Int32.Parse(drModule["sysMenuID"].ToString());
                visualUI.Text = Config.GetValue("Language").ToString() == "0" ? drModule["MenuName"].ToString() : drModule["MenuName2"].ToString();
            }
            else
            {
                visualUI.SysMenuParent = -1;
                visualUI.Text = "Bàn làm việc";
            }
            visualUI.RefreshData(dtModule);
            if (first)
            {
                visualUI.Show();
            }
            else
                visualUI.Activate();
        }

        public string CreateHotKey(string caption)
        {
            string tmp = caption;
            caption = caption.ToUpper();
            for (int i = 0; i < caption.Length; i++)
            {
                if (!_lstHotKey.Contains(caption[i]))
                {
                    tmp = tmp.Insert(i, "&");
                    _lstHotKey.Add(caption[i]);
                    break;
                }
            }
            return tmp;
        }

        public string CreateHotKeyForMenu(string caption)
        {
            string tmp = caption;
            caption = caption.ToUpper();
            for (int i = 0; i < caption.Length; i++)
            {
                if (!_lstCurHotKey.Contains(caption[i]))
                {
                    tmp = tmp.Insert(i, "&");
                    _lstCurHotKey.Add(caption[i]);
                    break;
                }
            }
            return tmp;
        }

        private Form MdiExists(string caption)
        {
            foreach (Form frm in _parentForm.MdiChildren)
                if (frm.Text == caption)
                    return frm;
            return null;
        }
    }
}
