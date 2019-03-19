using System;
using System.Collections.Generic;
using System.IO;
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
                Subject = "APV Versand 2019",
                Body = CreateBody(addressDto)
            };
            email.Attachements.Add(Path.Combine(FileSystemService.CurrentDocsDirectory, "Jahresprogramm_2019.pdf"));
            email.Attachements.Add(Path.Combine(FileSystemService.CurrentDocsDirectory, "GV_Protokoll_2018.pdf"));
            email.Attachements.Add(Path.Combine(FileSystemService.CurrentDocsDirectory, "Adressen_2019.xlsx"));
            email.Attachements.Add(Path.Combine(FileSystemService.CurrentDocsDirectory, "Mitgliederbeitrag_2019.pdf"));

            return email;
        }

        private static string CreateBody(AddressDto addressDto)
        {
            var body = new StringBuilder();

            // Anrede
            body.AppendLine($"{(addressDto.Gender == Gender.Male ? "Lieber" : "Liebe")} {addressDto.Nickname ?? addressDto.Firstname}");
            body.AppendLine();

            // Jahresprogramm
            body.AppendLine($"Fürs {DateTime.Now:yyyy} schicke ich dir einige Dateien.");
            body.AppendLine("Die wichtigste ist sicher das Jahresprogramm. Da findest du die aktuellen Anlässe.");
            body.AppendLine();

            // Protokoll GV
            body.AppendLine("Ausserdem hast du nun das Protokoll der letzten GV.");
            if (addressDto.Nickname == "Uranus")
            {
                body.AppendLine("Danke dir vielmals fürs protokollieren.");
            }
            body.AppendLine();

            // Digitales Archiv
            if (addressDto.GoogleAccount == null)
            {
                body.AppendLine("Der Zugang zu unserem digitalen Archiv (mit vielen Fotos) funktioniert nur mit einem eigenen Account bei Google.");
                body.AppendLine("Wenn du mir deinen Google-Account mitteilst, ertiele ich dir gerne die Zugriffsberechtigung.");
                body.AppendLine("Nach dem Einloggen findest du das Archiv unter \"Für mich freigegeben\".");
                body.AppendLine();
            }

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
            if (addressDto.Mobile == null)
            {
                body.AppendLine("Es existiert ein APV-Chat auf WhatsApp.");
                body.AppendLine("Wenn du da auch dabei sein willst, teile mir doch deine Handy-Nummer mit.");
                body.AppendLine();
            }

            // Kalender
            body.AppendLine("Und hier der Link auf unseren Kalender. Damit kannst du dir die APV-Termine direkt auf deinem Smartphone oder Computer anzeigen lassen.");
            body.AppendLine("https://calendar.google.com/calendar/ical/apv.admin%40blaustein.ch/public/basic.ics");
            body.AppendLine();

            // Gruss
            body.AppendLine("Herzliche Grüsse");
            body.AppendLine("Hirsch");

            return body.ToString();
        }
    }
}
