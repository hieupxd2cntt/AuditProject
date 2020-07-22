using System;
using System.Drawing;
using System.ServiceModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Core.Common;
using Core.Controllers;
using Core.Entities;
using Core.Utils;
using AppClient.Utils;
using Core;
using System.Net;
using System.Net.NetworkInformation;

namespace AppClient.Controls
{
    public partial class ucLogin : ucModule
    {
        string IPAddress = string.Empty;
        string macAddress = string.Empty;
        public ucLogin()
        {
            InitializeComponent();
            GetIPAddress();
            GetMacAddress();
        }

        protected override void InitializeModuleData()
        {
            base.InitializeModuleData();            
            var clientEnvironment = (App.Environment as ClientEnvironment);
            if(clientEnvironment != null)
            {
                //var appRegistry = clientEnvironment.CoreRegistry;
                //txtUsername.EditValue = appRegistry.GetValue("UserName", string.Empty);
                //txtPassword.EditValue = appRegistry.GetValue("Password", string.Empty);
                //chkSaveLogin.Checked = (string)appRegistry.GetValue("SaveLogin", "N") == "Y";
                //if(chkSaveLogin.Checked)
                //{
                //    btnLogin.Focus();
                //    ActiveControl = btnLogin;
                //}
            }            
        }

        public override void InitializeLayout()
        {
            base.InitializeLayout();
            var parentForm = Parent as XtraForm;

            if (parentForm != null)
            {
                parentForm.FormBorderStyle = FormBorderStyle.None;
                //BackgroundImage = Image.FromFile("Theme\\Login.png");
                parentForm.ClientSize = new System.Drawing.Size(400, 450);
                //btnLogin.BackgroundImage = Image.FromFile("Theme\\btnLoginSubmit.png");
                //btnClose.BackgroundImage = Image.FromFile("Theme\\btnLoginClose.png");
            }
        }

        public override void ShowModule(IWin32Window owner)
        {
            // terminal last session
            try
            {
                var clientInfos = CachedUtils.GetCacheOf<ClientInfo>("LastSession");
                if (clientInfos.Count > 0)
                {
                    App.Environment.ClientInfo.SessionKey = clientInfos[0].SessionKey;
                    using (var ctrlSA = new SAController())
                    {
                        ctrlSA.TerminalCurrentSession();
                    }
                }
            }
            catch
            {
            }
            // login
            var frmOwner = (XtraForm)Parent;
            InitializeModuleData();

            App.Environment.ClientInfo.SessionKey = null;
            frmOwner.ShowDialog(owner);

            if (App.Environment.ClientInfo.SessionKey == null)
            {
                MainProcess.Close();
            }            
        }
        private void ExecuteLogin(string strUser,string strPass)
        {
            try
            {
                using (var ctrlSA = new SAController())
                {
                    Session session;                    
                    ctrlSA.CreateUserSession(out session, strUser, strPass, IPAddress, macAddress);
                    MainProcess.LoginToSystem(session);                    
                    //MainProcess.SaveLoginToRegistry(chkSaveLogin.Checked, strUser, strPass);
                    
                }
                CloseModule();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (var ctrlSA = new SAController())
                {
                    Session session;                              
                    ctrlSA.CreateUserSession(out session, txtUsername.Text, txtPassword.Text, IPAddress, macAddress);                                                           
                    MainProcess.LoginToSystem(session);
                    //MainProcess.SaveLoginToRegistry(chkSaveLogin.Checked, txtUsername.Text, txtPassword.Text);

                    if (session.ChkLog == 1)
                    {
                        frmInfo.ShowWarning("Error System", "Tài khoản của bạn đăng nhập lần đầu hoặc mật khẩu của bạn đã hết hạn. \n Bạn nên thay đổi mật khẩu để đảm bảo an toàn bảo mật khi sử dụng hệ thống !", this);                    
                        MainProcess.ExecuteModule("02913", "MAD");
                    }                    
                }
                CloseModule();
            }
            catch (Exception ex)
            {
                ActiveControl = txtUsername;
                txtPassword.Text = "";
                //txtUsername.Focus();
                txtPassword.Focus();
                ShowError(ex);
            }
            //--End trungTT
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseModule();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    using (var ctrlSA = new SAController())
                    {
                        Session session;                        
                        ctrlSA.CreateUserSession(out session, txtUsername.Text, txtPassword.Text,IPAddress, macAddress);
                        MainProcess.LoginToSystem(session);
                        if (session.ChkLog == 1)
                        {                            
                            frmInfo.ShowWarning("Error System", "Tài khoản của bạn đăng nhập lần đầu hoặc mật khẩu của bạn đã hết hạn. \n Bạn nên thay đổi mật khẩu để đảm bảo an toàn bảo mật khi sử dụng hệ thống !", this);
                            MainProcess.ExecuteModule("02913", "MAD");
                        }    
                    }
                    CloseModule();
                }
                catch (Exception ex)
                {
                    ActiveControl = txtUsername;
                    txtPassword.Text = "";
                    //txtUsername.Focus();
                    txtPassword.Focus();
                    ShowError(ex);
                }
            }
        }

        public void GetIPAddress()
        {
            IPHostEntry Host = default(IPHostEntry);
            var Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
        }

        private void GetMacAddress()
        {            
           
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {                                              
                if (nic.OperationalStatus == OperationalStatus.Up && !nic.Description.Contains("Virtual"))
                {
                    if (!string.IsNullOrEmpty(nic.GetPhysicalAddress().ToString()))
                    {
                        macAddress = nic.GetPhysicalAddress().ToString();
                    }
                }

            }
           
        }
        private void chkSaveLogin_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ucLogin_Load(object sender, EventArgs e)
        {

        }

        #region Hover Control Fluent Design
        //private void labelControl1_MouseHover(object sender, EventArgs e)
        //{
        //    labelControl1.ForeColor = Color.White;
        //}

        //private void labelControl1_MouseLeave(object sender, EventArgs e)
        //{
        //    labelControl1.ForeColor = Color.Black;
        //}

        //private void btnLogin_MouseHover(object sender, EventArgs e)
        //{
        //    btnLogin.BackColor = Color.LightGray;
        //}

        //private void btnLogin_MouseLeave(object sender, EventArgs e)
        //{
        //    btnLogin.BackColor = Color.LightSkyBlue;
        //}

        //private void btnClose_MouseHover(object sender, EventArgs e)
        //{
        //    btnClose.BackColor = Color.LightGray;
        //}

        //private void btnClose_MouseLeave(object sender, EventArgs e)
        //{
        //    btnClose.BackColor = Color.LightSkyBlue;
        //}
        #endregion
    }
}
