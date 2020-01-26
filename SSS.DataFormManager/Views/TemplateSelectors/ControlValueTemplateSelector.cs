using SSS.DataFormManager.Models;
using System.Windows;
using System.Windows.Controls;

namespace SSS.DataFormManager.Views.TemplateSelectors
{
    public class ControlValueTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextBoxDataControlTemplate { get; set; }
        public DataTemplate TextBlockDataControlTemplate { get; set; }
        public DataTemplate CheckBoxkDataControlTemplate { get; set; }
        public DataTemplate RadioButtonkDataControlTemplate { get; set; }
        public DataTemplate ListBoxDataControlTemplate { get; set; }
        public DataTemplate DefaultControlTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataFormEntry dataFormEntry = item as DataFormEntry;
            if (dataFormEntry != null)
            {
                switch (dataFormEntry.DataControl)
                {
                    case "Text Box":
                        return TextBoxDataControlTemplate;

                    case "Text Block":
                        return TextBlockDataControlTemplate;

                    default:
                        return DefaultControlTemplate;
                }
            }
            else
                return base.SelectTemplate(item, container);
        }
    }
}