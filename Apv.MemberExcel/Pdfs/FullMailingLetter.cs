using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Apv.MemberExcel.Pdfs
{
    public class FullMailingLetter : Letter
    {
        public static void Write(IEnumerable<LetterAddress> addresses, string filePath)
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
                    AddContent(address.Gender, document, address.Mobiles.Any());
                    AddGreetings(document);
                    AddSender(document, writer);

                    document.NewPage();
                }
            }
        }

        private static void AddContent(LetterGender gender, Document document, bool anyMobileNumber)
        {
            var paragraph = new Paragraph { Font = FontNormal };
            SetLeading(paragraph);
            var plural = gender == LetterGender.Family;

            // Jahresprogramm
            paragraph.Add($"Das Jahresprogramm {DateTime.Now:yyyy} ist fertig! Ich hoffe, da ist was für {(plural ? "euch" : "dich")} dabei. ");
            paragraph.Add(Environment.NewLine);
            paragraph.Add(Environment.NewLine);

            // Mitgliederbeitrag
            paragraph.Add($"Zudem {(plural ? "findet ihr" : "findest du")} auch den Einzahlungsschein für den Mitgliederbeitrag im Couvert. ");
            paragraph.Add(Environment.NewLine);
            paragraph.Add("Der Jahresbeitrag ist CHF 30.-, respektive CHF 50.- für Familien.");
            paragraph.Add(Environment.NewLine);
            paragraph.Add("Danke fürs Einzahlen.");
            paragraph.Add(Environment.NewLine);
            paragraph.Add("(Für aktive Leiter der Abteilung ist der Mitgliederbeitrag freiwillig.)");
            paragraph.Add(Environment.NewLine);
            paragraph.Add(Environment.NewLine);

            // Email / WhatsApp
            paragraph.Add($"Da ich {(plural ? "eure" : "deine")} E-Mail-Adresse nicht habe, schicke ich alles per Post. ");
            paragraph.Add(Environment.NewLine);
            paragraph.Add($"Bitte {(plural ? "teilt mir eure" : "teile mir deine")} E-mail-Adresse mit (falls vorhanden). ");
            paragraph.Add("Dann schicke ich nächstes Jahr das Jahresprogramm und das Protokoll per E-Mail. ");
            paragraph.Add(Environment.NewLine);
            paragraph.Add("Details zu den einzelnen Anlässen werden nur noch per E-Mail und WhatsApp verschickt. ");
            if (!anyMobileNumber)
            {
                paragraph.Add($"Wenn {(plural ? "ihr mir eure" : "du mir deine")} Handy-Nummer {(plural ? "mitteilt" : "mitteilst")}, nehme ich {(plural ? "euch" : "dich")} gerne in die WhatsApp-Gruppe auf. ");
            }

            document.Add(paragraph);
            document.Add(Chunk.NEWLINE);
        }
    }
}
