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
                    AddContent(address.Gender, document, address.Mobiles.ToArray());
                    AddGreetings(document);
                    AddPostscriptum(document);
                    AddSender(document, writer);

                    document.NewPage();
                }
            }
        }

        private static void AddContent(LetterGender gender, Document document, string[] mobiles)
        {
            var paragraph = new Paragraph { Font = FontNormal };
            SetLeading(paragraph);
            var plural = gender == LetterGender.Family;

            // Jahresprogramm
            paragraph.Add($"Wir haben wieder ein Jahresprogramm zusammengestellt. {(plural ? "Ihr findet" : "Du findest")} es in diesem Couvert.");
            paragraph.Add(Environment.NewLine);
            paragraph.Add($"Falls {(plural ? "ihr" : "du")} mit dem APV lieber etwas anderes machen {(plural ? "möchtet" : "möchtest")}, {(plural ? "schickt mir euren" : "schicke mir deinen")} Vorschlag. Oder noch besser: {(plural ? "stellt eure" : "stelle deine")} Idee an der GV vor.");
            paragraph.Add(Environment.NewLine);
            paragraph.Add(Environment.NewLine);

            // Mitgliederbeitrag
            paragraph.Add($"Im Couvert {(plural ? "findet ihr" : "findest du")} auch den Einzahlungsschein für den Mitgliederbeitrag.");
            paragraph.Add(Environment.NewLine);
            paragraph.Add(Environment.NewLine);

            // Protokoll GV
            paragraph.Add("Was an der letzten GV alles besprochen wurde, kannst im Protokoll nachlesen.");
            paragraph.Add(Environment.NewLine);
            paragraph.Add("Danke an Atreju fürs protokollieren.");
            paragraph.Add(Environment.NewLine);
            paragraph.Add(Environment.NewLine);

            // Statuten
            paragraph.Add("Dieses Jahr versende ich auch einen Vorschlag für die Statuten, die wir an der GV im November annehmen wollen.");
            paragraph.Add(Environment.NewLine);
            paragraph.Add("Weil keine bisherigen Statuten mehr auffindbar sind, haben wir beschlossen neue zu erstellen. Hier sind sie!");
            paragraph.Add(Environment.NewLine);
            paragraph.Add("Falls du Änderungsvorschläge hast, melde die doch möglichst schon vor der GV bei mir.");
            paragraph.Add(Environment.NewLine);
            paragraph.Add(Environment.NewLine);

            // Whatsapp
            paragraph.Add("Als weitere Neuerung wird es dieses Jahr einen APV-Chat auf Whatsapp geben.");
            paragraph.Add(Environment.NewLine);
            if (!mobiles.Any())
            {
                paragraph.Add($"Wenn {(plural ? "ihr auch debei sein wollt, teilt mir doch eure Handy-Nummern" : "du da auch dabei sein willst, teile mir doch deine Handy-Nummer")} mit.");
            }
            else
            {
                paragraph.Add($"Ich werde {(plural ? "euch" : "dich")} mit {(mobiles.Length > 1 ? "der Nummer" : "den Nummern")} {string.Join(" / ", mobiles)} aufnehmen. ");
                paragraph.Add($"Wenn {(plural ? "ihr nicht dabei sein wollt, könnt ihr" : "du nicht dabei sein willst, kannst du")} gleich wieder austreten.");
            }
            paragraph.Add(Environment.NewLine);
            paragraph.Add(Environment.NewLine);

            // Adressen intern weitergeben
            paragraph.Add($"Damit {(plural ? "ihr euch" : "du dich")} mit anderen absprechen {(plural ? "könnt" : "kannst")} wer an welchen Anlass kommt, oder damit {(plural ? "ihr" : "du")} aus einem anderen Grund mit jemandem Kontakt aufnehmen {(plural ? "könnt" : "kannst")}, werde ich die Mitgliederliste mit Adresse, Telefon, E-Mail an alle verschicken.");
            paragraph.Add(Environment.NewLine);
            paragraph.Add($"Wenn {(plural ? "ihr" : "du")} damit nicht einverstanden {(plural ? "seit, meldet euch" : "bist, melde dich")} bitte bis Ende März bei mir.");
            paragraph.Add(Environment.NewLine);
            paragraph.Add(Environment.NewLine);

            // Email
            paragraph.Add($"Da ich {(plural ? "eure" : "deine")} E-Mail-Adresse nicht habe, schicke ich alles per Post. ");
            paragraph.Add($"Falls {(plural ? "ihr" : "du")} eine E-Mail-Adresse {(plural ? "habt" : "hast")} und mir diese mitteilen {(plural ? "wollt" : "willst")}, schicke ich nächstes Jahr das Jahresprogramm und das Protokoll per E-Mail. ");
            paragraph.Add(Environment.NewLine);
            paragraph.Add($"Details zu den einzelnen Anlässen werden sowieso nur noch per E-Mail verschickt. Ich hoffe {(plural ? "ihr versteht" : "du verstehst")} das. ");

            document.Add(paragraph);
            document.Add(Chunk.NEWLINE);
        }
    }
}
