using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Data;
namespace GLKC
{
    public class GLKC : IC
    {
        #region IC Members

        public void Execute(DataRow drMenu)
        {
            int menuID = Int32.Parse(drMenu["MenuPluginID"].ToString());
            if (_lstInfo[0].CType == ICType.Custom && _lstInfo[0].MenuID == menuID)
            {
                GLChooseTrans frm = new GLChooseTrans(drMenu["ExtraSql"].ToString());
                frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                frm.Text = drMenu["MenuName"].ToString();
                frm.ShowDialog();
            }
        }

        public List<InfoCustom> LstInfo
        {
            get { return _lstInfo; }
        }

        #endregion

        private List<InfoCustom> _lstInfo = new List<InfoCustom>();
        public GLKC()
        {
            InfoCustom ic = new InfoCustom(1002, "Kết chuyển cuối kỳ", "Tổng hợp");
            _lstInfo.Add(ic);
        }
    }
} 
