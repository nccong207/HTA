using System;
using System.Collections.Generic;
using System.Text;
using CDTDatabase;
using System.Data;
using CDTLib;
using System.Windows.Forms;
using Formula;
namespace GiaThanh
{
    class NhomGt
    {

        private DateTime _Tungay;
        private DateTime _Denngay;
        private DataTable TpNhapKho;
        private Database _dbData;
        private Database _dbStruct;
        private List<Ytgt> LstYt;
        private DataTable _BangGia;
        private string MaNhom;
        public NhomGt(DateTime tungay, DateTime denngay, string manhom)
        {
            this._dbData = Database.NewDataDatabase();
            this._dbStruct = Database.NewStructDatabase();
            this._Tungay = tungay;
            this._Denngay = denngay;
            this.TpNhapKho = this.GetSP(manhom);
            this.MaNhom = manhom;
            this.TinhGiaThanh();
        }


        // Methods
        private DataTable GetSP(string manhom)
        {
            DataTable table1 = this._dbData.GetDataSetByStore("SlTpNhapkho", new string[] { "@tungay", "@denngay", "@nhom" }, new object[] { this._Tungay, this._Denngay, manhom });
            table1.Columns[0].Caption = "Mã sản phẩm";
            table1.Columns[1].Caption = "Tên sản phẩm";
            table1.Columns[2].Caption = "Số lượng nhập kho";
            return table1;
        }

        public void TinhGiaThanh()
        {
            string text1;
            if (this.MaNhom == "")
            {
                text1 = "select * from dmytgt where mayt in (select mayt from codtgt) order by thutu";
            }
            else
            {
                text1 = "select * from dmytgt where mayt in (select mayt from codtgt where manhom='" + this.MaNhom + "') order by thutu";
            }
            _dbData.BeginMultiTrans();
            DataTable table1 = this._dbData.GetDataTable(text1);
            this._BangGia = this.TpNhapKho.Copy();
            this._BangGia.PrimaryKey = new DataColumn[] { this._BangGia.Columns[0] };
            this.LstYt = new List<Ytgt>();
            for (int i = 0; i < table1.Rows.Count; i++)
            {
                Ytgt yt;
                DataRow row1 = table1.Rows[i];
                switch (int.Parse(row1["Cachtinh"].ToString()))
                {
                    case 0:
                        {
                            yt = new YtGiandon(row1, this.TpNhapKho, this._Tungay, this._Denngay);
                            yt.DbData = _dbData;
                            yt.tk = row1["TK"].ToString().Trim();
                            yt.Manhom = this.MaNhom;
                            yt.TinhGiatri();
                            this.ImportCol(yt);
                            yt.Kq.PrimaryKey = new DataColumn[] { yt.Kq.Columns["MaSP"] };
                            this.LstYt.Add(yt);
                            continue;
                        }
                    case 1:
                        {
                            yt = new YtDinhMuc(row1, this.TpNhapKho, this._Tungay, this._Denngay);
                            yt.DbData = _dbData;
                            yt.tk = row1["TK"].ToString().Trim();
                            yt.Manhom = this.MaNhom;
                            yt.TinhGiatri();
                            this.ImportCol(yt);
                            yt.Kq.PrimaryKey = new DataColumn[] { yt.Kq.Columns["MaSP"] };
                            this.LstYt.Add(yt);
                            continue;
                        }
                    case 2:
                        {
                            yt = new YtHeso(row1, this.TpNhapKho, this._Tungay, this._Denngay);
                            yt.DbData = _dbData;
                            yt.tk = row1["TK"].ToString().Trim();
                            yt.Manhom = this.MaNhom;
                            yt.TinhGiatri();
                            this.ImportCol(yt);
                            yt.Kq.PrimaryKey = new DataColumn[] { yt.Kq.Columns["MaSP"] };
                            this.LstYt.Add(yt);
                            continue;
                        }
                    case 3:
                        {
                            if (row1["YTPT"] == null) continue;
                            yt = new YtPhanBo(row1, this.TpNhapKho, this._Tungay, this._Denngay);
                            yt.DbData = _dbData;
                            yt.tk = row1["TK"].ToString().Trim();
                            yt.Manhom = this.MaNhom;
        
                            foreach (Ytgt ytgt2 in this.LstYt)
                            {
                                if ((yt as YtPhanBo).bt.variables.Contains(ytgt2.Name.Trim()))
                                {
                                      (yt as YtPhanBo).YTPt.Add(ytgt2);
                                }
                            }
                            yt.TinhGiatri();
                            this.ImportCol(yt);
                            yt.Kq.PrimaryKey = new DataColumn[] { yt.Kq.Columns["MaSP"] };
                            this.LstYt.Add(yt);
                            continue;
                        }
                    default:
                        {
                            continue;
                        }
                }
                
            }
            DataTable tbdd = this._dbData.GetDataSetByStore("getdd", new string[] { "@tungay", "@denngay","@manhom" }, new object[] { this._Tungay, this._Denngay,this.MaNhom });
            this.ImportDD(tbdd);
            this.Tinhgia();
            if (!_dbData.HasErrors)
                _dbData.EndMultiTrans();
        }

        private void Tinhgia()
        {
            DataColumn column = new DataColumn("Gia", typeof(double));
            column.DefaultValue = 0;
            column.Caption = "Giá thành";
            this._BangGia.Columns.Add(column);
            foreach (DataRow row1 in this._BangGia.Rows)
            {
                //MessageBox.Show(row1[2].ToString());
                double num1 = double.Parse(row1[2].ToString());
                double num2 = 0;
                for (int i = 3; i < (this._BangGia.Columns.Count - 2); i++)
                {
                    
                    num2 += double.Parse(row1[i].ToString());

                }
                
                num2 -= double.Parse(row1[(this._BangGia.Columns.Count - 2)].ToString());
                if (num1 > 0)
                {
                    row1[column] = num2 / num1;
                }
            }
        }

        private void ImportDD(DataTable tbdd)
        {
            DataColumn column = new DataColumn("dddk", typeof(double));
            column.DefaultValue = 0;
            column.Caption = "Dở dang đầu kỳ";
            this._BangGia.Columns.Add(column);
            DataColumn column2 = new DataColumn("ddck", typeof(double));
            column2.DefaultValue = 0;
            column2.Caption = "Dở dang cuối kỳ";
            this._BangGia.Columns.Add(column2);
            for (int i = 0; i < tbdd.Rows.Count; i++)
            {
                DataRow row1 = this._BangGia.Rows.Find(tbdd.Rows[i][0]);
                if (row1 != null)
                {
                    row1[column] = tbdd.Rows[i]["dddk"];
                    row1[column2] = tbdd.Rows[i]["ddck"];
                }
            }
        }

        private void ImportCol(Ytgt yt)
        {
            DataColumn column = new DataColumn();
            column.ColumnName = yt.Kq.Columns[yt.Name].ColumnName;
            column.DataType = yt.Kq.Columns[yt.Name].DataType;
            column.DefaultValue = yt.Kq.Columns[yt.Name].DefaultValue;
            column.Caption = yt.Kq.Columns[yt.Name].Caption;
            if (!this._BangGia.Columns.Contains(column.ColumnName))
            {
                this._BangGia.Columns.Add(column);
            }
            else
            {
                return;
            }
            for (int i = 0; i < yt.Kq.Rows.Count; i++)
            {
                DataRow row1 = this._BangGia.Rows.Find(yt.Kq.Rows[i][0]);
                if (row1 != null)
                {
                    row1[column.ColumnName] = yt.Kq.Rows[i][yt.Name];
                }
            }
        }

        public bool UpdateGia() 
        {
            string DtName = "Dt41";
            string MtName = "Mt41";
            string Mtid = this._dbStruct.GetValue("select systableid from systable where tableName='" + MtName + "' and syspackageid=" + int.Parse(Config.GetValue("sysPackageID").ToString())).ToString();
            string Dtid = this._dbStruct.GetValue("select systableid from systable where tableName='" + DtName + "' and syspackageid=" + int.Parse(Config.GetValue("sysPackageID").ToString())).ToString(); ;

            foreach (DataRow dr in this._BangGia.Rows)
            {
                this.updateMTDT(dr);
            }
            new updateGiaThanh(this._dbData, MtName, DtName,Mtid,Dtid, "Gia", this._Tungay, this._Denngay, "ngayct").Update();
            new PostBl(this._dbData, Mtid, this._Tungay, this._Denngay).Post();
            SaveBangGia();
            return true;
        }
        private void SaveBangGia()
        {
            if (MaNhom == "") return;
            string sql = "delete CoGia" + MaNhom + " where ngayct between '" + _Tungay.ToShortDateString() + "' and '" + _Denngay.ToShortDateString() + "'";
            _dbData.UpdateByNonQuery(sql);
            foreach (DataRow dr in _BangGia.Rows)
            {
                sql="insert into CoGia" + MaNhom + "(";
                foreach(DataColumn col in _BangGia.Columns)
                {
                    if (col.ColumnName == "tenvt") continue;
                    sql +=col.ColumnName +",";
                }
                sql += "ngayct ";
                sql += ") values (" ;
                 foreach(DataColumn col in _BangGia.Columns)
                {
                    if (col.ColumnName == "tenvt") continue;
                    
                    if (col.DataType == typeof(double) || col.DataType == typeof(decimal))
                     {
                        sql +=dr[col].ToString() +",";
                     }else 
                     {
                         sql +="'" + dr[col].ToString() + "'" + ",";
                     }
                }
                sql +="'" + _Denngay.ToShortDateString() + "')";
                _dbData.UpdateByNonQuery(sql);
            }
            
            
        }
        private void updateMTDT(DataRow dr)
        {
            if (dr["Gia"].ToString() != string.Empty)
            {
                string str0 = "update dt41 set gia = " + dr["Gia"].ToString().Replace(",", ".");
                str0 = str0 + " where  mavt='" + dr["masp"].ToString() + "'";
                str0 = str0 + " and dt41.mt41id in  (select mt41id from mt41 where ";
                string text2 = str0;
                string[] values = new string[] { text2, " mt41.ngayct between cast('", this._Tungay.ToString(), "' as datetime) and cast('", this._Denngay.ToString(), "' as datetime))" };
                str0 = string.Concat(values);
                this._dbData.UpdateByNonQuery(str0);
                str0 = "update dt41 set giant = a.gia/b.tygia   from ";
                str0 = str0 + " dt41 a, mt41 b  where b.MaNT <> 'VND' and a.mt41id = b.mt41id and ";
                text2 = str0;
                values = new string[] { text2, "b.ngayct between cast('", this._Tungay.ToString(), "' as datetime) and cast('", this._Denngay.ToString(), "' as datetime)" };
                str0 = string.Concat(values);
                this._dbData.UpdateByNonQuery(str0);
            }
        }


        // Properties
        public DataTable BangGia
        {
            get
            {
                return this._BangGia;
            }
        }
		
    }
}
