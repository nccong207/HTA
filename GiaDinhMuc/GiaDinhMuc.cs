using System;
using System.Collections.Generic;
using System.Text;
using Plugins;

namespace GiaDinhMuc
{
    public class GiaDinhMuc : ICForm
    {
        private List<InfoCustomForm> _lstInfo = new List<InfoCustomForm>();
        private DataCustomFormControl _data;

        public GiaDinhMuc()
        {
            InfoCustomForm info = new InfoCustomForm(IDataType.Detail, 1001, "Áp giá nguyên liệu bình quân cuối kỳ vào giá định mức",
                "Apply material average cost into norms price", "DFNVL");
            _lstInfo.Add(info);
        }
        #region ICForm Members

        public DataCustomFormControl Data
        {
            set { _data = value; }
        }

        public void Execute(int menuID)
        {
            if (menuID == _lstInfo[0].MenuID)
            {
                FrmApGiaDM f = new FrmApGiaDM();
                f.ShowDialog();
            }
        }

        public List<InfoCustomForm> LstInfo
        {
            get { return _lstInfo; }
        }

        #endregion
    }
}
