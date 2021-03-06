using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Data;

namespace TMBCTC
{
    public class TMBCTC : IC
    {
        #region IC Members

        public void Execute(DataRow drMenu)
        {
            int menuID = Int32.Parse(drMenu["MenuPluginID"].ToString());
            if (_lstInfo[0].CType == ICType.Custom && _lstInfo[0].MenuID == menuID)
            {
                FrmFilter f = new FrmFilter();
                f.ShowDialog();
            }
        }

        public List<InfoCustom> LstInfo
        {
            get { return _lstInfo; }
        }

        #endregion
        private List<InfoCustom> _lstInfo = new List<InfoCustom>();
        public TMBCTC()
        {
            InfoCustom ic = new InfoCustom(1006, "Thuyết minh báo cáo tài chính", "Tổng hợp");
            _lstInfo.Add(ic);
        }
    }
}
