using System.Collections.Generic;
using System.IO;
using System.Linq;

using OfficeOpenXml;

namespace Apv.MemberExcel.Services
{
    public class ExcelService
    {
        public IEnumerable<AddressDto> ReadAddresses(string filePath)
        {
            var addresses = new List<AddressDto>();
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var workBook = package.Workbook;
                if (workBook?.Worksheets.Count > 0)
                {
                    var worksheet = workBook.Worksheets.First();

                    var rowIndex = 2;
                    AddressDto dto;
                    while ((dto = ReadLine(rowIndex, worksheet)) != null)
                    {
                        addresses.Add(dto);
                        rowIndex++;
                    }
                    
                }
            }
            return addresses;
        }

        private AddressDto ReadLine(int rowIndex, ExcelWorksheet worksheet)
        {
            var hasAnyValue = false;
            var dto = new AddressDto();
            for (var columnIndex = 1; columnIndex <= 18; columnIndex++)
            {
                var value = worksheet.Cells[rowIndex, columnIndex].Text;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    hasAnyValue = true;
                    dto.SetValue(columnIndex, value);
                }
            }

            return hasAnyValue ? dto : null;
        }
    }
}
