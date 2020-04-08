using SSS.DataFormManager.ViewModels.Interfacs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unity;
using Unity.Resolution;

namespace SSS.DataFormManager.Views.Windows
{
    /// <summary>
    /// Interaction logic for SubCategoryWindow.xaml
    /// </summary>
    public partial class SubCategoryWindow : Window
    {
        private readonly IUnityContainer container;
        public SubCategoryWindow(IUnityContainer container, int actionCode)
        {
            InitializeComponent();
            this.container = container;
            this.DataContext = container.Resolve<ISubCategoryWindowViewModel>(new ResolverOverride[]
                                   {
                                       new ParameterOverride("actionCode", actionCode)
                                   });
        }
    }
}
