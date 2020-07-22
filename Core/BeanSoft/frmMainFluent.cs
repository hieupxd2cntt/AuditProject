using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;
using AppClient.Controls;
using AppClient.Interface;
using AppClient.Utils;
using Core.Base;
using Core.Common;
using Core.Controllers;
using Core.Entities;
using Core.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;

namespace AppClient
{
    public partial class frmMainFluent : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm, IMain
    {
        internal MainProcess Process { get; set; }

        MainProcess IMain.Process => throw new NotImplementedException();

        IMainLanguage IMain.Language => throw new NotImplementedException();

        public frmMainFluent()
        {
            InitializeComponent();
            mainMenu.Visible = false;
            if (!DesignMode)
            {
                InitializeLanguage();
                //InitializeMenu();                
            }
            Process = new MainProcess(this);

            Application.Idle += Application_Idle;
            Application.ApplicationExit += Application_ApplicationExit;
        }
        private void LoadStatusBar()
        {

            StatusBar.BeginUpdate();

            BarButtonItem barButtonItem = new BarButtonItem();
            BarButtonItem barButtonItemUser = new BarButtonItem();
            User userInfo = new User();
            DataContainer container;
            string strSysDate = null;
            using (SAController ctrlSA = new SAController())
            {
                ctrlSA.GetSysDate(out container);
                var dsResult = container.DataSet;
                strSysDate = "Working Date: " + dsResult.Tables[0].Rows[0][0].ToString();
            }

            barButtonItemUser.Caption = "User: " + App.Environment.ClientInfo.UserName;
            barButtonItemUser.Glyph = (Image)ThemeUtils.Image16.Images["ACCOUNT"];
            barButtonItemUser.PaintStyle = BarItemPaintStyle.CaptionGlyph;

            barButtonItem.Caption = strSysDate;
            barButtonItem.Glyph = (Image)ThemeUtils.Image16.Images["SYSTIME"];
            barButtonItem.PaintStyle = BarItemPaintStyle.CaptionGlyph;

            barButtonItem.Alignment = BarItemLinkAlignment.Right;

            StatusBar.AddItem(barButtonItemUser);
            StatusBar.AddItem(barButtonItem);

            StatusBar.EndUpdate();
        }
        void IMain.OnLogout()
        {

        }

        public void InitializeMenu()
        {
            ((IMain)this).InitializeMenu();
            ShowFlyMenu("2");
            ShowAlert();
        }

        private void ShowAlert()
        {
            Message msg = new Message();
            alertControl.Show(this, msg.Caption, msg.Text, "", msg.Image, msg);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = Language.ApplicationTitle;
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            frmSplash.CloseForm();
            Activate();
            MainProcess.ShowLogin();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (App.Environment.ClientInfo.SessionKey == null || frmConfirm.ShowConfirm(Language.ExitTitle, Language.ExitConfirm, this))
            {
                MainProcess.LogoutFromSystem(false);
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
        protected override void DestroyHandle()
        {
            base.DestroyHandle();
            MainProcess.LogoutFromSystem(false);
        }

        #region Implemention
        void IMain.InitializeMenu()
        {
            mainMenu.Visible = true;
            var dicButtonItems = new Dictionary<string, object>();
            List<RibbonItemInfo> colRibbonItemsInfo;

            using (var ctrlSA = new SAController())
            {
                ctrlSA.ListRibbonItems(out colRibbonItemsInfo);
            }

            var mainMenuInfos = (from item in colRibbonItemsInfo
                                 where item.Type == "1"
                                 select item).ToList();

            foreach (var item in mainMenuInfos)
            {
                if (item.RibbonType == Core.CODES.DEFRIBBON.RIBTYPE.RIBBON_PAGE)
                {
                    //var ribbonPage = new RibbonPage
                    //{
                    //    Name = item.RibbonName,
                    //    Text = LangUtils.Translate(LangType.MENU_CAPTION, item.RibbonName),

                    //    Tag = item,
                    //    Visible = false
                    //};

                    var root = new AccordionControlElement()
                    {
                        Style = ElementStyle.Group,
                        Name = item.RibbonName,
                        Expanded = true,
                        Text = LangUtils.Translate(LangType.MENU_CAPTION, item.RibbonName),
                        Image = LangUtils.Get16x16Image(LangType.MENU_CAPTION, item.RibbonName),
                    };

                    mainMenu.Elements.Add(root);
                    //ribbon.Pages.Add(ribbonPage);
                    dicButtonItems.Add(item.RibbonID, root);
                }
                else if (item.RibbonType == Core.CODES.DEFRIBBON.RIBTYPE.RIBBON_GROUP)
                {
                    var menuGroup = new AccordionControlElement()
                    {
                        Style = ElementStyle.Group,
                        Name = item.RibbonName,
                        Expanded = true,
                        Text = LangUtils.Translate(LangType.MENU_CAPTION, item.RibbonName),
                        Image = LangUtils.Get16x16Image(LangType.MENU_CAPTION, item.RibbonName),
                    };

                    //mainMenu.Elements.Add(menuGroup);
                    dicButtonItems.Add(item.RibbonID, menuGroup);
                }
                else if (item.RibbonType == Core.CODES.DEFRIBBON.RIBTYPE.BUTTON48 ||
                    item.RibbonType == Core.CODES.DEFRIBBON.RIBTYPE.BUTTON16 ||
                    item.RibbonType == Core.CODES.DEFRIBBON.RIBTYPE.SUB_BUTTON16
                    )
                {
                    var elementItem = new AccordionControlElement()
                    {
                        Style = ElementStyle.Item,
                        Name = item.RibbonName,
                        Expanded = true,
                        Text = LangUtils.Translate(LangType.MENU_CAPTION, item.RibbonName),
                        Image = LangUtils.Get16x16Image(LangType.MENU_CAPTION, item.RibbonName),
                        Tag = item
                    };

                    dicButtonItems.Add(item.RibbonID, elementItem);
                }
            }

            foreach (var item in mainMenuInfos)
            {

                if (item.RibbonType == Core.CODES.DEFRIBBON.RIBTYPE.RIBBON_GROUP)
                {
                    var child = dicButtonItems[item.RibbonID] as AccordionControlElement;
                    var parent = dicButtonItems[item.RibbonOwnerID] as AccordionControlElement;
                    if (child != null)
                    {
                        parent.Elements.Add(child);
                    }
                }
            }

            foreach (var item in mainMenuInfos)
            {

                if (item.RibbonType == Core.CODES.DEFRIBBON.RIBTYPE.BUTTON48 ||
                    item.RibbonType == Core.CODES.DEFRIBBON.RIBTYPE.BUTTON16
                    )
                {
                    var child = dicButtonItems[item.RibbonID] as AccordionControlElement;
                    var parent = dicButtonItems[item.RibbonOwnerID] as AccordionControlElement;
                    if (parent != null && child != null)
                    {
                        parent.Elements.Add(child);
                    }
                }
            }

            mainMenu.CollapseAll();
        }

        void IMain.ApplyMenu()
        {
            InitializeMenu();
            LoadStatusBar();
            Startup();
        }

        void IMain.StartupModules()
        {
            Process.StartupModules();
        }

        XtraTabPage IMain.AddTabModule(XtraTabPage tabPage)
        {
            throw new NotImplementedException();
        }

        void IMain.SelectTabPage(XtraTabPage tabPage)
        {
            throw new NotImplementedException();
        }

        void IMain.RemoveTabModule(XtraTabPage tabPage)
        {
            throw new NotImplementedException();
        }

        void IMain.AddModulePreview(ucModulePreview preview)
        {
            throw new NotImplementedException();
        }

        void IMain.RemoveModulePreview(ucModulePreview preview)
        {
            throw new NotImplementedException();
        }

        void IMain.RegisterButton(BarButtonItem button)
        {
            throw new NotImplementedException();
        }

        void IMain.CancelRegisterButton(BarButtonItem button)
        {
            throw new NotImplementedException();
        }

        private void txtModuleExec_ItemClick(object sender, ItemClickEventArgs e)
        {
            //#if DEBUG                        
            //if (txtModuleID.EditValue != null)
            //{
            //    var moduleID = txtModuleID.EditValue.ToString().ToUpper();
            //    txtModuleID.EditValue = null;
            //    MainProcess.ExecuteModule(moduleID);
            //}
            //#endif
        }
        #endregion

        void ShowFlyMenu(string ribbonType)
        {
            var dicButtonItems = new Dictionary<string, object>();
            List<RibbonItemInfo> colRibbonItemsInfo;

            using (var ctrlSA = new SAController())
            {
                ctrlSA.ListRibbonItems(out colRibbonItemsInfo);
            }

            var flyMenuInfos = (from item in colRibbonItemsInfo
                                where item.Type == ribbonType
                                select item).ToList();

            foreach (var item in flyMenuInfos)
            {
                if (item.RibbonType == Core.CODES.DEFRIBBON.RIBTYPE.RIBBON_PAGE)
                {
                    var root = new AccordionControlElement()
                    {
                        Style = ElementStyle.Group,
                        Name = item.RibbonName,
                        Expanded = true,
                        Text = LangUtils.Translate(LangType.MENU_CAPTION, item.RibbonName),
                        Image = LangUtils.Get16x16Image(LangType.MENU_CAPTION, item.RibbonName),
                    };

                    flyMenu.Elements.Add(root);
                    //ribbon.Pages.Add(ribbonPage);
                    dicButtonItems.Add(item.RibbonID, root);
                }
                else if (item.RibbonType == Core.CODES.DEFRIBBON.RIBTYPE.RIBBON_GROUP)
                {
                    var menuGroup = new AccordionControlElement()
                    {
                        Style = ElementStyle.Group,
                        Name = item.RibbonName,
                        Expanded = true,
                        Text = LangUtils.Translate(LangType.MENU_CAPTION, item.RibbonName),
                        Image = LangUtils.Get16x16Image(LangType.MENU_CAPTION, item.RibbonName),
                    };

                    //mainMenu.Elements.Add(menuGroup);
                    dicButtonItems.Add(item.RibbonID, menuGroup);
                }
                else if (item.RibbonType == Core.CODES.DEFRIBBON.RIBTYPE.BUTTON48 ||
                    item.RibbonType == Core.CODES.DEFRIBBON.RIBTYPE.BUTTON16 ||
                    item.RibbonType == Core.CODES.DEFRIBBON.RIBTYPE.SUB_BUTTON16
                    )
                {
                    var elementItem = new AccordionControlElement()
                    {
                        Style = ElementStyle.Item,
                        Name = item.RibbonName,
                        Expanded = true,
                        Text = LangUtils.Translate(LangType.MENU_CAPTION, item.RibbonName),
                        Image = LangUtils.Get16x16Image(LangType.MENU_CAPTION, item.RibbonName),
                        Tag = item
                    };

                    dicButtonItems.Add(item.RibbonID, elementItem);
                }
            }

            foreach (var item in flyMenuInfos)
            {

                if (item.RibbonType == Core.CODES.DEFRIBBON.RIBTYPE.RIBBON_GROUP)
                {
                    var child = dicButtonItems[item.RibbonID] as AccordionControlElement;
                    var parent = dicButtonItems[item.RibbonOwnerID] as AccordionControlElement;
                    if (child != null)
                    {
                        parent.Elements.Add(child);
                    }
                }
            }

            foreach (var item in flyMenuInfos)
            {

                if (item.RibbonType == Core.CODES.DEFRIBBON.RIBTYPE.BUTTON48 ||
                    item.RibbonType == Core.CODES.DEFRIBBON.RIBTYPE.BUTTON16
                    )
                {
                    var child = dicButtonItems[item.RibbonID] as AccordionControlElement;
                    var parent = dicButtonItems[item.RibbonOwnerID] as AccordionControlElement;
                    if (parent != null && child != null)
                    {
                        parent.Elements.Add(child);
                    }
                }
            }

            flyMenu.CollapseAll();
        }

        private void txtModuleExec_EditValueChanged(object sender, EventArgs e)
        {
            //#if DEBUG                        
            if (txtModuleExec.EditValue != null)
            {
                var moduleID = txtModuleExec.EditValue.ToString().ToUpper();
                txtModuleExec.EditValue = null;
                if (moduleID.StartsWith("!"))
                {
                    moduleID = moduleID.Substring(1);
                    if (moduleID.StartsWith("03"))
                    {
                        AddControltoContrainer(moduleID, true);
                    }
                    else if (moduleID.StartsWith("09"))
                    {
                        AddControltoContrainer(moduleID, false);
                    }
                    else
                    {
                        MainProcess.ForceLoad(moduleID);
                        MainProcess.ExecuteModule(moduleID);
                    }
                }
                else if (moduleID.StartsWith("03"))
                {
                    AddControltoContrainer(moduleID, false);
                }
                else
                {
                    MainProcess.ExecuteModule(moduleID);
                }
            }
            //#endif
        }

        private void AddControltoContrainer(string moduleID, Boolean foreload)
        {
            if (foreload) MainProcess.ForceLoad(moduleID);

            var module = MainProcess.CreateModuleInstance(moduleID);
            container.Show();
            if (!container.Controls.Contains(module))
            {
                container.Controls.Clear();
                container.Controls.Add(module);
                module.Dock = DockStyle.Fill;
            }
            module.BringToFront();
        }

        private void mainMenu_ElementClick(object sender, ElementClickEventArgs e)
        {
            try
            {
                if (e.Element.Style == ElementStyle.Group) return;
                if (e.Element.Name == null) return;
                ResetBackground();
                e.Element.Appearance.Normal.BackColor = Color.LightGray;
                AddControltoContrainer(((RibbonItemInfo)e.Element.Tag).ModuleID, false);

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
        private void ResetBackground()
        {
            mainMenu.ForEachElement((el) => {
                el.Appearance.Normal.BackColor = ThemeUtils.BackTitleColor;
            }
            );
        }
        private delegate void CallModuleInvoker();
        private void AddControl()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CallModuleInvoker(this.AddControl));
            }
            else
            {
                var module = MainProcess.CreateModuleInstance("03901");
                //module.Execute();
                if (!container.Controls.Contains(module))
                {
                    container.Controls.Add(module);
                    module.Dock = DockStyle.Fill;
                    module.BringToFront();
                }
                module.BringToFront();
            }
        }


        public static void ExecuteMenu(string menuName, Core.Entities.RibbonItemInfo ribbonInfo)
        {
            //if (ribbonInfo != null)
            //{
            //    Instance._ExecuteMenu(menuName, ribbonInfo.ModuleID, ribbonInfo.SubModule);
            //}
            //else
            //{
            //    ExecuteMenu(menuName);
            //}
        }

        private void Startup()
        {
            if (App.Environment.ClientInfo.SessionKey != null)
            {
                //ThemeUtils.ChangeSkin(App.Environment.ClientInfo.UserProfile.ApplicationSkinName);

                foreach (var module in AllCaches.ModulesInfo)
                {
                    if (module.StartMode == Core.CODES.DEFMOD.STARTMODE.AUTOMATIC)
                    {
                        try
                        {
                            using (var ctrlSA = new SAController())
                            {
                                AddControltoContrainer(module.ModuleID, false);
                            }
                        }
                        catch (FaultException ex)
                        {
                            if (ex.Code.Name != ERR_SYSTEM.ERR_SYSTEM_MODULE_NOT_ALLOW_ACCESS.ToString())
                            {
                                frmInfo.ShowError(Language.ApplicationTitle, ex, this);
                            }
                        }
                        catch (Exception ex)
                        {
                            frmInfo.ShowError(Language.ApplicationTitle, ErrorUtils.CreateErrorWithSubMessage(ERR_SYSTEM.ERR_SYSTEM_UNKNOWN, ex.Message), this);
                        }
                    }
                }
            }
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            flyoutPanel1.ShowPopup();
        }

        #region LockScreen

        Boolean _ignore_action;
        DateTime _last_time_idle;
        Boolean _lockActive;
        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            Application.Idle -= Application_Idle;
        }
        private void Application_Idle(object sender, EventArgs e)
        {
            //check if the user is logged or the application is locked
            //if (LoginController.getInstance().OnlineUser != null && !_lockActive)
            //{
            //capture current time
            DateTime _now = DateTime.Now;
            TimeSpan _timeSpan = _now - _last_time_idle;


            //check elapsed time
            if (_timeSpan.TotalMinutes > 10)
            {
                _lockActive = true;

                //lock application here
                //frmLockScreen frm = new frmLockScreen();
                //frm.ShowDialog(owner);
                //MessageBox.Show("Lock screen here");
                //Only I gonna to override _last_time_idle if timer does not 
                //cause an invocation of idle event
                if (!_ignore_action)
                {
                    _last_time_idle = DateTime.Now;
                }
            }
            //}
            //Always set to false on all idle invocation
            _ignore_action = false;

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            _ignore_action = true;
        }


        #endregion

        private void alertControl_BeforeFormShow(object sender, DevExpress.XtraBars.Alerter.AlertFormEventArgs e)
        {
            e.AlertForm.OpacityLevel = 1;
        }

        private void alertControl_AlertClick(object sender, DevExpress.XtraBars.Alerter.AlertClickEventArgs e)
        {
            Message msg = e.Info.Tag as Message;
            XtraMessageBox.Show(msg.Text, msg.Caption);
        }
        public class Message
        {
            public Message()
            {
                this.Caption = "Smartfinance";
                this.Text = "Welcome to you, Enjoy";
            }
            public string Caption { get; set; }
            public string Text { get; set; }
            public Image Image { get; set; }
        }

    }
}
