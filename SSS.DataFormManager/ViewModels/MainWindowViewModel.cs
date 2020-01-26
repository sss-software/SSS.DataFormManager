using SSS.DataFormManager.Models;
using SSS.DataFormManager.Views.Commands;
using SSS.DataFormManager.Views.Helpers;
using SSS.DataFormManager.Views.Windows;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using System.Xml.Serialization;

namespace SSS.DataFormManager.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<DataFormEntry> _DataFormEntries;
        private ObservableCollection<DataFormEntry> _DataFormEntries2;
        private ObservableCollection<Type> _NewItemTypes;
        private DataFormTemplate _DataFormTemplate;
        private DataFormEntry _SelectedItem;
        private int _CurrentSelectedIndex;
        private bool _IsPropertiesPopupOpen;
        private string _DataFormHeader;
        private string _DataFormFooter;

        public MainWindowViewModel()
        {
            DataFormTemplate = new DataFormTemplate();
            DataFormEntries = new ObservableCollection<DataFormEntry>();
            NewItemTypes = new ObservableCollection<Type>();
            NewItemTypes.Add(typeof(DataFormListEntry));

            AddDataFormEntryCommand = new RelayCommand(AddDataFormEntryCommandExecute, CanAddDataFormEntryCommandExecute);
            RemoveDataFormEntryCommand = new RelayCommand(RemoveDataFormEntryExecute, CanRemoveDataFormEntryExecute);
            RowNumberGeneratorCommand = new RelayCommand(RowNumberGeneratorExecute, CanRowNumberGeneratorExecute);
            PropertyCommand = new RelayCommand(PropertyCommandExecute, CanPropertyCommandExecute);
            GenerateDataFormCommand = new RelayCommand(GenerateDataFormCommandExecute, CanGenerateDataFormCommand);
            DataFormHeader = "Data Form Header";
        }

        private bool CanGenerateDataFormCommand(object arg)
        {
            return true;
        }

        private void GenerateDataFormCommandExecute(object obj)
        {
            DataFormDTO dto = new DataFormDTO();
            var header = new DataFormHeader()
            {
                LocalId = -1,
                CloudId = -1,
                UniqueId = Guid.NewGuid(),
                Title = "Form Title",
                Description = "Form Description",
                IsEncrypted = true
            };
            var footer = new DataFormFooter()
            {
                CapturedBy = "Neil Slambert",
                CapturedOn = DateTime.Now,
                Note = "None"
            };
            var entries = new ObservableCollection<DataEntry>();
            foreach (DataFormEntry entry in DataFormEntries)
            {
                DataEntry data = new DataEntry()
                {
                    LabelControl = entry.LabelControl,
                    LabelValue = entry.LabelValue,
                    DataControl = entry.DataControl,
                    TextBoxControlValue = entry.TextBoxControlValue,
                    ListBoxControlItemsSource = entry.ListBoxItemsSource,
                    ListBoxControlSelectedItem = entry.ListBoxSelectedItem,
                    ListBoxControlSelectedIndex = entry.ListBoxSelectedIndex,
                    ListBoxControlSelectedValue = entry.ListBoxControlValue,
                    ComboBoxControlItemsSource = entry.ComboBoxItemsSource,
                    ComboBoxControlSelectedItem = entry.ComboBoxSelectedItem,
                    ComboBoxControlSelectedIndex = entry.ComboBoxSelectedIndex,
                    ComboBoxControlSelectedValue = entry.ComboBoxControlValue,
                };
                entries.Add(data);
            }

            var body = new DataFormBody()
            {
                DataEntries = entries
            };
            dto.DataFormHeader = header;
            dto.DataFormFooter = footer;
            dto.DataFormBody = body;

            XmlSerializer x = new XmlSerializer(dto.GetType());
            x.Serialize(Console.Out, dto);
            using (TextWriter writer = new StreamWriter(@"C:\Users\trebmals\Downloads\XDSL\data.xml"))
            {
                x.Serialize(writer, dto);
            }
            Console.WriteLine();
            Console.ReadLine();
            var myFileStream = new FileStream(@"C: \Users\trebmals\Downloads\XDSL\data.xml", FileMode.Open);
            // Call the Deserialize method and cast to the object type.
            var myObject = (DataFormDTO)x.Deserialize(myFileStream);
        }

        private bool CanRemoveDataFormEntryExecute(object arg)
        {
            return true;
        }

        private void RemoveDataFormEntryExecute(object obj)
        {
            if (obj != null)
            {
                DataFormEntries.Remove(obj as DataFormEntry);
            }
        }

        private bool CanRowNumberGeneratorExecute(object arg)
        {
            return true;
        }

        private void RowNumberGeneratorExecute(object obj)
        {
            Console.WriteLine("Test");
        }

        public DataFormEntry SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                _SelectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public int CurrentSelectedIndex
        {
            get
            {
                return _CurrentSelectedIndex;
            }
            set
            {
                _CurrentSelectedIndex = value;
                OnPropertyChanged("CurrentSelectedIndex");
            }
        }

        public DataFormTemplate DataFormTemplate
        {
            get
            {
                return _DataFormTemplate;
            }
            set
            {
                _DataFormTemplate = value;
                OnPropertyChanged("DataFormTemplate");
            }
        }

        public ObservableCollection<DataFormEntry> DataFormEntries
        {
            get
            {
                return _DataFormEntries;
            }
            set
            {
                _DataFormEntries = value;
                OnPropertyChanged("DataFormEntries");
            }
        }

        public ObservableCollection<DataFormEntry> DataFormEntries2
        {
            get
            {
                return _DataFormEntries2;
            }
            set
            {
                _DataFormEntries2 = value;
                OnPropertyChanged("DataFormEntries2");
            }
        }

        public ObservableCollection<Type> NewItemTypes
        {
            get
            {
                return _NewItemTypes;
            }
            set
            {
                _NewItemTypes = value;
                OnPropertyChanged("NewItemTypes");
            }
        }

        public bool IsPropertiesPopupOpen
        {
            get
            {
                return _IsPropertiesPopupOpen;
            }
            set
            {
                _IsPropertiesPopupOpen = value;
                OnPropertyChanged("IsPropertiesPopupOpen");
            }
        }

        public string DataFormHeader
        {
            get
            {
                return _DataFormHeader;
            }
            set
            {
                _DataFormHeader = value;
                OnPropertyChanged("DataFormHeader");
            }
        }

        public string DataFormFooter
        {
            get
            {
                return _DataFormFooter;
            }
            set
            {
                _DataFormFooter = value;
                OnPropertyChanged("DataFormFooter");
            }
        }

        public ICommand AddNewRowCommand { get; set; }

        public ICommand AddDataFormEntryCommand { get; set; }

        public ICommand RemoveDataFormEntryCommand { get; set; }

        public ICommand RowNumberGeneratorCommand { get; set; }

        public ICommand PropertyCommand { get; set; }

        public ICommand GenerateDataFormCommand { get; set; }

        private bool CanAddDataFormEntryCommandExecute(object parameter)
        {
            if (DataFormEntries.Count == 0)
            {
                return true;
            }
            else
            {
                var result = true;
                foreach (DataFormEntry entry in DataFormEntries)
                {
                    if ((entry != null) &&
                       ((string.IsNullOrEmpty(entry.LabelControl)) ||
                        (string.IsNullOrEmpty(entry.LabelValue)) || (string.IsNullOrEmpty(entry.DataControl)) ||
                        (string.IsNullOrEmpty(entry.DataType)) || (string.IsNullOrEmpty(entry.ValidationRule))))
                    {
                        result = false;
                        break;
                    }
                }
                return result;
            }
        }

        private void AddDataFormEntryCommandExecute(object parameter)
        {
            DataFormEntries.Add(new DataFormEntry());
        }

        private void PropertyCommandExecute(object parameter)
        {
            PropertiesWindow window;
            if ((SelectedItem != null) && (SelectedItem.ListBoxItemsSource != null))
            {
                window = new PropertiesWindow(SelectedItem.ListBoxItemsSource, SelectedItem.ListBoxSelectedItem);
            }
            else
            {
                window = new PropertiesWindow();
            }

            bool result = (bool)window.ShowDialog();
            if (result)
            {
                SelectedItem.ListBoxItemsSource = window.DataFormListEntries;
                SelectedItem.ListBoxSelectedItem = window.SelectedItem as DataFormListEntry;
            }
        }

        private bool CanPropertyCommandExecute(object arg)
        {
            return true;
        }
    }
}