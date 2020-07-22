namespace AppClient.Controls
{
    partial class ucUserRoleSetup
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
            this.roleTree = new DevExpress.XtraTreeList.TreeList();
            this.colRoleID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colRoleValue = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repoRoleValue = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.btnSetup = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.lbTitle = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.roleTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoRoleValue)).BeginInit();
            this.SuspendLayout();
            // 
            // roleTree
            // 
            this.roleTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.roleTree.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colRoleID,
            this.colName,
            this.colRoleValue});
            this.roleTree.KeyFieldName = "RoleID";
            this.roleTree.Location = new System.Drawing.Point(3, 56);
            this.roleTree.Name = "roleTree";
            this.roleTree.OptionsBehavior.AllowIndeterminateCheckState = true;
            this.roleTree.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.Single;
            this.roleTree.OptionsDragAndDrop.DropNodesMode = DevExpress.XtraTreeList.DropNodesMode.Advanced;
            this.roleTree.OptionsView.EnableAppearanceEvenRow = true;
            this.roleTree.OptionsView.EnableAppearanceOddRow = true;
            this.roleTree.OptionsView.ShowColumns = false;
            this.roleTree.OptionsView.ShowIndicator = false;
            this.roleTree.ParentFieldName = "CategoryID";
            this.roleTree.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoRoleValue});
            this.roleTree.Size = new System.Drawing.Size(692, 319);
            this.roleTree.TabIndex = 0;
            this.roleTree.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.roleTree_GetStateImage);
            this.roleTree.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.roleTree_CellValueChanged);
            // 
            // colRoleID
            // 
            this.colRoleID.AppearanceCell.Options.UseTextOptions = true;
            this.colRoleID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRoleID.AppearanceHeader.Options.UseTextOptions = true;
            this.colRoleID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRoleID.Caption = "Mã";
            this.colRoleID.FieldName = "RoleID";
            this.colRoleID.Name = "colRoleID";
            this.colRoleID.OptionsColumn.AllowEdit = false;
            this.colRoleID.OptionsColumn.AllowFocus = false;
            this.colRoleID.OptionsColumn.AllowMove = false;
            this.colRoleID.OptionsColumn.FixedWidth = true;
            this.colRoleID.Visible = true;
            this.colRoleID.VisibleIndex = 1;
            this.colRoleID.Width = 120;
            // 
            // colName
            // 
            this.colName.Caption = "Quyền thực thi";
            this.colName.FieldName = "TranslatedRoleName";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.OptionsColumn.AllowFocus = false;
            this.colName.OptionsColumn.AllowMove = false;
            this.colName.OptionsColumn.ReadOnly = true;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 471;
            // 
            // colRoleValue
            // 
            this.colRoleValue.AppearanceCell.Options.UseTextOptions = true;
            this.colRoleValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRoleValue.AppearanceHeader.Options.UseTextOptions = true;
            this.colRoleValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRoleValue.Caption = "#";
            this.colRoleValue.ColumnEdit = this.repoRoleValue;
            this.colRoleValue.FieldName = "RoleValue";
            this.colRoleValue.Name = "colRoleValue";
            this.colRoleValue.OptionsColumn.FixedWidth = true;
            this.colRoleValue.Visible = true;
            this.colRoleValue.VisibleIndex = 2;
            this.colRoleValue.Width = 80;
            // 
            // repoRoleValue
            // 
            this.repoRoleValue.AutoHeight = false;
            this.repoRoleValue.Name = "repoRoleValue";
            this.repoRoleValue.ValueChecked = "Y";
            this.repoRoleValue.ValueUnchecked = "N";
            this.repoRoleValue.EditValueChanged += new System.EventHandler(this.repoRoleValue_EditValueChanged);
            // 
            // btnSetup
            // 
            this.btnSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSetup.ImageOptions.Image = global::AppClient.Properties.Resources.SAVE;
            this.btnSetup.Location = new System.Drawing.Point(499, 381);
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.Size = new System.Drawing.Size(95, 23);
            this.btnSetup.TabIndex = 1;
            this.btnSetup.Text = "&Thiết lập";
            this.btnSetup.Click += new System.EventHandler(this.btnSetup_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.ImageOptions.Image = global::AppClient.Properties.Resources.HOME;
            this.btnClose.Location = new System.Drawing.Point(600, 381);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Đó&ng lại";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lbTitle
            // 
            this.lbTitle.AllowHtmlString = true;
            this.lbTitle.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 12.75F, System.Drawing.FontStyle.Bold);
            this.lbTitle.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbTitle.Appearance.Options.UseBackColor = true;
            this.lbTitle.Appearance.Options.UseFont = true;
            this.lbTitle.Appearance.Options.UseForeColor = true;
            this.lbTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTitle.Location = new System.Drawing.Point(0, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Padding = new System.Windows.Forms.Padding(20, 0, 20, 0);
            this.lbTitle.Size = new System.Drawing.Size(698, 50);
            this.lbTitle.TabIndex = 3;
            this.lbTitle.Text = "labelControl1";
            // 
            // ucUserRoleSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSetup);
            this.Controls.Add(this.roleTree);
            this.Name = "ucUserRoleSetup";
            this.Size = new System.Drawing.Size(698, 412);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.roleTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoRoleValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList roleTree;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repoRoleValue;
        private DevExpress.XtraEditors.SimpleButton btnSetup;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRoleValue;
        private DevExpress.XtraEditors.LabelControl lbTitle;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colRoleID;
    }
}
