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
    public partial class frmKhachHang : DevExpress.XtraEditors.XtraForm
    {
        public string makh = string.Empty;
        public frmKhachHang(string makh)
        {
            InitializeComponent();
            this.makh = makh;
        }
        Database db = Database.NewDataDatabase();
        DataTable dtKH=new DataTable();
        public DataView dvKh;
        bool isClick = false;
        private void btnCancel_Click(object sender, EventArgs e)
        {
            dvKh = null;
            isClick = true;
            this.Close();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            string sql = "select * from DMKH where isKH='1' and InActive='0' ";
            if(makh!=string.Empty)
                sql+="and MaKH not in (" + makh + ") ";
            sql += "order by MaKH";
            dtKH=db.GetDataTable(sql);            
            dtKH.Columns.Add("check",typeof(Boolean));
            dvKh = new DataView(dtKH);
            gcKhachHang.DataSource = dtKH;
            gvKhachHang.BestFitColumns();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            dvKh.RowFilter = "check='True'";
            isClick = true;
            this.Close();
        }

        private void chkAll_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (chkAll.CheckState == CheckState.Unchecked)
            {
                foreach (DataRow row in dtKH.Rows)
                {
                    row["check"] = "True";
                }
            }
            else
            {
                foreach (DataRow row in dtKH.Rows)
                {
                    row["check"] = "False";
                }
            }
        }

        private void frmKhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isClick)
                dvKh = null;
        }
    }
}