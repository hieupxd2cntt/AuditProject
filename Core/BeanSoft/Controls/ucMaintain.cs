using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using AppClient.Interface;
using Core.Base;
using Core.Controllers;
using Core.Entities;
using Core.Extensions;
using Core.Utils;
using Core.Common;
using DevExpress.XtraLayout;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit;
using System.Data;
using System.IO;
using System.Drawing;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.Utils.Commands;
using System.Text;
using System.Reflection;
using System.Net;
using System.Threading;
using System.Diagnostics;
using AppClient.Utils;
using Newtonsoft.Json;
using System.Linq;

namespace AppClient.Controls
{
    public partial class ucMaintain : ucModule,
        IParameterFieldSupportedModule,
        ICommonFieldSupportedModule
    {
        #region ICommonFieldSupportedModule Members
        
        public bool ValidateRequire
        {
            get { return true; }
        }

        public LayoutControl CommonLayout
        {
            get { return mainLayout; }
        }
        
        public string CommonLayoutStoredData
        {
            get
            {
                switch (ModuleInfo.SubModule)
                {
                    case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_ADD:
                        return Language.AddLayout;
                    case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_EDIT:
                        return Language.EditLayout;
                    case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_VIEW:
                        return Language.ViewLayout;
                    //TUDQ them
                    case Core.CODES.DEFMOD.SUBMOD.TRANSACTION_VIEW:
                        return Language.ViewLayout;
                    //
                    default:
                        return null;
                }
            }
        }
        #endregion

        #region Properties & Members       
        private ContextMenuStrip lnkFileContextMenu;
        private string filetempname;
        int prKey = 0;

        public MaintainModuleInfo MaintainInfo
        {
            get
            {
                return (MaintainModuleInfo)ModuleInfo;
            }
        }

        #endregion

        #region Override methods
        protected override void BuildFields()
        {
                        
            if (!string.IsNullOrEmpty(MaintainInfo.ButtonText))
            {
                btnCommit.Text = MaintainInfo.ButtonText; 
            }
            
            base.BuildFields();

            if (Parent is ContainerControl)
                ((ContainerControl)Parent).ActiveControl = mainLayout;

            
        }

        protected override void BuildButtons()
        {
//#if DEBUG
                SetupContextMenu(mainLayout);
                SetupModuleEdit();
                SetupGenenerateScript();
                SetupSeparator();
                SetupParameterFields();
                SetupCommonFields();
                SetupSeparator();
                SetupFieldMaker();
                SetupFieldsSuggestion();
                SetupSeparator();
                SetupLanguageTool();
                SetupSaveLayout(mainLayout);
                SetupSaveAllLayout(mainLayout);
//#endif
        }

        public override void InitializeLayout()
        {
            base.InitializeLayout();
            lbTitle.BackColor = ThemeUtils.BackTitleColor;
            lbTitle.ForeColor = ThemeUtils.TitleColor;
            //SaveAsFile();            
        }
        private void ucMaintain_Load(object sender, EventArgs e)
        {

        }
        private void SaveAsFile()
        {
            lnkFileContextMenu = new ContextMenuStrip();            
            lnkFile.ContextMenuStrip = lnkFileContextMenu;
            lnkFileContextMenu.Items.Clear();
            lnkFileContextMenu.Items.Add("Lưu file", ThemeUtils.Image16.Images["SAVE"]).Click+=            
               delegate
               {
                   try
                   {
                       bool allow = false;
                       foreach (var i in (FieldUtils.GetModuleFields(
                           ModuleInfo.ModuleID,
                           Core.CODES.DEFMODFLD.FLDGROUP.PARAMETER)))
                       {
                           strFLDID = i.FieldName;
                           allow = true;
                       }
                       if (allow)
                       {
                           var fields = FieldUtils.GetModuleFields(ModuleInfo.ModuleID, Core.CODES.DEFMODFLD.FLDGROUP.PARAMETER);
                           List<string> values = new List<string>();

                           string StoreName;                           
                           if (MaintainInfo.Approve == Core.CODES.MODMAINTAIN.APROVE.YES && !string.IsNullOrEmpty(Program.txnum))
                           {
                               values.Add(Program.txnum);
                               StoreName = "sp_tllog_file_sel";
                           }
                           else
                           {
                               foreach (var field in fields)
                               {
                                   values.Add(this[field.FieldID].ToString());
                               }
                               StoreName = MaintainInfo.ReportStore;
                           }  
                          
                           // End TuanLM
                           if (values.Count > 0)
                           {
                               using (var ctrlSA = new SAController())
                               {
                                   DataContainer container = null;
                                   ctrlSA.ExecuteProcedureFillDataset(out container,StoreName, values);
                                   if (container != null && container.DataTable != null)
                                   {
                                       var resultTable = container.DataTable;

                                       if (resultTable.Rows.Count > 0)
                                       {
                                           for (int i = 0; i <= resultTable.Rows.Count - 1; i++)
                                           {
                                               using (System.IO.MemoryStream ms = new System.IO.MemoryStream((Byte[])resultTable.Rows[i]["filestore"]))
                                               {
                                                   StreamReader memoryReader = new StreamReader(ms);

                                                   SaveFileDialog dlg = new SaveFileDialog();
                                                   dlg.FileName = resultTable.Rows[i]["filename"].ToString();
                                                   dlg.Filter = resultTable.Rows[i]["filetype"].ToString();
                                                   if (dlg.ShowDialog() == DialogResult.OK)
                                                   {
                                                       ms.WriteTo(dlg.OpenFile());                                                       
                                                   }                                                  
                                               }
                                           };
                                       }
                                       else
                                       {
                                           throw ErrorUtils.CreateError(ERR_FILE.ERR_FILE_IS_NOT_ATTACKED);
                                       }
                                   }

                               }
                           }
                       }
                       else
                       {
                           throw ErrorUtils.CreateError(ERR_FILE.ERR_FILE_IS_NOT_ATTACKED);
                       }

                   }
                   catch (Exception ex)
                   {
                       ShowError(ex);
                   }
               };  
        }         
        private void LoadDataFromCacheClient()
        {
            if (App.Environment.ClientInfo.DataClientCache.Count > 0)
            {
                var fields = GetModuleFields();
             
                foreach (var field in fields)
                {
                    if (!string.IsNullOrEmpty(field.ParameterName) && field.FieldName =="ROWID")
                    {
                        prKey = int.Parse(this[field.FieldID].Encode(field));
                    }
                }

                DataClientCache dataCache = (from field in App.Environment.ClientInfo.DataClientCache 
                                  where field.PrKey == prKey && (field.JsonKey == MaintainInfo.ModuleID + App.Environment.ClientInfo.SessionKey) 
                                  select field).Single();
                var con = new DataContainer();
                var dt = (DataTable)JsonConvert.DeserializeObject("[" + dataCache.JsonData+ "]", (typeof(DataTable)));
                con.DataTable = dt;
                AssignFieldValuesFromResult(con);
            }
        }
       
        private void LoadData(string storeName)
        {
            using (var ctrlSA = new SAController())
            {
                if (!string.IsNullOrEmpty(storeName))
                {
                    List<string> values;
                    GetOracleParameterValues(out values, storeName);

                    DataContainer con;
                    ctrlSA.ExecuteMaintainQuery(out con, MaintainInfo.ModuleID, MaintainInfo.SubModule, values);
                    AssignFieldValuesFromResult(con);
                    DataTable dt = con.DataTable;                    
                }
            }
        }

        protected override void InitializeModuleData()
        {
            base.InitializeModuleData();
            try
            {
                StopCallback = true;
                switch (ModuleInfo.SubModule)
                {
                    case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_EDIT:
                        lbTitle.Text = Language.EditTitle;
                        if (MaintainInfo.ISGVA == Core.Common.CONSTANTS.Yes)
                        {
                            LoadDataFromCacheClient();
                        }
                        else
                        {
                            LoadData(MaintainInfo.EditSelectStore);
                        }
                        break;
                    case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_VIEW:
                        lbTitle.Text = Language.ViewTitle;
                        if (MaintainInfo.Report == "F")
                        {
                            lnkFile.Visible = true;
                            lnkFile.Properties.Appearance.BackColor = Color.Transparent;                            
                            SaveAsFileToDisk(false);
                            LoadData(MaintainInfo.ViewSelectStore);
                        }
                        else if (MaintainInfo.Report == "V")
                        {
                            SaveAsFileToDisk(true);
                            IsFile = true;
                        }
                        else
                        {

                            if (MaintainInfo.Approve == Core.CODES.MODMAINTAIN.APROVE.YES && !string.IsNullOrEmpty(Program.txnum))
                                LoadDataTransaction(SYSTEM_STORE_PROCEDURES.TRANS_STOREPROC);
                            else
                                LoadData(MaintainInfo.ViewSelectStore);
                        }                                                                     
                        break;
                    case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_ADD:
                        lbTitle.Text = Language.AddTitle;
                        LoadData(MaintainInfo.AddSelectStore);
                        break;
                    //TUDQ them
                    case Core.CODES.DEFMOD.SUBMOD.TRANSACTION_VIEW:
                        lbTitle.Text = Language.AddTitle;
                        LoadDataTransaction(MaintainInfo.TRANSATIONSTORE);
                        break;
                    //END
                }
                
                // Configuration lbTitle
                switch (ModuleInfo.SubModule)
                {
                    case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_EDIT:
                        lbTitle.Text = string.Format(lbTitle.Text, this);
                        break;
                    case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_VIEW:
                        lbTitle.Text = string.Format(lbTitle.Text, this);
                        SAController ctrl = new SAController();//HUYVQ: 2014/06/09
                        Session s;
                        ctrl.GetCurrentSessionInfo(out s);
                        if (MaintainInfo.Approve == Core.CODES.MODMAINTAIN.APROVE.YES && !string.IsNullOrEmpty(Program.txnum) && s.Type == 1)
                        {
                            btnCommit.Visible = true;
                            btnCommit.Text = "Duyệt";
                        }
                        //add by trungtt 27.5.2014
                        else if (MaintainInfo.Report == "R" && MaintainInfo.SubModule == Core.CODES.DEFMOD.SUBMOD.MAINTAIN_VIEW)
                        {
                            btnCommit.Visible = true;
                            btnCommit.Text = "Thông tin";
                            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMaintain));
                            btnCommit.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadReport.Image")));
                        }
                        //end trungtt
                        else
                        {
                            btnCommit.Visible = false;
                        }                       
                        break;
                }

                if (ActiveControl is ComboBoxEdit)
                    LoadComboxListSource((ActiveControl as ComboBoxEdit).Properties);
            }
            catch
            {
                CloseModule();
                throw;
            }
            finally
            {
                StopCallback = false;
            }
        }

        protected delegate void ResetModuleDataInvoker();
        protected void ResetModuleData()
        {
            if(InvokeRequired)
            {
                Invoke(new ResetModuleDataInvoker(ResetModuleData));
                return;
            }

            InitializeModuleData();
            mainLayout.FocusHelper.FocusFirst();
        }

        protected override void SetDefaultValues(List<ModuleFieldInfo> fields)
        {
            base.SetDefaultValues(fields);

            foreach (var field in fields)
            {
                if (field.DefaultValue != null && field.ControlType != Core.CODES.DEFMODFLD.CTRLTYPE.CHECKEDCOMBOBOX)
                {
                    switch (field.FieldGroup)
                    {
                        case Core.CODES.DEFMODFLD.FLDGROUP.COMMON:
                            this[field.FieldID] = FieldUtils.Convert(field, field.DefaultValue);
                            break;
                    }
                }
                else if (field.DefaultValue != null && field.ControlType == Core.CODES.DEFMODFLD.CTRLTYPE.CHECKEDCOMBOBOX)              
                {
                    using (var ctrlSA = new SAController())
                    {

                        List<string> values;                        
                        DataContainer con;
                        GetOracleParameterValues(out values, field.DefaultValue);
                        ctrlSA.ExecuteProcedureFillDataset(out con, field.DefaultValue, values);
                        var dsResult = con.DataSet;                         
                        this[field.FieldID] = FieldUtils.Convert(field, dsResult.Tables[0].Rows[0][0].ToString());                                                    
                    }
                }
            }
        }
        #endregion

        #region Events
        private void btnCommit_Click(object sender, EventArgs e)
        {            
             Execute();                       
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Program.txnum = null;
            if (MaintainInfo.ModuleID == "02913")
            {
                MainProcess.LogoutFromSystem(true);
            }
            if (MaintainInfo.Report == Core.CODES.MODMAINTAIN.REPORT.FILE)
            {
                if (!string.IsNullOrEmpty(filetempname))
                {
                    DeleteFile(filetempname);
                }                              
            }
            //else
            CloseModule();           
        }

        #endregion

        public ucMaintain()
        {
            InitializeComponent();            
        }

        public delegate void PInvoke();
        
        //public override void Execute()
        public void Execute()
        {
            if (ValidateModule())
            {
                LockUserAction();

                new WorkerThread(
                    delegate
                     {
                         try
                         {
                             using (var ctrlSA = new SAController())
                             {
                                 List<string> values;
                                 var repeatInput = false;
                                 DataContainer container = null;
                                 switch (ModuleInfo.SubModule)
                                 {
                                     case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_ADD:
                                         if (MaintainInfo.ISGVA == Core.Common.CONSTANTS.Yes )
                                         {
                                             var dataCacheClient = new DataClientCache();
                                             int iCount = App.Environment.ClientInfo.DataClientCache.Count + 1;
                                             var fields = GetModuleFields();
                                             var param = new Dictionary<string, object>();
                                             param.Add(CONSTANTS.MAIN_COL_STT, iCount);
                                             foreach (var field in fields)
                                             {
                                                 if (!string.IsNullOrEmpty(field.ParameterName) && field.FieldGroup == Core.CODES.DEFMODFLD.FLDGROUP.COMMON)
                                                 {
                                                     param.Add(field.FieldName, this[field.FieldID].Encode(field));
                                                 }
                                             }
                                             dataCacheClient.JsonKey = ModuleInfo.ModuleID + App.Environment.ClientInfo.SessionKey;
                                             dataCacheClient.PrKey = iCount;
                                             dataCacheClient.JsonData = JsonConvert.SerializeObject(param);
                                             App.Environment.ClientInfo.DataClientCache.Add(dataCacheClient);
                                         }                                         
                                         else
                                         {
                                             GetOracleParameterValues(out values, MaintainInfo.AddInsertStore);
                                             List<string> temp = new List<string>();
                                             if (MaintainInfo.ModuleID == "QCALL")
                                             {
                                                 MainProcess.ExecuteModule(values[0].ToString(),"MMN");
                                             }
                                             else
                                             {
                                                 if (MaintainInfo.ModuleID == "02913")
                                                 {
                                                     foreach (string value in values)
                                                     {
                                                         temp.Add(CommonUtils.EncryptStringBySHA1(value));
                                                     }
                                                     values = temp;
                                                 }
                                                 if (MaintainInfo.TRANSACTION_MODE == "Y")
                                                 {
                                                     values[values.Count - 1] = MaintainInfo.ModuleID;
                                                     values.Add(null);
                                                 }

                                                 ctrlSA.ExecuteMaintain(out container, ModuleInfo.ModuleID, ModuleInfo.SubModule, values);
                                             }
                                         }

                                         if (MaintainInfo.AddRepeatInput == Core.CODES.MODMAINTAIN.REPEATINPUT.YES)
                                            repeatInput = true;
                                         break;
                                         
                                     case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_EDIT:
                                         if (MaintainInfo.ISGVA == Core.Common.CONSTANTS.Yes)
                                         {
                                             var datacachetemp = new DataClientCache();
                                             foreach (var datacache in App.Environment.ClientInfo.DataClientCache)
                                             {
                                                 if (datacache.PrKey == prKey && (datacache.JsonKey == MaintainInfo.ModuleID + App.Environment.ClientInfo.SessionKey))
                                                 {
                                                     datacachetemp = datacache;
                                                 }
                                             }
                                             App.Environment.ClientInfo.DataClientCache.Remove(datacachetemp);

                                             var dataCacheClient = new DataClientCache();
                                             var fields = GetModuleFields();
                                             var param = new Dictionary<string, object>();
                                             param.Add(CONSTANTS.MAIN_COL_STT, prKey);
                                             foreach (var field in fields)
                                             {
                                                 if (!string.IsNullOrEmpty(field.ParameterName) && field.FieldGroup == Core.CODES.DEFMODFLD.FLDGROUP.COMMON)
                                                 {
                                                     param.Add(field.FieldName, this[field.FieldID].Encode(field));
                                                 }
                                             }
                                             dataCacheClient.JsonKey = ModuleInfo.ModuleID + App.Environment.ClientInfo.SessionKey;
                                             dataCacheClient.PrKey = prKey;
                                             dataCacheClient.JsonData = JsonConvert.SerializeObject(param);
                                             App.Environment.ClientInfo.DataClientCache.Add(dataCacheClient);
                                         }
                                         else
                                         {
                                             GetOracleParameterValues(out values, MaintainInfo.EditUpdateStore);
                                             ctrlSA.ExecuteMaintain(out container, ModuleInfo.ModuleID, ModuleInfo.SubModule, values);

                                             if (MaintainInfo.EditRepeatInput == Core.CODES.MODMAINTAIN.REPEATINPUT.YES)
                                                 repeatInput = true;
                                         }

                                         break;

                                     case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_VIEW:
                                         try
                                         {
                                             // Lay thong tin module trong tllog
                                             List<string> value = new List<string>();
                                             string v_Submod = null ;
                                             value.Add(Program.txnum);
                                             ctrlSA.ExecuteProcedureFillDataset(out container, "sp_tllog_sel_basic", value);
                                             if (container != null && container.DataTable != null)
                                                {
                                                    var resultTable = container.DataTable;
                                                    if (resultTable.Rows.Count > 0)
                                                    {
                                                        v_Submod = resultTable.Rows[0]["SUBMOD"].ToString();
                                                    }
                                                }

                                             if (v_Submod == Core.CODES.DEFMOD.SUBMOD.MAINTAIN_ADD)
                                             {
                                                 GetOracleParameterValues(out values, MaintainInfo.AddInsertStore);
                                                 ctrlSA.ExecApprove(out container, ModuleInfo.ModuleID, v_Submod,SecID, values);

                                                 // Cap nhat tllogs
                                                 List<string> valueAcceptTrans = new List<string>();
                                                 valueAcceptTrans.Add(Program.txnum);
                                                 valueAcceptTrans.Add(App.Environment.ClientInfo.UserName);
                                                 ctrlSA.ExecuteStoreProcedure("sp_accept_trans", valueAcceptTrans);

                                                 List<string> values1 = new List<string>();
                                                 values1.Add(null);
                                                 values1.Add(Program.txnum);
                                                 // Cap nhat file dinh kem vao bang Bussiness tuong ung
                                                 if (!string.IsNullOrEmpty(MaintainInfo.EXECTRANSCTIONSTORE))
                                                 {
                                                     ctrlSA.ExecuteStoreProcedure(MaintainInfo.EXECTRANSCTIONSTORE, values1);
                                                 }
                                                 Program.txnum = null;
                                             }
                                             else if (v_Submod == Core.CODES.DEFMOD.SUBMOD.MAINTAIN_EDIT)
                                             {
                                                 GetOracleParameterValues(out values, MaintainInfo.EditUpdateStore);
                                                 string v_ROWID = values[0].ToString();

                                                 List<string> values1 = new List<string>();
                                                 values1.Add(v_ROWID);
                                                 values1.Add(Program.txnum);
                                                 // Duyet thong tin
                                                 ctrlSA.ExecApprove(out container, ModuleInfo.ModuleID, v_Submod,SecID, values);

                                                 // Cap nhat tllogs
                                                 List<string> valueAcceptTrans = new List<string>();
                                                 valueAcceptTrans.Add(Program.txnum);
                                                 valueAcceptTrans.Add(App.Environment.ClientInfo.UserName);
                                                 ctrlSA.ExecuteStoreProcedure("sp_accept_trans", valueAcceptTrans);

                                                 // Cap nhat file dinh kem vao bang Bussiness tuong ung
                                                 if (!string.IsNullOrEmpty(MaintainInfo.EXECTRANSCTIONSTORE))
                                                 {
                                                     ctrlSA.ExecuteStoreProcedure(MaintainInfo.EXECTRANSCTIONSTORE, values1);
                                                 }
                                                 Program.txnum = null;
                                             }                                             
                                         }
                                         catch (Exception ex)
                                         {
                                             ShowError(ex);
                                             UnLockUserAction();
                                         }
                                         break;
                                     //end trungtt
                                     case Core.CODES.DEFMOD.SUBMOD.TRANSACTION_VIEW:
                                         try
                                         {
                                             List<string> values1 = new List<string>();
                                             values1.Add(Program.txnum);
                                             values1.Add(ModuleInfo.ModuleID);
                                             values1.Add(App.Environment.ClientInfo.UserName);
                                             ctrlSA.ExecuteStoreProcedure(MaintainInfo.EXECTRANSCTIONSTORE,values1);
                                             repeatInput = false;
                                         }
                                         catch (Exception ex)
                                         {
                                             ShowError(ex);
                                             UnLockUserAction();
                                         }
                                         break;                                        
                                 }

                                 RequireRefresh = true;

                                 if(MaintainInfo.ShowSuccess == Core.CODES.MODMAINTAIN.SHOWSUCCESS.YES)
                                 {
                                     RowFormattable fmtRow = null;

                                     if(container != null)
                                     {
                                         var rows = container.DataTable.Rows;
                                         if(rows.Count == 1)
                                             fmtRow = new RowFormattable(rows[0]);
                                     }

                                     if(fmtRow != null)
                                     {
                                         frmInfo.ShowInfo(Language.Title, string.Format(Language.SuccessStatus, fmtRow), this);
                                     }
                                     else
                                         frmInfo.ShowInfo(Language.Title, Language.SuccessStatus, this);
                                 }

                                 if (!repeatInput)
                                 {
                                     CloseModule();
                                 }
                                 else
                                 {
                                     ResetModuleData();
                                     UnLockUserAction();
                                 }
                             }
                         }
                         catch (Exception ex)
                         {
                             ShowError(ex);
                             UnLockUserAction();
                         }
                     }, this).Start();
            }
        }

       
        public override void LockUserAction()
        {
            base.LockUserAction();

            if(!InvokeRequired)
            {
                Enabled = false;
                ShowWaitingBox();
            }
        }

        public override void UnLockUserAction()
        {
            base.UnLockUserAction();

            if(!InvokeRequired)
            {
                Enabled = true;
                HideWaitingBox();
                ActiveControl = mainLayout;
                mainLayout.Focus();
            }
        }

        string strFLDID;
        //edit by TrungTT - 8.10.2013 - adding output param into fuction
        private void SaveAsFileToDisk(bool _open)
        {
            try
            {
                bool allow = false;                
                foreach (var i in (FieldUtils.GetModuleFields(
                    ModuleInfo.ModuleID,
                    //CODES.DEFMODFLD.FLDGROUP.COMMON)))
                    Core.CODES.DEFMODFLD.FLDGROUP.PARAMETER)))
                {
                    //if (i.FieldName == "ROWID")
                    strFLDID = i.FieldName;
                    allow = true;
                }
                if (allow)
                {                    
                    var fields = FieldUtils.GetModuleFields(ModuleInfo.ModuleID, Core.CODES.DEFMODFLD.FLDGROUP.PARAMETER);
                    string StoreName;
                    List<string> values = new List<string>();
                    if (MaintainInfo.Approve == Core.CODES.MODMAINTAIN.APROVE.YES && !string.IsNullOrEmpty(Program.txnum))
                    {
                        values.Add(Program.txnum);
                        StoreName = "sp_tllog_file_sel";
                    }
                    else
                    {
                        foreach (var field in fields)
                        {
                            if (this[field.FieldID] != null) //HUYVQ: 05/06/2014
                            {
                                values.Add(this[field.FieldID].ToString());
                            }
                        }
                        StoreName = MaintainInfo.ReportStore;
                    }                                    
                    // End TuanLM
                    if (values.Count > 0)
                    {
                        using (var ctrlSA = new SAController())
                        {
                            DataContainer container = null;

                            ctrlSA.ExecuteProcedureFillDataset(out container, StoreName, values);
                            if (container != null && container.DataTable != null)
                            {
                                var resultTable = container.DataTable;

                                if (resultTable.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= resultTable.Rows.Count - 1; i++)
                                    {
                                        if (!string.IsNullOrEmpty(resultTable.Rows[i]["filestore"].ToString()))
                                        {
                                            using (System.IO.MemoryStream ms = new System.IO.MemoryStream((Byte[])resultTable.Rows[i]["filestore"]))
                                            {
                                                filetempname = System.Environment.GetEnvironmentVariable("TEMP") + "\\" + RandomString(10, false) + resultTable.Rows[i]["filename"].ToString();
                                                FileStream flStream = new FileStream(filetempname, FileMode.OpenOrCreate);
                                                StreamReader memoryReader = new StreamReader(ms);

                                                ms.WriteTo(flStream);
                                                lnkFile.Text = filetempname;
                                                flStream.Close();
                                                flStream.Dispose();
                                            }
                                            if (_open)
                                            {
                                                OpenFile(filetempname);
                                            }
                                        }
                                        else
                                        {
                                            lnkFile.Visible = false;
                                        }                                                                       
                                    }
                                }
                                //else
                                //{
                                //    throw ErrorUtils.CreateError(ERR_FILE.ERR_FILE_IS_NOT_ATTACKED);                                   
                                //}
                            }

                        }
                    }
                }
                else
                {
                    throw ErrorUtils.CreateError(ERR_FILE.ERR_FILE_IS_NOT_ATTACKED);                    
                }

            }
            catch (Exception ex)
            {                
                ShowError(ex);
            }
        
        }
        private void DeleteFile(string _filename)
        {
            System.IO.File.Delete(_filename);
        }
        
        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder sb = new StringBuilder();
            char c;
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                c = Convert.ToChar(Convert.ToInt32(rand.Next(65, 87)));
                sb.Append(c);
            }
            if (lowerCase)
                return sb.ToString().ToLower();
            return sb.ToString();

        }
        private void OpenFile(string _filename)
        {
            ProcessStartInfo pi = new ProcessStartInfo(_filename);
            pi.Arguments = Path.GetFileName(_filename);
            pi.UseShellExecute = true;
            pi.WorkingDirectory = Path.GetDirectoryName(_filename);
            pi.FileName = _filename;
            pi.Verb = "OPEN";
            System.Diagnostics.Process.Start(pi);
        }
       

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateModule())
            {
                LockUserAction();

                new WorkerThread(
                    delegate
                    {
                        try
                        {
                            using (var ctrlSA = new SAController())
                            {
                                var repeatInput = false;
                                DataContainer container = null;
                               
                                    switch (ModuleInfo.SubModule)
                                    {
                                        case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_ADD:
                                            if (MaintainInfo.AddRepeatInput == Core.CODES.MODMAINTAIN.REPEATINPUT.YES)
                                                repeatInput = true;
                                            break;
                                    }

                                    RequireRefresh = true;

                                    if (MaintainInfo.ShowSuccess == Core.CODES.MODMAINTAIN.SHOWSUCCESS.YES)
                                    {
                                        RowFormattable fmtRow = null;

                                        if (container != null)
                                        {
                                            var rows = container.DataTable.Rows;
                                            if (rows.Count == 1)
                                                fmtRow = new RowFormattable(rows[0]);
                                        }

                                        if (fmtRow != null)
                                        {
                                            frmInfo.ShowInfo(Language.Title, string.Format(Language.SuccessStatus, fmtRow), this);
                                        }
                                        else
                                            frmInfo.ShowInfo(Language.Title, Language.SuccessStatus, this);
                                    }

                                    if (!repeatInput)
                                    {
                                        CloseModule();
                                    }
                                    else
                                    {
                                        ResetModuleData();
                                        UnLockUserAction();
                                    }
                                
                            }
                        }
                        catch (Exception ex)
                        {
                            ShowError(ex);
                            UnLockUserAction();
                        }
                    }, this).Start();
            }
        }     

        private void ucMaintain_ModuleClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        //TUDQ them
        private void LoadDataTransaction(string storeName)
        {
            using (var ctrlSA = new SAController())
            {
                if (!string.IsNullOrEmpty(storeName))
                {
                    List<string> values = new List<string>();
                    values.Add(Program.txnum);

                    DataContainer con;
                    ctrlSA.ExecuteTransQuery(out con, MaintainInfo.ModuleID, MaintainInfo.SubModule, values);
                    AssignFieldValuesFromResult(con);
                    DataTable dt = con.DataTable;                   
                }
            }
        }
        //ENd      
    }
}