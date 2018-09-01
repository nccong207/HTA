using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using DevExpress;
using DevExpress.XtraEditors;
using CDTDatabase;
using CDTLib;
using Plugins;

namespace UpperSerial
{
    public class UpperSerial:ICControl
    {
        #region ICControl Members

        private DataCustomFormControl data ;
        private InfoCustomControl info = new InfoCustomControl(IDataType.MasterDetailDt);
        //private bool flag = false;
        public void AddEvent()
        {
            TextEdit txtSerial;
            if (data.BsMain == null || data.BsMain.DataSource == null)
                return;
            DataTable dt = (data.BsMain.DataSource as DataSet).Tables[0];
            if (dt.Columns.Contains("SoSeri"))
            {
                txtSerial = data.FrmMain.Controls.Find("SoSeri", true)[0] as TextEdit;
                if (txtSerial == null)
                    return; 
                txtSerial.Properties.CharacterCasing = CharacterCasing.Upper;
            }            
        }

        //void txtSerial_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        //{
        //    if (!flag)
        //    {
        //        TextEdit Serial = sender as TextEdit;
        //        if (Serial.Properties.ReadOnly)
        //            return;
        //        flag = true;                                
        //        Serial.EditValue = Serial.Text.ToUpper();
        //        Serial.SelectionStart = Serial.Text.Length;
        //    }
        //    else
        //        flag = false;
        //}
       
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
