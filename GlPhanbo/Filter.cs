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
using System.Globalization;
namespace GlPhanbo
{
    public partial class Filter : DevExpress.XtraEditors.XtraForm
    {
        NumberFormatInfo nfi = new NumberFormatInfo();
        string _mact = "Z80";
        int tuThang;
        int DenThang;
        Database db = Database.NewDataDatabase();
        DataTable trans;
        DateTime TuNgay;
        DateTime DenNgay;
        private string _phanLoai;

        public Filter(string phanLoai)
        {
            InitializeComponent();
            _phanLoai = phanLoai;
            spinEdit1.EditValue = Config.GetValue("KyKeToan") == null ? DateTime.Today.Month : Int32.Parse(Config.GetValue("KyKeToan").ToString());
            spinEdit2.EditValue = Config.GetValue("KyKeToan") == null ? DateTime.Today.Month : Int32.Parse(Config.GetValue("KyKeToan").ToString());
        }

        private bool KhoaSo(int newMonth)
        {
            if (Config.GetValue("NgayKhoaSo") == null)
                return false;
            if (Config.GetValue("NamLamViec") == null)
                return false;
            string tmp = Config.GetValue("NgayKhoaSo").ToString();
            int nam = Int32.Parse(Config.GetValue("NamLamViec").ToString());
            DateTime ngayKhoa;
            DateTimeFormatInfo dtInfo = new DateTimeFormatInfo();
            dtInfo.ShortDatePattern = "dd/MM/yyyy";
            if (DateTime.TryParse(tmp, dtInfo, DateTimeStyles.None, out ngayKhoa))
            {
                if (nam == ngayKhoa.Year && newMonth <= ngayKhoa.Month)
                {
                    string msg = "Kỳ kế toán đã khóa! Không thể chỉnh sửa số liệu!";
                    if (Config.GetValue("Language").ToString() == "1")
                        msg = UIDictionary.Translate(msg);
                    XtraMessageBox.Show(msg);
                    return true;
                }
                else
                    return false;
            }
            return false;
        }
          
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int Nam = int.Parse(Config.GetValue("NamLamViec").ToString());
            tuThang = int.Parse(spinEdit1.EditValue.ToString());
            DenThang = int.Parse(spinEdit2.EditValue.ToString());
            if (KhoaSo(DenThang))
                return;
            CultureInfo ci = Application.CurrentCulture;
            nfi.CurrencyDecimalSeparator = ".";
            nfi.CurrencyGroupSeparator = ",";
            Application.CurrentCulture.NumberFormat = nfi;
            db.BeginMultiTrans();
            bool kq = true;  
            for (int i = tuThang; i <= DenThang; i++)
            {
                string str = i.ToString() + "/" + "01" + "/" + Nam.ToString();
                TuNgay = Convert.ToDateTime(str);
                DenNgay = TuNgay.AddMonths(1).AddDays(-1);
                DataTable pb;
                List<DataRow> TransList = GetTransList();

                foreach (DataRow dr in TransList)
                {
                    string chiTieu = dr["ChiTieu"].ToString();
                    pb = db.GetDataSetByStore("Get_Phanbo",
                        new string[] { "@Ngayct1", "@Ngayct2", "@tkNguon", "@tkDich", "@tkChitieu", "@chitieu" },
                        new object[] { TuNgay, DenNgay, dr["tkNguon"].ToString(), dr["tkDich"].ToString(), dr["tkChitieu"].ToString(), chiTieu });
                    //lay cac gia tri chi tieu cua so lieu phan bo
                    List<string> lstTkNguon = new List<string>();
                    foreach (DataRow drPb in pb.Rows)
                    {
                        string tkNguon = drPb["TkNguon"].ToString();
                        if (!lstTkNguon.Contains(tkNguon))
                            lstTkNguon.Add(tkNguon);
                    }
                    foreach (string tkNguon in lstTkNguon)
                    {
                        DataView dv = new DataView(pb);
                        dv.RowFilter = "TkNguon = '" + tkNguon + "'";
                        decimal tienPb = 0;
                        for (int k = 0; k < dv.Count; k++)
                        {
                            DataRow drPb = dv[k].Row;
                            if (k == dv.Count - 1) //neu la dong cuoi cung thi tinh lai tien phan bo cho khop voi tong tien phan bo
                                drPb["TienPB"] = Decimal.Parse(drPb["TTienPB"].ToString(), nfi) - tienPb;
                            kq = Createbt(drPb, dr);
                            tienPb += Decimal.Parse(drPb["TienPB"].ToString(), nfi);
                            if (!kq)
                                break;
                        }
                        if (!kq)
                            break;
                    }
                    if (!kq)
                        break;
                }
                if (!kq)
                {
                    string msg = "Phân bổ tháng " + i.ToString() + " bị lỗi!";
                    if (Config.GetValue("Language").ToString() == "1")
                        msg = UIDictionary.Translate(msg);
                    XtraMessageBox.Show(msg);
                    break;
                }
                else
                {
                    string msg = "Phân bổ tháng " + i.ToString() + " thành công!";
                    if (Config.GetValue("Language").ToString() == "1")
                        msg = UIDictionary.Translate(msg);
                    XtraMessageBox.Show(msg);
                }
            }
            Application.CurrentCulture = ci;
            if (kq)
                db.EndMultiTrans();
        }

        private void Filter_Load(object sender, EventArgs e)
        {
            string sql = "select *,0.0 as Ps from DMPB";
            if (_phanLoai != "")
                sql += " where PhanLoai = N'" + _phanLoai + "'";
            sql += " order by ttpb";
            trans = db.GetDataTable(sql);
            gridControl1.DataSource = trans;
            if (Config.GetValue("Language").ToString() == "1")
                FormFactory.DevLocalizer.Translate(this);
        }

        private List<DataRow> GetTransList()
        {
            List<DataRow> TransList = new List<DataRow>();
            TransList.Clear();
            if (rgOption.SelectedIndex == 1)
                foreach (int i in gridView1.GetSelectedRows())
                {
                    TransList.Add(gridView1.GetDataRow(i));
                }
            else
                for (int i = 0; i < gridView1.DataRowCount; i++)
                {
                    TransList.Add(gridView1.GetDataRow(i));
                }
            return TransList;
        }

        private bool Createbt(DataRow drPb,DataRow dr)
        {
            Guid iddt = Guid.NewGuid();
            string soct = "'PBCK" + DenNgay.Month.ToString("00") + "/" + (Int32.Parse(dr["TTPB"].ToString())).ToString("00") + "'";
            try
            {
                string tableName = "bltk";
                List<string> fieldName = new List<string>();
                List<string> Values = new List<string>();
                Guid id = new Guid();
                fieldName.Add("MTID");
                fieldName.Add("MTIDDT");
                fieldName.Add("Nhomdk");
                fieldName.Add("MaCT");
                fieldName.Add("SoCT");
                fieldName.Add("NgayCT");
                fieldName.Add("Ongba");
                fieldName.Add("DienGiai");
                fieldName.Add("TK");
                fieldName.Add("TKdu");
                fieldName.Add("Psno");
                fieldName.Add("Psco");
                if (drPb[dr["chitieu"].ToString()].ToString() != "")
                    fieldName.Add(dr["chitieu"].ToString());

                Values.Clear();
                Values.Add("convert( uniqueidentifier,'" + id.ToString() + "')");
                Values.Add("convert( uniqueidentifier,'" + iddt.ToString() + "')");
                Values.Add("'" + dr["MaCT"].ToString() + "'");
                Values.Add("'" + _mact + "'");
                Values.Add(soct);
                Values.Add("cast('" + DenNgay.ToString() + "' as datetime)");
                Values.Add("''");
                Values.Add("N'" + dr["DienGiai"] + " tháng " + DenThang.ToString() + "/" + DenNgay.Year.ToString() + "'");
                Values.Add("'" + drPb["tknguon"].ToString() + "'");
                Values.Add("'" + dr["tkDich"].ToString() + "'");
                double tienPb = Math.Round(double.Parse(drPb["tienPb"].ToString(), nfi),0);
                if (tienPb >= 0)//Dư nợ tknguon -> phát sinh có
                {
                    Values.Add("0");
                    Values.Add(tienPb.ToString("###########0.#######"));

                }
                else
                {
                    Values.Add(tienPb.ToString("###########0.#######").Replace("-", ""));
                    Values.Add("0");

                }
                if (drPb[dr["chitieu"].ToString()].ToString() != "")
                    Values.Add("'" + drPb[dr["chitieu"].ToString()].ToString() + "'");
                if (!db.insertRow(tableName, fieldName, Values))
                {
                    return false;
                }
                //bt2
                id = new Guid();
                Values.Clear();
                Values.Add("convert( uniqueidentifier,'" + id.ToString() + "')");
                Values.Add("convert( uniqueidentifier,'" + iddt.ToString() + "')");
                Values.Add("'" + dr["MaCT"].ToString() + "'");
                Values.Add("'" + _mact + "'");
                Values.Add(soct);
                Values.Add("cast('" + DenNgay.ToString() + "' as datetime)");
                Values.Add("''");
                Values.Add("N' " + dr["DienGiai"] + " tháng " + DenThang.ToString() + "/" + DenNgay.Year.ToString() + "'");
                Values.Add("'" + dr["tkDich"].ToString() + "'");
                Values.Add("'" + drPb["tknguon"].ToString() + "'");
                if (tienPb >= 0)//Dư nợ tknguon -> phát sinh có
                {

                    Values.Add(tienPb.ToString("###########0.#######"));
                    Values.Add("0");
                }
                else
                {
                    Values.Add("0");
                    Values.Add(tienPb.ToString("###########0.#######").Replace("-", ""));

                }
                if (drPb[dr["chitieu"].ToString()].ToString() != "")
                    Values.Add("'" + drPb[dr["chitieu"].ToString()].ToString() + "'");
                if (!db.insertRow(tableName, fieldName, Values))
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                XtraMessageBox.Show(e.Message);
                return false;

            } 
            return true;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            tuThang = int.Parse(spinEdit1.EditValue.ToString());
            DenThang = int.Parse(spinEdit2.EditValue.ToString());
            if (KhoaSo(DenThang))
                return;
            db.BeginMultiTrans();
            bool result = true;
            int Nam = int.Parse(Config.GetValue("NamLamViec").ToString());
            List<DataRow> TransList = GetTransList();
            for (int i = tuThang; i <= DenThang; i++)
            {
                string str = i.ToString() + "/" + "01" + "/" + Nam.ToString();
                TuNgay = Convert.ToDateTime(str);
                DenNgay = TuNgay.AddMonths(1).AddDays(-1);
                foreach (DataRow dr in TransList)
                {
                    string sql = "delete bltk where MaCT = '" + _mact + "' and NgayCt=cast('" + DenNgay.ToString() + "' as datetime) and NhomDK = '" + dr["MaCT"].ToString() + "'";
                    result = db.UpdateByNonQuery(sql);
                    //xoa kieu cu
                    sql = "delete bltk where mact='" + dr["MaCT"].ToString() + "' and month(Ngayct)=" + i.ToString();
                    result = db.UpdateByNonQuery(sql);
                    if (!result)
                        break;
                }
                string msg = "Đã xóa bút toán phân bổ tháng " + i.ToString();
                if (Config.GetValue("Language").ToString() == "1")
                    msg = UIDictionary.Translate(msg);
                if (result)
                    XtraMessageBox.Show(msg);
                else
                {
                    XtraMessageBox.Show("Lỗi khi xóa bút toán phân bổ tháng " + i.ToString());
                    break;
                }
            }
            if (result)
                db.EndMultiTrans();
        }

    }
}