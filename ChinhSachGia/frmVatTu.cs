using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Plugins;
using CDTDatabase;

namespace ChinhSachGia
{
    public partial class frmVatTu : DevExpress.XtraEditors.XtraForm
    {
        public string mavt = string.Empty;
        public frmVatTu(string mavt)
        {
            InitializeComponent();
            this.mavt = mavt;
        }
        Database db = Database.NewDataDatabase();
        DataTable dtVattu=new DataTable();
        public DataView dvVattu;
        bool isClick = false;
        private void frmVatTu_Load(object sender, EventArgs e)
        {
            chkAll.Checked = false;
            string sql = "select VT.MaVT,VT.TenVT,VT.GiaBanNT,VT.GiaBan " +
                         "from dmvt VT where LoaiVT in (1,4)";
            if(mavt!=string.Empty)
                sql+=" and VT.MaVT not in ("+mavt+")";
            dtVattu = db.GetDataTable(sql);
            dvVattu = new DataView(dtVattu);
            dtVattu.Columns.Add("check",typeof(Boolean));
            gcVattu.DataSource = dtVattu;
            gvVattu.BestFitColumns();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dvVattu = null;
            isClick = true;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            dvVattu.RowFilter = "check='True'";
            isClick = true;
            this.Close();
        }        

        private void chkAll_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (chkAll.CheckState == CheckState.Unchecked)
            {
                foreach (DataRow row in dtVattu.Rows)
                {
                    row["check"] = "True";
                }
            }
            else
            {
                foreach (DataRow row in dtVattu.Rows)
                {
                    row["check"] = "False";
                }
            }
        }

        private void frmVatTu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClick)
                dvVattu = null;
        }
    }
}