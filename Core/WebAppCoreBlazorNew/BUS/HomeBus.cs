using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using WebModel;
using Newtonsoft.Json;
using WebAppCoreBlazorNew.Common.Utils;
using WebAppCoreBlazorWebAssembly.Service;
using Microsoft.Extensions.Configuration;
using WebCore.Entities;

namespace WebAppCoreBlazorNew.BUS
{
    public class HomeBus
    {
        private IConfiguration _Configuration;
        private IModuleService _moduleService;
        private IDistributedCache _distributedCache;
        public HomeBus(IModuleService moduleService, IConfiguration Configuration, IDistributedCache distributedCache)
        {
            _moduleService = moduleService;
            _Configuration = Configuration;
            _distributedCache = distributedCache;
        }
        
        public async Task<ModuleInfoViewModel> GetModule(string modId)
        {
            try
            {
                string key = ECacheKey.ModuleInfo.ToString() + modId;
                var cachedData = _distributedCache.GetString(key);
                if (cachedData != null)
                {
                    var module = JsonConvert.DeserializeObject<ModuleInfoViewModel>(cachedData);
                    return module;
                }
                else
                {
                    var model = await _moduleService.GetModule(modId);
                    RedisUtils.SetCacheData(_distributedCache, _Configuration, model, key);
                    return model;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<LanguageInfo>> LoadAllBtnLanguage()
        {
            string key = ECacheKey.BtnLanguageInfo.ToString();
            var cachedData = _distributedCache.GetString(key);
            if (cachedData == null)
            {
                await LoadBtnLanguage();
            }
            cachedData = _distributedCache.GetString(key);
            var languages = JsonConvert.DeserializeObject<List<LanguageInfo>>(cachedData);
            return languages;
        }
        public async Task LoadBtnLanguage()
        {
            try
            {
                string key = ECacheKey.BtnLanguageInfo.ToString();
                var cachedData = _distributedCache.GetString(key);
                if (cachedData != null)
                {
                    var module = JsonConvert.DeserializeObject<List<LanguageInfo>>(cachedData);
                }
                else
                {
                    var model = await _moduleService.GetAllBtnLanguageText();
                    RedisUtils.SetCacheData(_distributedCache, _Configuration, model, key);
                }
            }
            catch (Exception e)
            {
            }
        }
        public ModuleInfoModel ConvertFromViewModel(ModuleInfoViewModel viewModel)
        {
            var module = new ModuleInfoModel();
            var check = viewModel.ModulesInfo.Where(x => x.SubModule == "MMN");
            if (check.Any())
            {
                module.ModulesInfo = check.First();
            }
            else
            {
                if (viewModel.ModulesInfo != null && viewModel.ModulesInfo.Any())
                {
                    module.ModulesInfo = viewModel.ModulesInfo.First();
                }
            }
            module.FieldsInfo = viewModel.FieldsInfo.Where(x => x.ModuleID == module.ModulesInfo.ModuleID).ToList();
            module.ButtonsInfo = viewModel.ButtonsInfo;
            module.ButtonParamsInfo = viewModel.ButtonParamsInfo;
            var lstLanguage = Task.Run(() => LoadAllBtnLanguage()).Result;
            if (lstLanguage != null)
                viewModel.LanguageInfo.AddRange(lstLanguage);
            module.LanguageInfo = viewModel.LanguageInfo;
            var btnLang = Task.Run(() => LoadAllBtnLanguage()).Result;
            if (btnLang != null)
            {
                module.LanguageInfo.AddRange(btnLang);
            }

            return module;
        }
        /// <summary>
        /// Load Tất cả defcode vào cache
        /// </summary>
        /// <returns></returns>
        public async Task<List<CodeInfo>> LoadAllDefCode()
        {
            try
            {
                string key = ECacheKey.DefCode.ToString();
                var cachedData = _distributedCache.GetString(key);
                if (cachedData != null)
                {
                    var defcodes = JsonConvert.DeserializeObject<List<CodeInfo>>(cachedData);
                    return defcodes;
                }
                else
                {
                    var defcodes = await _moduleService.GetAllDefCode();
                    RedisUtils.SetCacheData(_distributedCache, _Configuration, defcodes, key);
                    return defcodes;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// Lấy Defcode từ All cache theo code.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<List<CodeInfo>> LoadAllDefCode(string code)
        {
            try
            {
                string key = ECacheKey.DefCode.ToString();
                var cachedData = _distributedCache.GetString(key);
                var defcodes = new List<CodeInfo>();
                if (cachedData != null)
                {
                    defcodes = JsonConvert.DeserializeObject<List<CodeInfo>>(cachedData);
                }
                else
                {
                    defcodes = await _moduleService.GetAllDefCode();
                    RedisUtils.SetCacheData(_distributedCache, _Configuration, defcodes, key);
                }
                return defcodes.Where(x => x.CodeName == code).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Lấy Defcode từ All cache theo code.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<List<CodeInfo>> LoadAllDefCode(string code, string value)
        {
            try
            {
                string key = ECacheKey.DefCode.ToString();
                var cachedData = _distributedCache.GetString(key);
                var defcodes = new List<CodeInfo>();
                if (cachedData != null)
                {
                    defcodes = JsonConvert.DeserializeObject<List<CodeInfo>>(cachedData);
                }
                else
                {
                    defcodes = await _moduleService.GetAllDefCode();
                    RedisUtils.SetCacheData(_distributedCache, _Configuration, defcodes, key);
                }
                return defcodes.Where(x => x.CodeName == code && x.CodeValue == value).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<CodeInfo>> LoadAllDefCode(List<string> codes)
        {
            try
            {
                string key = ECacheKey.DefCode.ToString();
                var cachedData = _distributedCache.GetString(key);
                var defcodes = new List<CodeInfo>();
                if (cachedData != null)
                {
                    defcodes = JsonConvert.DeserializeObject<List<CodeInfo>>(cachedData);
                }
                else
                {
                    defcodes = await _moduleService.GetAllDefCode();
                    RedisUtils.SetCacheData(_distributedCache, _Configuration, defcodes, key);
                }
                return defcodes.Where(x => codes.Contains(x.CodeName)).ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<SearchModuleInfo> LoadModSearchByModId(string modId)
        {
            try
            {
                string key = ECacheKey.ModuleSearchInfo.ToString() + modId;
                var cachedData = _distributedCache.GetString(key);
                if (cachedData != null)
                {
                    var module = JsonConvert.DeserializeObject<List<SearchModuleInfo>>(cachedData);
                    return module.FirstOrDefault();
                }
                else
                {
                    var modSearchs = await _moduleService.LoadModSearchByModId(modId);
                    RedisUtils.SetCacheData(_distributedCache, _Configuration, modSearchs, key);
                    return modSearchs.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}
