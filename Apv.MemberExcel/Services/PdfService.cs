using System.Collections.Generic;
using System.IO;

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Apv.MemberExcel.Services
{
    public class PdfService
    {
        public void WritePdf(IEnumerable<AddressDto> addresses, string filePath)
        {
            using (var document = new Document())
            {
                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.SetPageSize(new Rectangle(210, 297));
                document.Open();

                foreach (var dto in addresses)
                {
                    document.Add(new Paragraph($"{dto.Lastname} {dto.Firstname} / {dto.Nickname}"));
                    document.Add(new Paragraph(dto.AddressLine1));
                    document.Add(new Paragraph($"{dto.ZipCode} {dto.City}"));
                    document.NewPage();
                }

                document.Close();
            }
        }
    }
}
