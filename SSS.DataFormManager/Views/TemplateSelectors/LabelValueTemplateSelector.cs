using SSS.DataFormManager.Models;
using System.Windows;
using System.Windows.Controls;

namespace SSS.DataFormManager.Views.TemplateSelectors
{
    public class LabelValueTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextBoxControlTemplate { get; set; }
        public DataTemplate TextBlockControlTemplate { get; set; }
        public DataTemplate DefaultControlTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataFormEntry dataFormEntry = item as DataFormEntry;
            if (dataFormEntry != null)
            {
                switch (dataFormEntry.LabelControl)
                {
                    case "Text Block":
                        return TextBoxControlTemplate;

                    case "Label":
                        return TextBlockControlTemplate;

                    default:
                        return DefaultControlTemplate;
                }
            }
            else
                return base.SelectTemplate(item, container);
        }
    }
}