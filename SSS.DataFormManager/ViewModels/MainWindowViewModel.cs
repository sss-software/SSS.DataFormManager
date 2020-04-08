using SSS.DataFormManager.DAL.Models;
using SSS.DataFormManager.DAL.Repositories.Interfaces;
using SSS.DataFormManager.Domain.Services;
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
using System.Windows;
using System.Windows.Input;
using Unity;
using Unity.Resolution;

namespace SSS.DataFormManager.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IEncryptionManager encryptionManager;
        private readonly IFileManager fileManager;
        private readonly IGoogleDriveCloudManager googleDriveCloudManager;
        private readonly ISerializationManager serializationManager;
        private readonly IDataFormRepository dataFormRepository;
        private ObservableCollection<DataFormEntry> _DataFormEntries2;
        private string _dataFormFooter;
        private ObservableCollection<DataFormSubCategory> _dataFormSubCategories;
        private DataFormDesignTemplate dataFormTemplate;
        private bool _IsPropertiesPopupOpen;
        private DataFormCategoryDTO currentCategorySelection;
        private int currentDataFormTypeSelectedIndex;
        private DataFormType currentDataFormTypeSelection;
        private int currentSelectedIndex;
        private DataFormSubCategory currentSubCategorySelection;
        private string dataFormCapturedBy;
        private ObservableCollection<DataFormCategory> dataFormCategories;
        private ObservableCollection<DataFormEntry> dataFormEntries;
        private string formTitle;
        private ObservableCollection<DataFormType> dataFormTypes;
        private Guid dataFormUniqueId;
        private string displayHeader;
        private string formDescription;
        private bool isDataFormViewAvailable;
        private bool isDataFormViewExpanded;
        private ObservableCollection<Type> newItemTypes;
        private DataFormEntry selectedItem;
        private DataFormDesignTemplate dataFormDesignTemplate;
        private int currentCategorySelectedIndex;
        private int currentSubCategorySelectedIndex;
        private ObservableCollection<DataFormAttachment> dataFormAttachments;

        private IUnityContainer container;

        public MainWindowViewModel()
        {
            PerformViewModelSetup();
        }

        public MainWindowViewModel(IUnityContainer container, IFileManager fileManager, ISerializationManager serializationManager, IEncryptionManager encryptionManager, IGoogleDriveCloudManager googleDriveCloudManager, IDataFormRepository dataFormRepository)
        {
            this.container = container;
            this.fileManager = fileManager;
            this.serializationManager = serializationManager;
            this.encryptionManager = encryptionManager;
            this.googleDriveCloudManager = googleDriveCloudManager;
            this.dataFormRepository = dataFormRepository;
       
            PerformViewModelSetup();
        }

        public ICommand AddAttachmentCommand { get; set; }
        public ICommand AddDataFormEntryCommand { get; set; }

        public ICommand AddNewRowCommand { get; set; }

        public DataFormCategoryDTO CurrentCategorySelection
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

        public int CurrentCategorySelectedIndex
        {
            get
            {
                return currentCategorySelectedIndex;
            }
            set
            {
                currentCategorySelectedIndex = value;
                OnPropertyChanged("CurrentCategorySelectedIndex");
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

        public int CurrentSubCategorySelectedIndex
        {
            get
            {
                return currentSubCategorySelectedIndex;
            }
            set
            {
                currentSubCategorySelectedIndex = value;
                OnPropertyChanged("CurrentSubCategorySelectedIndex");
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

        public DateTime DataFormCapturedOn => DateTime.Now;

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

        public ObservableCollection<DataFormAttachment> DataFormAttachments
        {
            get
            {
                return dataFormAttachments;
            }
            set
            {
                dataFormAttachments = value;
                OnPropertyChanged("DataFormAttachments");
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

        public DataFormDesignTemplate DataFormDesignTemplate
        {
            get
            {
                return dataFormDesignTemplate;
            }
            set
            {
                dataFormDesignTemplate = value;
                OnPropertyChanged("DataFormDesignTemplate");
            }
        }

        public string FormTitle
        {
            get
            {
                return formTitle;
            }
            set
            {
                formTitle = value;
                OnPropertyChanged("FormTitle");
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

        public ICommand GenerateDataFormCommand { get; set; }

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
        public ICommand RemoveAttachmentCommand { get; set; }
        public ICommand RemoveDataFormEntryCommand { get; set; }
        public ICommand RowNumberGeneratorCommand { get; set; }
        public ICommand SaveToCloudCommand { get; set; }
        public ICommand SaveToLocalCommand { get; set; }

        public ICommand AddFormTypeCommand { get; set; }
        public ICommand UpdateFormTypeCommand { get; set; }
        public ICommand RemoveFormTypeCommand { get; set; }
        public ICommand AddCategoryCommand { get; set; }
        public ICommand UpdateCategoryCommand { get; set; }
        public ICommand RemoveCategoryCommand { get; set; }
        public ICommand AddSubCategoryCommand { get; set; }
        public ICommand UpdateSubCategoryCommand { get; set; }
        public ICommand RemoveSubCategoryCommand { get; set; }

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

        public DataFormDTO DataFormDTO { get; private set; }

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
            DataFormGenerationService service = new DataFormGenerationService();
            DataFormDesignTemplate.DataFormHeader = new DataFormHeader()
            {
                SynchronizationId = -1,
                LocalId = DataFormUniqueId.ToString(),
                CloudId = string.Empty,
                FormTitle = FormTitle,
                FormDescription = FormDescription,
                FormDisplayHeader = DisplayHeader,
                SelectedFormType = CurrentDataFormTypeSelection.FormTypeName,
                SelectedFormTypeIndex = CurrentDataFormTypeSelectedIndex,
                SelectedCategory = CurrentCategorySelection.CategoryName,
                SelectedCategoryIndex = CurrentCategorySelectedIndex,
                SelectedSubCategory = CurrentSubCategorySelection.SubCategoryName,
                SelectedSubCategoryIndex = CurrentSubCategorySelectedIndex,
                DataFormCategories = DataFormCategories,
                DataFormSubCategories = DataFormSubCategories,
                DataFormTypes = DataFormTypes,
                IsEncrypted = true
            };
            var dataEntries = new ObservableCollection<DataEntry>();
            foreach (DataFormEntry dataFormEntry in DataFormEntries)
            {
                DataEntry dataEntry = new DataEntry()
                {
                    LabelControl = dataFormEntry.LabelControl,
                    LabelValue = dataFormEntry.LabelValue,
                    DataControl = dataFormEntry.DataControl,
                    TextBoxControlValue = dataFormEntry.TextBoxControlValue,
                    ListBoxControlSelectedValue = dataFormEntry.ListBoxControlValue,
                    ListBoxControlItemsSource = dataFormEntry.ListBoxItemsSource,
                    ListBoxControlSelectedItem = dataFormEntry.ListBoxSelectedItem,
                    ListBoxControlSelectedIndex = dataFormEntry.ListBoxSelectedIndex,
                    ComboBoxControlSelectedValue = dataFormEntry.ListBoxControlValue,
                    ComboBoxControlItemsSource = dataFormEntry.ComboBoxItemsSource,
                    ComboBoxControlSelectedItem = dataFormEntry.ComboBoxSelectedItem,
                    ComboBoxControlSelectedIndex = dataFormEntry.ComboBoxSelectedIndex
                };
                dataEntries.Add(dataEntry);
            }

            var attachments = new ObservableCollection<DataFormAttachment>();

            DataFormDesignTemplate.DataFormBody = new DataFormBody()
            {
                DataEntries = dataEntries,
                Attachments = attachments
            };
            DataFormDesignTemplate.DataFormFooter = new DataFormFooter()
            {
                CapturedBy = DataFormCapturedBy,
                CapturedOn = DateTime.Now,
                Note = string.Empty
            };
            DataFormDTO = service.CreateDataFormDTO(DataFormDesignTemplate);
            IsDataFormViewAvailable = true;
            IsDataFormViewExpanded = true;
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

        private void LoadDataFormCategories()
        {
            if (DataFormCategories == null)
            {
                var categories = dataFormRepository.GetDataFormCategories();
           //DataFormCategories = new ObservableCollection<DataFormCategoryDTO>();
           //     DataFormCategories.Add(new DataFormCategoryDTO()
           //     { CategoryId = 1, CategoryName = "Personal Information", Description = "Personal Information" });
           //     DataFormCategories.Add(new DataFormCategoryDTO()
           //     { CategoryId = 2, CategoryName = "Work Knowledge", Description = "Work Knowledge" });
            }
        }

        private void LoadDataFormSubCategories(long categorId)
        {
            if (DataFormSubCategories == null)
            {
                var subCategories = dataFormRepository.GetSynchronizationRegistries();
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
                FormTypeDescription = "Personal Information",
                IsActive = true
            });

            DataFormTypes.Add(new DataFormType()
            {
                DataFormTypeId = 2,
                FormTypeName = "Business Information",
                FormTypeDescription = "Business Information",
                IsActive = true
            });

            DataFormTypes.Add(new DataFormType()
            {
                DataFormTypeId = 3,
                FormTypeName = "Personal Finances",
                FormTypeDescription = "Personal Finances",
                IsActive = true
            });

            DataFormTypes.Add(new DataFormType()
            {
                DataFormTypeId = 4,
                FormTypeName = "Programming and Development",
                FormTypeDescription = "",
                IsActive = true
            });

            DataFormTypes.Add(new DataFormType()
            {
                DataFormTypeId = 5,
                FormTypeName = "Personal Contacts",
                FormTypeDescription = "",
                IsActive = true
            });

            DataFormTypes = GetSortedList(DataFormTypes);
        }

        private void PerformViewModelSetup()
        {
            DataFormDesignTemplate = new DataFormDesignTemplate();
            DataFormEntries = new ObservableCollection<DataFormEntry>();
            NewItemTypes = new ObservableCollection<Type>();
            NewItemTypes.Add(typeof(DataFormListEntry));

            AddDataFormEntryCommand = new RelayCommand(AddDataFormEntryCommandExecute, CanAddDataFormEntryCommandExecute);
            RemoveDataFormEntryCommand = new RelayCommand(RemoveDataFormEntryExecute, CanRemoveDataFormEntryExecute);
            RowNumberGeneratorCommand = new RelayCommand(RowNumberGeneratorExecute, CanRowNumberGeneratorExecute);
            PropertyCommand = new RelayCommand(PropertyCommandExecute, CanPropertyCommandExecute);
            GenerateDataFormCommand = new RelayCommand(GenerateDataFormCommandExecute, CanGenerateDataFormCommand);

            SaveToCloudCommand = new RelayCommand(SaveToCloudCommandExecute, CanSaveToCloudCommandExecute);
            SaveToLocalCommand = new RelayCommand(SaveToLocalCommandExecute, CanSaveToLocalCommandExecute);

            AddFormTypeCommand = new RelayCommand(AddFormTypeCommandExecute, CanAddFormTypeCommandExecute);
            UpdateFormTypeCommand = new RelayCommand(UpdateFormTypeCommandExecute, CanUpdateFormTypeCommandExecute);
            RemoveFormTypeCommand = new RelayCommand(RemoveFormTypeCommandExecute, CanRemoveFormTypeCommandExecute);

            AddCategoryCommand = new RelayCommand(AddCategoryCommandExecute, CanAddCategoryCommandExecute);
            UpdateCategoryCommand = new RelayCommand(UpdateCategoryCommandExecute, CanUpdateCategoryCommandExecute);
            RemoveCategoryCommand = new RelayCommand(RemoveCategoryCommandExecute, CanRemoveCategoryCommandExecute);

            AddSubCategoryCommand = new RelayCommand(AddSubCategoryCommandExecute, CanAddSubCategoryCommandExecute);
            UpdateSubCategoryCommand = new RelayCommand(UpdateSubCategoryCommandExecute, CanUpdateSubCategoryCommandExecute);
            RemoveSubCategoryCommand = new RelayCommand(RemoveSubCategoryCommandExecute, CanRemoveSubCategoryCommandExecute);

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

        private bool CanUpdateSubCategoryCommandExecute(object arg)
        {
            return true;
        }

        private void UpdateSubCategoryCommandExecute(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanUpdateFormTypeCommandExecute(object arg)
        {
            return true;
        }

        private void UpdateFormTypeCommandExecute(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanUpdateCategoryCommandExecute(object arg)
        {
            return true;
        }

        private void UpdateCategoryCommandExecute(object obj)
        {
            CategoryWindow window = container.Resolve<CategoryWindow>(new ResolverOverride[]
                                   {
                                       new ParameterOverride("actionCode", 2)
                                   });
            window.Owner = (obj as Window);
            bool result = (bool)window.ShowDialog();
            if (result)
            {
                var update = (window.DataContext as CategoryWindowViewModel).DataFormCategory;
                var current = DataFormCategories.Where(s => s.CategoryId == update.CategoryId).FirstOrDefault();
                if (current != null)
                {
                    current.CategoryName = update.CategoryName;
                    current.Description = update.Description;
                }
           }
        }

        private bool CanRemoveSubCategoryCommandExecute(object arg)
        {
            return true;
        }

        private void RemoveSubCategoryCommandExecute(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanAddSubCategoryCommandExecute(object arg)
        {
            return true;
        }

        private void AddSubCategoryCommandExecute(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanRemoveCategoryCommandExecute(object arg)
        {
            return true;
        }

        private void RemoveCategoryCommandExecute(object obj)
        {
            CategoryWindow window = container.Resolve<CategoryWindow>(new ResolverOverride[]
                                   {
                                       new ParameterOverride("actionCode", 3)
                                   });
            window.Owner = (obj as Window);
            bool result = (bool)window.ShowDialog();
            if (result)
            {
                var removedCategory = (window.DataContext as CategoryWindowViewModel).DataFormCategory;
                var current = DataFormCategories.Where(s => s.CategoryId == removedCategory.CategoryId).FirstOrDefault();
                if (current != null)
                {
                    DataFormCategories.Remove(current);
                }
            }
        }

        private bool CanAddCategoryCommandExecute(object arg)
        {
            return true;
        }

        private void AddCategoryCommandExecute(object obj)
        {
            CategoryWindow window = container.Resolve<CategoryWindow>(new ResolverOverride[]
                                   {
                                       new ParameterOverride("actionCode", 1)
                                   });
            window.Owner = (obj as Window);
            bool result = (bool)window.ShowDialog();
            if (result)
            {
                var category = (window.DataContext as CategoryWindowViewModel).DataFormCategory;
                DataFormCategories.Add(category);
            }
        }

        private bool CanRemoveFormTypeCommandExecute(object arg)
        {
            return true;
        }

        private void RemoveFormTypeCommandExecute(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanAddFormTypeCommandExecute(object arg)
        {
            return true;
        }

        private void AddFormTypeCommandExecute(object obj)
        {
        }

        private bool CanSaveToLocalCommandExecute(object arg)
        {
            return true;
        }

        private void SaveToLocalCommandExecute(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanSaveToCloudCommandExecute(object arg)
        {
            return true;
        }

        private void SaveToCloudCommandExecute(object obj)
        {
            throw new NotImplementedException();
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