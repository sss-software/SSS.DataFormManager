using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace TestProject
{
    public class DataFormListBox : ListBox
    {
        public DataFormListBox()
        {
            var list = new ObservableCollection<string>();
            list.Add("Neil Slambert");
            this.ItemsSource = list;
            var x = this.Items;
        }
    }
}