using System.ComponentModel;
using System.Configuration.Install;

namespace Core.Service
{
    [RunInstaller(true)]
    public partial class CoreServiceInstaller : Installer
    {
        public CoreServiceInstaller()
        {
            InitializeComponent();
        }
    }
}
