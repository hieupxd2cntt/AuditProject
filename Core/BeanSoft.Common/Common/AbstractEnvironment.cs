using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Utils;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;

namespace Core.Common
{
    public enum EnvironmentType
    {
        CLIENT_APPLICATION = 1,
        SERVER_APPLICATION = 2
    }

    public abstract class AbstractEnvironment
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConfiguration _configuration;
        protected ServerInfo m_ServerInfo;
        public ClientInfo ClientInfo { get; set; }
        public ServerInfo ServerInfo
        {
            get
            {
                return m_ServerInfo;
            }
        }

        public CachedHashInfo CachedHashInfo { get; set; }
        public abstract EnvironmentType EnvironmentType { get; }
        public abstract RegistryKey CoreRegistry { get; }
        public abstract void InitializeMenu();
        public abstract List<LanguageInfo> BuildLanguageCache();
        public abstract List<ErrorInfo> BuildErrorsInfoCache();
        public abstract List<ModuleFieldInfo> BuildModuleFieldCache();
        public abstract List<ValidateInfo> BuildValidatesInfoCache();
        public abstract List<ModuleInfo> BuildModulesInfoCache();
        public abstract List<CodeInfo> BuildCodesInfoCache();
        public abstract List<ButtonInfo> BuildSearchButtonsCache();
        public abstract List<ButtonParamInfo> BuildSearchButtonParamsCache();
        public abstract List<OracleParam> BuildOracleParamsCache();
        public abstract List<GroupSummaryInfo> BuildGroupSummaryCache();
        public abstract List<ExportHeader> BuildExportHeaderCache();
        public abstract List<SysvarInfo> BuildSysvarInfoCache();
        public abstract List<NameValueItem> GetSourceList(ModuleInfo moduleInfo, ModuleFieldInfo fieldInfo, List<string> values);
        public abstract void GetCurrentUserProfile();

        public void InitializeEnvironment()
        {
            CachedHashInfo = new CachedHashInfo();
            if (EnvironmentType == EnvironmentType.CLIENT_APPLICATION)
            {
                OnInitializeStepChanged("Initialize themes, icons, images...");
                InitializeTheme();
            }


            OnInitializeStepChanged("Caching language information...");
            InitializeLanguage();

            OnInitializeStepChanged("Caching module buttons information...");
            InitializeSearchButton();

            OnInitializeStepChanged("Caching module group summaries information...");
            InitializeGroupSummaryInfo();

            OnInitializeStepChanged("Caching module button parameters information...");
            InitializeSearchButtonParams();

            OnInitializeStepChanged("Caching oracle parameters information...");
            InitializeOracleParams();

            OnInitializeStepChanged("Caching errors information...");
            InitializeErrorsInfo();
            
            OnInitializeStepChanged("Caching modules information...");
            InitializeModulesInfo();

            OnInitializeStepChanged("Caching fields information...");
            InitializeModuleFieldsInfo();

            OnInitializeStepChanged("Caching validates information...");
            InitializeValidatesInfoCache();

            OnInitializeStepChanged("Caching codes information...");
            InitializeCodesInfo();

            OnInitializeStepChanged("Caching module export header information...");            
            InitializeExportHeaderInfo();

            OnInitializeStepChanged("Caching sysvar information...");
            InitializeSysvarInfo();
        }

        protected virtual void OnInitializeStepChanged(string stepName)
        {
        }

        private void InitializeCodesInfo()
        {
            AllCaches.CodesInfo = BuildCodesInfoCache();
            CachedHashInfo.CodesInfoHash = CachedUtils.CalcHash(AllCaches.CodesInfo);
        }

        private void InitializeModulesInfo()
        {
            AllCaches.ModulesInfo = BuildModulesInfoCache();
            CachedHashInfo.ModulesInfoHash = CachedUtils.CalcHash(AllCaches.ModulesInfo);

            //RedisUtils.SetCacheData(AllCaches.ModulesInfo, "ModulesInfo");
            //List<SearchModuleInfo> searchModuleInfo = new List<SearchModuleInfo>();
            //foreach(ModuleInfo modInfo in AllCaches.ModulesInfo)
            //{
            //    var type = modInfo.GetType();
            //    if (type.Name =="SearchModuleInfo")
            //    {
            //        searchModuleInfo.Add((SearchModuleInfo)modInfo);
                   
            //    }
            //    //RedisUtils.SetData<List<ModuleInfo>>("ModulesInfo", AllCaches.ModulesInfo);
            //    //var newObject = (ModuleInfo)type.GetConstructor(new Type[0]).Invoke(new object[0]);
            //}

            //RedisUtils.SetData<List<SearchModuleInfo>>("SearchModuleInfo", searchModuleInfo);
        }

        private void InitializeModuleFieldsInfo()
        {
            AllCaches.ModuleFieldsInfo = BuildModuleFieldCache();
            CachedHashInfo.ModuleFieldsInfoHash = CachedUtils.CalcHash(AllCaches.ModuleFieldsInfo);
        }

        private void InitializeGroupSummaryInfo()
        {
            AllCaches.GroupSummaryInfos = BuildGroupSummaryCache();
            CachedHashInfo.GroupSummaryInfoHash = CachedUtils.CalcHash(AllCaches.GroupSummaryInfos);
        }

        private void InitializeExportHeaderInfo()
        {
            AllCaches.ExportHeaders = BuildExportHeaderCache();
            CachedHashInfo.ExportHeaderInfoHash = CachedUtils.CalcHash(AllCaches.ExportHeaders);
        }

        private void InitializeSysvarInfo()
        {
            AllCaches.SysvarsInfo = BuildSysvarInfoCache();
            CachedHashInfo.SysvarInfoHash = CachedUtils.CalcHash(AllCaches.SysvarsInfo);
        }

        private void InitializeOracleParams()
        {
            AllCaches.OracleParamsInfo = BuildOracleParamsCache();
            CachedHashInfo.OracleParamsInfoHash = CachedUtils.CalcHash(AllCaches.OracleParamsInfo);
        }

        private void InitializeSearchButton()
        {
            AllCaches.SearchButtonsInfo = BuildSearchButtonsCache();
            CachedHashInfo.SearchButtonsInfoHash = CachedUtils.CalcHash(AllCaches.SearchButtonsInfo);
        }

        private void InitializeSearchButtonParams()
        {
            AllCaches.SearchButtonParamsInfo = BuildSearchButtonParamsCache();
            CachedHashInfo.SearchButtonParamsInfoHash = CachedUtils.CalcHash(AllCaches.SearchButtonParamsInfo);
        }

        private void InitializeErrorsInfo()
        {
            AllCaches.BaseErrorsInfo = BuildErrorsInfoCache();
            CachedHashInfo.ErrorsInfoHash = CachedUtils.CalcHash(AllCaches.BaseErrorsInfo);

            AllCaches.ErrorsInfo = AllCaches.BaseErrorsInfo.ToDictionary(
                item => item.ErrorCode,
                item => item.ErrorName);
        }

        private void InitializeValidatesInfoCache()
        {
            AllCaches.BaseValidatesInfo = BuildValidatesInfoCache();
            CachedHashInfo.ValidatesInfoHash = CachedUtils.CalcHash(AllCaches.BaseValidatesInfo);

            AllCaches.ValidatesInfo = AllCaches.BaseValidatesInfo.ToDictionary(
                item => item.ValidateName,
                item => item);
        }

        public void InitializeLanguage()
        {
            AllCaches.BaseLanguageInfo = BuildLanguageCache();
            CachedHashInfo.LanguageHash = CachedUtils.CalcHash(AllCaches.BaseLanguageInfo);

            AllCaches.LanguageInfo = AllCaches.BaseLanguageInfo.ToDictionary(
                item => item.LanguageName,
                item => string.IsNullOrEmpty(item.LanguageValue) ? item.LargerLanguageValue : item.LanguageValue);
        }

        public void InitializeTheme()
        {
            ThemeUtils.Initialize();
        }
        public void UpdateEnvironment()
        {
            CachedUtils.ClearAllCache();
            CachedHashInfo = new CachedHashInfo();
            if (EnvironmentType == EnvironmentType.CLIENT_APPLICATION)
            {
                InitializeTheme();
            }
            InitializeLanguage();
            InitializeSearchButton();
            InitializeGroupSummaryInfo();
            InitializeSearchButtonParams();
            InitializeOracleParams();
            InitializeErrorsInfo();
            InitializeModulesInfo();
            InitializeModuleFieldsInfo();
            InitializeValidatesInfoCache();
            InitializeCodesInfo();
            InitializeExportHeaderInfo();
            InitializeSysvarInfo();
        }
    }
}
