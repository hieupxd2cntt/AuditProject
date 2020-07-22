using System;
using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel;
using System.Threading;
using System.Windows.Forms;
using Core.Common;
using Core.Utils;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraEditors;

namespace AppClient
{
    public static class Program
    {
        public static string StrUserName = "";
        public static string StrPassWord = "";
        public static string StrMessageID = "";
        public static string strAppStartUpPath = "";
        public static bool blLogin = false;
        public static bool blCheckFile = false;
        public static string FileName = "";
        public static string strExecMod = "";
        public static string treeModuleID = "";
        public static bool blVerifyImport = false;
        public static bool blEnableImport = false;
        public static string txnum = "";
        public static string rptid = "";
        public static string rptlogID = "";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]

        static void Main(string[] args)
        {
            var isInited = false;
            try
            {
                WindowsFormsSettings.ForceDirectXPaint();
                WindowsFormsSettings.EnableFormSkins();

                DevExpress.UserSkins.BonusSkins.Register();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                UserLookAndFeel.Default.SetSkinStyle("The Bezier", "Office Colorful");
                strAppStartUpPath = Application.StartupPath;

                SkinManager.EnableFormSkins();
                frmSplash.ShowSplashScreen();

                for (var count = 1; count <= 3; count++)
                {
                    try
                    {
                        App.Environment = new ClientEnvironment
                        {
                            ClientInfo =
                                                      {
                                                          Culture = new CultureInfo("en-US")
                                                                        {
                                                                            DateTimeFormat =
                                                                                {
                                                                                    ShortDatePattern = "d/M/yyyy",
                                                                                    LongDatePattern = "dd MMMM yyyy"
                                                                                }
                                                                        },
                                                          DataClientCache = new List <Core.Entities.DataClientCache>()
                                                      }
                        };

                        ThreadUtils.SetClientCultureInfo();
                        isInited = true;
                        break;
                    }

                    catch (Exception e)
                    {
                        var ex = ErrorUtils.CreateErrorWithSubMessage(ERR_SYSTEM.ERR_SYSTEM_UNKNOWN, e.Message);

                        for (var i = 5; i > 0; i--)
                        {
                            frmSplash.ChangeSplashStatus("Connect again after next " + i + "/" + count + " second(s)...");
                            Thread.Sleep(1000);
                        }
                    }
                }

                if (isInited)
                {
                    frmSplash.ChangeSplashStatus("Initializing application...");
                    frmSplash.CloseForm();

                    //var frmMain = new frmMainRibbon();
                    var frmMain = new frmMainFluent();
                    Application.Run(frmMain);

                }
            }
            catch (FaultException ex)
            {
                frmInfo.ShowError("Main", ex);
                Environment.Exit(1);
            }
            catch (Exception ex)
            {
                frmInfo.ShowError("Main", ErrorUtils.CreateErrorWithSubMessage(ERR_SYSTEM.ERR_SYSTEM_UNKNOWN, ex.Message));
                Environment.Exit(1);
            }
            finally
            {
                frmSplash.CloseForm();
                Environment.Exit(0);
            }
        }
    }
}