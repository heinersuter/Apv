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
                Subject = "APV Versand 2018",
                Body = CreateBody(addressDto)
            };
            email.Attachements.Add(@"C:\Users\hsu\Google Drive\Aktuelle-Dokumente\Jahresprogramm_2018.pdf");
            email.Attachements.Add(@"C:\Users\hsu\Google Drive\Aktuelle-Dokumente\GV_Protokoll_2017.pdf");
            email.Attachements.Add(@"C:\Users\hsu\Google Drive\Aktuelle-Dokumente\APV_Adressen_2018.xlsx");
            email.Attachements.Add(@"C:\Users\hsu\Google Drive\Aktuelle-Dokumente\Mitgliederbeitrag_2018.pdf");

            return email;
        }

        private static string CreateBody(AddressDto addressDto)
        {
            var body = new StringBuilder();

            // Anrede
            body.AppendLine($"{(addressDto.Gender == Gender.Male ? "Lieber" : "Liebe")} {addressDto.Nickname ?? addressDto.Firstname}");
            body.AppendLine();

            // Jahresprogramm
            body.AppendLine("Fürs 2018 schicke ich dir einige Dateien.");
            body.AppendLine("Die wichtigste ist sicher das Jahresprogramm. Da findest du die aktuellen Anlässe.");
            body.AppendLine();

            // Protokoll GV + Digitales Archiv
            body.AppendLine("Ausserdem hast du nun das Protokoll der letzten GV.");
            body.AppendLine("Brandneu: Die Zugangsdaten fürs digitale Archiv findest du im PDF.");
            body.AppendLine("Nach dem Einloggen findest du das Archiv unter \"Für mich freigegeben\".");
            if (addressDto.Nickname == "Uranus")
            {
                body.AppendLine("Danke dir vielmals fürs protokollieren.");
            }
            body.AppendLine();

            // Statuten
            body.AppendLine("An der GV haben wir die neuen Statuten verabschiedet und unterschrieben.");
            body.AppendLine("Hier der Link dazu:");
            body.AppendLine("https://drive.google.com/open?id=1wGzh6baTCUDy_TVz1nGy4H49fbREdYdF");
            body.AppendLine();

            // Adressliste
            body.AppendLine("Zudem gibt es die aktuelle Adressliste mit allen APV-lern als Excel.");
            body.Append("Wenn du Änderungen oder Ergänzungen hast, melde das bitte bei mir.");
            if (addressDto.AddressLine1 == null)
            {
                body.Append(" Deine Adresse???");
            }
            body.AppendLine();
            if (addressDto.Birthdate == null)
            {
                body.AppendLine("Zum Beispiel fehlt dein Geburtsdatum.");
            }
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
                body.AppendLine("Bitte teile mir mit, falls du keinen papierenen Einzahlungsschein brauchst. Dann kann ich mir den Versand sparen.");
            }
            body.AppendLine();

            // WhatsApp
            if (string.IsNullOrEmpty(addressDto.Mobile))
            {
                body.AppendLine("Es existiert ein APV-Chat auf WhatsApp.");
                body.AppendLine("Wenn du da auch dabei sein willst, teile mir doch deine Handy-Nummer mit.");
                body.AppendLine();
            }

            // Kalender
            body.AppendLine("Und hier nochmals der Link auf unseren Kalender. Damit kannst du dir die APV-Termine direkt auf deinem Smartphone oder Computer anzeigen lassen.");
            body.AppendLine("https://calendar.google.com/calendar/ical/fjt6cci1dlg0mfsv26ck3oguuo%40group.calendar.google.com/public/basic.ics");
            body.AppendLine();

            // Gruss
            body.AppendLine("Herzliche Grüsse");
            body.AppendLine("Hirsch");

            return body.ToString();
        }
    }
}
