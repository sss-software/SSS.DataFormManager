using SSS.DataFormManager.Models;
using System.Windows;
using System.Windows.Controls;

namespace SSS.DataFormManager.Views.TemplateSelectors
{
    public class DataFormDesignerEditingTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextBoxControlEditingTemplate { get; set; }
        public DataTemplate ListBoxControlEditingTemplate { get; set; }
        public DataTemplate ComboBoxControlEditingTemplate { get; set; }
        public DataTemplate DatePickerControlEditingTemplate { get; set; }
        public DataTemplate DefaultControlTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ContentPresenter presenter = container as ContentPresenter;
            DataGridCell cell = presenter.Parent as DataGridCell;
            DataFormEntry dataFormEntry = (cell.DataContext as DataFormEntry);
            //DataFormEntry dataFormEntry = item as DataFormEntry;
            if (dataFormEntry != null)
            {
                switch (dataFormEntry.DataControl)
                {
                    case "Text Box":
                        return TextBoxControlEditingTemplate;

                    case "List Box":
                        return ListBoxControlEditingTemplate;

                    case "Combo Box":
                        return ComboBoxControlEditingTemplate;

                    case "Date Picker":
                        return DatePickerControlEditingTemplate;

                    default:
                        return DefaultControlTemplate;
                }
            }
            else
                return base.SelectTemplate(item, container);
        }
    }
}