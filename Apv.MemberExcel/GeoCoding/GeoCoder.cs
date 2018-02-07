using System.Linq;
using System.Net.Http;
using System.Web;
using Apv.MemberExcel.Services;
using Newtonsoft.Json.Linq;

namespace Apv.MemberExcel.Geocoding
{
    public class Geocoder
    {
        public static void LoadAdressesAndUpdateMissingGeoCodes(string addressesExcelFile)
        {
            var addresses = ExcelService.ReadAddresses(addressesExcelFile)
                .Where(dto => dto.Status == Status.Active
                    && dto.AddressLine1 != null
                    && dto.ZipCode != null
                    && dto.City != null
                    && dto.GeoCode == null).ToList();

            foreach (var address in addresses)
            {
                ToLocation(address);
            }

            ExcelService.UpdateGeoCodes(addressesExcelFile, addresses);
        }

        private static void ToLocation(AddressDto addressDto)
        {
            var address = $"{addressDto.AddressLine1}, {addressDto.ZipCode} {addressDto.City}";
            var encodedAddress = HttpUtility.UrlEncode(address);

            //Logger.Debug($"Check {encodedAddress}");
            using (var client = new HttpClient())
            {
                string json;
                try
                {
                    var apiKey = "";
                    json = client.GetStringAsync($"https://maps.googleapis.com/maps/api/geocode/json?address={encodedAddress}&key={apiKey}").Result;
                }
                catch// (Exception ex)
                {
                    //Logger.Error(ex);
                    return;
                }
                var obj = JObject.Parse(json);
                if ((string)obj["status"] == "OK" && ((JArray)obj["results"]).Count == 1)
                {
                    var location = ((JArray)obj["results"]).Single()["geometry"]["location"];
                    var lat = (double)location["lat"];
                    var lng = (double)location["lng"];

                    addressDto.GeoCode = new GeoCode(lat, lng);
                }
                //Logger.Warn($"Status: {(string)obj["status"]} {((JArray)obj["results"]).Count}");
            }
        }
    }
}
