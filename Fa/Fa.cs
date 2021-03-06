using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Data;
namespace Fa
{
    public class Fa : IC
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
        public Fa()
        {
            InfoCustom ic = new InfoCustom(1001, "Khấu hao tài sản cố định", "Tài sản cố định");
            _lstInfo.Add(ic);
        }
    }


}
