using Microsoft.Win32;

namespace Core.Extensions
{
    public static class RegistryExtensions
    {
        public static string GetValueOrCreate(this RegistryKey regKey, string name, string defValue)
        {
            if (regKey.GetValue(name) == null)
            {
                regKey.SetValue(name, defValue);
            }

            return (string)regKey.GetValue(name);
        }
    }
}
