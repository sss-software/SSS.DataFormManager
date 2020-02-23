using SSS.DataFormManager.Models;
using SSS.DataFormManager.Views.Commands;
using SSS.DataFormManager.Views.Helpers;
using SSS.DataFormManager.Views.Windows;
using SSS.EncryptionManagementService;
using SSS.FileManagementService;
using SSS.GoogleDriveCloudService;
using SSS.SerializationManagementService;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SSS.DataFormManager.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IEncryptionManager encryptionManager;
        private readonly IFileManager fileManager;
        private readonly IGoogleDriveCloudManager googleDriveCloudManager;
        private readonly ISerializationManager serializationManager;
        private DataFormCategory currentCategorySelection;
        private DataFormType currentDataFormTypeSelection;
        private int currentSelectedIndex;
        private DataFormSubCategory currentSubCategorySelection;
        private string dataFormCapturedBy;
        private ObservableCollection<DataFormCategory> dataFormCategories;
        private ObservableCollection<DataFormEntry> dataFormEntries;
        private ObservableCollection<DataFormEntry> _DataFormEntries2;
        private string _dataFormFooter;
        private ObservableCollection<DataFormSubCategory> _dataFormSubCategories;
        private DataFormTemplate _DataFormTemplate;
        private string dataFormTitle;
        private Guid dataFormUniqueId;
        private bool _IsPropertiesPopupOpen;
        private ObservableCollection<Type> newItemTypes;
        private DataFormEntry selectedItem;
        private string displayHeader;
        private bool isDataFormViewExpanded;
        private string formDescription;
        private ObservableCollection<DataFormType> dataFormTypes;
        private int currentDataFormTypeSelectedIndex;
        private bool isDataFormViewAvailable;

        public MainWindowViewModel()
        {
            PerformViewModelSetup();
        }

        public MainWindowViewModel(IFileManager fileManager, ISerializationManager serializationManager, IEncryptionManager encryptionManager,
                                   IGoogleDriveCloudManager googleDriveCloudManager)
        {
            this.fileManager = fileManager;
            this.serializationManager = serializationManager;
            this.encryptionManager = encryptionManager;
            this.googleDriveCloudManager = googleDriveCloudManager;
            PerformViewModelSetup();
        }

        public ICommand AddDataFormEntryCommand { get; set; }

        public ICommand AddNewRowCommand { get; set; }

        public DataFormCategory CurrentCategorySelection
        {
            get
            {
                return currentCategorySelection;
            }
            set
            {
                currentCategorySelection = value;
                OnPropertyChanged("CurrentCategorySelection");
                LoadDataFormSubCategories(currentCategorySelection.CategoryId);
            }
        }

        public DataFormType CurrentDataFormTypeSelection
        {
            get
            {
                return currentDataFormTypeSelection;
            }
            set
            {
                currentDataFormTypeSelection = value;
                OnPropertyChanged("CurrentDataFormTypeSelection");
            }
        }

        public int CurrentSelectedIndex
        {
            get
            {
                return currentSelectedIndex;
            }
            set
            {
                currentSelectedIndex = value;
                OnPropertyChanged("CurrentSelectedIndex");
            }
        }

        public int CurrentDataFormTypeSelectedIndex
        {
            get
            {
                return currentDataFormTypeSelectedIndex;
            }
            set
            {
                currentDataFormTypeSelectedIndex = value;
                OnPropertyChanged("CurrentDataFormTypeSelectedIndex");
            }
        }

        public DataFormSubCategory CurrentSubCategorySelection
        {
            get
            {
                return currentSubCategorySelection;
            }
            set
            {
                currentSubCategorySelection = value;
                OnPropertyChanged("CurrentSubCategorySelection");
            }
        }

        public string DataFormCapturedBy
        {
            get
            {
                return dataFormCapturedBy;
            }
            set
            {
                dataFormCapturedBy = value;
                OnPropertyChanged("DataFormCapturedBy");
            }
        }

        public string DisplayHeader
        {
            get
            {
                return displayHeader;
            }
            set
            {
                displayHeader = value;
                OnPropertyChanged("DisplayHeader");
            }
        }

        public bool IsDataFormViewExpanded
        {
            get
            {
                return isDataFormViewExpanded;
            }
            set
            {
                isDataFormViewExpanded = value;
                OnPropertyChanged("IsDataFormViewExpanded");
            }
        }

        public bool IsDataFormViewAvailable
        {
            get
            {
                return isDataFormViewAvailable;
            }
            set
            {
                isDataFormViewAvailable = value;
                OnPropertyChanged("IsDataFormViewAvailable");
            }
        }

        public ObservableCollection<DataFormCategory> DataFormCategories
        {
            get
            {
                return dataFormCategories;
            }
            set
            {
                dataFormCategories = value;
                OnPropertyChanged("DataFormCategories");
            }
        }

        public ObservableCollection<DataFormEntry> DataFormEntries
        {
            get
            {
                return dataFormEntries;
            }
            set
            {
                dataFormEntries = value;
                OnPropertyChanged("DataFormEntries");
            }
        }

        public ObservableCollection<DataFormType> DataFormTypes
        {
            get
            {
                return dataFormTypes;
            }
            set
            {
                dataFormTypes = value;
                OnPropertyChanged("DataFormTypes");
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

        public string DataFormFooter
        {
            get
            {
                return _dataFormFooter;
            }
            set
            {
                _dataFormFooter = value;
                OnPropertyChanged("DataFormFooter");
            }
        }

        public ObservableCollection<DataFormSubCategory> DataFormSubCategories
        {
            get
            {
                return _dataFormSubCategories;
            }
            set
            {
                _dataFormSubCategories = value;
                OnPropertyChanged("DataFormSubCategories");
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

        public string DataFormTitle
        {
            get
            {
                return dataFormTitle;
            }
            set
            {
                dataFormTitle = value;
                OnPropertyChanged("DataFormTitle");
            }
        }

        public string FormDescription
        {
            get
            {
                return formDescription;
            }
            set
            {
                formDescription = value;
                OnPropertyChanged("FormDescription");
            }
        }

        public Guid DataFormUniqueId
        {
            get
            {
                if (dataFormUniqueId == Guid.Empty)
                {
                    dataFormUniqueId = Guid.NewGuid();
                }
                return dataFormUniqueId;
            }
        }

        public DateTime DataFormCapturedOn => DateTime.Now;

        public ICommand GenerateDataFormCommand { get; set; }

        public ICommand SaveToLocalCommand { get; set; }

        public ICommand SaveToCloudCommand { get; set; }

        public ICommand AddAttachmentCommand { get; set; }

        public ICommand RemoveAttachmentCommand { get; set; }

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

        public ObservableCollection<Type> NewItemTypes
        {
            get
            {
                return newItemTypes;
            }
            set
            {
                newItemTypes = value;
                OnPropertyChanged("NewItemTypes");
            }
        }

        public ICommand PropertyCommand { get; set; }

        public ICommand RemoveDataFormEntryCommand { get; set; }

        public ICommand RowNumberGeneratorCommand { get; set; }

        public DataFormEntry SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        private void AddDataFormEntryCommandExecute(object parameter)
        {
            DataFormEntries.Add(new DataFormEntry());
        }

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

        private bool CanGenerateDataFormCommand(object arg)
        {
            return true;
        }

        private bool CanPropertyCommandExecute(object arg)
        {
            return true;
        }

        private bool CanRemoveDataFormEntryExecute(object arg)
        {
            return true;
        }

        private bool CanRowNumberGeneratorExecute(object arg)
        {
            return true;
        }

        private void GenerateDataFormCommandExecute(object obj)
        {
            IsDataFormViewAvailable = true;
            IsDataFormViewExpanded = true;
            // DataFormEntries2 = DataFormEntries;
            // GoogleDriveAccessService.IsAuthenticated();

            //DataFormGenerationService dataFormGenerationService = new DataFormGenerationService();
            //DataFormDTO dto = dataFormGenerationService.CreateDataFormDTO(DataFormEntries);
            //SerializationRequest serializationRequest = new SerializationRequest()
            //{
            //    ObjectToSerialize = dto,
            //    ObjectType = dto.GetType(),
            //    OutputFilePath = @"C:\Users\trebmals\Downloads\XDSL\data.xml"
            //};
            //SerializationManager serialization = new SerializationManager();
            //SerializationResult result = serialization.SerializeToXML(serializationRequest);
            //if (result.IsSerizationValid)
            //{
            //    EncryptionManager encryptionManager = new EncryptionManager();
            //    EncryptionResult er = encryptionManager.Encrypt(new EncryptionRequest()
            //    {
            //    });
            //    if (er.IsEncryptionValid)
            //    {
            //    }
            //}
        }

        private void LoadDataFormCategories()
        {
            if (DataFormCategories == null)
            {
                DataFormCategories = new ObservableCollection<DataFormCategory>();
                DataFormCategories.Add(new DataFormCategory()
                { CategoryId = 1, CategoryName = "Personal Information", Description = "Personal Information" });
                DataFormCategories.Add(new DataFormCategory()
                { CategoryId = 2, CategoryName = "Work Knowledge", Description = "Work Knowledge" });
            }
        }

        private void LoadDataFormSubCategories(long categorId)
        {
            if (DataFormSubCategories == null)
            {
                DataFormSubCategories = new ObservableCollection<DataFormSubCategory>();
            }
            DataFormSubCategories.Clear();
            DataFormSubCategories.Add(new DataFormSubCategory()
            {
                CategoryId = 1,
                SubCategoryId = 1,
                SubCategoryName = "Credit Cards",
                Description = "Credit Cards"
            });
            DataFormSubCategories.Add(new DataFormSubCategory()
            {
                CategoryId = 1,
                SubCategoryId = 2,
                SubCategoryName = "Bank Accounts",
                Description = "Bank Accounts"
            });
            DataFormSubCategories.Add(new DataFormSubCategory()
            {
                CategoryId = 1,
                SubCategoryId = 3,
                SubCategoryName = "Web Accounts",
                Description = "Web Accounts"
            });
            DataFormSubCategories.Add(new DataFormSubCategory()
            {
                CategoryId = 2,
                SubCategoryId = 1,
                SubCategoryName = "Access Control",
                Description = "Access"
            });

            var temp = DataFormSubCategories.Where(category => category.CategoryId == categorId).OrderBy(z => z.SubCategoryName);
            DataFormSubCategories = new ObservableCollection<DataFormSubCategory>(temp);
        }

        private void LoadDataFormTypes()
        {
            if (DataFormTypes == null)
            {
                DataFormTypes = new ObservableCollection<DataFormType>();
            }

            DataFormTypes.Add(new DataFormType()
            {
                DataFormTypeId = 1,
                FormTypeName = "Personal Information",
                FormTypeNameDescription = "Personal Information",
                IsActive = true
            });

            DataFormTypes.Add(new DataFormType()
            {
                DataFormTypeId = 2,
                FormTypeName = "Business Information",
                FormTypeNameDescription = "Business Information",
                IsActive = true
            });

            DataFormTypes.Add(new DataFormType()
            {
                DataFormTypeId = 3,
                FormTypeName = "Personal Finances",
                FormTypeNameDescription = "Personal Finances",
                IsActive = true
            });

            DataFormTypes.Add(new DataFormType()
            {
                DataFormTypeId = 4,
                FormTypeName = "Programming and Development",
                FormTypeNameDescription = "",
                IsActive = true
            });

            DataFormTypes.Add(new DataFormType()
            {
                DataFormTypeId = 5,
                FormTypeName = "Personal Contacts",
                FormTypeNameDescription = "",
                IsActive = true
            });

            DataFormTypes = GetSortedList(DataFormTypes);
        }

        private ObservableCollection<DataFormType> GetSortedList(ObservableCollection<DataFormType> list)
        {
            ObservableCollection<DataFormType> result = new ObservableCollection<DataFormType>();
            ObservableCollection<DataFormType> temp = new ObservableCollection<DataFormType>(list.OrderBy(p => p.FormTypeName));
            result.Clear();
            foreach (DataFormType dataFormType in temp)
            {
                result.Add(dataFormType);
            }
            return result;
        }

        private void PerformViewModelSetup()
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
            LoadDataFormTypes();
            LoadDataFormCategories();

            if (CurrentCategorySelection == null)
            {
                CurrentCategorySelection = DataFormCategories[0];
                CurrentSubCategorySelection = DataFormSubCategories.Where(x => x.CategoryId == CurrentCategorySelection.CategoryId).FirstOrDefault();
            }

            if (CurrentDataFormTypeSelection == null)
            {
                CurrentDataFormTypeSelection = DataFormTypes[0];
            }
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

        private void RemoveDataFormEntryExecute(object obj)
        {
            if (obj != null)
            {
                DataFormEntries.Remove(obj as DataFormEntry);
            }
        }

        private void RowNumberGeneratorExecute(object obj)
        {
            Console.WriteLine("Test");
        }
    }
}