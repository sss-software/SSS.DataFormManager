using SSS.DataFormManager.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace SSS.DataFormManager.Views.TemplateSelectors
{
    public class DataFormDesignerValueTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextBoxControlValueTemplate { get; set; }
        public DataTemplate ListBoxControlValueTemplate { get; set; }
        public DataTemplate ComboBoxControlValueTemplate { get; set; }
        public DataTemplate DatePickerControlValueTemplate { get; set; }
        public DataTemplate DefaultControlTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ContentPresenter presenter = container as ContentPresenter;
            DataGridCell cell = presenter.Parent as DataGridCell;
            DataFormEntry dataFormEntry = (cell.DataContext as DataFormEntry);
            PropertyChangedEventHandler lambda = null;
            lambda = (o, args) =>
            {
                if (args.PropertyName == "ListBoxControlValue")
                {
                    dataFormEntry.PropertyChanged -= lambda;
                    var cp = (ContentPresenter)container;
                    cp.ContentTemplateSelector = null;
                    cp.ContentTemplateSelector = this;
                }
            };
            dataFormEntry.PropertyChanged += lambda;

            if (dataFormEntry != null)
            {
                switch (dataFormEntry.DataControl)
                {
                    case "Text Box":
                        return TextBoxControlValueTemplate;

                    case "List Box":
                        var x = dataFormEntry.ListBoxControlValue;
                        var y = dataFormEntry.ListBoxControlEditorValue;
                        return ListBoxControlValueTemplate;

                    case "Combo Box":
                        return ComboBoxControlValueTemplate;

                    case "Date Picker":
                        return DatePickerControlValueTemplate;

                    default:
                        return DefaultControlTemplate;
                }
            }
            else
                return base.SelectTemplate(item, container);
        }
    }
}