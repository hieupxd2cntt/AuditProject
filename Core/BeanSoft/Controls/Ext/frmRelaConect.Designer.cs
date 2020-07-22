﻿using Core.Entities;

namespace AppClient.Controls.Ext.frmRelaConect
{
    partial class frmRelaConect
    {
        class ChartManagerLanguage : ModuleLanguage
        {
            public string Layout { get; set; }
            public ChartManagerLanguage(ModuleInfo moduleInfo)
                : base(moduleInfo)
            {
            }
        }

        private new ChartManagerLanguage Language
        {
            get
            {
                return (ChartManagerLanguage)base.Language;
            }
        }

        public override void InitializeLanguage()
        {
            base.Language = new ChartManagerLanguage(ModuleInfo);
            Language.Layout = Language.GetLayout(null);
            base.InitializeLanguage();
        }

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRelaConect));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip5 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem5 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip6 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem6 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip7 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem7 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip8 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem8 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip9 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem9 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip10 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem10 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip11 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem11 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip12 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem12 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip13 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem13 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip14 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem14 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup1 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup2 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup3 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup4 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup5 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup6 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup7 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup8 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup9 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup10 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup11 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup12 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup13 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup14 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup15 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup16 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup17 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup18 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup19 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup20 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup21 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup22 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup23 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup24 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup25 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup26 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup27 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup28 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup29 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup30 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup31 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup32 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup33 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup34 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup35 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup36 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.Utils.SuperToolTip superToolTip15 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem15 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip16 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem16 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip17 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem17 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip18 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem18 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip19 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem19 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip20 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem20 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip21 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem21 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip22 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem22 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.SuperToolTip superToolTip23 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem23 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup37 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup38 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            this.diagramControl1 = new DevExpress.XtraDiagram.DiagramControl();
            this.propertyGridControl1 = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.applicationMenu1 = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.diagramCommandNewFileBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandNewFileBarButtonItem();
            this.diagramCommandOpenFileBarButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.diagramCommandSaveFileBarButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.diagramCommandPrintMenuBarSplitButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandPrintMenuBarSplitButtonItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.diagramCommandPrintBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandPrintBarButtonItem();
            this.diagramCommandQuickPrintBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandQuickPrintBarButtonItem();
            this.diagramCommandExportAsBarSplitButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandExportAsBarSplitButtonItem();
            this.popupMenu2 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.diagramCommandExportDiagram_PNGBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandExportDiagram_PNGBarButtonItem();
            this.diagramCommandExportDiagram_JPEGBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandExportDiagram_JPEGBarButtonItem();
            this.diagramCommandExportDiagram_BMPBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandExportDiagram_BMPBarButtonItem();
            this.diagramCommandExportDiagram_GIFBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandExportDiagram_GIFBarButtonItem();
            this.diagramCommandUndoBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandUndoBarButtonItem();
            this.diagramCommandRedoBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandRedoBarButtonItem();
            this.diagramCommandPageOrientationBarDropDownItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandPageOrientationBarDropDownItem();
            this.diagramCommandPageOrientation_HorizontalBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandPageOrientation_HorizontalBarCheckItem();
            this.diagramCommandPageOrientation_VerticalBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandPageOrientation_VerticalBarCheckItem();
            this.diagramCommandPageSizeBarDropDownItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandPageSizeBarDropDownItem();
            this.diagramCommandPageSize_LetterBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_LetterBarCheckItem();
            this.diagramCommandPageSize_TabloidBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_TabloidBarCheckItem();
            this.diagramCommandPageSize_LegalBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_LegalBarCheckItem();
            this.diagramCommandPageSize_StatementBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_StatementBarCheckItem();
            this.diagramCommandPageSize_ExecutiveBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_ExecutiveBarCheckItem();
            this.diagramCommandPageSize_A3BarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_A3BarCheckItem();
            this.diagramCommandPageSize_A4BarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_A4BarCheckItem();
            this.diagramCommandPageSize_A5BarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_A5BarCheckItem();
            this.diagramCommandPageSize_B4BarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_B4BarCheckItem();
            this.diagramCommandPageSize_B5BarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_B5BarCheckItem();
            this.diagramCommandFitToDrawingBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandFitToDrawingBarButtonItem();
            this.diagramCommandSetPageParameters_PageSizeBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSetPageParameters_PageSizeBarButtonItem();
            this.diagramCommandAutoSizeBarDropDownItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandAutoSizeBarDropDownItem();
            this.diagramCommandAutoSize_NoneBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandAutoSize_NoneBarCheckItem();
            this.diagramCommandAutoSize_AutoSizeBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandAutoSize_AutoSizeBarCheckItem();
            this.diagramCommandAutoSize_FillBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandAutoSize_FillBarCheckItem();
            this.diagramCommandThemesBarGalleryItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandThemesBarGalleryItem();
            this.diagramCommandSnapToItemsBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSnapToItemsBarCheckItem();
            this.diagramCommandSnapToGridBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSnapToGridBarCheckItem();
            this.diagramCommandReLayoutBarDropDownItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandReLayoutBarDropDownItem();
            this.diagramReLayoutTreeBarHeaderItem1 = new DevExpress.XtraDiagram.Bars.DiagramReLayoutTreeBarHeaderItem();
            this.diagramCommandTreeLayout_DownBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandTreeLayout_DownBarButtonItem();
            this.diagramCommandTreeLayout_UpBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandTreeLayout_UpBarButtonItem();
            this.diagramCommandTreeLayout_RightBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandTreeLayout_RightBarButtonItem();
            this.diagramCommandTreeLayout_LeftBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandTreeLayout_LeftBarButtonItem();
            this.diagramReLayoutSugiyamaBarHeaderItem1 = new DevExpress.XtraDiagram.Bars.DiagramReLayoutSugiyamaBarHeaderItem();
            this.diagramCommandSugiyamaLayout_DownBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSugiyamaLayout_DownBarButtonItem();
            this.diagramCommandSugiyamaLayout_UpBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSugiyamaLayout_UpBarButtonItem();
            this.diagramCommandSugiyamaLayout_RightBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSugiyamaLayout_RightBarButtonItem();
            this.diagramCommandSugiyamaLayout_LeftBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSugiyamaLayout_LeftBarButtonItem();
            this.diagramCommandShowRulersBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandShowRulersBarCheckItem();
            this.diagramCommandShowGridBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandShowGridBarCheckItem();
            this.diagramCommandShowPageBreaksBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandShowPageBreaksBarCheckItem();
            this.diagramCommandFitToPageBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandFitToPageBarButtonItem();
            this.diagramCommandFitToWidthBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandFitToWidthBarButtonItem();
            this.diagramCommandPasteBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandPasteBarButtonItem();
            this.diagramCommandCutBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandCutBarButtonItem();
            this.diagramCommandCopyBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandCopyBarButtonItem();
            this.barButtonGroup1 = new DevExpress.XtraBars.BarButtonGroup();
            this.diagramCommandFontFamilyBarEditItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandFontFamilyBarEditItem();
            this.repositoryItemFontEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemFontEdit();
            this.diagramCommandFontSizeBarEditItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandFontSizeBarEditItem();
            this.repositoryItemDiagramFontSizeEdit1 = new DevExpress.XtraDiagram.Bars.RepositoryItemDiagramFontSizeEdit();
            this.diagramCommandIncreaseFontSizeBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandIncreaseFontSizeBarButtonItem();
            this.diagramCommandDecreaseFontSizeBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandDecreaseFontSizeBarButtonItem();
            this.barButtonGroup2 = new DevExpress.XtraBars.BarButtonGroup();
            this.diagramCommandToggleFontBoldBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandToggleFontBoldBarCheckItem();
            this.diagramCommandToggleFontItalicBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandToggleFontItalicBarCheckItem();
            this.diagramCommandToggleFontUnderlineBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandToggleFontUnderlineBarCheckItem();
            this.diagramCommandToggleFontStrikethroughBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandToggleFontStrikethroughBarCheckItem();
            this.diagramCommandForegroundColorBarSplitButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandForegroundColorBarSplitButtonItem();
            this.barButtonGroup3 = new DevExpress.XtraBars.BarButtonGroup();
            this.diagramCommandSetVerticalAlignment_TopBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSetVerticalAlignment_TopBarCheckItem();
            this.diagramCommandSetVerticalAlignment_CenterBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSetVerticalAlignment_CenterBarCheckItem();
            this.diagramCommandSetVerticalAlignment_BottomBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSetVerticalAlignment_BottomBarCheckItem();
            this.barButtonGroup4 = new DevExpress.XtraBars.BarButtonGroup();
            this.diagramCommandSetHorizontalAlignment_LeftBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSetHorizontalAlignment_LeftBarCheckItem();
            this.diagramCommandSetHorizontalAlignment_CenterBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSetHorizontalAlignment_CenterBarCheckItem();
            this.diagramCommandSetHorizontalAlignment_RightBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSetHorizontalAlignment_RightBarCheckItem();
            this.diagramCommandSetHorizontalAlignment_JustifyBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSetHorizontalAlignment_JustifyBarCheckItem();
            this.diagramCommandSelectTool_PointerToolBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSelectTool_PointerToolBarCheckItem();
            this.diagramCommandSelectTool_ConnectorToolBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSelectTool_ConnectorToolBarCheckItem();
            this.diagramCommandToolsContainerCheckDropDownItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandToolsContainerCheckDropDownItem();
            this.popupMenu3 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.diagramCommandSelectTool_RectangleToolBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSelectTool_RectangleToolBarCheckItem();
            this.diagramCommandSelectTool_EllipseToolBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSelectTool_EllipseToolBarCheckItem();
            this.diagramCommandSelectTool_RightTriangleToolBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSelectTool_RightTriangleToolBarCheckItem();
            this.diagramCommandSelectTool_HexagonToolBarCheckItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSelectTool_HexagonToolBarCheckItem();
            this.diagramCommandShapeStylesBarGalleryItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandShapeStylesBarGalleryItem();
            this.diagramCommandBackgroundColorBarSplitButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandBackgroundColorBarSplitButtonItem();
            this.diagramCommandStrokeColorBarSplitButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandStrokeColorBarSplitButtonItem();
            this.diagramCommandBringToFrontBarSplitButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandBringToFrontBarSplitButtonItem();
            this.popupMenu4 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.diagramCommandBringForwardBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandBringForwardBarButtonItem();
            this.diagramCommandBringToFrontBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandBringToFrontBarButtonItem();
            this.diagramCommandSendToBackBarSplitButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSendToBackBarSplitButtonItem();
            this.popupMenu5 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.diagramCommandSendBackwardBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSendBackwardBarButtonItem();
            this.diagramCommandSendToBackBarButtonItem1 = new DevExpress.XtraDiagram.Bars.DiagramCommandSendToBackBarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.diagramHomeRibbonPage1 = new DevExpress.XtraDiagram.Bars.DiagramHomeRibbonPage();
            this.diagramClipboardRibbonPageGroup1 = new DevExpress.XtraDiagram.Bars.DiagramClipboardRibbonPageGroup();
            this.diagramFontRibbonPageGroup1 = new DevExpress.XtraDiagram.Bars.DiagramFontRibbonPageGroup();
            this.diagramParagraphRibbonPageGroup1 = new DevExpress.XtraDiagram.Bars.DiagramParagraphRibbonPageGroup();
            this.diagramToolsRibbonPageGroup1 = new DevExpress.XtraDiagram.Bars.DiagramToolsRibbonPageGroup();
            this.diagramShapeStylesRibbonPageGroup1 = new DevExpress.XtraDiagram.Bars.DiagramShapeStylesRibbonPageGroup();
            this.diagramArrangeRibbonPageGroup1 = new DevExpress.XtraDiagram.Bars.DiagramArrangeRibbonPageGroup();
            this.diagramViewRibbonPage1 = new DevExpress.XtraDiagram.Bars.DiagramViewRibbonPage();
            this.diagramShowRibbonPageGroup1 = new DevExpress.XtraDiagram.Bars.DiagramShowRibbonPageGroup();
            this.diagramZoomRibbonPageGroup1 = new DevExpress.XtraDiagram.Bars.DiagramZoomRibbonPageGroup();
            this.diagramDesignRibbonPage1 = new DevExpress.XtraDiagram.Bars.DiagramDesignRibbonPage();
            this.diagramPageSetupRibbonPageGroup1 = new DevExpress.XtraDiagram.Bars.DiagramPageSetupRibbonPageGroup();
            this.diagramThemesRibbonPageGroup1 = new DevExpress.XtraDiagram.Bars.DiagramThemesRibbonPageGroup();
            this.diagramOptionsRibbonPageGroup1 = new DevExpress.XtraDiagram.Bars.DiagramOptionsRibbonPageGroup();
            this.diagramTreeLayoutRibbonPageGroup1 = new DevExpress.XtraDiagram.Bars.DiagramTreeLayoutRibbonPageGroup();
            this.diagramBarController1 = new DevExpress.XtraDiagram.Bars.DiagramBarController();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnLoadData = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.ParamLayout = new DevExpress.XtraLayout.LayoutControl();
            this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.dockPanel2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.popupMenu6 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDiagramFontSizeEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramBarController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.panelContainer1.SuspendLayout();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParamLayout)).BeginInit();
            this.ParamLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            this.dockPanel2.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu6)).BeginInit();
            this.SuspendLayout();
            // 
            // diagramControl1
            // 
            this.diagramControl1.Appearance.Connector.ContentBackground = System.Drawing.Color.Empty;
            this.diagramControl1.Appearance.HRuler.ContentBackground = System.Drawing.Color.Empty;
            this.diagramControl1.Appearance.Shape.ContentBackground = System.Drawing.Color.Empty;
            this.diagramControl1.Appearance.VRuler.ContentBackground = System.Drawing.Color.Empty;
            this.diagramControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diagramControl1.Location = new System.Drawing.Point(0, 141);
            this.diagramControl1.Name = "diagramControl1";
            this.diagramControl1.PropertyGrid = this.propertyGridControl1;
            this.diagramControl1.SelectedStencils = new DevExpress.Diagram.Core.StencilCollection(new string[] {
            "BasicShapes",
            "BasicFlowchartShapes"});
            this.diagramControl1.Size = new System.Drawing.Size(1068, 655);
            this.diagramControl1.TabIndex = 0;
            this.diagramControl1.Text = "diagramControl1";
            // 
            // propertyGridControl1
            // 
            this.propertyGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridControl1.Location = new System.Drawing.Point(0, 0);
            this.propertyGridControl1.Name = "propertyGridControl1";
            this.propertyGridControl1.Size = new System.Drawing.Size(310, 324);
            this.propertyGridControl1.TabIndex = 0;
            this.propertyGridControl1.CustomDrawRowValueCell += new DevExpress.XtraVerticalGrid.Events.CustomDrawRowValueCellEventHandler(this.propertyGridControl1_CustomDrawRowValueCell);
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ApplicationButtonDropDownControl = this.applicationMenu1;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.diagramCommandNewFileBarButtonItem1,
            this.diagramCommandOpenFileBarButtonItem1,
            this.diagramCommandSaveFileBarButtonItem1,
            this.diagramCommandPrintMenuBarSplitButtonItem1,
            this.diagramCommandPrintBarButtonItem1,
            this.diagramCommandQuickPrintBarButtonItem1,
            this.diagramCommandExportAsBarSplitButtonItem1,
            this.diagramCommandExportDiagram_PNGBarButtonItem1,
            this.diagramCommandExportDiagram_JPEGBarButtonItem1,
            this.diagramCommandExportDiagram_BMPBarButtonItem1,
            this.diagramCommandExportDiagram_GIFBarButtonItem1,
            this.diagramCommandUndoBarButtonItem1,
            this.diagramCommandRedoBarButtonItem1,
            this.diagramCommandPageOrientationBarDropDownItem1,
            this.diagramCommandPageSizeBarDropDownItem1,
            this.diagramCommandAutoSizeBarDropDownItem1,
            this.diagramCommandPageOrientation_HorizontalBarCheckItem1,
            this.diagramCommandPageOrientation_VerticalBarCheckItem1,
            this.diagramCommandPageSize_LetterBarCheckItem1,
            this.diagramCommandPageSize_TabloidBarCheckItem1,
            this.diagramCommandPageSize_LegalBarCheckItem1,
            this.diagramCommandPageSize_StatementBarCheckItem1,
            this.diagramCommandPageSize_ExecutiveBarCheckItem1,
            this.diagramCommandPageSize_A3BarCheckItem1,
            this.diagramCommandPageSize_A4BarCheckItem1,
            this.diagramCommandPageSize_A5BarCheckItem1,
            this.diagramCommandPageSize_B4BarCheckItem1,
            this.diagramCommandPageSize_B5BarCheckItem1,
            this.diagramCommandFitToDrawingBarButtonItem1,
            this.diagramCommandSetPageParameters_PageSizeBarButtonItem1,
            this.diagramCommandAutoSize_NoneBarCheckItem1,
            this.diagramCommandAutoSize_AutoSizeBarCheckItem1,
            this.diagramCommandAutoSize_FillBarCheckItem1,
            this.diagramCommandThemesBarGalleryItem1,
            this.diagramCommandSnapToItemsBarCheckItem1,
            this.diagramCommandSnapToGridBarCheckItem1,
            this.diagramCommandReLayoutBarDropDownItem1,
            this.diagramReLayoutTreeBarHeaderItem1,
            this.diagramCommandTreeLayout_DownBarButtonItem1,
            this.diagramCommandTreeLayout_UpBarButtonItem1,
            this.diagramCommandTreeLayout_RightBarButtonItem1,
            this.diagramCommandTreeLayout_LeftBarButtonItem1,
            this.diagramReLayoutSugiyamaBarHeaderItem1,
            this.diagramCommandSugiyamaLayout_DownBarButtonItem1,
            this.diagramCommandSugiyamaLayout_UpBarButtonItem1,
            this.diagramCommandSugiyamaLayout_RightBarButtonItem1,
            this.diagramCommandSugiyamaLayout_LeftBarButtonItem1,
            this.diagramCommandShowRulersBarCheckItem1,
            this.diagramCommandShowGridBarCheckItem1,
            this.diagramCommandShowPageBreaksBarCheckItem1,
            this.diagramCommandFitToPageBarButtonItem1,
            this.diagramCommandFitToWidthBarButtonItem1,
            this.diagramCommandPasteBarButtonItem1,
            this.diagramCommandCutBarButtonItem1,
            this.diagramCommandCopyBarButtonItem1,
            this.barButtonGroup1,
            this.diagramCommandFontFamilyBarEditItem1,
            this.diagramCommandFontSizeBarEditItem1,
            this.diagramCommandIncreaseFontSizeBarButtonItem1,
            this.diagramCommandDecreaseFontSizeBarButtonItem1,
            this.barButtonGroup2,
            this.diagramCommandToggleFontBoldBarCheckItem1,
            this.diagramCommandToggleFontItalicBarCheckItem1,
            this.diagramCommandToggleFontUnderlineBarCheckItem1,
            this.diagramCommandToggleFontStrikethroughBarCheckItem1,
            this.diagramCommandForegroundColorBarSplitButtonItem1,
            this.barButtonGroup3,
            this.diagramCommandSetVerticalAlignment_TopBarCheckItem1,
            this.diagramCommandSetVerticalAlignment_CenterBarCheckItem1,
            this.diagramCommandSetVerticalAlignment_BottomBarCheckItem1,
            this.barButtonGroup4,
            this.diagramCommandSetHorizontalAlignment_LeftBarCheckItem1,
            this.diagramCommandSetHorizontalAlignment_CenterBarCheckItem1,
            this.diagramCommandSetHorizontalAlignment_RightBarCheckItem1,
            this.diagramCommandSetHorizontalAlignment_JustifyBarCheckItem1,
            this.diagramCommandSelectTool_PointerToolBarCheckItem1,
            this.diagramCommandSelectTool_ConnectorToolBarCheckItem1,
            this.diagramCommandToolsContainerCheckDropDownItem1,
            this.diagramCommandSelectTool_RectangleToolBarCheckItem1,
            this.diagramCommandSelectTool_EllipseToolBarCheckItem1,
            this.diagramCommandSelectTool_RightTriangleToolBarCheckItem1,
            this.diagramCommandSelectTool_HexagonToolBarCheckItem1,
            this.diagramCommandShapeStylesBarGalleryItem1,
            this.diagramCommandBackgroundColorBarSplitButtonItem1,
            this.diagramCommandStrokeColorBarSplitButtonItem1,
            this.diagramCommandBringToFrontBarSplitButtonItem1,
            this.diagramCommandSendToBackBarSplitButtonItem1,
            this.diagramCommandBringForwardBarButtonItem1,
            this.diagramCommandBringToFrontBarButtonItem1,
            this.diagramCommandSendBackwardBarButtonItem1,
            this.diagramCommandSendToBackBarButtonItem1,
            this.barButtonItem1});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 3;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.diagramHomeRibbonPage1,
            this.diagramViewRibbonPage1,
            this.diagramDesignRibbonPage1});
            this.ribbonControl1.QuickToolbarItemLinks.Add(this.diagramCommandOpenFileBarButtonItem1);
            this.ribbonControl1.QuickToolbarItemLinks.Add(this.diagramCommandSaveFileBarButtonItem1);
            this.ribbonControl1.QuickToolbarItemLinks.Add(this.diagramCommandUndoBarButtonItem1);
            this.ribbonControl1.QuickToolbarItemLinks.Add(this.diagramCommandRedoBarButtonItem1);
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemFontEdit1,
            this.repositoryItemDiagramFontSizeEdit1});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbonControl1.Size = new System.Drawing.Size(1387, 141);
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Above;
            // 
            // applicationMenu1
            // 
            this.applicationMenu1.ItemLinks.Add(this.diagramCommandNewFileBarButtonItem1);
            this.applicationMenu1.ItemLinks.Add(this.diagramCommandOpenFileBarButtonItem1);
            this.applicationMenu1.ItemLinks.Add(this.diagramCommandSaveFileBarButtonItem1);
            this.applicationMenu1.ItemLinks.Add(this.diagramCommandPrintMenuBarSplitButtonItem1);
            this.applicationMenu1.ItemLinks.Add(this.diagramCommandExportAsBarSplitButtonItem1);
            this.applicationMenu1.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.LargeImagesTextDescription;
            this.applicationMenu1.MinWidth = 350;
            this.applicationMenu1.Name = "applicationMenu1";
            this.applicationMenu1.Ribbon = this.ribbonControl1;
            // 
            // diagramCommandNewFileBarButtonItem1
            // 
            this.diagramCommandNewFileBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandNewFileBarButtonItem1.Caption = "Mới";
            this.diagramCommandNewFileBarButtonItem1.Description = "Tạo quan hệ mới";
            this.diagramCommandNewFileBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandNewFileBarButtonItem1.Glyph")));
            this.diagramCommandNewFileBarButtonItem1.Id = 1;
            this.diagramCommandNewFileBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandNewFileBarButtonItem1.LargeGlyph")));
            this.diagramCommandNewFileBarButtonItem1.Name = "diagramCommandNewFileBarButtonItem1";
            superToolTip1.AllowHtmlText = DevExpress.Utils.DefaultBoolean.False;
            toolTipTitleItem1.Text = "Mới (Ctrl+N)";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Tạo mới sơ đồ quan hệ";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.diagramCommandNewFileBarButtonItem1.SuperTip = superToolTip1;
            // 
            // diagramCommandOpenFileBarButtonItem1
            // 
            this.diagramCommandOpenFileBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandOpenFileBarButtonItem1.Caption = "Mở...";
            this.diagramCommandOpenFileBarButtonItem1.Description = "Mở File ";
            this.diagramCommandOpenFileBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandOpenFileBarButtonItem1.Glyph")));
            this.diagramCommandOpenFileBarButtonItem1.Id = 2;
            this.diagramCommandOpenFileBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandOpenFileBarButtonItem1.LargeGlyph")));
            this.diagramCommandOpenFileBarButtonItem1.Name = "diagramCommandOpenFileBarButtonItem1";
            superToolTip2.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem2.Text = "Mở (Ctrl+O)";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Mở biểu đồ quan hệ đã tạo";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.diagramCommandOpenFileBarButtonItem1.SuperTip = superToolTip2;
            this.diagramCommandOpenFileBarButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.diagramCommandOpenFileBarButtonItem1_ItemClick);
            // 
            // diagramCommandSaveFileBarButtonItem1
            // 
            this.diagramCommandSaveFileBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSaveFileBarButtonItem1.Caption = "Lưu";
            this.diagramCommandSaveFileBarButtonItem1.Description = "Save this diagram";
            this.diagramCommandSaveFileBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSaveFileBarButtonItem1.Glyph")));
            this.diagramCommandSaveFileBarButtonItem1.Id = 3;
            this.diagramCommandSaveFileBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSaveFileBarButtonItem1.LargeGlyph")));
            this.diagramCommandSaveFileBarButtonItem1.Name = "diagramCommandSaveFileBarButtonItem1";
            this.diagramCommandSaveFileBarButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.diagramCommandSaveFileBarButtonItem1_ItemClick);
            // 
            // diagramCommandPrintMenuBarSplitButtonItem1
            // 
            this.diagramCommandPrintMenuBarSplitButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandPrintMenuBarSplitButtonItem1.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.diagramCommandPrintMenuBarSplitButtonItem1.Description = "Print this diagram";
            this.diagramCommandPrintMenuBarSplitButtonItem1.DropDownControl = this.popupMenu1;
            this.diagramCommandPrintMenuBarSplitButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPrintMenuBarSplitButtonItem1.Glyph")));
            this.diagramCommandPrintMenuBarSplitButtonItem1.Id = 5;
            this.diagramCommandPrintMenuBarSplitButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPrintMenuBarSplitButtonItem1.LargeGlyph")));
            this.diagramCommandPrintMenuBarSplitButtonItem1.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.Default;
            this.diagramCommandPrintMenuBarSplitButtonItem1.Name = "diagramCommandPrintMenuBarSplitButtonItem1";
            // 
            // popupMenu1
            // 
            this.popupMenu1.ItemLinks.Add(this.diagramCommandPrintBarButtonItem1);
            this.popupMenu1.ItemLinks.Add(this.diagramCommandQuickPrintBarButtonItem1);
            this.popupMenu1.Name = "popupMenu1";
            this.popupMenu1.Ribbon = this.ribbonControl1;
            // 
            // diagramCommandPrintBarButtonItem1
            // 
            this.diagramCommandPrintBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandPrintBarButtonItem1.Description = "Select printer, number of copies, and other printing options before printing.";
            this.diagramCommandPrintBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPrintBarButtonItem1.Glyph")));
            this.diagramCommandPrintBarButtonItem1.Id = 6;
            this.diagramCommandPrintBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPrintBarButtonItem1.LargeGlyph")));
            this.diagramCommandPrintBarButtonItem1.Name = "diagramCommandPrintBarButtonItem1";
            // 
            // diagramCommandQuickPrintBarButtonItem1
            // 
            this.diagramCommandQuickPrintBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandQuickPrintBarButtonItem1.Description = "Send the document directly to the default printer without making changes.";
            this.diagramCommandQuickPrintBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandQuickPrintBarButtonItem1.Glyph")));
            this.diagramCommandQuickPrintBarButtonItem1.Id = 7;
            this.diagramCommandQuickPrintBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandQuickPrintBarButtonItem1.LargeGlyph")));
            this.diagramCommandQuickPrintBarButtonItem1.Name = "diagramCommandQuickPrintBarButtonItem1";
            // 
            // diagramCommandExportAsBarSplitButtonItem1
            // 
            this.diagramCommandExportAsBarSplitButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandExportAsBarSplitButtonItem1.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.diagramCommandExportAsBarSplitButtonItem1.Description = "Export the diagram to various file formats such as PNG or JPEG";
            this.diagramCommandExportAsBarSplitButtonItem1.DropDownControl = this.popupMenu2;
            this.diagramCommandExportAsBarSplitButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandExportAsBarSplitButtonItem1.Glyph")));
            this.diagramCommandExportAsBarSplitButtonItem1.Id = 8;
            this.diagramCommandExportAsBarSplitButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandExportAsBarSplitButtonItem1.LargeGlyph")));
            this.diagramCommandExportAsBarSplitButtonItem1.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.Default;
            this.diagramCommandExportAsBarSplitButtonItem1.Name = "diagramCommandExportAsBarSplitButtonItem1";
            // 
            // popupMenu2
            // 
            this.popupMenu2.ItemLinks.Add(this.diagramCommandExportDiagram_PNGBarButtonItem1);
            this.popupMenu2.ItemLinks.Add(this.diagramCommandExportDiagram_JPEGBarButtonItem1);
            this.popupMenu2.ItemLinks.Add(this.diagramCommandExportDiagram_BMPBarButtonItem1);
            this.popupMenu2.ItemLinks.Add(this.diagramCommandExportDiagram_GIFBarButtonItem1);
            this.popupMenu2.Name = "popupMenu2";
            this.popupMenu2.Ribbon = this.ribbonControl1;
            // 
            // diagramCommandExportDiagram_PNGBarButtonItem1
            // 
            this.diagramCommandExportDiagram_PNGBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandExportDiagram_PNGBarButtonItem1.Description = "Save the diagram as a PNG image that has a high quality and takes very little spa" +
    "ce if the diagram has large areas of exactly uniform color.";
            this.diagramCommandExportDiagram_PNGBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandExportDiagram_PNGBarButtonItem1.Glyph")));
            this.diagramCommandExportDiagram_PNGBarButtonItem1.Id = 9;
            this.diagramCommandExportDiagram_PNGBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandExportDiagram_PNGBarButtonItem1.LargeGlyph")));
            this.diagramCommandExportDiagram_PNGBarButtonItem1.Name = "diagramCommandExportDiagram_PNGBarButtonItem1";
            // 
            // diagramCommandExportDiagram_JPEGBarButtonItem1
            // 
            this.diagramCommandExportDiagram_JPEGBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandExportDiagram_JPEGBarButtonItem1.Description = "Save the diagram as a JPEG image that achieves good compression with little perce" +
    "ptible loss in image quality.";
            this.diagramCommandExportDiagram_JPEGBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandExportDiagram_JPEGBarButtonItem1.Glyph")));
            this.diagramCommandExportDiagram_JPEGBarButtonItem1.Id = 10;
            this.diagramCommandExportDiagram_JPEGBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandExportDiagram_JPEGBarButtonItem1.LargeGlyph")));
            this.diagramCommandExportDiagram_JPEGBarButtonItem1.Name = "diagramCommandExportDiagram_JPEGBarButtonItem1";
            this.diagramCommandExportDiagram_JPEGBarButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // diagramCommandExportDiagram_BMPBarButtonItem1
            // 
            this.diagramCommandExportDiagram_BMPBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandExportDiagram_BMPBarButtonItem1.Description = "Save the diagram as bitmap image file that takes more disk space due to lack of c" +
    "ompression.";
            this.diagramCommandExportDiagram_BMPBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandExportDiagram_BMPBarButtonItem1.Glyph")));
            this.diagramCommandExportDiagram_BMPBarButtonItem1.Id = 11;
            this.diagramCommandExportDiagram_BMPBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandExportDiagram_BMPBarButtonItem1.LargeGlyph")));
            this.diagramCommandExportDiagram_BMPBarButtonItem1.Name = "diagramCommandExportDiagram_BMPBarButtonItem1";
            this.diagramCommandExportDiagram_BMPBarButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // diagramCommandExportDiagram_GIFBarButtonItem1
            // 
            this.diagramCommandExportDiagram_GIFBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandExportDiagram_GIFBarButtonItem1.Description = "Save the diagram as a GIF image that is compact but has a reduced quality if the " +
    "diagram has more than 256 colors.";
            this.diagramCommandExportDiagram_GIFBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandExportDiagram_GIFBarButtonItem1.Glyph")));
            this.diagramCommandExportDiagram_GIFBarButtonItem1.Id = 12;
            this.diagramCommandExportDiagram_GIFBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandExportDiagram_GIFBarButtonItem1.LargeGlyph")));
            this.diagramCommandExportDiagram_GIFBarButtonItem1.Name = "diagramCommandExportDiagram_GIFBarButtonItem1";
            this.diagramCommandExportDiagram_GIFBarButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // diagramCommandUndoBarButtonItem1
            // 
            this.diagramCommandUndoBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandUndoBarButtonItem1.Description = "Undo the last operation.";
            this.diagramCommandUndoBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandUndoBarButtonItem1.Glyph")));
            this.diagramCommandUndoBarButtonItem1.Id = 13;
            this.diagramCommandUndoBarButtonItem1.Name = "diagramCommandUndoBarButtonItem1";
            // 
            // diagramCommandRedoBarButtonItem1
            // 
            this.diagramCommandRedoBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandRedoBarButtonItem1.Description = "Redo the last operation.";
            this.diagramCommandRedoBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandRedoBarButtonItem1.Glyph")));
            this.diagramCommandRedoBarButtonItem1.Id = 14;
            this.diagramCommandRedoBarButtonItem1.Name = "diagramCommandRedoBarButtonItem1";
            // 
            // diagramCommandPageOrientationBarDropDownItem1
            // 
            this.diagramCommandPageOrientationBarDropDownItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandPageOrientationBarDropDownItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageOrientationBarDropDownItem1.Glyph")));
            this.diagramCommandPageOrientationBarDropDownItem1.Id = 15;
            this.diagramCommandPageOrientationBarDropDownItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageOrientationBarDropDownItem1.LargeGlyph")));
            this.diagramCommandPageOrientationBarDropDownItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandPageOrientation_HorizontalBarCheckItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandPageOrientation_VerticalBarCheckItem1)});
            this.diagramCommandPageOrientationBarDropDownItem1.Name = "diagramCommandPageOrientationBarDropDownItem1";
            // 
            // diagramCommandPageOrientation_HorizontalBarCheckItem1
            // 
            this.diagramCommandPageOrientation_HorizontalBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandPageOrientation_HorizontalBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageOrientation_HorizontalBarCheckItem1.Glyph")));
            this.diagramCommandPageOrientation_HorizontalBarCheckItem1.Id = 18;
            this.diagramCommandPageOrientation_HorizontalBarCheckItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageOrientation_HorizontalBarCheckItem1.LargeGlyph")));
            this.diagramCommandPageOrientation_HorizontalBarCheckItem1.Name = "diagramCommandPageOrientation_HorizontalBarCheckItem1";
            toolTipTitleItem3.Text = "Landscape";
            superToolTip3.Items.Add(toolTipTitleItem3);
            this.diagramCommandPageOrientation_HorizontalBarCheckItem1.SuperTip = superToolTip3;
            // 
            // diagramCommandPageOrientation_VerticalBarCheckItem1
            // 
            this.diagramCommandPageOrientation_VerticalBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandPageOrientation_VerticalBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageOrientation_VerticalBarCheckItem1.Glyph")));
            this.diagramCommandPageOrientation_VerticalBarCheckItem1.Id = 19;
            this.diagramCommandPageOrientation_VerticalBarCheckItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageOrientation_VerticalBarCheckItem1.LargeGlyph")));
            this.diagramCommandPageOrientation_VerticalBarCheckItem1.Name = "diagramCommandPageOrientation_VerticalBarCheckItem1";
            toolTipTitleItem4.Text = "Portrait";
            superToolTip4.Items.Add(toolTipTitleItem4);
            this.diagramCommandPageOrientation_VerticalBarCheckItem1.SuperTip = superToolTip4;
            // 
            // diagramCommandPageSizeBarDropDownItem1
            // 
            this.diagramCommandPageSizeBarDropDownItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandPageSizeBarDropDownItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSizeBarDropDownItem1.Glyph")));
            this.diagramCommandPageSizeBarDropDownItem1.Id = 16;
            this.diagramCommandPageSizeBarDropDownItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSizeBarDropDownItem1.LargeGlyph")));
            this.diagramCommandPageSizeBarDropDownItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandPageSize_LetterBarCheckItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandPageSize_TabloidBarCheckItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandPageSize_LegalBarCheckItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandPageSize_StatementBarCheckItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandPageSize_ExecutiveBarCheckItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandPageSize_A3BarCheckItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandPageSize_A4BarCheckItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandPageSize_A5BarCheckItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandPageSize_B4BarCheckItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandPageSize_B5BarCheckItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandFitToDrawingBarButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandSetPageParameters_PageSizeBarButtonItem1)});
            this.diagramCommandPageSizeBarDropDownItem1.Name = "diagramCommandPageSizeBarDropDownItem1";
            // 
            // diagramCommandPageSize_LetterBarCheckItem1
            // 
            this.diagramCommandPageSize_LetterBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandPageSize_LetterBarCheckItem1.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.diagramCommandPageSize_LetterBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_LetterBarCheckItem1.Glyph")));
            this.diagramCommandPageSize_LetterBarCheckItem1.Id = 20;
            this.diagramCommandPageSize_LetterBarCheckItem1.ItemInMenuAppearance.Normal.Options.UseTextOptions = true;
            this.diagramCommandPageSize_LetterBarCheckItem1.ItemInMenuAppearance.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_LetterBarCheckItem1.ItemInMenuAppearance.Pressed.Options.UseTextOptions = true;
            this.diagramCommandPageSize_LetterBarCheckItem1.ItemInMenuAppearance.Pressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_LetterBarCheckItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_LetterBarCheckItem1.LargeGlyph")));
            this.diagramCommandPageSize_LetterBarCheckItem1.Name = "diagramCommandPageSize_LetterBarCheckItem1";
            superToolTip5.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem5.Text = "<b>Letter</b><br>8.5\" x 11\"";
            superToolTip5.Items.Add(toolTipTitleItem5);
            this.diagramCommandPageSize_LetterBarCheckItem1.SuperTip = superToolTip5;
            // 
            // diagramCommandPageSize_TabloidBarCheckItem1
            // 
            this.diagramCommandPageSize_TabloidBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandPageSize_TabloidBarCheckItem1.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.diagramCommandPageSize_TabloidBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_TabloidBarCheckItem1.Glyph")));
            this.diagramCommandPageSize_TabloidBarCheckItem1.Id = 21;
            this.diagramCommandPageSize_TabloidBarCheckItem1.ItemInMenuAppearance.Normal.Options.UseTextOptions = true;
            this.diagramCommandPageSize_TabloidBarCheckItem1.ItemInMenuAppearance.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_TabloidBarCheckItem1.ItemInMenuAppearance.Pressed.Options.UseTextOptions = true;
            this.diagramCommandPageSize_TabloidBarCheckItem1.ItemInMenuAppearance.Pressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_TabloidBarCheckItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_TabloidBarCheckItem1.LargeGlyph")));
            this.diagramCommandPageSize_TabloidBarCheckItem1.Name = "diagramCommandPageSize_TabloidBarCheckItem1";
            superToolTip6.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem6.Text = "<b>Tabloid</b><br>11\" x 17\"";
            superToolTip6.Items.Add(toolTipTitleItem6);
            this.diagramCommandPageSize_TabloidBarCheckItem1.SuperTip = superToolTip6;
            // 
            // diagramCommandPageSize_LegalBarCheckItem1
            // 
            this.diagramCommandPageSize_LegalBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandPageSize_LegalBarCheckItem1.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.diagramCommandPageSize_LegalBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_LegalBarCheckItem1.Glyph")));
            this.diagramCommandPageSize_LegalBarCheckItem1.Id = 22;
            this.diagramCommandPageSize_LegalBarCheckItem1.ItemInMenuAppearance.Normal.Options.UseTextOptions = true;
            this.diagramCommandPageSize_LegalBarCheckItem1.ItemInMenuAppearance.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_LegalBarCheckItem1.ItemInMenuAppearance.Pressed.Options.UseTextOptions = true;
            this.diagramCommandPageSize_LegalBarCheckItem1.ItemInMenuAppearance.Pressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_LegalBarCheckItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_LegalBarCheckItem1.LargeGlyph")));
            this.diagramCommandPageSize_LegalBarCheckItem1.Name = "diagramCommandPageSize_LegalBarCheckItem1";
            superToolTip7.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem7.Text = "<b>Legal</b><br>8.5\" x 14\"";
            superToolTip7.Items.Add(toolTipTitleItem7);
            this.diagramCommandPageSize_LegalBarCheckItem1.SuperTip = superToolTip7;
            // 
            // diagramCommandPageSize_StatementBarCheckItem1
            // 
            this.diagramCommandPageSize_StatementBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandPageSize_StatementBarCheckItem1.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.diagramCommandPageSize_StatementBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_StatementBarCheckItem1.Glyph")));
            this.diagramCommandPageSize_StatementBarCheckItem1.Id = 23;
            this.diagramCommandPageSize_StatementBarCheckItem1.ItemInMenuAppearance.Normal.Options.UseTextOptions = true;
            this.diagramCommandPageSize_StatementBarCheckItem1.ItemInMenuAppearance.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_StatementBarCheckItem1.ItemInMenuAppearance.Pressed.Options.UseTextOptions = true;
            this.diagramCommandPageSize_StatementBarCheckItem1.ItemInMenuAppearance.Pressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_StatementBarCheckItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_StatementBarCheckItem1.LargeGlyph")));
            this.diagramCommandPageSize_StatementBarCheckItem1.Name = "diagramCommandPageSize_StatementBarCheckItem1";
            superToolTip8.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem8.Text = "<b>Statement</b><br>5.5\" x 8.5\"";
            superToolTip8.Items.Add(toolTipTitleItem8);
            this.diagramCommandPageSize_StatementBarCheckItem1.SuperTip = superToolTip8;
            // 
            // diagramCommandPageSize_ExecutiveBarCheckItem1
            // 
            this.diagramCommandPageSize_ExecutiveBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandPageSize_ExecutiveBarCheckItem1.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.diagramCommandPageSize_ExecutiveBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_ExecutiveBarCheckItem1.Glyph")));
            this.diagramCommandPageSize_ExecutiveBarCheckItem1.Id = 24;
            this.diagramCommandPageSize_ExecutiveBarCheckItem1.ItemInMenuAppearance.Normal.Options.UseTextOptions = true;
            this.diagramCommandPageSize_ExecutiveBarCheckItem1.ItemInMenuAppearance.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_ExecutiveBarCheckItem1.ItemInMenuAppearance.Pressed.Options.UseTextOptions = true;
            this.diagramCommandPageSize_ExecutiveBarCheckItem1.ItemInMenuAppearance.Pressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_ExecutiveBarCheckItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_ExecutiveBarCheckItem1.LargeGlyph")));
            this.diagramCommandPageSize_ExecutiveBarCheckItem1.Name = "diagramCommandPageSize_ExecutiveBarCheckItem1";
            superToolTip9.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem9.Text = "<b>Executive</b><br>7.25\" x 10.5\"";
            superToolTip9.Items.Add(toolTipTitleItem9);
            this.diagramCommandPageSize_ExecutiveBarCheckItem1.SuperTip = superToolTip9;
            // 
            // diagramCommandPageSize_A3BarCheckItem1
            // 
            this.diagramCommandPageSize_A3BarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandPageSize_A3BarCheckItem1.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.diagramCommandPageSize_A3BarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_A3BarCheckItem1.Glyph")));
            this.diagramCommandPageSize_A3BarCheckItem1.Id = 25;
            this.diagramCommandPageSize_A3BarCheckItem1.ItemInMenuAppearance.Normal.Options.UseTextOptions = true;
            this.diagramCommandPageSize_A3BarCheckItem1.ItemInMenuAppearance.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_A3BarCheckItem1.ItemInMenuAppearance.Pressed.Options.UseTextOptions = true;
            this.diagramCommandPageSize_A3BarCheckItem1.ItemInMenuAppearance.Pressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_A3BarCheckItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_A3BarCheckItem1.LargeGlyph")));
            this.diagramCommandPageSize_A3BarCheckItem1.Name = "diagramCommandPageSize_A3BarCheckItem1";
            superToolTip10.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem10.Text = "<b>A3</b><br>11.7\" x 16.53\"";
            superToolTip10.Items.Add(toolTipTitleItem10);
            this.diagramCommandPageSize_A3BarCheckItem1.SuperTip = superToolTip10;
            // 
            // diagramCommandPageSize_A4BarCheckItem1
            // 
            this.diagramCommandPageSize_A4BarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandPageSize_A4BarCheckItem1.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.diagramCommandPageSize_A4BarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_A4BarCheckItem1.Glyph")));
            this.diagramCommandPageSize_A4BarCheckItem1.Id = 26;
            this.diagramCommandPageSize_A4BarCheckItem1.ItemInMenuAppearance.Normal.Options.UseTextOptions = true;
            this.diagramCommandPageSize_A4BarCheckItem1.ItemInMenuAppearance.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_A4BarCheckItem1.ItemInMenuAppearance.Pressed.Options.UseTextOptions = true;
            this.diagramCommandPageSize_A4BarCheckItem1.ItemInMenuAppearance.Pressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_A4BarCheckItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_A4BarCheckItem1.LargeGlyph")));
            this.diagramCommandPageSize_A4BarCheckItem1.Name = "diagramCommandPageSize_A4BarCheckItem1";
            superToolTip11.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem11.Text = "<b>A4</b><br>8.27\" x 11.7\"";
            superToolTip11.Items.Add(toolTipTitleItem11);
            this.diagramCommandPageSize_A4BarCheckItem1.SuperTip = superToolTip11;
            // 
            // diagramCommandPageSize_A5BarCheckItem1
            // 
            this.diagramCommandPageSize_A5BarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandPageSize_A5BarCheckItem1.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.diagramCommandPageSize_A5BarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_A5BarCheckItem1.Glyph")));
            this.diagramCommandPageSize_A5BarCheckItem1.Id = 27;
            this.diagramCommandPageSize_A5BarCheckItem1.ItemInMenuAppearance.Normal.Options.UseTextOptions = true;
            this.diagramCommandPageSize_A5BarCheckItem1.ItemInMenuAppearance.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_A5BarCheckItem1.ItemInMenuAppearance.Pressed.Options.UseTextOptions = true;
            this.diagramCommandPageSize_A5BarCheckItem1.ItemInMenuAppearance.Pressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_A5BarCheckItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_A5BarCheckItem1.LargeGlyph")));
            this.diagramCommandPageSize_A5BarCheckItem1.Name = "diagramCommandPageSize_A5BarCheckItem1";
            superToolTip12.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem12.Text = "<b>A5</b><br>5.82\" x 8.27\"";
            superToolTip12.Items.Add(toolTipTitleItem12);
            this.diagramCommandPageSize_A5BarCheckItem1.SuperTip = superToolTip12;
            // 
            // diagramCommandPageSize_B4BarCheckItem1
            // 
            this.diagramCommandPageSize_B4BarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandPageSize_B4BarCheckItem1.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.diagramCommandPageSize_B4BarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_B4BarCheckItem1.Glyph")));
            this.diagramCommandPageSize_B4BarCheckItem1.Id = 28;
            this.diagramCommandPageSize_B4BarCheckItem1.ItemInMenuAppearance.Normal.Options.UseTextOptions = true;
            this.diagramCommandPageSize_B4BarCheckItem1.ItemInMenuAppearance.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_B4BarCheckItem1.ItemInMenuAppearance.Pressed.Options.UseTextOptions = true;
            this.diagramCommandPageSize_B4BarCheckItem1.ItemInMenuAppearance.Pressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_B4BarCheckItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_B4BarCheckItem1.LargeGlyph")));
            this.diagramCommandPageSize_B4BarCheckItem1.Name = "diagramCommandPageSize_B4BarCheckItem1";
            superToolTip13.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem13.Text = "<b>B4</b><br>9.84\" x 13.9\"";
            superToolTip13.Items.Add(toolTipTitleItem13);
            this.diagramCommandPageSize_B4BarCheckItem1.SuperTip = superToolTip13;
            // 
            // diagramCommandPageSize_B5BarCheckItem1
            // 
            this.diagramCommandPageSize_B5BarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandPageSize_B5BarCheckItem1.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.diagramCommandPageSize_B5BarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_B5BarCheckItem1.Glyph")));
            this.diagramCommandPageSize_B5BarCheckItem1.Id = 29;
            this.diagramCommandPageSize_B5BarCheckItem1.ItemInMenuAppearance.Normal.Options.UseTextOptions = true;
            this.diagramCommandPageSize_B5BarCheckItem1.ItemInMenuAppearance.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_B5BarCheckItem1.ItemInMenuAppearance.Pressed.Options.UseTextOptions = true;
            this.diagramCommandPageSize_B5BarCheckItem1.ItemInMenuAppearance.Pressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.diagramCommandPageSize_B5BarCheckItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPageSize_B5BarCheckItem1.LargeGlyph")));
            this.diagramCommandPageSize_B5BarCheckItem1.Name = "diagramCommandPageSize_B5BarCheckItem1";
            superToolTip14.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem14.Text = "<b>B5</b><br>6.93\" x 9.84\"";
            superToolTip14.Items.Add(toolTipTitleItem14);
            this.diagramCommandPageSize_B5BarCheckItem1.SuperTip = superToolTip14;
            // 
            // diagramCommandFitToDrawingBarButtonItem1
            // 
            this.diagramCommandFitToDrawingBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandFitToDrawingBarButtonItem1.Description = "Truncate the page width and height so that the page size matches the diagram size" +
    ".";
            this.diagramCommandFitToDrawingBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandFitToDrawingBarButtonItem1.Glyph")));
            this.diagramCommandFitToDrawingBarButtonItem1.Id = 30;
            this.diagramCommandFitToDrawingBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandFitToDrawingBarButtonItem1.LargeGlyph")));
            this.diagramCommandFitToDrawingBarButtonItem1.Name = "diagramCommandFitToDrawingBarButtonItem1";
            // 
            // diagramCommandSetPageParameters_PageSizeBarButtonItem1
            // 
            this.diagramCommandSetPageParameters_PageSizeBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSetPageParameters_PageSizeBarButtonItem1.Description = "Choose a page size from the list or set a custom size.";
            this.diagramCommandSetPageParameters_PageSizeBarButtonItem1.Id = 31;
            this.diagramCommandSetPageParameters_PageSizeBarButtonItem1.Name = "diagramCommandSetPageParameters_PageSizeBarButtonItem1";
            // 
            // diagramCommandAutoSizeBarDropDownItem1
            // 
            this.diagramCommandAutoSizeBarDropDownItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandAutoSizeBarDropDownItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandAutoSizeBarDropDownItem1.Glyph")));
            this.diagramCommandAutoSizeBarDropDownItem1.Id = 17;
            this.diagramCommandAutoSizeBarDropDownItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandAutoSizeBarDropDownItem1.LargeGlyph")));
            this.diagramCommandAutoSizeBarDropDownItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandAutoSize_NoneBarCheckItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandAutoSize_AutoSizeBarCheckItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandAutoSize_FillBarCheckItem1)});
            this.diagramCommandAutoSizeBarDropDownItem1.Name = "diagramCommandAutoSizeBarDropDownItem1";
            // 
            // diagramCommandAutoSize_NoneBarCheckItem1
            // 
            this.diagramCommandAutoSize_NoneBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandAutoSize_NoneBarCheckItem1.Description = "The page size is not changed on moving elements outside of its borders.";
            this.diagramCommandAutoSize_NoneBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandAutoSize_NoneBarCheckItem1.Glyph")));
            this.diagramCommandAutoSize_NoneBarCheckItem1.Id = 32;
            this.diagramCommandAutoSize_NoneBarCheckItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandAutoSize_NoneBarCheckItem1.LargeGlyph")));
            this.diagramCommandAutoSize_NoneBarCheckItem1.Name = "diagramCommandAutoSize_NoneBarCheckItem1";
            // 
            // diagramCommandAutoSize_AutoSizeBarCheckItem1
            // 
            this.diagramCommandAutoSize_AutoSizeBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandAutoSize_AutoSizeBarCheckItem1.Description = "Automatically resize the page.";
            this.diagramCommandAutoSize_AutoSizeBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandAutoSize_AutoSizeBarCheckItem1.Glyph")));
            this.diagramCommandAutoSize_AutoSizeBarCheckItem1.Id = 33;
            this.diagramCommandAutoSize_AutoSizeBarCheckItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandAutoSize_AutoSizeBarCheckItem1.LargeGlyph")));
            this.diagramCommandAutoSize_AutoSizeBarCheckItem1.Name = "diagramCommandAutoSize_AutoSizeBarCheckItem1";
            // 
            // diagramCommandAutoSize_FillBarCheckItem1
            // 
            this.diagramCommandAutoSize_FillBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandAutoSize_FillBarCheckItem1.Description = "Fills the entire visible area";
            this.diagramCommandAutoSize_FillBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandAutoSize_FillBarCheckItem1.Glyph")));
            this.diagramCommandAutoSize_FillBarCheckItem1.Id = 34;
            this.diagramCommandAutoSize_FillBarCheckItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandAutoSize_FillBarCheckItem1.LargeGlyph")));
            this.diagramCommandAutoSize_FillBarCheckItem1.Name = "diagramCommandAutoSize_FillBarCheckItem1";
            // 
            // diagramCommandThemesBarGalleryItem1
            // 
            // 
            // 
            // 
            this.diagramCommandThemesBarGalleryItem1.Gallery.ColumnCount = 8;
            this.diagramCommandThemesBarGalleryItem1.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            galleryItemGroup1,
            galleryItemGroup2,
            galleryItemGroup3,
            galleryItemGroup4,
            galleryItemGroup5,
            galleryItemGroup6,
            galleryItemGroup7,
            galleryItemGroup8,
            galleryItemGroup9,
            galleryItemGroup10,
            galleryItemGroup11,
            galleryItemGroup12,
            galleryItemGroup13,
            galleryItemGroup14,
            galleryItemGroup15,
            galleryItemGroup16,
            galleryItemGroup17,
            galleryItemGroup18,
            galleryItemGroup19,
            galleryItemGroup20,
            galleryItemGroup21,
            galleryItemGroup22,
            galleryItemGroup23,
            galleryItemGroup24,
            galleryItemGroup25,
            galleryItemGroup26,
            galleryItemGroup27,
            galleryItemGroup28,
            galleryItemGroup29,
            galleryItemGroup30,
            galleryItemGroup31,
            galleryItemGroup32,
            galleryItemGroup33,
            galleryItemGroup34,
            galleryItemGroup35,
            galleryItemGroup36});
            this.diagramCommandThemesBarGalleryItem1.Gallery.ImageSize = new System.Drawing.Size(65, 46);
            this.diagramCommandThemesBarGalleryItem1.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.SingleRadio;
            this.diagramCommandThemesBarGalleryItem1.Id = 35;
            this.diagramCommandThemesBarGalleryItem1.Name = "diagramCommandThemesBarGalleryItem1";
            // 
            // diagramCommandSnapToItemsBarCheckItem1
            // 
            this.diagramCommandSnapToItemsBarCheckItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.diagramCommandSnapToItemsBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSnapToItemsBarCheckItem1.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.diagramCommandSnapToItemsBarCheckItem1.Description = "Align diagram items with each other.";
            this.diagramCommandSnapToItemsBarCheckItem1.Id = 36;
            this.diagramCommandSnapToItemsBarCheckItem1.Name = "diagramCommandSnapToItemsBarCheckItem1";
            // 
            // diagramCommandSnapToGridBarCheckItem1
            // 
            this.diagramCommandSnapToGridBarCheckItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.diagramCommandSnapToGridBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSnapToGridBarCheckItem1.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.diagramCommandSnapToGridBarCheckItem1.Description = "Position diagram items to the closes intersection of the grid.";
            this.diagramCommandSnapToGridBarCheckItem1.Id = 37;
            this.diagramCommandSnapToGridBarCheckItem1.Name = "diagramCommandSnapToGridBarCheckItem1";
            // 
            // diagramCommandReLayoutBarDropDownItem1
            // 
            this.diagramCommandReLayoutBarDropDownItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandReLayoutBarDropDownItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandReLayoutBarDropDownItem1.Glyph")));
            this.diagramCommandReLayoutBarDropDownItem1.Id = 38;
            this.diagramCommandReLayoutBarDropDownItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandReLayoutBarDropDownItem1.LargeGlyph")));
            this.diagramCommandReLayoutBarDropDownItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramReLayoutTreeBarHeaderItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandTreeLayout_DownBarButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandTreeLayout_UpBarButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandTreeLayout_RightBarButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandTreeLayout_LeftBarButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramReLayoutSugiyamaBarHeaderItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandSugiyamaLayout_DownBarButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandSugiyamaLayout_UpBarButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandSugiyamaLayout_RightBarButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.diagramCommandSugiyamaLayout_LeftBarButtonItem1)});
            this.diagramCommandReLayoutBarDropDownItem1.MultiColumn = DevExpress.Utils.DefaultBoolean.True;
            this.diagramCommandReLayoutBarDropDownItem1.Name = "diagramCommandReLayoutBarDropDownItem1";
            // 
            // diagramReLayoutTreeBarHeaderItem1
            // 
            this.diagramReLayoutTreeBarHeaderItem1.Caption = "Hierarchy";
            this.diagramReLayoutTreeBarHeaderItem1.Id = 39;
            this.diagramReLayoutTreeBarHeaderItem1.Name = "diagramReLayoutTreeBarHeaderItem1";
            // 
            // diagramCommandTreeLayout_DownBarButtonItem1
            // 
            this.diagramCommandTreeLayout_DownBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandTreeLayout_DownBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandTreeLayout_DownBarButtonItem1.Glyph")));
            this.diagramCommandTreeLayout_DownBarButtonItem1.Id = 40;
            this.diagramCommandTreeLayout_DownBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandTreeLayout_DownBarButtonItem1.LargeGlyph")));
            this.diagramCommandTreeLayout_DownBarButtonItem1.Name = "diagramCommandTreeLayout_DownBarButtonItem1";
            toolTipTitleItem15.Text = "Top To Bottom";
            superToolTip15.Items.Add(toolTipTitleItem15);
            this.diagramCommandTreeLayout_DownBarButtonItem1.SuperTip = superToolTip15;
            // 
            // diagramCommandTreeLayout_UpBarButtonItem1
            // 
            this.diagramCommandTreeLayout_UpBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandTreeLayout_UpBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandTreeLayout_UpBarButtonItem1.Glyph")));
            this.diagramCommandTreeLayout_UpBarButtonItem1.Id = 41;
            this.diagramCommandTreeLayout_UpBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandTreeLayout_UpBarButtonItem1.LargeGlyph")));
            this.diagramCommandTreeLayout_UpBarButtonItem1.Name = "diagramCommandTreeLayout_UpBarButtonItem1";
            toolTipTitleItem16.Text = "Bottom To Top";
            superToolTip16.Items.Add(toolTipTitleItem16);
            this.diagramCommandTreeLayout_UpBarButtonItem1.SuperTip = superToolTip16;
            // 
            // diagramCommandTreeLayout_RightBarButtonItem1
            // 
            this.diagramCommandTreeLayout_RightBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandTreeLayout_RightBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandTreeLayout_RightBarButtonItem1.Glyph")));
            this.diagramCommandTreeLayout_RightBarButtonItem1.Id = 42;
            this.diagramCommandTreeLayout_RightBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandTreeLayout_RightBarButtonItem1.LargeGlyph")));
            this.diagramCommandTreeLayout_RightBarButtonItem1.Name = "diagramCommandTreeLayout_RightBarButtonItem1";
            toolTipTitleItem17.Text = "Left To Right";
            superToolTip17.Items.Add(toolTipTitleItem17);
            this.diagramCommandTreeLayout_RightBarButtonItem1.SuperTip = superToolTip17;
            // 
            // diagramCommandTreeLayout_LeftBarButtonItem1
            // 
            this.diagramCommandTreeLayout_LeftBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandTreeLayout_LeftBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandTreeLayout_LeftBarButtonItem1.Glyph")));
            this.diagramCommandTreeLayout_LeftBarButtonItem1.Id = 43;
            this.diagramCommandTreeLayout_LeftBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandTreeLayout_LeftBarButtonItem1.LargeGlyph")));
            this.diagramCommandTreeLayout_LeftBarButtonItem1.Name = "diagramCommandTreeLayout_LeftBarButtonItem1";
            toolTipTitleItem18.Text = "Right To Left";
            superToolTip18.Items.Add(toolTipTitleItem18);
            this.diagramCommandTreeLayout_LeftBarButtonItem1.SuperTip = superToolTip18;
            // 
            // diagramReLayoutSugiyamaBarHeaderItem1
            // 
            this.diagramReLayoutSugiyamaBarHeaderItem1.Caption = "Layered";
            this.diagramReLayoutSugiyamaBarHeaderItem1.Id = 44;
            this.diagramReLayoutSugiyamaBarHeaderItem1.Name = "diagramReLayoutSugiyamaBarHeaderItem1";
            // 
            // diagramCommandSugiyamaLayout_DownBarButtonItem1
            // 
            this.diagramCommandSugiyamaLayout_DownBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSugiyamaLayout_DownBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSugiyamaLayout_DownBarButtonItem1.Glyph")));
            this.diagramCommandSugiyamaLayout_DownBarButtonItem1.Id = 45;
            this.diagramCommandSugiyamaLayout_DownBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSugiyamaLayout_DownBarButtonItem1.LargeGlyph")));
            this.diagramCommandSugiyamaLayout_DownBarButtonItem1.Name = "diagramCommandSugiyamaLayout_DownBarButtonItem1";
            toolTipTitleItem19.Text = "Top To Bottom";
            superToolTip19.Items.Add(toolTipTitleItem19);
            this.diagramCommandSugiyamaLayout_DownBarButtonItem1.SuperTip = superToolTip19;
            // 
            // diagramCommandSugiyamaLayout_UpBarButtonItem1
            // 
            this.diagramCommandSugiyamaLayout_UpBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSugiyamaLayout_UpBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSugiyamaLayout_UpBarButtonItem1.Glyph")));
            this.diagramCommandSugiyamaLayout_UpBarButtonItem1.Id = 46;
            this.diagramCommandSugiyamaLayout_UpBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSugiyamaLayout_UpBarButtonItem1.LargeGlyph")));
            this.diagramCommandSugiyamaLayout_UpBarButtonItem1.Name = "diagramCommandSugiyamaLayout_UpBarButtonItem1";
            toolTipTitleItem20.Text = "Bottom To Top";
            superToolTip20.Items.Add(toolTipTitleItem20);
            this.diagramCommandSugiyamaLayout_UpBarButtonItem1.SuperTip = superToolTip20;
            // 
            // diagramCommandSugiyamaLayout_RightBarButtonItem1
            // 
            this.diagramCommandSugiyamaLayout_RightBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSugiyamaLayout_RightBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSugiyamaLayout_RightBarButtonItem1.Glyph")));
            this.diagramCommandSugiyamaLayout_RightBarButtonItem1.Id = 47;
            this.diagramCommandSugiyamaLayout_RightBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSugiyamaLayout_RightBarButtonItem1.LargeGlyph")));
            this.diagramCommandSugiyamaLayout_RightBarButtonItem1.Name = "diagramCommandSugiyamaLayout_RightBarButtonItem1";
            toolTipTitleItem21.Text = "Left To Right";
            superToolTip21.Items.Add(toolTipTitleItem21);
            this.diagramCommandSugiyamaLayout_RightBarButtonItem1.SuperTip = superToolTip21;
            // 
            // diagramCommandSugiyamaLayout_LeftBarButtonItem1
            // 
            this.diagramCommandSugiyamaLayout_LeftBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSugiyamaLayout_LeftBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSugiyamaLayout_LeftBarButtonItem1.Glyph")));
            this.diagramCommandSugiyamaLayout_LeftBarButtonItem1.Id = 48;
            this.diagramCommandSugiyamaLayout_LeftBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSugiyamaLayout_LeftBarButtonItem1.LargeGlyph")));
            this.diagramCommandSugiyamaLayout_LeftBarButtonItem1.Name = "diagramCommandSugiyamaLayout_LeftBarButtonItem1";
            toolTipTitleItem22.Text = "Right To Left";
            superToolTip22.Items.Add(toolTipTitleItem22);
            this.diagramCommandSugiyamaLayout_LeftBarButtonItem1.SuperTip = superToolTip22;
            // 
            // diagramCommandShowRulersBarCheckItem1
            // 
            this.diagramCommandShowRulersBarCheckItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.diagramCommandShowRulersBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandShowRulersBarCheckItem1.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.diagramCommandShowRulersBarCheckItem1.Description = "View the rulers used to measure and line up objects in the document.";
            this.diagramCommandShowRulersBarCheckItem1.Id = 49;
            this.diagramCommandShowRulersBarCheckItem1.Name = "diagramCommandShowRulersBarCheckItem1";
            this.diagramCommandShowRulersBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // diagramCommandShowGridBarCheckItem1
            // 
            this.diagramCommandShowGridBarCheckItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.diagramCommandShowGridBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandShowGridBarCheckItem1.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.diagramCommandShowGridBarCheckItem1.Description = "The gridlines make it easy for to you align objects with text, other objects, or " +
    "a particular spot";
            this.diagramCommandShowGridBarCheckItem1.Id = 50;
            this.diagramCommandShowGridBarCheckItem1.Name = "diagramCommandShowGridBarCheckItem1";
            this.diagramCommandShowGridBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // diagramCommandShowPageBreaksBarCheckItem1
            // 
            this.diagramCommandShowPageBreaksBarCheckItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.diagramCommandShowPageBreaksBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandShowPageBreaksBarCheckItem1.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.diagramCommandShowPageBreaksBarCheckItem1.Description = "Turn on page breaks to see what will be printed on each page of your document.";
            this.diagramCommandShowPageBreaksBarCheckItem1.Id = 51;
            this.diagramCommandShowPageBreaksBarCheckItem1.Name = "diagramCommandShowPageBreaksBarCheckItem1";
            this.diagramCommandShowPageBreaksBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // diagramCommandFitToPageBarButtonItem1
            // 
            this.diagramCommandFitToPageBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandFitToPageBarButtonItem1.Description = "Zoom the document so that the entire page fits in and fills the window.";
            this.diagramCommandFitToPageBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandFitToPageBarButtonItem1.Glyph")));
            this.diagramCommandFitToPageBarButtonItem1.Id = 52;
            this.diagramCommandFitToPageBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandFitToPageBarButtonItem1.LargeGlyph")));
            this.diagramCommandFitToPageBarButtonItem1.Name = "diagramCommandFitToPageBarButtonItem1";
            this.diagramCommandFitToPageBarButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // diagramCommandFitToWidthBarButtonItem1
            // 
            this.diagramCommandFitToWidthBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandFitToWidthBarButtonItem1.Description = "Zoom the document so that the page is as wide as the window.";
            this.diagramCommandFitToWidthBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandFitToWidthBarButtonItem1.Glyph")));
            this.diagramCommandFitToWidthBarButtonItem1.Id = 53;
            this.diagramCommandFitToWidthBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandFitToWidthBarButtonItem1.LargeGlyph")));
            this.diagramCommandFitToWidthBarButtonItem1.Name = "diagramCommandFitToWidthBarButtonItem1";
            this.diagramCommandFitToWidthBarButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // diagramCommandPasteBarButtonItem1
            // 
            this.diagramCommandPasteBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandPasteBarButtonItem1.Description = "Paste the contents of the Clipboard.";
            this.diagramCommandPasteBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPasteBarButtonItem1.Glyph")));
            this.diagramCommandPasteBarButtonItem1.Id = 58;
            this.diagramCommandPasteBarButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandPasteBarButtonItem1.LargeGlyph")));
            this.diagramCommandPasteBarButtonItem1.Name = "diagramCommandPasteBarButtonItem1";
            this.diagramCommandPasteBarButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // diagramCommandCutBarButtonItem1
            // 
            this.diagramCommandCutBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandCutBarButtonItem1.Description = "Remove the selection and put it on the Clipboard so you can paste it somewhere el" +
    "se.";
            this.diagramCommandCutBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandCutBarButtonItem1.Glyph")));
            this.diagramCommandCutBarButtonItem1.Id = 59;
            this.diagramCommandCutBarButtonItem1.Name = "diagramCommandCutBarButtonItem1";
            this.diagramCommandCutBarButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // diagramCommandCopyBarButtonItem1
            // 
            this.diagramCommandCopyBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandCopyBarButtonItem1.Description = "Put a copy of the selection on the Clipboard so you can paste it somewhere else.";
            this.diagramCommandCopyBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandCopyBarButtonItem1.Glyph")));
            this.diagramCommandCopyBarButtonItem1.Id = 60;
            this.diagramCommandCopyBarButtonItem1.Name = "diagramCommandCopyBarButtonItem1";
            this.diagramCommandCopyBarButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // barButtonGroup1
            // 
            this.barButtonGroup1.Id = 54;
            this.barButtonGroup1.ItemLinks.Add(this.diagramCommandFontFamilyBarEditItem1);
            this.barButtonGroup1.ItemLinks.Add(this.diagramCommandFontSizeBarEditItem1);
            this.barButtonGroup1.ItemLinks.Add(this.diagramCommandIncreaseFontSizeBarButtonItem1);
            this.barButtonGroup1.ItemLinks.Add(this.diagramCommandDecreaseFontSizeBarButtonItem1);
            this.barButtonGroup1.Name = "barButtonGroup1";
            this.barButtonGroup1.Tag = "bgFontSizeAndFamily";
            // 
            // diagramCommandFontFamilyBarEditItem1
            // 
            this.diagramCommandFontFamilyBarEditItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandFontFamilyBarEditItem1.Description = "Pick a new font for your text.";
            this.diagramCommandFontFamilyBarEditItem1.Edit = this.repositoryItemFontEdit1;
            this.diagramCommandFontFamilyBarEditItem1.EditWidth = 130;
            this.diagramCommandFontFamilyBarEditItem1.Id = 61;
            this.diagramCommandFontFamilyBarEditItem1.Name = "diagramCommandFontFamilyBarEditItem1";
            // 
            // repositoryItemFontEdit1
            // 
            this.repositoryItemFontEdit1.AutoHeight = false;
            this.repositoryItemFontEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemFontEdit1.Name = "repositoryItemFontEdit1";
            // 
            // diagramCommandFontSizeBarEditItem1
            // 
            this.diagramCommandFontSizeBarEditItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandFontSizeBarEditItem1.Description = "Change the size of your text.";
            this.diagramCommandFontSizeBarEditItem1.Edit = this.repositoryItemDiagramFontSizeEdit1;
            this.diagramCommandFontSizeBarEditItem1.Id = 62;
            this.diagramCommandFontSizeBarEditItem1.Name = "diagramCommandFontSizeBarEditItem1";
            // 
            // repositoryItemDiagramFontSizeEdit1
            // 
            this.repositoryItemDiagramFontSizeEdit1.AutoHeight = false;
            this.repositoryItemDiagramFontSizeEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDiagramFontSizeEdit1.Diagram = this.diagramControl1;
            this.repositoryItemDiagramFontSizeEdit1.Name = "repositoryItemDiagramFontSizeEdit1";
            // 
            // diagramCommandIncreaseFontSizeBarButtonItem1
            // 
            this.diagramCommandIncreaseFontSizeBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandIncreaseFontSizeBarButtonItem1.Description = "Make your text a bit bigger.";
            this.diagramCommandIncreaseFontSizeBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandIncreaseFontSizeBarButtonItem1.Glyph")));
            this.diagramCommandIncreaseFontSizeBarButtonItem1.Id = 63;
            this.diagramCommandIncreaseFontSizeBarButtonItem1.Name = "diagramCommandIncreaseFontSizeBarButtonItem1";
            this.diagramCommandIncreaseFontSizeBarButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // diagramCommandDecreaseFontSizeBarButtonItem1
            // 
            this.diagramCommandDecreaseFontSizeBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandDecreaseFontSizeBarButtonItem1.Description = "Make your text a bit smaller.";
            this.diagramCommandDecreaseFontSizeBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandDecreaseFontSizeBarButtonItem1.Glyph")));
            this.diagramCommandDecreaseFontSizeBarButtonItem1.Id = 64;
            this.diagramCommandDecreaseFontSizeBarButtonItem1.Name = "diagramCommandDecreaseFontSizeBarButtonItem1";
            this.diagramCommandDecreaseFontSizeBarButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // barButtonGroup2
            // 
            this.barButtonGroup2.Id = 55;
            this.barButtonGroup2.ItemLinks.Add(this.diagramCommandToggleFontBoldBarCheckItem1);
            this.barButtonGroup2.ItemLinks.Add(this.diagramCommandToggleFontItalicBarCheckItem1);
            this.barButtonGroup2.ItemLinks.Add(this.diagramCommandToggleFontUnderlineBarCheckItem1);
            this.barButtonGroup2.ItemLinks.Add(this.diagramCommandToggleFontStrikethroughBarCheckItem1);
            this.barButtonGroup2.ItemLinks.Add(this.diagramCommandForegroundColorBarSplitButtonItem1);
            this.barButtonGroup2.Name = "barButtonGroup2";
            this.barButtonGroup2.Tag = "bgFontTypeAndColor";
            // 
            // diagramCommandToggleFontBoldBarCheckItem1
            // 
            this.diagramCommandToggleFontBoldBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandToggleFontBoldBarCheckItem1.Description = "Make your text bold.";
            this.diagramCommandToggleFontBoldBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandToggleFontBoldBarCheckItem1.Glyph")));
            this.diagramCommandToggleFontBoldBarCheckItem1.Id = 65;
            this.diagramCommandToggleFontBoldBarCheckItem1.Name = "diagramCommandToggleFontBoldBarCheckItem1";
            this.diagramCommandToggleFontBoldBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // diagramCommandToggleFontItalicBarCheckItem1
            // 
            this.diagramCommandToggleFontItalicBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandToggleFontItalicBarCheckItem1.Description = "Italicize your text.";
            this.diagramCommandToggleFontItalicBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandToggleFontItalicBarCheckItem1.Glyph")));
            this.diagramCommandToggleFontItalicBarCheckItem1.Id = 66;
            this.diagramCommandToggleFontItalicBarCheckItem1.Name = "diagramCommandToggleFontItalicBarCheckItem1";
            this.diagramCommandToggleFontItalicBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // diagramCommandToggleFontUnderlineBarCheckItem1
            // 
            this.diagramCommandToggleFontUnderlineBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandToggleFontUnderlineBarCheckItem1.Description = "Underline your text.";
            this.diagramCommandToggleFontUnderlineBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandToggleFontUnderlineBarCheckItem1.Glyph")));
            this.diagramCommandToggleFontUnderlineBarCheckItem1.Id = 67;
            this.diagramCommandToggleFontUnderlineBarCheckItem1.Name = "diagramCommandToggleFontUnderlineBarCheckItem1";
            this.diagramCommandToggleFontUnderlineBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // diagramCommandToggleFontStrikethroughBarCheckItem1
            // 
            this.diagramCommandToggleFontStrikethroughBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandToggleFontStrikethroughBarCheckItem1.Description = "Cross something out by drawing a line through it.";
            this.diagramCommandToggleFontStrikethroughBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandToggleFontStrikethroughBarCheckItem1.Glyph")));
            this.diagramCommandToggleFontStrikethroughBarCheckItem1.Id = 68;
            this.diagramCommandToggleFontStrikethroughBarCheckItem1.Name = "diagramCommandToggleFontStrikethroughBarCheckItem1";
            this.diagramCommandToggleFontStrikethroughBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // diagramCommandForegroundColorBarSplitButtonItem1
            // 
            this.diagramCommandForegroundColorBarSplitButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandForegroundColorBarSplitButtonItem1.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.diagramCommandForegroundColorBarSplitButtonItem1.Color = System.Drawing.Color.Empty;
            this.diagramCommandForegroundColorBarSplitButtonItem1.Description = "Change the color of your text.";
            this.diagramCommandForegroundColorBarSplitButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandForegroundColorBarSplitButtonItem1.Glyph")));
            this.diagramCommandForegroundColorBarSplitButtonItem1.Id = 69;
            this.diagramCommandForegroundColorBarSplitButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandForegroundColorBarSplitButtonItem1.LargeGlyph")));
            this.diagramCommandForegroundColorBarSplitButtonItem1.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.Default;
            this.diagramCommandForegroundColorBarSplitButtonItem1.Name = "diagramCommandForegroundColorBarSplitButtonItem1";
            this.diagramCommandForegroundColorBarSplitButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // barButtonGroup3
            // 
            this.barButtonGroup3.Id = 56;
            this.barButtonGroup3.ItemLinks.Add(this.diagramCommandSetVerticalAlignment_TopBarCheckItem1);
            this.barButtonGroup3.ItemLinks.Add(this.diagramCommandSetVerticalAlignment_CenterBarCheckItem1);
            this.barButtonGroup3.ItemLinks.Add(this.diagramCommandSetVerticalAlignment_BottomBarCheckItem1);
            this.barButtonGroup3.Name = "barButtonGroup3";
            this.barButtonGroup3.Tag = "bgVerticalTextAlignment";
            // 
            // diagramCommandSetVerticalAlignment_TopBarCheckItem1
            // 
            this.diagramCommandSetVerticalAlignment_TopBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSetVerticalAlignment_TopBarCheckItem1.Description = "Align text to the top of the text block.";
            this.diagramCommandSetVerticalAlignment_TopBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSetVerticalAlignment_TopBarCheckItem1.Glyph")));
            this.diagramCommandSetVerticalAlignment_TopBarCheckItem1.Id = 70;
            this.diagramCommandSetVerticalAlignment_TopBarCheckItem1.Name = "diagramCommandSetVerticalAlignment_TopBarCheckItem1";
            this.diagramCommandSetVerticalAlignment_TopBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // diagramCommandSetVerticalAlignment_CenterBarCheckItem1
            // 
            this.diagramCommandSetVerticalAlignment_CenterBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSetVerticalAlignment_CenterBarCheckItem1.Description = "Align text so that it is centered between the top and bottom of the text block.";
            this.diagramCommandSetVerticalAlignment_CenterBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSetVerticalAlignment_CenterBarCheckItem1.Glyph")));
            this.diagramCommandSetVerticalAlignment_CenterBarCheckItem1.Id = 71;
            this.diagramCommandSetVerticalAlignment_CenterBarCheckItem1.Name = "diagramCommandSetVerticalAlignment_CenterBarCheckItem1";
            this.diagramCommandSetVerticalAlignment_CenterBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // diagramCommandSetVerticalAlignment_BottomBarCheckItem1
            // 
            this.diagramCommandSetVerticalAlignment_BottomBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSetVerticalAlignment_BottomBarCheckItem1.Description = "Align text to the bottom of the text block.";
            this.diagramCommandSetVerticalAlignment_BottomBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSetVerticalAlignment_BottomBarCheckItem1.Glyph")));
            this.diagramCommandSetVerticalAlignment_BottomBarCheckItem1.Id = 72;
            this.diagramCommandSetVerticalAlignment_BottomBarCheckItem1.Name = "diagramCommandSetVerticalAlignment_BottomBarCheckItem1";
            this.diagramCommandSetVerticalAlignment_BottomBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // barButtonGroup4
            // 
            this.barButtonGroup4.Id = 57;
            this.barButtonGroup4.ItemLinks.Add(this.diagramCommandSetHorizontalAlignment_LeftBarCheckItem1);
            this.barButtonGroup4.ItemLinks.Add(this.diagramCommandSetHorizontalAlignment_CenterBarCheckItem1);
            this.barButtonGroup4.ItemLinks.Add(this.diagramCommandSetHorizontalAlignment_RightBarCheckItem1);
            this.barButtonGroup4.ItemLinks.Add(this.diagramCommandSetHorizontalAlignment_JustifyBarCheckItem1);
            this.barButtonGroup4.Name = "barButtonGroup4";
            this.barButtonGroup4.Tag = "bgHorizontalTextAlignment";
            // 
            // diagramCommandSetHorizontalAlignment_LeftBarCheckItem1
            // 
            this.diagramCommandSetHorizontalAlignment_LeftBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSetHorizontalAlignment_LeftBarCheckItem1.Description = "Align your content to the left.";
            this.diagramCommandSetHorizontalAlignment_LeftBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSetHorizontalAlignment_LeftBarCheckItem1.Glyph")));
            this.diagramCommandSetHorizontalAlignment_LeftBarCheckItem1.Id = 73;
            this.diagramCommandSetHorizontalAlignment_LeftBarCheckItem1.Name = "diagramCommandSetHorizontalAlignment_LeftBarCheckItem1";
            this.diagramCommandSetHorizontalAlignment_LeftBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // diagramCommandSetHorizontalAlignment_CenterBarCheckItem1
            // 
            this.diagramCommandSetHorizontalAlignment_CenterBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSetHorizontalAlignment_CenterBarCheckItem1.Description = "Center your content.";
            this.diagramCommandSetHorizontalAlignment_CenterBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSetHorizontalAlignment_CenterBarCheckItem1.Glyph")));
            this.diagramCommandSetHorizontalAlignment_CenterBarCheckItem1.Id = 74;
            this.diagramCommandSetHorizontalAlignment_CenterBarCheckItem1.Name = "diagramCommandSetHorizontalAlignment_CenterBarCheckItem1";
            this.diagramCommandSetHorizontalAlignment_CenterBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // diagramCommandSetHorizontalAlignment_RightBarCheckItem1
            // 
            this.diagramCommandSetHorizontalAlignment_RightBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSetHorizontalAlignment_RightBarCheckItem1.Description = "Align your content to the right.";
            this.diagramCommandSetHorizontalAlignment_RightBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSetHorizontalAlignment_RightBarCheckItem1.Glyph")));
            this.diagramCommandSetHorizontalAlignment_RightBarCheckItem1.Id = 75;
            this.diagramCommandSetHorizontalAlignment_RightBarCheckItem1.Name = "diagramCommandSetHorizontalAlignment_RightBarCheckItem1";
            this.diagramCommandSetHorizontalAlignment_RightBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // diagramCommandSetHorizontalAlignment_JustifyBarCheckItem1
            // 
            this.diagramCommandSetHorizontalAlignment_JustifyBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSetHorizontalAlignment_JustifyBarCheckItem1.Description = "Distribute your text evenly between the margins.";
            this.diagramCommandSetHorizontalAlignment_JustifyBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSetHorizontalAlignment_JustifyBarCheckItem1.Glyph")));
            this.diagramCommandSetHorizontalAlignment_JustifyBarCheckItem1.Id = 76;
            this.diagramCommandSetHorizontalAlignment_JustifyBarCheckItem1.Name = "diagramCommandSetHorizontalAlignment_JustifyBarCheckItem1";
            this.diagramCommandSetHorizontalAlignment_JustifyBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // diagramCommandSelectTool_PointerToolBarCheckItem1
            // 
            this.diagramCommandSelectTool_PointerToolBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSelectTool_PointerToolBarCheckItem1.Description = "Select, move, and resize objects.";
            this.diagramCommandSelectTool_PointerToolBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSelectTool_PointerToolBarCheckItem1.Glyph")));
            this.diagramCommandSelectTool_PointerToolBarCheckItem1.Id = 77;
            this.diagramCommandSelectTool_PointerToolBarCheckItem1.Name = "diagramCommandSelectTool_PointerToolBarCheckItem1";
            this.diagramCommandSelectTool_PointerToolBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // diagramCommandSelectTool_ConnectorToolBarCheckItem1
            // 
            this.diagramCommandSelectTool_ConnectorToolBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSelectTool_ConnectorToolBarCheckItem1.Description = "Draw connectors between objects.";
            this.diagramCommandSelectTool_ConnectorToolBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSelectTool_ConnectorToolBarCheckItem1.Glyph")));
            this.diagramCommandSelectTool_ConnectorToolBarCheckItem1.Id = 78;
            this.diagramCommandSelectTool_ConnectorToolBarCheckItem1.Name = "diagramCommandSelectTool_ConnectorToolBarCheckItem1";
            this.diagramCommandSelectTool_ConnectorToolBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // diagramCommandToolsContainerCheckDropDownItem1
            // 
            this.diagramCommandToolsContainerCheckDropDownItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandToolsContainerCheckDropDownItem1.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.CheckDropDown;
            this.diagramCommandToolsContainerCheckDropDownItem1.Caption = "Rectangle";
            this.diagramCommandToolsContainerCheckDropDownItem1.Description = "Drag to draw a rectangle.";
            this.diagramCommandToolsContainerCheckDropDownItem1.DropDownControl = this.popupMenu3;
            this.diagramCommandToolsContainerCheckDropDownItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandToolsContainerCheckDropDownItem1.Glyph")));
            this.diagramCommandToolsContainerCheckDropDownItem1.Id = 79;
            this.diagramCommandToolsContainerCheckDropDownItem1.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.SmallImagesText;
            this.diagramCommandToolsContainerCheckDropDownItem1.Name = "diagramCommandToolsContainerCheckDropDownItem1";
            this.diagramCommandToolsContainerCheckDropDownItem1.RememberLastCommand = true;
            this.diagramCommandToolsContainerCheckDropDownItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            toolTipTitleItem23.Text = "Rectangle (Ctrl+8)";
            toolTipItem3.Text = "Drag to draw a rectangle.";
            superToolTip23.Items.Add(toolTipTitleItem23);
            superToolTip23.Items.Add(toolTipItem3);
            this.diagramCommandToolsContainerCheckDropDownItem1.SuperTip = superToolTip23;
            // 
            // popupMenu3
            // 
            this.popupMenu3.ItemLinks.Add(this.diagramCommandSelectTool_RectangleToolBarCheckItem1);
            this.popupMenu3.ItemLinks.Add(this.diagramCommandSelectTool_EllipseToolBarCheckItem1);
            this.popupMenu3.ItemLinks.Add(this.diagramCommandSelectTool_RightTriangleToolBarCheckItem1);
            this.popupMenu3.ItemLinks.Add(this.diagramCommandSelectTool_HexagonToolBarCheckItem1);
            this.popupMenu3.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.SmallImagesText;
            this.popupMenu3.Name = "popupMenu3";
            this.popupMenu3.Ribbon = this.ribbonControl1;
            // 
            // diagramCommandSelectTool_RectangleToolBarCheckItem1
            // 
            this.diagramCommandSelectTool_RectangleToolBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSelectTool_RectangleToolBarCheckItem1.Description = "Drag to draw a rectangle.";
            this.diagramCommandSelectTool_RectangleToolBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSelectTool_RectangleToolBarCheckItem1.Glyph")));
            this.diagramCommandSelectTool_RectangleToolBarCheckItem1.Id = 80;
            this.diagramCommandSelectTool_RectangleToolBarCheckItem1.Name = "diagramCommandSelectTool_RectangleToolBarCheckItem1";
            this.diagramCommandSelectTool_RectangleToolBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // diagramCommandSelectTool_EllipseToolBarCheckItem1
            // 
            this.diagramCommandSelectTool_EllipseToolBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSelectTool_EllipseToolBarCheckItem1.Description = "Drag to draw an ellipse.";
            this.diagramCommandSelectTool_EllipseToolBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSelectTool_EllipseToolBarCheckItem1.Glyph")));
            this.diagramCommandSelectTool_EllipseToolBarCheckItem1.Id = 81;
            this.diagramCommandSelectTool_EllipseToolBarCheckItem1.Name = "diagramCommandSelectTool_EllipseToolBarCheckItem1";
            this.diagramCommandSelectTool_EllipseToolBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // diagramCommandSelectTool_RightTriangleToolBarCheckItem1
            // 
            this.diagramCommandSelectTool_RightTriangleToolBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSelectTool_RightTriangleToolBarCheckItem1.Description = "Drag to draw a right triangle.";
            this.diagramCommandSelectTool_RightTriangleToolBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSelectTool_RightTriangleToolBarCheckItem1.Glyph")));
            this.diagramCommandSelectTool_RightTriangleToolBarCheckItem1.Id = 82;
            this.diagramCommandSelectTool_RightTriangleToolBarCheckItem1.Name = "diagramCommandSelectTool_RightTriangleToolBarCheckItem1";
            this.diagramCommandSelectTool_RightTriangleToolBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // diagramCommandSelectTool_HexagonToolBarCheckItem1
            // 
            this.diagramCommandSelectTool_HexagonToolBarCheckItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSelectTool_HexagonToolBarCheckItem1.Description = "Drag to draw a hexagon.";
            this.diagramCommandSelectTool_HexagonToolBarCheckItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSelectTool_HexagonToolBarCheckItem1.Glyph")));
            this.diagramCommandSelectTool_HexagonToolBarCheckItem1.Id = 83;
            this.diagramCommandSelectTool_HexagonToolBarCheckItem1.Name = "diagramCommandSelectTool_HexagonToolBarCheckItem1";
            this.diagramCommandSelectTool_HexagonToolBarCheckItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // diagramCommandShapeStylesBarGalleryItem1
            // 
            // 
            // 
            // 
            this.diagramCommandShapeStylesBarGalleryItem1.Gallery.ColumnCount = 7;
            galleryItemGroup37.Caption = "Variant Styles";
            galleryItemGroup37.Tag = "Variant Styles";
            galleryItemGroup38.Caption = "Theme Styles";
            galleryItemGroup38.Tag = "Theme Styles";
            this.diagramCommandShapeStylesBarGalleryItem1.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            galleryItemGroup37,
            galleryItemGroup38});
            this.diagramCommandShapeStylesBarGalleryItem1.Gallery.ImageSize = new System.Drawing.Size(65, 46);
            this.diagramCommandShapeStylesBarGalleryItem1.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.SingleRadio;
            this.diagramCommandShapeStylesBarGalleryItem1.Id = 84;
            this.diagramCommandShapeStylesBarGalleryItem1.Name = "diagramCommandShapeStylesBarGalleryItem1";
            // 
            // diagramCommandBackgroundColorBarSplitButtonItem1
            // 
            this.diagramCommandBackgroundColorBarSplitButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandBackgroundColorBarSplitButtonItem1.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.diagramCommandBackgroundColorBarSplitButtonItem1.Color = System.Drawing.Color.Empty;
            this.diagramCommandBackgroundColorBarSplitButtonItem1.Description = "Change the background color.";
            this.diagramCommandBackgroundColorBarSplitButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandBackgroundColorBarSplitButtonItem1.Glyph")));
            this.diagramCommandBackgroundColorBarSplitButtonItem1.Id = 85;
            this.diagramCommandBackgroundColorBarSplitButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandBackgroundColorBarSplitButtonItem1.LargeGlyph")));
            this.diagramCommandBackgroundColorBarSplitButtonItem1.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.Default;
            this.diagramCommandBackgroundColorBarSplitButtonItem1.Name = "diagramCommandBackgroundColorBarSplitButtonItem1";
            this.diagramCommandBackgroundColorBarSplitButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // diagramCommandStrokeColorBarSplitButtonItem1
            // 
            this.diagramCommandStrokeColorBarSplitButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandStrokeColorBarSplitButtonItem1.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.diagramCommandStrokeColorBarSplitButtonItem1.Color = System.Drawing.Color.Empty;
            this.diagramCommandStrokeColorBarSplitButtonItem1.Description = "Change the stroke color.";
            this.diagramCommandStrokeColorBarSplitButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandStrokeColorBarSplitButtonItem1.Glyph")));
            this.diagramCommandStrokeColorBarSplitButtonItem1.Id = 86;
            this.diagramCommandStrokeColorBarSplitButtonItem1.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandStrokeColorBarSplitButtonItem1.LargeGlyph")));
            this.diagramCommandStrokeColorBarSplitButtonItem1.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.Default;
            this.diagramCommandStrokeColorBarSplitButtonItem1.Name = "diagramCommandStrokeColorBarSplitButtonItem1";
            this.diagramCommandStrokeColorBarSplitButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            // 
            // diagramCommandBringToFrontBarSplitButtonItem1
            // 
            this.diagramCommandBringToFrontBarSplitButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandBringToFrontBarSplitButtonItem1.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.diagramCommandBringToFrontBarSplitButtonItem1.Description = "Bring the selected object in front of all other objects.";
            this.diagramCommandBringToFrontBarSplitButtonItem1.DropDownControl = this.popupMenu4;
            this.diagramCommandBringToFrontBarSplitButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandBringToFrontBarSplitButtonItem1.Glyph")));
            this.diagramCommandBringToFrontBarSplitButtonItem1.Id = 87;
            this.diagramCommandBringToFrontBarSplitButtonItem1.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.SmallImagesText;
            this.diagramCommandBringToFrontBarSplitButtonItem1.Name = "diagramCommandBringToFrontBarSplitButtonItem1";
            this.diagramCommandBringToFrontBarSplitButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // popupMenu4
            // 
            this.popupMenu4.ItemLinks.Add(this.diagramCommandBringForwardBarButtonItem1);
            this.popupMenu4.ItemLinks.Add(this.diagramCommandBringToFrontBarButtonItem1);
            this.popupMenu4.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.SmallImagesText;
            this.popupMenu4.Name = "popupMenu4";
            this.popupMenu4.Ribbon = this.ribbonControl1;
            // 
            // diagramCommandBringForwardBarButtonItem1
            // 
            this.diagramCommandBringForwardBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandBringForwardBarButtonItem1.Description = "Bring the selected object forward one level so that it\'s hidden behind fewer obje" +
    "cts";
            this.diagramCommandBringForwardBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandBringForwardBarButtonItem1.Glyph")));
            this.diagramCommandBringForwardBarButtonItem1.Id = 89;
            this.diagramCommandBringForwardBarButtonItem1.Name = "diagramCommandBringForwardBarButtonItem1";
            // 
            // diagramCommandBringToFrontBarButtonItem1
            // 
            this.diagramCommandBringToFrontBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandBringToFrontBarButtonItem1.Description = "Bring the selected object in front of all other objects.";
            this.diagramCommandBringToFrontBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandBringToFrontBarButtonItem1.Glyph")));
            this.diagramCommandBringToFrontBarButtonItem1.Id = 90;
            this.diagramCommandBringToFrontBarButtonItem1.Name = "diagramCommandBringToFrontBarButtonItem1";
            // 
            // diagramCommandSendToBackBarSplitButtonItem1
            // 
            this.diagramCommandSendToBackBarSplitButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSendToBackBarSplitButtonItem1.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.diagramCommandSendToBackBarSplitButtonItem1.Description = "Send the selected object behind all other objects.";
            this.diagramCommandSendToBackBarSplitButtonItem1.DropDownControl = this.popupMenu5;
            this.diagramCommandSendToBackBarSplitButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSendToBackBarSplitButtonItem1.Glyph")));
            this.diagramCommandSendToBackBarSplitButtonItem1.Id = 88;
            this.diagramCommandSendToBackBarSplitButtonItem1.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.SmallImagesText;
            this.diagramCommandSendToBackBarSplitButtonItem1.Name = "diagramCommandSendToBackBarSplitButtonItem1";
            this.diagramCommandSendToBackBarSplitButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // popupMenu5
            // 
            this.popupMenu5.ItemLinks.Add(this.diagramCommandSendBackwardBarButtonItem1);
            this.popupMenu5.ItemLinks.Add(this.diagramCommandSendToBackBarButtonItem1);
            this.popupMenu5.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.SmallImagesText;
            this.popupMenu5.Name = "popupMenu5";
            this.popupMenu5.Ribbon = this.ribbonControl1;
            // 
            // diagramCommandSendBackwardBarButtonItem1
            // 
            this.diagramCommandSendBackwardBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSendBackwardBarButtonItem1.Description = "Send the selected object back one level so that it\'s hidden behind more objects.";
            this.diagramCommandSendBackwardBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSendBackwardBarButtonItem1.Glyph")));
            this.diagramCommandSendBackwardBarButtonItem1.Id = 91;
            this.diagramCommandSendBackwardBarButtonItem1.Name = "diagramCommandSendBackwardBarButtonItem1";
            // 
            // diagramCommandSendToBackBarButtonItem1
            // 
            this.diagramCommandSendToBackBarButtonItem1.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.diagramCommandSendToBackBarButtonItem1.Description = "Send the selected object behind all other objects.";
            this.diagramCommandSendToBackBarButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("diagramCommandSendToBackBarButtonItem1.Glyph")));
            this.diagramCommandSendToBackBarButtonItem1.Id = 92;
            this.diagramCommandSendToBackBarButtonItem1.Name = "diagramCommandSendToBackBarButtonItem1";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 1;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // diagramHomeRibbonPage1
            // 
            this.diagramHomeRibbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.diagramClipboardRibbonPageGroup1,
            this.diagramFontRibbonPageGroup1,
            this.diagramParagraphRibbonPageGroup1,
            this.diagramToolsRibbonPageGroup1,
            this.diagramShapeStylesRibbonPageGroup1,
            this.diagramArrangeRibbonPageGroup1});
            this.diagramHomeRibbonPage1.Name = "diagramHomeRibbonPage1";
            // 
            // diagramClipboardRibbonPageGroup1
            // 
            this.diagramClipboardRibbonPageGroup1.ItemLinks.Add(this.diagramCommandPasteBarButtonItem1);
            this.diagramClipboardRibbonPageGroup1.ItemLinks.Add(this.diagramCommandCutBarButtonItem1);
            this.diagramClipboardRibbonPageGroup1.ItemLinks.Add(this.diagramCommandCopyBarButtonItem1);
            this.diagramClipboardRibbonPageGroup1.Name = "diagramClipboardRibbonPageGroup1";
            // 
            // diagramFontRibbonPageGroup1
            // 
            this.diagramFontRibbonPageGroup1.ItemLinks.Add(this.barButtonGroup1);
            this.diagramFontRibbonPageGroup1.ItemLinks.Add(this.barButtonGroup2);
            this.diagramFontRibbonPageGroup1.Name = "diagramFontRibbonPageGroup1";
            // 
            // diagramParagraphRibbonPageGroup1
            // 
            this.diagramParagraphRibbonPageGroup1.ItemLinks.Add(this.barButtonGroup3);
            this.diagramParagraphRibbonPageGroup1.ItemLinks.Add(this.barButtonGroup4);
            this.diagramParagraphRibbonPageGroup1.Name = "diagramParagraphRibbonPageGroup1";
            // 
            // diagramToolsRibbonPageGroup1
            // 
            this.diagramToolsRibbonPageGroup1.ItemLinks.Add(this.diagramCommandSelectTool_PointerToolBarCheckItem1);
            this.diagramToolsRibbonPageGroup1.Name = "diagramToolsRibbonPageGroup1";
            // 
            // diagramShapeStylesRibbonPageGroup1
            // 
            this.diagramShapeStylesRibbonPageGroup1.ItemLinks.Add(this.diagramCommandShapeStylesBarGalleryItem1);
            this.diagramShapeStylesRibbonPageGroup1.ItemLinks.Add(this.diagramCommandBackgroundColorBarSplitButtonItem1);
            this.diagramShapeStylesRibbonPageGroup1.ItemLinks.Add(this.diagramCommandStrokeColorBarSplitButtonItem1);
            this.diagramShapeStylesRibbonPageGroup1.Name = "diagramShapeStylesRibbonPageGroup1";
            // 
            // diagramArrangeRibbonPageGroup1
            // 
            this.diagramArrangeRibbonPageGroup1.ItemLinks.Add(this.diagramCommandBringToFrontBarSplitButtonItem1);
            this.diagramArrangeRibbonPageGroup1.ItemLinks.Add(this.diagramCommandSendToBackBarSplitButtonItem1);
            this.diagramArrangeRibbonPageGroup1.Name = "diagramArrangeRibbonPageGroup1";
            // 
            // diagramViewRibbonPage1
            // 
            this.diagramViewRibbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.diagramShowRibbonPageGroup1,
            this.diagramZoomRibbonPageGroup1});
            this.diagramViewRibbonPage1.Name = "diagramViewRibbonPage1";
            // 
            // diagramShowRibbonPageGroup1
            // 
            this.diagramShowRibbonPageGroup1.ItemLinks.Add(this.diagramCommandShowRulersBarCheckItem1);
            this.diagramShowRibbonPageGroup1.ItemLinks.Add(this.diagramCommandShowGridBarCheckItem1);
            this.diagramShowRibbonPageGroup1.ItemLinks.Add(this.diagramCommandShowPageBreaksBarCheckItem1);
            this.diagramShowRibbonPageGroup1.Name = "diagramShowRibbonPageGroup1";
            // 
            // diagramZoomRibbonPageGroup1
            // 
            this.diagramZoomRibbonPageGroup1.ItemLinks.Add(this.diagramCommandFitToPageBarButtonItem1);
            this.diagramZoomRibbonPageGroup1.ItemLinks.Add(this.diagramCommandFitToWidthBarButtonItem1);
            this.diagramZoomRibbonPageGroup1.Name = "diagramZoomRibbonPageGroup1";
            // 
            // diagramDesignRibbonPage1
            // 
            this.diagramDesignRibbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.diagramPageSetupRibbonPageGroup1,
            this.diagramThemesRibbonPageGroup1,
            this.diagramOptionsRibbonPageGroup1,
            this.diagramTreeLayoutRibbonPageGroup1});
            this.diagramDesignRibbonPage1.Name = "diagramDesignRibbonPage1";
            // 
            // diagramPageSetupRibbonPageGroup1
            // 
            this.diagramPageSetupRibbonPageGroup1.ItemLinks.Add(this.diagramCommandPageOrientationBarDropDownItem1);
            this.diagramPageSetupRibbonPageGroup1.ItemLinks.Add(this.diagramCommandPageSizeBarDropDownItem1);
            this.diagramPageSetupRibbonPageGroup1.ItemLinks.Add(this.diagramCommandAutoSizeBarDropDownItem1);
            this.diagramPageSetupRibbonPageGroup1.Name = "diagramPageSetupRibbonPageGroup1";
            // 
            // diagramThemesRibbonPageGroup1
            // 
            this.diagramThemesRibbonPageGroup1.ItemLinks.Add(this.diagramCommandThemesBarGalleryItem1);
            this.diagramThemesRibbonPageGroup1.Name = "diagramThemesRibbonPageGroup1";
            // 
            // diagramOptionsRibbonPageGroup1
            // 
            this.diagramOptionsRibbonPageGroup1.ItemLinks.Add(this.diagramCommandSnapToItemsBarCheckItem1);
            this.diagramOptionsRibbonPageGroup1.ItemLinks.Add(this.diagramCommandSnapToGridBarCheckItem1);
            this.diagramOptionsRibbonPageGroup1.Name = "diagramOptionsRibbonPageGroup1";
            // 
            // diagramTreeLayoutRibbonPageGroup1
            // 
            this.diagramTreeLayoutRibbonPageGroup1.ItemLinks.Add(this.diagramCommandReLayoutBarDropDownItem1);
            this.diagramTreeLayoutRibbonPageGroup1.Name = "diagramTreeLayoutRibbonPageGroup1";
            // 
            // diagramBarController1
            // 
            this.diagramBarController1.BarItems.Add(this.diagramCommandNewFileBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandOpenFileBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSaveFileBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandPrintMenuBarSplitButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandExportAsBarSplitButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandUndoBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandRedoBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandPrintBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandQuickPrintBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandExportDiagram_PNGBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandExportDiagram_JPEGBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandExportDiagram_BMPBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandExportDiagram_GIFBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandPageOrientationBarDropDownItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandPageSizeBarDropDownItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandAutoSizeBarDropDownItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandPageOrientation_HorizontalBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandPageOrientation_VerticalBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandPageSize_LetterBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandPageSize_TabloidBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandPageSize_LegalBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandPageSize_StatementBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandPageSize_ExecutiveBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandPageSize_A3BarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandPageSize_A4BarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandPageSize_A5BarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandPageSize_B4BarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandPageSize_B5BarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandFitToDrawingBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSetPageParameters_PageSizeBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandAutoSize_NoneBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandAutoSize_AutoSizeBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandAutoSize_FillBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandThemesBarGalleryItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSnapToItemsBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSnapToGridBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandReLayoutBarDropDownItem1);
            this.diagramBarController1.BarItems.Add(this.diagramReLayoutTreeBarHeaderItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandTreeLayout_DownBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandTreeLayout_UpBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandTreeLayout_RightBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandTreeLayout_LeftBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramReLayoutSugiyamaBarHeaderItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSugiyamaLayout_DownBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSugiyamaLayout_UpBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSugiyamaLayout_RightBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSugiyamaLayout_LeftBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandShowRulersBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandShowGridBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandShowPageBreaksBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandFitToPageBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandFitToWidthBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandPasteBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandCutBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandCopyBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandFontFamilyBarEditItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandFontSizeBarEditItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandIncreaseFontSizeBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandDecreaseFontSizeBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandToggleFontBoldBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandToggleFontItalicBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandToggleFontUnderlineBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandToggleFontStrikethroughBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandForegroundColorBarSplitButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSetVerticalAlignment_TopBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSetVerticalAlignment_CenterBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSetVerticalAlignment_BottomBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSetHorizontalAlignment_LeftBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSetHorizontalAlignment_CenterBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSetHorizontalAlignment_RightBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSetHorizontalAlignment_JustifyBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSelectTool_PointerToolBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSelectTool_ConnectorToolBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandToolsContainerCheckDropDownItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSelectTool_RectangleToolBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSelectTool_EllipseToolBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSelectTool_RightTriangleToolBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSelectTool_HexagonToolBarCheckItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandShapeStylesBarGalleryItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandBackgroundColorBarSplitButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandStrokeColorBarSplitButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandBringToFrontBarSplitButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSendToBackBarSplitButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandBringForwardBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandBringToFrontBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSendBackwardBarButtonItem1);
            this.diagramBarController1.BarItems.Add(this.diagramCommandSendToBackBarButtonItem1);
            this.diagramBarController1.Control = this.diagramControl1;
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.panelContainer1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl"});
            // 
            // panelContainer1
            // 
            this.panelContainer1.Controls.Add(this.dockPanel1);
            this.panelContainer1.Controls.Add(this.dockPanel2);
            this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.panelContainer1.ID = new System.Guid("6804f263-6d0b-4a65-a004-f1215503575b");
            this.panelContainer1.Location = new System.Drawing.Point(1068, 141);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.OriginalSize = new System.Drawing.Size(319, 200);
            this.panelContainer1.Size = new System.Drawing.Size(319, 655);
            this.panelContainer1.Text = "panelContainer1";
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel1.FloatVertical = true;
            this.dockPanel1.ID = new System.Guid("886271f9-19d5-40a1-8c0d-a8e930506a69");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(272, 304);
            this.dockPanel1.Size = new System.Drawing.Size(319, 304);
            this.dockPanel1.Text = "Tham số";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.layoutControl1);
            this.dockPanel1_Container.Controls.Add(this.ParamLayout);
            this.dockPanel1_Container.Location = new System.Drawing.Point(5, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(310, 276);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // layoutControl1
            // 
            this.layoutControl1.AutoScroll = false;
            this.layoutControl1.Controls.Add(this.btnLoadData);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.layoutControl1.Location = new System.Drawing.Point(0, 250);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1579, 211, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup2;
            this.layoutControl1.Size = new System.Drawing.Size(310, 26);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(101, 2);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(117, 22);
            this.btnLoadData.StyleController = this.layoutControl1;
            this.btnLoadData.TabIndex = 5;
            this.btnLoadData.Text = "Vẽ sơ đồ";
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.emptySpaceItem4});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "Root";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(310, 26);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnLoadData;
            this.layoutControlItem2.Location = new System.Drawing.Point(99, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(121, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(99, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.Location = new System.Drawing.Point(220, 0);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(90, 26);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ParamLayout
            // 
            this.ParamLayout.Controls.Add(this.buttonEdit1);
            this.ParamLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParamLayout.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.ParamLayout.Location = new System.Drawing.Point(0, 0);
            this.ParamLayout.Name = "ParamLayout";
            this.ParamLayout.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(459, 294, 430, 471);
            this.ParamLayout.Root = this.layoutControlGroup1;
            this.ParamLayout.Size = new System.Drawing.Size(310, 276);
            this.ParamLayout.TabIndex = 0;
            this.ParamLayout.Text = "layoutControl1";
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.Location = new System.Drawing.Point(109, 247);
            this.buttonEdit1.MenuManager = this.ribbonControl1;
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEdit1.Size = new System.Drawing.Size(115, 20);
            this.buttonEdit1.StyleController = this.ParamLayout;
            this.buttonEdit1.TabIndex = 6;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.buttonEdit1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 235);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(216, 24);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(50, 20);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(310, 276);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // dockPanel2
            // 
            this.dockPanel2.Controls.Add(this.dockPanel2_Container);
            this.dockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel2.ID = new System.Guid("cb7cf7fb-c936-4259-8a42-7e8946de99c2");
            this.dockPanel2.Location = new System.Drawing.Point(0, 304);
            this.dockPanel2.Name = "dockPanel2";
            this.dockPanel2.OriginalSize = new System.Drawing.Size(272, 351);
            this.dockPanel2.Size = new System.Drawing.Size(319, 351);
            this.dockPanel2.Text = "Thuộc tính";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.propertyGridControl1);
            this.dockPanel2_Container.Location = new System.Drawing.Point(5, 23);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(310, 324);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // popupMenu6
            // 
            this.popupMenu6.Name = "popupMenu6";
            this.popupMenu6.Ribbon = this.ribbonControl1;
            // 
            // frmRelaConect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.diagramControl1);
            this.Controls.Add(this.panelContainer1);
            this.Controls.Add(this.ribbonControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmRelaConect";
            this.Size = new System.Drawing.Size(1387, 796);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.applicationMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemFontEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDiagramFontSizeEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diagramBarController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.panelContainer1.ResumeLayout(false);
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParamLayout)).EndInit();
            this.ParamLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            this.dockPanel2.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraDiagram.DiagramControl diagramControl1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu applicationMenu1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandNewFileBarButtonItem diagramCommandNewFileBarButtonItem1;
        private DevExpress.XtraBars.BarButtonItem diagramCommandOpenFileBarButtonItem1;
        private DevExpress.XtraBars.BarButtonItem diagramCommandSaveFileBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandPrintMenuBarSplitButtonItem diagramCommandPrintMenuBarSplitButtonItem1;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandPrintBarButtonItem diagramCommandPrintBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandQuickPrintBarButtonItem diagramCommandQuickPrintBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandExportAsBarSplitButtonItem diagramCommandExportAsBarSplitButtonItem1;
        private DevExpress.XtraBars.PopupMenu popupMenu2;
        private DevExpress.XtraDiagram.Bars.DiagramCommandExportDiagram_PNGBarButtonItem diagramCommandExportDiagram_PNGBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandExportDiagram_JPEGBarButtonItem diagramCommandExportDiagram_JPEGBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandExportDiagram_BMPBarButtonItem diagramCommandExportDiagram_BMPBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandExportDiagram_GIFBarButtonItem diagramCommandExportDiagram_GIFBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandUndoBarButtonItem diagramCommandUndoBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandRedoBarButtonItem diagramCommandRedoBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandPageOrientationBarDropDownItem diagramCommandPageOrientationBarDropDownItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandPageOrientation_HorizontalBarCheckItem diagramCommandPageOrientation_HorizontalBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandPageOrientation_VerticalBarCheckItem diagramCommandPageOrientation_VerticalBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandPageSizeBarDropDownItem diagramCommandPageSizeBarDropDownItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_LetterBarCheckItem diagramCommandPageSize_LetterBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_TabloidBarCheckItem diagramCommandPageSize_TabloidBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_LegalBarCheckItem diagramCommandPageSize_LegalBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_StatementBarCheckItem diagramCommandPageSize_StatementBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_ExecutiveBarCheckItem diagramCommandPageSize_ExecutiveBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_A3BarCheckItem diagramCommandPageSize_A3BarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_A4BarCheckItem diagramCommandPageSize_A4BarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_A5BarCheckItem diagramCommandPageSize_A5BarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_B4BarCheckItem diagramCommandPageSize_B4BarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandPageSize_B5BarCheckItem diagramCommandPageSize_B5BarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandFitToDrawingBarButtonItem diagramCommandFitToDrawingBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSetPageParameters_PageSizeBarButtonItem diagramCommandSetPageParameters_PageSizeBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandAutoSizeBarDropDownItem diagramCommandAutoSizeBarDropDownItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandAutoSize_NoneBarCheckItem diagramCommandAutoSize_NoneBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandAutoSize_AutoSizeBarCheckItem diagramCommandAutoSize_AutoSizeBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandAutoSize_FillBarCheckItem diagramCommandAutoSize_FillBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandThemesBarGalleryItem diagramCommandThemesBarGalleryItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSnapToItemsBarCheckItem diagramCommandSnapToItemsBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSnapToGridBarCheckItem diagramCommandSnapToGridBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandReLayoutBarDropDownItem diagramCommandReLayoutBarDropDownItem1;
        private DevExpress.XtraDiagram.Bars.DiagramReLayoutTreeBarHeaderItem diagramReLayoutTreeBarHeaderItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandTreeLayout_DownBarButtonItem diagramCommandTreeLayout_DownBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandTreeLayout_UpBarButtonItem diagramCommandTreeLayout_UpBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandTreeLayout_RightBarButtonItem diagramCommandTreeLayout_RightBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandTreeLayout_LeftBarButtonItem diagramCommandTreeLayout_LeftBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramReLayoutSugiyamaBarHeaderItem diagramReLayoutSugiyamaBarHeaderItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSugiyamaLayout_DownBarButtonItem diagramCommandSugiyamaLayout_DownBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSugiyamaLayout_UpBarButtonItem diagramCommandSugiyamaLayout_UpBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSugiyamaLayout_RightBarButtonItem diagramCommandSugiyamaLayout_RightBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSugiyamaLayout_LeftBarButtonItem diagramCommandSugiyamaLayout_LeftBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandShowRulersBarCheckItem diagramCommandShowRulersBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandShowGridBarCheckItem diagramCommandShowGridBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandShowPageBreaksBarCheckItem diagramCommandShowPageBreaksBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandFitToPageBarButtonItem diagramCommandFitToPageBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandFitToWidthBarButtonItem diagramCommandFitToWidthBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandPasteBarButtonItem diagramCommandPasteBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandCutBarButtonItem diagramCommandCutBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandCopyBarButtonItem diagramCommandCopyBarButtonItem1;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandFontFamilyBarEditItem diagramCommandFontFamilyBarEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemFontEdit repositoryItemFontEdit1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandFontSizeBarEditItem diagramCommandFontSizeBarEditItem1;
        private DevExpress.XtraDiagram.Bars.RepositoryItemDiagramFontSizeEdit repositoryItemDiagramFontSizeEdit1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandIncreaseFontSizeBarButtonItem diagramCommandIncreaseFontSizeBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandDecreaseFontSizeBarButtonItem diagramCommandDecreaseFontSizeBarButtonItem1;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup2;
        private DevExpress.XtraDiagram.Bars.DiagramCommandToggleFontBoldBarCheckItem diagramCommandToggleFontBoldBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandToggleFontItalicBarCheckItem diagramCommandToggleFontItalicBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandToggleFontUnderlineBarCheckItem diagramCommandToggleFontUnderlineBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandToggleFontStrikethroughBarCheckItem diagramCommandToggleFontStrikethroughBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandForegroundColorBarSplitButtonItem diagramCommandForegroundColorBarSplitButtonItem1;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup3;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSetVerticalAlignment_TopBarCheckItem diagramCommandSetVerticalAlignment_TopBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSetVerticalAlignment_CenterBarCheckItem diagramCommandSetVerticalAlignment_CenterBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSetVerticalAlignment_BottomBarCheckItem diagramCommandSetVerticalAlignment_BottomBarCheckItem1;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup4;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSetHorizontalAlignment_LeftBarCheckItem diagramCommandSetHorizontalAlignment_LeftBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSetHorizontalAlignment_CenterBarCheckItem diagramCommandSetHorizontalAlignment_CenterBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSetHorizontalAlignment_RightBarCheckItem diagramCommandSetHorizontalAlignment_RightBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSetHorizontalAlignment_JustifyBarCheckItem diagramCommandSetHorizontalAlignment_JustifyBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSelectTool_PointerToolBarCheckItem diagramCommandSelectTool_PointerToolBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSelectTool_ConnectorToolBarCheckItem diagramCommandSelectTool_ConnectorToolBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandToolsContainerCheckDropDownItem diagramCommandToolsContainerCheckDropDownItem1;
        private DevExpress.XtraBars.PopupMenu popupMenu3;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSelectTool_RectangleToolBarCheckItem diagramCommandSelectTool_RectangleToolBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSelectTool_EllipseToolBarCheckItem diagramCommandSelectTool_EllipseToolBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSelectTool_RightTriangleToolBarCheckItem diagramCommandSelectTool_RightTriangleToolBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSelectTool_HexagonToolBarCheckItem diagramCommandSelectTool_HexagonToolBarCheckItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandShapeStylesBarGalleryItem diagramCommandShapeStylesBarGalleryItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandBackgroundColorBarSplitButtonItem diagramCommandBackgroundColorBarSplitButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandStrokeColorBarSplitButtonItem diagramCommandStrokeColorBarSplitButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandBringToFrontBarSplitButtonItem diagramCommandBringToFrontBarSplitButtonItem1;
        private DevExpress.XtraBars.PopupMenu popupMenu4;
        private DevExpress.XtraDiagram.Bars.DiagramCommandBringForwardBarButtonItem diagramCommandBringForwardBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandBringToFrontBarButtonItem diagramCommandBringToFrontBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSendToBackBarSplitButtonItem diagramCommandSendToBackBarSplitButtonItem1;
        private DevExpress.XtraBars.PopupMenu popupMenu5;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSendBackwardBarButtonItem diagramCommandSendBackwardBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramCommandSendToBackBarButtonItem diagramCommandSendToBackBarButtonItem1;
        private DevExpress.XtraDiagram.Bars.DiagramHomeRibbonPage diagramHomeRibbonPage1;
        private DevExpress.XtraDiagram.Bars.DiagramClipboardRibbonPageGroup diagramClipboardRibbonPageGroup1;
        private DevExpress.XtraDiagram.Bars.DiagramFontRibbonPageGroup diagramFontRibbonPageGroup1;
        private DevExpress.XtraDiagram.Bars.DiagramParagraphRibbonPageGroup diagramParagraphRibbonPageGroup1;
        private DevExpress.XtraDiagram.Bars.DiagramToolsRibbonPageGroup diagramToolsRibbonPageGroup1;
        private DevExpress.XtraDiagram.Bars.DiagramShapeStylesRibbonPageGroup diagramShapeStylesRibbonPageGroup1;
        private DevExpress.XtraDiagram.Bars.DiagramArrangeRibbonPageGroup diagramArrangeRibbonPageGroup1;
        private DevExpress.XtraDiagram.Bars.DiagramViewRibbonPage diagramViewRibbonPage1;
        private DevExpress.XtraDiagram.Bars.DiagramShowRibbonPageGroup diagramShowRibbonPageGroup1;
        private DevExpress.XtraDiagram.Bars.DiagramZoomRibbonPageGroup diagramZoomRibbonPageGroup1;
        private DevExpress.XtraDiagram.Bars.DiagramDesignRibbonPage diagramDesignRibbonPage1;
        private DevExpress.XtraDiagram.Bars.DiagramPageSetupRibbonPageGroup diagramPageSetupRibbonPageGroup1;
        private DevExpress.XtraDiagram.Bars.DiagramThemesRibbonPageGroup diagramThemesRibbonPageGroup1;
        private DevExpress.XtraDiagram.Bars.DiagramOptionsRibbonPageGroup diagramOptionsRibbonPageGroup1;
        private DevExpress.XtraDiagram.Bars.DiagramTreeLayoutRibbonPageGroup diagramTreeLayoutRibbonPageGroup1;
        private DevExpress.XtraDiagram.Bars.DiagramBarController diagramBarController1;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControl1;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraLayout.LayoutControl ParamLayout;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
        private DevExpress.XtraEditors.SimpleButton btnLoadData;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel2;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.PopupMenu popupMenu6;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.Utils.ToolTipController toolTipController1;
    }
}
