using DevExpress.XtraLayout;

namespace AppClient.Interface
{
    public interface ICommonFieldSupportedModule
    {
        bool ValidateRequire { get; }
        LayoutControl CommonLayout { get; }
        string CommonLayoutStoredData { get; }
    }
}
