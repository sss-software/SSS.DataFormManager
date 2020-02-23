using SSS.ArchiveManagementService;
using SSS.DataFormManager.ViewModels;
using SSS.EncryptionManagementService;
using SSS.FileManagementService;
using SSS.GoogleDriveCloudService;
using SSS.SerializationManagementService;
using System.Windows;
using Unity;

namespace SSS.DataFormManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IUnityContainer container = new UnityContainer();
            container.RegisterType<IEncryptionManager, EncryptionManager>();
            container.RegisterType<IGoogleDriveCloudManager, GoogleDriveCloudManager>();
            container.RegisterType<ISerializationManager, SerializationManager>();
            container.RegisterType<IFileManager, FileManager>();
            container.RegisterType<IZipManager, ZipManager>();

            var mainWindowViewModel = container.Resolve<MainWindowViewModel>();
            var window = new MainWindow
            {
                DataContext = mainWindowViewModel
            };
            window.Show();
        }
    }
}