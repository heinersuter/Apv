using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Apv.MemberExcel.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Apv.MemberExcel.Pdfs
{
    public class DepositSlipLetter : Letter
    {
        public static void Write(IEnumerable<AddressDto> addresses, string filePath, bool requiresDepositSlipUnknown)
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
                    AddContent(dto.Gender, document, requiresDepositSlipUnknown);
                    AddGreetings(document);
                    AddSender(document, writer);

                    document.NewPage();
                }
            }
        }

        private static void AddContent(Gender gender, Document document, bool requiresDepositSlipUnknown)
        {
            var paragraph = new Paragraph { Font = Font12 };
            SetLeading(paragraph);

            var plural = gender == Gender.Family;
            paragraph.Add($"In diesem Covert {(plural ? "findet ihr" : "findest du")} den Einzahlungsschein für den Mitgliederbeitrag 2016.");
            paragraph.Add(Environment.NewLine);
            if (requiresDepositSlipUnknown)
            {
                paragraph.Add($"{(plural ? "Ihr habt" : "Du hast")} mir noch nicht mitgeteilt, ob {(plural ? "ihr" : "du")} den Einzahlungsschein in Papierform {(plural ? "braucht" : "brauchst")}. ");
                paragraph.Add($"{(plural ? "Könnt ihr" : "Kannst du")} das bitte noch melden? ");
                paragraph.Add($"{(plural ? "Wenn ihr den Einzahlungsschein wollt" : "Wenn du den Einzahlungsschein willst")}, schicke ich den gerne, gar kein Problem. ");
                paragraph.Add($"Aber wenn {(plural ? "ihr" : "du")} die Einzahlung sowieso per E-Banking {(plural ? "tätigt" : "tätigst")}, dann kann ich mir den Versand gerne auch sparen. ");
                paragraph.Add(Environment.NewLine);
            }
            paragraph.Add($"Der Rest des Jahresversandes und die Details zu den Anlässen werden {(requiresDepositSlipUnknown ? "sowieso " : String.Empty)}nur noch per E-Mail verschickt. Ich hoffe {(plural ? "ihr versteht" : "du verstehst")} das. ");

            document.Add(paragraph);
            document.Add(Chunk.NEWLINE);
        }
    }
}
