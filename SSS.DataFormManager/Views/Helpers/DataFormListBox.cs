using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace SSS.DataFormManager.Views.Helpers
{
    public class DataFormListBox : ListBox, INotifyPropertyChanged
    {
        private DataFormListEntryCollection _DataFormListEntryCollection;

        [Editor(typeof(DataFormListEntryCollectionEditor), typeof(UITypeEditor))]
        [Category("A - User Lists")]
        [DisplayName("User List Entries")]
        [Description("A collection of user list entries")]
        [NewItemTypes(typeof(DataFormListEntry))]
        public DataFormListEntryCollection DataFormListEntryCollection
        {
            get { return _DataFormListEntryCollection; }
            set
            {
                _DataFormListEntryCollection = value;
                OnPropertyChanged("DataFormListEntryCollection");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}