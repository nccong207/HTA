using System;
using System.Collections.Generic;
using System.Text;
using Plugins;

namespace CapNhatHD
{
    public class CapNhatHD:IC
    {
        #region IC Members

        private List<InfoCustom> _lstInfo = new List<InfoCustom>();

        public CapNhatHD() 
        {
            InfoCustom ic = new InfoCustom(1207, "Tính lại số tiền phải trả của hóa đơn.", "Mua hàng phải trả");
            InfoCustom ic1 = new InfoCustom(1208, "Tính lại số tiền phải thu của hóa đơn.", "Bán hàng phải thu");
            _lstInfo.Add(ic);
            _lstInfo.Add(ic1);
        }

        public void Execute(System.Data.DataRow drMenu)
        {
            int menuID = Int32.Parse(drMenu["MenuPluginID"].ToString());

            if (menuID == _lstInfo[0].MenuID && _lstInfo[0].CType==ICType.Custom)
            {
                frmHoaDon frm = new frmHoaDon(menuID);
                frm.Text = "Danh sách hóa đơn mua";
                frm.ShowDialog();
            }
            else if (menuID == _lstInfo[1].MenuID && _lstInfo[0].CType == ICType.Custom)
            {
                frmHoaDon frm = new frmHoaDon(menuID);
                frm.Text = "Danh sách hóa đơn bán";
                frm.ShowDialog();
            }
        } 

        public List<InfoCustom> LstInfo
        {
            get { return _lstInfo; }
        }

        #endregion
    }
}
