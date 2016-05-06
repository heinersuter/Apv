using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Apv.MemberExcel.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Apv.MemberExcel.Pdfs
{
    public class Envelope : Pdf
    {
        public static void Write(IEnumerable<AddressDto> addresses, string filePath)
        {
            if (!addresses.Any())
            {
                return;
            }

            using (var document = new Document())
            {
                PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.SetPageSize(new Rectangle(Mm(229), Mm(162)));
                document.SetMargins(Mm(10), Mm(10), Mm(10), Mm(10));
                document.Open();

                foreach (var dto in addresses)
                {
                    AddSenderParagraph(document);
                    AddAddressParagraph(dto, document);
                    document.NewPage();
                }
            }
        }

        private static void AddSenderParagraph(Document document)
        {
            var paragraph = new Paragraph { Font = FontFactory.GetFont(FontFactory.HELVETICA, 8f) };
            paragraph.SetLeading(0f, 1.4f);

            paragraph.Add("Heiner Suter / Hirsch");
            paragraph.Add(Environment.NewLine);
            paragraph.Add("Ackersteinstrasse 207");
            paragraph.Add(Environment.NewLine);
            paragraph.Add("8049 Zürich");
            document.Add(paragraph);
        }

        private static void AddAddressParagraph(AddressDto dto, Document document)
        {
            var paragraph = new Paragraph { Font = FontFactory.GetFont(FontFactory.HELVETICA, 12f) };
            paragraph.SetLeading(0f, 1.4f);
            paragraph.IndentationLeft = Mm(90);
            paragraph.SpacingBefore = Mm(70);

            paragraph.Add($"{dto.Firstname} {dto.Lastname} / {dto.Nickname}");
            paragraph.Add(Environment.NewLine);
            paragraph.Add(dto.AddressLine1);
            paragraph.Add(Environment.NewLine);
            paragraph.Add($"{dto.ZipCode} {dto.City}");
            document.Add(paragraph);
        }
    }
}
