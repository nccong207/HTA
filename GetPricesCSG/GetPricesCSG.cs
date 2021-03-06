using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Plugins;
using CDTDatabase;
using DevExpress;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;

namespace GetPricesCSG
{
    public class GetPricesCSG:ICControl
    {
        #region ICControl Members
        private DataCustomFormControl data;
        private InfoCustomControl info = new InfoCustomControl(IDataType.MasterDetailDt);
        Database db = Database.NewDataDatabase();
        public void AddEvent()
        {            
            GridView gvDetail = (data.FrmMain.Controls.Find("gcMain", true)[0] as GridControl).MainView as GridView;
            gvDetail.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(gvDetail_CellValueChanged);
        }
         
        void gvDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv=sender as GridView;
            if (e.Column.FieldName.ToUpper().Equals("MAVT"))
            {
                //khach hang
                TextEdit txtKh = data.FrmMain.Controls.Find("MaKH", true)[0] as TextEdit;
                //vat tu
                string vt = gv.GetFocusedRowCellValue(e.Column).ToString();
                //ngayCT
                DateEdit dtp = data.FrmMain.Controls.Find("ngayCT",true)[0] as DateEdit;                
                string sql = "select * from ChinhSachGia CSG INNER JOIN GiaVT VT on VT.CSGID=CSG.CSGID " +
                           "LEFT JOIN GiaKH KH on KH.CSGID=CSG.CSGID WHERE VT.MaVT='" + vt + "'";
                DataTable dt = db.GetDataTable(sql);
                DataView dvGiaVT = new DataView(dt);
                DataView dvKH;

                string nhomkh = "";
                sql = "select Nhom1 from dmkh where makh ='"+txtKh.Text+"'";
                DataTable dtNhom = db.GetDataTable(sql);
                if (dtNhom.Rows.Count > 0)
                    nhomkh = dtNhom.Rows[0]["Nhom1"].ToString();
                dvGiaVT.RowFilter = "TuNgay is null and DenNgay is null";

                //nhóm có, mã có
                //nhóm không, mã có

                //nhóm có, mã không
                //nhóm không , mã không  

                if (dvGiaVT.Count > 0)
                {
                    dvKH = new DataView(dvGiaVT.ToTable());                                      
                    dvKH.RowFilter = "MaKH is null and NhomKH is null";
                    if (dvKH.Count == 0)
                    {
                        if(nhomkh != "")
                            dvKH.RowFilter = "MaKH is null and NhomKH = '"+nhomkh+"'";
                    }
                    if (dvKH.Count == 0)
                        dvKH.RowFilter = "MaKH='" + txtKh.Text + "'";
                    if (dvKH.Count == 0)//kt trường hợp vattu có chính sách giá trong khoảng thời gian hay ko?
                    {
                        dvGiaVT.RowFilter = "TuNgay <= '" + dtp.DateTime + "' and DenNgay >= '" + dtp.DateTime + "'";
                        dvKH = new DataView(dvGiaVT.ToTable());
                        dvKH.RowFilter = "MaKH is null and NhomKH is null";
                        if (dvKH.Count == 0)
                        {
                            if (nhomkh != "")
                                dvKH.RowFilter = "MaKH is null and NhomKH = '" + nhomkh + "'";
                        }
                        if (dvKH.Count == 0)
                            dvKH.RowFilter = "MaKH='" + txtKh.Text + "'";
                    }
                }
                else
                {
                    dvGiaVT.RowFilter = "TuNgay <= '" + dtp.DateTime + "' and DenNgay >= '" + dtp.DateTime + "'";
                    dvKH = new DataView(dvGiaVT.ToTable());
                    dvKH.RowFilter = "MaKH is null and NhomKH is null";
                    if (dvKH.Count == 0)
                    {
                        if (nhomkh != "")
                            dvKH.RowFilter = "MaKH is null and NhomKH = '" + nhomkh + "'";
                    }
                    if (dvKH.Count == 0)
                        dvKH.RowFilter = "MaKH='" + txtKh.Text + "'";                   
                }
                setPrice(dvKH, gv);
            }
        } 

        void setPrice(DataView dv, GridView gv)
        {
            foreach (DataRowView drv in dv)
            {
                gv.SetFocusedRowCellValue(gv.Columns["Gia"], drv.Row["GiaBan"].ToString());
                break;
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

        #endregion
    }
}
