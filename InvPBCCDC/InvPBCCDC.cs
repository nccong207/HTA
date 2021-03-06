using System;
using System.Collections.Generic;
using Plugins;
using System.Data;

namespace InvPBCCDC
{
    public class InvPBCCDC : IC
    {
        #region IC Members

        public void Execute(DataRow drMenu)
        {
            int menuID = Int32.Parse(drMenu["MenuPluginID"].ToString());
            if (_lstInfo[0].CType == ICType.Custom && _lstInfo[0].MenuID == menuID)
            {
                Filter frm = new Filter();
                frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                frm.Text = _lstInfo[0].MenuName;
                frm.ShowDialog();
            }
        }

        public List<InfoCustom> LstInfo
        {
            get { return _lstInfo; }
        }

        #endregion

        private List<InfoCustom> _lstInfo = new List<InfoCustom>();
        public InvPBCCDC()
        {
            InfoCustom ic = new InfoCustom(1000, "Phân bổ chi phí trả trước", "Tổng hợp");
            _lstInfo.Add(ic);
        }
    }
}