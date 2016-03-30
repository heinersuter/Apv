using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Apv.MemberExcel.Services
{
    public class PdfService
    {
        public static void WritePdf(IEnumerable<AddressDto> addresses, string filePath)
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
            var sender = new Paragraph { Font = FontFactory.GetFont(FontFactory.HELVETICA, 8f) };
            sender.SetLeading(0f, 1.4f);

            sender.Add("Heiner Suter / Hirsch");
            sender.Add(Environment.NewLine);
            sender.Add("Ackersteinstrasse 207");
            sender.Add(Environment.NewLine);
            sender.Add("8049 Zürich");
            document.Add(sender);
        }

        private static void AddAddressParagraph(AddressDto dto, Document document)
        {
            var address = new Paragraph { Font = FontFactory.GetFont(FontFactory.HELVETICA, 12f) };
            address.SetLeading(0f, 1.4f);
            address.IndentationLeft = Mm(90);
            address.SpacingBefore = Mm(70);

            address.Add($"{dto.Lastname} {dto.Firstname} / {dto.Nickname}");
            address.Add(Environment.NewLine);
            address.Add(dto.AddressLine1);
            address.Add(Environment.NewLine);
            address.Add($"{dto.ZipCode} {dto.City}");
            document.Add(address);
        }

        private static float Mm(float mm)
        {
            return mm / 25.4f * 72f;
        }
    }
}
