using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Apv.MemberExcel.Pdfs
{
    public class Envelope : Pdf
    {
        public static void Write(IEnumerable<LetterAddress> addresses, string filePath)
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

                foreach (var address in addresses)
                {
                    AddSenderParagraph(document);
                    AddAddressParagraph(address, document);
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

        private static void AddAddressParagraph(LetterAddress address, Document document)
        {
            var paragraph = new Paragraph { Font = FontFactory.GetFont(FontFactory.HELVETICA, 12f) };
            paragraph.SetLeading(0f, 1.4f);
            paragraph.IndentationLeft = Mm(90);
            paragraph.SpacingBefore = Mm(70);

            paragraph.Add(address.AddressName);
            paragraph.Add(Environment.NewLine);
            paragraph.Add(address.AddressLine1);
            paragraph.Add(Environment.NewLine);
            paragraph.Add($"{address.ZipCode} {address.City}");
            document.Add(paragraph);
        }
    }
}
