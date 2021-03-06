using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Data;

namespace HoaHongSales
{
    public class HoaHongSales : IC
    {
        #region IC Members

        public void Execute(DataRow drMenu)
        {
            int menuID = Int32.Parse(drMenu["MenuPluginID"].ToString());
            if (_lstInfo[0].CType == ICType.Custom && _lstInfo[0].MenuID == menuID)
            {
                FrmTinhToan frm = new FrmTinhToan();
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
        public HoaHongSales()
        {
            InfoCustom ic = new InfoCustom(3000, "Quản lý hoa hồng nhân viên kinh doanh", "Bán hàng phải thu");
            _lstInfo.Add(ic);
        }
    }
}
