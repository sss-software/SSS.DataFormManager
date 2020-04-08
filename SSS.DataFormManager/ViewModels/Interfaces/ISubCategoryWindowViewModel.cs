using SSS.DataFormManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SSS.DataFormManager.ViewModels.Interfacs
{
    public interface ISubCategoryWindowViewModel
    {
        ICommand SubCategoryManageCommand { get; set; }
        string SubCategoryButtonTitle { get; set; }
        bool IsAddAction { get; set; }
        bool IsUpdateAction { get; set; }
        bool IsRemoveAction { get; set; }
        DataFormSubCategoryDTO DataFormSubCategory { get; set; }
    }
}
