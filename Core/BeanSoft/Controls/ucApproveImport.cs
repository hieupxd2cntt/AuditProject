using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;
using Core.Utils;
using Core.Controllers;
using Core.Entities;
using DevExpress.XtraEditors.Controls;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.Data;
using System.Xml.Serialization;
using System.IO;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid;
using AppClient.Interface;
using Core.Base;
using Core.Extensions;
using Core.Common;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.StyleFormatConditions;

namespace AppClient.Controls
{
    public partial class ucApproveImport : ucModule, IParameterFieldSupportedModule
    {
        public int moduleid; 
        public ucApproveImport()
        {
            InitializeComponent();
        }

    }   
}
