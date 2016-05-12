using System.Windows;
using Apv.MemberExcel.Services;

namespace Apv.MemberExcel
{
    public partial class App
    {
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            SettingsService.Save();
        }
    }
}
