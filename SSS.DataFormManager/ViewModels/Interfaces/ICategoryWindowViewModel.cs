using SSS.DataFormManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SSS.DataFormManager.ViewModels.Interfacs
{
    public interface ICategoryWindowViewModel
    {
        ICommand CategoryManageCommand { get; set; }
        string CategoryButtonTitle { get; set; }
        bool IsAddAction { get; set; }
        bool IsUpdateAction { get; set; }
        bool IsRemoveAction { get; set; }
        DataFormCategoryDTO DataFormCategory { get; set; }
    }
}
