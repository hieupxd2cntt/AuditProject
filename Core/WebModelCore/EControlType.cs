using System.ComponentModel;

namespace WebModelCore
{
    public enum EControlType
    {
        [Description("Combobox")]
        CB,
        [Description("Textbox")]
        TB,
        [Description("TextArea")]
        TA,
        [Description("DatePicker")]
        DT,
        [Description("Upload")]
        UL,
        [Description("Slide")]
        SL,
        [Description("Barcode")]
        BC,
        [Description("Schedule")]
        SCL,
        [Description("TAB")]
        TAB,
        [Description("GRID EDIT")]
        GE,
        [Description("GRID View")]
        GV,
        [Description("EDITOR")]
        EDT,
        [Description("CheckBox")]
        CK,
        [Description("ComboboxCheck")]
        CBC,
        [Description("RadioGroup")]
        RG,
        [Description("LookupValues")]
        LV,
    }
}
