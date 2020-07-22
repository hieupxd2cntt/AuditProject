namespace AppClient.Controls
{
    partial class ucSendMail
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
            this.lbTitle = new DevExpress.XtraEditors.LabelControl();
            this.mainLayoutGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.mainLayout = new DevExpress.XtraLayout.LayoutControl();
            this.lnkFile = new DevExpress.XtraEditors.HyperLinkEdit();
            this.btnSendMail = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainLayoutGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainLayout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkFile.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
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
            this.lbTitle.Size = new System.Drawing.Size(456, 47);
            this.lbTitle.TabIndex = 5;
            this.lbTitle.Text = "lbTitle";
            // 
            // mainLayoutGroup
            // 
            this.mainLayoutGroup.CustomizationFormText = "sendMailLayoutGroup";
            this.mainLayoutGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.mainLayoutGroup.GroupBordersVisible = false;
            this.mainLayoutGroup.Name = "mainLayoutGroup";
            this.mainLayoutGroup.Size = new System.Drawing.Size(456, 318);
            this.mainLayoutGroup.TextVisible = false;
            // 
            // mainLayout
            // 
            this.mainLayout.AllowCustomization = false;
            this.mainLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainLayout.Location = new System.Drawing.Point(0, 47);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(494, 160, 250, 350);
            this.mainLayout.Root = this.mainLayoutGroup;
            this.mainLayout.Size = new System.Drawing.Size(456, 318);
            this.mainLayout.TabIndex = 6;
            this.mainLayout.Text = "layoutControl1";
            // 
            // lnkFile
            // 
            this.lnkFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkFile.Location = new System.Drawing.Point(12, 374);
            this.lnkFile.Name = "lnkFile";
            this.lnkFile.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lnkFile.Properties.Appearance.Options.UseBackColor = true;
            this.lnkFile.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lnkFile.Properties.Image = global::AppClient.Properties.Resources.ATTACH;
            this.lnkFile.Properties.ImageAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lnkFile.Size = new System.Drawing.Size(20, 20);
            this.lnkFile.TabIndex = 104;
            this.lnkFile.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.lnkFile_OpenLink);
            // 
            // btnSendMail
            // 
            this.btnSendMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendMail.Appearance.Image = global::AppClient.Properties.Resources.MAIL;
            this.btnSendMail.Appearance.Options.UseImage = true;
            this.btnSendMail.ImageOptions.Image = global::AppClient.Properties.Resources.SEND_MAIL;
            this.btnSendMail.Location = new System.Drawing.Point(369, 371);
            this.btnSendMail.Name = "btnSendMail";
            this.btnSendMail.Size = new System.Drawing.Size(75, 23);
            this.btnSendMail.TabIndex = 7;
            this.btnSendMail.Text = "Gửi Mail";
            this.btnSendMail.Click += new System.EventHandler(this.btnSendMail_Click);
            // 
            // ucSendMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lnkFile);
            this.Controls.Add(this.btnSendMail);
            this.Controls.Add(this.mainLayout);
            this.Controls.Add(this.lbTitle);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ucSendMail";
            this.Size = new System.Drawing.Size(456, 407);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainLayoutGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainLayout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lnkFile.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lbTitle;
        private DevExpress.XtraEditors.SimpleButton btnSendMail;
        private DevExpress.XtraLayout.LayoutControlGroup mainLayoutGroup;
        private DevExpress.XtraLayout.LayoutControl mainLayout;
        private DevExpress.XtraEditors.HyperLinkEdit lnkFile;
    }
}
