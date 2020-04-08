using SSS.DataFormDataService.Interfaces;
using SSS.DataFormDataService.Models;
using SSS.DataFormManager.DAL.Models;
using SSS.DataFormManager.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSS.DataFormDataService
{

        public class DataFormDataManager : IDataFormDataManager
        {
            private readonly IDataFormCategoryRepository dataFormCategoryRepository;
            public DataFormDataManager(IDataFormCategoryRepository dataFormCategoryRepository)
            {
                this.dataFormCategoryRepository = dataFormCategoryRepository;
            }

            public IList<DataFormCategoryVM> GetDataFormCategories()
            {
                IList<DataFormCategoryVM> result = new List<DataFormCategoryVM>();
                return result;
            }

            public IList<DataFormSubCategoryVM> GetDataFormSubCategories()
            {
                IList<DataFormSubCategoryVM> result = new List<DataFormSubCategoryVM>();
                return result;
            }
            public IList<DataFormTypeVM> GetDataFormTypes()
            {
                IList<DataFormTypeVM> result = new List<DataFormTypeVM>();
                return result;
            }

            public IList<SynchronizationRegistryVM> GetSynchronizationRegistries()
            {
                IList<SynchronizationRegistryVM> result = new List<SynchronizationRegistryVM>();
                return result;
            }
        }
}
