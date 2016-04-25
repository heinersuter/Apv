using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Apv.MemberExcel.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Apv.MemberExcel.Pdfs
{
    public class FullMailingLetter : Letter
    {
        public static void Write(IEnumerable<AddressDto> addresses, string filePath)
        {
            if (!addresses.Any())
            {
                return;
            }

            using (var document = new Document())
            {
                var writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.SetPageSize(PageSize.A4);
                document.SetMargins(Mm(20), Mm(20), Mm(20), Mm(20));
                document.Open();
                writer.PageEvent = new PdfPageEventHelper();

                foreach (var dto in addresses)
                {
                    AddLogo(document);
                    AddAddress(dto, document);
                    AddDate(document);
                    AddSalutation(dto.Gender, dto.Nickname ?? dto.Firstname, document);
                    AddContent(dto.Gender, document);
                    AddGreetings(document);
                    AddSender(document, writer);

                    document.NewPage();
                }
            }
        }

        private static void AddContent(Gender gender, Document document)
        {
            var paragraph = new Paragraph { Font = Font12 };
            SetLeading(paragraph);

            var plural = gender == Gender.Family;
            paragraph.Add($"In diesem Covert {(plural ? "findet ihr" : "findest du")} den Jahresversand vom APV für 2016.");
            paragraph.Add(Environment.NewLine);
            paragraph.Add($"Da ich {(plural ? "eure" : "deine")} E-Mail-Adresse nicht habe, schicke ich alles per Post. ");
            paragraph.Add($"Falls {(plural ? "ihr" : "du")} eine E-Mail-Adresse {(plural ? "habt" : "hast")} und mir diese mitteilen {(plural ? "wollt" : "willst")}, schicke ich nächstes Jahr das Jahresprogramm und das Protokoll per E-Mail. ");
            paragraph.Add(Environment.NewLine);
            paragraph.Add($"Details zu den einzelnen Anlässen werden sowieso nur noch per E-Mail verschickt. Ich hoffe {(plural ? "ihr versteht" : "du verstehst")} das. ");

            document.Add(paragraph);
            document.Add(Chunk.NEWLINE);
        }
    }
}
