using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System.Data;
using CDTDatabase;
using CDTLib;
using DevExpress.XtraGrid.Views.Grid;

namespace DanhSoCT
{
    public class DanhSoCT : ICReport
    {
        private DataCustomReport _data;
        private InfoCustomReport _info = new InfoCustomReport(IDataType.Report);
        #region ICReport Members

        public DataCustomReport Data
        {
            set { _data = value; }
        }

        public void Execute()
        {
            SimpleButton btnXuLy = _data.FrmMain.Controls.Find("btnXuLy", true)[0] as SimpleButton;
            btnXuLy.Text = "Cập nhật";
            btnXuLy.Click += new EventHandler(btnXuLy_Click);
        }

        void btnXuLy_Click(object sender, EventArgs e)
        {
            GridView gv = (_data.FrmMain.Controls.Find("gridControlReport", true)[0] as GridControl).MainView as GridView;
            DataView dv = gv.DataSource as DataView;
            dv.Table.AcceptChanges();
            dv.RowFilter = "[Chọn] = 0";
            if (dv.Count > 0)
            {
                dv.RowFilter = "";
                XtraMessageBox.Show("Vui lòng đánh dấu chọn tất cả chứng từ để cập nhật", Config.GetValue("PackageName").ToString());
                return;
            }
            dv.RowFilter = "";
            string s1 = "update {0} set SoPhieuThu = '{1}' where SoPhieuThu = '{2}' and NgayCT = '{3}'";
            string s2 = "update {0} set SoPhieuChi = '{1}' where SoPhieuChi = '{2}' and NgayCT = '{3}'";
            string s3 = "update {0} set SoHD = '{1}' where SoHD = '{2}' and NgayCT = '{3}'";
            string s4 = "update {0} set SoHDDV = '{1}' where SoHDDV = '{2}' and NgayCT = '{3}'";
            string s = @"update {0} set SoCT = '{1}' where {0}ID = '{2}'; 
                        update BLTK set SoCT = '{1}' where MTID = '{2}';  
                        update BLVT set SoCT = '{1}' where MTID = '{2}';";
            Database db = Database.NewDataDatabase();
            db.BeginMultiTrans();
            foreach (DataRowView drv in dv)
            {
                string mact = drv["MaCT"].ToString();
                string mtid = drv["MTID"].ToString();
                string tb = drv["TbName"].ToString();
                string soct = drv["Số mới"].ToString();
                string soctcu = drv["Số cũ"].ToString();
                string ngayct = drv["NgayCT"].ToString();
                switch (mact)   //cap nhat truong hop hoa don ban/mua 111
                {
                    case "PT":
                        db.UpdateByNonQuery(string.Format(s1, "MT31", soct, soctcu, ngayct));
                        db.UpdateByNonQuery(string.Format(s1, "MT32", soct, soctcu, ngayct));
                        break;
                    case "PC":
                        db.UpdateByNonQuery(string.Format(s2, "MT21", soct, soctcu, ngayct));
                        db.UpdateByNonQuery(string.Format(s2, "MT22", soct, soctcu, ngayct));
                        break;
                    case "HDB":
                        db.UpdateByNonQuery(string.Format(s3, "MT11", soct, soctcu, ngayct));
                        break;
                    case "HDV":
                        db.UpdateByNonQuery(string.Format(s4, "MT11", soct, soctcu, ngayct));
                        break;
                    case "PNM":
                        db.UpdateByNonQuery(string.Format(s3, "MT12", soct, soctcu, ngayct));
                        break;
                    case "MDV":
                        db.UpdateByNonQuery(string.Format(s4, "MT12", soct, soctcu, ngayct));
                        break;
                }
                db.UpdateByNonQuery(string.Format(s, tb, soct, mtid));
                if (db.HasErrors)
                    break;
            }
            if (!db.HasErrors)
            {
                db.EndMultiTrans();
                XtraMessageBox.Show("Đã đánh lại số chứng từ thành công", Config.GetValue("PackageName").ToString());
                dv.RowFilter = "[Chọn] = 0";
            }
        }

        public InfoCustomReport Info
        {
            get { return _info; }
        }

        #endregion
    }
}
