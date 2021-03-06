using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Data;
namespace GiaThanh
{
    class GiaThanh : IC
    {
        #region IC Members

        public void Execute(DataRow drMenu)
        {
            int menuID = Int32.Parse(drMenu["MenuPluginID"].ToString());
            switch (menuID)
            { 
                case 5001:
                    Coster cost = new Coster(drMenu["ExtraSql"].ToString());
                    break;
                case 5002:
                    UpdateddFilter frm = new UpdateddFilter();
                    frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    frm.Text = _lstInfo[1].MenuName;
                    frm.ShowDialog();
                    break;
            }
        }

        public List<InfoCustom> LstInfo
        {
            get { return _lstInfo; }
        }

        #endregion

        private List<InfoCustom> _lstInfo = new List<InfoCustom>();
        public GiaThanh()
        {
            InfoCustom ic1 = new InfoCustom(5001, "Tính giá thành", "Chi phí giá thành");
            InfoCustom ic2 = new InfoCustom(5002, "Tính dở dang cuối kỳ", "Chi phí giá thành");
            _lstInfo.AddRange(new InfoCustom[] { ic1, ic2 });
        }
    }
}
 