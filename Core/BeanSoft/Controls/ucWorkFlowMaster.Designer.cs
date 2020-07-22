namespace AppClient.Controls
{
    partial class ucWorkFlowMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucWorkFlowMaster));
            this.mainLayout = new DevExpress.XtraLayout.LayoutControl();
            this.mainLayoutGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lbTitle = new DevExpress.XtraEditors.LabelControl();
            this.btnCommit = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.TopPanel = new DevExpress.XtraEditors.SidePanel();
            this.leftPanel = new DevExpress.XtraEditors.SidePanel();
            this.acdWorkFlowMenu = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.BottomPanel = new DevExpress.XtraEditors.SidePanel();
            this.btnPrev = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.ContentPanel = new DevExpress.XtraEditors.SidePanel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainLayout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainLayoutGroup)).BeginInit();
            this.TopPanel.SuspendLayout();
            this.leftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.acdWorkFlowMenu)).BeginInit();
            this.BottomPanel.SuspendLayout();
            this.ContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.AllowCustomization = false;
            this.mainLayout.Appearance.ControlFocused.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.mainLayout.Appearance.ControlFocused.Options.UseBorderColor = true;
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.LookAndFeel.UseDefaultLookAndFeel = false;
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.OptionsCustomizationForm.ShowLoadButton = false;
            this.mainLayout.OptionsCustomizationForm.ShowPropertyGrid = true;
            this.mainLayout.OptionsCustomizationForm.ShowSaveButton = false;
            this.mainLayout.OptionsFocus.AllowFocusReadonlyEditors = false;
            this.mainLayout.OptionsSerialization.RestoreAppearanceItemCaption = false;
            this.mainLayout.OptionsSerialization.RestoreAppearanceTabPage = true;
            this.mainLayout.OptionsSerialization.RestoreGroupPadding = true;
            this.mainLayout.OptionsSerialization.RestoreGroupSpacing = true;
            this.mainLayout.OptionsSerialization.RestoreLayoutGroupAppearanceGroup = true;
            this.mainLayout.OptionsSerialization.RestoreLayoutItemPadding = true;
            this.mainLayout.OptionsSerialization.RestoreLayoutItemSpacing = true;
            this.mainLayout.OptionsSerialization.RestoreRootGroupPadding = true;
            this.mainLayout.OptionsSerialization.RestoreRootGroupSpacing = true;
            this.mainLayout.OptionsSerialization.RestoreTabbedGroupPadding = true;
            this.mainLayout.OptionsSerialization.RestoreTabbedGroupSpacing = true;
            this.mainLayout.OptionsSerialization.RestoreTextToControlDistance = true;
            this.mainLayout.OptionsView.HighlightFocusedItem = true;
            this.mainLayout.Root = this.mainLayoutGroup;
            this.mainLayout.Size = new System.Drawing.Size(586, 377);
            this.mainLayout.TabIndex = 101;
            this.mainLayout.Text = "layoutControl1";
            // 
            // mainLayoutGroup
            // 
            this.mainLayoutGroup.CustomizationFormText = "layoutControlGroup1";
            this.mainLayoutGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.mainLayoutGroup.GroupBordersVisible = false;
            this.mainLayoutGroup.Name = "mainLayoutGroup";
            this.mainLayoutGroup.OptionsItemText.TextToControlDistance = 5;
            this.mainLayoutGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.mainLayoutGroup.Size = new System.Drawing.Size(586, 377);
            this.mainLayoutGroup.TextVisible = false;
            // 
            // lbTitle
            // 
            this.lbTitle.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold);
            this.lbTitle.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbTitle.Appearance.Options.UseBackColor = true;
            this.lbTitle.Appearance.Options.UseFont = true;
            this.lbTitle.Appearance.Options.UseForeColor = true;
            this.lbTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTitle.Location = new System.Drawing.Point(0, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.lbTitle.Size = new System.Drawing.Size(760, 55);
            this.lbTitle.TabIndex = 1;
            this.lbTitle.Text = "lbTitle";
            // 
            // btnCommit
            // 
            this.btnCommit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCommit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCommit.ImageOptions.Image")));
            this.btnCommit.Location = new System.Drawing.Point(397, 4);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(86, 23);
            this.btnCommit.TabIndex = 99;
            this.btnCommit.Text = "btnCommit";
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.ImageOptions.Image")));
            this.btnClose.Location = new System.Drawing.Point(489, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(83, 23);
            this.btnClose.TabIndex = 100;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.lbTitle);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(760, 56);
            this.TopPanel.TabIndex = 102;
            this.TopPanel.Text = "TopPanel";
            // 
            // leftPanel
            // 
            this.leftPanel.Controls.Add(this.acdWorkFlowMenu);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 56);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(174, 410);
            this.leftPanel.TabIndex = 103;
            this.leftPanel.Text = "sidePanel2";
            // 
            // acdWorkFlowMenu
            // 
            this.acdWorkFlowMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acdWorkFlowMenu.Location = new System.Drawing.Point(0, 0);
            this.acdWorkFlowMenu.Name = "acdWorkFlowMenu";
            this.acdWorkFlowMenu.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Hidden;
            this.acdWorkFlowMenu.Size = new System.Drawing.Size(173, 410);
            this.acdWorkFlowMenu.TabIndex = 0;
            this.acdWorkFlowMenu.Text = "acdWorkFlowMenu";
            this.acdWorkFlowMenu.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // BottomPanel
            // 
            this.BottomPanel.BorderThickness = 0;
            this.BottomPanel.Controls.Add(this.btnPrev);
            this.BottomPanel.Controls.Add(this.btnNext);
            this.BottomPanel.Controls.Add(this.btnClose);
            this.BottomPanel.Controls.Add(this.btnCommit);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(174, 433);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(586, 33);
            this.BottomPanel.TabIndex = 104;
            this.BottomPanel.Text = "BottomPanel";
            // 
            // btnPrev
            // 
            this.btnPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrev.ImageOptions.Image = global::AppClient.Properties.Resources.prev_16x16;
            this.btnPrev.Location = new System.Drawing.Point(6, 6);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(77, 23);
            this.btnPrev.TabIndex = 102;
            this.btnPrev.Text = "Previous";
            this.btnPrev.Click += new System.EventHandler(this.BtnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNext.ImageOptions.Image = global::AppClient.Properties.Resources.next_16x16;
            this.btnNext.Location = new System.Drawing.Point(87, 6);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(77, 23);
            this.btnNext.TabIndex = 101;
            this.btnNext.Text = "Next";
            this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.mainLayout);
            this.ContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContentPanel.Location = new System.Drawing.Point(174, 56);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(586, 377);
            this.ContentPanel.TabIndex = 105;
            this.ContentPanel.Text = "ContentPanel";
            // 
            // ucWorkFlowMaster
            // 
            this.AcceptButton = this.btnCommit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.ContentPanel);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.TopPanel);
            this.Name = "ucWorkFlowMaster";
            this.Size = new System.Drawing.Size(760, 466);
            this.ModuleClosing += new System.ComponentModel.CancelEventHandler(this.ucMaintain_ModuleClosing);
            this.Load += new System.EventHandler(this.ucWorkFlowMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainLayout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainLayoutGroup)).EndInit();
            this.TopPanel.ResumeLayout(false);
            this.leftPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.acdWorkFlowMenu)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            this.ContentPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCommit;
        private DevExpress.XtraEditors.LabelControl lbTitle;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlGroup mainLayoutGroup;
        public DevExpress.XtraLayout.LayoutControl mainLayout;
        private DevExpress.XtraEditors.SidePanel TopPanel;
        private DevExpress.XtraEditors.SidePanel leftPanel;
        private DevExpress.XtraEditors.SidePanel BottomPanel;
        private DevExpress.XtraEditors.SidePanel ContentPanel;
        private DevExpress.XtraBars.Navigation.AccordionControl acdWorkFlowMenu;
        private DevExpress.XtraEditors.SimpleButton btnPrev;
        private DevExpress.XtraEditors.SimpleButton btnNext;
    }
}
