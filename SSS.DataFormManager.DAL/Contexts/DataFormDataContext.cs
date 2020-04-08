using SSS.DataFormManager.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSS.DataFormManager.DAL.Contexts
{
    public class DataFormDataContext : DbContext
    {
        public DataFormDataContext() : base("name=DataFormManager") {
        }

        public DbSet<DataFormCategory> DataFormCategories { get; set; }
        public DbSet<DataFormSubCategory> DataFormSubCategories { get; set; }
        public DbSet<DataFormType> DataFormTypes { get; set; }
        public DbSet<SynchronizationRegistry> SynchronizationRegister { get; set; }
    }
}
