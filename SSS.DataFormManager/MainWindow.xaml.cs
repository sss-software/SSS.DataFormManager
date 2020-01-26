using SSS.DataFormManager.ViewModels;
using SSS.DataFormManager.Views.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace SSS.DataFormManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var dataContext = new MainWindowViewModel();
            DataContext = dataContext;
            dgDataFormTemplateDesigner.ItemContainerGenerator.ItemsChanged += ItemContainerGenerator_ItemsChanged;
            // var listBox = new DataFormListBox();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ItemContainerGenerator_ItemsChanged(object sender, ItemsChangedEventArgs e)
        {
            IEnumerable<DataGridRow> rows = FindVisualChildren<DataGridRow>(dgDataFormTemplateDesigner);
            foreach (DataGridRow row in rows)
            {
                row.Header = (row.GetIndex() + 1).ToString();
            }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject dependencyObject)
            where T : DependencyObject
        {
            if (dependencyObject != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(dependencyObject, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void DgDataFormTemplateDesigner_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            IEnumerable<DataGridRow> rows = FindVisualChildren<DataGridRow>(dgDataFormTemplateDesigner);
            List<DataGridRow> myList = rows.ToList();
            foreach (DataGridRow row in rows)
            {
                row.Header = (row.GetIndex() + 1).ToString();
            }
            //int index = myList[myList.Count - 1].GetIndex();
            //dgDataFormTemplateDesigner.SelectedItem = dgDataFormTemplateDesigner.Items[index];
            //dgDataFormTemplateDesigner.ScrollIntoView(dgDataFormTemplateDesigner.Items[index]);
            //DataGridRow dgrow = (DataGridRow)dgDataFormTemplateDesigner.ItemContainerGenerator.ContainerFromItem(dgDataFormTemplateDesigner.Items[index]);
            //dgrow.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            //dgDataFormTemplateDesigner.CurrentCell = new DataGridCellInfo(dgDataFormTemplateDesigner.Items[index], dgDataFormTemplateDesigner.Columns[0]);
            //dgDataFormTemplateDesigner.BeginEdit();
            ////first focus the grid
            //dataGrid1.Focus();
        }

        private void DgDataFormTemplateDesigner_Loaded(object sender, RoutedEventArgs e)
        {
            int index = 0;
            //dgDataFormTemplateDesigner.Focus();
            //if ((dgDataFormTemplateDesigner.Items != null) && (dgDataFormTemplateDesigner.Items.Count > 0))
            //{
            //    DataGridCellInfo cellInfo = new DataGridCellInfo(dgDataFormTemplateDesigner.Items[index],
            //                                     dgDataFormTemplateDesigner.Columns[0]);
            //    dgDataFormTemplateDesigner.CurrentCell = cellInfo;
            //    dgDataFormTemplateDesigner.ScrollIntoView(dgDataFormTemplateDesigner.Items[index]);
            //    dgDataFormTemplateDesigner.BeginEdit();
            // }
        }

        private void DgDataFormTemplateDesigner_Selected(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource.GetType() == typeof(DataGridCell))
            {
                DataGrid g = (DataGrid)sender;
                g.BeginEdit(e);
                DataGridCell cell = e.OriginalSource as DataGridCell;
                List<TextBox> textBoxes = DependencyObjectHelper.FindChildrenByType<TextBox>(cell);
                if (textBoxes.Count > 0)
                {
                    textBoxes[0].Focus();
                    textBoxes[0].SelectAll();
                }
            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            //var dataFormEntries = (DataContext as MainWindowViewModel).DataFormEntries;
            //if (dataFormEntries != null)
            //{
            //    ColumnDefinition column = new ColumnDefinition();
            //    column.Width = new GridLength(0, GridUnitType.Auto);
            //    DataFormGrid.ColumnDefinitions.Add(column);
            //    column = new ColumnDefinition();
            //    column.Width = new GridLength(0, GridUnitType.Auto);
            //    DataFormGrid.ColumnDefinitions.Add(column);

            //    DataFormGrid.Background = new SolidColorBrush(new Color());
            //    TextBlock tblock;
            //    foreach (DataFormEntry entry in dataFormEntries)
            //    {
            //        RowDefinition newRow = new RowDefinition();
            //        newRow.Height = new GridLength(25, GridUnitType.Pixel);

            //        DataFormGrid.RowDefinitions.Add(newRow);
            //    }
            //    for (int i = 0; i < dataFormEntries.Count; i++)
            //    {
            //        if (dataFormEntries[i].LabelControl == "Text Block")
            //        {
            //            tblock = new TextBlock();
            //            tblock.Text = dataFormEntries[i].LabelValue;
            //            Grid.SetRow(tblock, i);
            //            Grid.SetColumn(tblock, 0);
            //            DataFormGrid.Children.Add(tblock);
            //        }
            //    }
            //}
        }
    }
}