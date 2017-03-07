using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apv.MemberExcel.Services;

namespace Apv.MemberExcel.Email
{
    public class EmailWriter
    {
        public static IEnumerable<EmailDto> CreateEmails(IEnumerable<AddressDto> addressDtos)
        {
            return addressDtos.Select(CreateEmail);
        }

        private static EmailDto CreateEmail(AddressDto addressDto)
        {
            var email = new EmailDto
            {
                To = addressDto.Email1,
                Subject = "APV - Versand 2017"
            };
            email.Attachements.Add(@"C:\Users\hsu\OneDrive\APV\pdfs\Jahresprogramm_2017.pdf");
            email.Attachements.Add(@"C:\Users\hsu\OneDrive\APV\pdfs\Mitgliederbeitrag_2017.pdf");
            email.Attachements.Add(@"C:\Users\hsu\OneDrive\APV\pdfs\GV_Protokoll_2016.pdf");
            email.Attachements.Add(@"C:\Users\hsu\OneDrive\APV\pdfs\Statuten_Provisorisch.pdf");

            email.Body = CreateBody(addressDto);
            return email;
        }

        private static string CreateBody(AddressDto addressDto)
        {
            var body = new StringBuilder();

            // Anrede
            body.Append(addressDto.Gender == Gender.Male ? "Lieber " : "Liebe ");
            body.AppendLine(addressDto.Nickname ?? addressDto.Firstname);
            body.AppendLine();

            // Jahresprogramm
            body.AppendLine("Wir haben wieder ein Jahresprogramm zusammengestellt. Du findest es als PDF im Anhang.");
            body.AppendLine("Falls du mit dem APV lieber etwas anderes machen möchtest, schicke mir deinen Vorschlag. Oder noch besser: stelle deine Idee an der GV vor.");
            body.AppendLine();

            // Mitgliederbeitrag
            body.AppendLine("Im Anhang findest du auch die Zahlungsinformationen für den Mitgliederbeitrag.");
            if (addressDto.Payment == PaymentType.Email)
            {
                body.AppendLine("Danke, dass du auf die Post-Zustellung verzichtet hast.");
            }
            else if (addressDto.Payment == PaymentType.DepositSlip)
            {
                body.AppendLine("Du wirst wie abgemacht per Post einen Einzahlungsschein erhalten.");
            }
            else
            {
                body.AppendLine("Den Einzahlungsschein erhältst du auch noch per Post.");
                body.AppendLine("Bitte teile mir mit, falls du keinen papierenen Einzahlungsschein braucht. Dann kann ich mir den Versand sparen.");
            }
            body.AppendLine();

            // Protokoll GV
            body.AppendLine("Was an der letzten GV alles besprochen wurde, kannst im Protokoll nachlesen.");
            if (addressDto.Nickname == "Atreju")
            {
                body.AppendLine("Danke dir vielmals fürs protokollieren.");
            }
            else
            {
                body.AppendLine("Danke an Atreju fürs protokollieren.");
            }
            body.AppendLine();

            // Statuten
            body.AppendLine("Dieses Jahr versende ich auch einen Vorschlag für die Statuten, die wir an der GV im November annehmen wollen.");
            body.AppendLine("Weil keine bisherigen Statuten mehr auffindbar sind, haben wir beschlossen neue zu erstellen. Hier sind sie!");
            body.AppendLine("Falls du Änderungsvorschläge hast, melde die doch möglichst schon vor der GV bei mir.");
            body.AppendLine();

            // Whatsapp
            body.AppendLine("Als weitere Neuerung wird es dieses Jahr einen APV-Chat auf Whatsapp geben.");
            if (string.IsNullOrEmpty(addressDto.Mobile))
            {
                body.AppendLine("Wenn du da auch dabei sein willst, teile mir doch deine Handy-Nummer mit.");
            }
            else
            {
                body.AppendLine($"Ich werde dich mit der Nummer {addressDto.Mobile} aufnehmen. Wenn du nicht dabei sein willst, kannst du gleich wieder austreten.");
            }
            body.AppendLine();

            // Adressen intern weitergeben
            body.AppendLine("Damit du dich mit anderen absprechen kannst wer an welchen Anlass kommt, oder damit du aus einem anderen Grund mit jemandem Kontakt aufnehmen kannst, werde ich die Mitgliederliste mit Adresse, Telefon, E-Mail an alle verschicken.");
            body.AppendLine("Wenn du damit nicht einverstanden bist, melde dich bitte bis Ende März bei mir.");
            body.AppendLine();

            // Kalender
            body.AppendLine("Und hier nochmals der Link auf unseren Kalender. Damit kannst du die die APV-Termine direkt auf deinem Smartphone oder in Outlook anzeigen lassen.");
            body.AppendLine("https://calendar.google.com/calendar/ical/fjt6cci1dlg0mfsv26ck3oguuo%40group.calendar.google.com/public/basic.ics");
            body.AppendLine();

            // Gruss
            body.AppendLine("Herzliche Grüsse");
            body.AppendLine("Hirsch");

            return body.ToString();
        }
    }
}
