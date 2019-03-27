using System.Collections.Generic;
using System.IO;
using System.Linq;

using OfficeOpenXml;

namespace Apv.MemberExcel.Services
{
    public class ExcelService
    {
        public static IEnumerable<AddressDto> ReadAddresses(string filePath)
        {
            var addresses = new List<AddressDto>();
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var workbook = package.Workbook;
                if (!(workbook?.Worksheets.Count > 0))
                {
                    return addresses;
                }

                var worksheet = workbook.Worksheets.First();

                var rowIndex = 2;
                AddressDto dto;
                while ((dto = ReadLine(rowIndex, worksheet)) != null)
                {
                    dto.RowIndex = rowIndex;
                    addresses.Add(dto);
                    rowIndex++;
                }
            }
            return addresses;
        }

        public static void UpdateGeoCodes(string filePath, IEnumerable<AddressDto> addresses)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets.First();

                foreach (var addressDto in addresses)
                {
                    if (worksheet.Cells[addressDto.RowIndex, 13].Text != addressDto.GeoCode?.ToString())
                    {
                        worksheet.Cells[addressDto.RowIndex, 13].Value = addressDto.GeoCode?.ToString();
                    }
                }
                package.Save();
            }
        }

        public static void UpdateProfilePhoto(string filePath, IEnumerable<AddressDto> addresses)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets.First();

                foreach (var addressDto in addresses)
                {
                    if (worksheet.Cells[addressDto.RowIndex, 24].Text != addressDto.ProfilePhoto)
                    {
                        worksheet.Cells[addressDto.RowIndex, 24].Value = addressDto.ProfilePhoto;
                    }
                }
                package.Save();
            }
        }

        private static AddressDto ReadLine(int rowIndex, ExcelWorksheet worksheet)
        {
            var hasAnyValue = false;
            var dto = new AddressDto();
            for (var columnIndex = 1; columnIndex <= 24; columnIndex++)
            {
                var value = worksheet.Cells[rowIndex, columnIndex].Text;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    hasAnyValue = true;
                    MapClumnToProperty(dto, columnIndex, value);
                }
            }

            return hasAnyValue ? dto : null;
        }

        private static void MapClumnToProperty(AddressDto dto, int columnIndex, string value)
        {
            switch (columnIndex)
            {
                case 1:
                    dto.Status = value == "Aktiv" ? Status.Active : Status.Inactive;
                    break;
                case 2:
                    dto.Nickname = value;
                    break;
                case 3:
                    dto.Lastname = value;
                    break;
                case 4:
                    dto.Firstname = value;
                    break;
                case 5:
                    dto.Phone = value;
                    break;
                case 6:
                    dto.Mobile = value;
                    break;
                case 7:
                    dto.Comment = value;
                    break;
                case 8:
                    dto.Email1 = value;
                    break;
                case 9:
                    dto.Email2 = value;
                    break;
                case 10:
                    dto.AddressLine1 = value;
                    break;
                case 11:
                    dto.ZipCode = value;
                    break;
                case 12:
                    dto.City = value;
                    break;
                case 13:
                    dto.GeoCode = GeoCode.Parse(value);
                    break;
                case 14:
                    dto.Functions = value;
                    break;
                case 15:
                    dto.FunctionsScouts = value;
                    break;
                case 16:
                    dto.Gender = value == "m" ? Gender.Male : Gender.Female;
                    break;
                case 17:
                    dto.Birthdate = Date.Parse(value);
                    break;
                case 18:
                    dto.Payment = value == "Papier" ? PaymentType.DepositSlip : value == "E-Mail" ? PaymentType.Email : (PaymentType?)null;
                    break;
                case 19:
                    dto.ResignDate = Date.Parse(value);
                    break;
                case 20:
                    dto.FamilyId = int.Parse(value);
                    break;
                case 21:
                    dto.WhatsApp = value;
                    break;
                case 22:
                    dto.GoogleAccount = value;
                    break;
                case 23:
                    dto.JoinDate = Date.Parse(value);
                    break;
                case 24:
                    dto.ProfilePhoto = value;
                    break;
            }
        }
    }
}
