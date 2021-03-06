namespace LapChungTu
{
    partial class frmBangLuong
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.gcBangLuong = new DevExpress.XtraGrid.GridControl();
            this.gvBangLuong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colThang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNam = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayCong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTongLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTamUng = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBHYT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBHTN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTNCN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThucLinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBangLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBangLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Controls.Add(this.gcBangLuong);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(873, 359);
            this.layoutControl1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(478, 331);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Bỏ qua";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(355, 331);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(67, 22);
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "Đồng ý";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gcBangLuong
            // 
            this.gcBangLuong.EmbeddedNavigator.Name = "";
            this.gcBangLuong.Location = new System.Drawing.Point(7, 7);
            this.gcBangLuong.MainView = this.gvBangLuong;
            this.gcBangLuong.Name = "gcBangLuong";
            this.gcBangLuong.Size = new System.Drawing.Size(860, 313);
            this.gcBangLuong.TabIndex = 4;
            this.gcBangLuong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBangLuong});
            // 
            // gvBangLuong
            // 
            this.gvBangLuong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colThang,
            this.colNam,
            this.colNgayCong,
            this.colTongLuong,
            this.colTamUng,
            this.colBH,
            this.colBHYT,
            this.colBHTN,
            this.colTNCN,
            this.colThucLinh,
            this.colID});
            this.gvBangLuong.GridControl = this.gcBangLuong;
            this.gvBangLuong.Name = "gvBangLuong";
            this.gvBangLuong.OptionsView.ShowGroupPanel = false;
            // 
            // colThang
            // 
            this.colThang.Caption = "Tháng";
            this.colThang.FieldName = "Thang";
            this.colThang.Name = "colThang";
            this.colThang.OptionsColumn.AllowEdit = false;
            this.colThang.Visible = true;
            this.colThang.VisibleIndex = 0;
            this.colThang.Width = 43;
            // 
            // colNam
            // 
            this.colNam.Caption = "Năm";
            this.colNam.FieldName = "Nam";
            this.colNam.Name = "colNam";
            this.colNam.OptionsColumn.AllowEdit = false;
            this.colNam.Visible = true;
            this.colNam.VisibleIndex = 1;
            this.colNam.Width = 41;
            // 
            // colNgayCong
            // 
            this.colNgayCong.Caption = "Ngày Công";
            this.colNgayCong.DisplayFormat.FormatString = "### ### ### ##0.######";
            this.colNgayCong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNgayCong.FieldName = "TSNgayCong";
            this.colNgayCong.Name = "colNgayCong";
            this.colNgayCong.OptionsColumn.AllowEdit = false;
            this.colNgayCong.Visible = true;
            this.colNgayCong.VisibleIndex = 2;
            this.colNgayCong.Width = 92;
            // 
            // colTongLuong
            // 
            this.colTongLuong.Caption = "Tổng Lương";
            this.colTongLuong.DisplayFormat.FormatString = "### ### ### ##0";
            this.colTongLuong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTongLuong.FieldName = "TongLuongThang";
            this.colTongLuong.Name = "colTongLuong";
            this.colTongLuong.OptionsColumn.AllowEdit = false;
            this.colTongLuong.Visible = true;
            this.colTongLuong.VisibleIndex = 3;
            this.colTongLuong.Width = 92;
            // 
            // colTamUng
            // 
            this.colTamUng.Caption = "Tổng Tạm Ứng";
            this.colTamUng.DisplayFormat.FormatString = "### ### ### ##0";
            this.colTamUng.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTamUng.FieldName = "TongTamUng";
            this.colTamUng.Name = "colTamUng";
            this.colTamUng.OptionsColumn.AllowEdit = false;
            this.colTamUng.Visible = true;
            this.colTamUng.VisibleIndex = 4;
            this.colTamUng.Width = 92;
            // 
            // colBH
            // 
            this.colBH.Caption = "BHXH";
            this.colBH.DisplayFormat.FormatString = "### ### ### ##0";
            this.colBH.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBH.FieldName = "TBHXH";
            this.colBH.Name = "colBH";
            this.colBH.OptionsColumn.AllowEdit = false;
            this.colBH.Visible = true;
            this.colBH.VisibleIndex = 5;
            this.colBH.Width = 92;
            // 
            // colBHYT
            // 
            this.colBHYT.Caption = "BHYT";
            this.colBHYT.DisplayFormat.FormatString = "### ### ### ##0";
            this.colBHYT.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBHYT.FieldName = "TBHYT";
            this.colBHYT.Name = "colBHYT";
            this.colBHYT.Visible = true;
            this.colBHYT.VisibleIndex = 6;
            this.colBHYT.Width = 92;
            // 
            // colBHTN
            // 
            this.colBHTN.Caption = "BHTN";
            this.colBHTN.DisplayFormat.FormatString = "### ### ### ##0";
            this.colBHTN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBHTN.FieldName = "TBHTN";
            this.colBHTN.Name = "colBHTN";
            this.colBHTN.Visible = true;
            this.colBHTN.VisibleIndex = 7;
            this.colBHTN.Width = 111;
            // 
            // colTNCN
            // 
            this.colTNCN.Caption = "Thuế TNCN";
            this.colTNCN.DisplayFormat.FormatString = "### ### ### ##0";
            this.colTNCN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTNCN.FieldName = "TongThueTNCN";
            this.colTNCN.Name = "colTNCN";
            this.colTNCN.OptionsColumn.AllowEdit = false;
            this.colTNCN.Visible = true;
            this.colTNCN.VisibleIndex = 8;
            this.colTNCN.Width = 92;
            // 
            // colThucLinh
            // 
            this.colThucLinh.Caption = "Tổng Thực Lĩnh";
            this.colThucLinh.DisplayFormat.FormatString = "### ### ### ##0";
            this.colThucLinh.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colThucLinh.FieldName = "TongThucLinh";
            this.colThucLinh.Name = "colThucLinh";
            this.colThucLinh.OptionsColumn.AllowEdit = false;
            this.colThucLinh.Visible = true;
            this.colThucLinh.VisibleIndex = 9;
            this.colThucLinh.Width = 92;
            // 
            // colID
            // 
            this.colID.Caption = "ID";
            this.colID.FieldName = "IDBL";
            this.colID.Name = "colID";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(873, 359);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcBangLuong;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem1.Size = new System.Drawing.Size(871, 324);
            this.layoutControlItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnCancel;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(471, 324);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem3.Size = new System.Drawing.Size(79, 33);
            this.layoutControlItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 324);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem1.Size = new System.Drawing.Size(348, 33);
            this.emptySpaceItem1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(550, 324);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem2.Size = new System.Drawing.Size(321, 33);
            this.emptySpaceItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(426, 324);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.emptySpaceItem3.Size = new System.Drawing.Size(45, 33);
            this.emptySpaceItem3.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnOK;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(348, 324);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem2.Size = new System.Drawing.Size(78, 33);
            this.layoutControlItem2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // frmBangLuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 359);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmBangLuong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chọn bảng lương cần xử lý";
            this.Load += new System.EventHandler(this.frmBangLuong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcBangLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBangLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcBangLuong;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBangLuong;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colThang;
        private DevExpress.XtraGrid.Columns.GridColumn colNam;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayCong;
        private DevExpress.XtraGrid.Columns.GridColumn colTongLuong;
        private DevExpress.XtraGrid.Columns.GridColumn colTamUng;
        private DevExpress.XtraGrid.Columns.GridColumn colBH;
        private DevExpress.XtraGrid.Columns.GridColumn colTNCN;
        private DevExpress.XtraGrid.Columns.GridColumn colThucLinh;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colBHYT;
        private DevExpress.XtraGrid.Columns.GridColumn colBHTN;
    }
}