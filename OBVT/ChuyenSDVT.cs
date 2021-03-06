using System;
using System.Collections.Generic;
using System.Text;
using Plugins;
using System.Data;

namespace ChuyenSDVT
{
    public class ChuyenSDVT : ICData
    {
        #region ICData Members
        private DataCustomData _data;
        private InfoCustomData _info;
        public ChuyenSDVT()
        {
            _info = new InfoCustomData(IDataType.Single);
        }
        public DataCustomData Data
        {
            set { _data = value; }
        } 

        public void ExecuteAfter()
        {
            string tableName = _data.DrTable["TableName"].ToString();
            if (tableName == "OBVT" || tableName == "OBNTXT")
            {
                _data.DbData.EndMultiTrans();       //hoan thanh viec cap nhat du lieu truoc

                _data.DbData.BeginMultiTrans();
                DataView dv = new DataView(_data.DsDataCopy.Tables[0]);
                dv.RowStateFilter = DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedCurrent;
                foreach (DataRowView drv in dv)
                {
                    //kiem tra ma kho truoc
                    string maKho = drv["MaKho"].ToString();
                    DataTable dt = _data.DbData.GetDataTable("select * from DMKho where MaKho = '" + maKho + "'");
                    if (dt == null || dt.Rows.Count == 0)
                        continue;
                    DataRow drKho = dt.Rows[0];
                    if (Boolean.Parse(drKho["KDL"].ToString()) && drKho["TKKDL"].ToString() != "")
                    {
                        string tk = drKho["TKKDL"].ToString();
                        string s = "delete from obtk where tk = '" + tk + "'";
                        _data.DbData.UpdateByNonQuery(s);
                        s = "insert into obtk(tk, duno, dunont) " +
                            " select v.TKKDL, sum(o.dudau), sum(dudaunt) " +
                            " from " + tableName + " o inner join dmkho v on o.makho = v.makho group by v.TKKDL having v.TKKDL = '" + tk + "'";
                        _data.DbData.UpdateByNonQuery(s);
                    }
                    else
                    {
                        //tiep tuc kiem tra ma vt
                        string maVT = drv["MaVT"].ToString();
                        string tk = _data.DbData.GetValue("select TkKho from DMVT where MaVT = '" + maVT + "'").ToString();
                        string s = "delete from obtk where tk = '" + tk + "'";
                        _data.DbData.UpdateByNonQuery(s);
                        s = "insert into obtk(tk, duno, dunont) " +
                            " select v.tkkho, sum(o.dudau), sum(dudaunt) " +
                            " from " + tableName + " o inner join dmvt v on o.mavt = v.mavt inner join dmkho k on o.makho = k.makho " +
                            " where k.KDL = 0 and k.TKKDL is null group by v.tkkho having v.tkkho = '" + tk + "'";
                        _data.DbData.UpdateByNonQuery(s);
                    }
                }
                if (!_data.DbData.HasErrors)
                    _data.DbData.EndMultiTrans();
            }
//            if (tableName == "DMVT")
//            {
//                if (_data.CurMasterIndex < 0)
//                    return;
//                DataRow drMaster = _data.DsDataCopy.Tables[0].Rows[_data.CurMasterIndex];
//                if (drMaster.RowState == DataRowState.Modified      //thay đổi tài khoản kho --> cập nhật lại số dư tài khoản kho
//                    && drMaster["TkKho", DataRowVersion.Original].ToString() != drMaster["TkKho", DataRowVersion.Current].ToString())
//                {
//                    _data.DbData.EndMultiTrans();       //hoan thanh viec cap nhat du lieu truoc
//                    string otk = drMaster["TkKho", DataRowVersion.Original].ToString();
//                    string ntk = drMaster["TkKho", DataRowVersion.Current].ToString();

//                    _data.DbData.BeginMultiTrans();
//                    string s = "delete from obtk where tk in ('" + otk + "','" + ntk + "')";
//                    _data.DbData.UpdateByNonQuery(s);
//                    s = @"insert into obtk(tk, duno, dunont)
//                        select v.tkkho, sum(o.dudau), sum(dudaunt)
//                        from (select makho, mavt, dudau, dudaunt from OBVT
//                            union all select makho, mavt, dudau, dudaunt from OBNTXT)o 
//                        inner join dmvt v on o.mavt = v.mavt inner join dmkho k on o.makho = k.makho
//                        where k.KDL = 0 and k.TKKDL is null group by v.tkkho having v.tkkho in ('" + otk + "','" + ntk + "')";
//                    _data.DbData.UpdateByNonQuery(s);
//                    if (!_data.DbData.HasErrors)
//                        _data.DbData.EndMultiTrans();
//                }
//            }
        }

        public void ExecuteBefore()
        {
        }

        public InfoCustomData Info
        {
            get { return _info; }
        }
        #endregion
    }
}
