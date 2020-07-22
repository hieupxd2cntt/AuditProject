using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using WebAppCore.Models;
using WebAppCoreNew.Common;
using WebAppCoreNew.Common.Utils;
using WebAppCoreNew.Service;
using WebCore.Entities;
using WebGia.Common;
using WebModelCore;
using WebModelCore.CodeInfo;
using WebModelCore.Common;
using WebModelCore.LoginModel;

namespace WebAppCoreNew.Controllers
{
    [Area("Admin")]
    //[ResponseCache(CacheProfileName = "Default")]
    [CustomAuthorization(209715200)]
    public class HomeController : BaseController
    {
        private readonly IDistributedCache _distributedCache;
        protected IConfiguration _Configuration;
        protected IModuleService _moduleService;
        protected IUserService _userService;
        protected ILogService _logService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private IHttpContextAccessor _accessor;
        private string Schema = "core";
        public HomeController(IConfiguration configuration, IModuleService moduleService, IUserService userService, ILogService logService, IDistributedCache distributedCache, IHostingEnvironment hostingEnvironment, IHttpContextAccessor accessor) : base(configuration)
        {
            _Configuration = configuration;
            _moduleService = moduleService;
            _distributedCache = distributedCache;
            _hostingEnvironment = hostingEnvironment;
            _userService = userService;
            _logService = logService;
            _accessor = accessor;
        }

        public async Task LoadSysVar()
        {
            try
            {
                string key = ECacheKey.Sysvar.ToString();
                var cachedData = _distributedCache.GetString(key);
                if (cachedData != null)
                {
                    var module = JsonConvert.DeserializeObject<List<SysVar>>(cachedData);
                }
                else
                {
                    var model = await _moduleService.GetAllSysVar();
                    RedisUtils.SetCacheData(_distributedCache, _Configuration, model, key);
                }
            }
            catch (Exception e)
            {
            }
        }
        public async Task ReLoadSysVar()
        {
            try
            {
                string key = ECacheKey.Sysvar.ToString();
                var model = await _moduleService.GetAllSysVar();
                RedisUtils.SetCacheData(_distributedCache, _Configuration, model, key);
            }
            catch (Exception e)
            {
            }
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
        public async Task<SysVar> GetSysVarByVarName(string varName)
        {
            string key = ECacheKey.Sysvar.ToString();
            var cachedData = _distributedCache.GetString(key);
            if (cachedData == null)
            {
                await ReLoadSysVar();
            }
            cachedData = _distributedCache.GetString(key);
            var sysVars = JsonConvert.DeserializeObject<List<SysVar>>(cachedData);
            var checkVar = sysVars.Where(x => x.Var_Name.Trim().ToUpper() == varName.ToUpper());
            if (checkVar.Any())
            {
                return checkVar.First();
            }
            return null;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string modId = "03200", string parr = "")
        {

            if (modId == "HomePage")
            {
                return RedirectToAction("Index", "HomePage");
            }
            var dataMod = await GetModule(modId);
            if (dataMod == null || !dataMod.ModulesInfo.Any())
            {
                return RedirectToAction("Login", "Home");
            }
            var module = ConvertFromViewModel(dataMod);
            if (module.ModulesInfo.ModuleType == ((int)EModuleType.MAINTAIN).ToString("D2"))
            {
                return RedirectToAction("Edit", new { modId = modId, parram = parr });
            }
            else
            {
                return RedirectToAction("ModSearch", new { modId = modId, parramMods = parr });
            }
            return View();
        }
        public async Task<IActionResult> HomePage()
        {
            var loggerConfig = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
            {
                FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
                EmitEventFailure = EmitEventFailureHandling.RaiseCallback,
                ModifyConnectionSettings = x => x.BasicAuthentication(username: "elastic", password: "changeme")
            });

            var logger = loggerConfig.CreateLogger();


            while (true)
            {
                Console.WriteLine("Logging..");
                logger.Information("This is Serilog by AnhTT47...");
                //Thread.Sleep(1000);
            }
            return View();
        }
        public async Task<IActionResult> GoToMod(string modId, string fieldName, string parr = "", string key = "")
        {
            if (modId == "QC")
            {
                return RedirectToAction("Index", "QC", new { attrsetId = parr, keyBtn = key });
            }
            var dataMod = await GetModule(modId);
            if (!dataMod.ModulesInfo.Any())
            {
                return RedirectToAction("Login", "Home");
            }

            var module = ConvertFromViewModel(dataMod);
            var btnParram = new List<ButtonParamInfo>();
            btnParram.Add(new ButtonParamInfo { FieldName = fieldName, Value = parr, ModuleID = modId });
            if (module.ModulesInfo.ModuleTypeName == EModuleType.MAINTAIN.ToString())
            {
                return RedirectToAction("Edit", new { modId = modId, parram = JsonConvert.SerializeObject(btnParram) });
            }
            else
            {
                return RedirectToAction("Search", new { modId = modId, parramMods = JsonConvert.SerializeObject(btnParram) });
            }
            return View();
        }

        public async Task<IActionResult> Edit(string modId, string modSearchId, string subModId, string fieldNameEdit, string parram, bool edit = true, int success = 0)
        {
            if (modId == ConstMod.ModViewPdf)
            {
                return RedirectToAction("Index", "Default");
            }
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login", "Home");
            }
            await LoadViewBagEdit(modId, modSearchId, subModId, fieldNameEdit, parram, edit, success);
            return View();
        }
        /// <summary>
        /// Gán Parram field trường hợp update, delete
        /// </summary>
        /// <param name="parram"></param>
        /// <param name="fields"></param>
        private void AssignParamField(string parram, List<ModuleFieldInfo> fields)
        {
            if (string.IsNullOrEmpty(parram))
            {
                return;
            }
            try
            {
                var lstParram = JsonConvert.DeserializeObject<List<ButtonParamInfo>>(parram);
                foreach (var item in fields)
                {
                    foreach (var prr in lstParram)
                    {
                        if (item.FieldName.ToUpper() == prr.FieldName.ToUpper())
                        {
                            item.FieldID += prr.Value + ",";
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        private async Task LoadViewBagEdit(string modId, string modSearchId, string subModId, string fieldNameEdit, string parram, bool edit, int success)
        {
            var ip = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            string modShowPdf = ConstMod.ModViewPdf;//Mod đặc biệt là mod xem pdf.
            var modelBarCode = "";
            var dataModule = await GetModule(modId);
            var fields = CommonFunction.GetModuleFields(dataModule.FieldsInfo, modId, FLDGROUP.PARAMETER);
            foreach (var item in fields)
            {//Set lại giá trị null cho field
                item.FieldID = "";
            }
            AssignParamField(parram, fields);
            var dataMaintainInfo = (await _moduleService.LoadDataMainTainModule(modId, subModId, parram, fields));
            ViewBag.DataControl = dataMaintainInfo;
            ViewBag.ModId = modId;
            ViewBag.SubModId = subModId;
            ViewBag.ModSearchId = modSearchId;
            ViewBag.KeyEdit = parram;
            ViewBag.Edit = edit;
            ViewBag.FieldNameEdit = fieldNameEdit;
            var data = await GetModule(modId);
            var module = ConvertFromViewModel(data);
            ViewBag.ModuleInfo = module;
            ViewBag.Success = success;
            var cb = module.FieldsInfo.Where(x => !String.IsNullOrEmpty(x.ListSource));
            if (cb.Any())
            {
                var codeInfoParram = cb.Select(x => new CodeInfoParram
                {
                    CtrlType = x.ControlType,
                    Name = x.FieldName,
                    ListSource = x.ListSource
                });
                //var para = string.Join("", sources);
                var sourceCodeInfo = cb.Where(x => x.ListSource.Contains(":"));//Lấy những thông tin các ListSource từ DefCode
                var codeInfoModels = new List<CodeInfoModel>();
                if (sourceCodeInfo != null && sourceCodeInfo.Any())
                {
                    var defCodeAll = await LoadAllDefCode();
                    var lstSource = sourceCodeInfo.Select(x => x.ListSource).ToList();
                    var cbDefCode = defCodeAll.Where(x => lstSource.Contains(":" + x.CodeType + "." + x.CodeName));
                    foreach (var item in sourceCodeInfo)
                    {
                        codeInfoModels.Add(new CodeInfoModel { Name = item.FieldName, CodeInfos = cbDefCode.Where(x => ":" + x.CodeType + "." + x.CodeName == item.ListSource).ToList() });
                    }
                }
                var dataCB = (await _moduleService.GetCombobox(codeInfoParram.Where(x => !x.ListSource.Contains(":")).ToList()));//Lấy thông tin các Combobox theo Store
                if (dataCB.Data != null)
                {
                    codeInfoModels.AddRange(dataCB.Data);
                }
                ViewBag.DataCombobox = codeInfoModels;
            }
            //if (cb.Any())
            //{

            //    var sources = cb.Select(x => x.ListSource).Distinct().ToList();
            //    if (modId == modShowPdf)
            //    {
            //        var codeInfoParram = cb.Select(x => new CodeInfoParram
            //        {

            //            Name = x.FieldName,
            //            ListSource = x.ListSource,
            //            Parrams = String.IsNullOrEmpty(HttpContext.Session.GetString("UserName")) ? string.Format("{0}${1}", modelBarCode, ip) : string.Format("{0}${1}#{2}", modelBarCode, ip, HttpContext.Session.GetString("UserName"))
            //        });
            //        var dataCB = (await _moduleService.GetCombobox(codeInfoParram.ToList()));
            //        ViewBag.DataCombobox = dataCB.Data;
            //    }
            //    else
            //    {
            //        var codeInfoParram = cb.Select(x => new CodeInfoParram
            //        {

            //            Name = x.FieldName,
            //            ListSource = x.ListSource,
            //        });
            //        var dataCB = (await _moduleService.GetCombobox(codeInfoParram.ToList()));
            //        ViewBag.DataCombobox = dataCB.Data;
            //    }
            //    //var para = string.Join("", sources);

            //}
        }
        public async Task<IActionResult> Search(string modId, string parramMods = "")
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")) && modId.ToLower() != ConstMod.ModListHomo.ToLower())
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.ParramMods = parramMods;
                var data = await GetModule(modId);
                int userId = int.Parse("0" + HttpContext.Session.GetString("UserId"));
                var groupModUser = await _moduleService.GetGroupModByUserId(userId);
                ViewBag.RoleUser = groupModUser;
                var module = ConvertFromViewModel(data);
                ViewBag.Title = module.ModulesInfo.ModuleName.GetLanguageTitle(await LoadAllBtnLanguage());
                ViewBag.ModuleInfo = module;
                var cb = module.FieldsInfo.Where(x => !String.IsNullOrEmpty(x.ListSource));
                var scdType = module.FieldsInfo.Select(x => x.ConditionType);
                if (cb.Any())
                {
                    var codeInfoParram = cb.Select(x => new CodeInfoParram
                    {
                        CtrlType = x.ControlType,
                        Name = x.FieldName,
                        ListSource = x.ListSource
                    });
                    //var para = string.Join("", sources);
                    var sourceCodeInfo = cb.Where(x => x.ListSource.Contains(":"));//Lấy những thông tin các ListSource từ DefCode
                    var codeInfoModels = new List<CodeInfoModel>();
                    if (sourceCodeInfo != null && sourceCodeInfo.Any())
                    {
                        var defCodeAll = await LoadAllDefCode();
                        var lstSource = sourceCodeInfo.Select(x => x.ListSource).ToList();
                        var cbDefCode = defCodeAll.Where(x => lstSource.Contains(":" + x.CodeType + "." + x.CodeName));
                        foreach (var item in sourceCodeInfo)
                        {
                            codeInfoModels.Add(new CodeInfoModel { Name = item.FieldName, CodeInfos = cbDefCode.Where(x => ":" + x.CodeType + "." + x.CodeName == item.ListSource).ToList() });
                        }
                    }
                    var dataCB = (await _moduleService.GetCombobox(codeInfoParram.Where(x => !x.ListSource.Contains(":")).ToList()));//Lấy thông tin các Combobox theo Store
                    codeInfoModels.AddRange(dataCB.Data);
                    ViewBag.DataCombobox = codeInfoModels;
                }

                var modSearch = await LoadModSearchByModId(modId);
                if (modSearch != null)
                {
                    var parrams = new List<string>();
                    if (!string.IsNullOrEmpty(parramMods))
                    {
                        var btnParramInfo = (List<ButtonParamInfo>)JsonConvert.DeserializeObject<List<ButtonParamInfo>>(parramMods);
                        var temp = btnParramInfo.Select(x => x.FieldName + " = '" + x.Value + "'");
                        parrams.AddRange(temp);
                    }
                    var query = "";
                    if (modSearch.QueryFormat.IndexOf("{0}") > 0)
                    {
                        if (modSearch.QueryFormat.IndexOf("{1}") > 0)
                        {
                            var currPage = 1;
                            var paging = String.Format(" RowNumber Between {0} AND {1}", (currPage - 1) * CommonMethod.PageSize, (currPage - 1) * CommonMethod.PageSize + CommonMethod.PageSize);
                            query = string.Format(modSearch.QueryFormat, Schema, parrams.Any() ? String.Join(" AND ", parrams) : " 1=1 ", paging);
                            ViewBag.CurrPage = currPage;
                        }
                        else
                        {
                            query = string.Format(modSearch.QueryFormat, Schema, parrams.Any() ? String.Join(" AND ", parrams) : " 1=1 ");
                        }
                    }
                    else
                    {
                        query = String.Format(modSearch.QueryFormat, Schema);
                    }
                    var dataGrid = await _moduleService.LoadQueryModule(new ParramModuleQuery { Store = query });

                    //var dataGrid = await _moduleService.LoadQueryModule(new ParramModuleQuery { Store = modSearch.QueryFormat });
                    ViewBag.DataSearch = dataGrid;
                }

                return View("Search", modId);
            }
            catch (Exception ex)
            {

                return View("Search", modId);
            }

        }

        public async Task<IActionResult> ModSearch(string modId, string parramMods = "")
        {
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
                {
                    return RedirectToAction("Login", "Home");
                }
                ViewBag.ParramMods = parramMods;
                var data = await GetModule(modId);
                int userId = int.Parse("0" + HttpContext.Session.GetString("UserId"));
                var groupModUser = await _moduleService.GetGroupModByUserId(userId);
                ViewBag.RoleUser = groupModUser;
                var module = ConvertFromViewModel(data);
                ViewBag.Title = module.ModulesInfo.ModuleName.GetLanguageTitle(await LoadAllBtnLanguage());
                ViewBag.ModuleInfo = module;
                var cb = module.FieldsInfo.Where(x => !String.IsNullOrEmpty(x.ListSource));
                var scdType = module.FieldsInfo.Select(x => x.ConditionType);
                if (cb.Any())
                {
                    var codeInfoParram = cb.Select(x => new CodeInfoParram
                    {
                        CtrlType = x.ControlType,
                        Name = x.FieldName,
                        ListSource = x.ListSource
                    });
                    //var para = string.Join("", sources);
                    var sourceCodeInfo = cb.Where(x => x.ListSource.Contains(":"));//Lấy những thông tin các ListSource từ DefCode
                    var codeInfoModels = new List<CodeInfoModel>();
                    if (sourceCodeInfo != null && sourceCodeInfo.Any())
                    {
                        var defCodeAll = await LoadAllDefCode();
                        var lstSource = sourceCodeInfo.Select(x => x.ListSource).ToList();
                        var cbDefCode = defCodeAll.Where(x => lstSource.Contains(":" + x.CodeType + "." + x.CodeName));
                        foreach (var item in sourceCodeInfo)
                        {
                            codeInfoModels.Add(new CodeInfoModel { Name = item.FieldName, CodeInfos = cbDefCode.Where(x => ":" + x.CodeType + "." + x.CodeName == item.ListSource).ToList() });
                        }
                    }
                    var dataCB = (await _moduleService.GetCombobox(codeInfoParram.Where(x => !x.ListSource.Contains(":")).ToList()));//Lấy thông tin các Combobox theo Store
                    codeInfoModels.AddRange(dataCB.Data);
                    ViewBag.DataCombobox = codeInfoModels;
                }
                var selectItem = new List<SelectListItem>();
                var cbAllControl = module.FieldsInfo.Where(x => x.FieldGroup == FLDGROUP.SEARCH_CONDITION).Select(x => new SelectListItem { Value = x.FieldID, Text = x.FieldName }).ToList();
                ViewBag.CbAllControl = cbAllControl;
                var modSearch = await LoadModSearchByModId(modId);
                if (modSearch != null)
                {
                    var parrams = new List<string>();
                    if (!string.IsNullOrEmpty(parramMods))
                    {
                        var btnParramInfo = (List<ButtonParamInfo>)JsonConvert.DeserializeObject<List<ButtonParamInfo>>(parramMods);
                        var temp = btnParramInfo.Select(x => x.FieldName + " = '" + x.Value + "'");
                        parrams.AddRange(temp);
                    }
                    var query = "";
                    if (modSearch.QueryFormat.IndexOf("{0}") > 0)
                    {
                        if (modSearch.QueryFormat.IndexOf("{1}") > 0)
                        {
                            var currPage = 1;
                            var paging = String.Format(" RowNumber Between {0} AND {1}", (currPage - 1) * CommonMethod.PageSize, (currPage - 1) * CommonMethod.PageSize + CommonMethod.PageSize);
                            query = string.Format(modSearch.QueryFormat, Schema, parrams.Any() ? String.Join(" AND ", parrams) : " 1=1 ", paging);
                            ViewBag.CurrPage = currPage;
                        }
                        else
                        {
                            query = string.Format(modSearch.QueryFormat, Schema, parrams.Any() ? String.Join(" AND ", parrams) : " 1=1 ");
                        }
                    }
                    else
                    {
                        query = String.Format(modSearch.QueryFormat, Schema);
                    }
                    var dataGrid = await _moduleService.LoadQueryModule(new ParramModuleQuery { Store = query });

                    //var dataGrid = await _moduleService.LoadQueryModule(new ParramModuleQuery { Store = modSearch.QueryFormat });
                    ViewBag.DataSearch = dataGrid;
                }

                return View("ModSearch", modId);
            }
            catch (Exception ex)
            {

                return View("ModSearch", modId);
            }

        }
        public FileResult DownloadFile(string fileName)
        {
            var fileBytes = System.IO.File.ReadAllBytes(fileName);
            var name = Path.GetFileName(fileName);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, name);
        }
        private async Task<ModuleInfoViewModel> GetModule(string modId)
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
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        public async Task<IActionResult> AjaxLoadModSchedule(string modId)
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login", "Home");
            }
            string modSearchId = "";
            string subModId = "", fieldNameEdit = "", parram = "";
            var edit = true; var success = 0;


            await LoadViewBagEdit(modId, modSearchId, subModId, fieldNameEdit, parram, edit, success);
            return PartialView(@"\Areas\Admin\Views\Home\Edit.cshtml");
        }
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        public async Task<ActionResult> AjaxCallBackOnchangeControl(string valueControl, string idControl, string modId)
        {
            var modInfo = await GetModule(modId);
            var fldInfo = modInfo.FieldsInfo.Where(x => x.FieldName == idControl);
            if (fldInfo != null && fldInfo.Any())
            {
                var storeCallBack = fldInfo.First().Callback;
                var lst = new List<string>();
                lst.Add(valueControl);
                var parram = new ParramModuleQuery { Store = storeCallBack, Parram = lst.ToArray(), Fields = fldInfo.ToList() };
                var data = await _moduleService.ExcuteStore2DataTable(parram);
                return Json(data);
            }

            return PartialView(@"\Areas\Admin\Views\Home\Edit.cshtml");
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        public async Task<ActionResult> AjaxAddControlModSearch(string modId, string valueControl, int index)
        {
            var modInfos = await GetModule(modId);
            var modInfo = ConvertFromViewModel(modInfos);
            var fldInfo = modInfo.FieldsInfo.Where(x => x.FieldID == valueControl);
            if (fldInfo != null && fldInfo.Any())
            {
                var field = fldInfo.First();
                var codeInfo = new List<CodeInfoModel>();
                if (!string.IsNullOrEmpty(field.ListSource))
                {
                    var listSource = field.ListSource;
                    var codeParramInfo = new List<CodeInfoParram>();
                    codeParramInfo.Add(new CodeInfoParram { CtrlType = field.ControlType, ListSource = field.ListSource, Name = field.FieldName });
                    var data = await _moduleService.GetCombobox(codeParramInfo);
                    codeInfo = data.Data;
                }
                var conditionDefCode = await LoadAllDefCode("SCDTYPE", field.ConditionType);
                var model = new ModSearchControlModel
                {
                    CodeInfos = codeInfo,
                    FldInfo = field,
                    ModInfo = modInfo.ModulesInfo,
                    Languages = modInfo.LanguageInfo,

                };
                if (conditionDefCode != null && conditionDefCode.Any())
                {
                    var conditionValues = await LoadAllDefCode(conditionDefCode.First().CodeValueName);
                    if (conditionValues != null && conditionValues.Any())
                    {
                        model.Condition = conditionValues.Select(x => new SelectListItem { Value = x.CodeValue, Text = x.CodeValueName }).ToList();
                    }
                }
                model.Index = index;

                return PartialView(@"\Areas\Admin\Views\Home\_ModSearchControl.cshtml", model);
            }

            return PartialView(@"\Areas\Admin\Views\Home\_ModSearchControl.cshtml");
        }
        private async Task<SearchModuleInfo> LoadModSearchByModId(string modId)
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
        private ModuleInfoModel ConvertFromViewModel(ModuleInfoViewModel viewModel)
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
        private async Task<List<LanguageInfo>> LoadAllBtnLanguage()
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
        [HttpPost]
        [RequestSizeLimit(100000000)]
        public async Task<ActionResult> Edit(IFormCollection model)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login", "Home");
            }
            var modId = "";
            if (model["ModId"].Any())
            {
                modId = (model["ModId"]).First().ToString();
            }

            var subModId = "";
            if (model["SubModId"].Any())
            {
                subModId = (model["SubModId"]).First().ToString();
            }

            var keyEdit = "";
            if (model["KeyEdit"].Any())
            {
                keyEdit = (model["KeyEdit"]).First().ToString();
            }
            if (string.IsNullOrEmpty(modId))
            {
                return RedirectToAction("Index", "Home");
            }
            var fieldNameEdit = "";
            if (model["FieldNameEdit"].Any())
            {
                fieldNameEdit = (model["FieldNameEdit"]).First().ToString();
            }

            var modInfo = await GetModule(modId);
            var modSearchId = model["ModSearchId"].ToString();
            var fieldEdits = CommonFunction.GetModuleFields(modInfo.FieldsInfo, modId, FLDGROUP.COMMON);
            foreach (var item in fieldEdits)
            {//Set lại giá trị null cho field
                item.FieldID = "";
            }
            var fieldParram = CommonFunction.GetModuleFields(modInfo.FieldsInfo, modId, FLDGROUP.PARAMETER);
            foreach (var item in fieldParram)
            {
                if (!fieldEdits.Where(x => x.FieldName.ToUpper() == item.FieldName.ToUpper()).Any())
                {
                    item.FieldID = "";
                    fieldEdits.Add(item);
                }
            }

            string validate = "";

            foreach (var field in fieldEdits)
            {
                if (field.FieldName.ToUpper() == keyEdit.ToUpper())
                {
                    continue;
                }
                if (!string.IsNullOrEmpty(keyEdit))
                {
                    var keyEditObj = JsonConvert.DeserializeObject<List<ButtonParamInfo>>(keyEdit);
                    if (keyEditObj.Where(x => x.ColumnName.ToUpper() == field.FieldName.ToUpper()).Count() > 0)
                    {
                        continue;
                    }
                }

                var checkKey = false;
                foreach (var item in model.Keys)
                {
                    var fldArr = "";
                    if (item.IndexOf("[") >= 0)
                    {
                        fldArr = item.Substring(0, item.IndexOf("["));
                    }

                    if ((item.ToUpper() == field.FieldName.ToUpper() || fldArr.ToUpper() == field.FieldName.ToUpper()) && ((field.HideWeb ?? "") != "Y" && field.FieldGroup != FLDGROUP.PARAMETER))
                    {
                        checkKey = true;
                        if (string.IsNullOrEmpty(fldArr))
                        {
                            field.FieldID = model[item].ToString().Trim();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(field.FieldID))
                            {
                                field.FieldID = field.FieldID.Trim(',') + ",";
                            }
                            field.FieldID += model[item].ToString().Trim() + ",";
                        }
                        var valid = field.ValidateFieldInfo();
                        if (!string.IsNullOrEmpty(valid))
                        {//Nếu validate trường dữ liệu có lỗi.
                            var invalidArr = valid.ToStringArray('.');
                            var fieldName = field.FieldName;
                            validate += string.Join(",", invalidArr.Select(x => fieldName + " " + x));
                        }
                    }
                }
                if (!checkKey)
                {
                    field.FieldID = "";
                }
            }
            if (!string.IsNullOrEmpty(validate))
            {
                await LoadViewBagEdit(modId, modSearchId, subModId, fieldNameEdit, keyEdit, true, 0);
                return View("Edit", validate);

            }
            if (model.Files != null)
            {
                var fileFields = model.Files.Select(x => x.Name).Distinct().ToList();
                foreach (var filefield in fileFields)
                {
                    var fieldEdit = fieldEdits.Where(x => x.FieldName.ToUpper() == filefield.ToUpper());
                    string nameFile = "";
                    var files = model.Files.Where(x => x.Name == filefield);
                    foreach (var file in files)
                    {
                        var uploads = Path.Combine(_hostingEnvironment.WebRootPath, String.Format("Uploads/{0}", modId));
                        var filePath = Path.Combine(uploads, file.FileName);
                        if (!Directory.Exists(uploads))
                        {
                            Directory.CreateDirectory(uploads);
                        }
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            nameFile += String.Format("{0}/{1},", modId, file.FileName);
                        }
                        if (Path.GetExtension(filePath).ToUpper() == ".PDF")
                        {
                            PdfHelper pdfHelper = new PdfHelper();
                            string fileNameReplace = "";
                            if (modId == ConstMod.ModEditPdf)
                            {
                                var isEdit = model["EDIT_PAGE_FILE"].ToString();
                                if (isEdit.ToUpper() == "Y")
                                {
                                    fileNameReplace = file.FileName;
                                    nameFile = "";
                                }
                            }
                            // pdfHelper.ExtractJpeg(filePath, fileNameReplace);
                        }
                    }
                    var fieldInfo = new ModuleFieldInfo();
                    if (fieldEdit.Any())
                    {
                        fieldInfo = fieldEdit.First();
                        fieldInfo.FieldID = nameFile.Trim(',');
                    }
                }

            }

            AssignParamField(keyEdit, fieldEdits);
            var modMaintain = await LoadMaintainModuleInfo(modId);
            var store = "";
            if (string.IsNullOrEmpty(keyEdit))
            {
                store = modMaintain.AddInsertStore;
            }
            else
            {
                store = modMaintain.EditUpdateStore;
            }
            var excute = (await _moduleService.SaveEditModule(modId, store, keyEdit, fieldEdits));
            if (excute.Data != "success")
            {
                var err = excute.Data.GetError();
                var errText = await GetErrText(err);
                var langText = await GetTextLang(errText);
                await LoadViewBagEdit(modId, modSearchId, subModId, fieldNameEdit, keyEdit, true, 0);
                return View("Edit", langText);
            }
            try
            {
                var note = JsonConvert.SerializeObject(fieldEdits.Select(x => string.Format("{0} = {1}", x.FieldName, x.FieldID)).ToList());
                await _logService.WriteLog(modId, (string.IsNullOrEmpty(keyEdit) ? ELogType.Insert.ToString() : ELogType.Update.ToString()), "ACTION_LOG", note.ToString(), _accessor.HttpContext.Connection.RemoteIpAddress.ToString());
            }
            catch (Exception e)
            {

            }
            if (modId == ConstMod.ModChangeModel)//Nếu Mod là mod cần reload lại Tham số Sysvar
            {
                await ReLoadSysVar();
            }
            if (string.IsNullOrEmpty(modSearchId))
            {
                return RedirectToAction("Edit", "Home", new { modId = modId, success = 1 });
            }
            return RedirectToAction("Index", "Home", new { modId = modSearchId });
        }

        private async Task<MaintainModuleInfo> LoadMaintainModuleInfo(string modId)
        {
            string key = ECacheKey.MaintainModuleInfo.ToString() + modId;
            var cachedData = _distributedCache.GetString(key);
            if (cachedData != null)
            {
                var exec = JsonConvert.DeserializeObject<List<MaintainModuleInfo>>(cachedData);
                return exec.First();
            }
            else
            {
                var exec = await _moduleService.LoadMaintainModuleInfo(modId);
                RedisUtils.SetCacheData(_distributedCache, _Configuration, exec, key);
                return exec.First();
            }
        }

        [HttpPost]
        //[RequestSizeLimit(2147483648)]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        //[RequestFormSizeLimit(valueCountLimit: 10000, Order = 1)]
        public async Task<ActionResult> Delete(string modId, string subModId, List<string> keyDels)
        {
            var outPut = new RestOutput<string>();
            var modInfo = await GetModule(modId);
            var fieldDels = CommonFunction.GetModuleFields(modInfo.FieldsInfo, modId, FLDGROUP.PARAMETER);
            foreach (var item in fieldDels)
            {//Set lại giá trị null cho field
                item.FieldID = "";
            }
            foreach (var keyDel in keyDels)
            {
                AssignParamField(keyDel, fieldDels);
            }
            //foreach (var item in fieldDels) {
            //    item.FieldID = keyDels;
            //}
            var dataExcute = await LoadExcuteModule(modId);
            if (dataExcute != null)
            {
                var excute = (await _moduleService.DeleteModule(dataExcute.ExecuteStore, fieldDels));
                outPut.ResultCode = 1;
                if (excute.Data != "success")
                {
                    var err = excute.Data.GetError();
                    var errText = await GetErrText(err);
                    outPut.Data = await GetTextLang(errText);
                    return Json(outPut);
                }
                outPut = excute;
                return Json(outPut);
            }
            return Json("outPut");
        }

        public async Task<string> GetErrText(int error)
        {
            string key = ECacheKey.ErrorInfo.ToString();
            var cachedData = _distributedCache.GetString(key);
            var lstAllError = new List<ErrorInfo>();
            if (cachedData != null)
            {
                lstAllError = JsonConvert.DeserializeObject<List<ErrorInfo>>(cachedData);
            }
            else
            {
                lstAllError = await _moduleService.GetAllError();
                RedisUtils.SetCacheData(_distributedCache, _Configuration, lstAllError, key);
            }
            if (lstAllError != null && lstAllError.Any())
            {
                var err = lstAllError.Where(x => x.ErrorCode == error);
                if (err != null && err.Any())
                {
                    return err.First().ErrorName;
                }
            }

            return "Không tìm thấy lỗi";
        }

        public async Task<string> GetTextLang(string text)
        {
            string key = ECacheKey.LanguageInfo.ToString();
            var cachedData = _distributedCache.GetString(key);
            var lsAllLanguage = new List<LanguageInfo>();
            if (cachedData != null)
            {
                lsAllLanguage = JsonConvert.DeserializeObject<List<LanguageInfo>>(cachedData);
            }
            else
            {
                lsAllLanguage = await _moduleService.GetAllTextLanguage();
                RedisUtils.SetCacheData(_distributedCache, _Configuration, lsAllLanguage, key);
            }
            if (lsAllLanguage != null && lsAllLanguage.Any())
            {
                var lang = lsAllLanguage.Where(x => x.LanguageName == "DEFERROR$" + text);
                if (lang != null && lang.Any())
                {
                    return lang.First().LanguageValue;
                }
            }
            return text;
        }

        public async Task<ExecProcModuleInfo> LoadExcuteModule(string modId)
        {
            string key = ECacheKey.ExecProcModuleInfo.ToString() + modId;
            var cachedData = _distributedCache.GetString(key);
            if (cachedData != null && cachedData != "[]")
            {
                var exec = JsonConvert.DeserializeObject<List<ExecProcModuleInfo>>(cachedData);
                return exec.First();
            }
            else
            {
                var exec = await _moduleService.LoadExcuteModule(modId);
                RedisUtils.SetCacheData(_distributedCache, _Configuration, exec, key);
                return exec.First();
            }

        }

        /// <summary>
        /// Chạy Submit tìm kiếm
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Submit(IFormCollection model, string export = "")
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login", "Home");
            }
            var modId = model["ModId"].ToString();
            var data = await GetModule(modId);
            var module = ConvertFromViewModel(data);
            ViewBag.ModuleInfo = module;
            var currPage = int.Parse(model["CurrPage"].ToString());
            var cb = module.FieldsInfo.Where(x => !String.IsNullOrEmpty(x.ListSource));
            if (cb.Any())
            {
                var sources = cb.Select(x => x.ListSource).Distinct().ToList();
                var codeInfoParram = cb.Select(x => new CodeInfoParram
                {
                    CtrlType = x.ControlType,
                    Name = x.FieldName,
                    ListSource = x.ListSource
                });
                //var para = string.Join("", sources);
                var dataCB = (await _moduleService.GetCombobox(codeInfoParram.ToList()));
                ViewBag.DataCombobox = dataCB.Data;
            }
            var modSearch = await LoadModSearchByModId(modId);
            if (modSearch != null)
            {
                var fieldEdits = CommonFunction.GetModuleFields(module.FieldsInfo, modId, FLDGROUP.SEARCH_CONDITION);
                List<string> parrams = new List<string>();

                foreach (var item in model.Keys)
                {
                    foreach (var field in fieldEdits)
                    {
                        if (item.ToUpper() == field.FieldName.ToUpper())
                        {
                            field.FieldID = model[item];
                            if (!string.IsNullOrEmpty(model[item]))
                            {
                                if ((field.FieldType == EFieldType.DEC.ToString()) || (field.FieldType == EFieldType.INT.ToString()))
                                {
                                    parrams.Add(string.Format("{0}={1}", field.FieldName, model[item].ToString().Trim()));
                                }
                                else
                                {
                                    parrams.Add(string.Format("{0} LIKE N'%{1}%'", field.FieldName, model[item].ToString().Trim()));
                                }
                            }
                        }
                        var valid = field.ValidateFieldInfo();
                        if (!string.IsNullOrEmpty(valid))
                        {//Nếu validate trường dữ liệu có lỗi.
                            var invalidArr = valid.ToStringArray('.');
                            var fieldName = field.FieldName;
                            string.Join(",", invalidArr.Select(x => fieldName + " " + x));
                        }
                    }
                }
                var query = "";
                if (modSearch.QueryFormat.IndexOf("{1}") > 0)
                {
                    if (modSearch.QueryFormat.IndexOf("{2}") > 0)
                    {
                        var paging = String.Format(" Limit {0} offset 1", CommonMethod.PageSize, (currPage - 1) * CommonMethod.PageSize + 1);
                        query = string.Format(modSearch.QueryFormat, Schema, parrams.Any() ? String.Join(" AND ", parrams) : " 1=1 ", paging);
                    }
                    else
                    {
                        query = string.Format(modSearch.QueryFormat, Schema, parrams.Any() ? String.Join(" AND ", parrams) : " 1=1 ");
                    }
                }
                else
                {
                    query = String.Format(modSearch.QueryFormat, Schema);
                }

                ViewBag.CurrPage = currPage;
                ViewBag.FieldSubmited = fieldEdits;
                var dataGrid = await _moduleService.LoadQueryModule(new ParramModuleQuery { Store = query });
                if (!string.IsNullOrEmpty(export))
                {
                    string pathSaveAs = Path.Combine(_hostingEnvironment.WebRootPath, String.Format("FileTemplate/TemplateExport_{0}.xls", DateTime.Now.ToString("dd-MM-yyyy")));
                    Data2ExcelFile(dataGrid, module.FieldsInfo, module, pathSaveAs);
                    return DownloadFile(pathSaveAs);
                }
                ViewBag.DataSearch = dataGrid;
                int userId = int.Parse("0" + HttpContext.Session.GetString("UserId"));
                var groupModUser = await _moduleService.GetGroupModByUserId(userId);
                ViewBag.RoleUser = groupModUser;
            }
            return View("Search", modId);
        }


        /// <summary>
        /// Chạy Submit tìm kiếm
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SubmitDinamic(IFormCollection model, string export = "")
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
            {
                return RedirectToAction("Login", "Home");
            }
            var modId = model["ModId"].ToString();
            var data = await GetModule(modId);
            var module = ConvertFromViewModel(data);
            ViewBag.ModuleInfo = module;
            var currPage = int.Parse(model["CurrPage"].ToString());
            var cb = module.FieldsInfo.Where(x => !String.IsNullOrEmpty(x.ListSource));
            if (cb.Any())
            {
                var sources = cb.Select(x => x.ListSource).Distinct().ToList();
                var codeInfoParram = cb.Select(x => new CodeInfoParram
                {
                    CtrlType = x.ControlType,
                    Name = x.FieldName,
                    ListSource = x.ListSource
                });
                //var para = string.Join("", sources);
                var dataCB = (await _moduleService.GetCombobox(codeInfoParram.ToList()));
                ViewBag.DataCombobox = dataCB.Data;
            }

            var staticConditionInstances = new List<SearchConditionInstance>();

            int i = 0;

            while (!String.IsNullOrEmpty(model["Conditions[" + i + "].ConditionID"].ToString()))
            {
                staticConditionInstances.Add(new SearchConditionInstance()
                {
                    ConditionID = model[String.Format("Conditions[{0}].ConditionID", i)].ToString(),
                    Value = model[String.Format("Conditions[{0}].Value", i)]
                });
                i++;
            }


            var modSearch = await LoadModSearchByModId(modId);
            #region Call kiểu cũ
            var searchModSearch = new SearchModSearch()
            {
                ModInfo = module.ModulesInfo,
                SearchInfo = modSearch,
                StaticConditionInstances = staticConditionInstances
            };
            var dataSearch = await _moduleService.SearchModSearch(searchModSearch);
            /*
             if (modSearch != null)
            {
                var fieldEdits = CommonFunction.GetModuleFields(module.FieldsInfo, modId, FLDGROUP.SEARCH_CONDITION);
                List<string> parrams = new List<string>();

                foreach (var item in model.Keys)
                {
                    foreach (var field in fieldEdits)
                    {
                        if (item.ToUpper() == field.FieldName.ToUpper())
                        {
                            field.FieldID = model[item];
                            if (!string.IsNullOrEmpty(model[item]))
                            {
                                if ((field.FieldType == EFieldType.DEC.ToString()) || (field.FieldType == EFieldType.INT.ToString()))
                                {
                                    parrams.Add(string.Format("{0}={1}", field.FieldName, model[item].ToString().Trim()));
                                }
                                else
                                {
                                    parrams.Add(string.Format("{0} LIKE N'%{1}%'", field.FieldName, model[item].ToString().Trim()));
                                }
                            }
                        }
                        var valid = field.ValidateFieldInfo();
                        if (!string.IsNullOrEmpty(valid))
                        {//Nếu validate trường dữ liệu có lỗi.
                            var invalidArr = valid.ToStringArray('.');
                            var fieldName = field.FieldName;
                            string.Join(",", invalidArr.Select(x => fieldName + " " + x));
                        }
                    }
                }
                var query = "";
                if (modSearch.QueryFormat.IndexOf("{1}") > 0)
                {
                    if (modSearch.QueryFormat.IndexOf("{2}") > 0)
                    {
                        var paging = String.Format(" Limit {0} offset 1", CommonMethod.PageSize, (currPage - 1) * CommonMethod.PageSize + 1);
                        query = string.Format(modSearch.QueryFormat, Schema, parrams.Any() ? String.Join(" AND ", parrams) : " 1=1 ", paging);
                    }
                    else
                    {
                        query = string.Format(modSearch.QueryFormat, Schema, parrams.Any() ? String.Join(" AND ", parrams) : " 1=1 ");
                    }
                }
                else
                {
                    query = String.Format(modSearch.QueryFormat, Schema);
                }

                ViewBag.CurrPage = currPage;
                ViewBag.FieldSubmited = fieldEdits;
                var dataGrid = await _moduleService.LoadQueryModule(new ParramModuleQuery { Store = query });
                if (!string.IsNullOrEmpty(export))
                {
                    string pathSaveAs = Path.Combine(_hostingEnvironment.WebRootPath, String.Format("FileTemplate/TemplateExport_{0}.xls", DateTime.Now.ToString("dd-MM-yyyy")));
                    Data2ExcelFile(dataGrid, module.FieldsInfo, module, pathSaveAs);
                    return DownloadFile(pathSaveAs);
                }
                ViewBag.DataSearch = dataGrid;
                int userId = int.Parse("0" + HttpContext.Session.GetString("UserId"));
                var groupModUser = await _moduleService.GetGroupModByUserId(userId);
                ViewBag.RoleUser = groupModUser;
             */
            #endregion
            return View("ModSearch", modId);
        }

        private void Data2ExcelFile(List<dynamic> data, List<ModuleFieldInfo> moduleFieldInfo, ModuleInfoModel moduleInfoModel, string pathSaveAs)
        {
            //string path = Path.Combine(_hostingEnvironment.WebRootPath, "FileTemplate/TemplateExport.xls");
            //using (var package = new ExcelPackage(new FileInfo(path)))
            //{
            //    // var package = new ExcelPackage(new FileInfo(path));
            //    ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
            //    int index = 2;
            //    if (data != null)
            //    {
            //        //foreach (var item in data.Data.Data)
            //        var fieldGrid = moduleFieldInfo.Where(x => x.FieldGroup == FLDGROUP.SEARCH_COLUMN).ToList();
            //        int column = 1;
            //        foreach (var item in fieldGrid)
            //        {//Fill Header
            //            if (item.HideWeb == "Y")
            //            {
            //                continue;
            //            }
            //            workSheet.Cells[index, column].Value = item.FieldName.GetLanguage(moduleInfoModel.LanguageInfo, moduleInfoModel.ModulesInfo.ModuleName);
            //            workSheet.Cells[index, column].Style.Font.Bold = true;
            //            workSheet.Cells[index, column].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            //            workSheet.Cells[index, column].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            //            workSheet.Cells[index, column].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            //            workSheet.Cells[index, column].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            //            column++;
            //        }
            //        index++;
            //        foreach (var item in data)
            //        {
            //            var dataRows = ((Newtonsoft.Json.Linq.JContainer)item);
            //            column = 1;
            //            foreach (var col in fieldGrid)
            //            {
            //                if (col.HideWeb == "Y")
            //                {
            //                    continue;
            //                }

            //                foreach (var columnData in dataRows)
            //                {
            //                    var columnName = ((Newtonsoft.Json.Linq.JProperty)columnData).Name;
            //                    if (columnName.ToUpper() == col.FieldName.ToUpper())
            //                    {
            //                        var valueExcel = (((Newtonsoft.Json.Linq.JValue)((Newtonsoft.Json.Linq.JProperty)columnData).Value).Value);
            //                        workSheet.Cells[index, column].Value = valueExcel;
            //                        column++;
            //                    }
            //                }
            //            }
            //            index++;
            //        }
            //    }
            //    package.SaveAs(new FileInfo(pathSaveAs));
            //}
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        public async Task<IActionResult> RowGridModMaintain(string modId, List<ModuleFieldInfo> fieldInfos)
        {
            var model = new RowGridModMaintainModel();
            model.Fields = fieldInfos;
            var data = await GetModule(modId);
            //var module = ConvertFromViewModel(data);
            //model.ModuleInfo = module;
            return PartialView("~/Views/Home/RowGridModMaintain.cshtml", model);
        }
        [HttpPost]
        public async Task SendMesseage()
        {
        }
        public IActionResult Test()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userService.GetUserByUserNamePassword(model.UserName, model.Password);
            if (user == null)
            {
                model.Err = "Username hoặc Password không chính xác";
                return View(model);
            }
            HttpContext.Session.SetString("DisplayName", user.DisplayName);
            HttpContext.Session.SetString("UserName", user.Username);
            HttpContext.Session.SetString("UserId", user.UserID.ToString());
            return RedirectToAction("Index");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("DisplayName", string.Empty);
            HttpContext.Session.SetString("UserName", string.Empty);
            HttpContext.Session.SetString("UserId", string.Empty);
            return RedirectToAction("Login");
        }
    }
}
