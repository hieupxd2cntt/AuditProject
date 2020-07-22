using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using AppClient.Interface;
using Core.Base;
using Core.Common;
using Core.Controllers;
using Core.Entities;
using Core.Extensions;
using Core.Utils;
using Newtonsoft.Json;

namespace AppClient.Controls
{
    public partial class ucWorkFlowMaster : ucModule,
        IParameterFieldSupportedModule,
        ICommonFieldSupportedModule
    {
        private string ModID;
        private string WorkLogsID;
        private string WorkLogsRowID;
        private int steps = 1;
        private int isLast;
        List<TaskInfo> taskInfos = new List<TaskInfo>();

        #region ICommonFieldSupportedModule Members

        public bool ValidateRequire {
            get { return true; }
        }

        public LayoutControl CommonLayout {
            get { return mainLayout; }
        }

        public string CommonLayoutStoredData {
            get
            {
                return Language.AddLayout;
            }
        }
        #endregion

        #region Properties & Members               

        public MaintainModuleInfo MaintainInfo {
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
                btnCommit.Enabled = false;
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
            InitMenuBar();
            btnCommit.Enabled = false;
            lbTitle.BackColor = ThemeUtils.BackTitleColor;
            lbTitle.ForeColor = ThemeUtils.TitleColor;
        }
        private void ucWorkFlowMaster_Load(object sender, EventArgs e)
        {

        }

        #region InitBarWorkFlowMenu
        private void InitMenuBar()
        {
            AddNewGroup();
            acdWorkFlowMenu.ElementClick += new ElementClickEventHandler(this.acdWorkFlowMenu_ElementClick);
            acdWorkFlowMenu.AllowItemSelection = true;
            btnPrev.Enabled = false;
            btnNext.Enabled = true;
        }
        private void AddNewGroup()
        {
            string GroupName = "";
            string GroupText = "";
            taskInfos = InitWorkFlow(ModuleInfo.ModuleID, out GroupName, out GroupText);
            AccordionControlElement objElement = new AccordionControlElement()
            {
                Style = ElementStyle.Group,
                Name = GroupName,
                Expanded = true,
                Text = GroupText,
            };
            objElement.ImageOptions.Image = ThemeUtils.Image32.Images["workflow"];
            acdWorkFlowMenu.Elements.Add(objElement);

            foreach (var task in taskInfos)
            {
                AddNewItem(objElement, task);
            }

            CallTask(taskInfos[0].TaskMod, Core.CODES.DEFMOD.SUBMOD.MAINTAIN_ADD);
        }
        private void AddNewItem(AccordionControlElement GroupName, TaskInfo task)
        {
            AccordionControlElement _Element = new AccordionControlElement()
            {
                Name = task.TaskMod,
                Style = ElementStyle.Item,
                Tag = task.STT,
                Text = task.TaskDesc
            };
            _Element.ImageOptions.Image = ThemeUtils.Image24.Images[task.ImageUri];
            GroupName.Elements.Add(_Element);
        }
        private void acdWorkFlowMenu_ExpandStateChanged(object sender, DevExpress.XtraBars.Navigation.ExpandStateChangedEventArgs e)
        {
            e.Element.Appearance.Normal.BackColor = Color.Maroon;
            e.Element.Appearance.Hovered.BackColor = Color.Maroon;
        }
        #endregion

        private void acdWorkFlowMenu_ElementClick(object sender, ElementClickEventArgs e)
        {
            if (e.Element.Style == DevExpress.XtraBars.Navigation.ElementStyle.Group) return;
            if (e.Element.Name == null) return;
            steps = Int32.Parse(e.Element.Tag.ToString());
            SetButtonControl();
            CallTask(e.Element.Name.ToString(), Core.CODES.DEFMOD.SUBMOD.MAINTAIN_ADD);
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
                        LoadData(MaintainInfo.EditSelectStore);
                        break;
                    case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_VIEW:
                        lbTitle.Text = Language.ViewTitle;
                        LoadData(MaintainInfo.ViewSelectStore);                        
                        break;
                    case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_ADD:
                        lbTitle.Text = Language.AddTitle;
                        LoadData(MaintainInfo.AddSelectStore);
                        break;                    
                    case Core.CODES.DEFMOD.SUBMOD.TRANSACTION_VIEW:
                        lbTitle.Text = Language.AddTitle;
                        LoadDataTransaction(MaintainInfo.TRANSATIONSTORE);
                        break;                        
                }

                // Configuration lbTitle
                switch (ModuleInfo.SubModule)
                {
                    case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_EDIT:
                        lbTitle.Text = string.Format(lbTitle.Text, this);
                        break;
                    case Core.CODES.DEFMOD.SUBMOD.MAINTAIN_VIEW:
                        lbTitle.Text = string.Format(lbTitle.Text, this);
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
            if (InvokeRequired)
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
            CloseModule();
        }

        #endregion

        public ucWorkFlowMaster()
        {
            InitializeComponent();
        }

        public delegate void PInvoke();
        public void P()
        {
            if (InvokeRequired)
            {
                Invoke(new PInvoke(P));
                return;
            }
        }

        //public override void Execute()
        public void Execute()
        {
            if (ValidateModule())
            {
                LockUserAction();

                new WorkerThread(
                    delegate {
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
                                        try
                                        {
                                            AssignValue();
                                        }
                                        catch (Exception ex)
                                        {

                                        }
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


        public override void LockUserAction()
        {
            base.LockUserAction();

            if (!InvokeRequired)
            {
                Enabled = false;
                ShowWaitingBox();
            }
        }

        public override void UnLockUserAction()
        {
            base.UnLockUserAction();

            if (!InvokeRequired)
            {
                Enabled = true;
                HideWaitingBox();
                ActiveControl = mainLayout;
                mainLayout.Focus();
            }
        }

        private void ucMaintain_ModuleClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

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

        private List<TaskInfo> InitWorkFlow(string ModID, out string GroupName, out string GroupDesc)
        {
            GroupName = null;
            GroupDesc = null;
            try
            {
                var taskInfo = new List<TaskInfo>();
                List<string> values = new List<string>();
                values.Add(ModID);
                DataContainer container;
                using (var ctrlSA = new SAController())
                {
                    ctrlSA.ExecuteProcedureFillDataset(out container, "sp_definetask_sel_maintain", values);
                    var ds = container.DataSet;

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            var objTask = new TaskInfo();
                            objTask.TaskMod = ds.Tables[0].Rows[i]["TASKMOD"].ToString();
                            objTask.TaskName = ds.Tables[0].Rows[i]["TASKNAME"].ToString();
                            objTask.TaskDesc = ds.Tables[0].Rows[i]["TASKDESC"].ToString();
                            objTask.ImageUri = ds.Tables[0].Rows[i]["IMAGEURI"].ToString();
                            objTask.STT = isLast = Int16.Parse(ds.Tables[0].Rows[i]["STT"].ToString());
                            GroupName = ds.Tables[0].Rows[i]["NAME"].ToString();
                            GroupDesc = ds.Tables[0].Rows[i]["DESCRIPTION"].ToString();
                            taskInfo.Add(objTask);
                        }
                    }
                }
                return taskInfo;
            }
            catch (System.ServiceModel.FaultException ex)
            {
                return null;
            }
        }

        private void ProcessWorkFlow(string ModID)
        {
            try
            {
                List<string> values = new List<string>();
                values.Add(ModID);
                values.Add(WorkLogsID);
                DataContainer container;
                string Modid = "";
                string SubMod = "";
                using (var ctrlSA = new SAController())
                {
                    ctrlSA.ExecuteProcedureFillDataset(out container, "sp_definetask_sel", values);
                    var ds = container.DataSet;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Modid = ds.Tables[0].Rows[0]["TASKMOD"].ToString();
                        SubMod = ds.Tables[0].Rows[0]["TASKSUBMOD"].ToString();
                        WorkLogsRowID = ds.Tables[0].Rows[0]["ROWID"].ToString();
                        CallTask(Modid, SubMod);
                    }

                }
            }
            catch (FaultException ex)
            {
                ShowError(ex);
            }
        }

        private void WorkFlowUpdateStatus(string v_Status)
        {
            try
            {
                List<string> values = new List<string>();
                values.Add(WorkLogsRowID);
                values.Add(v_Status);
                values.Add(ModID);
                DataContainer container;
                using (var ctrlSA = new SAController())
                {
                    ctrlSA.ExecuteProcedureFillDataset(out container, "dev_workflowlog_upd_status", values);
                }
            }
            catch (FaultException ex)
            {
                ShowError(ex);
            }
        }

        private void CallTask(string v_modid, string v_Submod)
        {
            var moduleInfo = ModuleUtils.GetModuleInfo(v_modid);
            List<string> values = new List<string>();
            values.Add(moduleInfo.ModuleName + ".Layout[Add]");
            DataContainer container;
            using (var ctrlSA = new SAController())
            {
                ctrlSA.ExecuteProcedureFillDataset(out container, "get_layout_from_langname", values);
                var ds = container.DataSet;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(ds.Tables[0].Rows[0][0].ToString())))
                    {
                        mainLayout.RestoreLayoutFromStream(ms);
                        ms.Close();
                    }
                }
            }
        }

        private void AssignValue()
        {
            var fields = GetModuleFields();
            var param = new Dictionary<string, object>();
            foreach (var field in fields)
            {
                if (!string.IsNullOrEmpty(field.ParameterName))
                {
                    param.Add(field.FieldID, this[field.FieldID].Encode(field));
                }
            }
            // Write to Json file
            string json = JsonConvert.SerializeObject(param, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(@"D:\Data.txt", json);
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            steps += 1;
            SetButtonControl();
            NextStep(steps);
        }
        private void BtnPrev_Click(object sender, EventArgs e)
        {
            steps -= 1;
            SetButtonControl();
            PreStep(steps);
        }
        private void NextStep(int step)
        {
            var task = taskInfos.Where(x => x.STT == step).ToList();
            if (task.Count > 0) CallTask(task[0].TaskMod, Core.CODES.DEFMOD.SUBMOD.MAINTAIN_ADD);
        }
        private void PreStep(int step)
        {
            var task = taskInfos.Where(x => x.STT == step).ToList();
            if (task.Count > 0) CallTask(task[0].TaskMod, Core.CODES.DEFMOD.SUBMOD.MAINTAIN_ADD);
        }
        private void SetButtonControl()
        {
            if (steps == isLast) { btnPrev.Enabled = true; btnNext.Enabled = false; btnCommit.Enabled = true; }
            if (steps == 1) { btnPrev.Enabled = false; btnNext.Enabled = true; }
        }
    }
}