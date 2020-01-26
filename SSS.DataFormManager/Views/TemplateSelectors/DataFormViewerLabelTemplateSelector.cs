using SSS.DataFormManager.Models;
using System.Windows;
using System.Windows.Controls;

namespace SSS.DataFormManager.Views.TemplateSelectors
{
    public class DataFormViewerLabelTemplateSelector : DataTemplateSelector
    {
        public DataTemplate LabelControlViewTemplate { get; set; }
        public DataTemplate TextBlockControlViewTemplate { get; set; }
        public DataTemplate DefaultControlTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataFormEntry dataFormEntry = item as DataFormEntry;
            if (dataFormEntry != null)
            {
                switch (dataFormEntry.LabelControl)
                {
                    case "Label":
                        return LabelControlViewTemplate;

                    case "Text Block":
                        return TextBlockControlViewTemplate;

                    default:
                        return DefaultControlTemplate;
                }
            }
            else
                return base.SelectTemplate(item, container);
        }
    }
}