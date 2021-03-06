namespace Chamcong
{
    partial class frmChamcong
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
            this.spinThang = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.panelCong = new DevExpress.XtraEditors.PanelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cbCongT7 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkSat = new DevExpress.XtraEditors.CheckEdit();
            this.panelPhongban = new DevExpress.XtraEditors.PanelControl();
            this.lookupPhongBan = new DevExpress.XtraEditors.LookUpEdit();
            this.btnChamCong = new DevExpress.XtraEditors.SimpleButton();
            this.groupStyle = new DevExpress.XtraEditors.RadioGroup();
            this.cbLoaiCong = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.dtpTo = new DevExpress.XtraEditors.DateEdit();
            this.dtpFrom = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gridNhanVien = new DevExpress.XtraGrid.GridControl();
            this.gridListNhanvien = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridLoaiCong = new DevExpress.XtraGrid.GridControl();
            this.gvLoaiCong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcChon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSoCong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gcKyHieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcDienGiai = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.spinThang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelCong)).BeginInit();
            this.panelCong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbCongT7.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelPhongban)).BeginInit();
            this.panelPhongban.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookupPhongBan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupStyle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbLoaiCong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridListNhanvien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLoaiCong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoaiCong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // spinThang
            // 
            this.spinThang.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinThang.Location = new System.Drawing.Point(91, 15);
            this.spinThang.Name = "spinThang";
            this.spinThang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinThang.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinThang.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinThang.Properties.MaxLength = 2;
            this.spinThang.Properties.MaxValue = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.spinThang.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinThang.Size = new System.Drawing.Size(67, 20);
            this.spinThang.TabIndex = 0;
            this.spinThang.EditValueChanged += new System.EventHandler(this.spinThang_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(25, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Chọn tháng:";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(183, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Đồng ý";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(564, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Lưu trữ";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(661, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(107, 23);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "In bảng chấm công";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(774, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(91, 23);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Thoát";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.panelCong);
            this.groupControl1.Controls.Add(this.chkSat);
            this.groupControl1.Controls.Add(this.panelPhongban);
            this.groupControl1.Controls.Add(this.btnChamCong);
            this.groupControl1.Controls.Add(this.groupStyle);
            this.groupControl1.Controls.Add(this.cbLoaiCong);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.dtpTo);
            this.groupControl1.Controls.Add(this.dtpFrom);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Location = new System.Drawing.Point(0, 50);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(884, 135);
            this.groupControl1.TabIndex = 6;
            this.groupControl1.Text = "Chấm công nhanh";
            // 
            // panelCong
            // 
            this.panelCong.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelCong.Controls.Add(this.labelControl5);
            this.panelCong.Controls.Add(this.cbCongT7);
            this.panelCong.Location = new System.Drawing.Point(546, 62);
            this.panelCong.Name = "panelCong";
            this.panelCong.Size = new System.Drawing.Size(207, 37);
            this.panelCong.TabIndex = 15;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(4, 9);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(49, 13);
            this.labelControl5.TabIndex = 0;
            this.labelControl5.Text = "Loại công:";
            // 
            // cbCongT7
            // 
            this.cbCongT7.Location = new System.Drawing.Point(60, 7);
            this.cbCongT7.Name = "cbCongT7";
            this.cbCongT7.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbCongT7.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbCongT7.Size = new System.Drawing.Size(93, 20);
            this.cbCongT7.TabIndex = 14;
            // 
            // chkSat
            // 
            this.chkSat.Location = new System.Drawing.Point(461, 69);
            this.chkSat.Name = "chkSat";
            this.chkSat.Properties.Caption = "Chấm thứ 7";
            this.chkSat.Size = new System.Drawing.Size(84, 19);
            this.chkSat.TabIndex = 13;
            this.chkSat.CheckedChanged += new System.EventHandler(this.chkSat_CheckedChanged);
            // 
            // panelPhongban
            // 
            this.panelPhongban.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelPhongban.Controls.Add(this.lookupPhongBan);
            this.panelPhongban.Location = new System.Drawing.Point(258, 59);
            this.panelPhongban.Name = "panelPhongban";
            this.panelPhongban.Size = new System.Drawing.Size(142, 39);
            this.panelPhongban.TabIndex = 12;
            // 
            // lookupPhongBan
            // 
            this.lookupPhongBan.Location = new System.Drawing.Point(6, 9);
            this.lookupPhongBan.Name = "lookupPhongBan";
            this.lookupPhongBan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookupPhongBan.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaBP", "Mã phòng ban", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenBP", "Tên phòng ban", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
            this.lookupPhongBan.Properties.NullText = "";
            this.lookupPhongBan.Size = new System.Drawing.Size(133, 20);
            this.lookupPhongBan.TabIndex = 11;
            // 
            // btnChamCong
            // 
            this.btnChamCong.Location = new System.Drawing.Point(604, 31);
            this.btnChamCong.Name = "btnChamCong";
            this.btnChamCong.Size = new System.Drawing.Size(110, 23);
            this.btnChamCong.TabIndex = 10;
            this.btnChamCong.Text = "Chấm công nhanh";
            this.btnChamCong.Click += new System.EventHandler(this.btnChamCong_Click);
            // 
            // groupStyle
            // 
            this.groupStyle.Location = new System.Drawing.Point(21, 59);
            this.groupStyle.Name = "groupStyle";
            this.groupStyle.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.groupStyle.Properties.Appearance.Options.UseBackColor = true;
            this.groupStyle.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.groupStyle.Properties.Columns = 2;
            this.groupStyle.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Tất cả nhân viên"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Theo phòng ban")});
            this.groupStyle.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupStyle.Size = new System.Drawing.Size(221, 39);
            this.groupStyle.TabIndex = 9;
            this.groupStyle.SelectedIndexChanged += new System.EventHandler(this.groupStyle_SelectedIndexChanged);
            // 
            // cbLoaiCong
            // 
            this.cbLoaiCong.Location = new System.Drawing.Point(464, 33);
            this.cbLoaiCong.Name = "cbLoaiCong";
            this.cbLoaiCong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbLoaiCong.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbLoaiCong.Size = new System.Drawing.Size(106, 20);
            this.cbLoaiCong.TabIndex = 8;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(395, 37);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(49, 13);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Loại công:";
            // 
            // dtpTo
            // 
            this.dtpTo.EditValue = null;
            this.dtpTo.Location = new System.Drawing.Point(264, 31);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTo.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtpTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpTo.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dtpTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpTo.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dtpTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpTo.Size = new System.Drawing.Size(100, 20);
            this.dtpTo.TabIndex = 5;
            // 
            // dtpFrom
            // 
            this.dtpFrom.EditValue = null;
            this.dtpFrom.Location = new System.Drawing.Point(85, 31);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpFrom.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtpFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpFrom.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dtpFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpFrom.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dtpFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpFrom.Size = new System.Drawing.Size(100, 20);
            this.dtpFrom.TabIndex = 4;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(207, 34);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(51, 13);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "Đến ngày:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(25, 34);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(44, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Từ ngày:";
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.btnOK);
            this.panelControl1.Controls.Add(this.spinThang);
            this.panelControl1.Controls.Add(this.btnExit);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.btnPrint);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(884, 49);
            this.panelControl1.TabIndex = 7;
            // 
            // gridNhanVien
            // 
            this.gridNhanVien.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridNhanVien.EmbeddedNavigator.Name = "";
            this.gridNhanVien.Location = new System.Drawing.Point(0, 155);
            this.gridNhanVien.MainView = this.gridListNhanvien;
            this.gridNhanVien.Name = "gridNhanVien";
            this.gridNhanVien.Size = new System.Drawing.Size(884, 360);
            this.gridNhanVien.TabIndex = 8;
            this.gridNhanVien.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridListNhanvien});
            // 
            // gridListNhanvien
            // 
            this.gridListNhanvien.GridControl = this.gridNhanVien;
            this.gridListNhanvien.Name = "gridListNhanvien";
            this.gridListNhanvien.OptionsView.ColumnAutoWidth = false;
            this.gridListNhanvien.OptionsView.ShowGroupPanel = false;
            this.gridListNhanvien.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridListNhanvien_CellValueChanged);
            // 
            // gridLoaiCong
            // 
            this.gridLoaiCong.EmbeddedNavigator.Name = "";
            this.gridLoaiCong.Location = new System.Drawing.Point(465, 211);
            this.gridLoaiCong.MainView = this.gvLoaiCong;
            this.gridLoaiCong.Name = "gridLoaiCong";
            this.gridLoaiCong.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.gridLoaiCong.Size = new System.Drawing.Size(400, 200);
            this.gridLoaiCong.TabIndex = 9;
            this.gridLoaiCong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLoaiCong});
            // 
            // gvLoaiCong
            // 
            this.gvLoaiCong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcChon,
            this.gcSoCong,
            this.gcKyHieu,
            this.gcDienGiai});
            this.gvLoaiCong.GridControl = this.gridLoaiCong;
            this.gvLoaiCong.Name = "gvLoaiCong";
            this.gvLoaiCong.OptionsView.ShowGroupPanel = false;
            // 
            // gcChon
            // 
            this.gcChon.Caption = "Chọn";
            this.gcChon.FieldName = "Chon";
            this.gcChon.Name = "gcChon";
            this.gcChon.Visible = true;
            this.gcChon.VisibleIndex = 0;
            // 
            // gcSoCong
            // 
            this.gcSoCong.Caption = "Số Công";
            this.gcSoCong.ColumnEdit = this.repositoryItemTextEdit1;
            this.gcSoCong.DisplayFormat.FormatString = "##0.##";
            this.gcSoCong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gcSoCong.FieldName = "SoCong";
            this.gcSoCong.Name = "gcSoCong";
            this.gcSoCong.Visible = true;
            this.gcSoCong.VisibleIndex = 1;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.DisplayFormat.FormatString = "##0.##";
            this.repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.EditFormat.FormatString = "##0.##";
            this.repositoryItemTextEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.Mask.EditMask = "##0.##";
            this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit1.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // gcKyHieu
            // 
            this.gcKyHieu.Caption = "Ký Hiệu";
            this.gcKyHieu.FieldName = "KyHieu";
            this.gcKyHieu.Name = "gcKyHieu";
            this.gcKyHieu.OptionsColumn.AllowEdit = false;
            this.gcKyHieu.Visible = true;
            this.gcKyHieu.VisibleIndex = 2;
            // 
            // gcDienGiai
            // 
            this.gcDienGiai.Caption = "Diễn Giải";
            this.gcDienGiai.FieldName = "DienGiai";
            this.gcDienGiai.Name = "gcDienGiai";
            this.gcDienGiai.OptionsColumn.AllowEdit = false;
            this.gcDienGiai.Visible = true;
            this.gcDienGiai.VisibleIndex = 3;
            // 
            // frmChamcong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 515);
            this.Controls.Add(this.gridLoaiCong);
            this.Controls.Add(this.gridNhanVien);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.groupControl1);
            this.Name = "frmChamcong";
            this.Text = "frmChamcong";
            this.Load += new System.EventHandler(this.frmChamcong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spinThang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelCong)).EndInit();
            this.panelCong.ResumeLayout(false);
            this.panelCong.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbCongT7.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelPhongban)).EndInit();
            this.panelPhongban.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lookupPhongBan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupStyle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbLoaiCong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridListNhanvien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLoaiCong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLoaiCong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SpinEdit spinThang;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.DateEdit dtpTo;
        private DevExpress.XtraEditors.DateEdit dtpFrom;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ComboBoxEdit cbLoaiCong;
        private DevExpress.XtraEditors.RadioGroup groupStyle;
        private DevExpress.XtraEditors.PanelControl panelPhongban;
        private DevExpress.XtraEditors.LookUpEdit lookupPhongBan;
        private DevExpress.XtraEditors.SimpleButton btnChamCong;
        private DevExpress.XtraGrid.GridControl gridNhanVien;
        private DevExpress.XtraGrid.Views.Grid.GridView gridListNhanvien;
        private DevExpress.XtraGrid.GridControl gridLoaiCong;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLoaiCong;
        private DevExpress.XtraGrid.Columns.GridColumn gcChon;
        private DevExpress.XtraGrid.Columns.GridColumn gcSoCong;
        private DevExpress.XtraGrid.Columns.GridColumn gcKyHieu;
        private DevExpress.XtraGrid.Columns.GridColumn gcDienGiai;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.ComboBoxEdit cbCongT7;
        private DevExpress.XtraEditors.CheckEdit chkSat;
        private DevExpress.XtraEditors.PanelControl panelCong;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}