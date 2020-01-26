using System;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;

namespace SSS.DataFormManager.Views.Helpers
{
    public class DataFormListEntryCollectionEditor : CollectionEditor
    {
        public DataFormListEntryCollectionEditor(Type type) : base()
        {
            base.Editor.CollectionUpdated += Editor_CollectionUpdated;
        }

        private void Editor_CollectionUpdated(object sender, System.Windows.RoutedEventArgs e)
        {
        }

        protected string GetDisplayText(object value)
        {
            DataFormListEntry item = new DataFormListEntry();
            item = (DataFormListEntry)value;

            return string.Format("{0}, {1}", item.KeyName, item.KeyValue);
        }
    }
}