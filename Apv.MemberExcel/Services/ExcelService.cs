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
                    if (worksheet.Cells[addressDto.RowIndex, ExcelColumn.GeoCode.Index()].Text != addressDto.GeoCode?.ToString())
                    {
                        worksheet.Cells[addressDto.RowIndex, ExcelColumn.GeoCode.Index()].Value = addressDto.GeoCode?.ToString();
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
                    if (worksheet.Cells[addressDto.RowIndex, ExcelColumn.ProfilePhoto.Index()].Text != addressDto.ProfilePhoto)
                    {
                        worksheet.Cells[addressDto.RowIndex, ExcelColumn.ProfilePhoto.Index()].Value = addressDto.ProfilePhoto;
                    }
                }
                package.Save();
            }
        }

        private static AddressDto ReadLine(int rowIndex, ExcelWorksheet worksheet)
        {
            var hasAnyValue = false;
            var dto = new AddressDto();
            foreach (var excelColumn in ExcelColumnExtensions.All())
            {
                var value = worksheet.Cells[rowIndex, excelColumn.Index()].Text;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    hasAnyValue = true;
                    MapClumnToProperty(dto, excelColumn, value);
                }
            }

            return hasAnyValue ? dto : null;
        }

        private static void MapClumnToProperty(AddressDto dto, ExcelColumn excelColumn, string value)
        {
            switch (excelColumn)
            {
                case ExcelColumn.Status:
                    dto.Status = value == "Aktiv" ? Status.Active : Status.Inactive;
                    break;
                case ExcelColumn.Nickname:
                    dto.Nickname = value;
                    break;
                case ExcelColumn.Lastname:
                    dto.Lastname = value;
                    break;
                case ExcelColumn.Firstname:
                    dto.Firstname = value;
                    break;
                case ExcelColumn.Phone:
                    dto.Phone = value;
                    break;
                case ExcelColumn.Mobile:
                    dto.Mobile = value;
                    break;
                case ExcelColumn.Comment:
                    dto.Comment = value;
                    break;
                case ExcelColumn.Email1:
                    dto.Email1 = value;
                    break;
                case ExcelColumn.Email2:
                    dto.Email2 = value;
                    break;
                case ExcelColumn.AddressLine1:
                    dto.AddressLine1 = value;
                    break;
                case ExcelColumn.ZipCode:
                    dto.ZipCode = value;
                    break;
                case ExcelColumn.City:
                    dto.City = value;
                    break;
                case ExcelColumn.GeoCode:
                    dto.GeoCode = GeoCode.Parse(value);
                    break;
                case ExcelColumn.Functions:
                    dto.Functions = value;
                    break;
                case ExcelColumn.ScoutFunctions:
                    dto.FunctionsScouts = value;
                    break;
                case ExcelColumn.Gender:
                    dto.Gender = value == "m" ? Gender.Male : Gender.Female;
                    break;
                case ExcelColumn.Birthdate:
                    dto.Birthdate = Date.Parse(value);
                    break;
                case ExcelColumn.Payment:
                    dto.Payment = value == "Papier" ? PaymentType.DepositSlip : value == "E-Mail" ? PaymentType.Email : (PaymentType?)null;
                    break;
                case ExcelColumn.ResignDate:
                    dto.ResignDate = Date.Parse(value);
                    break;
                case ExcelColumn.FamilyId:
                    dto.FamilyId = int.Parse(value);
                    break;
                case ExcelColumn.WhatsApp:
                    dto.WhatsApp = value;
                    break;
                case ExcelColumn.GoogleAccount:
                    dto.GoogleAccount = value;
                    break;
                case ExcelColumn.JoinDate:
                    dto.JoinDate = Date.Parse(value);
                    break;
                case ExcelColumn.ProfilePhoto:
                    dto.ProfilePhoto = value;
                    break;
                case ExcelColumn.OldLastname:
                    dto.OldLastname = value;
                    break;
            }
        }
    }
}
