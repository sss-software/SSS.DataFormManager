using SSS.DataFormDataService.Models;
using System.Collections.Generic;

namespace SSS.DataFormDataService.Interfaces
{
    public interface IDataFormDataManager
    {
        IList<DataFormCategoryVM> GetDataFormCategories();
        IList<DataFormSubCategoryVM> GetDataFormSubCategories();
        IList<DataFormTypeVM> GetDataFormTypes();
        IList<SynchronizationRegistryVM> GetSynchronizationRegistries();
    }
}
