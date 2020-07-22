using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppClient.Interface;
using Core.Base;
using Core.Controllers;
using Core.Entities;
using Core.Utils;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraLayout;

namespace AppClient.Controls
{
    public partial class ucDashboardView : ucModule,
        IParameterFieldSupportedModule
    {
        public DashboardInfo DashboardInfo {
            get
            {
                return (DashboardInfo)ModuleInfo;
            }
        }
        bool firstLoad = true;
        private DataTable data;
        public LayoutControl CommonLayout {
            get { return layoutControl1; }
        }
        public ucDashboardView()
        {
            InitializeComponent();
            //Execute();
            //RefreshData();
        }
        protected override void BuildFields()
        {
            base.BuildFields();
            if (Parent is ContainerControl)
                ((ContainerControl)Parent).ActiveControl = layoutControl1;

        }
        protected override void BuildButtons()
        {
            SetupContextMenu(layoutControl1);
            StartAllTasks();
#if DEBUG
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
            SetupSaveLayout(layoutControl1);
            SetupSaveAllLayout(layoutControl1);
#endif
        }
        public override void InitializeLayout()
        {
            //StartAllTasks();
            //int test = DashboardInfo.Autoupdate;
            base.InitializeLayout();
        }
        void worker_DoUpdateGUI(object sender, EventArgs e)
        {
        }

        public override void LockUserAction()
        {
            base.LockUserAction();

            if (!InvokeRequired)
            {
                ShowWaitingBox();
                Enabled = false;
            }
        }

        public override void UnLockUserAction()
        {
            base.UnLockUserAction();

            if (!InvokeRequired)
            {
                HideWaitingBox();
                Enabled = true;
            }
        }

        public override void Execute()
        {

            try
            {
                using (var client = new SAController())
                {
                    DataContainer con;
                    var values = new List<string>();

                    client.ExecuteProcedureFillDataset(out con, DashboardInfo.Source, values);
                    data = con.DataSet.Tables[0];

                    var dashBoard = new Dashboard();
                    dashBoard.LoadFromXml(CommonUtils.StringToStream(DashboardInfo.Layout));
                    dashboardViewer.Dashboard = dashBoard;

                    firstLoad = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

        }
        private bool stop = false;
        private async void StartAllTasks()
        {
            LoopTask(() => Task1(), DashboardInfo.Autoupdate);

        }

        private async void LoopTask(Func<Task> task, int delay)
        {
            while (!stop)
            {
                try
                {
                    await task();
                }
                catch
                {
                }

                await Task.Delay(delay);
            }
        }

        private async Task Task1()
        {
            if (firstLoad)
            {
                Execute();
            }
            else
            {
                dashboardViewer.ReloadData();
            }
        }
        //public  async void RefreshData()
        //{
        //    if (firstLoad)
        //    {
        //        Execute();
        //    }
        //    else
        //    {
        //        dashboardViewer1.ReloadData();
        //    }
        //    await Task.Run(new Action(Longtask));
        //}

        //public  void Longtask()
        //{
        //    try
        //    {
        //       // dashboardViewer1.ReloadData();
        //        Thread.Sleep(DashboardInfo.Autoupdate);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private void dabView_ConfigureDataConnection(object sender, DevExpress.DashboardCommon.DashboardConfigureDataConnectionEventArgs e)
        {
            OracleConnectionParameters ocp = e.ConnectionParameters as OracleConnectionParameters;
            if (ocp != null)
            {
                using (var client = new SAController())
                {
                    bool vResult;
                    client.Reconnect(out vResult, out ocp);
                }
            }
        }

        private void dashboardViewer_DataLoading(object sender, DataLoadingEventArgs e)
        {
            e.Data = data;
        }
    }
}
