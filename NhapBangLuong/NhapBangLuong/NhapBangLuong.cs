using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Plugins;
using CDTDatabase;
using CDTLib;
using DevExpress;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;


namespace NhapBangLuong
{
    public class NhapBangLuong:ICControl
    {
        private DataCustomFormControl _data;
        private InfoCustomControl _info = new InfoCustomControl(IDataType.MasterDetailDt);
        Database db = Database.NewDataDatabase();
        DataTable dtDMNhanVien;
        DataView dvNhanVien;
        DataTable dtBieuThue;       

        #region ICControl Members

        public void AddEvent()
        {
            _data.FrmMain.Shown += new System.EventHandler(FrmMain_Shown);
            _data.BsMain.DataSourceChanged += new EventHandler(BsMain_DataSourceChanged);
            BsMain_DataSourceChanged(_data.BsMain, new EventArgs());
        }

        void BsMain_DataSourceChanged(object sender, EventArgs e)
        {
            DataSet ds = _data.BsMain.DataSource as DataSet;
            if (ds != null)
                ds.Tables[1].ColumnChanged += new DataColumnChangeEventHandler(NhapBangLuong_ColumnChanged);
        }

        void NhapBangLuong_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            //Tinh lại bảo hiểm, và thuế TNCN, Thực lĩnh  khi Tổng lương thay đổi        
            //if (e.Column.ColumnName.ToString().ToUpper() == "LUONGCB")
            //{               
            //    dvNhanVien.RowFilter = "MaNV='" + e.Row["MaNV"].ToString() + "'";
            //    foreach (DataRowView drv in dvNhanVien)
            //    {
            //        if (Convert.ToBoolean(drv.Row["TinhBH"].ToString()))
            //        {                                               
            //            e.Row["BHYT"] = Convert.ToDouble(e.Row["LuongCB"].ToString()) * 1.5 / 100;
            //            e.Row["BHXH"] = Convert.ToDouble(e.Row["LuongCB"].ToString()) * 8 / 100;
            //            e.Row["BHTN"] = Convert.ToDouble(e.Row["LuongCB"].ToString()) * 1 / 100;
            //        }
            //        if (Convert.ToBoolean(drv.Row["TinhThue"].ToString()))
            //        {
            //            double TNTinhThue = Convert.ToDouble(e.Row["TNTinhThue"].ToString());
            //            double ThueTNCN = TinhThue(TNTinhThue);
            //            e.Row["ThueTNCN"] = Math.Round(ThueTNCN, 0);
            //        }
            //        //Thực lĩnh
            //        e.Row["ThucLinh"] = Convert.ToDouble("0" + e.Row["TongLuong"].ToString()) - Convert.ToDouble("0" + e.Row["TamUng"].ToString()) - Convert.ToDouble("0" + e.Row["BHYT"].ToString()) - Convert.ToDouble("0" + e.Row["BHTN"].ToString()) - Convert.ToDouble("0" + e.Row["BHXH"].ToString()) - Convert.ToDouble("0" + e.Row["ThueTNCN"].ToString());
            //    }
            //}

            ////Tạm ứng thay đổi -> tính lại thực lãnh
            //if (e.Column.ColumnName.ToString().ToUpper() == "TAMUNG")
            //    e.Row["ThucLinh"] = Convert.ToDouble("0" + e.Row["TongLuong"].ToString()) - Convert.ToDouble("0" + e.Row["TamUng"].ToString()) - Convert.ToDouble("0" + e.Row["BHYT"].ToString()) - Convert.ToDouble("0" + e.Row["BHTN"].ToString()) - Convert.ToDouble("0" + e.Row["BHXH"].ToString()) - Convert.ToDouble("0" + e.Row["ThueTNCN"].ToString());

            //Thu nhập tính thuế thay đổi -> tính lại thuế TNCN và Thực Lĩnh
            if (e.Column.ColumnName.ToString().ToUpper() == "TNTINHTHUE")
            {
                dvNhanVien.RowFilter = "MaNV='" + e.Row["MaNV"].ToString() + "'";
                foreach (DataRowView drv in dvNhanVien)
                {
                    if (Convert.ToBoolean(drv.Row["TinhThue"].ToString()))
                    {
                        double TNTinhThue = Convert.ToDouble(e.Row["TNTinhThue"].ToString());
                        double ThueTNCN = TinhThue(TNTinhThue);
                        e.Row["ThueTNCN"] = Math.Round(ThueTNCN, 0);

                        //e.Row["ThucLinh"] = Convert.ToDouble("0" + e.Row["TongLuong"].ToString()) - Convert.ToDouble("0" + e.Row["TamUng"].ToString()) - Convert.ToDouble("0" + e.Row["BHYT"].ToString()) - Convert.ToDouble("0" + e.Row["BHTN"].ToString()) - Convert.ToDouble("0" + e.Row["BHXH"].ToString()) - Convert.ToDouble("0" + e.Row["ThueTNCN"].ToString());
                    }
                }
            }
        }

        //Tính thuế TNCN
        double TinhThue(double thunhap)
        {
            double temp=thunhap;
            double TongThue=0;
            foreach (DataRow row in dtBieuThue.Rows)
            {
                if (temp > 0)
                {
                    double TNDen=Convert.ToDouble(row["ThuNhapDen"].ToString());
                    if (temp - TNDen > 0 && TNDen !=0 )
                    {
                        temp -= TNDen;
                        TongThue += TNDen / 100 * Convert.ToDouble(row["MucThue"].ToString());
                    }
                    else
                    {
                        TongThue += temp / 100 * Convert.ToDouble(row["MucThue"].ToString());
                        temp = 0;
                    }
                }
                else
                    break;
            }
            return TongThue;
        }

        void FrmMain_Shown(object sender, EventArgs e)
        {
            int nam = 0;
            if (Config.GetValue("NamLamViec") != null)
                nam = Convert.ToInt32(Config.GetValue("NamLamViec").ToString());

            //lay table BieuThueTNCN
            dtBieuThue = db.GetDataTable("select * from BieuThueTNCN order by ThuNhapTu ASC");            

            DataRowView drv = _data.BsMain.Current as DataRowView;

            if (drv.Row.RowState == DataRowState.Added)
            {
                frmShow frm = new frmShow();
                frm.ShowDialog();
                int thang = frm.thang;
                int ngaycong = frm.ngaycong;
                if (nam != 0&&thang!=0&& ngaycong!=0)
                {
                    //lay table nhan vien
                    string sql = @"select *  from DMNhanVien NV   
                                    where (NV.DaNV=0 or (NV.DaNV = 1 and month(NV.NgayNV) > {0} and year(NV.NgayNV) >= {1}))
	                                     and month(NV.NgayLV) <= {0} and year(NV.NgayLV) <= {1}";
                    dtDMNhanVien = db.GetDataTable(string.Format(sql, thang, nam));
                    dvNhanVien = new DataView(dtDMNhanVien);
                    //khoi tao gridview
                    GridView gvBLCT = (_data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl).MainView as GridView;
                    TextEdit txtThang=(_data.FrmMain.Controls.Find("Thang",true)[0] as TextEdit) as TextEdit;
                    TextEdit txtNam = (_data.FrmMain.Controls.Find("Nam", true)[0] as TextEdit) as TextEdit;
                    TextEdit txtTSNgaycong = (_data.FrmMain.Controls.Find("TSNgayCong", true)[0] as TextEdit) as TextEdit;

                    txtThang.Properties.ReadOnly = true;
                    txtNam.Properties.ReadOnly = true;
                    txtTSNgaycong.Properties.ReadOnly = true;

                    DataTable dt = createTableLuong(thang, nam, ngaycong);
                    addRows(dt, gvBLCT);
                    setEdit(gvBLCT);  
                    //
                    drv["Thang"] = thang.ToString();
                    drv["Nam"] = nam.ToString();
                    drv["TSNgayCong"] = ngaycong.ToString();                    
                }
                else
                    XtraMessageBox.Show("Thông tin về thời gian lập bảng lương chưa đầy đủ!");
            }
            //không hỗ trợ cập nhật lại bảng lương
            //else if (drv.Row.RowState == DataRowState.Modified)
            //{
            //    //Lay bang chi tiet luong
            //    GridView gvBLCT = (_data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl).MainView as GridView;
            //    if (gvBLCT.OptionsBehavior.Editable && gvBLCT.DataRowCount > 0)
            //    {
            //        DialogResult result = XtraMessageBox.Show("Bạn có muốn cập nhật lại bảng lương này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (result == DialogResult.Yes)
            //        {
            //            for (int i = 0; i < gvBLCT.DataRowCount; i++)
            //                gvBLCT.DeleteRow(i);                        
            //        }
            //    }
            //}
        }       

        //Cho edit columns hay khong
        void setEdit(GridView gv)
        {
            gv.Columns["MaNV"].OptionsColumn.AllowEdit = false;
            //gv.Columns["TenNV"].OptionsColumn.AllowEdit = false;
            gv.Columns["BHYT"].OptionsColumn.AllowEdit = false;
            gv.Columns["BHTN"].OptionsColumn.AllowEdit = false;
            gv.Columns["BHXH"].OptionsColumn.AllowEdit = false;
            gv.Columns["TongLuong"].OptionsColumn.AllowEdit = false;
            gv.Columns["ThucLinh"].OptionsColumn.AllowEdit = false;
            gv.Columns["TNTinhThue"].OptionsColumn.AllowEdit = false;
            gv.Columns["ThueTNCN"].OptionsColumn.AllowEdit = false;
        }

        //Tạo bảng lương
        DataTable createTableLuong(int thang, int nam, int ngaycong)
        {
            string sql = @"select NV.*,0.0 as LCB,0.0 as PC, 0.0 as TNKhac,0.0 as TU, 0.0 as GTGC, 0.0 as TL, 0.0 as TNTT,
                                    0.0 as BHYT, 0.0 as BHXH, 0.0 as BHTN, 0.0 as ThueTNCN, 0.0 as ThucLinh  from DMNhanVien NV   
                            where (NV.DaNV=0 or (NV.DaNV = 1 and month(NV.NgayNV) > {0} and year(NV.NgayNV) >= {1}))
	                                 and month(NV.NgayLV) <= {0} and year(NV.NgayLV) <= {1}";
            DataTable dtNhanvien = db.GetDataTable(string.Format(sql, thang, nam));

            //du lieu tong cong theo tung loai cong
            sql = " select NV.MaNV,LC.KyHieu,LC.LoaiCongLam,LC.TienCong,sum(CC.SoCong) as TongCong, NV.LuongCB, " +
                        " (NV.LuongCB* sum(CC.SoCong)) as LuongNgay, (LC.TienCong*sum(CC.SoCong)) as LuongKhac " +
                        " from DMNhanVien NV inner join ChamCong CC on CC.MaNV=NV.MaNV " +
                        " inner join LoaiCong LC on LC.ID=CC.LoaiCong " +
                        " where year(CC.Ngay)='" + nam + "' and month(CC.Ngay)='" + thang + "' " +
                        " group by NV.MaNV,LC.KyHieu,LC.LoaiCongLam,NV.LuongCB,LC.TienCong";
            DataTable dtChiTietCong = db.GetDataTable(sql);
            DataView dvChiTietCong = new DataView(dtChiTietCong);

            //danh sach bien 
            double LuongThucLam = 0, PC = 0, GTGC = 0, TL = 0, BHYT = 0, BHXH = 0, BHTN = 0, ThueTNCN = 0, ThucLinh = 0, TNTT = 0;

            //Tinh Luong
            foreach (DataRow row in dtNhanvien.Rows)
            {
                //DataRow[] dr= dtChiTietCong.Select("MaNV='"+row["MaNV"].ToString()+"' and LoaiCongLam=N'Ngày'");              
                LuongThucLam = BHYT = BHXH = BHTN = ThueTNCN = ThucLinh = TNTT = 0;

                //Luong CB 
                ///Cong ngay
                dvChiTietCong.RowFilter = "MaNV='" + row["MaNV"].ToString() + "' and LoaiCongLam='Ngày'";

                foreach (DataRow dr in dvChiTietCong.ToTable().Rows)
                {
                    LuongThucLam += Convert.ToDouble("0" + dr["LuongNgay"].ToString());
                }
                LuongThucLam = LuongThucLam / ngaycong;
                //Cong Khac
                dvChiTietCong.RowFilter = "MaNV='" + row["MaNV"].ToString() + "' and LoaiCongLam not in('Ngày')";

                foreach (DataRow dr in dvChiTietCong.ToTable().Rows)
                {
                    LuongThucLam += Convert.ToDouble("0" + dr["LuongKhac"].ToString());
                }

                //Phu cap
                PC = Convert.ToDouble(row["PhuCap"].ToString());

                //Giam tru gia canh
                GTGC = Convert.ToDouble(row["GiamTru"].ToString());

                double LuongTinhBH = Convert.ToDouble(row["LuongCB"]);
                //BHXH,BHYT
                if (Convert.ToBoolean(row["TinhBH"].ToString()))
                {
                    BHYT = LuongTinhBH * 1.5 / 100;
                    BHXH = LuongTinhBH * 8 / 100;
                }
                //BHTN
                if (Convert.ToBoolean(row["TinhBHTN"].ToString()))
                {
                    BHTN = LuongTinhBH * 1 / 100;
                }
                //Tong Luong
                TL = LuongThucLam + PC;

                //Thu nhap tinh thue
                TNTT = TL - GTGC;

                //ThueTNCN
                if (Convert.ToBoolean(row["TinhThue"].ToString()))
                {
                    ThueTNCN = TinhThue(TNTT);
                }

                //Thuc Linh
                ThucLinh = TL - BHTN - BHXH - BHYT - ThueTNCN;

                //cập nhật lương vào bảng lương
                row["LCB"] = Math.Round(LuongThucLam, 0);
                row["PC"] = Math.Round(PC, 0);
                row["GTGC"] = Math.Round(GTGC, 0);
                row["TL"] = Math.Round(TL, 0);
                row["BHYT"] = Math.Round(BHYT, 0);
                row["BHXH"] = Math.Round(BHXH, 0);
                row["BHTN"] = Math.Round(BHTN, 0);
                row["TNTT"] = Math.Round(TNTT, 0);
                row["ThueTNCN"] = Math.Round(ThueTNCN, 0);
                row["ThucLinh"] = Math.Round(ThucLinh, 0);
            }
            return dtNhanvien;
        }

        //Add row cho girdview
        void addRows(DataTable dt, GridView gvChiTiet)
        {
            foreach (DataRow row in dt.Rows)
            {
                gvChiTiet.AddNewRow();
                gvChiTiet.UpdateCurrentRow();
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["MaNV"], row["MaNV"].ToString());
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["TenNV"],row["TenNV"].ToString());
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["LuongCB"],row["LCB"].ToString());
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["PhuCap"],row["PC"].ToString());
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["ThuNhapKhac"],row["TNKhac"].ToString());
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["TongLuong"],row["TL"].ToString());
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["TamUng"], row["TU"].ToString());
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["GiamTru"], row["GTGC"].ToString());

                if (Convert.ToBoolean(row["TinhThue"]))
                    gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["TNTinhThue"], row["TNTT"].ToString());
                else
                    gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["TNTinhThue"], "0");
                
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["BHYT"], row["BHYT"].ToString());
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["BHXH"], row["BHXH"].ToString());
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["BHTN"], row["BHTN"].ToString());
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["ThueTNCN"], row["ThueTNCN"].ToString());
                gvChiTiet.SetFocusedRowCellValue(gvChiTiet.Columns["ThucLinh"], row["ThucLinh"].ToString());
            }            
        }       

        public DataCustomFormControl Data
        {
            set { _data = value; }
        }

        public InfoCustomControl Info
        {
            get { return _info; }
        }
        

        #endregion
    }
}
