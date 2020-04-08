using SSS.DataFormManager.DAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SSS.DataFormManager.DAL.Repositories.Interfaces
{
    public interface IDataFormRepository
    {
        bool AddDataFormCategory(DataFormCategory dataFormCategory);
        bool UpdateDataFormCategory(DataFormCategory dataFormCategory);
        bool RemoveDataFormCategory(DataFormCategory dataFormCategory);
        bool AddDataFormSubCategory(DataFormSubCategory dataFormSubCategory);
        bool UpdateDataFormSubCategory(DataFormSubCategory dataFormSubCategory);
        bool RemoveDataFormCategory(DataFormSubCategory dataFormSubCategory);

        ObservableCollection<DataFormCategory> GetDataFormCategories();

        ObservableCollection<DataFormSubCategory> GetDataFormSubCategories();

        ObservableCollection<DataFormType> GetDataFormTypes();

        ObservableCollection<SynchronizationRegistry> GetSynchronizationRegistries();


    }
}
