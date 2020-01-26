using SSS.DataFormManager.Views.Commands;
using SSS.DataFormManager.Views.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace SSS.DataFormManager.Views.Windows
{
    /// <summary>
    /// Interaction logic for PropertiesWindow.xaml
    /// </summary>
    public partial class PropertiesWindow : Window
    {
        private ObservableCollection<DataFormListEntry> _DataFormListEntries;

        public PropertiesWindow()
        {
            InitializeComponent();
            DoSetup();
        }

        public PropertiesWindow(ObservableCollection<DataFormListEntry> entries, DataFormListEntry selectedItem)
        {
            InitializeComponent();
            DoSetup();
            foreach (DataFormListEntry entry in entries)
            {
                ItemsSource.Add(entry);
            }
            this.SelectedItem = selectedItem;
        }

        private void DoSetup()
        {
            NewItemTypes = new List<Type>();
            NewItemTypes.Add(typeof(DataFormListEntry));
            ItemsSource = new ObservableCollection<object>();
            ItemsSourceType = typeof(DataFormListEntryCollection);
            OkCommand = new RelayCommand(OkCommandExecute, CanOkCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute, CanCancelCommandExecute);
        }

        private bool CanCancelCommandExecute(object arg)
        {
            return true;
        }

        private void CancelCommandExecute(object obj)
        {
            this.DialogResult = false;
            this.Close();
        }

        private bool CanOkCommandExecute(object arg)
        {
            return true;
        }

        private void OkCommandExecute(object obj)
        {
            var x = (ObservableCollection<object>)(obj);
            DataFormListEntries = new ObservableCollection<DataFormListEntry>();
            foreach (object o in x)
            {
                DataFormListEntry e = (DataFormListEntry)o;
                DataFormListEntries.Add(e);
            }
            this.DialogResult = true;
            this.Close();
        }

        public ObservableCollection<DataFormListEntry> DataFormListEntries
        {
            get
            {
                return _DataFormListEntries;
            }
            set
            {
                _DataFormListEntries = value;
                //OnPropertyChanged("DataFormListEntries");
            }
        }

        #region NewItemType Property

        public static readonly DependencyProperty NewItemTypesProperty =
            DependencyProperty.Register("NewItemTypes",
                                        typeof(IList),
                                        typeof(PropertiesWindow),
                                        new UIPropertyMetadata(null));

        public IList<Type> NewItemTypes
        {
            get
            {
                return (IList<Type>)GetValue(NewItemTypesProperty);
            }
            set
            {
                SetValue(NewItemTypesProperty, value);
            }
        }

        #endregion NewItemType Property

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource",
                                        typeof(ObservableCollection<object>),
                                        typeof(PropertiesWindow),
                                        new UIPropertyMetadata(null));

        public ObservableCollection<object> ItemsSource
        {
            get
            {
                return (ObservableCollection<object>)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public static readonly DependencyProperty ItemsSourceTypeProperty = DependencyProperty.Register("ItemsSourceType", typeof(Type), typeof(PropertiesWindow),
                                                                            new UIPropertyMetadata(null));

        public Type ItemsSourceType
        {
            get
            {
                return (Type)GetValue(ItemsSourceTypeProperty);
            }
            set
            {
                SetValue(ItemsSourceTypeProperty, value);
            }
        }

        #region SelectedItem Property

        public static readonly DependencyProperty SelectedItemProperty =
                                            DependencyProperty.Register("SelectedItem",
                                            typeof(object),
                                            typeof(PropertiesWindow),
                                            new UIPropertyMetadata(null));

        public object SelectedItem
        {
            get
            {
                return (object)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        #endregion SelectedItem Property

        #region EditorDefinitions Property

        public static readonly DependencyProperty EditorDefinitionsProperty = DependencyProperty.Register("EditorDefinitions", typeof(EditorDefinitionCollection),
            typeof(PropertiesWindow), new UIPropertyMetadata(null));

        public EditorDefinitionCollection EditorDefinitions
        {
            get
            {
                return (EditorDefinitionCollection)GetValue(EditorDefinitionsProperty);
            }
            set
            {
                SetValue(EditorDefinitionsProperty, value);
            }
        }

        #endregion EditorDefinitions Property

        public ICommand OkCommand { get; set; }

        public ICommand CancelCommand { get; set; }
    }
}