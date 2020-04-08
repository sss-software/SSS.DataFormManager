using SSS.DataFormManager.DAL.Contexts;
using SSS.DataFormManager.DAL.Models;
using SSS.DataFormManager.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSS.DataFormManager.DAL.Repositories
{
    public class DataFormRepository : IDataFormRepository
    {
        public DataFormRepository()
        {
            try
            {
                using (var context = new DataFormDataContext())
                {
                    context.Database.CreateIfNotExists();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public bool AddDataFormCategory(DataFormCategory dataFormCategory)
        {
            bool result = false;
            using (var context = new DataFormDataContext())
            {
                context.DataFormCategories.Add(dataFormCategory);
                int save = context.SaveChanges();
                if (save > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool UpdateDataFormCategory(DataFormCategory dataFormCategory)
        {
            bool result = false;
            using (var context = new DataFormDataContext())
            {
                var category = context.DataFormCategories.Where(x => x.DataFormCategoryId == dataFormCategory.DataFormCategoryId).FirstOrDefault();
                if (category != null)
                {
                    category = dataFormCategory;
                    int save = context.SaveChanges();
                    if (save > 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool RemoveDataFormCategory(DataFormCategory dataFormCategory)
        {
            bool result = false;
            using (var context = new DataFormDataContext())
            {
                var category = context.DataFormCategories.Where(x => x.DataFormCategoryId == dataFormCategory.DataFormCategoryId).FirstOrDefault();
                if (category != null)
                {
                    context.DataFormCategories.Remove(category);
                    int save = context.SaveChanges();
                    if (save > 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool AddDataFormSubCategory(DataFormSubCategory dataFormSubCategory)
        {
            bool result = false;
            using (var context = new DataFormDataContext())
            {
                context.DataFormSubCategories.Add(dataFormSubCategory);
                int save = context.SaveChanges();
                if (save > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool UpdateDataFormSubCategory(DataFormSubCategory dataFormSubCategory)
        {
            bool result = false;
            using (var context = new DataFormDataContext())
            {
                var subCategory = context.DataFormSubCategories.Where(x => x.DataFormSubCategoryId == dataFormSubCategory.DataFormSubCategoryId).FirstOrDefault();
                if (subCategory != null)
                {
                    subCategory = dataFormSubCategory;
                    int save = context.SaveChanges();
                    if (save > 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool RemoveDataFormCategory(DataFormSubCategory dataFormSubCategory)
        {
            bool result = false;
            using (var context = new DataFormDataContext())
            {
                var subCategory = context.DataFormSubCategories.Where(x => x.DataFormSubCategoryId == dataFormSubCategory.DataFormSubCategoryId).FirstOrDefault();
                if (subCategory != null)
                {
                    context.DataFormSubCategories.Remove(subCategory);
                    int save = context.SaveChanges();
                    if (save > 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool AddDataFormType(DataFormType dataFormType)
        {
            bool result = false;
            using (var context = new DataFormDataContext())
            {
                context.DataFormTypes.Add(dataFormType);
                int save = context.SaveChanges();
                if (save > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool UpdateDataFormType(DataFormType dataFormType)
        {
            bool result = false;
            using (var context = new DataFormDataContext())
            {
                var dtf = context.DataFormTypes.Where(x => x.DataFormTypeId == dataFormType.DataFormTypeId).FirstOrDefault();
                if (dtf != null)
                {
                    dtf = dataFormType;
                    int save = context.SaveChanges();
                    if (save > 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool RemoveDataFormType(DataFormType dataFormType)
        {
            bool result = false;
            using (var context = new DataFormDataContext())
            {
                var dtf = context.DataFormTypes.Where(x => x.DataFormTypeId == dataFormType.DataFormTypeId).FirstOrDefault();
                if (dtf != null)
                {
                    context.DataFormTypes.Remove(dtf);
                    int save = context.SaveChanges();
                    if (save > 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool AddSynchronizationRegistry(SynchronizationRegistry synchronizationRegistry)
        {
            bool result = false;
            using (var context = new DataFormDataContext())
            {
                context.SynchronizationRegister.Add(synchronizationRegistry);
                int save = context.SaveChanges();
                if (save > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public ObservableCollection<DataFormCategory> GetDataFormCategories()
        {
            ObservableCollection<DataFormCategory> result = null;
            using (var context = new DataFormDataContext())
            {
                var categories = context.DataFormCategories.Where(x => x.IsActive == true).ToList<DataFormCategory>();
                if (categories != null)
                {
                    result = new ObservableCollection<DataFormCategory>(categories);
                }
            }
            return result;
        }

        public ObservableCollection<DataFormSubCategory> GetDataFormSubCategories()
        {
            ObservableCollection<DataFormSubCategory> result = null;
            using (var context = new DataFormDataContext())
            {
                var subCategories = context.DataFormSubCategories.Where(x => x.IsActive == true).ToList<DataFormSubCategory>();
                if (subCategories != null)
                {
                    result = new ObservableCollection<DataFormSubCategory>(subCategories);
                }
            }
            return result;
        }

        public ObservableCollection<DataFormType> GetDataFormTypes()
        {
            ObservableCollection<DataFormType> result = null;
            using (var context = new DataFormDataContext())
            {
                var formTypes = context.DataFormTypes.Where(x => x.IsActive == true).ToList<DataFormType>();
                if (formTypes != null)
                {
                    result = new ObservableCollection<DataFormType>(formTypes);
                }
            }
            return result;
        }

        public ObservableCollection<SynchronizationRegistry> GetSynchronizationRegistries()
        {
            ObservableCollection<SynchronizationRegistry> result = null;
            using (var context = new DataFormDataContext())
            {
                var registries = context.SynchronizationRegister.Where(x => x.IsActive == true).ToList<SynchronizationRegistry>();
                if (registries != null)
                {
                    result = new ObservableCollection<SynchronizationRegistry>(registries);
                }
            }
            return result;
        }
    }
}
