using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Data;

namespace DonHang
{
    public class DonHang : IC
    {
        #region IC Members

        private List<InfoCustom> _lstInfo = new List<InfoCustom>();
        public void Execute(DataRow drMenu)
        {
            int menuID = Int32.Parse(drMenu["MenuPluginID"].ToString());
            FrmDonHang frm = new FrmDonHang(drMenu["ExtraSQL"].ToString());
            frm.Text = drMenu["MenuName"].ToString();
            frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            frm.ShowDialog();
        }

        public List<InfoCustom> LstInfo
        {
            get { return _lstInfo; }
        }

        #endregion

        public DonHang()
        {
            InfoCustom ic = new InfoCustom(6001, "Theo dõi đơn hàng", "Xử lý đơn hàng");
            _lstInfo.Add(ic);
        }
    }
}
