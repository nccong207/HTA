using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Data;
using CDTDatabase;

namespace TMBCTC
{
    public class ExcelReport
    {
        private string _tmpFile = string.Empty;
        private string _fileName = string.Empty;
        private DateTime _fromDate;
        private DateTime _toDate;
        private Database _dbData = Database.NewDataDatabase();
        private bool _isError = false;

        public bool IsError
        {
            get { return _isError; }
        }

        public ExcelReport(string tmpFile, string fileName, DateTime fromDate, DateTime toDate)
        {
            _fileName = fileName;
            _tmpFile = tmpFile;
            _fromDate = fromDate;
            _toDate = toDate;
        }

        public void FillData()
        {
            Application app = new ApplicationClass();
            Workbook wb = app.Workbooks.Open(_tmpFile, Type.Missing, false, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, 
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            try
            {
                Worksheet ws = (Worksheet)wb.Sheets[1];
                ws.get_Range(ws.Cells[1, 1], ws.Cells[1, 1]).ClearNotes();
                Range rData = ws.UsedRange;
                object[,] o = (object[,])rData.get_Value(XlRangeValueDataType.xlRangeValueDefault);
                for (int i = 1; i <= o.GetLength(0); i++)
                {
                    for (int j = 1; j <= o.GetLength(1); j++)
                    {
                        int c = o.GetLength(1);
                        if (o[i, j] != null)
                        {
                            string s = o[i, j].ToString().Trim();
                            if (s.StartsWith("&") && s.EndsWith("&"))
                            {
                                decimal value = Decimal.Parse(GetData(s).ToString());
                                if (value == -1)
                                {
                                    _isError = true;
                                    (rData[i, j] as Range).AddComment("Incorrect format formula");
                                }
                                else
                                    rData[i, j] = value;
                            }
                        }
                    }
                } 
            }
            catch 
            {
                _isError = true;
            }
            finally
            {
                try
                {
                    wb.SaveAs(_fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    wb.Close(false, false, false);
                    app.Quit();
                }
                catch { }
                finally
                {
                    app.Quit();
                }
            }
        }

        private object GetData(string s)
        {
            string[] f = s.Replace("&","").Split(new char[] { ';' });
            if (f.Length < 3 || f.Length > 4)
                return -1;
            string storeName = "GetValueForExcelReport";
            string[] paraNames;
            object[] paraValues;
            if (f.Length == 3)
            {
                paraNames = new string[] { "@ngayct1", "@ngayct2", "@loaiCt", "@tk", "@tkdu", "@iscn", "@value" };
                paraValues = new object[] { _fromDate, _toDate, f[2], f[0], f[1], 0, null };
            }
            else
            {
                paraNames = new string[] { "@ngayct1", "@ngayct2", "@loaiCt", "@tk", "@tkdu", "@iscn", "@value" };
                paraValues = new object[] { _fromDate, _toDate, f[2], f[0], f[1], f[3], null };
            }
            object[] os = _dbData.GetValueByStore(storeName, paraNames, paraValues,
                new SqlDbType[] { SqlDbType.DateTime, SqlDbType.DateTime, SqlDbType.Int, SqlDbType.NVarChar, SqlDbType.NVarChar, SqlDbType.Bit, SqlDbType.Float },
                new ParameterDirection[] { ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Input, ParameterDirection.Output });
            if (os == null || os.Length == 0)
                return null;
            return os[0];
        }
    }
}
