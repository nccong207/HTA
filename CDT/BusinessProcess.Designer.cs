namespace CDT
{
    public class MyButton : DevExpress.XtraEditors.SimpleButton
    {
        protected override DevExpress.XtraEditors.ViewInfo.BaseStyleControlViewInfo CreateViewInfo()
        {
            return new MyButtonViewInfo(this);
        }
    }

    class MyButtonViewInfo : DevExpress.XtraEditors.ViewInfo.SimpleButtonViewInfo
    {
        public MyButtonViewInfo(DevExpress.XtraEditors.SimpleButton owner) : base(owner) { }
        protected override DevExpress.Utils.Drawing.ObjectState CalcButtonState(DevExpress.XtraEditors.Drawing.EditorButtonObjectInfoArgs buttonInfo, DevExpress.Utils.Drawing.ObjectState state)
        {
            //if (state == DevExpress.Utils.Drawing.ObjectState.Hot)
            return DevExpress.Utils.Drawing.ObjectState.Normal;
            //else
            //    return state;
        }
    }

    partial class BusinessProcess
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.abc = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgNV = new DevExpress.XtraLayout.LayoutControl();
            this.img30 = new DevExpress.XtraEditors.PictureEdit();
            this.img20 = new DevExpress.XtraEditors.PictureEdit();
            this.img33 = new DevExpress.XtraEditors.PictureEdit();
            this.img23 = new DevExpress.XtraEditors.PictureEdit();
            this.img29 = new DevExpress.XtraEditors.PictureEdit();
            this.img19 = new DevExpress.XtraEditors.PictureEdit();
            this.img13 = new DevExpress.XtraEditors.PictureEdit();
            this.img32 = new DevExpress.XtraEditors.PictureEdit();
            this.img31 = new DevExpress.XtraEditors.PictureEdit();
            this.img28 = new DevExpress.XtraEditors.PictureEdit();
            this.img27 = new DevExpress.XtraEditors.PictureEdit();
            this.img26 = new DevExpress.XtraEditors.PictureEdit();
            this.img25 = new DevExpress.XtraEditors.PictureEdit();
            this.img24 = new DevExpress.XtraEditors.PictureEdit();
            this.img22 = new DevExpress.XtraEditors.PictureEdit();
            this.img21 = new DevExpress.XtraEditors.PictureEdit();
            this.img18 = new DevExpress.XtraEditors.PictureEdit();
            this.img17 = new DevExpress.XtraEditors.PictureEdit();
            this.img16 = new DevExpress.XtraEditors.PictureEdit();
            this.img15 = new DevExpress.XtraEditors.PictureEdit();
            this.img12 = new DevExpress.XtraEditors.PictureEdit();
            this.img11 = new DevExpress.XtraEditors.PictureEdit();
            this.img14 = new DevExpress.XtraEditors.PictureEdit();
            this.lgrNV = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgDM = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem14 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem13 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem11 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem12 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.img34 = new DevExpress.XtraEditors.PictureEdit();
            this.img35 = new DevExpress.XtraEditors.PictureEdit();
            this.img36 = new DevExpress.XtraEditors.PictureEdit();
            this.img37 = new DevExpress.XtraEditors.PictureEdit();
            this.img38 = new DevExpress.XtraEditors.PictureEdit();
            this.img39 = new DevExpress.XtraEditors.PictureEdit();
            this.img40 = new DevExpress.XtraEditors.PictureEdit();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem9 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem10 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem15 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem16 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.btn25 = new CDT.MyButton();
            this.btn24 = new CDT.MyButton();
            this.btn3 = new CDT.MyButton();
            this.btn2 = new CDT.MyButton();
            this.btn1 = new CDT.MyButton();
            this.btn22 = new CDT.MyButton();
            this.btn20 = new CDT.MyButton();
            this.btn18 = new CDT.MyButton();
            this.btn23 = new CDT.MyButton();
            this.btn16 = new CDT.MyButton();
            this.btn12 = new CDT.MyButton();
            this.btn8 = new CDT.MyButton();
            this.btn21 = new CDT.MyButton();
            this.btn19 = new CDT.MyButton();
            this.btn17 = new CDT.MyButton();
            this.btn15 = new CDT.MyButton();
            this.btn14 = new CDT.MyButton();
            this.btn13 = new CDT.MyButton();
            this.btn11 = new CDT.MyButton();
            this.btn10 = new CDT.MyButton();
            this.btn9 = new CDT.MyButton();
            this.btn7 = new CDT.MyButton();
            this.btn6 = new CDT.MyButton();
            this.btn5 = new CDT.MyButton();
            this.lciNV17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV24 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV25 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem54 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem56 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem52 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem51 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem50 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem49 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem48 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem44 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem46 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem42 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem41 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem40 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem39 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem38 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem34 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem36 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNV3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.abc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgNV)).BeginInit();
            this.lcgNV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img30.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img20.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img33.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img23.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img29.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img19.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img13.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img32.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img31.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img28.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img27.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img26.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img25.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img24.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img22.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img21.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img18.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img17.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img16.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img15.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img12.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img11.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img14.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lgrNV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img34.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img35.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img36.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img37.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img38.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img39.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img40.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem54)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem56)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem52)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem51)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem49)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem48)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem44)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem46)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem42)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem41)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem40)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem39)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem38)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem34)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem36)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            this.SuspendLayout();
            // 
            // abc
            // 
            this.abc.AppearanceGroup.Options.UseTextOptions = true;
            this.abc.AppearanceGroup.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.abc.CustomizationFormText = "Quy trình nghiệp vụ";
            this.abc.Location = new System.Drawing.Point(125, 0);
            this.abc.Name = "abc";
            this.abc.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.abc.Size = new System.Drawing.Size(511, 580);
            this.abc.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.abc.Text = "abc";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.layoutControlGroup1.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup1.AppearanceGroup.Options.UseTextOptions = true;
            this.layoutControlGroup1.AppearanceGroup.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlGroup1.CustomizationFormText = "Quy trình nghiệp vụ";
            this.layoutControlGroup1.Location = new System.Drawing.Point(125, 0);
            this.layoutControlGroup1.Name = "grNghiepVu";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(511, 580);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup1.Text = "xyz";
            // 
            // lcgNV
            // 
            this.lcgNV.Controls.Add(this.btn25);
            this.lcgNV.Controls.Add(this.btn24);
            this.lcgNV.Controls.Add(this.img40);
            this.lcgNV.Controls.Add(this.img39);
            this.lcgNV.Controls.Add(this.btn3);
            this.lcgNV.Controls.Add(this.btn2);
            this.lcgNV.Controls.Add(this.btn1);
            this.lcgNV.Controls.Add(this.img38);
            this.lcgNV.Controls.Add(this.img37);
            this.lcgNV.Controls.Add(this.img36);
            this.lcgNV.Controls.Add(this.img35);
            this.lcgNV.Controls.Add(this.img34);
            this.lcgNV.Controls.Add(this.btn22);
            this.lcgNV.Controls.Add(this.btn20);
            this.lcgNV.Controls.Add(this.btn18);
            this.lcgNV.Controls.Add(this.img30);
            this.lcgNV.Controls.Add(this.img20);
            this.lcgNV.Controls.Add(this.btn23);
            this.lcgNV.Controls.Add(this.btn16);
            this.lcgNV.Controls.Add(this.btn12);
            this.lcgNV.Controls.Add(this.btn8);
            this.lcgNV.Controls.Add(this.img33);
            this.lcgNV.Controls.Add(this.img23);
            this.lcgNV.Controls.Add(this.img29);
            this.lcgNV.Controls.Add(this.img19);
            this.lcgNV.Controls.Add(this.img13);
            this.lcgNV.Controls.Add(this.btn21);
            this.lcgNV.Controls.Add(this.btn19);
            this.lcgNV.Controls.Add(this.btn17);
            this.lcgNV.Controls.Add(this.btn15);
            this.lcgNV.Controls.Add(this.img32);
            this.lcgNV.Controls.Add(this.btn14);
            this.lcgNV.Controls.Add(this.img31);
            this.lcgNV.Controls.Add(this.btn13);
            this.lcgNV.Controls.Add(this.img28);
            this.lcgNV.Controls.Add(this.img27);
            this.lcgNV.Controls.Add(this.img26);
            this.lcgNV.Controls.Add(this.img25);
            this.lcgNV.Controls.Add(this.img24);
            this.lcgNV.Controls.Add(this.btn11);
            this.lcgNV.Controls.Add(this.img22);
            this.lcgNV.Controls.Add(this.btn10);
            this.lcgNV.Controls.Add(this.img21);
            this.lcgNV.Controls.Add(this.btn9);
            this.lcgNV.Controls.Add(this.img18);
            this.lcgNV.Controls.Add(this.img17);
            this.lcgNV.Controls.Add(this.img16);
            this.lcgNV.Controls.Add(this.img15);
            this.lcgNV.Controls.Add(this.btn7);
            this.lcgNV.Controls.Add(this.img12);
            this.lcgNV.Controls.Add(this.btn6);
            this.lcgNV.Controls.Add(this.img11);
            this.lcgNV.Controls.Add(this.img14);
            this.lcgNV.Controls.Add(this.btn5);
            this.lcgNV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcgNV.Location = new System.Drawing.Point(0, 0);
            this.lcgNV.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.lcgNV.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lcgNV.Name = "lcgNV";
            this.lcgNV.OptionsView.ShareLookAndFeelWithChildren = false;
            this.lcgNV.Root = this.lgrNV;
            this.lcgNV.Size = new System.Drawing.Size(950, 600);
            this.lcgNV.TabIndex = 1;
            this.lcgNV.Text = "Quy trình nghiệp vụ";
            // 
            // img30
            // 
            this.img30.Location = new System.Drawing.Point(633, 294);
            this.img30.Name = "img30";
            this.img30.Properties.AllowFocused = false;
            this.img30.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img30.Properties.Appearance.Options.UseBackColor = true;
            this.img30.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img30.Properties.NullText = " ";
            this.img30.Properties.ReadOnly = true;
            this.img30.Properties.ShowMenu = false;
            this.img30.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchVertical;
            this.img30.Size = new System.Drawing.Size(90, 76);
            this.img30.TabIndex = 9;
            // 
            // img20
            // 
            this.img20.Location = new System.Drawing.Point(633, 118);
            this.img20.Name = "img20";
            this.img20.Properties.AllowFocused = false;
            this.img20.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img20.Properties.Appearance.Options.UseBackColor = true;
            this.img20.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img20.Properties.NullText = " ";
            this.img20.Properties.ReadOnly = true;
            this.img20.Properties.ShowMenu = false;
            this.img20.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchVertical;
            this.img20.Size = new System.Drawing.Size(90, 76);
            this.img20.TabIndex = 9;
            // 
            // img33
            // 
            this.img33.Location = new System.Drawing.Point(533, 380);
            this.img33.Name = "img33";
            this.img33.Properties.AllowFocused = false;
            this.img33.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img33.Properties.Appearance.Options.UseBackColor = true;
            this.img33.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img33.Properties.NullText = " ";
            this.img33.Properties.ReadOnly = true;
            this.img33.Properties.ShowMenu = false;
            this.img33.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchHorizontal;
            this.img33.Size = new System.Drawing.Size(90, 80);
            this.img33.TabIndex = 8;
            // 
            // img23
            // 
            this.img23.Location = new System.Drawing.Point(533, 204);
            this.img23.Name = "img23";
            this.img23.Properties.AllowFocused = false;
            this.img23.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img23.Properties.Appearance.Options.UseBackColor = true;
            this.img23.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img23.Properties.NullText = " ";
            this.img23.Properties.ReadOnly = true;
            this.img23.Properties.ShowMenu = false;
            this.img23.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchHorizontal;
            this.img23.Size = new System.Drawing.Size(90, 80);
            this.img23.TabIndex = 8;
            // 
            // img29
            // 
            this.img29.Location = new System.Drawing.Point(533, 294);
            this.img29.Name = "img29";
            this.img29.Properties.AllowFocused = false;
            this.img29.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img29.Properties.Appearance.Options.UseBackColor = true;
            this.img29.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img29.Properties.NullText = " ";
            this.img29.Properties.ReadOnly = true;
            this.img29.Properties.ShowMenu = false;
            this.img29.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.img29.Size = new System.Drawing.Size(90, 76);
            this.img29.TabIndex = 8;
            // 
            // img19
            // 
            this.img19.Location = new System.Drawing.Point(533, 118);
            this.img19.Name = "img19";
            this.img19.Properties.AllowFocused = false;
            this.img19.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img19.Properties.Appearance.Options.UseBackColor = true;
            this.img19.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img19.Properties.NullText = " ";
            this.img19.Properties.ReadOnly = true;
            this.img19.Properties.ShowMenu = false;
            this.img19.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.img19.Size = new System.Drawing.Size(90, 76);
            this.img19.TabIndex = 8;
            // 
            // img13
            // 
            this.img13.Location = new System.Drawing.Point(533, 28);
            this.img13.Name = "img13";
            this.img13.Properties.AllowFocused = false;
            this.img13.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img13.Properties.Appearance.Options.UseBackColor = true;
            this.img13.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img13.Properties.NullText = " ";
            this.img13.Properties.ReadOnly = true;
            this.img13.Properties.ShowMenu = false;
            this.img13.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchHorizontal;
            this.img13.Size = new System.Drawing.Size(90, 80);
            this.img13.TabIndex = 8;
            // 
            // img32
            // 
            this.img32.Location = new System.Drawing.Point(333, 380);
            this.img32.Name = "img32";
            this.img32.Properties.AllowFocused = false;
            this.img32.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img32.Properties.Appearance.Options.UseBackColor = true;
            this.img32.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img32.Properties.NullText = " ";
            this.img32.Properties.ReadOnly = true;
            this.img32.Properties.ShowMenu = false;
            this.img32.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchHorizontal;
            this.img32.Size = new System.Drawing.Size(90, 80);
            this.img32.TabIndex = 8;
            // 
            // img31
            // 
            this.img31.Location = new System.Drawing.Point(133, 380);
            this.img31.Name = "img31";
            this.img31.Properties.AllowFocused = false;
            this.img31.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img31.Properties.Appearance.Options.UseBackColor = true;
            this.img31.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img31.Properties.NullText = " ";
            this.img31.Properties.ReadOnly = true;
            this.img31.Properties.ShowMenu = false;
            this.img31.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchHorizontal;
            this.img31.Size = new System.Drawing.Size(90, 80);
            this.img31.TabIndex = 9;
            // 
            // img28
            // 
            this.img28.Location = new System.Drawing.Point(433, 294);
            this.img28.Name = "img28";
            this.img28.Properties.AllowFocused = false;
            this.img28.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img28.Properties.Appearance.Options.UseBackColor = true;
            this.img28.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img28.Properties.NullText = " ";
            this.img28.Properties.ReadOnly = true;
            this.img28.Properties.ShowMenu = false;
            this.img28.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchVertical;
            this.img28.Size = new System.Drawing.Size(90, 76);
            this.img28.TabIndex = 8;
            // 
            // img27
            // 
            this.img27.Location = new System.Drawing.Point(333, 294);
            this.img27.Name = "img27";
            this.img27.Properties.AllowFocused = false;
            this.img27.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img27.Properties.Appearance.Options.UseBackColor = true;
            this.img27.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img27.Properties.NullText = " ";
            this.img27.Properties.ReadOnly = true;
            this.img27.Properties.ShowMenu = false;
            this.img27.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.img27.Size = new System.Drawing.Size(90, 76);
            this.img27.TabIndex = 12;
            // 
            // img26
            // 
            this.img26.Location = new System.Drawing.Point(233, 294);
            this.img26.Name = "img26";
            this.img26.Properties.AllowFocused = false;
            this.img26.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img26.Properties.Appearance.Options.UseBackColor = true;
            this.img26.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img26.Properties.NullText = " ";
            this.img26.Properties.ReadOnly = true;
            this.img26.Properties.ShowMenu = false;
            this.img26.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchVertical;
            this.img26.Size = new System.Drawing.Size(90, 76);
            this.img26.TabIndex = 12;
            // 
            // img25
            // 
            this.img25.Location = new System.Drawing.Point(133, 294);
            this.img25.Name = "img25";
            this.img25.Properties.AllowFocused = false;
            this.img25.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img25.Properties.Appearance.Options.UseBackColor = true;
            this.img25.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img25.Properties.NullText = " ";
            this.img25.Properties.ReadOnly = true;
            this.img25.Properties.ShowMenu = false;
            this.img25.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.img25.Size = new System.Drawing.Size(90, 76);
            this.img25.TabIndex = 12;
            // 
            // img24
            // 
            this.img24.Location = new System.Drawing.Point(33, 294);
            this.img24.Name = "img24";
            this.img24.Properties.AllowFocused = false;
            this.img24.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img24.Properties.Appearance.Options.UseBackColor = true;
            this.img24.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img24.Properties.NullText = " ";
            this.img24.Properties.ReadOnly = true;
            this.img24.Properties.ShowMenu = false;
            this.img24.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchVertical;
            this.img24.Size = new System.Drawing.Size(90, 76);
            this.img24.TabIndex = 11;
            // 
            // img22
            // 
            this.img22.Location = new System.Drawing.Point(333, 204);
            this.img22.Name = "img22";
            this.img22.Properties.AllowFocused = false;
            this.img22.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img22.Properties.Appearance.Options.UseBackColor = true;
            this.img22.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img22.Properties.NullText = " ";
            this.img22.Properties.ReadOnly = true;
            this.img22.Properties.ShowMenu = false;
            this.img22.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchHorizontal;
            this.img22.Size = new System.Drawing.Size(90, 80);
            this.img22.TabIndex = 8;
            // 
            // img21
            // 
            this.img21.Location = new System.Drawing.Point(133, 204);
            this.img21.Name = "img21";
            this.img21.Properties.AllowFocused = false;
            this.img21.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img21.Properties.Appearance.Options.UseBackColor = true;
            this.img21.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img21.Properties.NullText = " ";
            this.img21.Properties.ReadOnly = true;
            this.img21.Properties.ShowMenu = false;
            this.img21.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchHorizontal;
            this.img21.Size = new System.Drawing.Size(90, 80);
            this.img21.TabIndex = 9;
            // 
            // img18
            // 
            this.img18.Location = new System.Drawing.Point(433, 118);
            this.img18.Name = "img18";
            this.img18.Properties.AllowFocused = false;
            this.img18.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img18.Properties.Appearance.Options.UseBackColor = true;
            this.img18.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img18.Properties.NullText = " ";
            this.img18.Properties.ReadOnly = true;
            this.img18.Properties.ShowMenu = false;
            this.img18.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchVertical;
            this.img18.Size = new System.Drawing.Size(90, 76);
            this.img18.TabIndex = 11;
            // 
            // img17
            // 
            this.img17.Location = new System.Drawing.Point(333, 118);
            this.img17.Name = "img17";
            this.img17.Properties.AllowFocused = false;
            this.img17.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img17.Properties.Appearance.Options.UseBackColor = true;
            this.img17.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img17.Properties.NullText = " ";
            this.img17.Properties.ReadOnly = true;
            this.img17.Properties.ShowMenu = false;
            this.img17.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.img17.Size = new System.Drawing.Size(90, 76);
            this.img17.TabIndex = 11;
            // 
            // img16
            // 
            this.img16.Location = new System.Drawing.Point(233, 118);
            this.img16.Name = "img16";
            this.img16.Properties.AllowFocused = false;
            this.img16.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img16.Properties.Appearance.Options.UseBackColor = true;
            this.img16.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img16.Properties.NullText = " ";
            this.img16.Properties.ReadOnly = true;
            this.img16.Properties.ShowMenu = false;
            this.img16.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchVertical;
            this.img16.Size = new System.Drawing.Size(90, 76);
            this.img16.TabIndex = 10;
            // 
            // img15
            // 
            this.img15.Location = new System.Drawing.Point(133, 118);
            this.img15.Name = "img15";
            this.img15.Properties.AllowFocused = false;
            this.img15.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img15.Properties.Appearance.Options.UseBackColor = true;
            this.img15.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img15.Properties.NullText = " ";
            this.img15.Properties.ReadOnly = true;
            this.img15.Properties.ShowMenu = false;
            this.img15.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.img15.Size = new System.Drawing.Size(90, 76);
            this.img15.TabIndex = 11;
            // 
            // img12
            // 
            this.img12.Location = new System.Drawing.Point(333, 28);
            this.img12.Name = "img12";
            this.img12.Properties.AllowFocused = false;
            this.img12.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img12.Properties.Appearance.Options.UseBackColor = true;
            this.img12.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img12.Properties.NullText = " ";
            this.img12.Properties.ReadOnly = true;
            this.img12.Properties.ShowMenu = false;
            this.img12.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchHorizontal;
            this.img12.Size = new System.Drawing.Size(90, 80);
            this.img12.TabIndex = 8;
            // 
            // img11
            // 
            this.img11.Location = new System.Drawing.Point(133, 28);
            this.img11.Name = "img11";
            this.img11.Properties.AllowFocused = false;
            this.img11.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img11.Properties.Appearance.Options.UseBackColor = true;
            this.img11.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img11.Properties.NullText = " ";
            this.img11.Properties.ReadOnly = true;
            this.img11.Properties.ShowMenu = false;
            this.img11.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchHorizontal;
            this.img11.Size = new System.Drawing.Size(90, 80);
            this.img11.TabIndex = 9;
            // 
            // img14
            // 
            this.img14.Location = new System.Drawing.Point(33, 118);
            this.img14.Name = "img14";
            this.img14.Properties.AllowFocused = false;
            this.img14.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img14.Properties.Appearance.Options.UseBackColor = true;
            this.img14.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img14.Properties.NullText = " ";
            this.img14.Properties.ReadOnly = true;
            this.img14.Properties.ShowMenu = false;
            this.img14.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchVertical;
            this.img14.Size = new System.Drawing.Size(90, 76);
            this.img14.TabIndex = 8;
            // 
            // lgrNV
            // 
            this.lgrNV.AppearanceGroup.BackColor = System.Drawing.Color.Transparent;
            this.lgrNV.AppearanceGroup.Options.UseBackColor = true;
            this.lgrNV.AppearanceGroup.Options.UseTextOptions = true;
            this.lgrNV.AppearanceGroup.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lgrNV.BackgroundImage = global::CDT.Properties.Resources.Hinh_nen_5;
            this.lgrNV.BackgroundImageVisible = true;
            this.lgrNV.CustomizationFormText = "layoutControlGroup1";
            this.lgrNV.GroupBordersVisible = false;
            this.lgrNV.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgDM,
            this.layoutControlGroup2});
            this.lgrNV.Location = new System.Drawing.Point(0, 0);
            this.lgrNV.Name = "lgrNV";
            this.lgrNV.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lgrNV.Size = new System.Drawing.Size(950, 600);
            this.lgrNV.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lgrNV.Text = "Quy trình nghiệp vụ";
            // 
            // lcgDM
            // 
            this.lcgDM.AppearanceGroup.BackColor = System.Drawing.Color.Transparent;
            this.lcgDM.AppearanceGroup.Options.UseBackColor = true;
            this.lcgDM.CustomizationFormText = "Thiết lập danh mục";
            this.lcgDM.GroupBordersVisible = false;
            this.lcgDM.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciNV17,
            this.lciNV19,
            this.lciNV21,
            this.lciNV23,
            this.lciNV18,
            this.lciNV20,
            this.lciNV22,
            this.emptySpaceItem14,
            this.emptySpaceItem3,
            this.emptySpaceItem13,
            this.emptySpaceItem4,
            this.lciNV24,
            this.lciNV25,
            this.emptySpaceItem2,
            this.emptySpaceItem5,
            this.emptySpaceItem6,
            this.emptySpaceItem7,
            this.emptySpaceItem8,
            this.emptySpaceItem9,
            this.emptySpaceItem10,
            this.emptySpaceItem15});
            this.lcgDM.Location = new System.Drawing.Point(0, 475);
            this.lcgDM.Name = "lcgDM";
            this.lcgDM.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgDM.Size = new System.Drawing.Size(950, 125);
            this.lcgDM.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.lcgDM.Text = "Thiết lập danh mục";
            this.lcgDM.TextVisible = false;
            // 
            // emptySpaceItem14
            // 
            this.emptySpaceItem14.CustomizationFormText = "emptySpaceItem14";
            this.emptySpaceItem14.Location = new System.Drawing.Point(0, 25);
            this.emptySpaceItem14.Name = "emptySpaceItem14";
            this.emptySpaceItem14.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem14.Size = new System.Drawing.Size(28, 100);
            this.emptySpaceItem14.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem14.Text = "emptySpaceItem14";
            this.emptySpaceItem14.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(28, 105);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem3.Size = new System.Drawing.Size(900, 20);
            this.emptySpaceItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem13
            // 
            this.emptySpaceItem13.CustomizationFormText = "emptySpaceItem13";
            this.emptySpaceItem13.Location = new System.Drawing.Point(928, 25);
            this.emptySpaceItem13.Name = "emptySpaceItem13";
            this.emptySpaceItem13.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem13.Size = new System.Drawing.Size(22, 100);
            this.emptySpaceItem13.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem13.Text = "emptySpaceItem13";
            this.emptySpaceItem13.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem4.Size = new System.Drawing.Size(950, 25);
            this.emptySpaceItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.CustomizationFormText = "Quy trình nghiệp vụ";
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciNV14,
            this.layoutControlItem54,
            this.lciNV13,
            this.layoutControlItem56,
            this.lciNV15,
            this.layoutControlItem8,
            this.lciNV16,
            this.layoutControlItem17,
            this.layoutControlItem5,
            this.layoutControlItem52,
            this.layoutControlItem51,
            this.layoutControlItem50,
            this.layoutControlItem49,
            this.layoutControlItem48,
            this.lciNV9,
            this.layoutControlItem44,
            this.lciNV10,
            this.layoutControlItem46,
            this.lciNV11,
            this.layoutControlItem6,
            this.lciNV12,
            this.layoutControlItem16,
            this.layoutControlItem4,
            this.layoutControlItem42,
            this.layoutControlItem41,
            this.layoutControlItem40,
            this.layoutControlItem39,
            this.layoutControlItem38,
            this.lciNV5,
            this.layoutControlItem34,
            this.lciNV6,
            this.layoutControlItem36,
            this.lciNV7,
            this.layoutControlItem2,
            this.lciNV8,
            this.emptySpaceItem11,
            this.emptySpaceItem12,
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem7,
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.lciNV1,
            this.lciNV2,
            this.lciNV3,
            this.layoutControlItem14,
            this.layoutControlItem15,
            this.emptySpaceItem16});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(950, 475);
            this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup2.Text = "Quy trình nghiệp vụ";
            this.layoutControlGroup2.TextVisible = false;
            // 
            // emptySpaceItem11
            // 
            this.emptySpaceItem11.CustomizationFormText = "emptySpaceItem11";
            this.emptySpaceItem11.Location = new System.Drawing.Point(0, 23);
            this.emptySpaceItem11.Name = "emptySpaceItem11";
            this.emptySpaceItem11.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem11.Size = new System.Drawing.Size(28, 452);
            this.emptySpaceItem11.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem11.Text = "emptySpaceItem11";
            this.emptySpaceItem11.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem12
            // 
            this.emptySpaceItem12.CustomizationFormText = "emptySpaceItem12";
            this.emptySpaceItem12.Location = new System.Drawing.Point(928, 23);
            this.emptySpaceItem12.Name = "emptySpaceItem12";
            this.emptySpaceItem12.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem12.Size = new System.Drawing.Size(22, 452);
            this.emptySpaceItem12.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem12.Text = "emptySpaceItem12";
            this.emptySpaceItem12.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem1.Size = new System.Drawing.Size(950, 23);
            this.emptySpaceItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // img34
            // 
            this.img34.Location = new System.Drawing.Point(733, 28);
            this.img34.Name = "img34";
            this.img34.Properties.AllowFocused = false;
            this.img34.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img34.Properties.Appearance.Options.UseBackColor = true;
            this.img34.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img34.Properties.NullText = " ";
            this.img34.Properties.ReadOnly = true;
            this.img34.Properties.ShowMenu = false;
            this.img34.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchHorizontal;
            this.img34.Size = new System.Drawing.Size(90, 80);
            this.img34.TabIndex = 9;
            // 
            // img35
            // 
            this.img35.Location = new System.Drawing.Point(733, 118);
            this.img35.Name = "img35";
            this.img35.Properties.AllowFocused = false;
            this.img35.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img35.Properties.Appearance.Options.UseBackColor = true;
            this.img35.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img35.Properties.NullText = " ";
            this.img35.Properties.ReadOnly = true;
            this.img35.Properties.ShowMenu = false;
            this.img35.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchHorizontal;
            this.img35.Size = new System.Drawing.Size(90, 76);
            this.img35.TabIndex = 9;
            // 
            // img36
            // 
            this.img36.Location = new System.Drawing.Point(733, 204);
            this.img36.Name = "img36";
            this.img36.Properties.AllowFocused = false;
            this.img36.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img36.Properties.Appearance.Options.UseBackColor = true;
            this.img36.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img36.Properties.NullText = " ";
            this.img36.Properties.ReadOnly = true;
            this.img36.Properties.ShowMenu = false;
            this.img36.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchHorizontal;
            this.img36.Size = new System.Drawing.Size(90, 80);
            this.img36.TabIndex = 9;
            // 
            // img37
            // 
            this.img37.Location = new System.Drawing.Point(733, 294);
            this.img37.Name = "img37";
            this.img37.Properties.AllowFocused = false;
            this.img37.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img37.Properties.Appearance.Options.UseBackColor = true;
            this.img37.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img37.Properties.NullText = " ";
            this.img37.Properties.ReadOnly = true;
            this.img37.Properties.ShowMenu = false;
            this.img37.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchHorizontal;
            this.img37.Size = new System.Drawing.Size(90, 76);
            this.img37.TabIndex = 9;
            // 
            // img38
            // 
            this.img38.Location = new System.Drawing.Point(733, 380);
            this.img38.Name = "img38";
            this.img38.Properties.AllowFocused = false;
            this.img38.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img38.Properties.Appearance.Options.UseBackColor = true;
            this.img38.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img38.Properties.NullText = " ";
            this.img38.Properties.ReadOnly = true;
            this.img38.Properties.ShowMenu = false;
            this.img38.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchHorizontal;
            this.img38.Size = new System.Drawing.Size(90, 80);
            this.img38.TabIndex = 9;
            // 
            // img39
            // 
            this.img39.Location = new System.Drawing.Point(833, 118);
            this.img39.Name = "img39";
            this.img39.Properties.AllowFocused = false;
            this.img39.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img39.Properties.Appearance.Options.UseBackColor = true;
            this.img39.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img39.Properties.NullText = " ";
            this.img39.Properties.ReadOnly = true;
            this.img39.Properties.ShowMenu = false;
            this.img39.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchVertical;
            this.img39.Size = new System.Drawing.Size(90, 76);
            this.img39.TabIndex = 10;
            // 
            // img40
            // 
            this.img40.Location = new System.Drawing.Point(833, 294);
            this.img40.Name = "img40";
            this.img40.Properties.AllowFocused = false;
            this.img40.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.img40.Properties.Appearance.Options.UseBackColor = true;
            this.img40.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.img40.Properties.NullText = " ";
            this.img40.Properties.ReadOnly = true;
            this.img40.Properties.ShowMenu = false;
            this.img40.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.StretchVertical;
            this.img40.Size = new System.Drawing.Size(90, 76);
            this.img40.TabIndex = 10;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(123, 25);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem2.Size = new System.Drawing.Size(10, 80);
            this.emptySpaceItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.CustomizationFormText = "emptySpaceItem5";
            this.emptySpaceItem5.Location = new System.Drawing.Point(223, 25);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem5.Size = new System.Drawing.Size(10, 80);
            this.emptySpaceItem5.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem5.Text = "emptySpaceItem5";
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.CustomizationFormText = "emptySpaceItem6";
            this.emptySpaceItem6.Location = new System.Drawing.Point(323, 25);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem6.Size = new System.Drawing.Size(10, 80);
            this.emptySpaceItem6.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem6.Text = "emptySpaceItem6";
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem7
            // 
            this.emptySpaceItem7.CustomizationFormText = "emptySpaceItem7";
            this.emptySpaceItem7.Location = new System.Drawing.Point(423, 25);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem7.Size = new System.Drawing.Size(10, 80);
            this.emptySpaceItem7.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem7.Text = "emptySpaceItem7";
            this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem8
            // 
            this.emptySpaceItem8.CustomizationFormText = "emptySpaceItem8";
            this.emptySpaceItem8.Location = new System.Drawing.Point(523, 25);
            this.emptySpaceItem8.Name = "emptySpaceItem8";
            this.emptySpaceItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem8.Size = new System.Drawing.Size(10, 80);
            this.emptySpaceItem8.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem8.Text = "emptySpaceItem8";
            this.emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem9
            // 
            this.emptySpaceItem9.CustomizationFormText = "emptySpaceItem9";
            this.emptySpaceItem9.Location = new System.Drawing.Point(623, 25);
            this.emptySpaceItem9.Name = "emptySpaceItem9";
            this.emptySpaceItem9.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem9.Size = new System.Drawing.Size(10, 80);
            this.emptySpaceItem9.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem9.Text = "emptySpaceItem9";
            this.emptySpaceItem9.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem10
            // 
            this.emptySpaceItem10.CustomizationFormText = "emptySpaceItem10";
            this.emptySpaceItem10.Location = new System.Drawing.Point(723, 25);
            this.emptySpaceItem10.Name = "emptySpaceItem10";
            this.emptySpaceItem10.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem10.Size = new System.Drawing.Size(10, 80);
            this.emptySpaceItem10.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem10.Text = "emptySpaceItem10";
            this.emptySpaceItem10.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem15
            // 
            this.emptySpaceItem15.CustomizationFormText = "emptySpaceItem15";
            this.emptySpaceItem15.Location = new System.Drawing.Point(823, 25);
            this.emptySpaceItem15.Name = "emptySpaceItem15";
            this.emptySpaceItem15.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem15.Size = new System.Drawing.Size(10, 80);
            this.emptySpaceItem15.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem15.Text = "emptySpaceItem15";
            this.emptySpaceItem15.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem16
            // 
            this.emptySpaceItem16.CustomizationFormText = "emptySpaceItem16";
            this.emptySpaceItem16.Location = new System.Drawing.Point(28, 465);
            this.emptySpaceItem16.Name = "emptySpaceItem16";
            this.emptySpaceItem16.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem16.Size = new System.Drawing.Size(900, 10);
            this.emptySpaceItem16.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem16.Text = "emptySpaceItem16";
            this.emptySpaceItem16.TextSize = new System.Drawing.Size(0, 0);
            // 
            // btn25
            // 
            this.btn25.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn25.Appearance.Options.UseForeColor = true;
            this.btn25.Appearance.Options.UseTextOptions = true;
            this.btn25.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn25.Location = new System.Drawing.Point(838, 505);
            this.btn25.LookAndFeel.SkinName = "Blue";
            this.btn25.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn25.Name = "btn25";
            this.btn25.Size = new System.Drawing.Size(85, 70);
            this.btn25.TabIndex = 15;
            this.btn25.Text = "NVxx";
            // 
            // btn24
            // 
            this.btn24.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn24.Appearance.Options.UseForeColor = true;
            this.btn24.Appearance.Options.UseTextOptions = true;
            this.btn24.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn24.Location = new System.Drawing.Point(738, 505);
            this.btn24.LookAndFeel.SkinName = "Blue";
            this.btn24.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn24.Name = "btn24";
            this.btn24.Size = new System.Drawing.Size(80, 70);
            this.btn24.TabIndex = 15;
            this.btn24.Text = "NVxx";
            // 
            // btn3
            // 
            this.btn3.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn3.Appearance.Options.UseForeColor = true;
            this.btn3.Appearance.Options.UseTextOptions = true;
            this.btn3.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn3.Location = new System.Drawing.Point(833, 380);
            this.btn3.LookAndFeel.SkinName = "Blue";
            this.btn3.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(90, 80);
            this.btn3.TabIndex = 8;
            this.btn3.Text = "NVxx";
            // 
            // btn2
            // 
            this.btn2.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn2.Appearance.Options.UseForeColor = true;
            this.btn2.Appearance.Options.UseTextOptions = true;
            this.btn2.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn2.Location = new System.Drawing.Point(833, 204);
            this.btn2.LookAndFeel.SkinName = "Blue";
            this.btn2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(90, 80);
            this.btn2.TabIndex = 8;
            this.btn2.Text = "NVxx";
            // 
            // btn1
            // 
            this.btn1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn1.Appearance.Options.UseForeColor = true;
            this.btn1.Appearance.Options.UseTextOptions = true;
            this.btn1.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn1.Location = new System.Drawing.Point(833, 28);
            this.btn1.LookAndFeel.SkinName = "Blue";
            this.btn1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(90, 80);
            this.btn1.TabIndex = 8;
            this.btn1.Text = "NVxx";
            // 
            // btn22
            // 
            this.btn22.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn22.Appearance.Options.UseForeColor = true;
            this.btn22.Appearance.Options.UseTextOptions = true;
            this.btn22.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn22.Location = new System.Drawing.Point(538, 505);
            this.btn22.LookAndFeel.SkinName = "Blue";
            this.btn22.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn22.Name = "btn22";
            this.btn22.Size = new System.Drawing.Size(80, 70);
            this.btn22.TabIndex = 14;
            this.btn22.Text = "NVxx";
            // 
            // btn20
            // 
            this.btn20.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn20.Appearance.Options.UseForeColor = true;
            this.btn20.Appearance.Options.UseTextOptions = true;
            this.btn20.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn20.Location = new System.Drawing.Point(338, 505);
            this.btn20.LookAndFeel.SkinName = "Blue";
            this.btn20.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn20.Name = "btn20";
            this.btn20.Size = new System.Drawing.Size(80, 70);
            this.btn20.TabIndex = 14;
            this.btn20.Text = "NVxx";
            // 
            // btn18
            // 
            this.btn18.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn18.Appearance.Options.UseForeColor = true;
            this.btn18.Appearance.Options.UseTextOptions = true;
            this.btn18.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn18.Location = new System.Drawing.Point(138, 505);
            this.btn18.LookAndFeel.SkinName = "Blue";
            this.btn18.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn18.Name = "btn18";
            this.btn18.Size = new System.Drawing.Size(80, 70);
            this.btn18.TabIndex = 14;
            this.btn18.Text = "NVxx";
            // 
            // btn23
            // 
            this.btn23.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn23.Appearance.Options.UseForeColor = true;
            this.btn23.Appearance.Options.UseTextOptions = true;
            this.btn23.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn23.Location = new System.Drawing.Point(638, 505);
            this.btn23.LookAndFeel.SkinName = "Blue";
            this.btn23.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn23.Name = "btn23";
            this.btn23.Size = new System.Drawing.Size(80, 70);
            this.btn23.TabIndex = 7;
            this.btn23.Text = "NVxx";
            // 
            // btn16
            // 
            this.btn16.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn16.Appearance.Options.UseForeColor = true;
            this.btn16.Appearance.Options.UseTextOptions = true;
            this.btn16.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn16.Location = new System.Drawing.Point(633, 380);
            this.btn16.LookAndFeel.SkinName = "Blue";
            this.btn16.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn16.Name = "btn16";
            this.btn16.Size = new System.Drawing.Size(90, 80);
            this.btn16.TabIndex = 7;
            this.btn16.Text = "NVxx";
            // 
            // btn12
            // 
            this.btn12.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn12.Appearance.Options.UseForeColor = true;
            this.btn12.Appearance.Options.UseTextOptions = true;
            this.btn12.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn12.Location = new System.Drawing.Point(633, 204);
            this.btn12.LookAndFeel.SkinName = "Blue";
            this.btn12.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn12.Name = "btn12";
            this.btn12.Size = new System.Drawing.Size(90, 80);
            this.btn12.TabIndex = 7;
            this.btn12.Text = "NVxx";
            // 
            // btn8
            // 
            this.btn8.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn8.Appearance.Options.UseForeColor = true;
            this.btn8.Appearance.Options.UseTextOptions = true;
            this.btn8.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn8.Location = new System.Drawing.Point(633, 28);
            this.btn8.LookAndFeel.SkinName = "Blue";
            this.btn8.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(90, 80);
            this.btn8.TabIndex = 7;
            this.btn8.Text = "NVxx";
            // 
            // btn21
            // 
            this.btn21.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn21.Appearance.Options.UseForeColor = true;
            this.btn21.Appearance.Options.UseTextOptions = true;
            this.btn21.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn21.Location = new System.Drawing.Point(438, 505);
            this.btn21.LookAndFeel.SkinName = "Blue";
            this.btn21.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn21.Name = "btn21";
            this.btn21.Size = new System.Drawing.Size(80, 70);
            this.btn21.TabIndex = 8;
            this.btn21.Text = "NVxx";
            // 
            // btn19
            // 
            this.btn19.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn19.Appearance.Options.UseForeColor = true;
            this.btn19.Appearance.Options.UseTextOptions = true;
            this.btn19.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn19.Location = new System.Drawing.Point(238, 505);
            this.btn19.LookAndFeel.SkinName = "Blue";
            this.btn19.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn19.Name = "btn19";
            this.btn19.Size = new System.Drawing.Size(80, 70);
            this.btn19.TabIndex = 7;
            this.btn19.Text = "NVxx";
            // 
            // btn17
            // 
            this.btn17.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn17.Appearance.Options.UseForeColor = true;
            this.btn17.Appearance.Options.UseTextOptions = true;
            this.btn17.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn17.Location = new System.Drawing.Point(33, 505);
            this.btn17.LookAndFeel.SkinName = "Blue";
            this.btn17.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn17.Name = "btn17";
            this.btn17.Size = new System.Drawing.Size(85, 70);
            this.btn17.TabIndex = 13;
            this.btn17.Text = "NVxx";
            // 
            // btn15
            // 
            this.btn15.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn15.Appearance.Options.UseForeColor = true;
            this.btn15.Appearance.Options.UseTextOptions = true;
            this.btn15.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn15.Location = new System.Drawing.Point(433, 380);
            this.btn15.LookAndFeel.SkinName = "Blue";
            this.btn15.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn15.Name = "btn15";
            this.btn15.Size = new System.Drawing.Size(90, 80);
            this.btn15.TabIndex = 7;
            this.btn15.Text = "NVxx";
            // 
            // btn14
            // 
            this.btn14.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn14.Appearance.Options.UseForeColor = true;
            this.btn14.Appearance.Options.UseTextOptions = true;
            this.btn14.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn14.Location = new System.Drawing.Point(233, 380);
            this.btn14.LookAndFeel.SkinName = "Blue";
            this.btn14.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn14.Name = "btn14";
            this.btn14.Size = new System.Drawing.Size(90, 80);
            this.btn14.TabIndex = 6;
            this.btn14.Text = "NVxx";
            // 
            // btn13
            // 
            this.btn13.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn13.Appearance.Options.UseForeColor = true;
            this.btn13.Appearance.Options.UseTextOptions = true;
            this.btn13.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn13.Location = new System.Drawing.Point(33, 380);
            this.btn13.LookAndFeel.SkinName = "Blue";
            this.btn13.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn13.Name = "btn13";
            this.btn13.Size = new System.Drawing.Size(90, 80);
            this.btn13.TabIndex = 12;
            this.btn13.Text = "NVxx";
            // 
            // btn11
            // 
            this.btn11.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn11.Appearance.Options.UseForeColor = true;
            this.btn11.Appearance.Options.UseTextOptions = true;
            this.btn11.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn11.Location = new System.Drawing.Point(433, 204);
            this.btn11.LookAndFeel.SkinName = "Blue";
            this.btn11.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn11.Name = "btn11";
            this.btn11.Size = new System.Drawing.Size(90, 80);
            this.btn11.TabIndex = 11;
            this.btn11.Text = "NVxx";
            // 
            // btn10
            // 
            this.btn10.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn10.Appearance.Options.UseForeColor = true;
            this.btn10.Appearance.Options.UseTextOptions = true;
            this.btn10.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn10.Location = new System.Drawing.Point(233, 204);
            this.btn10.LookAndFeel.SkinName = "Blue";
            this.btn10.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn10.Name = "btn10";
            this.btn10.Size = new System.Drawing.Size(90, 80);
            this.btn10.TabIndex = 10;
            this.btn10.Text = "NVxx";
            // 
            // btn9
            // 
            this.btn9.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn9.Appearance.Options.UseForeColor = true;
            this.btn9.Appearance.Options.UseTextOptions = true;
            this.btn9.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn9.Location = new System.Drawing.Point(33, 204);
            this.btn9.LookAndFeel.SkinName = "Blue";
            this.btn9.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(90, 80);
            this.btn9.TabIndex = 11;
            this.btn9.Text = "NVxx";
            // 
            // btn7
            // 
            this.btn7.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn7.Appearance.Options.UseForeColor = true;
            this.btn7.Appearance.Options.UseTextOptions = true;
            this.btn7.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn7.Location = new System.Drawing.Point(433, 28);
            this.btn7.LookAndFeel.SkinName = "Blue";
            this.btn7.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(90, 80);
            this.btn7.TabIndex = 6;
            this.btn7.Text = "NVxx";
            // 
            // btn6
            // 
            this.btn6.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn6.Appearance.Options.UseForeColor = true;
            this.btn6.Appearance.Options.UseTextOptions = true;
            this.btn6.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn6.Location = new System.Drawing.Point(233, 28);
            this.btn6.LookAndFeel.SkinName = "Blue";
            this.btn6.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(90, 80);
            this.btn6.TabIndex = 9;
            this.btn6.Text = "NVxx";
            // 
            // btn5
            // 
            this.btn5.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btn5.Appearance.Options.UseForeColor = true;
            this.btn5.Appearance.Options.UseTextOptions = true;
            this.btn5.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.btn5.Location = new System.Drawing.Point(33, 28);
            this.btn5.LookAndFeel.SkinName = "Blue";
            this.btn5.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(90, 80);
            this.btn5.TabIndex = 10;
            this.btn5.Text = "NVxx";
            // 
            // lciNV17
            // 
            this.lciNV17.Control = this.btn17;
            this.lciNV17.CustomizationFormText = "layoutControlItem62";
            this.lciNV17.Location = new System.Drawing.Point(28, 25);
            this.lciNV17.MaxSize = new System.Drawing.Size(0, 91);
            this.lciNV17.MinSize = new System.Drawing.Size(49, 33);
            this.lciNV17.Name = "lciNV17";
            this.lciNV17.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV17.Size = new System.Drawing.Size(95, 80);
            this.lciNV17.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV17.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV17.Text = "lciNV17";
            this.lciNV17.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV17.TextToControlDistance = 0;
            this.lciNV17.TextVisible = false;
            // 
            // lciNV19
            // 
            this.lciNV19.Control = this.btn19;
            this.lciNV19.CustomizationFormText = "layoutControlItem65";
            this.lciNV19.Location = new System.Drawing.Point(233, 25);
            this.lciNV19.MaxSize = new System.Drawing.Size(0, 91);
            this.lciNV19.MinSize = new System.Drawing.Size(49, 33);
            this.lciNV19.Name = "lciNV19";
            this.lciNV19.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV19.Size = new System.Drawing.Size(90, 80);
            this.lciNV19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV19.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV19.Text = "lciNV19";
            this.lciNV19.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV19.TextToControlDistance = 0;
            this.lciNV19.TextVisible = false;
            // 
            // lciNV21
            // 
            this.lciNV21.Control = this.btn21;
            this.lciNV21.CustomizationFormText = "layoutControlItem67";
            this.lciNV21.Location = new System.Drawing.Point(433, 25);
            this.lciNV21.MaxSize = new System.Drawing.Size(0, 91);
            this.lciNV21.MinSize = new System.Drawing.Size(49, 33);
            this.lciNV21.Name = "lciNV21";
            this.lciNV21.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV21.Size = new System.Drawing.Size(90, 80);
            this.lciNV21.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV21.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV21.Text = "lciNV21";
            this.lciNV21.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV21.TextToControlDistance = 0;
            this.lciNV21.TextVisible = false;
            // 
            // lciNV23
            // 
            this.lciNV23.Control = this.btn23;
            this.lciNV23.CustomizationFormText = "lciNV20";
            this.lciNV23.Location = new System.Drawing.Point(633, 25);
            this.lciNV23.MaxSize = new System.Drawing.Size(0, 91);
            this.lciNV23.MinSize = new System.Drawing.Size(49, 33);
            this.lciNV23.Name = "lciNV23";
            this.lciNV23.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV23.Size = new System.Drawing.Size(90, 80);
            this.lciNV23.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV23.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV23.Text = "lciNV23";
            this.lciNV23.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV23.TextToControlDistance = 0;
            this.lciNV23.TextVisible = false;
            // 
            // lciNV18
            // 
            this.lciNV18.Control = this.btn18;
            this.lciNV18.CustomizationFormText = "lciNV18";
            this.lciNV18.Location = new System.Drawing.Point(133, 25);
            this.lciNV18.MaxSize = new System.Drawing.Size(0, 91);
            this.lciNV18.MinSize = new System.Drawing.Size(48, 32);
            this.lciNV18.Name = "lciNV18";
            this.lciNV18.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV18.Size = new System.Drawing.Size(90, 80);
            this.lciNV18.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV18.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV18.Text = "lciNV18";
            this.lciNV18.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV18.TextToControlDistance = 0;
            this.lciNV18.TextVisible = false;
            // 
            // lciNV20
            // 
            this.lciNV20.Control = this.btn20;
            this.lciNV20.CustomizationFormText = "lciNV20";
            this.lciNV20.Location = new System.Drawing.Point(333, 25);
            this.lciNV20.MaxSize = new System.Drawing.Size(0, 91);
            this.lciNV20.MinSize = new System.Drawing.Size(48, 32);
            this.lciNV20.Name = "lciNV20";
            this.lciNV20.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV20.Size = new System.Drawing.Size(90, 80);
            this.lciNV20.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV20.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV20.Text = "lciNV20";
            this.lciNV20.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV20.TextToControlDistance = 0;
            this.lciNV20.TextVisible = false;
            // 
            // lciNV22
            // 
            this.lciNV22.Control = this.btn22;
            this.lciNV22.CustomizationFormText = "lciNV22";
            this.lciNV22.Location = new System.Drawing.Point(533, 25);
            this.lciNV22.MaxSize = new System.Drawing.Size(0, 91);
            this.lciNV22.MinSize = new System.Drawing.Size(48, 32);
            this.lciNV22.Name = "lciNV22";
            this.lciNV22.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV22.Size = new System.Drawing.Size(90, 80);
            this.lciNV22.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV22.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV22.Text = "lciNV22";
            this.lciNV22.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV22.TextToControlDistance = 0;
            this.lciNV22.TextVisible = false;
            // 
            // lciNV24
            // 
            this.lciNV24.Control = this.btn24;
            this.lciNV24.CustomizationFormText = "lciNV24";
            this.lciNV24.Location = new System.Drawing.Point(733, 25);
            this.lciNV24.MaxSize = new System.Drawing.Size(0, 92);
            this.lciNV24.MinSize = new System.Drawing.Size(48, 32);
            this.lciNV24.Name = "lciNV24";
            this.lciNV24.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV24.Size = new System.Drawing.Size(90, 80);
            this.lciNV24.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV24.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV24.Text = "lciNV24";
            this.lciNV24.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV24.TextToControlDistance = 0;
            this.lciNV24.TextVisible = false;
            // 
            // lciNV25
            // 
            this.lciNV25.Control = this.btn25;
            this.lciNV25.CustomizationFormText = "lciNV25";
            this.lciNV25.Location = new System.Drawing.Point(833, 25);
            this.lciNV25.MaxSize = new System.Drawing.Size(0, 92);
            this.lciNV25.MinSize = new System.Drawing.Size(48, 32);
            this.lciNV25.Name = "lciNV25";
            this.lciNV25.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV25.Size = new System.Drawing.Size(95, 80);
            this.lciNV25.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV25.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV25.Text = "lciNV25";
            this.lciNV25.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV25.TextToControlDistance = 0;
            this.lciNV25.TextVisible = false;
            // 
            // lciNV14
            // 
            this.lciNV14.Control = this.btn14;
            this.lciNV14.CustomizationFormText = "layoutControlItem55";
            this.lciNV14.Location = new System.Drawing.Point(228, 375);
            this.lciNV14.MaxSize = new System.Drawing.Size(0, 92);
            this.lciNV14.MinSize = new System.Drawing.Size(49, 33);
            this.lciNV14.Name = "lciNV14";
            this.lciNV14.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV14.Size = new System.Drawing.Size(100, 90);
            this.lciNV14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV14.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV14.Text = "lciNV14";
            this.lciNV14.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV14.TextToControlDistance = 0;
            this.lciNV14.TextVisible = false;
            // 
            // layoutControlItem54
            // 
            this.layoutControlItem54.Control = this.img31;
            this.layoutControlItem54.CustomizationFormText = "layoutControlItem54";
            this.layoutControlItem54.Location = new System.Drawing.Point(128, 375);
            this.layoutControlItem54.MinSize = new System.Drawing.Size(31, 31);
            this.layoutControlItem54.Name = "layoutControlItem54";
            this.layoutControlItem54.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem54.Size = new System.Drawing.Size(100, 90);
            this.layoutControlItem54.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem54.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem54.Text = "layoutControlItem54";
            this.layoutControlItem54.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem54.TextToControlDistance = 0;
            this.layoutControlItem54.TextVisible = false;
            // 
            // lciNV13
            // 
            this.lciNV13.Control = this.btn13;
            this.lciNV13.CustomizationFormText = "layoutControlItem53";
            this.lciNV13.Location = new System.Drawing.Point(28, 375);
            this.lciNV13.MaxSize = new System.Drawing.Size(0, 95);
            this.lciNV13.MinSize = new System.Drawing.Size(49, 33);
            this.lciNV13.Name = "lciNV13";
            this.lciNV13.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV13.Size = new System.Drawing.Size(100, 90);
            this.lciNV13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV13.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV13.Text = "lciNV13";
            this.lciNV13.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV13.TextToControlDistance = 0;
            this.lciNV13.TextVisible = false;
            // 
            // layoutControlItem56
            // 
            this.layoutControlItem56.Control = this.img32;
            this.layoutControlItem56.CustomizationFormText = "layoutControlItem56";
            this.layoutControlItem56.Location = new System.Drawing.Point(328, 375);
            this.layoutControlItem56.MinSize = new System.Drawing.Size(31, 31);
            this.layoutControlItem56.Name = "layoutControlItem56";
            this.layoutControlItem56.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem56.Size = new System.Drawing.Size(100, 90);
            this.layoutControlItem56.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem56.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem56.Text = "layoutControlItem56";
            this.layoutControlItem56.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem56.TextToControlDistance = 0;
            this.layoutControlItem56.TextVisible = false;
            // 
            // lciNV15
            // 
            this.lciNV15.Control = this.btn15;
            this.lciNV15.CustomizationFormText = "layoutControlItem57";
            this.lciNV15.Location = new System.Drawing.Point(428, 375);
            this.lciNV15.MaxSize = new System.Drawing.Size(0, 92);
            this.lciNV15.MinSize = new System.Drawing.Size(49, 33);
            this.lciNV15.Name = "lciNV15";
            this.lciNV15.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV15.Size = new System.Drawing.Size(100, 90);
            this.lciNV15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV15.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV15.Text = "lciNV15";
            this.lciNV15.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV15.TextToControlDistance = 0;
            this.lciNV15.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.img33;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(528, 375);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem8.Size = new System.Drawing.Size(100, 90);
            this.layoutControlItem8.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // lciNV16
            // 
            this.lciNV16.Control = this.btn16;
            this.lciNV16.CustomizationFormText = "lciNV16";
            this.lciNV16.Location = new System.Drawing.Point(628, 375);
            this.lciNV16.MaxSize = new System.Drawing.Size(0, 92);
            this.lciNV16.MinSize = new System.Drawing.Size(49, 33);
            this.lciNV16.Name = "lciNV16";
            this.lciNV16.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV16.Size = new System.Drawing.Size(100, 90);
            this.lciNV16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV16.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV16.Text = "lciNV16";
            this.lciNV16.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV16.TextToControlDistance = 0;
            this.lciNV16.TextVisible = false;
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.img30;
            this.layoutControlItem17.CustomizationFormText = "layoutControlItem17";
            this.layoutControlItem17.Location = new System.Drawing.Point(628, 289);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem17.Size = new System.Drawing.Size(100, 86);
            this.layoutControlItem17.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem17.Text = "layoutControlItem17";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem17.TextToControlDistance = 0;
            this.layoutControlItem17.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.img29;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(528, 289);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem5.Size = new System.Drawing.Size(100, 86);
            this.layoutControlItem5.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem52
            // 
            this.layoutControlItem52.Control = this.img28;
            this.layoutControlItem52.CustomizationFormText = "layoutControlItem52";
            this.layoutControlItem52.Location = new System.Drawing.Point(428, 289);
            this.layoutControlItem52.MinSize = new System.Drawing.Size(31, 31);
            this.layoutControlItem52.Name = "layoutControlItem52";
            this.layoutControlItem52.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem52.Size = new System.Drawing.Size(100, 86);
            this.layoutControlItem52.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem52.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem52.Text = "layoutControlItem52";
            this.layoutControlItem52.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem52.TextToControlDistance = 0;
            this.layoutControlItem52.TextVisible = false;
            // 
            // layoutControlItem51
            // 
            this.layoutControlItem51.Control = this.img27;
            this.layoutControlItem51.CustomizationFormText = "layoutControlItem51";
            this.layoutControlItem51.Location = new System.Drawing.Point(328, 289);
            this.layoutControlItem51.MinSize = new System.Drawing.Size(31, 31);
            this.layoutControlItem51.Name = "layoutControlItem51";
            this.layoutControlItem51.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem51.Size = new System.Drawing.Size(100, 86);
            this.layoutControlItem51.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem51.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem51.Text = "layoutControlItem51";
            this.layoutControlItem51.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem51.TextToControlDistance = 0;
            this.layoutControlItem51.TextVisible = false;
            // 
            // layoutControlItem50
            // 
            this.layoutControlItem50.Control = this.img26;
            this.layoutControlItem50.CustomizationFormText = "layoutControlItem50";
            this.layoutControlItem50.Location = new System.Drawing.Point(228, 289);
            this.layoutControlItem50.MinSize = new System.Drawing.Size(31, 31);
            this.layoutControlItem50.Name = "layoutControlItem50";
            this.layoutControlItem50.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem50.Size = new System.Drawing.Size(100, 86);
            this.layoutControlItem50.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem50.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem50.Text = "layoutControlItem50";
            this.layoutControlItem50.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem50.TextToControlDistance = 0;
            this.layoutControlItem50.TextVisible = false;
            // 
            // layoutControlItem49
            // 
            this.layoutControlItem49.Control = this.img25;
            this.layoutControlItem49.CustomizationFormText = "layoutControlItem49";
            this.layoutControlItem49.Location = new System.Drawing.Point(128, 289);
            this.layoutControlItem49.MinSize = new System.Drawing.Size(31, 31);
            this.layoutControlItem49.Name = "layoutControlItem49";
            this.layoutControlItem49.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem49.Size = new System.Drawing.Size(100, 86);
            this.layoutControlItem49.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem49.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem49.Text = "layoutControlItem49";
            this.layoutControlItem49.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem49.TextToControlDistance = 0;
            this.layoutControlItem49.TextVisible = false;
            // 
            // layoutControlItem48
            // 
            this.layoutControlItem48.Control = this.img24;
            this.layoutControlItem48.CustomizationFormText = "layoutControlItem48";
            this.layoutControlItem48.Location = new System.Drawing.Point(28, 289);
            this.layoutControlItem48.MinSize = new System.Drawing.Size(31, 86);
            this.layoutControlItem48.Name = "layoutControlItem48";
            this.layoutControlItem48.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem48.Size = new System.Drawing.Size(100, 86);
            this.layoutControlItem48.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem48.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem48.Text = "layoutControlItem48";
            this.layoutControlItem48.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem48.TextToControlDistance = 0;
            this.layoutControlItem48.TextVisible = false;
            // 
            // lciNV9
            // 
            this.lciNV9.Control = this.btn9;
            this.lciNV9.CustomizationFormText = "layoutControlItem43";
            this.lciNV9.Location = new System.Drawing.Point(28, 199);
            this.lciNV9.MaxSize = new System.Drawing.Size(0, 95);
            this.lciNV9.MinSize = new System.Drawing.Size(49, 50);
            this.lciNV9.Name = "lciNV9";
            this.lciNV9.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV9.Size = new System.Drawing.Size(100, 90);
            this.lciNV9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV9.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV9.Text = "lciNV9";
            this.lciNV9.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV9.TextToControlDistance = 0;
            this.lciNV9.TextVisible = false;
            // 
            // layoutControlItem44
            // 
            this.layoutControlItem44.Control = this.img21;
            this.layoutControlItem44.CustomizationFormText = "layoutControlItem44";
            this.layoutControlItem44.Location = new System.Drawing.Point(128, 199);
            this.layoutControlItem44.MinSize = new System.Drawing.Size(31, 31);
            this.layoutControlItem44.Name = "layoutControlItem44";
            this.layoutControlItem44.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem44.Size = new System.Drawing.Size(100, 90);
            this.layoutControlItem44.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem44.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem44.Text = "layoutControlItem44";
            this.layoutControlItem44.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem44.TextToControlDistance = 0;
            this.layoutControlItem44.TextVisible = false;
            // 
            // lciNV10
            // 
            this.lciNV10.Control = this.btn10;
            this.lciNV10.CustomizationFormText = "layoutControlItem45";
            this.lciNV10.Location = new System.Drawing.Point(228, 199);
            this.lciNV10.MaxSize = new System.Drawing.Size(0, 92);
            this.lciNV10.MinSize = new System.Drawing.Size(49, 33);
            this.lciNV10.Name = "lciNV10";
            this.lciNV10.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV10.Size = new System.Drawing.Size(100, 90);
            this.lciNV10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV10.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV10.Text = "lciNV10";
            this.lciNV10.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV10.TextToControlDistance = 0;
            this.lciNV10.TextVisible = false;
            // 
            // layoutControlItem46
            // 
            this.layoutControlItem46.Control = this.img22;
            this.layoutControlItem46.CustomizationFormText = "layoutControlItem46";
            this.layoutControlItem46.Location = new System.Drawing.Point(328, 199);
            this.layoutControlItem46.MinSize = new System.Drawing.Size(31, 31);
            this.layoutControlItem46.Name = "layoutControlItem46";
            this.layoutControlItem46.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem46.Size = new System.Drawing.Size(100, 90);
            this.layoutControlItem46.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem46.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem46.Text = "layoutControlItem46";
            this.layoutControlItem46.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem46.TextToControlDistance = 0;
            this.layoutControlItem46.TextVisible = false;
            // 
            // lciNV11
            // 
            this.lciNV11.Control = this.btn11;
            this.lciNV11.CustomizationFormText = "layoutControlItem47";
            this.lciNV11.Location = new System.Drawing.Point(428, 199);
            this.lciNV11.MaxSize = new System.Drawing.Size(0, 92);
            this.lciNV11.MinSize = new System.Drawing.Size(49, 33);
            this.lciNV11.Name = "lciNV11";
            this.lciNV11.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV11.Size = new System.Drawing.Size(100, 90);
            this.lciNV11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV11.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV11.Text = "lciNV11";
            this.lciNV11.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV11.TextToControlDistance = 0;
            this.lciNV11.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.img23;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(528, 199);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem6.Size = new System.Drawing.Size(100, 90);
            this.layoutControlItem6.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // lciNV12
            // 
            this.lciNV12.Control = this.btn12;
            this.lciNV12.CustomizationFormText = "lciNV12";
            this.lciNV12.Location = new System.Drawing.Point(628, 199);
            this.lciNV12.MaxSize = new System.Drawing.Size(0, 92);
            this.lciNV12.MinSize = new System.Drawing.Size(49, 33);
            this.lciNV12.Name = "lciNV12";
            this.lciNV12.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV12.Size = new System.Drawing.Size(100, 90);
            this.lciNV12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV12.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV12.Text = "lciNV12";
            this.lciNV12.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV12.TextToControlDistance = 0;
            this.lciNV12.TextVisible = false;
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.img20;
            this.layoutControlItem16.CustomizationFormText = "layoutControlItem16";
            this.layoutControlItem16.Location = new System.Drawing.Point(628, 113);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem16.Size = new System.Drawing.Size(100, 86);
            this.layoutControlItem16.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem16.Text = "layoutControlItem16";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem16.TextToControlDistance = 0;
            this.layoutControlItem16.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.img19;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(528, 113);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem4.Size = new System.Drawing.Size(100, 86);
            this.layoutControlItem4.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem42
            // 
            this.layoutControlItem42.Control = this.img18;
            this.layoutControlItem42.CustomizationFormText = "layoutControlItem42";
            this.layoutControlItem42.Location = new System.Drawing.Point(428, 113);
            this.layoutControlItem42.MinSize = new System.Drawing.Size(31, 31);
            this.layoutControlItem42.Name = "layoutControlItem42";
            this.layoutControlItem42.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem42.Size = new System.Drawing.Size(100, 86);
            this.layoutControlItem42.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem42.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem42.Text = "layoutControlItem42";
            this.layoutControlItem42.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem42.TextToControlDistance = 0;
            this.layoutControlItem42.TextVisible = false;
            // 
            // layoutControlItem41
            // 
            this.layoutControlItem41.Control = this.img17;
            this.layoutControlItem41.CustomizationFormText = "layoutControlItem41";
            this.layoutControlItem41.Location = new System.Drawing.Point(328, 113);
            this.layoutControlItem41.MinSize = new System.Drawing.Size(31, 31);
            this.layoutControlItem41.Name = "layoutControlItem41";
            this.layoutControlItem41.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem41.Size = new System.Drawing.Size(100, 86);
            this.layoutControlItem41.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem41.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem41.Text = "layoutControlItem41";
            this.layoutControlItem41.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem41.TextToControlDistance = 0;
            this.layoutControlItem41.TextVisible = false;
            // 
            // layoutControlItem40
            // 
            this.layoutControlItem40.Control = this.img16;
            this.layoutControlItem40.CustomizationFormText = "layoutControlItem40";
            this.layoutControlItem40.Location = new System.Drawing.Point(228, 113);
            this.layoutControlItem40.MinSize = new System.Drawing.Size(31, 31);
            this.layoutControlItem40.Name = "layoutControlItem40";
            this.layoutControlItem40.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem40.Size = new System.Drawing.Size(100, 86);
            this.layoutControlItem40.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem40.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem40.Text = "layoutControlItem40";
            this.layoutControlItem40.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem40.TextToControlDistance = 0;
            this.layoutControlItem40.TextVisible = false;
            // 
            // layoutControlItem39
            // 
            this.layoutControlItem39.Control = this.img15;
            this.layoutControlItem39.CustomizationFormText = "layoutControlItem39";
            this.layoutControlItem39.Location = new System.Drawing.Point(128, 113);
            this.layoutControlItem39.MinSize = new System.Drawing.Size(31, 31);
            this.layoutControlItem39.Name = "layoutControlItem39";
            this.layoutControlItem39.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem39.Size = new System.Drawing.Size(100, 86);
            this.layoutControlItem39.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem39.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem39.Text = "layoutControlItem39";
            this.layoutControlItem39.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem39.TextToControlDistance = 0;
            this.layoutControlItem39.TextVisible = false;
            // 
            // layoutControlItem38
            // 
            this.layoutControlItem38.Control = this.img14;
            this.layoutControlItem38.CustomizationFormText = "layoutControlItem38";
            this.layoutControlItem38.Location = new System.Drawing.Point(28, 113);
            this.layoutControlItem38.MinSize = new System.Drawing.Size(31, 86);
            this.layoutControlItem38.Name = "layoutControlItem38";
            this.layoutControlItem38.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem38.Size = new System.Drawing.Size(100, 86);
            this.layoutControlItem38.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem38.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem38.Text = "layoutControlItem38";
            this.layoutControlItem38.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem38.TextToControlDistance = 0;
            this.layoutControlItem38.TextVisible = false;
            // 
            // lciNV5
            // 
            this.lciNV5.Control = this.btn5;
            this.lciNV5.CustomizationFormText = "layoutControlItem33";
            this.lciNV5.Location = new System.Drawing.Point(28, 23);
            this.lciNV5.MaxSize = new System.Drawing.Size(0, 95);
            this.lciNV5.MinSize = new System.Drawing.Size(49, 50);
            this.lciNV5.Name = "lciNV5";
            this.lciNV5.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV5.Size = new System.Drawing.Size(100, 90);
            this.lciNV5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV5.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV5.Text = "lciNV5";
            this.lciNV5.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV5.TextToControlDistance = 0;
            this.lciNV5.TextVisible = false;
            // 
            // layoutControlItem34
            // 
            this.layoutControlItem34.Control = this.img11;
            this.layoutControlItem34.CustomizationFormText = "layoutControlItem34";
            this.layoutControlItem34.Location = new System.Drawing.Point(128, 23);
            this.layoutControlItem34.MinSize = new System.Drawing.Size(31, 31);
            this.layoutControlItem34.Name = "layoutControlItem34";
            this.layoutControlItem34.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem34.Size = new System.Drawing.Size(100, 90);
            this.layoutControlItem34.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem34.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem34.Text = "layoutControlItem34";
            this.layoutControlItem34.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem34.TextToControlDistance = 0;
            this.layoutControlItem34.TextVisible = false;
            // 
            // lciNV6
            // 
            this.lciNV6.AppearanceItemCaption.ForeColor = System.Drawing.Color.DarkRed;
            this.lciNV6.AppearanceItemCaption.Options.UseForeColor = true;
            this.lciNV6.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lciNV6.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lciNV6.Control = this.btn6;
            this.lciNV6.CustomizationFormText = "layoutControlItem35";
            this.lciNV6.Location = new System.Drawing.Point(228, 23);
            this.lciNV6.MaxSize = new System.Drawing.Size(0, 92);
            this.lciNV6.MinSize = new System.Drawing.Size(49, 33);
            this.lciNV6.Name = "lciNV6";
            this.lciNV6.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV6.Size = new System.Drawing.Size(100, 90);
            this.lciNV6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV6.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV6.Text = "lciNV6";
            this.lciNV6.TextLocation = DevExpress.Utils.Locations.Bottom;
            this.lciNV6.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV6.TextToControlDistance = 0;
            this.lciNV6.TextVisible = false;
            // 
            // layoutControlItem36
            // 
            this.layoutControlItem36.Control = this.img12;
            this.layoutControlItem36.CustomizationFormText = "layoutControlItem36";
            this.layoutControlItem36.Location = new System.Drawing.Point(328, 23);
            this.layoutControlItem36.MinSize = new System.Drawing.Size(31, 31);
            this.layoutControlItem36.Name = "layoutControlItem36";
            this.layoutControlItem36.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem36.Size = new System.Drawing.Size(100, 90);
            this.layoutControlItem36.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem36.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem36.Text = "layoutControlItem36";
            this.layoutControlItem36.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem36.TextToControlDistance = 0;
            this.layoutControlItem36.TextVisible = false;
            // 
            // lciNV7
            // 
            this.lciNV7.Control = this.btn7;
            this.lciNV7.CustomizationFormText = "layoutControlItem37";
            this.lciNV7.Location = new System.Drawing.Point(428, 23);
            this.lciNV7.MaxSize = new System.Drawing.Size(0, 92);
            this.lciNV7.MinSize = new System.Drawing.Size(49, 33);
            this.lciNV7.Name = "lciNV7";
            this.lciNV7.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV7.Size = new System.Drawing.Size(100, 90);
            this.lciNV7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV7.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV7.Text = "lciNV7";
            this.lciNV7.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV7.TextToControlDistance = 0;
            this.lciNV7.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.img13;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(528, 23);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem2.Size = new System.Drawing.Size(100, 90);
            this.layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // lciNV8
            // 
            this.lciNV8.Control = this.btn8;
            this.lciNV8.CustomizationFormText = "lciNV8";
            this.lciNV8.Location = new System.Drawing.Point(628, 23);
            this.lciNV8.MaxSize = new System.Drawing.Size(0, 92);
            this.lciNV8.MinSize = new System.Drawing.Size(49, 33);
            this.lciNV8.Name = "lciNV8";
            this.lciNV8.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV8.Size = new System.Drawing.Size(100, 90);
            this.lciNV8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV8.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV8.Text = "lciNV8";
            this.lciNV8.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV8.TextToControlDistance = 0;
            this.lciNV8.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.img34;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(728, 23);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem1.Size = new System.Drawing.Size(100, 90);
            this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.img35;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(728, 113);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem3.Size = new System.Drawing.Size(100, 86);
            this.layoutControlItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.img36;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(728, 199);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem7.Size = new System.Drawing.Size(100, 90);
            this.layoutControlItem7.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.img37;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new System.Drawing.Point(728, 289);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem9.Size = new System.Drawing.Size(100, 86);
            this.layoutControlItem9.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.img38;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new System.Drawing.Point(728, 375);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem10.Size = new System.Drawing.Size(100, 90);
            this.layoutControlItem10.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem10.Text = "layoutControlItem10";
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // lciNV1
            // 
            this.lciNV1.Control = this.btn1;
            this.lciNV1.CustomizationFormText = "lciNV1";
            this.lciNV1.Location = new System.Drawing.Point(828, 23);
            this.lciNV1.MaxSize = new System.Drawing.Size(0, 90);
            this.lciNV1.MinSize = new System.Drawing.Size(48, 50);
            this.lciNV1.Name = "lciNV1";
            this.lciNV1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV1.Size = new System.Drawing.Size(100, 90);
            this.lciNV1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV1.Text = "lciNV1";
            this.lciNV1.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV1.TextToControlDistance = 0;
            this.lciNV1.TextVisible = false;
            // 
            // lciNV2
            // 
            this.lciNV2.Control = this.btn2;
            this.lciNV2.CustomizationFormText = "lciNV2";
            this.lciNV2.Location = new System.Drawing.Point(828, 199);
            this.lciNV2.MaxSize = new System.Drawing.Size(0, 92);
            this.lciNV2.MinSize = new System.Drawing.Size(48, 32);
            this.lciNV2.Name = "lciNV2";
            this.lciNV2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV2.Size = new System.Drawing.Size(100, 90);
            this.lciNV2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV2.Text = "lciNV2";
            this.lciNV2.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV2.TextToControlDistance = 0;
            this.lciNV2.TextVisible = false;
            // 
            // lciNV3
            // 
            this.lciNV3.Control = this.btn3;
            this.lciNV3.CustomizationFormText = "lciNV3";
            this.lciNV3.Location = new System.Drawing.Point(828, 375);
            this.lciNV3.MaxSize = new System.Drawing.Size(0, 91);
            this.lciNV3.MinSize = new System.Drawing.Size(48, 32);
            this.lciNV3.Name = "lciNV3";
            this.lciNV3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lciNV3.Size = new System.Drawing.Size(100, 90);
            this.lciNV3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciNV3.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciNV3.Text = "lciNV3";
            this.lciNV3.TextSize = new System.Drawing.Size(0, 0);
            this.lciNV3.TextToControlDistance = 0;
            this.lciNV3.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.img39;
            this.layoutControlItem14.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem14.Location = new System.Drawing.Point(828, 113);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem14.Size = new System.Drawing.Size(100, 86);
            this.layoutControlItem14.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem14.Text = "layoutControlItem14";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.img40;
            this.layoutControlItem15.CustomizationFormText = "layoutControlItem15";
            this.layoutControlItem15.Location = new System.Drawing.Point(828, 289);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem15.Size = new System.Drawing.Size(100, 86);
            this.layoutControlItem15.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem15.Text = "layoutControlItem15";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem15.TextToControlDistance = 0;
            this.layoutControlItem15.TextVisible = false;
            // 
            // BusinessProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lcgNV);
            this.Name = "BusinessProcess";
            this.Size = new System.Drawing.Size(950, 600);
            ((System.ComponentModel.ISupportInitialize)(this.abc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgNV)).EndInit();
            this.lcgNV.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.img30.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img20.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img33.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img23.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img29.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img19.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img13.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img32.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img31.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img28.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img27.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img26.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img25.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img24.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img22.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img21.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img18.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img17.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img16.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img15.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img12.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img11.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img14.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lgrNV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgDM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img34.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img35.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img36.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img37.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img38.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img39.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img40.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem54)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem56)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem52)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem51)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem49)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem48)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem44)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem46)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem42)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem41)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem40)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem39)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem38)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem34)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem36)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNV3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlGroup abc;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControl lcgNV;
        private MyButton btn21;
        private MyButton btn19;
        private MyButton btn17;
        private MyButton btn15;
        private DevExpress.XtraEditors.PictureEdit img32;
        private MyButton btn14;
        private DevExpress.XtraEditors.PictureEdit img31;
        private MyButton btn13;
        private DevExpress.XtraEditors.PictureEdit img28;
        private DevExpress.XtraEditors.PictureEdit img27;
        private DevExpress.XtraEditors.PictureEdit img26;
        private DevExpress.XtraEditors.PictureEdit img25;
        private DevExpress.XtraEditors.PictureEdit img24;
        private MyButton btn11;
        private DevExpress.XtraEditors.PictureEdit img22;
        private MyButton btn10;
        private DevExpress.XtraEditors.PictureEdit img21;
        private MyButton btn9;
        private DevExpress.XtraEditors.PictureEdit img18;
        private DevExpress.XtraEditors.PictureEdit img17;
        private DevExpress.XtraEditors.PictureEdit img16;
        private DevExpress.XtraEditors.PictureEdit img15;
        private MyButton btn7;
        private DevExpress.XtraEditors.PictureEdit img12;
        private MyButton btn6;
        private DevExpress.XtraEditors.PictureEdit img11;
        private DevExpress.XtraEditors.PictureEdit img14;
        private MyButton btn5;
        private DevExpress.XtraLayout.LayoutControlGroup lgrNV;
        private DevExpress.XtraLayout.LayoutControlItem lciNV5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem34;
        private DevExpress.XtraLayout.LayoutControlItem lciNV6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem36;
        private DevExpress.XtraLayout.LayoutControlItem lciNV7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem38;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem39;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem40;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem41;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem42;
        private DevExpress.XtraLayout.LayoutControlItem lciNV9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem44;
        private DevExpress.XtraLayout.LayoutControlItem lciNV10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem46;
        private DevExpress.XtraLayout.LayoutControlItem lciNV11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem48;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem50;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem51;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem52;
        private DevExpress.XtraLayout.LayoutControlItem lciNV13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem49;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem54;
        private DevExpress.XtraLayout.LayoutControlItem lciNV14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem56;
        private DevExpress.XtraLayout.LayoutControlItem lciNV15;
        private DevExpress.XtraLayout.LayoutControlItem lciNV17;
        private DevExpress.XtraLayout.LayoutControlItem lciNV19;
        private DevExpress.XtraLayout.LayoutControlItem lciNV21;
        private MyButton btn16;
        private MyButton btn12;
        private MyButton btn8;
        private DevExpress.XtraEditors.PictureEdit img33;
        private DevExpress.XtraEditors.PictureEdit img23;
        private DevExpress.XtraEditors.PictureEdit img29;
        private DevExpress.XtraEditors.PictureEdit img19;
        private DevExpress.XtraEditors.PictureEdit img13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem lciNV8;
        private DevExpress.XtraLayout.LayoutControlItem lciNV12;
        private DevExpress.XtraLayout.LayoutControlItem lciNV16;
        private MyButton btn23;
        private DevExpress.XtraEditors.PictureEdit img30;
        private DevExpress.XtraEditors.PictureEdit img20;
        private DevExpress.XtraLayout.LayoutControlItem lciNV23;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraLayout.LayoutControlGroup lcgDM;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private MyButton btn22;
        private MyButton btn20;
        private MyButton btn18;
        private DevExpress.XtraLayout.LayoutControlItem lciNV18;
        private DevExpress.XtraLayout.LayoutControlItem lciNV20;
        private DevExpress.XtraLayout.LayoutControlItem lciNV22;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem13;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem14;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem11;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem12;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.PictureEdit img34;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.PictureEdit img35;
        private DevExpress.XtraEditors.PictureEdit img38;
        private DevExpress.XtraEditors.PictureEdit img37;
        private DevExpress.XtraEditors.PictureEdit img36;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private MyButton btn3;
        private MyButton btn2;
        private MyButton btn1;
        private DevExpress.XtraLayout.LayoutControlItem lciNV1;
        private DevExpress.XtraLayout.LayoutControlItem lciNV2;
        private DevExpress.XtraLayout.LayoutControlItem lciNV3;
        private DevExpress.XtraEditors.PictureEdit img40;
        private DevExpress.XtraEditors.PictureEdit img39;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private MyButton btn25;
        private MyButton btn24;
        private DevExpress.XtraLayout.LayoutControlItem lciNV24;
        private DevExpress.XtraLayout.LayoutControlItem lciNV25;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem10;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem15;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem16;
    }
}
