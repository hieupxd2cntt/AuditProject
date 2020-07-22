using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using AppClient.Utils;
using Core.Common;

namespace AppClient
{
	/// <summary>
    /// Summary description for frmAlert.
	/// </summary>
    public partial class frmAlert : XtraForm
    {
		private System.ComponentModel.IContainer components;
		int X=0;
        private RichTextBox lstInput;
        int Y = 0;

        public void InputText(string txtInput )
        {
            lstInput.Text += "\n" + txtInput;
        }
        public frmAlert()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code 
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAlert));
            this.lstInput = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lstInput
            // 
            this.lstInput.BackColor = System.Drawing.SystemColors.WindowText;
            this.lstInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstInput.ForeColor = System.Drawing.SystemColors.Window;
            this.lstInput.Location = new System.Drawing.Point(0, 0);
            this.lstInput.Name = "lstInput";
            this.lstInput.Size = new System.Drawing.Size(427, 256);
            this.lstInput.TabIndex = 0;
            this.lstInput.Text = "";
            // 
            // frmAlert
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
            this.ClientSize = new System.Drawing.Size(427, 256);
            this.Controls.Add(this.lstInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmAlert.IconOptions.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAlert";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Javis";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

		}
		#endregion		
		
		private void Form1_Load(object sender, System.EventArgs e)
		{
			
            //X=Screen.GetWorkingArea(this).Width; 
            //Y=Screen.GetWorkingArea(this).Height; 
            //this.Location=new Point(X-this.Width,Y- this.Height);            			
		}
						

        private void lnkMessage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {            
            MainProcess.ExecuteModule("03271", "MMN");
        }

        protected override void OnLoad(EventArgs e)
        {
            var screen = Screen.FromPoint(this.Location);
            this.Location = new Point(screen.WorkingArea.Right - this.Width, screen.WorkingArea.Bottom - this.Height);
            base.OnLoad(e);
        }
        public void AppendText(string text, Color color, bool addNewLine = false)
        {
            lstInput.SuspendLayout();
            lstInput.SelectionColor = color;
            lstInput.AppendText(addNewLine
                ? $"{text}{Environment.NewLine}"
                : text);
            lstInput.ScrollToCaret();
            lstInput.ResumeLayout();
        }
    }
}
