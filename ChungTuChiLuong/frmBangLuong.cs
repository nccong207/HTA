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

namespace ChungTuChiLuong
{
    public partial class frmBangLuong : DevExpress.XtraEditors.XtraForm
    {
        public frmBangLuong()
        {
            InitializeComponent();
        }
        Database data = Database.NewDataDatabase();
        public string id = "";
        public int thang = 0;
        public int nam = 0;
        public double tongtien = 0;   

        private void frmBangLuong_Load(object sender, EventArgs e)
        {
            string sql = " select * from BangLuong BL where 'CTL_'+cast(BL.IDBL as nvarchar) not in " +
                         " (select RefValue from MT12 where RefValue is not null  " +
                         " union all select RefValue from MT16 where RefValue is not null)";
            DataTable dt = data.GetDataTable(sql);
            if (dt.Rows.Count == 0)
            {
                XtraMessageBox.Show("Không có bảng lương hoặc các bảng lương đều đã được xử lý!");
                this.Close();
            }
            gcBangLuong.DataSource = dt;
            gvBangLuong.BestFitColumns();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {              
            int[] seletedrow = gvBangLuong.GetSelectedRows();
            if (seletedrow == null)
            {
                XtraMessageBox.Show("Vui lòng chọn bảng lương để hạch toán");               
            }
            else
            {
                for (int i = 0; i < seletedrow.Length; i++)
                {
                    DataRow row = gvBangLuong.GetDataRow(i);
                    id = row["IDBL"].ToString();
                    thang = Convert.ToInt16( row["Thang"].ToString());
                    nam = Convert.ToInt32(row["Nam"].ToString());
                    tongtien = Convert.ToDouble(row["TongThucLinh"].ToString());
                    this.Close();
                    break;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}