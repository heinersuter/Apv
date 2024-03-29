﻿using System;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Apv.MemberExcel.Pdfs
{
    public abstract class Letter : Pdf
    {
        protected static void AddLogo(Document document, PdfWriter writer)
        {
            var paragraph = new Paragraph { Font = FontBig };

            paragraph.Add("APV Gränichen");

            document.Add(paragraph);
            document.Add(Chunk.NEWLINE);
        }

        protected static void AddAddress(LetterAddress address, Document document)
        {
            var paragraph = new Paragraph { Font = FontNormal };
            SetLeading(paragraph);
            SetIndentation(paragraph);
            paragraph.SpacingBefore = Mm(20);

            paragraph.Add(address.AddressName);
            paragraph.Add(Environment.NewLine);
            paragraph.Add(address.AddressLine1);
            paragraph.Add(Environment.NewLine);
            paragraph.Add($"{address.ZipCode} {address.City}");

            document.Add(paragraph);
            document.Add(Chunk.NEWLINE);
        }

        protected static void AddDate(Document document)
        {
            var paragraph = new Paragraph { Font = FontNormal };
            SetLeading(paragraph);
            SetIndentation(paragraph);
            paragraph.SpacingBefore = Mm(15);

            paragraph.Add(string.Format(CultureInfo.GetCultureInfo("de-CH"), "Lenzburg, {0:d}", DateTime.Now));

            document.Add(paragraph);
            document.Add(Chunk.NEWLINE);
        }

        protected static void AddSalutation(LetterGender gender, string name, Document document)
        {
            var paragraph = new Paragraph { Font = FontNormal };
            SetLeading(paragraph);
            paragraph.SpacingBefore = Mm(15);

            switch (gender)
            {
                case LetterGender.Male:
                    paragraph.Add("Lieber ");
                    break;
                case LetterGender.Female:
                    paragraph.Add("Liebe ");
                    break;
                case LetterGender.Family:
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
            var paragraph = new Paragraph { Font = FontNormal };
            SetLeading(paragraph);

            paragraph.Add("Herzliche Grüsse");
            paragraph.Add(Environment.NewLine);
            paragraph.Add("Hirsch");

            document.Add(paragraph);
        }

        protected static void AddPostscriptum(Document document)
        {
            var paragraph = new Paragraph { Font = FontNormal };
            SetLeading(paragraph);
            paragraph.SpacingBefore = Mm(15);

            paragraph.Add("PS: Am 20. Mai ist schon der Gamblerabend.");

            document.Add(paragraph);
        }

        protected static void AddSender(Document document, PdfWriter writer)
        {
            var contentByte = writer.DirectContent;
            var footer = new Phrase("Hirsch | Heiner Suter | Zelglistrasse 12 | 5600 Lenzburg | 079 609 42 82 | heiner.suter@gmx.ch", FontSmall);
            ColumnText.ShowTextAligned(
                contentByte,
                Element.ALIGN_LEFT,
                footer,
                document.LeftMargin,
                document.Bottom - 8f,
                0);
        }

        private static string GetTitle(LetterGender gender)
        {
            switch (gender)
            {
                case LetterGender.Male:
                    return "Herr";
                case LetterGender.Female:
                    return "Frau";
                case LetterGender.Family:
                    return "Familie";
                default:
                    throw new ArgumentOutOfRangeException(nameof(gender), gender, null);
            }
        }
    }
}
