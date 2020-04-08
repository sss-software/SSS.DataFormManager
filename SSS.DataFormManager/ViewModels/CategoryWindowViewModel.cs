using SSS.DataFormManager.DAL.Models;
using SSS.DataFormManager.DAL.Repositories.Interfaces;
using SSS.DataFormManager.Models;
using SSS.DataFormManager.ViewModels.Interfacs;
using SSS.DataFormManager.Views.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Unity;

namespace SSS.DataFormManager.ViewModels
{
    public class CategoryWindowViewModel:  ViewModelBase, ICategoryWindowViewModel
    {
        private readonly IUnityContainer container;
        private readonly IDataFormRepository dataFormRepository;
        private int actionCode;

        private bool isUpdateAction;
        private bool isRemoveAction;
        private bool isAddAction;
        private string categoryButtonTitle;
        private DataFormCategoryDTO dataFormCategory;

        public CategoryWindowViewModel(IUnityContainer container, IDataFormRepository dataFormRepository, int actionCode)
        {
            this.container = container;
            this.dataFormRepository = dataFormRepository;
            PerformWindowSetup(actionCode);
            PerformViewModelSetup();
        }

        public ICommand CategoryManageCommand { get; set; }

        public string CategoryButtonTitle
        {
            get
            {
                return categoryButtonTitle;
            }
            set
            {
                categoryButtonTitle = value;
                OnPropertyChanged("CategoryButtonTitle");
            }
        }

        public bool IsAddAction
        {
            get
            {
                return isAddAction;
            }
            set
            {
                isAddAction = value;
                if (isAddAction)
                {
                    IsRemoveAction = false;
                    IsUpdateAction = false;
                }
            }
        }

        public bool IsUpdateAction
        {
            get
            {
                return isUpdateAction;
            }
            set
            {
                isUpdateAction = value;
                if (isUpdateAction)
                {
                    IsAddAction = false;
                    IsRemoveAction = false;
                }
            }
        }


        public bool IsRemoveAction
        {
            get
            {
                return isRemoveAction;
            }
            set
            {
                isRemoveAction = value;
                if (isRemoveAction)
                {
                    IsAddAction = false;
                    IsUpdateAction = false;
                }
            }
        }

        public DataFormCategoryDTO DataFormCategory
        {
            get
            {
                return dataFormCategory;
            }
            set
            {
                dataFormCategory = value;
                OnPropertyChanged("DataFormCategory");
            }
        }

        private void PerformViewModelSetup()
        {
            DataFormCategory = new DataFormCategoryDTO();
            CategoryManageCommand = new RelayCommand(CategoryManageCommandExecute, CanCategoryManageCommandExecute);
        }

        private bool CanCategoryManageCommandExecute(object arg)
        {
            return true;
        }

        private void CategoryManageCommandExecute(object obj)
        {

            if (IsValidInput())
            {
                bool isSaved = Save();
                if ((obj != null) && (obj is Window))
                {
                    (obj as Window).DialogResult = true;
                    (obj as Window).Close();
                }
                else
                {
                    ShowValidationMessage();
                }
            }
            else
            {
                ShowValidationMessage();
            }
        }

        private bool Save(DataFormCategory dataFormCategory)
        {
            bool result = true;
            if (IsAddAction)
            {
                result = dataFormRepository.AddDataFormCategory(dataFormCategory);
            }
            else if (IsUpdateAction)
            {
                result = dataFormRepository.UpdateDataFormCategory(dataFormCategory);
            }
            else if (IsRemoveAction)
            {
                result = dataFormRepository.RemoveDataFormCategory(dataFormCategory);
            }

            return result;
        }

        private bool IsValidInput()
        {
            bool result = false;
            if ((IsAddAction || IsUpdateAction) && (!IsDuplicateCategory()))
            {
                result = ((DataFormCategory != null) && (IsAddAction || IsUpdateAction) &&
                              (!string.IsNullOrEmpty(DataFormCategory.CategoryName)) && (!string.IsNullOrEmpty(DataFormCategory.Description)));
            }
            else if (IsRemoveAction && DoesCategoryExist())
            {
                result = true;
            }
            return result;
        }

        private void ShowValidationMessage()
        {

        }

        private bool DoesCategoryExist()
        {
            bool result = false;
            return result;
        }

        private bool IsDuplicateCategory()
        {
            bool result = false;
            return result;
        }

        private void PerformWindowSetup(int actionCode)
        {
            this.actionCode = actionCode;
            switch (actionCode)
            {
                case 1:
                    categoryButtonTitle = "SAVE";
                    isAddAction = true;
                    break;
                case 2:
                    categoryButtonTitle = "UPDATE";
                    isUpdateAction = true;
                    break;
                case 3:
                    categoryButtonTitle = "REMOVE";
                    isRemoveAction = true;
                    break;
                default:
                    categoryButtonTitle = "OK";
                    break;

            }
        }
    }
}
