﻿namespace AppClient.Controls
{
    partial class ucStatisticsMaster
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
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.mnuSort = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.btnAscending = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDescending = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNoSort = new System.Windows.Forms.ToolStripMenuItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.splitContainer = new DevExpress.XtraEditors.SplitContainerControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cboPages = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.btnCopyStatus = new DevExpress.XtraEditors.SimpleButton();
            this.txtSearchStatus = new DevExpress.XtraEditors.LabelControl();
            this.btnBestFitColumns = new DevExpress.XtraEditors.SimpleButton();
            this.mainLayout = new DevExpress.XtraLayout.LayoutControl();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnExecute = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.gpStatisticsParameters = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPages.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainLayout)).BeginInit();
            this.mainLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpStatisticsParameters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gcMain, 3);
            this.gcMain.ContextMenuStrip = this.mnuSort;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(3, 29);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(656, 352);
            this.gcMain.TabIndex = 4;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            this.gcMain.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.gcMain_PreviewKeyDown);
            // 
            // mnuSort
            // 
            this.mnuSort.Name = "mnuSort";
            this.mnuSort.Size = new System.Drawing.Size(61, 4);
            // 
            // gvMain
            // 
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsCustomization.AllowChangeBandParent = true;
            this.gvMain.OptionsCustomization.AllowGroup = false;
            this.gvMain.OptionsFind.AlwaysVisible = true;
            this.gvMain.OptionsLayout.Columns.StoreAppearance = true;
            this.gvMain.OptionsLayout.StoreAppearance = true;
            this.gvMain.OptionsMenu.ShowConditionalFormattingItem = true;
            this.gvMain.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.EnableAppearanceOddRow = true;
            this.gvMain.OptionsView.ShowAutoFilterRow = true;
            this.gvMain.OptionsView.ShowColumnHeaders = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            this.gvMain.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvMain_RowCellClick);
            this.gvMain.ShowGridMenu += new DevExpress.XtraGrid.Views.Grid.GridMenuEventHandler(this.gvMain_ShowGridMenu);
            this.gvMain.UnboundExpressionEditorCreated += new DevExpress.XtraGrid.Views.Base.UnboundExpressionEditorEventHandler(this.gvMain_UnboundExpressionEditorCreated);
            // 
            // btnAscending
            // 
            this.btnAscending.Image = global::AppClient.Properties.Resources.Ascending;
            this.btnAscending.Name = "btnAscending";
            this.btnAscending.Size = new System.Drawing.Size(136, 22);
            this.btnAscending.Text = "Ascending";
            // 
            // btnDescending
            // 
            this.btnDescending.Image = global::AppClient.Properties.Resources.Descending;
            this.btnDescending.Name = "btnDescending";
            this.btnDescending.Size = new System.Drawing.Size(136, 22);
            this.btnDescending.Text = "Descending";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(133, 6);
            // 
            // btnNoSort
            // 
            this.btnNoSort.Name = "btnNoSort";
            this.btnNoSort.Size = new System.Drawing.Size(136, 22);
            this.btnNoSort.Text = "Clear sort";
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 367);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(88, 10);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // splitContainer
            // 
            this.splitContainer.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer.Panel1.Text = "Panel1";
            this.splitContainer.Panel2.Controls.Add(this.mainLayout);
            this.splitContainer.Panel2.Text = "Panel2";
            this.splitContainer.Size = new System.Drawing.Size(952, 413);
            this.splitContainer.SplitterPosition = 280;
            this.splitContainer.TabIndex = 1;
            this.splitContainer.Text = "splitContainerControl1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Controls.Add(this.gcMain, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cboPages, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnCopyStatus, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtSearchStatus, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnBestFitColumns, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(662, 413);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // cboPages
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.cboPages, 2);
            this.cboPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboPages.Location = new System.Drawing.Point(3, 387);
            this.cboPages.Name = "cboPages";
            this.cboPages.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPages.Size = new System.Drawing.Size(628, 20);
            this.cboPages.TabIndex = 5;
            // 
            // btnCopyStatus
            // 
            this.btnCopyStatus.AllowFocus = false;
            this.btnCopyStatus.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.btnCopyStatus.Location = new System.Drawing.Point(6, 2);
            this.btnCopyStatus.Margin = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this.btnCopyStatus.Name = "btnCopyStatus";
            this.btnCopyStatus.Size = new System.Drawing.Size(24, 22);
            this.btnCopyStatus.TabIndex = 6;
            this.btnCopyStatus.TabStop = false;
            this.btnCopyStatus.Visible = false;
            this.btnCopyStatus.Click += new System.EventHandler(this.btnCopyStatus_Click);
            // 
            // txtSearchStatus
            // 
            this.txtSearchStatus.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchStatus.Appearance.Options.UseFont = true;
            this.txtSearchStatus.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.tableLayoutPanel1.SetColumnSpan(this.txtSearchStatus, 2);
            this.txtSearchStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearchStatus.Location = new System.Drawing.Point(35, 3);
            this.txtSearchStatus.Name = "txtSearchStatus";
            this.txtSearchStatus.Size = new System.Drawing.Size(624, 20);
            this.txtSearchStatus.TabIndex = 7;
            // 
            // btnBestFitColumns
            // 
            this.btnBestFitColumns.ImageOptions.Image = global::AppClient.Properties.Resources.AutoSize;
            this.btnBestFitColumns.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnBestFitColumns.Location = new System.Drawing.Point(637, 387);
            this.btnBestFitColumns.Name = "btnBestFitColumns";
            this.btnBestFitColumns.Size = new System.Drawing.Size(22, 23);
            this.btnBestFitColumns.TabIndex = 8;
            this.btnBestFitColumns.Text = "btnBestFitColumns";
            // 
            // mainLayout
            // 
            this.mainLayout.AllowCustomization = false;
            this.mainLayout.Controls.Add(this.btnExport);
            this.mainLayout.Controls.Add(this.btnExecute);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.Root = this.layoutControlGroup1;
            this.mainLayout.Size = new System.Drawing.Size(280, 413);
            this.mainLayout.TabIndex = 0;
            this.mainLayout.Text = "layoutControl1";
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.ImageOptions.Image = global::AppClient.Properties.Resources.EXPORT;
            this.btnExport.Location = new System.Drawing.Point(97, 381);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(84, 22);
            this.btnExport.StyleController = this.mainLayout;
            this.btnExport.TabIndex = 6;
            this.btnExport.TabStop = false;
            this.btnExport.Text = "btnExport";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.ImageOptions.Image = global::AppClient.Properties.Resources.STATISTICS;
            this.btnExecute.Location = new System.Drawing.Point(185, 381);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(85, 22);
            this.btnExecute.StyleController = this.mainLayout;
            this.btnExecute.TabIndex = 5;
            this.btnExecute.Text = "btnExecute";
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.gpStatisticsParameters});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(280, 413);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // gpStatisticsParameters
            // 
            this.gpStatisticsParameters.CustomizationFormText = "gpStatisticsParameters";
            this.gpStatisticsParameters.HeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.gpStatisticsParameters.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.layoutControlItem3,
            this.layoutControlItem2});
            this.gpStatisticsParameters.Location = new System.Drawing.Point(0, 0);
            this.gpStatisticsParameters.Name = "gpStatisticsParameters";
            this.gpStatisticsParameters.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.gpStatisticsParameters.Size = new System.Drawing.Size(280, 413);
            this.gpStatisticsParameters.Text = "Điều kiện tìm kiếm";
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(264, 351);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 351);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(87, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnExport;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(87, 346);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(88, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(88, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(88, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnExecute;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(175, 346);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(89, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(89, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(89, 26);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // ucStatisticsMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "ucStatisticsMaster";
            this.Size = new System.Drawing.Size(952, 413);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPages.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainLayout)).EndInit();
            this.mainLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpStatisticsParameters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.SplitContainerControl splitContainer;
        private DevExpress.XtraEditors.SimpleButton btnExecute;
        private DevExpress.XtraLayout.LayoutControl mainLayout;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraEditors.ImageComboBoxEdit cboPages;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView gvMain;
        private DevExpress.XtraLayout.LayoutControlGroup gpStatisticsParameters;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btnCopyStatus;
        private DevExpress.XtraEditors.LabelControl txtSearchStatus;
        private DevExpress.XtraEditors.SimpleButton btnBestFitColumns;
        private System.Windows.Forms.ContextMenuStrip mnuSort;
        private System.Windows.Forms.ToolStripMenuItem btnAscending;
        private System.Windows.Forms.ToolStripMenuItem btnDescending;
        private System.Windows.Forms.ToolStripMenuItem btnNoSort;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
