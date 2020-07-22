using System.Collections.Generic;
using System.Data;
using DevExpress.Skins;
using Core.Base;
using System.Reflection;
using System;
using System.Threading.Tasks;
using DevExpress.Utils;
using DevExpress.Diagram.Core;
using DevExpress.XtraDiagram;
using System.Drawing;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Repository;
using Core.Entities;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Rows;
using AppClient.Interface;
using DevExpress.XtraLayout;
using DevExpress.XtraGrid;
using System.Windows.Forms;

namespace AppClient.Controls.Ext.frmRelaConect
{
    public partial class frmRelaConect : ucModule, ICommonFieldSupportedModule,
                IParameterFieldSupportedModule
    {

        public RelationEntites relation = new RelationEntites();
        private ReturnFromDB rbd = new ReturnFromDB();

        public frmRelaConect()
        {
            InitializeComponent();
            PauseCallback = false;
            StopCallback = false;
            //LoadParameter();
            SetupUI();
        }

        private void LoadParameter()
        {
            rbd.getListParam(relation);
            return;
        }

        private void SetupUI()
        {
            //SetupUI
            ribbonControl1.Minimized = true;

            diagramControl1.ShowToolTips = true;
            diagramControl1.MouseDoubleClick += DiagramControl1_MouseDoubleClick;
            diagramControl1.ToolTipController = toolTipController1;
            diagramControl1.ToolTipController.BeforeShow += ToolTipController_BeforeShow;
            diagramControl1.MouseClick += DiagramControl1_MouseClick;
            diagramControl1.ShowingEditor += (object sender, DiagramShowingEditorEventArgs e) => { e.Cancel = true; };
            diagramControl1.LostFocus += (object sender, EventArgs e) => { RervertColor(((DiagramControl)sender).Items); };
            return;
        }

        private void DiagramControl1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            var item = diagramControl1.CalcHitItem(e.Location);
            if (item is DiagramShape)
            {
                if (((DiagramShape)item).Tag != null)
                {
                    try
                    {
                        RelationEntites.Investor inves = new RelationEntites.Investor();
                        inves.AcctNo = ((DiagramShape)item).Tag.ToString();
                        inves = relation.ListInvestor[relation.ListInvestor.BinarySearch(inves)];

                        ToolTipController controller = toolTipController1;
                        var gridTooltip = controller.ActiveObject as GridToolTipInfo;
                        controller.HideHint();
                        string Text = @"Số tài khoản: " + inves.AcctNo + "\n" +
                                             @"Chủ tài khoản: " + inves.InvestorName + "\n" +
                                             @"Số CMND/HC: " + inves.IdNo + "\n" +
                                             @"Nơi Cấp: " + inves.IdPlace + "\n" +
                                             @"Địa chỉ: " + inves.Address + "\n" +
                                             @"Tổng giá trị giao dịch: " + long.Parse(inves.Weight).ToString("#,##0");

                        controller.ShowHint(Text);

                        //SuperToolTip supperToolTip = new SuperToolTip();
                        //SuperToolTipSetupArgs args = new SuperToolTipSetupArgs();

                        //args.Contents.Text = @"<b>Số tài khoản:</b> " + inves.AcctNo + "\n" + "\n" +
                        //                     @"<b>Chủ tài khoản:</b> " + inves.InvestorName + "\n" + "\n" +
                        //                     @"<b>Số CMND/HC:</b> " + inves.IdNo + "\n" + "\n" +
                        //                     @"<b>Nơi Cấp:</b> " + inves.IdPlace + "\n" + "\n" +
                        //                     @"<b>Địa chỉ:</b> " + inves.Address + "\n" + "\n" +
                        //                     @"<b>Tổng giá trị giao dịch:</b> " + inves.Weight;


                        //args.Contents.Image = Properties.Resources.NgocTrinh;
                        //args.Footer.Text = "Public by SSC";
                        //args.ShowFooterSeparator = true;

                        //supperToolTip.Setup(args);
                        //supperToolTip.AllowHtmlText = DefaultBoolean.True;
                        //ToolTipControlInfo info = new ToolTipControlInfo(gridTooltip, args.Contents.Text);
                        //info.SuperTip = supperToolTip;
                        //info.ToolTipType = ToolTipType.SuperTip;
                        //ToolTipController toolTipController = new DevExpress.Utils.ToolTipController();
                        //toolTipController.ShowBeak = true;
                        //toolTipController.KeepWhileHovered = true;
                        //toolTipController.CalcSize += (object senderCalc, ToolTipControllerCalcSizeEventArgs eCalc) => {
                        //    try
                        //    {
                        //        Size size = Size.Empty;
                        //        System.Drawing.Size shape = new System.Drawing.Size();
                        //        shape.Width = 500;//any size  you want
                        //        shape.Height = 300;//any size  you want
                        //        size = shape;
                        //        eCalc.Size = size;
                        //    }
                        //    catch (Exception)
                        //    { }
                        //};

                        //toolTipController.CloseOnClick = DefaultBoolean.True;
                        //toolTipController.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
                        //toolTipController.ShowHint(info);
                    }
                    catch (Exception)
                    { }
                }
            }
            else
                RervertColor(((DiagramControl)sender).Items);
        }

        private void DiagramControl1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            RervertColor(((DiagramControl)sender).Items);
            var item = diagramControl1.CalcHitItem(e.Location);
            if (item is DiagramShape)
            {
                SetColor(diagramControl1.Items);
                HilighDiagram((DiagramShape)item);
            }
        }

        private void ToolTipController_BeforeShow(object sender, ToolTipControllerShowEventArgs e)
        {
            if (sender is DiagramShape)
            {
                if (((DiagramShape)sender).Tag != null)
                {
                    try
                    {
                        RelationEntites.Investor inves = new RelationEntites.Investor();
                        inves.AcctNo = ((DiagramShape)sender).Tag.ToString();
                        inves = relation.ListInvestor[relation.ListInvestor.BinarySearch(inves)];

                        ToolTipController controller = toolTipController1;
                        var gridTooltip = controller.ActiveObject as GridToolTipInfo;
                        controller.HideHint();

                        SuperToolTip supperToolTip = new SuperToolTip();
                        SuperToolTipSetupArgs args = new SuperToolTipSetupArgs();

                        args.Contents.Text = @"<b>Số tài khoản:</b> " + inves.AcctNo + "\n" + "\n" +
                                             @"<b>Chủ tài khoản:</b> " + inves.InvestorName + "\n" + "\n" +
                                             @"<b>Số CMND/HC:</b> " + inves.IdNo + "\n" + "\n" +
                                             @"<b>Nơi Cấp:</b> " + inves.IdPlace + "\n" + "\n" +
                                             @"<b>Địa chỉ:</b> " + inves.Address + "\n" + "\n" +
                                             @"<b>Tổng giá trị giao dịch:</b> " + inves.Weight;


                        args.Contents.Image = Properties.Resources.NgocTrinh;
                        args.Footer.Text = "Public by SSC";
                        args.ShowFooterSeparator = true;

                        supperToolTip.Setup(args);
                        supperToolTip.AllowHtmlText = DefaultBoolean.True;
                        ToolTipControlInfo info = new ToolTipControlInfo(gridTooltip, args.Contents.Text);
                        info.SuperTip = supperToolTip;
                        info.ToolTipType = ToolTipType.SuperTip;
                        ToolTipController toolTipController = new DevExpress.Utils.ToolTipController();
                        toolTipController.ShowBeak = true;
                        toolTipController.KeepWhileHovered = true;
                        toolTipController.CalcSize += (object senderCalc, ToolTipControllerCalcSizeEventArgs eCalc) =>
                        {
                            try
                            {
                                Size size = Size.Empty;
                                System.Drawing.Size shape = new System.Drawing.Size();
                                shape.Width = 500;//any size  you want
                                shape.Height = 300;//any size  you want
                                size = shape;
                                eCalc.Size = size;
                            }
                            catch (Exception)
                            { }
                        };
                        toolTipController.AutoPopDelay = 1;
                        toolTipController.CloseOnClick = DefaultBoolean.True;
                        toolTipController.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
                        toolTipController.ShowHint(info);
                    }
                    catch (Exception)
                    { }
                }
            }
        }

        private void HilighDiagram(DiagramShape item)
        {
            var hilightColor = Color.Red;
            var unHilightColor = Color.Gray;
            //item.Appearance.BackColor = hilightColor;
            foreach (var diagramItem in diagramControl1.Items)
            {
                if (diagramItem is DiagramConnector)
                {
                    if (((DiagramConnector)diagramItem).BeginItem == item || ((DiagramConnector)diagramItem).EndItem == item)
                    {
                        //((DiagramConnector)diagramItem).Appearance.BorderColor = hilightColor;
                    }
                    else
                    {
                        ((DiagramConnector)diagramItem).Appearance.BorderColor = unHilightColor;
                        ((DiagramConnector)diagramItem).Appearance.ForeColor = unHilightColor;
                    }
                }
                else if (diagramItem is DiagramShape)
                {
                    if ((DiagramShape)diagramItem != item)
                        ((DiagramShape)diagramItem).Appearance.BackColor = unHilightColor;
                }
            }
        }

        List<Color> listHistColor = new List<Color>();

        private void SetColor(DiagramItemCollection items)
        {
            listHistColor.Clear();
            if (items.Count > 0)
                for (int i = 0; i < items.Count; i++)
                {
                    Color c = (items[i].Appearance.BackColor);
                    listHistColor.Add(c);
                }
        }

        private void RervertColor(DiagramItemCollection items)
        {
            if (listHistColor.Count > 0)
                for (int i = 0; i < listHistColor.Count; i++)
                {
                    if (items[i] is DiagramConnector)
                    {
                        items[i].Appearance.BorderColor = listHistColor[i];
                        items[i].Appearance.ForeColor = listHistColor[i];
                    }
                    else
                        items[i].Appearance.BackColor = listHistColor[i];
                }
            listHistColor.Clear();
        }

        public RelationEntites RelationEntitesData()
        {
            return relation;
        }

        #region Override methods
        protected override void InitializeGUI(Skin skin)
        {
            base.InitializeGUI(skin);
        }

        protected override void BuildFields()
        {
            base.BuildFields();
            if (Parent is System.Windows.Forms.ContainerControl)
                ((System.Windows.Forms.ContainerControl)Parent).ActiveControl = ParamLayout;
        }

        protected override void BuildButtons()
        {
#if DEBUG
            SetupContextMenu(ParamLayout);
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
            ParamLayout.OptionsCustomizationForm.ShowPropertyGrid = true;
#endif
        }

        /// <summary>
        /// Hiển thị Module
        /// </summary>
        public override void ShowModule(IWin32Window owner)
        {
            base.ShowModule(owner);
            StopCallback = false;
        }

        #endregion

        #region ICommonFieldSupportedModule Members

        public bool ValidateRequire
        {
            get { return true; }
        }

        public LayoutControl CommonLayout
        {
            get { return ParamLayout; }
        }

        public string CommonLayoutStoredData
        {
            get { return Language.Layout; }
        }

        #endregion

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateModule())
                {
                    LockUserAction();
                    GetOracleParameterValues(out relation.ListParameter, "PKG_RELATIONSHIP_MAP.SP_GET_LIST_ACCOUNT_DATA_1");
                    rbd.getData(relation);
                    if (relation.ListInvestor != null && relation.ListConnection != null)
                    {
                        DrawDiagram();
                    }
                    UnLockUserAction();
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void DrawDiagram()
        {
            diagramControl1.NewDocument();
            AddItemsInLine(relation.ListInvestor, new PointFloat(600, 50), new Size(75, 75), 20, (Investor, item) =>
            {
                InvestorToInvestorCache[item] = Investor;
            });
            CreateConnections();
        }

        readonly Dictionary<IDiagramItem, RelationEntites.Investor> InvestorToInvestorCache = new Dictionary<IDiagramItem, RelationEntites.Investor>();

        void AddItemsInLine<T>(IEnumerable<T> itemsSource, PointFloat startPosition, Size itemSize, int margin, Action<T, DiagramShape> itemAction)
        {
            PointFloat position = startPosition;
            foreach (var dataItem in itemsSource)
            {
                var diagramItem = new DiagramShape() { Width = itemSize.Width, Height = itemSize.Height };
                diagramItem.Shape = BasicShapes.Ellipse;
                diagramItem.CanResize = diagramItem.CanCopy = diagramItem.CanDelete = false;
                var AcctNo = dataItem.GetType().GetProperty("AcctNo").GetValue(dataItem, null).ToString();
                //var InvestorName = dataItem.GetType().GetProperty("InvestorName").GetValue(dataItem, null).ToString();
                //var IdNo = dataItem.GetType().GetProperty("IdNo").GetValue(dataItem, null).ToString();
                //var IdPlace = dataItem.GetType().GetProperty("IdPlace").GetValue(dataItem, null).ToString();
                //diagramItem.Content = string.Format("TK: {0} \n Chủ TK: {1} \n Số CMND/HC: {2} \n Nơi Cấp: {3}", AcctNo);
                diagramItem.Content = string.Format("TK: {0} ", AcctNo);
                diagramItem.Tag = AcctNo;
                diagramItem.Position = position;
                (diagramItem as IDiagramItem).FontSize = 10;
                position.Offset(0, itemSize.Height + margin);
                diagramControl1.Items.Add(diagramItem);
                itemAction(dataItem, diagramItem);
            }
        }

        void CreateConnections()
        {
            var contentItems = diagramControl1.Items.OfType<DiagramShape>();
            const float minThickness = 3, maxThickness = 10;

            var groupedData = relation.ListConnection.GroupBy(x => new { x.AccountNo, x.Co_AccountNo, x.RelationName }).ToArray();
            long minCount = groupedData.Min(x => x.Sum(d => d.Weight));
            long maxCount = groupedData.Max(x => x.Sum(d => d.Weight));
            if (minCount.Equals(maxCount)) maxCount++;
            foreach (var dataItem in groupedData)
            {
                var FromPerson = contentItems.First(x => object.Equals(x.Tag.ToString(), dataItem.Key.AccountNo.ToString()));
                var ToPerson = contentItems.First(x => object.Equals(x.Tag.ToString(), dataItem.Key.Co_AccountNo.ToString()));
                var connector = new DiagramConnector() { BeginArrow = null, EndArrow = null };
                diagramControl1.Items.Add(connector);
                connector.ThemeStyleId = FromPerson.ThemeStyleId;
                connector.BeginItem = FromPerson;
                connector.BeginItemPointIndex = 1;
                connector.EndItem = ToPerson;
                connector.EndItemPointIndex = 3;
                connector.Type = ConnectorType.Straight;
                connector.CanMove = connector.CanCopy = connector.CanDelete = false;
                long quantity = dataItem.Sum(x => x.Weight);
                connector.Content = "Loại quan hệ: " + dataItem.Key.RelationName + "\n Giá trị: " + quantity.ToString("#,##0");
                (connector as IDiagramItem).StrokeThickness = minThickness + (maxThickness - minThickness) * (quantity - minCount) / (maxCount - minCount);
            }
            diagramControl1.UpdateRoute();
        }

        private void diagramCommandOpenFileBarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            rbd.getListFileSaved(relation);

            var ucModule = new ListMaps();
            ucModule.ShowForm(this, ToDataTable(relation.ListRelationEntity));
        }

        public void LoadSavedData(RelationEntites.RelationEntity returnRelationEntites)
        {
            diagramControl1.NewDocument();
            try
            {
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(returnRelationEntites.Relation_Layout);
                writer.Flush();
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                diagramControl1.LoadDocument(stream);
            }
            catch (Exception ex)
            {
            }
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private void diagramCommandSaveFileBarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RervertColor(((DiagramControl)diagramControl1).Items);

            Stream stream = new MemoryStream();
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            this.diagramControl1.SaveDocument(stream);
            stream.Position = 0;
            var sr = new StreamReader(stream);
            relation.relationEntity.Relation_Layout = sr.ReadToEnd();
            relation.relationEntity.Relation_Parameter = relation.ListParam;

            var ucModule = new SaveForm();
            ucModule.ShowForm(this, relation);
        }

        void HideRows(VGridRows rows)
        {
            foreach (BaseRow row in rows)
            {
                if (row is EditorRow) HideRow((EditorRow)row);
                HideRows(row.ChildRows);
            }
        }

        void HideRow(EditorRow row)
        {
            if (row.Properties.Caption.Contains("Content") || row.Properties.Caption.Contains("StrokeThickness"))
                row.Visible = false;
        }

        private void propertyGridControl1_CustomDrawRowValueCell(object sender, DevExpress.XtraVerticalGrid.Events.CustomDrawRowValueCellEventArgs e)
        {
            HideRows(((PropertyGridControl)sender).Rows);
        }
    }

    public class RelationEntites
    {
        public class Investor : IComparable<Investor>
        {
            public string InvestorName { get; set; }
            public string AcctNo { get; set; }
            public string IdNo { get; set; }
            public string Address { get; set; }
            public string IdPlace { get; set; }

            public string Weight { get; set; }

            public int CompareTo(Investor other)
            {
                return string.Compare(this.AcctNo, other.AcctNo);
            }

            public override string ToString()
            {
                return AcctNo;
            }
        }

        public Investor RootInvestor = new Investor();

        public string ListParam = "";

        public List<string> ListParameter = new List<string>();

        public List<Investor> ListInvestor = new List<Investor>();

        public List<Investor> ListInvestorParam = new List<Investor>();

        public class RelationEntity
        {
            public string Id { get; set; }
            public string Relation_Name { get; set; }
            public string Relation_Parameter { get; set; }
            public string Relation_Description { get; set; }
            public string Relation_Layout { get; set; }
            public string Created_By { get; set; }
            public string Created_Time { get; set; }
            public string Edited_By { get; set; }
            public string Edited_Time { get; set; }
        }

        public RelationEntity relationEntity = new RelationEntity();

        public class Connection : IComparable<Connection>
        {
            public string AccountNo { get; set; }
            public string Co_AccountNo { get; set; }
            public string RelationType { get; set; }
            public long Weight { get; set; }
            public string RelationName { get; set; }

            public int CompareTo(Connection other)
            {
                return string.Compare(this.AccountNo, other.AccountNo);
            }

            public override string ToString()
            {
                return AccountNo;
            }
        }

        public List<Connection> ListConnection = new List<Connection>();

        public List<RelationEntity> ListRelationEntity = new List<RelationEntity>();
    }

    public class ReturnFromDB
    {
        Core.Controllers.SAController ctrlSA = new Core.Controllers.SAController();

        public void getListParam(RelationEntites relationEntites)
        {
            relationEntites.ListInvestorParam.Clear();
            DataContainer dc = null;
            List<string> param = new List<string>();
            ctrlSA.ExecuteProcedureFillDataset(out dc, "PKG_RELATIONSHIP_MAP.SP_GET_LIST_ACCOUNT_REL_1", param);
            DataTable tableResult = dc.DataTable;

            for (int i = 0; i < tableResult.Rows.Count; i++)
            {
                RelationEntites.Investor temp = new RelationEntites.Investor();
                PropertyInfo[] Props = temp.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    prop.SetValue(temp, Convert.ChangeType(tableResult.Rows[i][prop.Name].ToString(), prop.PropertyType), null);
                }

                relationEntites.ListInvestorParam.Add(temp);
            }

            return;
        }

        public void getData(RelationEntites relationEntites)
        {
            Task task1 = Task.Factory.StartNew(() =>
            {
                relationEntites.ListInvestor.Clear();

                DataContainer dc = null;
                ctrlSA.ExecuteProcedureFillDataset(out dc, "PKG_RELATIONSHIP_MAP.SP_GET_LIST_ACCOUNT_DATA_1", relationEntites.ListParameter);
                DataTable tableResult = dc.DataTable;

                for (int i = 0; i < tableResult.Rows.Count; i++)
                {
                    RelationEntites.Investor temp = new RelationEntites.Investor();
                    PropertyInfo[] Props = temp.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo prop in Props)
                    {
                        prop.SetValue(temp, Convert.ChangeType(tableResult.Rows[i][prop.Name].ToString(), prop.PropertyType), null);
                    }

                    relationEntites.ListInvestor.Add(temp);
                }

                relationEntites.ListInvestor.Sort();
            });

            Task task2 = Task.Factory.StartNew(() =>
            {
                relationEntites.ListConnection.Clear();

                DataContainer dc = null;
                ctrlSA.ExecuteProcedureFillDataset(out dc, "PKG_RELATIONSHIP_MAP.SP_GET_LIST_RELATION_DATA_1", relationEntites.ListParameter);
                DataTable tableResult = dc.DataTable;

                for (int i = 0; i < tableResult.Rows.Count; i++)
                {
                    RelationEntites.Connection temp = new RelationEntites.Connection();
                    PropertyInfo[] Props = temp.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo prop in Props)
                    {
                        prop.SetValue(temp, Convert.ChangeType(tableResult.Rows[i][prop.Name].ToString(), prop.PropertyType), null);
                    }

                    relationEntites.ListConnection.Add(temp);
                }

                relationEntites.ListConnection.Sort();
            });

            Task.WaitAll(task1, task2);

            return;
        }

        public string SaveToDB(RelationEntites.RelationEntity relationEntites)
        {
            string Result = "";

            try
            {
                User userInfo = new User();
                ctrlSA.GetSessionUserInfo(out userInfo);

                DataContainer dc = null;
                List<string> param = new List<string>();
                param.Add(relationEntites.Id);
                param.Add(relationEntites.Relation_Name);
                param.Add(relationEntites.Relation_Parameter);
                param.Add(relationEntites.Relation_Description);
                param.Add(relationEntites.Relation_Layout);
                param.Add(userInfo.Username);
                param.Add(userInfo.Username);
                ctrlSA.ExecuteProcedureFillDataset(out dc, "PKG_RELATIONSHIP_MAP.SP_CREATE_NEW_MAP", param);
                DataTable tableResult = dc.DataTable;

                if (tableResult.Rows.Count != 1)
                {
                    Result = "fail";
                }

            }
            catch (Exception ex)
            {
                Result = ex.Message;
            }

            return Result;
        }

        public RelationEntites.RelationEntity Openfile(RelationEntites.RelationEntity relationEntites)
        {
            RelationEntites.RelationEntity temp = new RelationEntites.RelationEntity();
            DataContainer dc = null;
            List<string> param = new List<string>();
            param.Add(relationEntites.Relation_Name);
            ctrlSA.ExecuteProcedureFillDataset(out dc, "PKG_RELATIONSHIP_MAP.SP_OPEN_SAVED_MAP", param);
            DataTable tableResult = dc.DataTable;

            for (int i = 0; i < tableResult.Rows.Count; i++)
            {
                PropertyInfo[] Props = temp.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //if(prop.Name.Equals("Preview"))
                    //{
                    //    prop.SetValue(temp, File.WriteAllBytes(, Convert.FromBase64String(tableResult.Rows[i][prop.Name].ToString())));
                    //}
                    //else
                    prop.SetValue(temp, Convert.ChangeType(tableResult.Rows[i][prop.Name].ToString(), prop.PropertyType), null);
                }

            }
            return temp;
        }

        public void getListFileSaved(RelationEntites relationEntites)
        {
            relationEntites.ListRelationEntity.Clear();
            DataContainer dc = null;
            List<string> param = new List<string>();
            ctrlSA.ExecuteProcedureFillDataset(out dc, "PKG_RELATIONSHIP_MAP.SP_GET_LIST_SAVED_MAP", param);
            DataTable tableResult = dc.DataTable;

            for (int i = 0; i < tableResult.Rows.Count; i++)
            {
                RelationEntites.RelationEntity temp = new RelationEntites.RelationEntity();
                PropertyInfo[] Props = temp.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    prop.SetValue(temp, Convert.ChangeType(tableResult.Rows[i][prop.Name].ToString(), prop.PropertyType), null);
                }

                relationEntites.ListRelationEntity.Add(temp);
            }

            return;
        }

        public DataTable GetTypeDiagram()
        {
            DataTable tableResult = null;
            try
            {
                DataContainer dc = null;
                List<string> param = new List<string>();
                ctrlSA.ExecuteProcedureFillDatasetWithoutPram(out dc, "PKG_RELATIONSHIP_MAP.SP_GET_DIGRAM_TYPE");
                tableResult = dc.DataTable;
            }
            catch (Exception ex)
            {

            }
            return tableResult;
        }

        public string DeleteDiagram(string InputFileName)
        {
            //SP_DELETE_MAP
            string Result = "";
            try
            {
                User userInfo = new User();
                ctrlSA.GetSessionUserInfo(out userInfo);
                List<string> param = new List<string>();
                param.Add(InputFileName);
                ctrlSA.ExecuteStoreProcedure("PKG_RELATIONSHIP_MAP.SP_DELETE_MAP", param);
            }
            catch (Exception ex)
            {
                Result = ex.Message;
            }

            return Result;
        }
    }

    public partial class SaveForm : XtraForm
    {

        private System.ComponentModel.IContainer components = null;
        public RelationEntites relation = new RelationEntites();
        private ReturnFromDB rbd = new ReturnFromDB();
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.MemoEdit txtDescription;

        public SaveForm()
        {
            InitializeComponent();
            LookAndFeel.SkinName = UserLookAndFeel.Default.SkinName;
            LookAndFeel.UseDefaultLookAndFeel = false;
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Lưu sơ đồ quan hệ";
            if (relation != null)
            {
                DateTime localDate = DateTime.Now;
                txtName.Text = " - Relationship - " + localDate.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("Vi-VN"));
                txtDescription.Text = "Relation: " + relation.ListParam;
            }
        }

        public void ShowForm(System.Windows.Forms.Control owner, RelationEntites inputRelation)
        {
            relation = inputRelation;
            this.ShowDialog(owner);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (relation != null)
            {
                relation.relationEntity.Id = Guid.NewGuid().ToString();
                relation.relationEntity.Relation_Name = txtName.Text.Trim();
                relation.relationEntity.Relation_Description = txtDescription.Text.Trim();
                RelationEntites.RelationEntity returnRelationEntites = rbd.Openfile(relation.relationEntity);
                if (!string.IsNullOrEmpty(returnRelationEntites.Relation_Name))
                {
                    var dialogResult = XtraMessageBox.Show("File đã tồn tại trên hệ thống \nBạn có muốn ghi đè lên file cũ? ", "File đã tồn tại", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning);
                    if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        SaveRelationMaptoDB();
                    }
                }
                else
                {
                    SaveRelationMaptoDB();
                }
            }
        }

        private void SaveRelationMaptoDB()
        {
            var Result = rbd.SaveToDB(relation.relationEntity);
            if (!string.IsNullOrEmpty(Result))
            {
                frmInfo.ShowWarning("Lưu file thất bại", "không thể lưu file vào DB \n" + Result, this);
            }
            else
            {
                frmInfo.ShowInfo("Thông báo", "Lưu file thành công", this);
                this.Close();
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DevExpress.XtraLayout.LayoutControl layoutControl1;
            DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
            DevExpress.XtraEditors.LabelControl labelControl1;
            DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
            DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
            DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
            DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
            DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
            DevExpress.XtraEditors.SimpleButton btnCancle;
            DevExpress.XtraEditors.SimpleButton btnOK;
            DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
            DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
            DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
            DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
            DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
            DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;

            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            txtName = new DevExpress.XtraEditors.TextEdit();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            txtDescription = new DevExpress.XtraEditors.MemoEdit();
            btnOK = new DevExpress.XtraEditors.SimpleButton();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            btnCancle = new DevExpress.XtraEditors.SimpleButton();
            layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(layoutControl1)).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem5)).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(btnCancle);
            layoutControl1.Controls.Add(btnOK);
            layoutControl1.Controls.Add(labelControl1);
            layoutControl1.Controls.Add(txtName);
            layoutControl1.Controls.Add(txtDescription);
            layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            layoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            layoutControlItem4});
            layoutControl1.Location = new System.Drawing.Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(576, 115, 768, 499);
            layoutControl1.Root = layoutControlGroup1;
            layoutControl1.Size = new System.Drawing.Size(607, 394);
            layoutControl1.TabIndex = 0;
            layoutControl1.Text = "layoutControl1";
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            layoutControlItem1,
            emptySpaceItem1,
            layoutControlItem5,
            layoutControlItem2,
            layoutControlItem3,
            layoutControlItem7,
            emptySpaceItem2,
            emptySpaceItem3,
            emptySpaceItem4,
            emptySpaceItem5});
            layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup1.Name = "Root";
            layoutControlGroup1.Size = new System.Drawing.Size(607, 394);
            layoutControlGroup1.TextVisible = false;
            // 
            // txtName
            // 
            txtName.Location = new System.Drawing.Point(76, 115);
            txtName.Name = "txtName";
            txtName.Size = new System.Drawing.Size(519, 20);
            txtName.StyleController = layoutControl1;
            txtName.TabIndex = 4;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = txtName;
            layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            layoutControlItem1.Location = new System.Drawing.Point(0, 103);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(587, 24);
            layoutControlItem1.Text = "Tên biểu đồ:";
            layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            layoutControlItem1.TextSize = new System.Drawing.Size(60, 13);
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            layoutControlItem4.Location = new System.Drawing.Point(0, 113);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new System.Drawing.Size(503, 24);
            layoutControlItem4.Text = "layoutControlItem4";
            layoutControlItem4.TextLocation = DevExpress.Utils.Locations.Left;
            layoutControlItem4.TextSize = new System.Drawing.Size(93, 13);
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.AllowHotTrack = false;
            emptySpaceItem1.Location = new System.Drawing.Point(0, 39);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(587, 64);
            emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // labelControl1
            // 
            labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelControl1.Appearance.ForeColor = System.Drawing.SystemColors.Highlight;
            labelControl1.Location = new System.Drawing.Point(12, 12);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new System.Drawing.Size(271, 35);
            labelControl1.StyleController = layoutControl1;
            labelControl1.TabIndex = 8;
            labelControl1.Text = "Lưu sơ đồ quan hệ";
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = labelControl1;
            layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new System.Drawing.Size(587, 39);
            layoutControlItem5.Text = "Lưu sơ đồ quan hệ";
            layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = txtDescription;
            layoutControlItem2.Location = new System.Drawing.Point(0, 127);
            layoutControlItem2.MinSize = new System.Drawing.Size(50, 25);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(587, 199);
            layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem2.StartNewLine = true;
            layoutControlItem2.Text = "Mô tả";
            layoutControlItem2.TextSize = new System.Drawing.Size(60, 13);
            // 
            // txtDescription
            // 
            txtDescription.Location = new System.Drawing.Point(76, 139);
            txtDescription.Name = "txtDescription";
            txtDescription.Properties.MaxLength = 2000;
            txtDescription.Size = new System.Drawing.Size(519, 195);
            txtDescription.StyleController = layoutControl1;
            txtDescription.TabIndex = 5;
            // 
            // btnOK
            // 
            btnOK.Location = new System.Drawing.Point(147, 360);
            btnOK.Name = "btnOK";
            btnOK.Size = new System.Drawing.Size(116, 22);
            btnOK.StyleController = layoutControl1;
            btnOK.TabIndex = 9;
            btnOK.Text = "Lưu";
            btnOK.Click += new System.EventHandler(btnOK_Click);
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = btnOK;
            layoutControlItem3.Location = new System.Drawing.Point(135, 348);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(120, 26);
            layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem3.TextVisible = false;
            // 
            // btnCancle
            // 
            btnCancle.Location = new System.Drawing.Point(329, 360);
            btnCancle.Name = "btnCancle";
            btnCancle.Size = new System.Drawing.Size(123, 22);
            btnCancle.StyleController = layoutControl1;
            btnCancle.TabIndex = 11;
            btnCancle.Text = "Hủy";
            btnCancle.Click += new System.EventHandler(btnCancle_Click);
            // 
            // layoutControlItem7
            // 
            layoutControlItem7.Control = btnCancle;
            layoutControlItem7.Location = new System.Drawing.Point(317, 348);
            layoutControlItem7.Name = "layoutControlItem7";
            layoutControlItem7.Size = new System.Drawing.Size(127, 26);
            layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem7.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            emptySpaceItem2.AllowHotTrack = false;
            emptySpaceItem2.Location = new System.Drawing.Point(0, 348);
            emptySpaceItem2.Name = "emptySpaceItem2";
            emptySpaceItem2.Size = new System.Drawing.Size(135, 26);
            emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            emptySpaceItem3.AllowHotTrack = false;
            emptySpaceItem3.Location = new System.Drawing.Point(444, 348);
            emptySpaceItem3.Name = "emptySpaceItem3";
            emptySpaceItem3.Size = new System.Drawing.Size(143, 26);
            emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            emptySpaceItem4.AllowHotTrack = false;
            emptySpaceItem4.Location = new System.Drawing.Point(255, 348);
            emptySpaceItem4.Name = "emptySpaceItem4";
            emptySpaceItem4.Size = new System.Drawing.Size(62, 26);
            emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem5
            // 
            emptySpaceItem5.AllowHotTrack = false;
            emptySpaceItem5.Location = new System.Drawing.Point(0, 326);
            emptySpaceItem5.Name = "emptySpaceItem5";
            emptySpaceItem5.Size = new System.Drawing.Size(587, 22);
            emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // SaveForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(layoutControl1);
            Name = "SaveForm";
            Size = new System.Drawing.Size(607, 394);
            ((System.ComponentModel.ISupportInitialize)(layoutControl1)).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem5)).EndInit();
            ResumeLayout(false);
        }
    }

    public partial class ListMaps : XtraForm
    {
        private ReturnFromDB rbd = new ReturnFromDB();
        public ListMaps()
        {
            InitializeComponent();
            checkPrevie.Enabled = false;
            gridListControl.MainView = GridTableSetup(gridView1);
        }

        public void ShowForm(System.Windows.Forms.Control owner, DataTable inputSource)
        {
            gridListControl.DataSource = inputSource;
            this.Tag = owner;
            this.ShowDialog(owner);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var filename = ((DataRowView)(((GridView)gridView1).GetRow(((GridView)gridView1).GetSelectedRows()[0])))["Relation_Name"].ToString();
            OpenFile(filename);
        }

        private void OpenFile(string filename)
        {
            try
            {
                RelationEntites.RelationEntity returnRelationEntites = new RelationEntites.RelationEntity();
                returnRelationEntites.Relation_Name = filename;
                returnRelationEntites = rbd.Openfile(returnRelationEntites);
                ((frmRelaConect)this.Tag).LoadSavedData(returnRelationEntites);
                this.Close();
            }
            catch
            {
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var filename = ((DataRowView)(((GridView)gridView1).GetRow(((GridView)gridView1).GetSelectedRows()[0])))["Relation_Name"].ToString();
            var Create = ((DataRowView)(((GridView)gridView1).GetRow(((GridView)gridView1).GetSelectedRows()[0])))["Created_By"].ToString();
            var CreateTime = ((DataRowView)(((GridView)gridView1).GetRow(((GridView)gridView1).GetSelectedRows()[0])))["Created_Time"].ToString();
            var edit = ((DataRowView)(((GridView)gridView1).GetRow(((GridView)gridView1).GetSelectedRows()[0])))["Edited_By"].ToString();
            var editTime = ((DataRowView)(((GridView)gridView1).GetRow(((GridView)gridView1).GetSelectedRows()[0])))["Edited_Time"].ToString();

            var dialogResult = XtraMessageBox.Show("Bạn muốn xóa file? \nTên file: " + filename + "\nNgười tạo: " + Create + " Thời gian: " + CreateTime
                + "\nNgười sửa cuối: " + edit + " Thời gian: " + editTime, "Xóa file", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                var result = rbd.DeleteDiagram(filename);
                if (!string.IsNullOrEmpty(result))
                    frmInfo.ShowWarning("Thông báo", "Xóa file thất bại \n Lỗi: " + result, this);
            }
            RelationEntites relation = ((frmRelaConect)this.Tag).relation;
            rbd.getListFileSaved(relation);
            gridListControl.DataSource = ToDataTable(relation.ListRelationEntity);
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            var filename = ((GridView)sender).GetRowCellValue(((GridView)sender).FocusedRowHandle, "Relation_Name").ToString();
            OpenFile(filename);
        }

        private void checkPrevie_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private BaseView GridTableSetup(GridView inputGridView)
        {
            inputGridView.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
            inputGridView.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
            inputGridView.OptionsBehavior.Editable = false;
            GridAddColumn(inputGridView, "Tên sơ đồ", "Relation_Name", DevExpress.Utils.HorzAlignment.Center);
            //GridAddColumn(inputGridView, "Tham số", "Relation_Parameter", DevExpress.Utils.HorzAlignment.Center);
            GridAddColumn(inputGridView, "Mô tả", "Relation_Description", DevExpress.Utils.HorzAlignment.Near);
            GridAddColumn(inputGridView, "Người tạo", "Created_By", DevExpress.Utils.HorzAlignment.Center);
            GridAddColumn(inputGridView, "Giờ tạo", "Created_Time", DevExpress.Utils.HorzAlignment.Center);
            GridAddColumn(inputGridView, "Ngưới sửa cuối", "Edited_By", DevExpress.Utils.HorzAlignment.Center);
            GridAddColumn(inputGridView, "Lần sửa cuối", "Edited_Time", DevExpress.Utils.HorzAlignment.Center);
            //GridAddImageColumn(inputGridView, "Xem trước", "Preview", DevExpress.Utils.HorzAlignment.Center);
            return inputGridView;
        }

        private void GridAddColumn(GridView inputGridView, string inputName, string inputFieldName, HorzAlignment hAlignment)
        {
            DevExpress.XtraGrid.Columns.GridColumn field = new DevExpress.XtraGrid.Columns.GridColumn();
            field.Name = inputName;
            field.Caption = inputName;
            field.FieldName = inputFieldName;
            field.Visible = true;
            field.AppearanceHeader.Options.UseTextOptions = true;
            field.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            field.AppearanceCell.Options.UseTextOptions = true;
            field.AppearanceCell.TextOptions.HAlignment = hAlignment;
            inputGridView.Columns.Add(field);
        }

        private void GridAddImageColumn(GridView inputGridView, string inputName, string inputFieldName, HorzAlignment hAlignment)
        {
            DevExpress.XtraGrid.Columns.GridColumn field = new DevExpress.XtraGrid.Columns.GridColumn();
            field.Name = inputName;
            field.Caption = inputName;
            field.FieldName = inputFieldName;
            field.Visible = false;
            field.AppearanceHeader.Options.UseTextOptions = true;
            field.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            field.AppearanceCell.Options.UseTextOptions = true;
            field.AppearanceCell.TextOptions.HAlignment = hAlignment;
            field.ColumnEdit = new RepositoryItemPictureEdit();
            inputGridView.Columns.Add(field);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            checkPrevie = new DevExpress.XtraEditors.CheckEdit();
            btnCancle = new DevExpress.XtraEditors.SimpleButton();
            btnOk = new DevExpress.XtraEditors.SimpleButton();
            gridListControl = new DevExpress.XtraGrid.GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            btnDelete = new DevExpress.XtraEditors.SimpleButton();
            layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem9 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(layoutControl1)).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(checkPrevie.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(gridListControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem9)).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(btnDelete);
            layoutControl1.Controls.Add(checkPrevie);
            layoutControl1.Controls.Add(btnCancle);
            layoutControl1.Controls.Add(btnOk);
            layoutControl1.Controls.Add(gridListControl);
            layoutControl1.Controls.Add(labelControl1);
            layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            layoutControl1.Location = new System.Drawing.Point(0, 0);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1238, 457, 250, 350);
            layoutControl1.Root = layoutControlGroup1;
            layoutControl1.Size = new System.Drawing.Size(929, 522);
            layoutControl1.TabIndex = 0;
            layoutControl1.Text = "layoutControl1";
            // 
            // checkPrevie
            // 
            checkPrevie.Location = new System.Drawing.Point(77, 61);
            checkPrevie.Name = "checkPrevie";
            checkPrevie.Properties.Caption = "Xem trước sơ đồ";
            checkPrevie.Size = new System.Drawing.Size(385, 19);
            checkPrevie.StyleController = layoutControl1;
            checkPrevie.TabIndex = 8;
            checkPrevie.CheckStateChanged += new System.EventHandler(checkPrevie_CheckStateChanged);
            // 
            // btnCancle
            // 
            btnCancle.Location = new System.Drawing.Point(791, 488);
            btnCancle.Name = "btnCancle";
            btnCancle.Size = new System.Drawing.Size(94, 22);
            btnCancle.StyleController = layoutControl1;
            btnCancle.TabIndex = 7;
            btnCancle.Text = "Hủy";
            btnCancle.Click += new System.EventHandler(btnCancle_Click);
            // 
            // btnOk
            // 
            btnOk.Location = new System.Drawing.Point(663, 488);
            btnOk.Name = "btnOk";
            btnOk.Size = new System.Drawing.Size(93, 22);
            btnOk.StyleController = layoutControl1;
            btnOk.TabIndex = 6;
            btnOk.Text = "Mở";
            btnOk.Click += new System.EventHandler(btnOk_Click);
            // 
            // gridListControl
            // 
            gridListControl.Location = new System.Drawing.Point(12, 94);
            gridListControl.MainView = gridView1;
            gridListControl.Name = "gridListControl";
            gridListControl.Size = new System.Drawing.Size(905, 345);
            gridListControl.TabIndex = 5;
            gridListControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            gridView1});
            // 
            // gridView1
            // 
            gridView1.GridControl = gridListControl;
            gridView1.Name = "gridView1";
            gridView1.DoubleClick += new System.EventHandler(gridView1_DoubleClick);
            // 
            // labelControl1
            // 
            labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelControl1.Appearance.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            labelControl1.Location = new System.Drawing.Point(12, 12);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new System.Drawing.Size(336, 33);
            labelControl1.StyleController = layoutControl1;
            labelControl1.TabIndex = 4;
            labelControl1.Text = "Danh sách sơ đồ quan hệ";
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            layoutControlItem1,
            emptySpaceItem1,
            layoutControlItem2,
            layoutControlItem3,
            layoutControlItem4,
            emptySpaceItem2,
            emptySpaceItem3,
            emptySpaceItem4,
            layoutControlItem5,
            emptySpaceItem6,
            emptySpaceItem5,
            emptySpaceItem7,
            emptySpaceItem8,
            layoutControlItem6,
            emptySpaceItem9});
            layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            layoutControlGroup1.Name = "Root";
            layoutControlGroup1.Size = new System.Drawing.Size(929, 522);
            layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = labelControl1;
            layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new System.Drawing.Size(909, 37);
            layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.AllowHotTrack = false;
            emptySpaceItem1.Location = new System.Drawing.Point(454, 49);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new System.Drawing.Size(455, 23);
            emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = gridListControl;
            layoutControlItem2.Location = new System.Drawing.Point(0, 82);
            layoutControlItem2.MaxSize = new System.Drawing.Size(909, 0);
            layoutControlItem2.MinSize = new System.Drawing.Size(909, 24);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new System.Drawing.Size(909, 349);
            layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = btnOk;
            layoutControlItem3.Location = new System.Drawing.Point(651, 476);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new System.Drawing.Size(97, 26);
            layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = btnCancle;
            layoutControlItem4.Location = new System.Drawing.Point(779, 476);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new System.Drawing.Size(98, 26);
            layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            emptySpaceItem2.AllowHotTrack = false;
            emptySpaceItem2.Location = new System.Drawing.Point(0, 476);
            emptySpaceItem2.Name = "emptySpaceItem2";
            emptySpaceItem2.Size = new System.Drawing.Size(520, 26);
            emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            emptySpaceItem3.AllowHotTrack = false;
            emptySpaceItem3.Location = new System.Drawing.Point(877, 476);
            emptySpaceItem3.Name = "emptySpaceItem3";
            emptySpaceItem3.Size = new System.Drawing.Size(32, 26);
            emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            emptySpaceItem4.AllowHotTrack = false;
            emptySpaceItem4.Location = new System.Drawing.Point(748, 476);
            emptySpaceItem4.Name = "emptySpaceItem4";
            emptySpaceItem4.Size = new System.Drawing.Size(31, 26);
            emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = checkPrevie;
            layoutControlItem5.Location = new System.Drawing.Point(65, 49);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new System.Drawing.Size(389, 23);
            layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem5.TextVisible = false;
            // 
            // emptySpaceItem6
            // 
            emptySpaceItem6.AllowHotTrack = false;
            emptySpaceItem6.Location = new System.Drawing.Point(0, 431);
            emptySpaceItem6.MinSize = new System.Drawing.Size(104, 24);
            emptySpaceItem6.Name = "emptySpaceItem6";
            emptySpaceItem6.Size = new System.Drawing.Size(909, 45);
            emptySpaceItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem5
            // 
            emptySpaceItem5.AllowHotTrack = false;
            emptySpaceItem5.Location = new System.Drawing.Point(0, 72);
            emptySpaceItem5.Name = "emptySpaceItem5";
            emptySpaceItem5.Size = new System.Drawing.Size(909, 10);
            emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem7
            // 
            emptySpaceItem7.AllowHotTrack = false;
            emptySpaceItem7.Location = new System.Drawing.Point(0, 37);
            emptySpaceItem7.Name = "emptySpaceItem7";
            emptySpaceItem7.Size = new System.Drawing.Size(909, 12);
            emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem8
            // 
            emptySpaceItem8.AllowHotTrack = false;
            emptySpaceItem8.Location = new System.Drawing.Point(0, 49);
            emptySpaceItem8.Name = "emptySpaceItem8";
            emptySpaceItem8.Size = new System.Drawing.Size(65, 23);
            emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
            // 
            // btnDelete
            // 
            btnDelete.Location = new System.Drawing.Point(532, 488);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(81, 22);
            btnDelete.StyleController = layoutControl1;
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Xóa";
            btnDelete.Click += new System.EventHandler(btnDelete_Click);
            // 
            // layoutControlItem6
            // 
            layoutControlItem6.Control = btnDelete;
            layoutControlItem6.Location = new System.Drawing.Point(520, 476);
            layoutControlItem6.Name = "layoutControlItem6";
            layoutControlItem6.Size = new System.Drawing.Size(85, 26);
            layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem9
            // 
            emptySpaceItem9.AllowHotTrack = false;
            emptySpaceItem9.Location = new System.Drawing.Point(605, 476);
            emptySpaceItem9.Name = "emptySpaceItem9";
            emptySpaceItem9.Size = new System.Drawing.Size(46, 26);
            emptySpaceItem9.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ListMaps
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(layoutControl1);
            Name = "ListMaps";
            Size = new System.Drawing.Size(945, 522);
            ((System.ComponentModel.ISupportInitialize)(layoutControl1)).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(checkPrevie.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(gridListControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(emptySpaceItem9)).EndInit();
            ResumeLayout(false);

        }


        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnCancle;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraGrid.GridControl gridListControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraEditors.CheckEdit checkPrevie;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem9;

        #endregion
    }

    public static class BinarySearchUtils
    {
        public const string FIRST = "First";

        public const string LAST = "Last";

        public static int BinarySearchIndexOf<TItem>(this IList<TItem> list, TItem targetValue, IComparer<TItem> comparer = null)
        {
            Func<TItem, TItem, int> compareFunc = comparer != null ? comparer.Compare : (Func<TItem, TItem, int>)Comparer<TItem>.Default.Compare;
            int index = BinarySearchIndexOfBy(list, compareFunc, targetValue);
            return index;
        }

        public static int BinarySearchIndexOfBy<TItem, TValue>(this IList<TItem> list, Func<TItem, TValue, int> comparer, TValue value)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (comparer == null)
                throw new ArgumentNullException("comparer");

            if (list.Count == 0)
                return -2;

            // Implementation below copied largely from .NET4 ArraySortHelper.InternalBinarySearch()
            int lo = 0;
            int hi = list.Count - 1;
            while (lo <= hi)
            {
                int i = lo + ((hi - lo) >> 1);
                int order = comparer(list[i], value);

                if (order == 0)
                    return i;
                if (order < 0)
                {
                    lo = i + 1;
                }
                else
                {
                    hi = i - 1;
                }
            }

            return -1;
        }

        public static string BinarySearchNearByIndexOf<TItem>(this IList<TItem> list, TItem targetValue, IComparer<TItem> comparer = null)
        {
            Func<TItem, TItem, int> compareFunc = comparer != null ? comparer.Compare : (Func<TItem, TItem, int>)Comparer<TItem>.Default.Compare;
            string index = BinarySearchNearByIndexOfBy(list, compareFunc, targetValue);
            return index;
        }

        public static string BinarySearchNearByIndexOfBy<TItem, TValue>(this IList<TItem> list, Func<TItem, TValue, int> comparer, TValue value)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (comparer == null)
                throw new ArgumentNullException("comparer");

            if (list.Count == 0)
                return (-2).ToString();

            int lo = 0;
            int hi = list.Count - 1;
            int listCount = list.Count - 1;
            int order = -1;
            while (lo <= hi)
            {
                int i = lo + ((hi - lo) >> 1);
                order = comparer(list[i], value);

                if (order == 0)
                    return i.ToString();
                if (order < 0)
                {
                    lo = i + 1;
                    //Console.WriteLine(" order < 0 lo: " + lo + " hi: " + hi + " i: " + i);
                    if (listCount < i + 1) return (list.Count - 1).ToString();
                    if (comparer(list[i + 1], value) > 0) return (i).ToString() + "," + (i + 1).ToString();
                }
                else
                {
                    hi = i - 1;
                    //Console.WriteLine(" order > 0 lo: " + lo + " hi: " + hi + " i: " + i);
                    if (i - 1 < 0) return FIRST;
                    if (comparer(list[i - 1], value) < 0) return (i - 1).ToString() + "," + (i).ToString();
                }
            }

            return (-1).ToString();
        }

        public static string BinarySearchNearByIndexOfLast<TItem>(this IList<TItem> list, TItem targetValue, IComparer<TItem> comparer = null)
        {
            Func<TItem, TItem, int> compareFunc = comparer != null ? comparer.Compare : (Func<TItem, TItem, int>)Comparer<TItem>.Default.Compare;
            string index = BinarySearchNearByIndexOfByLast(list, compareFunc, targetValue);
            return index;
        }

        public static string BinarySearchNearByIndexOfByLast<TItem, TValue>(this IList<TItem> list, Func<TItem, TValue, int> comparer, TValue value)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (comparer == null)
                throw new ArgumentNullException("comparer");

            if (list.Count == 0)
                return (-2).ToString();

            int lo = 0;
            int hi = list.Count - 1;
            int listCount = list.Count - 1;
            int order = -1;
            while (lo <= hi)
            {
                int i = lo + ((hi - lo) >> 1);
                order = comparer(list[i], value);

                if (order == 0)
                    return i.ToString();
                if (order < 0)
                {
                    lo = i + 1;
                    //Console.WriteLine(" order < 0 lo: " + lo + " hi: " + hi + " i: " + i);
                    if (listCount < i + 1) return (list.Count).ToString();
                    if (comparer(list[i + 1], value) > 0) return (i).ToString() + "," + (i + 1).ToString();
                }
                else
                {
                    hi = i - 1;
                    //Console.WriteLine(" order > 0 lo: " + lo + " hi: " + hi + " i: " + i);
                    if (i - 1 < 0) return (0).ToString();
                    if (comparer(list[i - 1], value) < 0) return (i - 1).ToString() + "," + (i).ToString();
                }
            }

            return (-1).ToString();
        }

        public static bool DictionaryEquals<TKey, TValue>(this Dictionary<TKey, TValue> left, Dictionary<TKey, TValue> right)
        {
            var comp = EqualityComparer<TValue>.Default;
            if (left.Count != right.Count)
            {
                return false;
            }
            foreach (var pair in left)
            {
                TValue value;
                if (!right.TryGetValue(pair.Key, out value)
                     || !comp.Equals(pair.Value, value))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
