using System.IO;
using System.Linq;
using System.Text;

namespace Apv.MemberExcel.Services
{
    public static class EmailExportService
    {
        public static void CreateEmailExport(string addressExcelFile)
        {
            var addressDtos = ExcelService.ReadAddresses(addressExcelFile)
                .Where(dto => dto.Status == Status.Active
                    && dto.Email1 != null)
                .ToList();

            var stringBuilder = new StringBuilder();
            foreach (var dto in addressDtos)
            {
                stringBuilder.AppendLine(dto.Email1);
            }

            var exportFile = Path.Combine(Path.GetDirectoryName(addressExcelFile), "APV-Emails.txt");
            File.WriteAllText(exportFile, stringBuilder.ToString());
        }
    }
}
