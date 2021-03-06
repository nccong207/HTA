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
using CDTLib;
using CDTControl;
using DevExpress;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraPrinting;
namespace Chamcong
{
    public partial class frmChamcong : DevExpress.XtraEditors.XtraForm
    {
        public frmChamcong()
        {
            InitializeComponent();
        }
        Database _dbData = Database.NewDataDatabase();
        Database _dbNewDate = Database.NewStructDatabase();
        DataTable dtLCBackup;
        DataTable dtLoaiCong;
        DataView dvLoaiCong;
        DataTable dtTongCong;
        DataTable dtChamCong;
        DataView dvChamcong;
        DataView dvOldChamCong;
        int year = Int32.Parse(Config.GetValue("NamLamViec").ToString());

        public int thanglamviec = 0;
        string[,] oldValues;
        string[,] newValues;
        string[] arrDMLC; // Mảng chứa danh mục loại công
        RepositoryItemPopupContainerEdit riPopup;
        bool isError = false;//bien ghi nhan co loi khi cham cong        

        private void frmChamcong_Load(object sender, EventArgs e)
        {
            riPopup = new RepositoryItemPopupContainerEdit();
            panelCong.Visible = false;

            //table Loai Cong
            dtLoaiCong = _dbData.GetDataTable("select * from LoaiCong where NgungSuDung='0' order by LoaiCongLam ");
            dvLoaiCong = new DataView(dtLoaiCong);
            //kỳ kế toán
            if(Config.GetValue("KyKeToan")!=null)
                spinThang.Value = Convert.ToInt16(Config.GetValue("KyKeToan").ToString());
            
            //Chấm công từ ngày đến ngày
            if (spinThang.Value.ToString() != "")
            {
                dtpFrom.DateTime = new DateTime(year, Convert.ToInt16(spinThang.Value), 01);
                int numDayofMonth = DateTime.DaysInMonth(year, Convert.ToInt16(spinThang.Value));
                dtpTo.DateTime = new DateTime(year, Convert.ToInt16(spinThang.Value), numDayofMonth);
            }
            //Công trình
            //bindCongTrinh();
            //Phòng ban
            bindPhongban();
            //Danh muc loai cong
            bindDMLoaiCong();

            groupStyle.EditValue = 0;
            gridLoaiCong.Visible = false;
            dtChamCong = new DataTable();            
        }

        //tạo cấu trúc bảng chấm công nhân viên
        DataTable createTable(DataTable dt)
        {
            if (spinThang.Value.ToString() != "")
            {
                DataColumn colMaNV = new DataColumn();
                colMaNV.ColumnName = "MaNV";
                dt.Columns.Add(colMaNV);
                DataColumn colTenNV = new DataColumn();
                colTenNV.ColumnName = "TenNV";
                dt.Columns.Add(colTenNV);
                DataColumn colTenBP = new DataColumn();
                colTenBP.ColumnName = "TenBP";
                dt.Columns.Add(colTenBP);

                //tạo cột ngày trong tháng
                int numDayofMonth = DateTime.DaysInMonth(year, Convert.ToInt16(spinThang.Value));
                for (int i = 1; i <= numDayofMonth; i++)
                {
                    DataColumn col = new DataColumn();
                    col.AllowDBNull = true;
                    col.ColumnName = (i < 10 ? "0" + i.ToString() : i.ToString());
                    dt.Columns.Add(col);
                }

                for (int i = 0; i < arrDMLC.Length; i++)
                {
                    DataColumn colSum = new DataColumn();
                    colSum.AllowDBNull = true;
                    colSum.ColumnName = arrDMLC[i].ToString().Trim();
                    dt.Columns.Add(colSum);
                }
                dt.Columns.Add("type");                
                dt.AcceptChanges();
            }
            return dt;
        }

        void EditViews(DataTable dt)
        {
            //Thiết lập size và cố định cột
            gridListNhanvien.Columns["MaNV"].Caption = "Mã nhân viên";
            gridListNhanvien.Columns["MaNV"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridListNhanvien.Columns["MaNV"].OptionsColumn.AllowEdit = false;

            gridListNhanvien.Columns["TenNV"].Caption = "Tên nhân viên";
            gridListNhanvien.Columns["TenNV"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            gridListNhanvien.Columns["TenNV"].OptionsColumn.AllowEdit = false;

            //Nhóm theo tên phòng ban
            gridListNhanvien.Columns["TenBP"].Caption = "Phòng ban";
            gridListNhanvien.Columns["TenBP"].GroupIndex = 0;

            //Caption cho các cột tổng
            for (int i = 0; i < arrDMLC.Length; i++)
            {
                gridListNhanvien.Columns[arrDMLC[i].ToString().Trim()].Caption = "Tổng: " + arrDMLC[i].ToString();
                //ko cho edit
                gridListNhanvien.Columns[arrDMLC[i].ToString().Trim()].OptionsColumn.AllowEdit = false;
                gridListNhanvien.Columns[arrDMLC[i].ToString().Trim()].Fixed = FixedStyle.Right;
            }

            int numDayofMonth = DateTime.DaysInMonth(year, Convert.ToInt16(spinThang.Value));
            //set màu cho cột ngày vào thứ 7, CN va đăng ký Popup
            for (int i = 1; i <= numDayofMonth; i++)
            {               
                DateTime dtpDate = new DateTime(year, Convert.ToInt16(spinThang.Value), i);
                //dang ky popup
                gridListNhanvien.Columns[(i < 10 ? "0" + i.ToString() : i.ToString())].ColumnEdit = riPopup;

                if (dtpDate.DayOfWeek == DayOfWeek.Saturday || dtpDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    gridListNhanvien.Columns[(i < 10 ? "0" + i.ToString() : i.ToString())].AppearanceHeader.ForeColor = System.Drawing.Color.Red;
                    gridListNhanvien.Columns[(i < 10 ? "0" + i.ToString() : i.ToString())].AppearanceHeader.Options.UseForeColor = true;
                }
            }

            gridListNhanvien.Columns["type"].Visible = false;
        }

        void bindDMLoaiCong()
        {
            //loai cong
            string sql = " select Tip " +
                     " from sysTable TB inner join sysField FL on TB.sysTableID=FL.sysTableID " +
                     " where TB.TableName='LoaiCong' and FL.FieldName='LoaiCongLam'";
            DataTable dt_sub = _dbNewDate.GetDataTable(sql);
            if (dt_sub.Rows.Count > 0)
            {
                arrDMLC = dt_sub.Rows[0]["Tip"].ToString().Split(new char[] { ';' });
            }
            //DM Loại công
            foreach (DataRow row in dtLoaiCong.Rows)
            {
                cbLoaiCong.Properties.Items.Add(row["KyHieu"].ToString());
                cbCongT7.Properties.Items.Add(row["KyHieu"].ToString());
            }
            cbLoaiCong.Properties.Items.Insert(0, "");
            cbLoaiCong.SelectedIndex = 0;
            cbCongT7.Properties.Items.Insert(0,"");
            cbCongT7.SelectedIndex = 0;

        }

        //void bindCongTrinh()
        //{
        //    string sql = "select * from DMCongTrinh where isHT = 0";
        //    DataTable dt = _dbData.GetDataTable(sql);
        //    leCongTrinh.Properties.DataSource = dt;
        //    leCongTrinh.Properties.DisplayMember = "TenCongTrinh";
        //    leCongTrinh.Properties.ValueMember = "MaCongTrinh";
        //    leCongTrinh.Properties.BestFit();
        //    if (dt.Rows.Count > 0)
        //        leCongTrinh.EditValue = dt.Rows[0]["MaCongTrinh"].ToString();
        //}

        void bindPhongban()
        {
            string sql = "select * from DMBoPhan";
            DataTable dt = _dbData.GetDataTable(sql);
            lookupPhongBan.Properties.DataSource = dt;
            lookupPhongBan.Properties.DisplayMember = "TenBP";
            lookupPhongBan.Properties.ValueMember = "MaBP";
            if (dt.Rows.Count > 0)
                lookupPhongBan.EditValue = dt.Rows[0]["MaBP"].ToString();
        }
       
        void InitPopup()
        {
            gridLoaiCong.Visible = true;
            //Lấy table loại công và add column Chon
            dtLoaiCong = _dbData.GetDataTable("select * from LoaiCong where NgungSuDung='0' order by LoaiCongLam");
            DataColumn dcChon = new DataColumn("Chon", typeof(Boolean));
            dcChon.DefaultValue = false;
            dtLoaiCong.Columns.Add(dcChon);
            dtLCBackup = dtLoaiCong.Copy();

            // gán dữ liệu vào gridLoaiCong
            gridLoaiCong.DataSource = dtLoaiCong;
            gvLoaiCong.BestFitColumns();
            gridLoaiCong.Dock = DockStyle.Fill;

            //
            PopupContainerControl pcc = new PopupContainerControl();
            pcc.Controls.Add(gridLoaiCong);
            riPopup.PopupControl = pcc;
            riPopup.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            riPopup.PopupFormMinSize = new Size(300, 200);
            riPopup.QueryPopUp += new CancelEventHandler(riPopup_QueryPopUp);   //chuyen du lieu tu cell vao grid control loai cong
            riPopup.CloseUp += new DevExpress.XtraEditors.Controls.CloseUpEventHandler(riPopup_CloseUp);    //lay du lieu tu grid control loai cong vao cell

            gridNhanVien.RepositoryItems.Add(riPopup);
        }

        void GetData()
        {
            //bool isCT = leCongTrinh.EditValue != null && leCongTrinh.EditValue.ToString() != "";
            //string mact = isCT ? leCongTrinh.EditValue.ToString() : "";
            string sql = "";            
            InitPopup();
            int numDayofMonth = DateTime.DaysInMonth(year, Convert.ToInt16(spinThang.Value));
            //Dữ liệu chứa tổng công làm theo từng ngày               
            sql = " SELECT CC.MaNV,NV.TenNV,CC.Ngay " +
                    "      , REPLACE(RTRIM((SELECT cast( KyHieu as varchar) + ':' + rtrim(replace(replace(rtrim(replace(cast(c.socong as varchar),'0',' ')),' ','0') + ' ','. ','')) + ' ' " +
                    "      FROM ChamCong C inner join LoaiCong LC on LC.ID=C.LoaiCong WHERE (C.MaNV = CC.MaNV and C.Ngay=CC.Ngay  )  FOR XML PATH (''))),' ','; ') AS TongCong " +
                    " FROM ChamCong CC inner join DMNhanVien NV on NV.MaNV=CC.MaNV " +
                    " WHERE YEAR(CC.Ngay)='" + year + "' and MONTH(CC.Ngay)='" + Convert.ToInt16(spinThang.EditValue) + "' " +
                    //(isCT ? " and cc.MaCongTrinh = '" + mact + "' " : " and cc.MaCongTrinh is null") +
                    " GROUP BY CC.MaNV,NV.TenNV,CC.Ngay";           

            DataTable dtData = _dbData.GetDataTable(sql);

            //Danh sách nhân viên          
            sql = " SELECT CC.MaNV,NV.TenNV,NV.PhongBan, 0 as [Type] " +
                  " FROM ChamCong CC inner join DMNhanVien NV on NV.MaNV=CC.MaNV " +
                  " WHERE YEAR(CC.Ngay)='" + year + "' and MONTH(CC.Ngay)='" + Convert.ToInt16(spinThang.EditValue) + "' " +
                //(isCT ? " and cc.MaCongTrinh = '" + mact + "' " : " and cc.MaCongTrinh is null") +
                  " GROUP BY CC.MaNV,NV.TenNV,NV.PhongBan " +
                  " union all " +
                  " select NV.ManV,NV.TenNV,NV.PhongBan, 1 as [Type] " +
                  " from DMNhanVien NV  " +
                  " where (NV.DaNV='0' or (NV.DaNV = 1 and NV.NgayNV > '" + spinThang.Text + "/1/" + year + "'" +
                  ")) and not exists (select * from ChamCong CC where CC.MaNV=NV.MaNV and YEAR(CC.Ngay)='" + year + 
                  "' and MONTH(CC.Ngay)='" + Convert.ToInt16(spinThang.EditValue) + "')";
                  //(isCT ? "' and cc.MaCongTrinh = '" + mact + "' " : "' and cc.MaCongTrinh is null") + " )";

            DataTable dtNhanvien = _dbData.GetDataTable(sql);
            DataView dvChiTietCong = new DataView(dtData);

            //tổng công theo từng loại công
            sql = " select cc.Manv,Loaiconglam,sum(cc.socong) as tongcong " +
                  " from chamcong cc inner join loaicong lc on cc.loaicong=lc.id " +
                  " where year(ngay)='" + year + "' and month(ngay)='" + Convert.ToInt16(spinThang.EditValue) + "' " +
                  //(isCT ? " and cc.MaCongTrinh = '" + mact + "' " : " and cc.MaCongTrinh is null") +
                  " group by cc.Manv,loaiconglam ";
            dtTongCong = _dbData.GetDataTable(sql);
            DataView dvTongCong = new DataView(dtTongCong);

            //Hiển thị công
            if (dtNhanvien.Rows.Count > 0)
            {
                foreach (DataRow row in dtNhanvien.Rows)
                {
                    DataRow dr = dtChamCong.NewRow();
                    dr["MaNV"] = row["MaNV"].ToString();
                    dr["TenNV"] = row["TenNV"].ToString();
                    dr["TenBP"] = row["PhongBan"].ToString();
                    for (int i = 1; i <= numDayofMonth; i++)
                    {
                        dvChiTietCong.RowFilter = "MaNV='" + row["MaNV"].ToString() + "' and Ngay='" + new DateTime(year, Convert.ToInt16(spinThang.EditValue), i) + "'";
                        if (dvChiTietCong.Count > 0)                        
                            dr[(i < 10 ? "0" + i.ToString() : i.ToString())] = dvChiTietCong.ToTable().Rows[0]["TongCong"].ToString();                                                   
                    }
                    //Tong cong
                    for (int j = 0; j < arrDMLC.Length; j++)
                    {
                        dvTongCong.RowFilter = "MaNV='" + row["MaNV"].ToString() + "' and LoaiCongLam='" + arrDMLC[j].Trim() + "'";
                        if (dvTongCong.Count > 0)
                            dr[arrDMLC[j].Trim()] = Convert.ToDecimal(dvTongCong.ToTable().Rows[0]["tongcong"].ToString()).ToString("### ### ### ##0.######");
                    }
                    //Nhan vien đã có chấm công hay nhân viên mới
                    dr["type"] = row["type"].ToString();
                    dtChamCong.Rows.Add(dr);
                }
                dtChamCong.AcceptChanges();
            }
            gridListNhanvien.BestFitColumns();
            gridListNhanvien.ExpandAllGroups();
        }

        void ChamCongNhanh()
        {
            int numDayofMonth = DateTime.DaysInMonth(year, thanglamviec);
            //Cong mac dinh cho cham cong nhanh
            string strCongMD = "";
            string strCongMDT7 = "";
            string sql = "select KyHieu + ':' + rtrim(replace(replace(rtrim(replace(cast(socong as varchar),'0',' ')),' ','0') + ' ','. ','')) as TongCong " +
                         "from LoaiCong where KyHieu='" + cbLoaiCong.SelectedItem.ToString() + "'";
            DataTable dt = _dbData.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                strCongMD = dt.Rows[0]["TongCong"].ToString();
            }

            //cong cho thu 7
            sql = "select KyHieu + ':' + rtrim(replace(replace(rtrim(replace(cast(socong as varchar),'0',' ')),' ','0') + ' ','. ','')) as TongCong " +
                         "from LoaiCong where KyHieu='" + cbCongT7.SelectedItem.ToString() + "'";
            dt = _dbData.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                strCongMDT7 = dt.Rows[0]["TongCong"].ToString();
            }

            //Danh sách nhân viên chấm công nhanh            
            DataView dv = new DataView(dtChamCong);
            if (groupStyle.EditValue.ToString() == "1")
                dv.RowFilter = "TenBP ='" + lookupPhongBan.EditValue.ToString() + "'";
            dt = dv.ToTable();
            dv = new DataView(dt);
            //ghi du lieu
            if (dv.Count > 0)
            {
                foreach (DataRow row in dtChamCong.Rows)
                {
                    if (row.RowState == DataRowState.Deleted)
                        continue;
                    dv.RowFilter = "MaNV='" + row["MaNV"].ToString() + "'";
                    if (dv.Count > 0)
                    {
                        for (int i = dtpFrom.DateTime.Day; i <= dtpTo.DateTime.Day; i++)
                        {
                            DateTime dtpLoop = new DateTime(year,thanglamviec,i);
                            if (chkSat.Checked) // cham ca thu 7
                            {
                                if (dtpLoop.DayOfWeek != DayOfWeek.Sunday && dtpLoop.DayOfWeek != DayOfWeek.Saturday)
                                    row[(i < 10 ? "0" + i.ToString() : i.ToString())] = strCongMD;
                                else if (dtpLoop.DayOfWeek == DayOfWeek.Saturday)
                                {
                                    row[(i < 10 ? "0" + i.ToString() : i.ToString())] = strCongMDT7;
                                }
                            }
                            else //khong cham thu 7
                            {
                                if (dtpLoop.DayOfWeek != DayOfWeek.Sunday && dtpLoop.DayOfWeek != DayOfWeek.Saturday)
                                    row[(i < 10 ? "0" + i.ToString() : i.ToString())] = strCongMD;
                                else if (dtpLoop.DayOfWeek == DayOfWeek.Saturday)
                                { 
                                    //xử lý trường hợp nếu đã chấm ngày thứ 7 (t7 đã có công rồi) thì xóa công thứ 7
                                    row[(i < 10 ? "0" + i.ToString() : i.ToString())] = "";
                                }

                            }
                        }
                    }
                }
            }
           
            //Tinh lai tong cong
            TongCongDuyet(numDayofMonth);
        }

        void TongCongDuyet(int numDayofMonth)
        {
            for (int i = 0; i < arrDMLC.Length; i++)
            {
                foreach (DataRow row in dtChamCong.Rows)
                {
                    if (row.RowState == DataRowState.Deleted)
                        continue;
                    double tongcong = 0;
                    for (int j = 1; j <= numDayofMonth; j++)
                    {
                        if (row[(j < 10 ? "0" + j.ToString() : j.ToString())].ToString() != "")
                        {
                            string[] arr = row[(j < 10 ? "0" + j.ToString() : j.ToString())].ToString().Split(new char[] { ';' });
                            for (int x = 0; x < arr.Length; x++)
                            {
                                if (arr[x].ToString() != "")
                                {
                                    string[] value = arr[x].Trim().Split(new char[] { ':' });
                                    dvLoaiCong.RowFilter = "KyHieu='" + value[0].Trim() + "'";
                                    if (dvLoaiCong.Count > 0)
                                    {
                                        foreach (DataRowView drv in dvLoaiCong)
                                        {
                                            if (drv.Row["LoaiCongLam"].ToString().Trim().ToUpper() == arrDMLC[i].Trim().ToUpper())
                                                tongcong += Convert.ToDouble(value[1].Trim());
                                        }
                                    }
                                }
                            }
                        }
                    }
                    row[arrDMLC[i].ToString()] = tongcong.ToString();
                }
            }
        }      

        void TongCong(DataRow row)
        {
            try
            {
                for (int j = 0; j < arrDMLC.Length; j++)
                {
                    //Trừ đi số liệu cũ              
                    for (int x = 0; x < oldValues.Length / 2; x++)
                    {
                        dvLoaiCong.RowFilter = "KyHieu='" + oldValues[x, 0] + "'";
                        foreach (DataRowView drv in dvLoaiCong)
                        {
                            if (arrDMLC[j].ToString().Trim().ToUpper().Equals(drv.Row["LoaiCongLam"].ToString().Trim().ToUpper()))
                                row[arrDMLC[j].ToString().Trim()] = Convert.ToDecimal("0" + row[arrDMLC[j].ToString().Trim()].ToString().Trim()) - Convert.ToDecimal(oldValues[x, 1].Trim());
                        }
                    }
                    //Cộng số liệu mới                
                    if (newValues != null)
                    {
                        for (int x = 0; x < newValues.Length / 2; x++)
                        {
                            dvLoaiCong.RowFilter = "KyHieu='" + newValues[x, 0] + "'";
                            foreach (DataRowView drv in dvLoaiCong)
                            {
                                if (arrDMLC[j].ToString().Trim().ToUpper().Equals(drv.Row["LoaiCongLam"].ToString().Trim().ToUpper()))
                                {
                                    if (row[arrDMLC[j].ToString().Trim()].ToString() != "")
                                        row[arrDMLC[j].ToString().Trim()] = Convert.ToDecimal("0" + row[arrDMLC[j].ToString().Trim()].ToString().Trim()) + Convert.ToDecimal(newValues[x, 1].Trim());
                                    else
                                        row[arrDMLC[j].ToString().Trim()] = Convert.ToDecimal(newValues[x, 1].Trim());
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        void TCongDuyetOneRow(int numDayofMonth, DataRow row)
        {
            for (int i = 0; i < arrDMLC.Length; i++)
            {
                double tongcong = 0;
                for (int j = 1; j <= numDayofMonth; j++)
                {
                    if (row[(j < 10 ? "0" + j.ToString() : j.ToString())].ToString() != "")
                    {
                        string[] arr = row[(j < 10 ? "0" + j.ToString() : j.ToString())].ToString().Split(new char[] { ';' });
                        for (int x = 0; x < arr.Length; x++)
                        {
                            if (arr[x].ToString() != "")
                            {
                                string[] value = arr[x].Trim().Split(new char[] { ':' });
                                dvLoaiCong.RowFilter = "KyHieu='" + value[0].Trim() + "'";
                                if (dvLoaiCong.Count > 0)
                                {
                                    foreach (DataRowView drv in dvLoaiCong)
                                    {
                                        if (drv.Row["LoaiCongLam"].ToString().Trim().ToUpper() == arrDMLC[i].Trim().ToUpper())
                                            tongcong += Convert.ToDouble(value[1].Trim());
                                    }
                                }
                            }
                        }
                    }
                }
                row[arrDMLC[i].ToString()] = tongcong.ToString();
            }
        }

        bool validate()
        {
            if (dtpFrom.DateTime.Year == 1 || dtpTo.DateTime.Year == 1)
            {
                XtraMessageBox.Show("Nhập ngày tháng chấm công", "Thông báo");
                return false;
            }          
            if (groupStyle.EditValue.ToString() == "1")
            {
                if (lookupPhongBan.EditValue == null)
                {
                    XtraMessageBox.Show("Chọn phòng ban", "Thông báo");
                    return false;
                }
            }
            return true;
        }    

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (groupStyle.EditValue.ToString() == "1")
            {
                panelPhongban.Visible = true;
            }
            else
                panelPhongban.Visible = false;
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            if (!validate())
                return;
            if (dtpFrom.DateTime.Day > dtpTo.DateTime.Day)
                XtraMessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày kết thúc");
            else
                ChamCongNhanh();
        }

        private void riPopup_CloseUp(object sender, DevExpress.XtraEditors.Controls.CloseUpEventArgs e)
        {
            int vtRow = gridListNhanvien.FocusedRowHandle;
            if (gvLoaiCong.FocusedRowHandle == gvLoaiCong.RowCount) //chuyen sang row khac de datasource duoc update
                gvLoaiCong.FocusedRowHandle--;
            else
                gvLoaiCong.FocusedRowHandle++;
            //loc bang loai cong de chuyen du lieu vao cell            
            DataView dv = new DataView(dtLoaiCong);
            dv.RowFilter = "Chon = 1";
            string s = "";
            if (dv.Count > 0)
            {
                //khai báo mảng chứa giá trị mới
                newValues = new string[dv.Count, 2];
                int i = 0;
                foreach (DataRowView drv in dv)
                {
                    s += drv["KyHieu"].ToString() + ":" + Convert.ToDecimal(drv["SoCong"].ToString().Trim()).ToString("### ### ### ##0.######").Trim() + "; ";
                    //gán giá trị mới
                    newValues[i, 0] = drv["KyHieu"].ToString();
                    newValues[i, 1] = Convert.ToDecimal(drv["SoCong"].ToString()).ToString("### ### ### ##0.######");
                    i++;
                }
            }
            else
                newValues = null;
            if(s!="")
                s = s.Remove(s.Length - 2);
            e.Value = s;

            //gan du lieu vao datasource            
            //gridListNhanvien.SetRowCellValue(gridListNhanvien.FocusedRowHandle, gridListNhanvien.FocusedColumn, s);

            //Tinh tong cong 
            DataRow row=gridListNhanvien.GetDataRow(gridListNhanvien.FocusedRowHandle);
                      
            int numDayofMonth = DateTime.DaysInMonth(year, thanglamviec);
            if (isError)
            {
                TCongDuyetOneRow(numDayofMonth, row);
                isError = false;
            }
            else
                TongCong(row);      
            
        }

        private void riPopup_QueryPopUp(object sender, CancelEventArgs e)
        {            
            string s = (sender as PopupContainerEdit).Text;            
            dtLoaiCong = dtLCBackup.Copy();
            gridLoaiCong.DataSource = dtLoaiCong;

            string[] arr = s.Split(new char[] { ';' });
            oldValues = new string[arr.Length, 2];

            for (int i = 0; i < arr.Length; i++)
            {
                if (ValidateFormat(arr[i]))
                {
                    if (arr[i].ToString() != "")
                    {
                        string kyhieu = "";
                        string socong = "";
                        string[] value = arr[i].Trim().Split(new char[] { ':' });
                        if (value.Length > 0)
                        {
                            foreach (DataRow row in dtLoaiCong.Rows)
                            {
                                if (row["KyHieu"].ToString().ToUpper().Trim().Equals(value[0].ToUpper().Trim()))
                                {
                                    row["Chon"] = true;
                                    row["SoCong"] = Convert.ToDecimal(value[1].ToString().Trim()).ToString("### ### ## ##0.######");
                                    //lấy giá trị cũ
                                    kyhieu = row["KyHieu"].ToString();
                                    socong = Convert.ToDecimal(value[1].ToString().Trim()).ToString("### ### ## ##0.######");

                                    break;
                                }
                            }
                        }
                        if (kyhieu != "" && socong != "")
                        {
                            oldValues[i, 0] = kyhieu;
                            oldValues[i, 1] = socong;
                        }
                    }
                }
                else
                    isError = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            thanglamviec = Convert.ToInt16(spinThang.EditValue);
            dtChamCong.Clear();
            dtChamCong.Dispose();
            dtChamCong.Reset();
            dtChamCong = new DataTable();
            dtChamCong = createTable(dtChamCong);
            gridListNhanvien.Columns.Clear();
            gridNhanVien.DataSource = dtChamCong;
            gridListNhanvien.PopulateColumns();
            gridListNhanvien.BestFitColumns();
            EditViews(dtChamCong);
            GetData();
            UpdateTTNV();
            dvChamcong = new DataView(dtChamCong);
            dvOldChamCong = new DataView(dtChamCong);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (gridListNhanvien.RowCount > 0)
            {
                //bool isCT = leCongTrinh.EditValue != null && leCongTrinh.EditValue.ToString() != "";
                string ngay = ""; //ghi nhớ các ngày không cập nhật do định dạng sai
                int numDayofMonth = DateTime.DaysInMonth(year, thanglamviec);
                _dbData.BeginMultiTrans();

                string sql = "insert into chamcong(MaNV,Ngay,LoaiCong,SoCong) values(@MaNV,@Ngay,@LoaiCong,@SoCong)";
                string[] paraNames = new string[] { "@MaNV", "@Ngay", "@LoaiCong", "@SoCong"};
                object[] paraValues;

                //insert cac nhan vien moi chua co cham cong                   
                dvChamcong.RowFilter = "type = 1";
                bool flag = false;//dấu hiệu để nhận biết có nhân viên mới hay không
                foreach (DataRowView drv in dvChamcong)
                {                    
                    flag=true;
                    for (int i = 1; i <= numDayofMonth; i++)
                    {
                        if (drv.Row[(i < 10) ? "0" + i.ToString() : i.ToString()].ToString() != "")
                        {
                            string[] arr = drv.Row[(i < 10) ? "0" + i.ToString() : i.ToString()].ToString().Trim().Split(new char[] { ';' });
                            if (arr.Length > 0)
                            {
                                for (int j = 0; j < arr.Length; j++)
                                {
                                    string[] value = arr[j].Trim().Split(new char[] { ':' });
                                    if (value.Length > 0 && value[0] != "")
                                    {
                                        string loaicong = null;
                                        dvLoaiCong.RowFilter = "KyHieu='" + value[0].Trim() + "'";
                                        foreach (DataRowView drv1 in dvLoaiCong)
                                            loaicong = drv1.Row["ID"].ToString().Trim();
                                        paraValues = new object[] { drv.Row["MaNV"].ToString(), new DateTime(year, Convert.ToInt16(spinThang.EditValue), i), loaicong, Convert.ToDecimal(value[1].Trim())};
                                        _dbData.UpdateDatabyPara(sql, paraNames, paraValues);                                        
                                    }
                                }
                            }
                        }
                    }
                }
                //update cac nhan vien da cham cong                

                dvChamcong.RowFilter = "type = 0";
                dvOldChamCong.RowFilter = "type = 0";
                
                dvChamcong.RowStateFilter = DataViewRowState.ModifiedCurrent;
                DataView curView = dvChamcong;

                dvOldChamCong.RowStateFilter = DataViewRowState.ModifiedOriginal;
                DataView oldView = dvOldChamCong;

                for (int numRow = 0; numRow < curView.Count; numRow++)
                {
                    for (int i = 1; i <= numDayofMonth; i++)
                    {
                        if (curView[numRow][(i < 10) ? "0" + i.ToString() : i.ToString()].ToString() != oldView[numRow][(i < 10) ? "0" + i.ToString() : i.ToString()].ToString())
                        {
                            if (ValidateFormat(curView[numRow][(i < 10) ? "0" + i.ToString() : i.ToString()].ToString().Trim()))
                            {
                                string sqlText = "";
                                if (curView[numRow][(i < 10) ? "0" + i.ToString() : i.ToString()].ToString() == "")
                                    sqlText = "0,";
                                string[] arr = curView[numRow][(i < 10) ? "0" + i.ToString() : i.ToString()].ToString().Trim().Split(new char[] { ';' });

                                for (int j = 0; j < arr.Length; j++)
                                {
                                    if (arr[j].ToString() != "")
                                    {
                                        string loaicong = null;
                                        string[] value = arr[j].Trim().Split(new char[] { ':' });
                                        if (value.Length > 0)
                                        {
                                            //kiem tra dinh dang, neu dung thi moi save                                        

                                            dvLoaiCong.RowFilter = "KyHieu='" + value[0].Trim() + "'";
                                            foreach (DataRowView drv1 in dvLoaiCong)
                                                loaicong = drv1.Row["ID"].ToString().Trim();
                                            //lấy các công đang được sử dụng hiện tại
                                            sqlText += loaicong.Trim() + ",";

                                            //kiem tra da co cham cong ngay nay hay chua
                                            sql = " select * from ChamCong CC inner join LoaiCong LC on CC.LoaiCong=LC.ID " +
                                                  " where CC.MaNV='" + curView[numRow]["MaNV"].ToString().Trim() + "' and CC.Ngay='" + new DateTime(year, Convert.ToInt16(spinThang.EditValue), i) + "' and LC.KyHieu='" + value[0].ToString().Trim() + "'";
                                                  //(isCT ? " and cc.MaCongTrinh = '" + leCongTrinh.EditValue.ToString() + "'" : "");
                                            DataTable dt = _dbData.GetDataTable(sql);
                                            if (dt.Rows.Count > 0)
                                            {
                                                //update
                                                sql = "update ChamCong set SoCong=@SoCong where MaNV=@MaNV and Ngay=@Ngay and LoaiCong=@LoaiCong";
                                                paraNames = new string[] { "@SoCong", "@MaNV", "@Ngay", "@LoaiCong"};
                                                paraValues = new object[] { value[1].Trim(), curView[numRow]["MaNV"].ToString(), new DateTime(year, Convert.ToInt16(spinThang.EditValue), i), loaicong };
                                                _dbData.UpdateDatabyPara(sql, paraNames, paraValues);
                                            }
                                            else
                                            {
                                                //insert
                                                sql = "insert into chamcong(MaNV,Ngay,LoaiCong,SoCong) values(@MaNV,@Ngay,@LoaiCong,@SoCong)";
                                                paraNames = new string[] { "@MaNV", "@Ngay", "@LoaiCong", "@SoCong" };
                                                paraValues = new object[] { curView[numRow]["MaNV"].ToString(), new DateTime(year, Convert.ToInt16(spinThang.EditValue), i), loaicong, Convert.ToDecimal(value[1].Trim()) };
                                                _dbData.UpdateDatabyPara(sql, paraNames, paraValues);
                                            }
                                        }
                                    }
                                }
                                //Xóa các công không còn sử dụng: vd ngày 9/1/2011 chấm 3 loại công, nhưng giờ bỏ 1
                                if (sqlText != "")
                                {
                                    sqlText = sqlText.Substring(0, sqlText.Length - 1);
                                    sql = "delete from chamcong where MaNV=@MaNV and Ngay=@Ngay and LoaiCong not in (" + sqlText + ")";
                                    paraNames = new string[] { "@MaNV", "@Ngay" };
                                    paraValues = new object[] { curView[numRow]["MaNV"].ToString(), new DateTime(year, Convert.ToInt16(spinThang.EditValue), i)};
                                    _dbData.UpdateDatabyPara(sql, paraNames, paraValues);
                                }
                            }
                            else
                            {
                                ngay += i.ToString() + ",";
                            }
                        }
                    }
                }

                if (!_dbData.HasErrors)
                {
                    _dbData.EndMultiTrans();
                    if (ngay == "")
                        XtraMessageBox.Show("Cập nhật chấm công thành công!", Config.GetValue("PackageName").ToString());
                    else
                    {
                        //ngay = ngay.Substring(0,ngay.Length-1);
                        XtraMessageBox.Show("Chấm công của nhân viên chưa hợp lệ!\nNhững công thay đổi hợp lệ đã được cập nhật.", Config.GetValue("PackageName").ToString());
                    }
                    if (flag)
                    {
                        foreach (DataRow row in dtChamCong.Rows)
                            if (row.RowState != DataRowState.Deleted)
                                row["type"] = "0";
                    }
                    dtChamCong.AcceptChanges();
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ShowCustomizePreview(gridNhanVien, true);
        }

        private void ShowCustomizePreview(IPrintable ctrl, bool isLanscape)
        {
            // Create a PrintingSystem component. 
            PrintingSystem ps = new PrintingSystem();
            // Create a link that will print a control. 
            PrintableComponentLink Print = new PrintableComponentLink(ps);
            // Specify the control to be printed. 
            Print.Component = ctrl;
            // Set the paper format. 
            Print.PaperKind = System.Drawing.Printing.PaperKind.A3;
            Print.Landscape = isLanscape;
            Print.Margins = new System.Drawing.Printing.Margins(40, 40, 40, 40);
            string dbName = Config.GetValue("DbName").ToString();
            if (dbName.Contains("DEMO"))
            {
                ps.Watermark.Text = "Hoa Tiêu Demo";
                ps.Watermark.TextTransparency = 150;
            }
            // Subscribe to the CreateReportHeaderArea event used to generate the report header. 
            Print.CreateReportHeaderArea +=
              new CreateAreaEventHandler(Print_CreateReportHeaderArea);
            Print.CreateReportFooterArea += new CreateAreaEventHandler(Print_CreateReportFooterArea);
            // Generate the report. 
            Print.CreateDocument();
            // Show the report. 
            Print.ShowPreview();

        }

        void Print_CreateReportFooterArea(object sender, CreateAreaEventArgs e)
        {
            string reportFooter = "Ngày " + DateTime.Today.Day.ToString("00") +
                " tháng " + DateTime.Today.Month.ToString("00") +
                " năm " + DateTime.Today.Year.ToString("0000") +
                "\nNgười lập            ";
            e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
            e.Graph.Font = new Font("Times New Roman", 10, FontStyle.Italic);
            e.Graph.BackColor = Color.Transparent;
            int x = (sender as PrintableComponentLink).Landscape ? 1200 : 900;
            RectangleF rec = new RectangleF(x, 20, 200, 50);
            e.Graph.DrawString(reportFooter, Color.Black, rec, BorderSide.None);
        }

        private void Print_CreateReportHeaderArea(object sender, CreateAreaEventArgs e)
        {
            if (Config.GetValue("TenCongTy") != null)
            {
                string info = Config.GetValue("TenCongTy").ToString();
                if (Config.GetValue("DiaChi") != null)
                    info += "\n" + Config.GetValue("DiaChi").ToString();
                e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Near);
                e.Graph.Font = new Font("Times New Roman", 10, FontStyle.Bold);
                e.Graph.BackColor = Color.Transparent;
                RectangleF rec1 = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
                e.Graph.DrawString(info, Color.Black, rec1, BorderSide.None);
            }

            string reportHeader = "BẢNG CHẤM CÔNG NHÂN VIÊN";
            e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
            e.Graph.Font = new Font("Times New Roman", 20, FontStyle.Bold);
            e.Graph.BackColor = Color.Transparent;
            RectangleF rec2 = new RectangleF(0, 50, e.Graph.ClientPageSize.Width, 50);
            e.Graph.DrawString(reportHeader, Color.Black, rec2, BorderSide.None);
        }

        private void spinThang_EditValueChanged(object sender, EventArgs e)
        {
            if (spinThang.Value.ToString() != "")
            {
                dtpFrom.DateTime = new DateTime(year, Convert.ToInt16(spinThang.Value), 01);
                int numDayofMonth = DateTime.DaysInMonth(year, Convert.ToInt16(spinThang.Value));
                dtpTo.DateTime = new DateTime(year, Convert.ToInt16(spinThang.Value), numDayofMonth);
            }
        }
        
        private void gridListNhanvien_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {            
            string value = e.Value.ToString();            
            bool flag=ValidateFormat(value);
            if (!flag)
                XtraMessageBox.Show("Chấm công chưa đúng định dạng", Config.GetValue("PackageName").ToString(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {   
                //tong cong
                int numDayofMonth = DateTime.DaysInMonth(year, thanglamviec);
                TCongDuyetOneRow(numDayofMonth, gridListNhanvien.GetDataRow(gridListNhanvien.FocusedRowHandle));
            }
        }

        bool ValidateFormat(string str)
        {
            string[] arr = str.Split(new char[] { ';' });

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].ToString() != "")
                {
                    if (!arr[i].Contains(":"))
                        return false;
                    string[] value = arr[i].Trim().Split(new char[] { ':' });
                    if (value[0].ToString() == "")
                        return false;
                    try
                    {
                        double num = Convert.ToDouble(value[1].ToString());
                    }
                    catch {
                        return false;
                    }
                }
            }            

            return true;
        }

        private void chkSat_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSat.Checked)
                panelCong.Visible = true;
            else
                panelCong.Visible = false;
        }

        private void UpdateTTNV()
        {
            DateTime ngaybd = new DateTime(year, Int32.Parse(spinThang.Text), 1);
            DateTime ngaykt = ngaybd.AddMonths(1).AddDays(-1);
            string sql = "select MaNV from DMNhanVien where (DaNV = 1 and NgayNV < '" + ngaybd.ToString("MM/dd/yyyy") + "') or NgayLV > '" + ngaykt.ToString("MM/dd/yyyy") + "'";
            DataTable dt = _dbData.GetDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                DataRow[] drs = dtChamCong.Select("MaNV = '" + dr["MaNV"].ToString() + "'");
                if (drs.Length == 0)
                    continue;
                drs[0].Delete();
            }
            string s1 = "delete from ChamCong where month(Ngay) = " + spinThang.Text + " and year(Ngay) = " + year + " and MaNV in (" + sql + ")";
            _dbData.UpdateByNonQuery(s1);
        }

        //private void leCongTrinh_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Delete)
        //        leCongTrinh.EditValue = null;
        //}

    }
}
