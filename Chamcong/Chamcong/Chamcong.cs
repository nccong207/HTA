using System;
using System.Collections.Generic;
using System.Text;
using Plugins;

namespace Chamcong
{
    public class Chamcong:IC
    {
        #region IC Members

         public void Execute(System.Data.DataRow drMenu)
        {
            int menuID = Int32.Parse(drMenu["MenuPluginID"].ToString());
            if (_lstInfo[0].CType == ICType.Custom && _lstInfo[0].MenuID == menuID)
            {
                frmChamcong frm = new frmChamcong();
                frm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                frm.Text = drMenu["MenuName"].ToString();
                frm.ShowDialog();
            }
        }

          public List<InfoCustom> LstInfo
        {
            get { return _lstInfo; }
        }        

        private List<InfoCustom> _lstInfo = new List<InfoCustom>();
        public Chamcong()
        {
            InfoCustom ic = new InfoCustom(1011, "Chấm công", "Tổng hợp");
            _lstInfo.Add(ic);
        }
        #endregion
    }
}
