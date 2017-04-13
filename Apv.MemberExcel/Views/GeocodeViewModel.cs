using Alsolos.Commons.Wpf.Mvvm;

namespace Apv.MemberExcel.Views
{
    public class GeocodeViewModel : ViewModel
    {
        public string ApiKey
        {
            get => BackingFields.GetValue<string>();
            set => BackingFields.SetValue(value);
        }

        public DelegateCommand GeocodeMissingAdressesCommand => BackingFields.GetCommand(GeocodeMissingAdresses, CanGeocodeMissingAdresses);

        private bool CanGeocodeMissingAdresses()
        {
            return true;
        }

        private void GeocodeMissingAdresses()
        {
        }
    }
}