using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Apv.MemberExcel.Pdfs
{
    public class DepositSlipLetter : Letter
    {
        public static void Write(IEnumerable<LetterAddress> addresses, string filePath, bool requiresDepositSlipUnknown)
        {
            if (!addresses.Any())
            {
                return;
            }

            using (var document = new Document())
            {
                var writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
                document.SetPageSize(PageSize.A4);
                document.SetMargins(Mm(20), Mm(20), Mm(17), Mm(20));
                document.Open();

                foreach (var address in addresses)
                {
                    AddLogo(document, writer);
                    AddAddress(address, document);
                    AddDate(document);
                    AddSalutation(address.Gender, address.CallingName, document);
                    AddContent(address.Gender, document, requiresDepositSlipUnknown);
                    AddGreetings(document);
                    AddSender(document, writer);

                    document.NewPage();
                }
            }
        }

        private static void AddContent(LetterGender gender, Document document, bool requiresDepositSlipUnknown)
        {
            var paragraph = new Paragraph { Font = FontNormal };
            SetLeading(paragraph);

            var plural = gender == LetterGender.Family;
            paragraph.Add($"In diesem Covert {(plural ? "findet ihr" : "findest du")} den Einzahlungsschein für den Mitgliederbeitrag.");
            paragraph.Add(Environment.NewLine);
            paragraph.Add("Der Jahresbeitrag ist CHF 30.-, respektive CHF 50.- für Familien.");
            paragraph.Add(Environment.NewLine);
            paragraph.Add("Danke fürs Einzahlen.");
            paragraph.Add(Environment.NewLine);
            paragraph.Add("(Für aktive Leiter der Abteilung ist der Mitgliederbeitrag freiwillig.)");
            paragraph.Add(Environment.NewLine);
            paragraph.Add(Environment.NewLine);
            if (requiresDepositSlipUnknown)
            {
                paragraph.Add($"{(plural ? "Ihr habt" : "Du hast")} mir noch nicht mitgeteilt, ob {(plural ? "ihr" : "du")} den Einzahlungsschein in Papierform {(plural ? "braucht" : "brauchst")}. ");
                paragraph.Add(Environment.NewLine);
                paragraph.Add($"{(plural ? "Könnt ihr" : "Kannst du")} das bitte noch melden? ");
                paragraph.Add(Environment.NewLine);
                paragraph.Add(Environment.NewLine);
                paragraph.Add($"{(plural ? "Wenn ihr den Einzahlungsschein wollt" : "Wenn du den Einzahlungsschein willst")}, schicke ich den gerne, gar kein Problem. ");
                paragraph.Add($"Aber wenn {(plural ? "ihr" : "du")} die Einzahlung sowieso per E-Banking {(plural ? "tätigt" : "tätigst")}, dann kann ich mir den Versand gerne auch sparen. ");
                paragraph.Add(Environment.NewLine);
            }
            paragraph.Add($"Der Rest des Jahresversandes und die Details zu den Anlässen werden {(requiresDepositSlipUnknown ? "sowieso " : string.Empty)}nur noch per E-Mail und WhatsApp verschickt. ");

            document.Add(paragraph);
            document.Add(Chunk.NEWLINE);
        }
    }
}
