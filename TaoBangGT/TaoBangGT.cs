using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Windows.Forms;
using System.Data;
using CDTDatabase;
namespace TaoBangGT
{
    class TaoBangGT : ICData
    {
        Database _dbData = Database.NewDataDatabase();
        DataRow _drMaster;
        public TaoBangGT()
        {
            _info = new InfoCustomData(IDataType.MasterDetailDt);
        }
        private InfoCustomData _info;
        private DataCustomData _data;

        #region ICData Members

        public DataCustomData Data
        {
            set { _data = value; }
        }

        public void ExecuteAfter()
        {
            
        }

        public void ExecuteBefore()
        {
            string tableName = _data.DrTableMaster["TableName"].ToString();
            //Custom nhóm giá thành : Mỗi nhóm giá thành có 1 bảng lưu riêng
            if (tableName.ToUpper() == "DMNHOMGT")
            {
                try
                {
                    _drMaster = _data.DsData.Tables[0].Rows[_data.CurMasterIndex];
                    if (_drMaster.RowState == DataRowState.Deleted)
                    {
                        _data.DsData.Tables[0].DefaultView.RowStateFilter = DataViewRowState.Deleted;
                        string Nhomgt = _data.DsData.Tables[0].DefaultView[0]["MaNhom"].ToString();
                        Deletetable("CoGia" + Nhomgt);
                        _data.DsData.Tables[0].DefaultView.RowStateFilter = DataViewRowState.None;

                        //
                    }
                    else if (_drMaster.RowState == DataRowState.Added)
                    {
                        string Nhomgt = _drMaster["MaNhom"].ToString();
                        CreateTable("CoGia" + Nhomgt);
                    }
                    _info.Result = true;
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    _info.Result = false;
                    return;
                }
            }
            if (tableName.ToUpper() == "CODTGT")
            {
                try
                {
                    string sql;
                    //Thêm 1 yếu tố, tạo 1 cột
                    DataView dvData = new DataView(_data.DsData.Tables[0]);
                    dvData.RowStateFilter = DataViewRowState.Added;
                    foreach (DataRowView dr in dvData)
                    {
                        sql = " alter table CoGia" + dr["MaNhom"].ToString() + " add " + dr["MaYT"].ToString();
                        sql += " [decimal](20, 6) not null  CONSTRAINT [df_CoGia" + dr["MaNhom"].ToString() + "_" + dr["MaYT"].ToString() + "] DEFAULT (0)";
                        _dbData.UpdateByNonQuery(sql);
                    }
                    dvData.RowStateFilter = DataViewRowState.Deleted;
                    foreach (DataRowView dr in dvData)
                    {
                        sql = " alter table CoGia" + dr["MaNhom"].ToString() + " drop CONSTRAINT [df_CoGia" + dr["MaNhom"].ToString() + "_" + dr["MaYT"].ToString() + "] \n";
                        sql += " alter table CoGia" + dr["MaNhom"].ToString() + " drop column " + dr["MaYT"].ToString();
                        _dbData.UpdateByNonQuery(sql);
                    }
                    dvData.RowStateFilter = DataViewRowState.ModifiedOriginal;
                    foreach (DataRowView dr in dvData)
                    {
                        sql = " alter table CoGia" + dr["MaNhom"].ToString() + " drop CONSTRAINT [df_CoGia" + dr["MaNhom"].ToString() + "_" + dr["MaYT"].ToString() + "] \n";
                        sql += " alter table CoGia" + dr["MaNhom"].ToString() + " drop column " + dr["MaYT"].ToString();
                        _dbData.UpdateByNonQuery(sql);
                    }
                    dvData.RowStateFilter = DataViewRowState.ModifiedCurrent;
                    foreach (DataRowView dr in dvData)
                    {
                        sql = " alter table CoGia" + dr["MaNhom"].ToString() + " add " + dr["MaYT"].ToString();
                        sql += " [decimal](20, 6) not null  CONSTRAINT [df_CoGia" + dr["MaNhom"].ToString() + "_" + dr["MaYT"].ToString() + "] DEFAULT (0)";
                        _dbData.UpdateByNonQuery(sql);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    _info.Result = false;
                    return;
                }

            }
        }

        public InfoCustomData Info
        {
            get { return _info; }
        }

        #endregion

        private void CreateTable(string NhomGt)
        {
            string sql = "create table " + NhomGt + "(";
            sql += "[ID] [int] IDENTITY (1, 1) NOT NULL ,";
            sql += "[NgayCT] [smalldatetime] NOT NULL ,";
            sql += "[MaSP] [varchar] (16) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,";
            sql += "[Soluong] [decimal](16, 6) NULL CONSTRAINT [df_" + NhomGt + "_soluong] DEFAULT ('0'),";
            sql += "[dddk] [decimal](16, 6) NULL CONSTRAINT [df_" + NhomGt + "_dddk] DEFAULT ('0'),";
            sql += "[ddck] [decimal](16, 6) NULL CONSTRAINT [df_" + NhomGt + "_ddck] DEFAULT ('0'),";
            sql += "[Gia] [decimal](16, 6) NULL CONSTRAINT [df_" + NhomGt + "_gia] DEFAULT ('0'),";
            sql += "CONSTRAINT [PK_" + NhomGt + "] PRIMARY KEY  CLUSTERED ([ID])  ON [PRIMARY], ";
            sql += "CONSTRAINT [fk_" + NhomGt + "_dmvt] FOREIGN KEY ([MaSP]) REFERENCES [DMVT] ([MaVT]) ON UPDATE CASCADE ";
            sql += ") ON [PRIMARY]";
            _dbData.UpdateByNonQuery(sql);

        }
        private void Deletetable(string NhomGt)
        {
            string sql = "Drop table " + NhomGt;
            _dbData.UpdateByNonQuery(sql);
        }

        //private bool _info.Result = true;
        //private StructPara _info;
        //#region ICustomData Members
        //DataRow _drMater;
        //public void ExecuteAfter()
        //{
           
        //}
        //private void CreateTable(string NhomGt)
        //{
        //    string sql = "create table " + NhomGt + "(";
        //    sql += "[ID] [int] IDENTITY (1, 1) NOT NULL ,";
        //    sql += "[NgayCT] [smalldatetime] NOT NULL ,";
        //    sql += "[MaSP] [varchar] (16) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,";
        //    sql += "[Soluong] [decimal](16, 6) NULL CONSTRAINT [df_" + NhomGt + "_soluong] DEFAULT ('0'),";
        //    sql += "[dddk] [decimal](16, 6) NULL CONSTRAINT [df_" + NhomGt + "_dddk] DEFAULT ('0'),";
        //    sql += "[ddck] [decimal](16, 6) NULL CONSTRAINT [df_" + NhomGt + "_ddck] DEFAULT ('0'),";
        //    sql += "[Gia] [decimal](16, 6) NULL CONSTRAINT [df_" + NhomGt + "_gia] DEFAULT ('0'),";
        //    sql += "CONSTRAINT [PK_" + NhomGt + "] PRIMARY KEY  CLUSTERED ([ID])  ON [PRIMARY], ";
        //    sql += "CONSTRAINT [fk_" + NhomGt + "_dmvt] FOREIGN KEY ([MaSP]) REFERENCES [DMVT] ([MaVT]) ON UPDATE CASCADE ";
        //    sql += ") ON [PRIMARY]";
        //    _dbData.UpdateByNonQuery(sql);

        //}
        //private void Deletetable(string NhomGt)
        //{
        //    string sql = "Drop table " + NhomGt;
        //    _dbData.UpdateByNonQuery(sql);
        //}
        //public void ExecuteBefore()
        //{
        //    //Custom nhóm giá thành : Mỗi nhóm giá thành có 1 bảng lưu riêng
        //    if (_info.TableName.ToUpper() == "DMNHOMGT")
        //    {
        //        try
        //        {
        //            _drMater = _data.DsData.Tables[0].Rows[_data.CurMasterIndex];
        //            if (_drMater.RowState == DataRowState.Deleted)
        //            {
        //                _data.DsData.Tables[0].DefaultView.RowStateFilter = DataViewRowState.Deleted;
        //                string Nhomgt = _data.DsData.Tables[0].DefaultView[0]["MaNhom"].ToString();
        //                Deletetable("CoGia" + Nhomgt);
        //                _data.DsData.Tables[0].DefaultView.RowStateFilter = DataViewRowState.None;

        //                //
        //            }
        //            else if (_drMater.RowState == DataRowState.Added)
        //            {
        //                string Nhomgt = _drMater["MaNhom"].ToString();
        //                CreateTable("CoGia" + Nhomgt);
        //            }
        //            _info.Result = true;
        //            return;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //            _info.Result = false;
        //            return;
        //        }
        //    }
        //    if (_info.TableName.ToUpper() == "CODTGT")
        //    {
        //        try
        //        {
        //            string sql;
        //            //Thêm 1 yếu tố, tạo 1 cột
        //            DataView dvData = new DataView(_data.DsData.Tables[0]);
        //            dvData.RowStateFilter = DataViewRowState.Added;
        //            foreach (DataRowView dr in dvData)
        //            {
        //                sql = " alter table CoGia" + dr["MaNhom"].ToString() + " add " + dr["MaYT"].ToString() ;
        //                sql += " [decimal](20, 6) not null  CONSTRAINT [df_CoGia" + dr["MaNhom"].ToString() + "_" +dr["MaYT"].ToString() + "] DEFAULT (0)";
        //                _dbData.UpdateByNonQuery(sql);
        //            }
        //            dvData.RowStateFilter = DataViewRowState.Deleted;
        //            foreach (DataRowView dr in dvData)
        //            {
        //                sql = " alter table CoGia" + dr["MaNhom"].ToString() + " drop CONSTRAINT [df_CoGia" + dr["MaNhom"].ToString() + "_" + dr["MaYT"].ToString() + "] \n";
        //                sql += " alter table CoGia" + dr["MaNhom"].ToString() + " drop column " + dr["MaYT"].ToString() ;
        //                _dbData.UpdateByNonQuery(sql);
        //            }
        //            dvData.RowStateFilter = DataViewRowState.ModifiedOriginal;
        //            foreach (DataRowView dr in dvData)
        //            {
        //                sql = " alter table CoGia" + dr["MaNhom"].ToString() + " drop CONSTRAINT [df_CoGia" + dr["MaNhom"].ToString() + "_" + dr["MaYT"].ToString() + "] \n";
        //                sql += " alter table CoGia" + dr["MaNhom"].ToString() + " drop column " + dr["MaYT"].ToString();
        //                _dbData.UpdateByNonQuery(sql);
        //            }
        //            dvData.RowStateFilter = DataViewRowState.ModifiedCurrent;
        //            foreach (DataRowView dr in dvData)
        //            {
        //                sql = " alter table CoGia" + dr["MaNhom"].ToString() + " add " + dr["MaYT"].ToString();
        //                sql += " [decimal](20, 6) not null  CONSTRAINT [df_CoGia" + dr["MaNhom"].ToString() + "_" + dr["MaYT"].ToString() + "] DEFAULT (0)";
        //                _dbData.UpdateByNonQuery(sql);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //            _info.Result = false;
        //            return;
        //        }
                
        //    }
        //}

        //public StructPara Info
        //{
        //    //set { throw new Exception("The method or operation is not implemented."); }
        //    set
        //    {
        //        _info = value;
        //    }
        //}

        //public bool Result
        //{
        //    //get { throw new Exception("The method or operation is not implemented."); }
        //    get
        //    {
        //        return _info.Result;
        //    }
        //}

        //#endregion
    }
}
