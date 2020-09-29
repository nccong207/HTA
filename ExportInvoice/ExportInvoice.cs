using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using CDTDatabase;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using System.Data;
using System.Windows.Forms;
using CDTControl;
using System.Diagnostics;
using System.IO;

namespace ExportInvoice
{
    public class ExportInvoice : ICControl
    {
        Database db = Database.NewDataDatabase();
        InfoCustomControl info = new InfoCustomControl(IDataType.MasterDetailDt);
        DataCustomFormControl data;
        public void AddEvent()
        {
            LayoutControl lcMain = data.FrmMain.Controls.Find("lcMain", true)[0] as LayoutControl;
            SimpleButton btnXuatFile = new SimpleButton();
            btnXuatFile.Text = "Xuất ra Excel";
            btnXuatFile.Name = "btnXuatFile";
            btnXuatFile.Click += new EventHandler(btnXuatFile_Click);
            LayoutControlItem lci2 = lcMain.AddItem("", btnXuatFile);
            lci2.Name = "cusXuatFile";
        }

        private void btnXuatFile_Click(object sender, EventArgs e)
        {
            //if (gvMain.Editable)
            //{
            //    XtraMessageBox.Show("Vui lòng thực hiện khi đã lưu hóa đơn bán hàng",
            //        Config.GetValue("PackageName").ToString());
            //    return;
            //}

            DataRow drCurrent = (data.BsMain.Current as DataRowView).Row;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.RestoreDirectory = true;
            sfd.FileName = drCurrent["SoHoaDon"].ToString();
            sfd.Filter = "Excel files (*.xls)|*.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string sql = @"SELECT m.SoHoaDon, m.NgayCT, m.OngBa, m.TenKH, m.DiaChi, MST = '''' + kh.MST, d.TenVT,
                                '' as Dai, '' as Rong, '' as Day,
                                dvt.TenDVT, d.SoLuong, d.Gia, d.PS, th.ThueSuat, 
                                ROUND((d.PS * th.ThueSuat)/100, 0) as TienThue, ROUND((d.PS * (th.ThueSuat + 100))/100, 0) as TongTien, kh.Email,
                                m.MaKH, '' as SoSO, '' as So_PGH, '' as SoPO
                                FROM MT32 m JOIN DT32 d ON m.MT32ID = d.MT32ID	
                                LEFT JOIN DMKH kh ON m.MaKH = kh.MaKH
                                LEFT JOIN DMDVT dvt ON d.MaDVT = dvt.MaDVT
                                LEFT JOIN DMThueSuat th ON m.MaThue = th.MaThue
                            WHERE m.MT32ID = '{0}' ORDER BY d.Stt";
                Database db = Database.NewDataDatabase();

                DataTable dtData = db.GetDataTable(string.Format(sql, drCurrent["MT32ID"]));
                string f = Application.StartupPath + "\\Reports\\HTA\\MauHoaDon.xls";

                ExportExcel exportExcel = new ExportExcel(f, sfd.FileName, dtData);
                if (exportExcel.Export() && File.Exists(sfd.FileName))
                    Process.Start(sfd.FileName);
            }
        }


        public DataCustomFormControl Data
        {
            set { data = value; }
        }

        public InfoCustomControl Info
        {
            get { return info; }
        }
    }
}
