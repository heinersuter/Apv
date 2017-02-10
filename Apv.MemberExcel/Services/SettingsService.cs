using Apv.MemberExcel.Properties;

namespace Apv.MemberExcel.Services
{
    internal static class SettingsService
    {
        static SettingsService()
        {
            if (Settings.Default.UpgradeRequired)
            {
                Settings.Default.Upgrade();
                Settings.Default.UpgradeRequired = false;
            }
            Settings = Settings.Default;
        }

        public static Settings Settings { get; }

        public static void Save()
        {
            Settings.Default.Save();
        }
    }
}
