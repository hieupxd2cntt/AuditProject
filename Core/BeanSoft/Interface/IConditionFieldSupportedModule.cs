﻿using DevExpress.XtraLayout;
using AppClient.Controls;

namespace AppClient.Interface
{
    public interface IConditionFieldSupportedModule
    {
        /// <summary>
        /// Layout Group sử dụng cho các field Condition
        /// </summary>
        LayoutControlGroup ConditionLayoutGroup { get; }
        /// <summary>
        /// Nhóm gốc của các điều kiện tìm kiếm
        /// </summary>
        UISearchGroup RootGroup { get; set; }

        void UpdateConditionQuery();
    }
}
