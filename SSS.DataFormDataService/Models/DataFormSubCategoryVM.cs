using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSS.DataFormDataService.Models
{
    public class DataFormSubCategoryVM
    {
        public long SubCategoryId { get; set; }
        public string Sub { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
