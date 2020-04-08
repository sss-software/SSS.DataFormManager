using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSS.DataFormDataService.Models
{
    public class DataFormTypeVM
    {
        public long DataFormTypeId { get; set; }
        public string FormTypeName { get; set; }
        public string FormTypeNameDescription { get; set; }
        public bool IsActive { get; set; }
    }
}
