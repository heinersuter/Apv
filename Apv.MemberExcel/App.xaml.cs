using System.Windows;
using Apv.MemberExcel.Services;
using Apv.MemberExcel.Views;

namespace Apv.MemberExcel
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindowViewModel = new MainWindowViewModel();
            var mainWindowView = new MainWindowView { DataContext = mainWindowViewModel };
            mainWindowView.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            SettingsService.Save();
        }
    }
}
