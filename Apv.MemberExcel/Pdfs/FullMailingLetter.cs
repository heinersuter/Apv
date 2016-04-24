using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Apv.MemberExcel.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Apv.MemberExcel.Pdfs
{
    public class FullMailingLetter : Pdf
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
                document.SetPageSize(new Rectangle(Mm(210), Mm(297)));
                document.SetMargins(Mm(20), Mm(20), Mm(20), Mm(20));
                document.Open();

                foreach (var dto in addresses)
                {
                    AddAddress(dto, document);
                    AddSalutation(dto.Gender, dto.Nickname ?? dto.Firstname, document);
                    AddContent(dto.Gender, document);
                    AddGreetings(document);
                    AddSender(document);
                    document.NewPage();
                }
            }
        }

        private static void AddAddress(AddressDto dto, Document document)
        {
            var paragraph = new Paragraph { Font = FontFactory.GetFont(FontFactory.HELVETICA, 12f) };
            paragraph.SetLeading(0f, 1.2f);
            paragraph.IndentationLeft = Mm(100);
            paragraph.SpacingBefore = Mm(20);

            paragraph.Add($"{dto.Lastname} {dto.Firstname} / {dto.Nickname}");
            paragraph.Add(Environment.NewLine);
            paragraph.Add(dto.AddressLine1);
            paragraph.Add(Environment.NewLine);
            paragraph.Add($"{dto.ZipCode} {dto.City}");

            document.Add(paragraph);
            document.Add(Chunk.NEWLINE);
        }

        private static void AddSalutation(Gender gender, string name, Document document)
        {
            var paragraph = new Paragraph { Font = FontFactory.GetFont(FontFactory.HELVETICA, 12f) };
            paragraph.SetLeading(0f, 1.2f);
            paragraph.SpacingBefore = Mm(20);

            switch (gender)
            {
                case Gender.Male:
                    paragraph.Add("Lieber ");
                    break;
                case Gender.Female:
                    paragraph.Add("Liebe ");
                    break;
                case Gender.Family:
                    paragraph.Add("Liebe ");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gender), gender, null);
            }
            paragraph.Add(name);
            
            document.Add(paragraph);
            document.Add(Chunk.NEWLINE);
        }

        private static void AddContent(Gender gender, Document document)
        {
            var paragraph = new Paragraph { Font = FontFactory.GetFont(FontFactory.HELVETICA, 12f) };
            paragraph.SetLeading(0f, 1.2f);

            paragraph.Add($"In diesem Covert {(gender == Gender.Family ? "findet ihr" : "findest du")} den Jahresversand vom APV für 2016.");
            paragraph.Add(Environment.NewLine);
            paragraph.Add($"Da ich {(gender == Gender.Family ? "eure" : "deine")} E-Mail-Adresse nicht habe, schicke ich alles per Post. ");
            paragraph.Add($"Falls {(gender == Gender.Family ? "ihr" : "du")} eine E-Mail-Adresse {(gender == Gender.Family ? "habt" : "hast")} und mir diese mitteilen {(gender == Gender.Family ? "wollt" : "willst")}, schicke ich nächstes Jahr das Jahresprogramm und das Protokoll per E-Mail. ");
            paragraph.Add(Environment.NewLine);
            paragraph.Add($"Details zu den einzelnen Anlässen werden sowieso nur noch per E-Mail verschickt. Ich hoffe {(gender == Gender.Family ? "ihr versteht" : "du verstehst")} das. ");

            document.Add(paragraph);
            document.Add(Chunk.NEWLINE);
        }

        private static void AddGreetings(Document document)
        {
            var paragraph = new Paragraph { Font = FontFactory.GetFont(FontFactory.HELVETICA, 12f) };
            paragraph.SetLeading(0f, 1.2f);

            paragraph.Add("Herzliche Grüsse");
            paragraph.Add(Environment.NewLine);
            paragraph.Add("Hirsch");

            document.Add(paragraph);
        }

        private static void AddSender(Document document)
        {
            var paragraph = new Paragraph { Font = FontFactory.GetFont(FontFactory.HELVETICA, 8f) };
            paragraph.SetLeading(0f, 1.2f);
            paragraph.SpacingBefore = Mm(50);

            paragraph.Add("Heiner Suter | Ackersteinstrasse 207 | 8049 Zürich | 079 609 42 82 | hirsch@blaustein.ch");

            document.Add(paragraph);
        }

        private static void AddSender(PdfWriter writer)
        {
            var cb = writer.DirectContent;
            var bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SaveState();
            cb.BeginText();
            cb.MoveText(Mm(20), Mm(300));
            cb.SetFontAndSize(bf, 8);
            cb.ShowText("Heiner Suter | Ackersteinstrasse 207 | 8049 Zürich | 079 609 42 82 | hirsch@blaustein.ch");
            cb.EndText();
            cb.RestoreState();
        }
    }
}
