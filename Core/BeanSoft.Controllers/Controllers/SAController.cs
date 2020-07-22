using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices.Protocols;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text.RegularExpressions;
using Core;
using Core.Base;
using Core.Common;
using Core.Entities;
using Core.Utils;
using Npgsql;
using NpgsqlTypes;

namespace Core.Controllers
{
    [ServiceContract(Name = "ISAController", Namespace = "http://rbs.vn/")]
    public class SAController : ControllerBase
    {
        private static readonly List<SearchResult> CachedSearchResult = new List<SearchResult>();
        public Session Session { get; set; }

#if DEBUG
        public static Dictionary<string, string> ModulesLog { get; set; }
        static SAController()
        {
            ModulesLog = new Dictionary<string, string>();
        }

        [OperationContract]
        public string GetSystemLog(bool clearLog)
        {
            if (clearLog) ModulesLog.Clear();

            //            var html = ModulesLog.Aggregate(@"<head>
            //	<style>
            //		body {background: black; color: white}
            //		table{border-collapse:collapse;}
            //		th, td { font-family: Tahoma; font-size: 8pt; border: 1px solid #333333;padding: 2px 5px;border-collapse: collapse}
            //        th {width: 200px; text-align: left;}
            //		th.mh {width: auto; text-align: center;}
            //        .g{color: lightgreen;}
            //        .y{color: yellow;}
            //	</style>
            //</head>
            //<body>", (current, pair) => current + string.Format(@"<table cellspacing=0 cellpadding=0 width=100%>
            //		<thead>
            //			<tr>
            //				<th class=mh colspan=2>{0}</th>
            //			</tr>
            //		</thead>
            //		<tbody>{1}
            //		</tbody>
            //	</table><br/>", pair.Key, pair.Value));
            //            html += @"</body>";

            var html = @"<head>
	            <style>
		            body {background: black; color: white}
		            table{border-collapse:collapse;}
		            th, td { font-family: Tahoma; font-size: 8pt; border: 1px solid #333333;padding: 2px 5px;border-collapse: collapse}
                    th {width: 200px; text-align: left;}
		            th.mh {width: auto; text-align: center;}
                    .g{color: lightgreen;}
                    .y{color: yellow;}
	            </style>
            </head>
            <body>
            < table cellspacing = 0 cellpadding = 0 width = 100 %>
     
                         < thead >
     
                             < tr >
     
                                 < th class=mh colspan = 2 >test</th>
			            </tr>
		            </thead>
		            <tbody>xxx
		            </tbody>
	            </table><br/>";
            html += @"</body>";
            return html;
        }

        public void WriteLog(string logKey, string name, string value)
        {
            WriteLog(logKey, name, value, "normal");
        }

        public void WriteLog(string logKey, string name, string value, string specialClass)
        {
            try
            {
                ModulesLog[logKey] += string.Format("<tr><th class=" + specialClass + ">{0}</th><td class=" + specialClass + ">{1}</td></tr>", name, value);
            }
            catch
            {
            }
        }
#endif
        [OperationContract]
        public void GetServerInfo(out ServerInfo serverInfo, out CachedHashInfo cachedInfoHash, string clientLanguageID)
        {
#if DEBUG
            App.Environment.InitializeEnvironment();
#endif
            serverInfo = App.Environment.ServerInfo;
            cachedInfoHash = App.Environment.CachedHashInfo;
        }

        [OperationContract]
        public void GetCurrentSessionInfo(out Session session)
        {
            session = Session;
        }

        [OperationContract]
        public void InitializeSessionID(string sessionID)
        {
            if (sessionID != null)
            {
                var sessions = PostgresqlHelper.ExecuteStoreProcedure<Session>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.GET_SESSION_INFO, sessionID);

                if (sessions.Count == 1)
                {
                    Session = sessions[0];
                    if (Session.SessionStatus == Core.CODES.SESSIONS.SESSIONSTATUS.SESSION_TERMINATED)
                    {
                        if (Session.Username != Session.TerminatedUsername)
                        {
                            throw ErrorUtils.CreateErrorWithSubMessage(ERR_SYSTEM.ERR_SYSTEM_SESSION_TERMINATED_BY_ADMIN,
                                Session.TerminatedUsername + ": " + Session.Description);
                        }
                        throw ErrorUtils.CreateError(ERR_SYSTEM.ERR_SYSTEM_SESSION_TERMINATED_BY_SELF);
                    }

                    if (Session.SessionStatus == Core.CODES.SESSIONS.SESSIONSTATUS.SESSION_TIMEOUT)
                    {
                        throw ErrorUtils.CreateError(ERR_SYSTEM.ERR_SYSTEM_SESSION_TIMEOUT);
                    }
                }
                else
                {
                    throw ErrorUtils.CreateError(ERR_SYSTEM.ERR_SYSTEM_SESSION_NOT_EXISTS_OR_DUPLICATE);
                }
            }
        }

        [OperationContract]
        public void GetSessionUserInfo(out User userInfo)
        {
            try
            {
                userInfo = PostgresqlHelper.ExecuteStoreProcedure<User>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.GET_SESSION_USER_INFO, Session.Username)[0];
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void CheckRole(ModuleInfo moduleInfo)
        {
            if (!string.IsNullOrEmpty(moduleInfo.RoleID))
            {
                if (Session == null) throw ErrorUtils.CreateError(ERR_SYSTEM.ERR_SYSTEM_MODULE_NOT_ALLOW_ACCESS);
                if (Session.Type == CONSTANTS.USER_TYPE_UBCKNN)
                {
                    PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, null, SYSTEM_STORE_PROCEDURES.CHECK_USER_ROLE, Session.UserID, moduleInfo.RoleID);
                }
            }
        }

        [OperationContract]
        public void ListCurrentRoles(out List<Role> roles)
        {
            _ListUserRoles(out roles, Session.UserID);
        }

        public List<CodeInfo> BuildCodesInfo()
        {
            try
            {
                return PostgresqlHelper.ExecuteStoreProcedure<CodeInfo>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.LIST_DEFCODE);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ListCodesInfo(out List<CodeInfo> codesInfo)
        {
            codesInfo = AllCaches.CodesInfo;
        }

        public List<ErrorInfo> BuildErrorsInfo()
        {
            try
            {
                return PostgresqlHelper.ExecuteStoreProcedure<ErrorInfo>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.LIST_DEFERROR);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ListErrorsInfo(out List<ErrorInfo> errorsInfo)
        {
            errorsInfo = AllCaches.BaseErrorsInfo;
        }

        public List<ValidateInfo> BuildValidatesInfo()
        {
            try
            {
                return PostgresqlHelper.ExecuteStoreProcedure<ValidateInfo>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.LIST_DEFVALIDATE);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ListValidatesInfo(out List<ValidateInfo> validatesInfo)
        {
            validatesInfo = AllCaches.BaseValidatesInfo;
        }

        public List<LanguageInfo> BuildLanguageInfo()
        {
            try
            {
                return PostgresqlHelper.ExecuteStoreProcedure<LanguageInfo>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.LIST_DEFLANG);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ListLanguage(out List<LanguageInfo> languageInfos, string languageID)
        {
            //languageInfos = (from language in AllCaches.BaseLanguageInfo
            //                 where language.LanguageID == languageID && language.AppType == apptype || language.BrType == apptype
            //                 select language).ToList();
            languageInfos = (from language in AllCaches.BaseLanguageInfo
                             where language.LanguageID == languageID
                             select language).ToList();
        }

        [OperationContract]
        public void ListMenuItems(out List<MenuItemInfo> menuItems)
        {
            try
            {
#if DEBUG
                menuItems = PostgresqlHelper.ExecuteStoreProcedure<MenuItemInfo>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.LIST_DEFMENU, "Y");
#else
                menuItems = PostgresqlHelper.ExecuteStoreProcedure<MenuItemInfo>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.LIST_DEFMENU, "N");
#endif
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ListRibbonItems(out List<RibbonItemInfo> ribbonItems)
        {
            try
            {
#if DEBUG
                ribbonItems = PostgresqlHelper.ExecuteStoreProcedure<RibbonItemInfo>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.LIST_DEFRIBBON, "Y");
#else
                ribbonItems = PostgresqlHelper.ExecuteStoreProcedure<RibbonItemInfo>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.LIST_DEFRIBBON, "N");
#endif
                var checkedRibbonItems = new List<RibbonItemInfo>();
                // LongND5: First add all item no child
                foreach (var ribbonParent in ribbonItems)
                {
                    var count = 0;
                    foreach (var ribbonChild in ribbonItems)
                    {
                        if (ribbonParent.RibbonID == ribbonChild.RibbonOwnerID)
                        {
                            count++;
                        }
                    }

                    if (count == 0)
                    {
                        try
                        {
                            if (ribbonParent.ModuleID != null)
                            {
                                CheckRole(ModuleUtils.GetModuleInfo(ribbonParent.ModuleID, ribbonParent.SubModule));
                            }
                            checkedRibbonItems.Add(ribbonParent);
                        }
                        catch
                        {
                        }
                    }
                }

                // Now add parent
                var stop = false;
                while (!stop)
                {
                    stop = true;
                    var tmp = new RibbonItemInfo[checkedRibbonItems.Count];
                    checkedRibbonItems.CopyTo(tmp);
                    foreach (var ribbonChild in tmp)
                    {
                        foreach (var ribbonParent in ribbonItems)
                        {
                            if (!checkedRibbonItems.Contains(ribbonParent) && ribbonParent.RibbonID == ribbonChild.RibbonOwnerID)
                            {
                                checkedRibbonItems.Add(ribbonParent);
                                stop = false;
                            }
                        }
                    }
                }

                ribbonItems = checkedRibbonItems;
                //ribbonItems.Sort();
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        public List<ModuleInfo> BuildModulesInfo()
        {
            try
            {
                var moduleInfos = new List<ModuleInfo>();
                moduleInfos.AddRange(PostgresqlHelper.ExecuteStoreProcedure<ModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_STATIC_MODULE).ToArray());
                moduleInfos.AddRange(PostgresqlHelper.ExecuteStoreProcedure<ModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_BATCH_MODULE).ToArray());
                moduleInfos.AddRange(PostgresqlHelper.ExecuteStoreProcedure<StatisticsModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_STATISTICS_MODULE).ToArray());
                moduleInfos.AddRange(PostgresqlHelper.ExecuteStoreProcedure<MaintainModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_MAINTAIN_MODULE).ToArray());
                moduleInfos.AddRange(PostgresqlHelper.ExecuteStoreProcedure<ChartModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_CHART_MODULE).ToArray());
                moduleInfos.AddRange(PostgresqlHelper.ExecuteStoreProcedure<SearchModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_SEARCHMASTER_MODULE).ToArray());
                moduleInfos.AddRange(PostgresqlHelper.ExecuteStoreProcedure<SwitchModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_SWITCH_MODULE).ToArray());
                moduleInfos.AddRange(PostgresqlHelper.ExecuteStoreProcedure<ImportModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_IMPORT_MODULE).ToArray());
                moduleInfos.AddRange(PostgresqlHelper.ExecuteStoreProcedure<ExecProcModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_EXECUTEPROC_MODULE).ToArray());
                moduleInfos.AddRange(PostgresqlHelper.ExecuteStoreProcedure<AlertModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_ALERT_MODULE).ToArray());
                moduleInfos.AddRange(PostgresqlHelper.ExecuteStoreProcedure<ReportModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_REPORT_MODULE).ToArray());
                moduleInfos.AddRange(PostgresqlHelper.ExecuteStoreProcedure<ModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_TREE_MODULE).ToArray());
                moduleInfos.AddRange(PostgresqlHelper.ExecuteStoreProcedure<ModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_EXP_MODULE).ToArray());
                moduleInfos.AddRange(PostgresqlHelper.ExecuteStoreProcedure<MaintainModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_WORKFLOW_MODULE).ToArray());
                //moduleInfos.AddRange(PostgresqlHelper.ExecuteStoreProcedure<MaintainModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_TRANSACT_MODULE).ToArray());
                moduleInfos.AddRange(PostgresqlHelper.ExecuteStoreProcedure<DashboardInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_DASHBOARD_MODULE).ToArray());

                return moduleInfos;
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ListModuleInfo(out List<ModuleInfo> moduleInfos)
        {
            moduleInfos = AllCaches.ModulesInfo;
        }

        [OperationContract]
        public void ListBatchInfo(out List<BatchInfo> moduleInfos, string moduleID)
        {
            try
            {
                moduleInfos = new List<BatchInfo>();
                moduleInfos.AddRange(PostgresqlHelper.ExecuteStoreProcedure<BatchInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_BATCH_INFO, moduleID).ToArray());
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        public List<ModuleFieldInfo> BuildModuleFieldsInfo()
        {
            try
            {
                return PostgresqlHelper.ExecuteStoreProcedure<ModuleFieldInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_FIELD_INFO);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ListModuleField(out List<ModuleFieldInfo> moduleFields, out int endRow, int startRow, string apptype)
        {
            const int MAX_FIELD_TO_TRANSFER = 1000;
            moduleFields = AllCaches.ModuleFieldsInfo.Skip(startRow).Take(MAX_FIELD_TO_TRANSFER).ToList();
            endRow = startRow + moduleFields.Count;
        }

        [OperationContract]
        public void GetDataSource(out List<NameValueItem> sourceData, string sourceName, List<string> values)
        {
            try
            {
                sourceData = PostgresqlHelper.ExecuteStoreProcedure<NameValueItem>(ConnectionString, Session, sourceName, values.ToArray());
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void TerminalCurrentSession()
        {
            if (Session != null)
                PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, null, SYSTEM_STORE_PROCEDURES.TERMINAL_SESSION_INFO, Session.SessionKey, Session.Username, CONSTANTS.SESSION_LOGOUT_DESCRIPTION);
        }

        public void CreateUserSession(out Session session, string userName, string password, string clientIP, string dnsName, string clientMacAddress)
        {
            try
            {
                session = PostgresqlHelper.ExecuteStoreProcedure<Session>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.CREATE_NEW_SESSION, userName, password, clientIP)[0];

                session.SessionKey = CommonUtils.MD5Standard(session.SessionID.ToString());
                session.ClientIP = clientIP;
                session.DNSName = dnsName;
                session.ClientMacAddress = clientMacAddress;
                PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, null, SYSTEM_STORE_PROCEDURES.UPDATE_SESSION_INFO,
                    session.SessionID,
                    session.SessionKey,
                    session.ClientIP,
                    session.DNSName,
                    session.ClientMacAddress);

            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void CreateUserSession(out Session session, string userName, string password, string clientIP, string clientMacAddress)
        {
            try
            {
                var endpoint = (RemoteEndpointMessageProperty)
                    OperationContext.Current
                    .IncomingMessageProperties[RemoteEndpointMessageProperty.Name];

                //IPHostEntry entry = null;                
                //string dnsName;
                //try
                //{
                //    entry = Dns.GetHostByAddress(endpoint.Address);
                //    dnsName = Dns.GetHostEntry(endpoint.Address).Aliases[0];
                //}
                //catch
                //{
                //    if (entry == null)
                //        dnsName = "Not Resolved";
                //    else
                //        dnsName = entry.HostName;
                //}


                //Compare  GetStockDapper
                //string totaltimeDapper;
                //string totaltime;

                //PostgresqlHelper.GetStock(ConnectionString, out totaltime);
                //PostgresqlHelper.GetStockDapper(ConnectionString,out totaltimeDapper);



                var dnsName = "Not Resolved";
                if (GetVarValue(SYSVAR.GRNAME_SYS, SYSVAR.VARNAME_DOMAINLOGIN) == CONSTANTS.Yes)
                {
                    if (ValidateUser(userName, password))
                    {
                        CreateUserSession(out session, userName, password, clientIP, dnsName, clientMacAddress);
                    }
                    else
                    {
                        throw ErrorUtils.CreateError(1);
                    }
                }
                else
                {
                    CreateUserSession(out session, userName, password, clientIP, dnsName, clientMacAddress);
                }
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }


        //duchvm
        [OperationContract]
        public Session AsyncCreateUserSession(string userName, string password)
        {
            try
            {
                var endpoint = (RemoteEndpointMessageProperty)
                    OperationContext.Current
                    .IncomingMessageProperties[RemoteEndpointMessageProperty.Name];

                //IPHostEntry entry = null;                
                string dnsName;
                //try
                //{                                        
                //    entry = Dns.GetHostByAddress(endpoint.Address);
                //    dnsName = Dns.GetHostEntry(endpoint.Address).Aliases[0];                    
                //}
                //catch
                //{
                //    if (entry == null)
                //        dnsName = "Not Resolved";
                //    else
                //        dnsName = entry.HostName;
                //}
                dnsName = "Not Resolved";
                if (GetVarValue(SYSVAR.GRNAME_SYS, SYSVAR.VARNAME_DOMAINLOGIN) == CONSTANTS.Yes)
                {
                    if (ValidateUser(userName, password))
                    {
                        return AsyncCreateUserSession(userName, password, endpoint.Address, dnsName);
                    }
                    else
                    {
                        throw ErrorUtils.CreateError(1);
                    }
                }
                else
                {
                    return AsyncCreateUserSession(userName, password, endpoint.Address, dnsName);
                }
            }
            catch (FaultException ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        public Session AsyncCreateUserSession(string userName, string password, string clientIP, string dnsName)
        {
            try
            {
                Session session = null;
                session = PostgresqlHelper.ExecuteStoreProcedure<Session>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.CREATE_NEW_SESSION, userName, password, clientIP)[0];

                session.SessionKey = CommonUtils.MD5Standard(session.SessionID.ToString());
                session.ClientIP = clientIP;
                session.DNSName = dnsName;

                PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, null, SYSTEM_STORE_PROCEDURES.UPDATE_SESSION_INFO,
                    session.SessionID,
                    session.SessionKey,
                    session.ClientIP,
                    session.DNSName);
                return session;
            }
            catch (FaultException ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }
        //end duchvm

        private bool ValidateUser(string username, string password)
        {
            bool blValidation;
            try
            {
                string AddSrv = GetVarValue(SYSVAR.GRNAME_SYS, SYSVAR.VARNAME_DOMAINSRV);
                string DomainName = GetVarValue(SYSVAR.GRNAME_SYS, SYSVAR.VARNAME_DOMAINNAME);
                if (username.IndexOf("@") == -1)
                {
                    username = username + "@" + DomainName;
                }

                LdapDirectoryIdentifier ldapDir = new LdapDirectoryIdentifier(AddSrv);
                LdapConnection lcon = new LdapConnection(ldapDir);
                NetworkCredential nc = new NetworkCredential(username, password);
                lcon.Credential = nc;
                lcon.AuthType = AuthType.Basic;
                lcon.Bind(nc);
                blValidation = true;
            }
            catch (LdapException err)
            {
                blValidation = false;
            }
            return blValidation;
        }

        private string GetVarValue(string grname, string varname)
        {
            string result = null;
            try
            {
                List<string> values = new List<string>();
                values.Add(grname);
                values.Add(varname);
                DataTable dt = new DataTable();
                PostgresqlHelper.FillDataTable(ConnectionString, "sp_sysvar_sel_bygrame", out dt, values.ToArray());
                if (dt.Rows.Count > 0)
                {
                    result = dt.Rows[0]["VARVALUE"].ToString();
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public List<ButtonInfo> BuildSearchButtonsInfo()
        {
            try
            {
                //return PostgresqlHelper.ExecuteStoreProcedure<ButtonInfo>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.LIST_BUTTON);
                return PostgresqlHelper.ExecuteStoreProcedure<ButtonInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_BUTTON);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ListSearchButton(out List<ButtonInfo> buttons)
        {
            buttons = AllCaches.SearchButtonsInfo;
        }

        public List<ButtonParamInfo> BuildSearchButtonParamsInfo()
        {
            try
            {
                return PostgresqlHelper.ExecuteStoreProcedure<ButtonParamInfo>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.LIST_BUTTON_PARAM);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ListSearchButtonParam(out List<ButtonParamInfo> searchButtonParamsInfo)
        {
            searchButtonParamsInfo = AllCaches.SearchButtonParamsInfo;
        }

        [OperationContract]
        public void ExecuteSwitchModule(out string targetModule, string moduleID, string subModule, List<string> values)
        {
            try
            {
                var switchInfo = (SwitchModuleInfo)ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(switchInfo);
                targetModule = PostgresqlHelper.ExecuteStoreProcedureGeneric<string>(ConnectionString, Session, switchInfo.SwitchStore, values.ToArray())[0];
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ExecuteMaintainQuery(out DataContainer executeResult, string moduleID, string subModule, List<string> values)
        {
            try
            {
                var maintainInfo = (MaintainModuleInfo)ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(maintainInfo);
                DataTable table = null;
                switch (subModule)
                {
                    case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_ADD:
                        PostgresqlHelper.FillDataTable(ConnectionString, Session, maintainInfo.AddSelectStore, out table, values.ToArray());
                        break;
                    case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_EDIT:
                        PostgresqlHelper.FillDataTable(ConnectionString, Session, maintainInfo.EditSelectStore, out table, values.ToArray());
                        break;
                    case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_VIEW:
                        PostgresqlHelper.FillDataTable(ConnectionString, Session, maintainInfo.ViewSelectStore, out table, values.ToArray());
                        break;
                }
                executeResult = new DataContainer { DataTable = table };
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ExecuteTransQuery(out DataContainer executeResult, string moduleID, string subModule, List<string> values)
        {
            try
            {
                var maintainInfo = (MaintainModuleInfo)ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(maintainInfo);
                DataTable table = null;
                PostgresqlHelper.FillDataTable(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.TRANS_STOREPROC, out table, values.ToArray());
                executeResult = new DataContainer { DataTable = table };
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        //add by TrungTT - 5.5.2011 - add module report
        [OperationContract]
        public void ExecuteReport(out DataContainer container, string moduleID, string subModule, List<string> values)
        {
            try
            {
                var reportInfo = (ReportModuleInfo)ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(reportInfo);
                DataSet ds;
                PostgresqlHelper.FillDataSet(ConnectionString, Session, reportInfo.StoreName, out ds, values.ToArray());
                container = new DataContainer() { DataSet = ds };
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("SMS", ex.ToString(), EventLogEntryType.Error);
                throw ErrorUtils.CreateError(ex);
            }
        }
        //end TrungTT

        //add by TrungTT - 10.11.2011 - maintain report
        [OperationContract]
        public void ExecuteMaintainReport(out DataContainer container, string moduleID, string subModule, List<string> values)
        {
            try
            {
                var maintainInfo = (MaintainModuleInfo)ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(maintainInfo);
                DataSet ds;
                PostgresqlHelper.FillDataSet(ConnectionString, Session, maintainInfo.ReportStore, out ds, values.ToArray());
                container = new DataContainer() { DataSet = ds };
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }
        //end TrungTT

        //add by TrungTT - 8.11.2011 - Auto Report
        [OperationContract]
        public void ExecuteAutoReport(out DataContainer container, string moduleID, string batchName, List<string> values)
        {
            try
            {
                var moduleInfo = ModuleUtils.GetModuleInfo(moduleID, Core.CODES.DEFMOD.SUBMOD.MODULE_MAIN);
                CheckRole(moduleInfo);
                DataSet ds;
                var batchInfo = PostgresqlHelper.ExecuteStoreProcedure<BatchInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_BATCHINFO_BY_NAME, moduleID, batchName)[0];
                PostgresqlHelper.FillDataSet(ConnectionString, Session, batchInfo.BatchStore, out ds, values.ToArray());
                container = new DataContainer() { DataSet = ds };
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }
        //End TrungTT

        //add by trungtt - 24.08.2011 - Menu Market
        //[OperationContract]
        //public void ExecuteMarket(out DataContainer container)
        //{
        //    try
        //    {
        //        DataSet ds;
        //        PostgresqlHelper.FillDataSet(ConnectionString, Session, "SP_EXECUTE_MARKET_CURRENT", out ds, null);
        //        container = new DataContainer() { DataSet = ds };
        //    }
        //    catch (FaultException)
        //    {
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ErrorUtils.CreateError(ex);
        //    }
        //}
        //end trungtt

        //add by TrungTT - 23.09.2011 - Menu Skins
        [OperationContract]
        public void ExecuteLoadSkins(out DataContainer container)
        {
            try
            {
                DataSet ds;
                PostgresqlHelper.FillDataSet(ConnectionString, Session, "SP_ExecuteLoadSkins", out ds, null);
                container = new DataContainer() { DataSet = ds };
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }
        //end TrungTT

        //add by TrungTT - 27.09.2011 - Get System Date
        [OperationContract]
        public void GetSysDate(out DataContainer container)
        {
            try
            {
                DataSet ds;
                PostgresqlHelper.FillDataSet(ConnectionString, Session, "SP_GetSysDate", out ds, null);
                container = new DataContainer() { DataSet = ds };
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }
        //end TrungTT

        //add by TrungTT - 26.09.2011 - Load Current Skins
        [OperationContract]
        public void ExecuteLoadCurrentSkins(out DataContainer container)
        {
            try
            {
                DataSet ds;
                PostgresqlHelper.FillDataSet(ConnectionString, Session, "SP_EXECUTELOADCURRENTSKINS", out ds, null);
                container = new DataContainer() { DataSet = ds };
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }
        //end TrungTT

        //ADD BY TRUNGTT - 08.12.2011 - EDIT LAYOUT GRID'S COLUMN - SOURCE BY LONGND5
        [OperationContract]
        public void GetExtraCurrentUserProfile(string extraProperty, out string extraValue)
        {
            try
            {
                extraValue = null;
                if (Session != null)
                {
                    extraValue = PostgresqlHelper.ExecuteStoreProcedureGeneric<string>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.PROFILES_SEL_EXTRA, Session.Username, extraProperty)[0];
                }
            }
            catch
            {
                extraValue = null;
            }
        }
        [OperationContract]
        public void SetExtraCurrentUserProfile(string extraProperty, string extraValue)
        {
            try
            {
                if (Session != null)
                {
                    PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.PROFILES_UDP_EXTRA, Session.Username, extraProperty, extraValue);
                }
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }
        //END TRUNGTT

        //add by trungtt - 25.08.2011 - update market
        [OperationContract]
        public void UpdateMarket(List<string> values)
        {
            try
            {
                PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, "SP_MARKET_CURRENT", values.ToArray());
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }
        //end trungtt

        //add by trungtt - 26.09.2011 - Change Skins
        [OperationContract]
        public void ExecuteChangeSkins(List<string> values)
        {
            try
            {
                PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, "SP_EXECUTECHANGESKINS", values.ToArray());
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }
        //end trungtt

        //CuongNH7 : 18-05-2011
        [OperationContract]
        public void ExecuteDownloadFile(out DataContainer container, string moduleID, string subModule, List<string> values)
        {
            try
            {
                var maintainInfo = (MaintainModuleInfo)ModuleUtils.GetModuleInfo(moduleID, subModule);
                //CheckRole(reportInfo); 
                container = null;
                DataSet ds;
                PostgresqlHelper.FillDataSet(ConnectionString, Session, maintainInfo.ReportStore, out ds, values.ToArray());
                if (ds.Tables.Count == 1)
                    container = new DataContainer() { DataTable = ds.Tables[0] };

            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }
        //End CuongNH7
        [OperationContract]
        public void ExecuteImport(string moduleID, string subModule, List<string> values)
        {
            try
            {
                var importInfo = (ImportModuleInfo)ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(importInfo);
                PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, importInfo.ImportStore, values.ToArray());
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ExecuteMaintain(out DataContainer container, string moduleID, string subModule, List<string> values)
        {
            try
            {
                var maintainInfo = (MaintainModuleInfo)ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(maintainInfo);
                container = null;

                switch (subModule)
                {
                    case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_ADD:
                        PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, maintainInfo.AddInsertStore, values.ToArray());
                        break;
                    case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_EDIT:

                        PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, maintainInfo.EditUpdateStore, values.ToArray());
                        break;
                }
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        //add by trungtt - 28.3.2013
        [OperationContract]
        public void ExecApprove(out DataContainer container, string moduleID, string subModule, string secID, List<string> values)
        {
            try
            {
                var maintainInfo = (MaintainModuleInfo)ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(maintainInfo);
                container = null;
                DataSet ds;
                switch (subModule)
                {
                    case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_ADD:
                        PostgresqlHelper.FillDataSet(ConnectionString, Session, maintainInfo.AddInsertStore, out ds, values.ToArray());
                        if (ds.Tables.Count == 1)
                            container = new DataContainer() { DataTable = ds.Tables[0] };
                        //SendMail(moduleID, maintainInfo.SubModule, maintainInfo.Approve, null,secID);
                        break;
                    case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_EDIT:
                        PostgresqlHelper.FillDataSet(ConnectionString, Session, maintainInfo.EditUpdateStore, out ds, values.ToArray());
                        if (ds.Tables.Count == 1)
                            container = new DataContainer() { DataTable = ds.Tables[0] };
                        //SendMail(moduleID, maintainInfo.SubModule, maintainInfo.Approve, null,secID);
                        break;
                }
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }
        //end trungtt

        [OperationContract]
        public void ExecuteSQL(out DataContainer container, string sqlQuery)
        {
#if DEBUG
            try
            {
                DataTable dt = new DataTable();
                PostgresqlHelper.ExecuteSQL(ConnectionString, out dt, sqlQuery);
                container = new DataContainer
                {
                    DataTable = dt
                };
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
#else
            throw ErrorUtils.CreateError(ERR_SYSTEM.ERR_SYSTEM_FUNCTION_ONLY_AVAILABLE_IN_DEBUG_MODE);
#endif
        }
        [OperationContract]
        public void ExecuteProcedureFillDataset(out DataContainer container, string storeData, List<string> values)
        {
            try
            {
                container = null;
                DataSet ds;
                PostgresqlHelper.FillDataSet(ConnectionString, Session, storeData, out ds, values.ToArray());
                if (ds.Tables.Count == 1)
                {
                    container = new DataContainer() { DataTable = ds.Tables[0] };
                }

                // TuanLM Change Dataset to DataReader                
                //var dar = PostgresqlHelper.ExecuteReader(ConnectionString, Session, storeData, values.ToArray());
                //DataTable dt = new DataTable();
                //dt.Load(dar[0]);
                ////if (dt.Rows.Count == 1)
                ////{
                //container = new DataContainer() { DataTable = dt };
                ////}


            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ExecuteProcedureFillDatasetWithoutPram(out DataContainer container, string storeData)
        {
            try
            {
                container = null;
                DataSet ds;
                PostgresqlHelper.FillDataSetWithoutPram(ConnectionString, Session, storeData, out ds);
                if (ds.Tables.Count == 1)
                {
                    //TUDQ them
                    //CheckDataRole(ds.Tables[0]);
                    //
                    container = new DataContainer() { DataTable = ds.Tables[0] };
                }

            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }
        //End duchvm

        [OperationContract]
        public void ProcedureFillDataset(out DataContainer container, string storeData, List<string> values)
        {
            try
            {
                container = new DataContainer();
                DataSet ds = new DataSet();
                var reader = PostgresqlHelper.ExecuteReader(ConnectionString, Session, storeData, values.ToArray());
                foreach (var item in reader)
                {
                    DataTable dt = new DataTable();
                    dt.Load(item);
                    ds.Tables.Add(dt);
                }

                if (ds.Tables.Count == 1)
                    container = new DataContainer() { DataTable = ds.Tables[0] };
                else
                    container.DataSet = ds;


            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }


        [OperationContract]
        public void ExecuteChartMaster(out DataContainer executeResult, string moduleID, string subModule, List<string> values)
        {
            try
            {
                var chartModuleInfo = (ChartModuleInfo)ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(chartModuleInfo);

                DataTable resultTable;
                PostgresqlHelper.FillDataTable(ConnectionString, Session, chartModuleInfo.ChartDataStore, out resultTable, values.ToArray());
                //TUDQ them
                CheckDataRole(resultTable);
                //
                executeResult = new DataContainer { DataTable = resultTable };
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ExecuteProcedure(string moduleID, string subModule, List<string> values)
        {
            try
            {
                var execProcInfo = (ExecProcModuleInfo)ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(execProcInfo);
                PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, execProcInfo.ExecuteStore, values.ToArray());
                if (execProcInfo.SendEmail == Core.CODES.DEFMOD.SENDMAIL.YES)
                {
                    DataTable resultTable;
                    string RptName = string.Empty;
                    string secid = null;
                    // Doan nay hardcode me rui
                    if (moduleID == "01285" || moduleID == "01255")
                    {
                        PostgresqlHelper.FillDataTable(ConnectionString, Session, "SP_RPTNAME_SEL_EX", out resultTable, out secid, values.ToArray());
                    }
                    else
                    {
                        PostgresqlHelper.FillDataTable(ConnectionString, Session, "SP_RPTNAME_SEL", out resultTable, values.ToArray());
                    }

                    if (resultTable.Rows.Count > 0)
                    {
                        RptName = resultTable.Rows[0][0].ToString();
                    }
                    //SendMail(moduleID, "MMN", null, RptName, secid);  
                }
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ExecuteInstallModule(string modulePackageData)
        {
            try
            {
                //var moduleInfo = ModuleUtils.GetModuleInfo(STATICMODULE.IEMODULE, Core.CODES.DEFMOD.SUBMOD.MODULE_MAIN);
                //CheckRole(moduleInfo);

                //var ds = new DataSet();
                //ds.ReadXml(new StringReader(modulePackageData));

                //// Process Script
                //foreach (DataRow row in ds.Tables["SCRIPT"].Rows)
                //{
                //    if (row["STOREBODY"] != DBNull.Value)
                //    {
                //        var query = string.Format("CREATE OR REPLACE\r\n{0}", row["STOREBODY"]);
                //        var conn = new NpgsqlConnection(ConnectionString);
                //        var comm = new NpgsqlCommand(query, conn);
                //        conn.Open();
                //        comm.ExecuteNonQuery();
                //        conn.Close();
                //    }
                //}

                //// Execute Script
                //foreach (DataTable dt in ds.Tables)
                //{
                //    if (dt.TableName != "TABLE_NAME" && dt.TableName != "SCRIPT")
                //    {
                //        var whereQuery = "";
                //        var sep2 = "";

                //        using (var conn = new NpgsqlConnection(ConnectionString))
                //        {
                //            conn.Open();

                //            var tableKey = (string)ds.Tables["TABLE_NAME"].Select("TABLE_NAME = '" + dt.TableName + "'")[0]["TABLE_KEY"];
                //            foreach (DataColumn col in dt.Columns)
                //            {
                //                if (tableKey.Contains(col.ColumnName))
                //                {
                //                    whereQuery += string.Format("{1}({0} = :{0} OR :{0} IS NULL)", col.ColumnName, sep2);
                //                    sep2 = " AND ";
                //                }
                //            }

                //            foreach (DataRow row in dt.Rows)
                //            {
                //                var comm = new NpgsqlCommand
                //                {
                //                    Connection = conn,
                //                    // BindByName = true,
                //                    CommandText = string.Format("DELETE FROM {0} WHERE {1}", dt.TableName, whereQuery)
                //                };

                //                foreach (DataColumn col in dt.Columns)
                //                {
                //                    if (tableKey.Contains(col.ColumnName))
                //                        comm.Parameters.Add(":" + col.ColumnName, row[col]);
                //                }

                //                comm.ExecuteNonQuery();
                //            }

                //            conn.Close();
                //        }
                //    }
                //}

                //foreach (DataTable dt in ds.Tables)
                //{
                //    if (dt.TableName != "TABLE_NAME" && dt.TableName != "SCRIPT")
                //    {
                //        var beginQuery = "";
                //        var endQuery = "";
                //        var sep = "";

                //        foreach (DataColumn dataCol in dt.Columns)
                //        {
                //            beginQuery += string.Format("{1}{0}", dataCol.ColumnName, sep);
                //            endQuery += string.Format("{1}:{0}", dataCol.ColumnName, sep);

                //            sep = ", ";
                //        }

                //        var query = string.Format("INSERT INTO {0}({1}) VALUES ({2})", dt.TableName, beginQuery, endQuery);

                //        foreach (DataRow dataRow in dt.Rows)
                //        {
                //            var conn = new NpgsqlConnection(ConnectionString);
                //            conn.Open();

                //            var comm = new NpgsqlCommand(query, conn) { BindByName = true };

                //            foreach (DataColumn col in dt.Columns)
                //            {
                //                comm.Parameters.Add(":" + col.ColumnName, dataRow[col]);
                //            }

                //            comm.ExecuteNonQuery();
                //            conn.Close();
                //        }
                //    }
                //}
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ExecuteUninstallModule(string moduleID)
        {
            try
            {
                var moduleInfo = ModuleUtils.GetModuleInfo(STATICMODULE.IEMODULE, Core.CODES.DEFMOD.SUBMOD.MODULE_MAIN);
                CheckRole(moduleInfo);

                PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.MODULE_UNINSTALL, moduleID);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ExecuteSaveLanguage(string langID, string langName, string langValue)
        {
            try
            {
                var moduleInfo = ModuleUtils.GetModuleInfo(STATICMODULE.EDITLANG, Core.CODES.DEFMOD.SUBMOD.MODULE_MAIN);
                CheckRole(moduleInfo);
                PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.UPDATE_DEFLANG, langID, langName, langValue);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ExecuteGenerateModulePackage(string moduleID, out string generatedPackage)
        {
            try
            {
                var moduleInfo = ModuleUtils.GetModuleInfo(STATICMODULE.IEMODULE, Core.CODES.DEFMOD.SUBMOD.MODULE_MAIN);
                CheckRole(moduleInfo);

                DataSet ds;
                PostgresqlHelper.FillDataSet(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GENERATE_PACKGE, out ds, moduleID);

                var tableList = ds.Tables[0];
                tableList.TableName = "TABLE_NAME";
                for (var i = 0; i < tableList.Rows.Count; i++)
                {
                    ds.Tables[1 + i].TableName = tableList.Rows[i][0].ToString();
                }

                var sw = new StringWriter();
                ds.WriteXml(sw, XmlWriteMode.WriteSchema);
                generatedPackage = sw.ToString();
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ValidateFieldInfoSyntax(string moduleID, string subModule, string fieldID, List<string> values)
        {
            try
            {
                var moduleInfo = ModuleUtils.GetModuleInfo(STATICMODULE.IEMODULE, Core.CODES.DEFMOD.SUBMOD.MODULE_MAIN);
                CheckRole(moduleInfo);

                var fieldInfo = FieldUtils.GetModuleFieldByID(moduleID, fieldID);
                var validateName = FieldUtils.GetValidateName(moduleInfo, fieldInfo);
                validateName = ExpressionUtils.ParseScript(validateName).StoreProcName;
                var validateInfo = FieldUtils.GetValidateInfo(validateName);

                PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, validateInfo.StoreValidate, values.ToArray());
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void CallbackQuery(out DataContainer executeResult, string moduleID, string callbackFieldID, List<string> values)
        {
            try
            {
                var moduleInfo = ModuleUtils.GetModuleInfo(STATICMODULE.IEMODULE, Core.CODES.DEFMOD.SUBMOD.MODULE_MAIN);
                CheckRole(moduleInfo);

                var storeFieldInfo =
                    FieldUtils.GetModuleFieldByID(
                        moduleID,
                        callbackFieldID
                    );

                executeResult = new DataContainer();
                if (storeFieldInfo != null)
                {
                    DataTable table;
                    PostgresqlHelper.FillDataTable(ConnectionString, Session, storeFieldInfo.Callback, out table, values.ToArray());
                    executeResult.DataTable = table;
                }
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        public List<OracleParam> BuildOracleParamsInfo()
        {
            try
            {
                var stores = PostgresqlHelper.ExecuteStoreProcedure<OracleStore>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.LIST_STOREPROC);
                var oracleParams = new List<OracleParam>();

                foreach (var store in stores)
                {
                    try
                    {
                        PostgresqlHelper.DiscoveryParameters(ConnectionString, store.StoreName, oracleParams);
                    }
                    catch
                    {
                    }
                }
                return oracleParams;
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ListOracleParameter(out List<OracleParam> oracleParams)
        {
            oracleParams = AllCaches.OracleParamsInfo;
        }

        private string BuildSearchExtension(SearchModuleInfo searchInfo, SearchConditionInstance conditionIntance)
        {
            var fields = FieldUtils.GetModuleFields(searchInfo.ModuleID);
            using (var conn = new NpgsqlConnection(ConnectionString))
            {
                using (var comm = new NpgsqlCommand(searchInfo.WhereExtension, conn))
                {
                    conn.Open();
                    comm.CommandType = CommandType.StoredProcedure;
                    PostgresqlHelper.DiscoveryParameters(comm);

                    foreach (var field in
                        fields.Where(field => field.WhereExtension == Core.CODES.DEFMODFLD.WHEREEXTENSION.YES))
                    {
                        comm.Parameters[field.FieldName].Value = DBNull.Value;
                        foreach (var condition in conditionIntance.SubCondition)
                        {
                            if (condition.ConditionID == field.FieldID && string.IsNullOrEmpty(condition.SQLLogic))
                            {
                                comm.Parameters[field.FieldName].Value = condition.Operator;
                            }
                        }
                    }
                    comm.ExecuteNonQuery();
                    return comm.Parameters["RETURN_VALUE"].Value.ToString();
                }
            }
        }

        [OperationContract]
        public void FetchAllSearchResult(out DataContainer searchResult, string moduleID, string subModule, string searchResultKey, DateTime searchTime, int fromRow)
        {
            try
            {
                var moduleInfo = ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(moduleInfo);

                var cacheResult = (from item in CachedSearchResult
                                   where
                                        item.SearchKey == searchResultKey &&
                                        item.TimeSearch == searchTime &&
                                        item.SessionKey == Session.SessionKey
                                   select item).SingleOrDefault();

                if (cacheResult != null)
                {
                    //TUDQ them
                    cacheResult.CachedResult = CheckDataRole(cacheResult.CachedResult);
                    //END
                    lock (cacheResult.CachedResult)
                    {
                        cacheResult.BufferData(fromRow + CONSTANTS.MAX_ROWS_IN_BUFFER);

                        var resultTable = cacheResult.CachedResult.Clone();
                        var rows = cacheResult.CachedResult.Rows.OfType<DataRow>().Skip(fromRow).Take(CONSTANTS.MAX_ROWS_IN_BUFFER).ToArray();
                        foreach (var t in rows)
                        {
                            resultTable.ImportRow(t);
                        }
                        //TUDQ them
                        resultTable = CheckDataRole(resultTable);
                        //
                        searchResult = new DataContainer { DataTable = resultTable };
                    }
                }
                else
                {
                    throw ErrorUtils.CreateError(ERR_SYSTEM.ERR_SEARCH_RESULT_NOT_FOUND);
                }
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void FetchSearchResult(out DataContainer searchResult, out int bufferSize, out int minPage, out int maxPage, out int startRow, string moduleID, string subModule, string searchResultKey, DateTime searchTime, int selectedPage, int maxPageSize)
        {
            try
            {
                var searchInfo = ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(searchInfo);

                var cacheResult = (from item in CachedSearchResult
                                   where
                                        item.SearchKey == searchResultKey &&
                                        item.TimeSearch == searchTime &&
                                        item.SessionKey == Session.SessionKey
                                   select item).FirstOrDefault();

                if (cacheResult != null)
                {
#if DEBUG
                    var startTime = DateTime.Now;
#endif
                    searchResult = new DataContainer();

                    if (cacheResult.IsBufferMode)
                    {
                        try
                        {
                            //TUDQ them
                            //cacheResult.CachedResult = CheckDataRole(cacheResult.CachedResult);
                            //END
                            searchResult.DataTable = cacheResult.GetSearchResult(selectedPage * maxPageSize, maxPageSize);
                        }
                        catch
                        {
                        }

                        startRow = selectedPage * maxPageSize;
                        minPage = 0;
                        maxPage = (int)Math.Ceiling(1.0 * cacheResult.CachedResult.Rows.Count / maxPageSize) - 1;
                    }
                    else
                    {
                        startRow = selectedPage * maxPageSize;
                        minPage = Math.Max(0, selectedPage - CONSTANTS.PAGE_VISIBLE_COUNT / 2);

                        try
                        {
                            cacheResult.BufferData((minPage + CONSTANTS.PAGE_VISIBLE_COUNT) * maxPageSize);
                            //TUDQ them
                            //cacheResult.CachedResult = CheckDataRole(cacheResult.CachedResult);
                            //END
                            searchResult.DataTable = cacheResult.GetSearchResult(selectedPage * maxPageSize, maxPageSize);
                        }
                        catch
                        {
                        }

                        maxPage = (int)Math.Ceiling(1.0 * cacheResult.CachedResult.Rows.Count / maxPageSize) - 1;
                        maxPage = Math.Min(maxPage, minPage + CONSTANTS.PAGE_VISIBLE_COUNT - 1);
                    }
                    bufferSize = cacheResult.CachedResult.Rows.Count;
#if DEBUG
                    WriteLog(
                        "Search: " + searchResultKey.Replace("&", "&&"),
                        string.Format("Fetch {0} row(s), from {1}", maxPageSize, selectedPage * maxPageSize),
                        string.Format("{0:#,0.000} second(s)", (DateTime.Now - startTime).TotalSeconds),
                        "y"
                    );
#endif
                }
                else
                {
                    throw ErrorUtils.CreateError(ERR_SYSTEM.ERR_SEARCH_RESULT_NOT_FOUND);
                }
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void DisposeSearchResult(string moduleID, string subModule, string searchResultKey, DateTime searchTime)
        {
            try
            {
                var searchInfo = (SearchModuleInfo)ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(searchInfo);

                var cacheResult = (from item in CachedSearchResult
                                   where
                                        item.SearchKey == searchResultKey &&
                                        item.TimeSearch == searchTime &&
                                        item.SessionKey == Session.SessionKey
                                   select item).SingleOrDefault();

                if (cacheResult != null)
                {
                    CachedSearchResult.Remove(cacheResult);
                    cacheResult.Dispose();
                }
            }
            catch
            {
            }
        }

        private void DiscoveryParametersForSearch(NpgsqlCommand command, SearchModuleInfo searchInfo, string queryFormat, SearchConditionInstance conditionIntance, List<SearchConditionInstance> staticConditionInstances)
        {
            //command.BindByName = true;

            var whereExtension = "1 = 1";
            if (!string.IsNullOrEmpty(searchInfo.WhereExtension))
            {
                whereExtension = BuildSearchExtension(searchInfo, conditionIntance);
            }


            BuildStaticConditions(searchInfo, command, staticConditionInstances);

            var whereCondition = ModuleUtils.BuildSearchCondition(searchInfo, ref whereExtension, command, conditionIntance);
            if (string.IsNullOrEmpty(whereCondition)) whereCondition = "1 = 1";

            command.CommandText = string.Format(queryFormat, whereCondition, whereExtension);
            if (searchInfo.ModuleID == STATICMODULE.UPFILE_MODID)
            {
                command.CommandText = queryFormat + " and sessionskey = '" + Session.SessionKey + "'";
            }

        }

        [OperationContract]
        public void GetSearchStatistic(out DataContainer searchStatistic, string moduleID, string subModule, SearchConditionInstance conditionIntance, List<SearchConditionInstance> staticConditionInstances)
        {
            try
            {
                var searchInfo = (SearchModuleInfo)ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(searchInfo);

                if (string.IsNullOrEmpty(searchInfo.StatisticQuery))
                {
                    searchStatistic = null;
                }
                else
                {
                    using (var conn = new NpgsqlConnection(ConnectionString))
                    {
                        using (var comm = new NpgsqlCommand())
                        {
                            comm.Connection = conn;
                            var adap = new NpgsqlDataAdapter(comm);

                            DiscoveryParametersForSearch(comm, searchInfo, searchInfo.StatisticQuery, conditionIntance, staticConditionInstances);
#if DEBUG
                            var startTime = DateTime.Now;
                            try
                            {
                                Directory.CreateDirectory("Queries");
                                File.Delete(string.Format("Queries\\{0}-Statistic.txt", searchInfo.ModuleID));
                                File.WriteAllText(string.Format("Queries\\{0}-Statistic.txt", searchInfo.ModuleID), comm.CommandText);
                            }
                            catch
                            {
                            }

#endif

                            var table = new DataTable();
                            adap.Fill(table);
                            searchStatistic = new DataContainer { DataTable = table };
#if DEBUG
                            var searchResultKey = searchInfo.ModuleID + "-" + searchInfo.ModuleName + "?" + ModuleUtils.BuildSearchConditionKey(searchInfo, conditionIntance);
                            WriteLog(
                                "Search: " + searchResultKey.Replace("&", "&&"),
                                "Query Status SQL",
                                string.Format("{0:#,0.000} second(s)", (DateTime.Now - startTime).TotalSeconds),
                                "y"
                            );
#endif
                        }
                    }
                }
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ExecuteSearch(out string searchResultKey, out DateTime searchTime, string moduleID, string subModule, SearchConditionInstance conditionIntance, List<SearchConditionInstance> staticConditionInstances)
        {
            try
            {
                var searchInfo = (SearchModuleInfo)ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(searchInfo);

                var conn = new NpgsqlConnection(ConnectionString);
                var comm = new NpgsqlCommand();
                try
                {
                    var ds = new DataSet();
                    comm.Connection = conn;
                    conn.Open();

                    searchResultKey = searchInfo.ModuleID + "-" + searchInfo.ModuleName + "?" + ModuleUtils.BuildSearchConditionKey(searchInfo, conditionIntance);
                    DiscoveryParametersForSearch(comm, searchInfo, searchInfo.QueryFormat, conditionIntance, staticConditionInstances);
#if DEBUG
                    var startTime = DateTime.Now;
                    WriteLog(
                        searchResultKey.Replace("&", "&&"),
                        "Start search",
                        string.Format("By user: <b>{0}</b>, at <b>{1:HH:mm:ss dd/MM/yyyy}</b>", Session.Username, DateTime.Now),
                        "g"
                    );
                    WriteLog(
                        searchResultKey.Replace("&", "&&"),
                        "Key search",
                        searchResultKey
                    );
                    WriteLog(
                        searchResultKey.Replace("&", "&&"),
                        "SQL",
                        comm.CommandText,
                        "y"
                    );

                    try
                    {
                        Directory.CreateDirectory("Queries");
                        File.Delete(string.Format("Queries\\{0}.txt", searchInfo.ModuleID));
                        File.WriteAllText(string.Format("Queries\\{0}.txt", searchInfo.ModuleID), comm.CommandText);
                    }
                    catch
                    {
                    }
#endif
                    searchTime = DateTime.Now;
                    if (searchInfo.PageMode == Core.CODES.MODSEARCH.PAGEMODE.PAGE_FROM_DATASET || searchInfo.PageMode == Core.CODES.MODSEARCH.PAGEMODE.ALL_FROM_DATASET)
                    {
                        var adap = new NpgsqlDataAdapter(comm);
                        adap.Fill(ds, "RESULT");
#if DEBUG
                        WriteLog(
                            "Search: " + searchResultKey.Replace("&", "&&"),
                            "Execute time",
                            string.Format("{0:#,0.000} second(s)", (DateTime.Now - startTime).TotalSeconds),
                            "y"
                        );
#endif
                        CachedSearchResult.Add(new SearchResult
                        {
                            SessionKey = Session.SessionKey,
                            SearchKey = searchResultKey,
                            TimeSearch = searchTime,
                            CachedResult = ds.Tables["RESULT"]
                        });
                    }
                    else
                    {
                        var dataReader = comm.ExecuteReader();
                        CachedSearchResult.Add(new SearchResult
                        {
                            SessionKey = Session.SessionKey,
                            SearchKey = searchResultKey,
                            TimeSearch = searchTime,
                            DataReader = dataReader,
                            DBConnection = conn
                        });
#if DEBUG
                        WriteLog(
                            "Search: " + searchResultKey.Replace("&", "&&"),
                            "Execute time",
                            string.Format("{0:#,0.000} second(s)", (DateTime.Now - startTime).TotalSeconds),
                            "y"
                        );
#endif
                    }
                }
                catch (FaultException)
                {
                    try
                    {
                        conn.Dispose();
                        comm.Dispose();
                    }
                    catch
                    {
                    }
                    throw;
                }
                catch (Exception ex)
                {
                    try
                    {
                        conn.Dispose();
                        comm.Dispose();
                    }
                    catch
                    {
                    }
                    throw ErrorUtils.CreateErrorWithSubMessage(
                        ERR_SYSTEM.ERR_SYSTEM_EXECUTE_SEARCH_FAIL, ex.Message);
                }
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ExecuteSearchEdit(string moduleID, string subModule, List<string> values)
        {
            try
            {
                var searchModuleInfo = (SearchModuleInfo)ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(searchModuleInfo);

                PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, searchModuleInfo.EditStore, values.ToArray());
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        private void BuildStaticConditions(SearchModuleInfo searchInfo, NpgsqlCommand comm, List<SearchConditionInstance> staticConditionInstances)
        {
            var fields = FieldUtils.GetModuleFields(searchInfo.ModuleID, Core.CODES.DEFMODFLD.FLDGROUP.PARAMETER);

            foreach (var field in fields)
            {
                switch (field.FieldName)
                {
                    case CONSTANTS.ORACLE_SESSION_USER:
                        //comm.Parameters.Add(":" + field.ParameterName, Session.Username);
                        break;
                    case CONSTANTS.ORACLE_CURSOR_OUTPUT:
                        //comm.Parameters.Add(new OracleParameter(":" + field.ParameterName, OracleDbType.RefCursor))
                        //    .Direction = ParameterDirection.Output;
                        break;
                }
            }
            var parameter = new NpgsqlParameter();
            fields = FieldUtils.GetModuleFields(searchInfo.ModuleID, Core.CODES.DEFMODFLD.FLDGROUP.COMMON);
            foreach (var condition in staticConditionInstances)
            {
                foreach (var field in fields)
                {
                    if (field.FieldID == condition.ConditionID)
                    {
                        if (string.IsNullOrEmpty(condition.Value))
                        {
                            parameter.ParameterName = ":" + field.ParameterName;
                            parameter.NpgsqlDbType = NpgsqlDbType.Text;
                            parameter.Value = DBNull.Value;
                            comm.Parameters.Add(parameter);
                            //comm.Parameters.Add(":" + field.ParameterName, DBNull.Value);
                        }
                        else
                        {
                            parameter.ParameterName = ":" + field.ParameterName;
                            parameter.NpgsqlDbType = NpgsqlDbType.Text;
                            parameter.Value = condition.Value.Decode(field);
                            comm.Parameters.Add(parameter);
                            //comm.Parameters.Add(":" + field.ParameterName, condition.Value.Decode(field));
                        }
                    }
                }
            }
        }

        private void _ListUserRoles(out List<Role> roles, int userID)
        {
            try
            {
                //roles = PostgresqlHelper.ExecuteStoreProcedure<Role>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.LIST_USER_ROLE, userID);
                if (userID == Session.UserID)
                {
                    roles = PostgresqlHelper.ExecuteStoreProcedure<Role>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.LIST_USER_ROLE, userID);
                }
                else
                {
                    roles = PostgresqlHelper.ExecuteStoreProcedure<Role>(ConnectionString, null,
                        SYSTEM_STORE_PROCEDURES.LIST_FUNCTION_USER_ROLE, userID, Session.UserID);
                }
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ListGroupRoles(out List<Role> roles, int groupID)
        {
            try
            {
                var moduleInfo = ModuleUtils.GetModuleInfo(STATICMODULE.GROUP_ROLE_MODULE, Core.CODES.DEFMOD.SUBMOD.MODULE_MAIN);
                CheckRole(moduleInfo);
                roles = PostgresqlHelper.ExecuteStoreProcedure<Role>(ConnectionString, null,
                    SYSTEM_STORE_PROCEDURES.LIST_GROUP_ROLE, groupID, Session.UserID);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ListUserRoles(out List<Role> roles, int userID)
        {
            try
            {
                var moduleInfo = ModuleUtils.GetModuleInfo(STATICMODULE.USER_ROLE_MODULE, Core.CODES.DEFMOD.SUBMOD.MODULE_MAIN);
                CheckRole(moduleInfo);
                _ListUserRoles(out roles, userID);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void SaveGroupRoles(List<Role> roles, int groupID)
        {
            try
            {
                var moduleInfo = ModuleUtils.GetModuleInfo(STATICMODULE.USER_ROLE_MODULE, Core.CODES.DEFMOD.SUBMOD.MODULE_MAIN);
                CheckRole(moduleInfo);

                PostgresqlHelper.ExecuteStoreProcedure<Role>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.DELETE_GROUP_ROLE, groupID);
                foreach (var role in roles)
                {
                    if (role.RoleValue == "Y")
                        PostgresqlHelper.ExecuteStoreProcedure<Role>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.INSERT_GROUP_ROLE, groupID, role.RoleID);
                }
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void SaveUserRoles(List<Role> roles, int userID)
        {
            try
            {
                var moduleInfo = ModuleUtils.GetModuleInfo(STATICMODULE.USER_ROLE_MODULE, Core.CODES.DEFMOD.SUBMOD.MODULE_MAIN);
                CheckRole(moduleInfo);

                PostgresqlHelper.ExecuteStoreProcedure<Role>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.DELETE_USER_ROLE, userID);
                foreach (var role in roles)
                {
#if DEBUG
                    PostgresqlHelper.ExecuteStoreProcedure<Role>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.DEFROLE_UDP_PARENT, role.RoleID, role.CategoryID);
#endif
                    if (role.RoleValue == "Y")
                        PostgresqlHelper.ExecuteStoreProcedure<Role>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.INSERT_USER_ROLE, userID, role.RoleID);
                }
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ExecuteAlert(out DataContainer alertResult, string moduleID, string subModule)
        {
            try
            {
                var alertInfo = (AlertModuleInfo)ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(alertInfo);
                DataTable tblAlert;
                //
                PostgresqlHelper.FillDataTable(ConnectionString, Session, alertInfo.AlertStore, out tblAlert);
                //
                alertResult = new DataContainer { DataTable = tblAlert };
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ExecuteBatch(string moduleID, string batchName)
        {
            try
            {
                var moduleInfo = ModuleUtils.GetModuleInfo(moduleID, Core.CODES.DEFMOD.SUBMOD.MODULE_MAIN);
                CheckRole(moduleInfo);
                var batchInfo = PostgresqlHelper.ExecuteStoreProcedure<BatchInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_BATCHINFO_BY_NAME, moduleID, batchName)[0];
                PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, batchInfo.BatchStore, batchInfo.BatchName);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }


        [OperationContract]
        public void ExecuteAlertClick(string moduleID, string subModule)
        {
            try
            {
                var alertInfo = (AlertModuleInfo)ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(alertInfo);
                //
                PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, alertInfo.ClickStore);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void GetListSource(out List<NameValueItem> listSource, string moduleID, string subModule, string fieldID, List<string> values)
        {
            try
            {
                var moduleInfo = ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(moduleInfo);

                var sql =
                    FieldUtils.GetModuleFieldByModule(
                        moduleInfo,
                        fieldID
                    ).ListSource;

                var match = Regex.Match(sql, "[^\\(]+");

                listSource = PostgresqlHelper.ExecuteStoreProcedure<NameValueItem>(ConnectionString, Session, match.Groups[0].Value.Trim(), values.ToArray());
                //DataTable tableRole = null;
                //List<string> values2 = new List<string>();
                //values2.Add(Session.UserID.ToString());
                //PostgresqlHelper.FillDataTable(ConnectionString, Session, "sp_DATAROLE_selbyid", out tableRole, values2.ToArray());
                //DataTable dtReturn = new DataTable();
                //List<NameValueItem> listSourceNew = new List<NameValueItem>();
                //bool flag = true;
                //for (int j = 0; j < listSource.Count; j++)
                //{

                //    for (int i = 0; i < tableRole.Rows.Count; i++)
                //    {

                //        if (tableRole.Rows[i]["CODECK"].ToString() == (string.IsNullOrEmpty(listSource[j].Value) == true ? null : listSource[j].Value.ToString()))
                //        {
                //            flag = false;
                //            listSourceNew.Add(listSource[j]);
                //            break;
                //        }
                //    }
                //}
                //if (!flag)
                //{
                //    listSource = listSourceNew;
                //}

            }
            //
            //}
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void SaveLayout(string moduleID, string subModule, string languageID, string layout)
        {
            //#if DEBUG
            try
            {
                PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.UPDATE_LAYOUT,
                    moduleID, subModule, languageID, layout);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
            //#endif
        }

        [OperationContract]
        public void SaveFile(FileUpload file)
        {
            try
            {
                byte[] filedata = ReadFully(file.UploadStream);
                if (string.Equals(file.ModID, "SERPT", StringComparison.OrdinalIgnoreCase))
                {
                    SaveFileToDisk(file, filedata);
                }
                else if (string.Equals(file.ModID, "06001", StringComparison.OrdinalIgnoreCase))
                {
                    PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, "PKG_TEMPLATE.SP_TEMPLATEFILE_INS", file.KeyID, file.FileName, filedata, Session.SessionKey);
                }
                else if (string.Equals(file.ModID, "06002", StringComparison.OrdinalIgnoreCase))
                {
                    PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, "PKG_EXCELREPORT.SP_EXCELREPORT_INS", file.KeyID, file.FileName, filedata, Session.SessionKey);
                    // PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, "PKG_TEMPLATE.SP_TEMPLATEFILE_INS", file.KeyID, file.FileName, filedata, Session.SessionKey);
                }
                else
                {
                    PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.SAVE_FILE, file.FileName, filedata, Session.Username);
                }
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        private byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        private void SaveFileToDisk(FileUpload file, byte[] filedata)
        {
            try
            {
                List<string> values = new List<string>();
                values.Add(SYSVAR.GRNAME_SYS);
                values.Add(SYSVAR.VARNAME_RPTEXCELFILEPATH);
                DataContainer container = new DataContainer();
                ExecuteProcedureFillDataset(out container, "sp_rptpath_sel", values);
                DataTable dt = container.DataSet.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    string _subPath = dt.Rows[0]["VARVALUE"].ToString() + "\\ORDERS\\";
                    //_subPath = _subPath + "\\" + file.KeyID ;
                    bool isExists = System.IO.Directory.Exists(_subPath);
                    if (!isExists)
                        System.IO.Directory.CreateDirectory(_subPath);

                    string _FileName = _subPath + file.FileName;
                    File.WriteAllBytes(_FileName, filedata);
                }
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ExecuteStoreProcedure(string storeProcedure, List<string> values)
        {
            try
            {
                PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, storeProcedure, values.ToArray());
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ResetCache()
        {
            try
            {
                var type = App.Environment.GetType();
                PostgresqlHelper.m_CachedParameters.Clear();
                App.Environment = (AbstractEnvironment)type.GetConstructor(new Type[] { }).Invoke(new object[] { });
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void CheckUpdate(string clientVersion, out string fileContent)
        {
            try
            {
                var serverVersion = CommonUtils.MD5File(App.Configs.UpdatedToDateVersion);
                if (serverVersion != clientVersion)
                {
                    using (var f = File.OpenRead(App.Configs.UpdatedToDateVersion))
                    {
                        var buffer = new byte[f.Length];
                        f.Read(buffer, 0, buffer.Length);
                        fileContent = CommonUtils.EncodeTo64(buffer);
                    }
                    return;
                }

                fileContent = null;
            }
            catch
            {
                fileContent = null;
            }
        }

        [OperationContract, DebuggerStepThrough]
        public void NewsInfo(out List<NewsInfo> newsInfo)
        {
            try
            {
                newsInfo = PostgresqlHelper.ExecuteStoreProcedure<NewsInfo>(ConnectionString, Session, "SP_NEWS_SEL");
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void GetCurrentUserProfile(out UserProfile userProfile)
        {
            try
            {
                userProfile = null;
                if (Session != null)
                {
                    userProfile = PostgresqlHelper.ExecuteStoreProcedure<UserProfile>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.PROFILES_SEL, Session.Username)[0];
                }
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ListUsersInGroup(out List<User> users, int groupID)
        {
            try
            {
                users = PostgresqlHelper.ExecuteStoreProcedure<User>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GROUP_LIST_USERS, groupID);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        //#if DEBUG
        [OperationContract]
        public void ForceLoadModule(
            out List<ModuleInfo> modulesInfo,
            out List<ModuleFieldInfo> fieldsInfo,
            out List<ButtonInfo> buttonsInfo,
            out List<ButtonParamInfo> buttonParamsInfo,
            out List<LanguageInfo> languageInfo,
            out List<OracleParam> oracleParamsInfo,
            string moduleID)
        {
            try
            {
                modulesInfo = new List<ModuleInfo>();
                modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedure<ModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_STATIC_MODULE, moduleID).ToArray());
                modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedure<ModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_BATCH_MODULE, moduleID).ToArray());
                modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedure<StatisticsModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_STATISTICS_MODULE, moduleID).ToArray());
                modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedure<MaintainModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_MAINTAIN_MODULE, moduleID).ToArray());
                modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedure<ReportModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_REPORT_MODULE, moduleID).ToArray());
                modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedure<ChartModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_CHART_MODULE, moduleID).ToArray());
                modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedure<SearchModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_SEARCHMASTER_MODULE, moduleID).ToArray());
                modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedure<SwitchModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_SWITCH_MODULE, moduleID).ToArray());
                modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedure<ImportModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_IMPORT_MODULE, moduleID).ToArray());
                modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedure<ExecProcModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_EXECUTEPROC_MODULE, moduleID).ToArray());
                modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedure<AlertModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_ALERT_MODULE, moduleID).ToArray());
                modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedure<ModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_TREE_MODULE, moduleID).ToArray());
                modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedure<MaintainModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_WORKFLOW_MODULE).ToArray());
                //modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedure<MaintainModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_TRANSACT_MODULE).ToArray());
                modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedure<ModuleInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_EXPRESSION_MODULE, moduleID).ToArray());
                modulesInfo.AddRange(PostgresqlHelper.ExecuteStoreProcedure<DashboardInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GET_DASHBOARD_MODULE, moduleID).ToArray());

                fieldsInfo = PostgresqlHelper.ExecuteStoreProcedure<ModuleFieldInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_FIELD_INFO_BY_MODID, moduleID);
                buttonsInfo = PostgresqlHelper.ExecuteStoreProcedure<ButtonInfo>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.LIST_BUTTON_BY_MODID, moduleID);
                buttonParamsInfo = PostgresqlHelper.ExecuteStoreProcedure<ButtonParamInfo>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.LIST_BUTTON_PARAM_BY_MODID, moduleID);
                languageInfo = PostgresqlHelper.ExecuteStoreProcedure<LanguageInfo>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.LIST_LANGUAGE_BY_MODID, moduleID);

                var stores = PostgresqlHelper.ExecuteStoreProcedure<OracleStore>(ConnectionString, null, SYSTEM_STORE_PROCEDURES.LIST_STOREPROC_BY_MODID, moduleID);
                oracleParamsInfo = new List<OracleParam>();

                foreach (var store in stores)
                {
                    try
                    {
                        PostgresqlHelper.m_CachedParameters.Remove(store.StoreName);
                        PostgresqlHelper.DiscoveryParameters(ConnectionString, store.StoreName, oracleParamsInfo);
                    }
                    catch
                    {
                    }
                }

                ModuleUtils.ForceLoad(moduleID,
                    modulesInfo,
                    fieldsInfo,
                    buttonsInfo,
                    buttonParamsInfo,
                    languageInfo,
                    oracleParamsInfo);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }
        //#endif

        [OperationContract]
        public void ExecuteStatistics(out string searchResultKey, out DateTime searchTime, string moduleID, string subModule, List<string> values)
        {
            try
            {
                var statisticsInfo = (StatisticsModuleInfo)ModuleUtils.GetModuleInfo(moduleID, subModule);
                CheckRole(statisticsInfo);

                var conn = new NpgsqlConnection(ConnectionString);
                var oracleParams = ModuleUtils.GetOracleParams(statisticsInfo.StoreName);

                searchResultKey = statisticsInfo.ModuleID + "?";
                for (var i = 0; i < values.Count; i++)
                {
                    searchResultKey += oracleParams[i].Name + "=" + values[i] + "&";
                }
#if DEBUG
                var startTime = DateTime.Now;
#endif
                searchTime = DateTime.Now;
                if (statisticsInfo.PageMode == Core.CODES.MODSEARCH.PAGEMODE.PAGE_FROM_DATASET || statisticsInfo.PageMode == Core.CODES.MODSEARCH.PAGEMODE.ALL_FROM_DATASET)
                {
                    DataTable table;
#if DEBUG
                    WriteLog(
                        "Statistic: " + searchResultKey.Replace("&", "&&"),
                        "Execute time",
                        string.Format("{0:#,0.000} second(s)", (DateTime.Now - startTime).TotalSeconds),
                        "y"
                    );
#endif
                    PostgresqlHelper.FillDataTable(ConnectionString, Session, statisticsInfo.StoreName, out table, values.ToArray());
                    CachedSearchResult.Add(new SearchResult
                    {
                        SessionKey = Session.SessionKey,
                        SearchKey = searchResultKey,
                        TimeSearch = searchTime,
                        CachedResult = table
                    });
                }
                else
                {
                    var dataReaders = PostgresqlHelper.ExecuteReader(ConnectionString, Session, statisticsInfo.StoreName, values.ToArray());
                    if (dataReaders.Length == 2)
                        CachedSearchResult.Add(new SearchResult
                        {
                            SessionKey = Session.SessionKey,
                            SearchKey = searchResultKey,
                            TimeSearch = searchTime,
                            DataReader = dataReaders[0],
                            DataReader2 = dataReaders[1],
                            DBConnection = conn
                        });
                    else
                        CachedSearchResult.Add(new SearchResult
                        {
                            SessionKey = Session.SessionKey,
                            SearchKey = searchResultKey,
                            TimeSearch = searchTime,
                            DataReader = dataReaders[0],
                            DBConnection = conn
                        });
#if DEBUG
                    WriteLog(
                        "Search: " + searchResultKey.Replace("&", "&&"),
                        "Execute time",
                        string.Format("{0:#,0.000} second(s)", (DateTime.Now - startTime).TotalSeconds),
                        "y"
                    );
#endif
                }
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }


        [OperationContract]
        public void ExecuteUsersLog(string moduleID, string moduleName, string subMod)
        {
            try
            {
                PostgresqlHelper.ExecuteStoreProcedure(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.USERSLOG_INSERT, Session.Username, moduleID, moduleName, DateTime.Now, subMod);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }
        //End TrungTT

        //Tudq them
        [OperationContract]
        public void GetTreeViewStore(out DataContainer executeResult, List<string> values)
        {
            try
            {

                DataTable table = null;
                PostgresqlHelper.FillDataTable(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GETMODULE_TREE, out table, values.ToArray());
                executeResult = new DataContainer { DataTable = table };
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void GetTreeViewLang(out DataContainer executeResult, List<string> values)
        {
            try
            {

                DataTable table = null;
                PostgresqlHelper.FillDataTable(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.GETMODULE_TREELANG, out table, values.ToArray());
                executeResult = new DataContainer { DataTable = table };
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }
        //End


        //TUDQ them
        [OperationContract]
        public DataTable CheckDataRole(DataTable dt)
        {
            DataTable tableRole = null;
            List<string> values = new List<string>();
            values.Add(Session.UserID.ToString());
            PostgresqlHelper.FillDataTable(ConnectionString, Session, "sp_DATAROLE_selbyid", out tableRole, values.ToArray());
            DataTable dtReturn = new DataTable();
            bool flag = false;
            bool flagRownum = false;
            bool flagStt = false;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName.ToUpper() == "SYMBOL")
                {
                    flag = true;
                }
                if (dt.Columns[i].ColumnName.ToUpper() == "STT")
                {
                    flagStt = true;
                }
                if (dt.Columns[i].ColumnName.ToUpper() == "ROWNUM")
                {
                    flagRownum = true;
                }
            }
            // Loc du lieu theo dieu kien
            if (flag)
            {
                dtReturn = dt.Clone();
                int rowNum = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    for (int k = 0; k < tableRole.Rows.Count; k++)
                    {
                        if (dt.Rows[j]["SYMBOL"].ToString() == tableRole.Rows[k]["CODECK"].ToString())
                        {
                            if (flagStt == true)
                            {
                                dt.Rows[j]["STT"] = rowNum + 1;
                                rowNum++;
                            }
                            if (flagRownum == true)
                            {
                                dt.Rows[j]["ROWNUM"] = rowNum + 1;
                                rowNum++;
                            }
                            dtReturn.ImportRow(dt.Rows[j]);
                            break;
                        }

                    }
                }
            }
            else
            {
                // Bo sung stt vao ket qua
                dtReturn = dt.Clone();
                int rowNum = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    //for (int k = 0; k < tableRole.Rows.Count; k++)
                    //{
                    if (flagStt == true)
                    {
                        dt.Rows[j]["STT"] = rowNum + 1;
                        rowNum++;
                    }
                    dtReturn.ImportRow(dt.Rows[j]);
                    //break;                        
                    //}
                }
                dtReturn = dt;
            }
            return dtReturn;
        }

        [OperationContract]
        public DataSet CheckDataRole1(DataSet ds)
        {
            DataTable tableRole = null;
            List<string> values = new List<string>();
            values.Add(Session.UserID.ToString());
            PostgresqlHelper.FillDataTable(ConnectionString, Session, "sp_DATAROLE_selbyid", out tableRole, values.ToArray());
            DataSet dsResult = ds.Clone();
            bool flag = false;
            for (int count = 0; count < ds.Tables.Count; count++)
            {
                DataTable dt = ds.Tables[count];
                DataTable dtReturn = new DataTable();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName.ToUpper() == "SECID")
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    dtReturn = dt.Clone();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        for (int k = 0; k < tableRole.Rows.Count; k++)
                        {
                            if (dt.Rows[j]["SECID"].ToString() == tableRole.Rows[k]["SECID"].ToString())
                            {
                                dtReturn.ImportRow(dt.Rows[j]);
                                break;
                            }
                        }
                    }
                    dsResult.Tables.Add(dtReturn);
                }
                else dsResult.Tables.Add(dt);
            }
            return dsResult;
        }

        //END
        public List<GroupSummaryInfo> BuildGroupSummaryInfo()
        {
            try
            {
                return PostgresqlHelper.ExecuteStoreProcedure<GroupSummaryInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_GROUP_SUMMARY_INFO);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        public List<ExportHeader> BuildExportHeaderInfo()
        {
            try
            {
                return PostgresqlHelper.ExecuteStoreProcedure<ExportHeader>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_HEADER_EXPORT_INFO);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        public List<SysvarInfo> BuildSysvarInfo()
        {
            try
            {
                return PostgresqlHelper.ExecuteStoreProcedure<SysvarInfo>(ConnectionString, Session, SYSTEM_STORE_PROCEDURES.LIST_SYSVAR_INFO);
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }

        [OperationContract]
        public void ListGroupSummaryInfo(out List<GroupSummaryInfo> groupSummaryInfos)
        {
            groupSummaryInfos = AllCaches.GroupSummaryInfos;
        }

        [OperationContract]
        public void ListExportHeaderInfo(out List<ExportHeader> ExportHeaderInfos)
        {
            ExportHeaderInfos = AllCaches.ExportHeaders;
        }

        [OperationContract]
        public void ListSysvarInfo(out List<SysvarInfo> SysvarInfos)
        {
            SysvarInfos = AllCaches.SysvarsInfo;
        }

        [OperationContract]
        public void GetModImport(out DataSet executeResult, string rptID)
        {
            try
            {

                DataSet ds = null;
                PostgresqlHelper.FillDataSet(ConnectionString, Session, "sp_modimport_sel", out ds, rptID);
                executeResult = ds;
            }
            catch (FaultException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw ErrorUtils.CreateError(ex);
            }
        }
        //End

    }
}