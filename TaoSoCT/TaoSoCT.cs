using System;
using System.Collections.Generic;
using System.Text;
using DevExpress;
using CDTDatabase;
using CDTLib;
using Plugins;
using System.Data;

namespace TaoSoCT
{
    public class TaoSoCT:ICData
    {
        private InfoCustomData _info;
        private DataCustomData _data;
        Database db = Database.NewDataDatabase();
        Database dbCDT = Database.NewStructDatabase();

        #region ICData Members
 
        public TaoSoCT()
        {
            _info = new InfoCustomData(IDataType.MasterDetailDt);
        }

        public DataCustomData Data
        {
            set { _data = value; }
        }

        public InfoCustomData Info
        {
            get { return _info; }
        }

        public void ExecuteAfter()
        {
            
        }

        public void ExecuteBefore()
        {
            List<string> lstTable = new List<string>(new string[] {"MT11","MT12","MT15","MT16",
                "MT21","MT22","MT23","MT24","MT25","MT26","MT31","MT32","MT33","MT34","MT41","MT42","MT43","MT44","MT45","MT51"});
            if (!lstTable.Contains(_data.DrTableMaster["TableName"].ToString()))
                return;
            CreateCT();
        }

        private bool KTSuaNgay(DataRow drMaster)
        {
            DateTime dt1 = DateTime.Parse(drMaster["NgayCT", DataRowVersion.Current].ToString());
            DateTime dt2 = DateTime.Parse(drMaster["NgayCT", DataRowVersion.Original].ToString());
            return (dt1.Year != dt2.Year || dt1.Month != dt2.Month);
        }

        void CreateCT()
        {
            if (_data.CurMasterIndex < 0)
                return;
            DataRow drMaster = _data.DsData.Tables[0].Rows[_data.CurMasterIndex];
            if (!drMaster.Table.Columns.Contains("SoCT") || !drMaster.Table.Columns.Contains("NgayCT") || !drMaster.Table.Columns.Contains("MaCT"))
                return;
            if (drMaster.RowState == DataRowState.Added
                || (drMaster.RowState == DataRowState.Modified && KTSuaNgay(drMaster)))
            {
                if (_data.DrTable["MaCT"].ToString() == "")
                    return;

                string sql = "", soctNew = "", mact = "", maCN = "", prefix = "", suffix = "", Thang = "", Nam = "";
                mact = _data.DrTable["MaCT"].ToString();
                DateTime NgayCT = (DateTime)drMaster["NgayCT"];
                // Tháng: 2 chữ số
                // Năm: 2 số cuối của năm
                Thang = NgayCT.Month.ToString("D2");
                Nam = NgayCT.Year.ToString();

                Nam = Nam.Substring(2, 2);

                if (Config.GetValue("MaCN") != null)
                    maCN = Config.GetValue("MaCN").ToString();
                // 3. Số phiếu thu tự nhảy. Qua mỗi năm số phiếu nhảy lại 001
                // vd: PT1501001
                // Số CT = [Mã CT] + [Năm (YY)] + [Thang(MM)] + [STT(XXX)]
                prefix = mact + Nam + Thang;

                sql = string.Format(@"  SELECT	TOP 1 CAST(REPLACE(SoCT,'{1}','') AS INT) [SoCT]
                                        FROM	{0}
                                        WHERE	SoCT LIKE '{1}%' 
		                                        AND ISNUMERIC(REPLACE(SoCT,'{1}','')) = 1
                                        ORDER BY CAST(REPLACE(SoCT,'{1}','') AS INT) DESC"
                                    , _data.DrTableMaster["TableName"].ToString(), prefix);

                using (DataTable dt = db.GetDataTable(sql))
                {
                    if (dt.Rows.Count > 0)
                    {
                        int i = (int)dt.Rows[0]["SoCT"] + 1;
                        soctNew = prefix + i.ToString("D3");
                    }
                    else
                    {
                        soctNew = prefix + "001";
                    }
                }
                if (soctNew != "")
                    drMaster["SoCT"] = soctNew;
            }
        }

        #endregion
    }
}
