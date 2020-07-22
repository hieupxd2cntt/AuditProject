using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Core.DataAccess;
using Microsoft.Extensions.Configuration;
using Npgsql;
using WebApiCore.Common;
using WebApiCore.Controllers;
using WebCore.Common;
using WebCore.Entities;
using WebCore.Utils;
using WebModelCore;
using WebModelCore.ModTreeViewModel;

namespace WebApi.Controllers
{
    public class CoreController : BaseController
    {
        public CoreController(IConfiguration configuration) : base(configuration)
        {
            Session = new Session();
        }


        public Session Session { get; set; }
        private string Schema = "audit";

        public void CreateUserSession(out Session session, string userName, string password, string clientIP, string clientMacAddress)
        {
            session = null;
            try
            {
                session = null;
                CreateUserSession(out session, userName, password, clientIP, null, clientMacAddress);
            }

            catch (Exception ex)
            {
                //throw ErrorUtils.CreateError(ex);
            }
        }
        public void CreateUserSession(out Session session, string userName, string password, string clientIP, string dnsName, string clientMacAddress)
        {
            session = null;
            try
            {

                session = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<Session>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.CREATE_NEW_SESSION, userName, password, clientIP)[0];

                session.SessionKey = CommonUtils.MD5Standard(session.SessionID.ToString());
                session.ClientIP = clientIP;
                session.DNSName = dnsName;
                session.ClientMacAddress = clientMacAddress;
                //SQLHelper.ExecuteStoreProcedure(ConnectionString, new Session(), SYSTEM_STORE_PROCEDURES.UPDATE_SESSION_INFO,
                //    session.SessionID,
                //    session.SessionKey,
                //    session.ClientIP,
                //    session.DNSName,
                //    session.ClientMacAddress);
            }
            catch (Exception ex)
            {
                //throw ErrorUtils.CreateError(ex);
            }
        }


        public void InitializeSessionID(string sessionID)
        {
            if (sessionID != null)
            {
                var sessions = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<Session>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.GET_SESSION_INFO, sessionID);

                if (sessions.Count == 1)
                {
                    Session = sessions[0];
                    if (Session.SessionStatus == WebCore.CODES.SESSIONS.SESSIONSTATUS.SESSION_TERMINATED)
                    {
                        if (Session.Username != Session.TerminatedUsername)
                        {
                            //throw ErrorUtils.CreateErrorWithSubMessage(ERR_SYSTEM.ERR_SYSTEM_SESSION_TERMINATED_BY_ADMIN,
                            //    Session.TerminatedUsername + ": " + Session.Description);
                        }
                        //throw ErrorUtils.CreateError(ERR_SYSTEM.ERR_SYSTEM_SESSION_TERMINATED_BY_SELF);
                    }

                    if (Session.SessionStatus == WebCore.CODES.SESSIONS.SESSIONSTATUS.SESSION_TIMEOUT)
                    {
                        //throw ErrorUtils.CreateError(ERR_SYSTEM.ERR_SYSTEM_SESSION_TIMEOUT);
                    }
                }
                else
                {
                    //throw ErrorUtils.CreateError(ERR_SYSTEM.ERR_SYSTEM_SESSION_NOT_EXISTS_OR_DUPLICATE);
                }
            }
        }

        public void ForceLoadModule(
           out List<ModuleInfo> modulesInfo,
           out List<ModuleFieldInfo> fieldsInfo,
           out List<ButtonInfo> buttonsInfo,
           out List<ButtonParamInfo> buttonParamsInfo,
           out List<LanguageInfo> languageInfo,
           out List<OracleParam> oracleParamsInfo,
           string moduleID)
        {
            modulesInfo = null;
            fieldsInfo = null;
            buttonsInfo = null;
            buttonParamsInfo = null;
            languageInfo = null;
            oracleParamsInfo = null;
            try
            {
                modulesInfo = new List<ModuleInfo>();
                modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<ModuleInfo>(ConnectionString, SYSTEM_STORE_PROCEDURES.GET_STATIC_MODULE, moduleID).ToArray());
                //modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<ModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_BATCH_MODULE, moduleID).ToArray());
                //modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<StatisticsModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_STATISTICS_MODULE, moduleID).ToArray());
                modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<MaintainModuleInfo>(ConnectionString, SYSTEM_STORE_PROCEDURES.GET_MAINTAIN_MODULE, moduleID).ToArray());
                //modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<MaintainModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_MAINTAINPOS_MODULE, moduleID).ToArray());
                //modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<ReportModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_REPORT_MODULE, moduleID).ToArray());
                //modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<ChartModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_CHART_MODULE, moduleID).ToArray());
                //modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<SearchModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_SEARCHMASTER_MODULE, moduleID).ToArray());
                //modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<SwitchModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_SWITCH_MODULE, moduleID).ToArray());
                //modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<ImportModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_IMPORT_MODULE, moduleID).ToArray());
                //modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<ExecProcModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_EXECUTEPROC_MODULE, moduleID).ToArray());
                //modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<AlertModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_ALERT_MODULE, moduleID).ToArray());
                //modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<ModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_TREE_MODULE, moduleID).ToArray());
                //modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<ModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_EXPRESSION_MODULE, moduleID).ToArray());
                ////POS
                //modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<MaintainModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_ORDER_MODULE, moduleID).ToArray());
                fieldsInfo = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<ModuleFieldInfo>(ConnectionString, SYSTEM_STORE_PROCEDURES.LIST_FIELD_INFO_BY_MODID, moduleID);
                buttonsInfo = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<ButtonInfo>(ConnectionString, SYSTEM_STORE_PROCEDURES.LIST_BUTTON_BY_MODID, moduleID);
                buttonParamsInfo = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<ButtonParamInfo>(ConnectionString, SYSTEM_STORE_PROCEDURES.LIST_BUTTON_PARAM_BY_MODID, moduleID);
                languageInfo = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<LanguageInfo>(ConnectionString, SYSTEM_STORE_PROCEDURES.LIST_LANGUAGE_BY_MODID, moduleID);
                //PostgresqlHelper postgresqlHelper = new PostgresqlHelper();
                var stores = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<OracleStore>(ConnectionString, SYSTEM_STORE_PROCEDURES.LIST_STOREPROC_BY_MODID, moduleID);
            }
            catch (Exception ex)
            {
                //throw ErrorUtils.CreateError(ex);
            }
        }

        public List<MaintainModuleInfo> LoadMainTainModule(string modId)
        {
            var stores = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<MaintainModuleInfo>(ConnectionString, SYSTEM_STORE_PROCEDURES.DEV_SP_MODMAINTAIN_SELBY_MODID, modId);
            return stores;
        }
        public List<ModTreeView> LoadAllModTreeView()
        {
            var stores = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<ModTreeView>(ConnectionString, SYSTEM_STORE_PROCEDURES.ModTreeView_SelectAll);
            return stores;
        }


        public List<CodeInfo> LoadDefModByTypeValue(string defModValue)
        {
            var stores = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<CodeInfo>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.DEFCODE_SelectByTypeValue, defModValue);
            return stores;
        }
        public List<SearchModuleInfo> LoadModSearchByModId(string defModValue)
        {
            var stores = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<SearchModuleInfo>(ConnectionString, SYSTEM_STORE_PROCEDURES.DEV_SP_MODSEARCH_SEL_BY_MODID, defModValue);
            return stores;
        }
        public object RunStoreToDataTable(string storeName, List<NpgsqlParameter> parrams)
        {
            try
            {
                var postgresqlHelper = new PostgresqlHelper();
                var data = postgresqlHelper.LoadDataTable(ConnectionString, storeName, parrams);
                return data;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public object RunStoreToDataTable(string storeName)
        {
            try
            {
                var postgresqlHelper = new PostgresqlHelper();
                var sqlCommand = CreateNpgsqlCommand(storeName);
                var data = postgresqlHelper.LoadDataTable(sqlCommand, storeName);
                return data;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public List<ExecProcModuleInfo> LoadModExecProcByModId(string modId)
        {
            try
            {
                var stores = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<ExecProcModuleInfo>(ConnectionString, SYSTEM_STORE_PROCEDURES.DEV_MODEXECPROC_SEL_BY_MODID, modId);
                return stores;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public string RunStore(string storeName, List<string> parrams, string userName)
        {
            try
            {
                PostgresqlHelper postgresqlHelper = new PostgresqlHelper();
                var command = CreateNpgsqlCommand(storeName);
                //postgresqlHelper.ExecuteNonQuery(ConnectionString, storeName, userName, parrams.ToArray());
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

        public string RunFunction(string storeName, List<NpgsqlParameter> parrams)
        {
            //storeName = String.Format("select * from {0}.{1}(" + String.Join(",", parrams.Select(x =>"@"+ x.ParameterName)) + ")",Schema,storeName);
            storeName = String.Format("select * from {0}(" + String.Join(",", parrams.Select(x => "@" + x.ParameterName)) + ")", storeName);
            var command = CreateNpgsqlCommand(storeName, parrams, false);
            try
            {

                PostgresqlHelper postgresqlHelper = new PostgresqlHelper();
                command.Connection.Open();
                postgresqlHelper.ExecuteNonQuery(command);
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            finally
            {
                command.Connection.Close();
            }

        }
        public string RunStore(string storeName, List<NpgsqlParameter> parrams)
        {
            try
            {
                var cm = new WebApiCore.CommonFunction();
                PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<object>(ConnectionString, storeName, parrams.ToArray()).ToArray();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }
        public List<NpgsqlParameter> DiscoveryParameters(string store)
        {
            var lst = new List<NpgsqlParameter>();
            PostgresqlHelper.DiscoveryParameters(ConnectionString, store, lst);
            return lst;
        }
        /// <summary>
        /// Tạo NpgsqlCommand
        /// </summary>
        /// <param name="store">Tên store</param>
        /// <param name="parameters">List parameter</param>
        /// <param name="isStoreProcedure">=true là CommandType=CommandType.StoredProcedure, false là mặc định </param>
        /// <returns></returns>
        private NpgsqlCommand CreateNpgsqlCommand(string store, List<NpgsqlParameter> parameters = null, bool isStoreProcedure = true)
        {
            var connectionString = Configuration["ConfigApp:DbContext"];
            var conn = new NpgsqlConnection(connectionString);
            var npgsqlCommand = new NpgsqlCommand(store, conn);
            if (isStoreProcedure)
                npgsqlCommand.CommandType = CommandType.StoredProcedure;
            if (parameters != null && parameters.Any())
            {
                foreach (var param in parameters)
                {
                    npgsqlCommand.Parameters.Add((NpgsqlParameter)param.Clone());
                }
            }

            return npgsqlCommand;
        }

        public ModuleExcuteViewModel LoadExcuteModule(string modId)
        {
            var data = new ModuleExcuteViewModel();
            var maintain = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<MaintainModuleInfo>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.DEV_SP_MODMAINTAIN_SELBY_MODID, modId);
            var search = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<SearchModuleInfo>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.DEV_SP_MODSEARCH_SEL_BY_MODID, modId);
            var excec = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<ExecProcModuleInfo>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.DEV_MODEXECPROC_SEL_BY_MODID, modId);
            if (excec.Any())
            {
                data.ExecProcModuleInfo = excec.First();
            }
            if (search.Any())
            {
                data.SearchModuleInfo = search.First();
            }
            if (maintain.Any())
            {
                data.MaintainModuleInfo = maintain.First();
            }
            return data;
        }
        public object LoadProcedure(string query, object[] parr)
        {
            var postgresqlHelper = new PostgresqlHelper();
            var data = postgresqlHelper.Scalar(CreateNpgsqlCommand(query, null, false));
            return data;
        }

        public object LoadProcedureDynamicQuery(ParramModuleQueryDynamicQuery query)
        {
            var postgresqlHelper = new PostgresqlHelper();
            var npgsqlCommand = new NpgsqlCommand();
            BuildQuerySearch build = new BuildQuerySearch(ConnectionString);

            build.BuildStaticConditions(query.SearchModuleInfo, npgsqlCommand, query.SearchConditionInstances);
            var data = postgresqlHelper.Scalar(npgsqlCommand);
            return data;
        }
        public DataSet ExcuteStore2DataSet(string query, string value)
        {
            var postgresqlHelper = new PostgresqlHelper();
            var parrams = DiscoveryParameters(query);
            var checkParram = parrams.Where(x => x.NpgsqlDbType != NpgsqlTypes.NpgsqlDbType.Refcursor);
            if (checkParram != null && checkParram.Any())
            {
                if (!string.IsNullOrEmpty(value))
                {
                    checkParram.First().Value = value;
                }

            }

            var command = CreateNpgsqlCommand(query, parrams);
            var data = postgresqlHelper.Scalar(command);
            return new DataSet();// data;
        }

        //public List<NpgsqlParameter> DiscoveryParameters(string store)
        //{
        //    var lst = new List<NpgsqlParameter>();
        //    PostgresqlHelper.DiscoveryParameters(ConnectionString, store, lst);
        //    return lst;
        //}

        public List<OracleParam> LoadParamByStore(string store)
        {
            List<OracleParam> lst = new List<OracleParam>();
            //PostgresqlHelper.DiscoveryParameters(ConnectionString, store, lst);
            return lst;
        }

        public List<User> GetUserByUserNamePassword(string username, string password)
        {
            var lst = new List<string>();
            lst.Add(username);
            lst.Add(password);
            return PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<User>(ConnectionString, SYSTEM_STORE_PROCEDURES.GET_USER_BY_USERNAME_PASSWORD, lst.ToArray());
        }
        public List<ErrorInfo> LoadAllErrorInfo()
        {
            var stores = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<ErrorInfo>(ConnectionString, SYSTEM_STORE_PROCEDURES.DEFERROR_SELECT_ALL, "");
            return stores;
        }
        public List<DefTasks> GetAllDefTask()
        {
            var stores = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<DefTasks>(ConnectionString, SYSTEM_STORE_PROCEDURES.DEFTASKS_SELECT_ALL, "");
            return stores;
        }

        public List<ModWorkflow> GetAllModWorkflow()
        {
            var stores = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<ModWorkflow>(ConnectionString, SYSTEM_STORE_PROCEDURES.DEFMODWORKFLOW_SELECT_ALL, "");
            return stores;
        }
        public List<LanguageInfo> GetAllLanguageText()
        {
            var stores = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<LanguageInfo>(ConnectionString, SYSTEM_STORE_PROCEDURES.DEFLANG_SELECT_TEXT_LANG, "");
            return stores;
        }
        public List<LanguageInfo> GetAllBtnLanguageText()
        {
            var stores = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<LanguageInfo>(ConnectionString, SYSTEM_STORE_PROCEDURES.DEFLANG_SELECT_BTN_LANG, "");
            return stores;
        }
        public List<CodeInfo> GetAllCodeInfo()
        {
            var stores = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<CodeInfo>(ConnectionString, SYSTEM_STORE_PROCEDURES.DEFCODE_SelectAll, "");
            return stores;
        }

        public List<SysVar> GetAllSysVar()
        {
            var stores = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<SysVar>(ConnectionString, SYSTEM_STORE_PROCEDURES.SYSVAR_SelectAll, "");
            return stores;
        }
        public List<MenuItemInfo> GetAllMenu(int userId)
        {
            var stores = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<MenuItemInfo>(ConnectionString, SYSTEM_STORE_PROCEDURES.Menu_SelectAll, userId);
            return stores;
        }
        public List<LanguageInfo> GetAllLanguageIcon()
        {
            var stores = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<LanguageInfo>(ConnectionString, SYSTEM_STORE_PROCEDURES.DEFLANG_SelectAllIcon_MenuText, "");
            return stores;
        }

        public List<GroupMod> GetGroupModByUserId(string userId)
        {
            var stores = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<GroupMod>(ConnectionString, SYSTEM_STORE_PROCEDURES.DEFLANG_SelectRoleByUserId, userId);
            return stores;
        }

        public List<TreeviewInfo> GetDataTreeviewInfo(string store, object[] param)
        {
            try
            {
                if (param!=null && param.Any())
                {
                    var stores = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<TreeviewInfo>(ConnectionString, store, param);
                    return stores;
                }
                else
                {
                    var stores = PostgresqlHelper.ExecuteStoreProcedurePostgreSQL<TreeviewInfo>(ConnectionString, store,"");
                    return stores;
                }
                
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}

