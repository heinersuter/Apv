using NLog;

namespace ApvLocationBuilder
{
    public class Program
    {
        private static readonly Logger Logger = LogManager.GetLogger("Location");

        public static void Main(string[] args)
        {
            Logger.Info("Start");
            AddressImport.GetLocations();
            Logger.Info("End");
        }
    }
}
