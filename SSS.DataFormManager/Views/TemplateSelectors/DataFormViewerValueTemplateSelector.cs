using SSS.DataFormManager.Models;
using System.Windows;
using System.Windows.Controls;

namespace SSS.DataFormManager.Views.TemplateSelectors
{
    public class DataFormViewerValueTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextBoxControlValueViewerTemplate { get; set; }
        public DataTemplate ListBoxControlValueViewerTemplate { get; set; }
        public DataTemplate ComboBoxControlValueViewerTemplate { get; set; }
        public DataTemplate DatePickerControlValueViewerTemplate { get; set; }
        public DataTemplate DefaultControlTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataFormEntry dataFormEntry = item as DataFormEntry;
            if (dataFormEntry != null)
            {
                switch (dataFormEntry.DataControl)
                {
                    case "List Box":
                        return ListBoxControlValueViewerTemplate;

                    case "Text Box":
                        return TextBoxControlValueViewerTemplate;

                    case "Combo Box":
                        return ComboBoxControlValueViewerTemplate;

                    case "Date Picker":
                        return DatePickerControlValueViewerTemplate;

                    default:
                        return DefaultControlTemplate;
                }
            }
            else
                return base.SelectTemplate(item, container);
        }
    }
}