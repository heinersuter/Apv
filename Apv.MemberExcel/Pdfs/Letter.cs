using System;
using Apv.MemberExcel.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Apv.MemberExcel.Pdfs
{
    public abstract class Letter : Pdf
    {
        protected static void AddLogo(Document document, PdfWriter writer)
        {
            var paragraph = new Paragraph { Font = Font24 };

            paragraph.Add("APV Gränichen");

            document.Add(paragraph);
            document.Add(Chunk.NEWLINE);
        }

        protected static void AddAddress(AddressDto dto, Document document)
        {
            var paragraph = new Paragraph { Font = Font11 };
            SetLeading(paragraph);
            SetIndentation(paragraph);
            paragraph.SpacingBefore = Mm(16);

            paragraph.Add($"{dto.Lastname} {dto.Firstname} / {dto.Nickname}");
            paragraph.Add(Environment.NewLine);
            paragraph.Add(dto.AddressLine1);
            paragraph.Add(Environment.NewLine);
            paragraph.Add($"{dto.ZipCode} {dto.City}");

            document.Add(paragraph);
            document.Add(Chunk.NEWLINE);
        }

        protected static void AddDate(Document document)
        {
            var paragraph = new Paragraph { Font = Font11 };
            SetLeading(paragraph);
            SetIndentation(paragraph);
            paragraph.SpacingBefore = Mm(16);

            paragraph.Add($"Zürich, {DateTime.Now:d}");

            document.Add(paragraph);
            document.Add(Chunk.NEWLINE);
        }

        protected static void AddSalutation(Gender gender, string name, Document document)
        {
            var paragraph = new Paragraph { Font = Font11 };
            SetLeading(paragraph);
            paragraph.SpacingBefore = Mm(16);

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

        protected static void AddGreetings(Document document)
        {
            var paragraph = new Paragraph { Font = Font11 };
            SetLeading(paragraph);

            paragraph.Add("Herzliche Grüsse");
            paragraph.Add(Environment.NewLine);
            paragraph.Add("Hirsch");

            document.Add(paragraph);
        }

        protected static void AddSender(Document document, PdfWriter writer)
        {
            var contentByte = writer.DirectContent;
            var footer = new Phrase("Hirsch | Heiner Suter | Ackersteinstrasse 207 | 8049 Zürich | 079 609 42 82 | hirsch@blaustein.ch", Font8);
            ColumnText.ShowTextAligned(
                contentByte,
                Element.ALIGN_LEFT,
                footer,
                document.LeftMargin,
                document.Bottom - 8f,
                0);
        }
    }
}
