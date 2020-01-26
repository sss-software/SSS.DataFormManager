using SSS.DataFormManager.ViewModels;
using System.Windows;

namespace TestProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewModel = new MainWindowViewModel();
            DataContext = viewModel;
            PropertyGrid.SelectedObject = new DataFormListBox();
            //DataFormControlDesignProperty designProperty = new DataFormControlDesignProperty();
            //PropertyGrid.SelectedObject = designProperty;
            //CollectionControl.ItemsSource = new List<ListEntry>();
            //CollectionControl.ItemsSourceType = typeof(ListEntryCollection);
            //CollectionControl.NewItemTypes = new List<ListEntry>();
            //CollectionControl.NewItemTypes.Add(typeof(ListEntry));
        }
    }
}