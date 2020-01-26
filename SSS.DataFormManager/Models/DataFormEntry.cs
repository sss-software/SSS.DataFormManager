using SSS.DataFormManager.Views.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SSS.DataFormManager.Models
{
    public class DataFormEntry : INotifyPropertyChanged
    {
        private string _LabelControl;
        private string _LabelValue;
        private string _DataControl;
        private string _DataType;
        private string _DataValue;
        private string _ValidationRule;

        private ObservableCollection<string> _LabelControlOptions;
        private ObservableCollection<string> _DataControlOptions;
        private ObservableCollection<string> _DataTypeOptions;
        private ObservableCollection<string> _DataValidationRuleOptions;
        private ObservableCollection<DataFormListEntry> _ListBoxItemsSource;
        private ObservableCollection<DataFormListEntry> _ComboBoxItemsSource;
        private DataFormListEntry _ListBoxSelectedItem;
        private int _ListBoxSelectedIndex;
        private DataFormListEntry _ComboBoxSelectedItem;
        private int _ComboBoxSelectedIndex;
        private string _TextBoxControlEditorValue;
        private string _ListBoxControlValue2;
        private string _ListBoxControlEditorValue;
        private string _ListBoxControlValue;
        private string _ComboBoxControlEditorValue;
        private string _DatePickerControlEditorValue;

        public event PropertyChangedEventHandler PropertyChanged;

        public DataFormEntry()
        {
            PopulateLabelControlOptions();
            PopulateDataControlOptions();
            PopulateDataTypeOptions();
            PopulateDataValidationRuleOptions();
            PopulateDefaultValues();
            _ListBoxControlValue2 = "Test 2";
        }

        private void PopulateDefaultValues()
        {
            LabelControl = "Text Block";
            LabelValue = "";
            DataControl = "Text Box";
            DataType = "Text";
            ValidationRule = "Required";
        }

        public long DataEntryId { get; set; }
        public Guid UniqueId { get; set; }

        public ObservableCollection<string> LabelControlOptions
        {
            get
            {
                return _LabelControlOptions;
            }
            set
            {
                _LabelControlOptions = value;
                OnPropertyChanged("LabelControlOptions");
            }
        }

        public ObservableCollection<string> DataControlOptions
        {
            get
            {
                return _DataControlOptions;
            }
            set
            {
                _DataControlOptions = value;
                OnPropertyChanged("DataControlOptions");
            }
        }

        public ObservableCollection<string> DataTypeOptions
        {
            get
            {
                return _DataTypeOptions;
            }
            set
            {
                _DataTypeOptions = value;
                OnPropertyChanged("DataTypeOptions");
            }
        }

        public ObservableCollection<string> DataValidationRuleOptions
        {
            get
            {
                return _DataValidationRuleOptions;
            }
            set
            {
                _DataValidationRuleOptions = value;
                OnPropertyChanged("DataValidationRuleOptions");
            }
        }

        public string LabelControl
        {
            get
            {
                return _LabelControl;
            }
            set
            {
                _LabelControl = value;
                OnPropertyChanged("LabelControl");
            }
        }

        public string DataControl
        {
            get
            {
                return _DataControl;
            }
            set
            {
                _DataControl = value;
                OnPropertyChanged("DataControl");
            }
        }

        public string LabelValue
        {
            get
            {
                return _LabelValue;
            }
            set
            {
                _LabelValue = value;
                OnPropertyChanged("LabelValue");
            }
        }

        public string DataType
        {
            get
            {
                return _DataType;
            }
            set
            {
                _DataType = value;
                OnPropertyChanged("DataType");
            }
        }

        public string ValidationRule
        {
            get
            {
                return _ValidationRule;
            }
            set
            {
                _ValidationRule = value;
                OnPropertyChanged("ValidationRule");
            }
        }

        public string DataValue
        {
            get
            {
                return _DataValue;
            }
            set
            {
                _DataValue = value;
                OnPropertyChanged("DataValue");
            }
        }

        public bool IsEncrypted { get; set; }

        #region ComboBox

        public ObservableCollection<DataFormListEntry> ComboBoxItemsSource
        {
            get
            {
                return _ComboBoxItemsSource;
            }
            set
            {
                _ComboBoxItemsSource = value;
                OnPropertyChanged("ComboBoxItemsSource");
            }
        }

        public DataFormListEntry ComboBoxSelectedItem
        {
            get
            {
                return _ComboBoxSelectedItem;
            }
            set
            {
                _ComboBoxSelectedItem = value;
                OnPropertyChanged("ComboBoxSelectedItem");
            }
        }

        public int ComboBoxSelectedIndex
        {
            get
            {
                return _ComboBoxSelectedIndex;
            }
            set
            {
                _ComboBoxSelectedIndex = value;
                OnPropertyChanged("ComboBoxSelectedIndex");
            }
        }

        public string ComboBoxControlValue
        {
            get
            {
                return _ComboBoxControlEditorValue;
            }
            set
            {
                _ComboBoxControlEditorValue = value;
                OnPropertyChanged("ComboBoxControlValue");
            }
        }

        public string ComboBoxControlEditorValue
        {
            get
            {
                return _ComboBoxControlEditorValue;
            }
            set
            {
                _ComboBoxControlEditorValue = value;
                OnPropertyChanged("ComboBoxControlEditorValue");
            }
        }

        #endregion ComboBox

        #region DatePicker

        public string DatePickerControlEditorValue
        {
            get
            {
                return _DatePickerControlEditorValue;
            }
            set
            {
                _DatePickerControlEditorValue = value;
                OnPropertyChanged("DatePickerControlEditorValue");
            }
        }

        #endregion DatePicker

        #region TextBox

        public string TextBoxControlValue
        {
            get
            {
                return _TextBoxControlEditorValue;
            }
            set
            {
                _TextBoxControlEditorValue = value;
                OnPropertyChanged("TextBoxControlValue");
            }
        }

        public string TextBoxControlEditorValue
        {
            get
            {
                return _TextBoxControlEditorValue;
            }
            set
            {
                _TextBoxControlEditorValue = value;
                OnPropertyChanged("TextBoxControlEditorValue");
                OnPropertyChanged("TextBoxControlValue");
            }
        }

        #endregion TextBox

        #region ListBox

        public string ListBoxControlValue
        {
            get
            {
                return _ListBoxControlValue;
            }
            set
            {
                _ListBoxControlValue = value;
                OnPropertyChanged("ListBoxControlValue");
            }
        }

        public string ListBoxControlValue2
        {
            get
            {
                return _ListBoxControlValue2;
            }
            set
            {
                _ListBoxControlValue2 = value;
                OnPropertyChanged("ListBoxControlValue2");
            }
        }

        public string ListBoxControlEditorValue
        {
            get
            {
                return _ListBoxControlEditorValue;
            }
            set
            {
                _ListBoxControlEditorValue = value;
                OnPropertyChanged("ListBoxControlEditorValue");
                OnPropertyChanged("ListBoxControlValue");
            }
        }

        public ObservableCollection<DataFormListEntry> ListBoxItemsSource
        {
            get
            {
                return _ListBoxItemsSource;
            }
            set
            {
                _ListBoxItemsSource = value;
                OnPropertyChanged("ListBoxItemsSource");
            }
        }

        public DataFormListEntry ListBoxSelectedItem
        {
            get
            {
                return _ListBoxSelectedItem;
            }
            set
            {
                _ListBoxSelectedItem = value;
                if (_ListBoxSelectedItem != null)
                {
                    ListBoxControlEditorValue = _ListBoxSelectedItem.ToString();
                    ListBoxControlValue = _ListBoxSelectedItem.ToString();
                }
                OnPropertyChanged("ListBoxSelectedItem");
            }
        }

        public int ListBoxSelectedIndex
        {
            get
            {
                return _ListBoxSelectedIndex;
            }
            set
            {
                _ListBoxSelectedIndex = value;
                OnPropertyChanged("ListBoxSelectedIndex");
            }
        }

        #endregion ListBox

        private void PopulateLabelControlOptions()
        {
            ObservableCollection<string> options = new ObservableCollection<string>();
            options.Add("Label");
            options.Add("Text Block");
            LabelControlOptions = options;
        }

        private void PopulateDataControlOptions()
        {
            ObservableCollection<string> options = new ObservableCollection<string>();
            options.Add("Check Box");
            options.Add("Combo Box");
            options.Add("List Box");
            options.Add("Text Block");
            options.Add("Text Box");
            options.Add("Radio Buttions");
            options.Add("Check Boxes");
            DataControlOptions = options;
        }

        private void PopulateDataTypeOptions()
        {
            ObservableCollection<string> options = new ObservableCollection<string>();
            options.Add("Text");
            options.Add("List");
            DataTypeOptions = options;
        }

        private void PopulateDataValidationRuleOptions()
        {
            ObservableCollection<string> options = new ObservableCollection<string>();
            options.Add("Required");
            options.Add("Optional");
            DataValidationRuleOptions = options;
        }

        private void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}