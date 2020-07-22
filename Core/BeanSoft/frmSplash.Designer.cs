namespace AppClient
{
    partial class frmSplash
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
            this.lbVersion = new DevExpress.XtraEditors.LabelControl();
            this.lbAction = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // lbVersion
            // 
            this.lbVersion.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbVersion.Appearance.Options.UseForeColor = true;
            this.lbVersion.Appearance.Options.UseTextOptions = true;
            this.lbVersion.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lbVersion.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lbVersion.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbVersion.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbVersion.Location = new System.Drawing.Point(0, 0);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Padding = new System.Windows.Forms.Padding(0, 10, 40, 0);
            this.lbVersion.Size = new System.Drawing.Size(470, 23);
            this.lbVersion.TabIndex = 0;
            this.lbVersion.Text = "Version {0}";
            // 
            // lbAction
            // 
            this.lbAction.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbAction.Appearance.Options.UseForeColor = true;
            this.lbAction.Appearance.Options.UseTextOptions = true;
            this.lbAction.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lbAction.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Bottom;
            this.lbAction.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbAction.Location = new System.Drawing.Point(0, 23);
            this.lbAction.Name = "lbAction";
            this.lbAction.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lbAction.Size = new System.Drawing.Size(470, 14);
            this.lbAction.TabIndex = 1;
            // 
            // frmSplash
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(470, 313);
            this.Controls.Add(this.lbAction);
            this.Controls.Add(this.lbVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSplash";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lbVersion;
        private DevExpress.XtraEditors.LabelControl lbAction;
    }
}