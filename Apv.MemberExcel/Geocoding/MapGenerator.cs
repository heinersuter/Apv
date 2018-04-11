using System.IO;
using System.Linq;
using Apv.MemberExcel.Services;
using Newtonsoft.Json;

namespace Apv.MemberExcel.Geocoding
{
    public static class MapGenerator
    {
        public static void GenerateMemberMap(string addressesExcelFile)
        {
            var addresses = ExcelService.ReadAddresses(addressesExcelFile)
                .Where(dto => dto.Status == Status.Active)
                .Where(dto => dto.GeoCode != null)
                .Select(dto => new
                {
                    dto.Nickname,
                    dto.Firstname,
                    dto.Lastname,
                    dto.AddressLine1,
                    dto.ZipCode,
                    dto.City,
                    dto.GeoCode.Value.Lat,
                    dto.GeoCode.Value.Lng
                });

            var json = JsonConvert.SerializeObject(addresses, Formatting.Indented);

            var html = File.ReadAllText(@"Geocoding\index.html");
            html = html.Replace("ARRAY_OF_LOCATIONS", json);

            var exportFile = Path.Combine(Path.GetDirectoryName(addressesExcelFile), "APV-Mitglieder-Standorte.html");
            File.WriteAllText(exportFile, html);
        }
    }
}
