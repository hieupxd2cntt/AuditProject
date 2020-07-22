using DevExpress.XtraBars;
using DevExpress.XtraTab;
using AppClient.Controls;
using AppClient.Utils;

namespace AppClient.Interface
{
    internal interface IMainLanguage
    {
        string ApplicationTitle { get; set; }
        string LogoutTitle { get; set; }
        string LogoutConfirm { get; set; }
    }
    internal interface IMain
    {
        MainProcess Process { get; }
        IMainLanguage Language { get; }
        void OnLogout();
        void InitializeMenu();
        void ApplyMenu();
        void StartupModules();
        XtraTabPage AddTabModule(XtraTabPage tabPage);
        void SelectTabPage(XtraTabPage tabPage);
        void RemoveTabModule(XtraTabPage tabPage);
        void AddModulePreview(ucModulePreview preview);
        void RemoveModulePreview(ucModulePreview preview);
        void RegisterButton(BarButtonItem button);
        void CancelRegisterButton(BarButtonItem button);
    }
}