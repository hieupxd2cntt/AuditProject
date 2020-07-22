using System.Collections.Generic;
using System.Diagnostics;
using Core.Controllers;
using Core.Entities;
using Microsoft.Win32;

namespace Core.Common
{
    public class ServerEnvironment : AbstractEnvironment
    {
        public override EnvironmentType EnvironmentType {
            get { return EnvironmentType.SERVER_APPLICATION; }
        }

        public override RegistryKey CoreRegistry {
            get
            {
                return Registry.LocalMachine
                    .OpenSubKey("SOFTWARE", true)
                    .CreateSubKey("App")
                    .CreateSubKey("Core")
                    .CreateSubKey("Server");
            }
        }

        static ServerEnvironment()
        {
            EventLog.WriteEntry("BackEnd.Service", "Back: Starting buffer cache...", EventLogEntryType.Information);
            EventLog.WriteEntry("BackEnd.Service", "Back: Buffer cache finished!", EventLogEntryType.Information);
        }

        public ServerEnvironment()
        {
            m_ServerInfo = new ServerInfo { CultureName = "en-US" };
            InitializeEnvironment();
        }

        public override void InitializeMenu()
        {
        }

        public override List<LanguageInfo> BuildLanguageCache()
        {
            using (var ctrlSA = new SAController())
            {
                return ctrlSA.BuildLanguageInfo();
            }
        }

        public override List<ErrorInfo> BuildErrorsInfoCache()
        {
            using (var ctrlSA = new SAController())
            {
                return ctrlSA.BuildErrorsInfo();
            }
        }

        public override List<ModuleFieldInfo> BuildModuleFieldCache()
        {
            using (var ctrlSA = new SAController())
            {
                return ctrlSA.BuildModuleFieldsInfo();
            }
        }

        public override List<GroupSummaryInfo> BuildGroupSummaryCache()
        {
            using (var ctrlSA = new SAController())
            {
                return ctrlSA.BuildGroupSummaryInfo();
            }
        }

        public override List<ModuleInfo> BuildModulesInfoCache()
        {
            using (var ctrlSA = new SAController())
            {
                return ctrlSA.BuildModulesInfo();
            }
        }

        public override List<CodeInfo> BuildCodesInfoCache()
        {
            using (var ctrlSA = new SAController())
            {
                return ctrlSA.BuildCodesInfo();
            }
        }

        public override List<ButtonInfo> BuildSearchButtonsCache()
        {
            using (var ctrlSA = new SAController())
            {
                return ctrlSA.BuildSearchButtonsInfo();
            }
        }

        public override List<ButtonParamInfo> BuildSearchButtonParamsCache()
        {
            using (var ctrlSA = new SAController())
            {
                return ctrlSA.BuildSearchButtonParamsInfo();
            }
        }

        public override List<OracleParam> BuildOracleParamsCache()
        {
            using (var ctrlSA = new SAController())
            {
                return ctrlSA.BuildOracleParamsInfo();
            }
        }

        public override List<ValidateInfo> BuildValidatesInfoCache()
        {
            using (var ctrlSA = new SAController())
            {
                return ctrlSA.BuildValidatesInfo();
            }
        }

        public override List<NameValueItem> GetSourceList(ModuleInfo moduleInfo, ModuleFieldInfo fieldInfo, List<string> values)
        {
            return null;
        }

        public override void GetCurrentUserProfile()
        {
            UserProfile userProfile;

            using (var ctrlSA = new SAController())
            {
                ctrlSA.GetCurrentUserProfile(out userProfile);
            }

            App.Environment.ClientInfo.UserProfile = userProfile;
        }

        public override List<ExportHeader> BuildExportHeaderCache()
        {
            using (var ctrlSA = new SAController())
            {
                return ctrlSA.BuildExportHeaderInfo();
            }
        }

        public override List<SysvarInfo> BuildSysvarInfoCache()
        {
            using (var ctrlSA = new SAController())
            {
                return ctrlSA.BuildSysvarInfo();
            }
        }
    }
}
