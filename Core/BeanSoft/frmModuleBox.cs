using System;
using System.Windows.Forms;
using AppClient.Controls;
using AppClient.Utils;
using DevExpress.XtraEditors;

namespace AppClient
{
    public partial class frmModuleBox : DevExpress.XtraEditors.XtraForm
    {
        public ucModule ucModule { get; set; }
        public bool CanUserClose { get; set; }

        public frmModuleBox()
        {
            InitializeComponent();
            CanUserClose = true;          
        }

        private void frmModuleBox_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                MinimizeToJobQueue();
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                this.FormBorderEffect = FormBorderEffect.Shadow;                
            }
            catch
            {
            }
        }

        private void MinimizeToJobQueue()
        {
            if(ucModule.ucPreview == null)
            {
                ucModule.ucPreview = new ucModulePreview { Module = ucModule, ModuleForm = this };

                ShowInTaskbar = true;
                Hide();

                MainProcess.AddModulePreview(ucModule.ucPreview);
            }
        }

        private void frmModuleBox_Shown(object sender, EventArgs e)
        {
            Activate();
        }

        private void frmModuleBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!IsDisposed)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    e.Cancel = true;
                    if (CanUserClose)
                    {
                        ucModule.CloseModule();
                        return;
                    }

                    MinimizeToJobQueue();
                }
            }
        }
    }
}