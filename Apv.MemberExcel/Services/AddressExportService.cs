using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace Apv.MemberExcel.Services
{
    public static class AddressExportService
    {
        public static void CreateAddressExport(string mainAddressExcelFilePath)
        {
            var addressDtos = ExcelService.ReadAddresses(mainAddressExcelFilePath)
                .Where(dto => dto.Status == Status.Active)
                .OrderBy(dto => dto.Nickname)
                .ThenBy(dto => dto.Firstname)
                .ToList();

            var exportFile = Path.Combine(Path.GetDirectoryName(mainAddressExcelFilePath), "APV-Adressen.xlsx");
            if (File.Exists(exportFile))
            {
                File.Delete(exportFile);
            }

            using (var package = new ExcelPackage(new FileInfo(exportFile)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Adressen");

                worksheet.Cells[1, 1].Value = "Pfadiname";
                worksheet.Cells[1, 2].Value = "Vorname";
                worksheet.Cells[1, 3].Value = "Nachname";
                worksheet.Cells[1, 4].Value = "Geburtsdatum";
                worksheet.Cells[1, 5].Value = "E-Mail";
                worksheet.Cells[1, 6].Value = "Telefon";
                worksheet.Cells[1, 7].Value = "Mobil-Telefon";
                worksheet.Cells[1, 8].Value = "Adresse";
                worksheet.Cells[1, 9].Value = "PLZ";
                worksheet.Cells[1, 10].Value = "Ort";
                worksheet.Cells[1, 11].Value = "Funktion";

                worksheet.Cells[1, 1, 1, 11].Style.Font.Bold = true;
                worksheet.Cells[1, 1, 1, 11].AutoFilter = true;

                for (var i = 0; i < addressDtos.Count; i++)
                {
                    var dto = addressDtos[i];

                    worksheet.Cells[2 + i, 1].Value = dto.Nickname;
                    worksheet.Cells[2 + i, 2].Value = dto.Firstname;
                    worksheet.Cells[2 + i, 3].Value = dto.Lastname;
                    worksheet.Cells[2 + i, 4].Value = dto.Birthdate != null ? $"{dto.Birthdate?.Day:D2}.{dto.Birthdate?.Month:D2}.{dto.Birthdate?.Year}" : null;
                    worksheet.Cells[2 + i, 5].Value = dto.Email1;
                    worksheet.Cells[2 + i, 6].Value = dto.Phone;
                    worksheet.Cells[2 + i, 7].Value = dto.Mobile;
                    worksheet.Cells[2 + i, 8].Value = dto.AddressLine1;
                    worksheet.Cells[2 + i, 9].Value = dto.ZipCode;
                    worksheet.Cells[2 + i, 10].Value = dto.City;
                    worksheet.Cells[2 + i, 11].Value = CreateFunctionField(dto);
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                package.Save();
            }
        }

        private static string CreateFunctionField(AddressDto dto)
        {
            if (dto.Functions != null && dto.FunctionsScouts != null)
            {
                return $"{dto.Functions}, {dto.FunctionsScouts} Abteilung";
            }
            if (dto.FunctionsScouts != null)
            {
                return $"{dto.FunctionsScouts} Abteilung";
            }
            return dto.Functions;
        }
    }
}
