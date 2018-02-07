using System.IO;
using Alsolos.Commons.Wpf.Mvvm;
using Apv.MemberExcel.Geocoding;
using Apv.MemberExcel.Services;

namespace Apv.MemberExcel.Views
{
    public class GeocodeViewModel : ViewModel
    {
        private readonly LetterViewModel _letterViewModel;

        public GeocodeViewModel(LetterViewModel letterViewModel)
        {
            _letterViewModel = letterViewModel;
        }
        public string ApiKey
        {
            get { return BackingFields.GetValue(() => SettingsService.Settings.GoogleApiKey); }
            set { BackingFields.SetValue(value, key => SettingsService.Settings.GoogleApiKey = key); }
        }

        public DelegateCommand GeocodeMissingAdressesCommand => BackingFields.GetCommand(GeocodeMissingAdresses, CanGeocodeMissingAdresses);

        private bool CanGeocodeMissingAdresses()
        {
            return !string.IsNullOrWhiteSpace(ApiKey) && File.Exists(_letterViewModel.ExcelFilePath);
        }

        private void GeocodeMissingAdresses()
        {
            Geocoder.LoadAdressesAndUpdateMissingGeoCodes(_letterViewModel.ExcelFilePath);
        }
    }
}