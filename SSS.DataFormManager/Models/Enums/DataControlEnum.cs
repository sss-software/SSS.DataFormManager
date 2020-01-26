using System.ComponentModel;

public enum DataControlEnum
{
    [Description("Label")]
    Label = 1,

    [Description("Text Block")]
    TextBlock = 2,

    [Description("Text Box")]
    TextBox = 3,

    [Description("Calendar")]
    Calendar = 4,

    [Description("List Box")]
    ListBox = 5,

    [Description("Radio Buttons")]
    RadioButtons = 6,

    [Description("Check Box")]
    CheckBox = 7,

    [Description("Data Grid")]
    DataGrid = 8,

    [Description("Combo Box")]
    ComboBox = 9
}