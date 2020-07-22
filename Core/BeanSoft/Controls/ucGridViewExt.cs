using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppClient.Utils;
using Core.Utils;
using Core.Controllers;
using Core.Common;
using Newtonsoft.Json;
using System.IO;
using Core.Entities;
using AppClient.Interface;

namespace AppClient.Controls
{
    public partial class ucGridViewExt : ucModule, IParameterFieldSupportedModule
    {
        private readonly int m_MaxPageSize =
            App.Environment.ClientInfo.UserProfile.MaxPageSize;
        private BufferedResultManager BufferResult { get; set; }
        protected int SelectedPage { get; set; }
        List<ButtonInfo> buttons = new List<ButtonInfo>();
        string SubModID = string.Empty;
        AppClient.ClientEnvironment environment = new ClientEnvironment();

        public MaintainModuleInfo MaintainInfo {
            get
            {
                return (MaintainModuleInfo)ModuleInfo;
            }
        }
        public ucGridViewExt()
        {
            InitializeComponent();
        }

        protected override void InitializeGUI(DevExpress.Skins.Skin skin)
        {
            base.InitializeGUI(skin);
            //BuildButtons();
            Button button = new Button();
            button.Click += this.Button_Click;
        }

            private void Button_Click(object sender, EventArgs e)
        {
            var btnsender = (Button)sender;
            BuildButtons();
            foreach (var button in buttons)
            {
                var buttonParams = ModuleUtils.GetSearchButtonParams(button);
                if (button.ButtonID == btnsender.Tag.ToString())
                {
                    ExecuteClick(button, buttonParams);
                }
            }
        }

        public override void Execute()
        {
            try
            {
                string data = string.Empty;
                var dataCacheClients = App.Environment.ClientInfo.DataClientCache;
                DataTable dt = new DataTable();
                if (dataCacheClients.Count > 0 )
                {
                    foreach (var dataCache in dataCacheClients)
                    {
                        DataTable dtTemp = new DataTable();
                        if (SubModID + App.Environment.ClientInfo.SessionKey == dataCache.JsonKey)
                        {
                            data = "[" + dataCache.JsonData + "]";
                            dtTemp = (DataTable)JsonConvert.DeserializeObject(data, (typeof(DataTable)));
                        }
                        dt.Merge(dtTemp);
                    }
                }
                gcMain.DataSource = dt;
            }
            catch(Exception ex)
            {
                ShowError(ex);
            }
        }
       
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Call Modmaitain Add
            BuildButtons();
            foreach (var button in buttons)
            {
                var buttonParams = ModuleUtils.GetSearchButtonParams(button);
                if (button.ButtonID ==btnAdd.Tag.ToString())
                {
                    SubModID = button.CallModuleID;
                    ExecuteClick(button, buttonParams);
                }
            }

        }
        protected override void BuildButtons()
        {
            buttons = ModuleUtils.GetSearchButtons(((ModuleFieldInfo)this.Tag).ModuleID);
        }
        private void ExecuteClick(ButtonInfo button, List<ButtonParamInfo> buttonParams)
        {
            var targetModule = MainProcess.CreateModuleInstance(button.CallModuleID, button.CallSubModule);
            if (targetModule != null)
            {
                foreach (var param in buttonParams)
                {
                    AssignConditionParam(targetModule, param);
                    AssignValueParam(targetModule, param);
                }

                targetModule.ModuleClosed += SubModule_Closed;
                targetModule.ShowModule(this);
            }
        }
        private static void AssignValueParam(ucModule targetModule, ButtonParamInfo param)
        {
            if (!string.IsNullOrEmpty(param.Value))
            {
                var targetFields = FieldUtils.GetModuleFieldsByName(targetModule.ModuleInfo.ModuleID, param.FieldName);
                foreach (var field in targetFields)
                {
                    targetModule[field.FieldID] = param.Value.Decode(field);
                }
            }
        }

        private void AssignConditionParam(ucModule targetModule, ButtonParamInfo param)
        {
            if (!string.IsNullOrEmpty(param.ConditionName))
            {
                var field = FieldUtils.GetModuleFieldsByName(
                    ModuleInfo.ModuleID,
                    Core.CODES.DEFMODFLD.FLDGROUP.COMMON,
                    param.ConditionName)[0];
                targetModule.SetFieldValue(param.FieldName, this[field.FieldID]);
            }
        }

        void SubModule_Closed(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                this.Invoke(new EventHandler(SubModule_Closed), sender, e);
            }
            else
            {
                if (((ucModule)sender).RequireRefresh) Execute();
            }
        }

        private List<DataRowView> GetSelectedRows()
        {
            var selectedRows = new List<DataRowView>();

            if (CheckMarksUtils != null)
            {
                if (CheckMarksUtils.SelectedCount == 0)
                {
                    if (gvMain.FocusedRowHandle >= 0)
                    {
                        selectedRows.Add((DataRowView)gvMain.GetRow(gvMain.FocusedRowHandle));
                    }
                }
                else
                {
                    for (var i = 0; i < CheckMarksUtils.SelectedCount; i++)
                    {
                        selectedRows.Add((DataRowView)CheckMarksUtils.GetSelectedRow(i));
                    }
                }
            }
            else
            {
                if (gvMain.FocusedRowHandle >= 0)
                    selectedRows.Add((DataRowView)gvMain.GetRow(gvMain.FocusedRowHandle));
            }

            return selectedRows;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DelDataFromCacheClient();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }
        private void DelDataFromCacheClient()
        {
            var selectedRows = GetSelectedRows();
            var datacachetemp = new DataClientCache();
            foreach (var datacache in App.Environment.ClientInfo.DataClientCache)
            {
                if (datacache.PrKey == int.Parse(selectedRows[0].Row[0].ToString()) && (datacache.JsonKey == SubModID  + App.Environment.ClientInfo.SessionKey))
                {
                    datacachetemp = datacache;
                }
            }
            App.Environment.ClientInfo.DataClientCache.Remove(datacachetemp);
            Execute();
            //gvMain.RefreshData();
        }

        private void ExecuteClickWithSingleRow(ButtonInfo button, List<ButtonParamInfo> buttonParams, DataRowView selectedRow, bool autoRefresh)
        {
            var targetModule = MainProcess.CreateModuleInstance(button.CallModuleID, button.CallSubModule);
            if (targetModule != null)
            {
                foreach (var param in buttonParams)
                {
                    AssignValueParam(targetModule, param);
                    AssignConditionParam(targetModule, param);
                    AssignColumnParam(targetModule, param, selectedRow);
                }

                if (autoRefresh) targetModule.ModuleClosed += SubModule_Closed;
                targetModule.ShowModule(this);

            }
        }
        private static void AssignColumnParam(ucModule targetModule, ButtonParamInfo param, DataRowView row)
        {
            if (!string.IsNullOrEmpty(param.ColumnName))
            {
                targetModule.SetFieldValue(param.FieldName, row[param.ColumnName]);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRows = GetSelectedRows();
                BuildButtons();
                foreach (var button in buttons)
                {
                    if (button.ButtonID == btnEdit.Tag.ToString())
                    {
                        var buttonParams = ModuleUtils.GetSearchButtonParams(button);
                        SubModID = button.CallModuleID;
                        ExecuteClickWithSingleRow(button, buttonParams, selectedRows[0], true);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }
    }
}
