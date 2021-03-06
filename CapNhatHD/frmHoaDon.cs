using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using CDTDatabase;
using CDTLib;

namespace CapNhatHD
{
    public partial class frmHoaDon : DevExpress.XtraEditors.XtraForm
    {
        int id;
        public frmHoaDon(int ID)
        {
            InitializeComponent();
            id = ID;
        }
        Database db = Database.NewDataDatabase();
        Database dbCDT = Database.NewStructDatabase();

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            string sql = "";            

            if (id == 1207) // mua hang
            {
                //xóa dữ liệu
                sql = "delete from dt26 where mt26id not in (select mt26id from mt26)";
                db.UpdateByNonQuery(sql);

                sql = "select *, isnull((select sum(TT) from dt26 DT where HD.MT21ID=DT.MT21ID),0) as ThucThu " +
                      "from wHoaDonMua HD " +
                      "where ConLai <> (Ttien - isnull((select sum(TT) from dt26 DT where DT.MT21ID = HD.MT21ID),0)) " +
                      "order by NgayCT desc";
                DataTable dt = db.GetDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Không có hóa đơn nào!",Config.GetValue("PackageName").ToString());
                    this.Close();
                }
                gcHoaDon.DataSource = dt;
                gvHoaDon.BestFitColumns();
            }
            else // ban hang
            {
                //xóa dữ liệu
                sql = "delete from dt34 where mt34id not in (select mt34id from mt34)";
                db.UpdateByNonQuery(sql);

                sql = " select *, isnull((select sum(TT) from dt34 DT where HD.MT31ID=DT.MT31ID),0) as ThucThu "+
                      " from wHoaDonBan HD "+
                      " where ConLai <> (Ttien - isnull((select sum(TT) from dt34 DT where DT.MT31ID = HD.MT31ID),0)) "+
                      "order by NgayCT desc";
                DataTable dt = db.GetDataTable(sql);
                if (dt.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Không có hóa đơn nào!", Config.GetValue("PackageName").ToString());
                    this.Close();
                }
                gcHoaDon.DataSource = dt;
                gvHoaDon.BestFitColumns();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        } 

        private void btnOK_Click(object sender, EventArgs e)
        {            
            DataTable dt = gcHoaDon.DataSource as DataTable;
            string sql = "";
            DataTable dtSub = new DataTable();
            foreach (DataRow row in dt.Rows)
            {
                sql = "select Top 1 * from sysTable where MaCT = '" + row["MaCT"].ToString() + "'";
                dtSub = dbCDT.GetDataTable(sql);
                if (dtSub.Rows.Count == 0)
                    continue;
                string pkRow = id == 1207 ? "MT21ID" : "MT31ID";
                string tableName = dtSub.Rows[0]["MasterTable"].ToString().Trim();
                string pk = tableName + "ID";
                sql = String.Format("update {0} set DaTT = '{1}' where {2} = '{3}'", tableName, row["ThucThu"].ToString().Replace(",", "."), pk, row[pkRow].ToString());
                db.UpdateByNonQuery(sql);
            }
            XtraMessageBox.Show("Cập nhật thành công!", Config.GetValue("PackageName").ToString());
            this.Close();
        }

        private void gcHoaDon_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

       
    }
}