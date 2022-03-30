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
                Subject = $"APV Versand {DateTime.Now.Year}",
                Body = CreateBody(addressDto)
            };
            email.Attachements.Add(Path.Combine(FileSystemService.CurrentDocsDirectory, $"Jahresprogramm_{DateTime.Now.Year}.pdf"));
            //email.Attachements.Add(Path.Combine(FileSystemService.CurrentDocsDirectory, $"GV_Protokoll_{DateTime.Now.Year - 1}.pdf"));
            email.Attachements.Add(Path.Combine(FileSystemService.CurrentDocsDirectory, $"Adressen_{DateTime.Now.Year}.xlsx"));
            email.Attachements.Add(Path.Combine(FileSystemService.CurrentDocsDirectory, $"Mitgliederbeitrag_{DateTime.Now.Year}.pdf"));

            return email;
        }

        private static string CreateBody(AddressDto addressDto)
        {
            var body = new StringBuilder();

            // Anrede
            body.AppendLine($"{(addressDto.Gender == Gender.Male ? "Lieber" : "Liebe")} {addressDto.Nickname ?? addressDto.Firstname}");
            body.AppendLine();

            // Jahresprogramm
            body.AppendLine($"Fürs {DateTime.Now:yyyy} schicke ich dir das Jahresprogramm mit Online-Kalender-Link, Infos zum Mitgliederbeitrag und die aktuelle Adressliste.");

            //// Protokoll GV
            //if (addressDto.Nickname == "Uranus") body.AppendLine("Danke dir vielmals fürs protokollieren.");
            //body.AppendLine();

            // Adressliste
            if (addressDto.AddressLine1 == null)
            {
                body.AppendLine("Deine Post-Adresse fehlt in der Adressliste.");
            }
            body.AppendLine();

            // Mitgliederbeitrag
            if (addressDto.Payment == PaymentType.DepositSlip)
            {
                body.AppendLine("Du wirst wie abgemacht per Post einen Einzahlungsschein für den Mitgliederbeitrag erhalten. Du kannst mir melden, falls du den nicht mehr brauchst.");
                body.AppendLine();
            }
            //else if (addressDto.Payment == null)
            //{
            //    body.AppendLine("Den Einzahlungsschein erhältst du auch noch per Post.");
            //    body.AppendLine("WICHTIG: Bitte teile mir mit, ob du keinen papierenen Einzahlungsschein brauchst! Dann kann ich mir den Versand sparen.");
            //    body.AppendLine();
            //}

            //// WhatsApp
            //if (addressDto.Mobile == null)
            //{
            //    body.Append("Es existiert ein APV-Chat auf WhatsApp. ");
            //    body.AppendLine("Teile mir doch deine Mobile-Nummer mit, wenn du da auch dabei sein willst.");
            //    body.AppendLine();
            //}

            //// Kalender
            //body.Append("Und hier der Link auf unseren Kalender: ");
            //body.AppendLine("https://calendar.google.com/calendar/ical/apv.admin%40blaustein.ch/public/basic.ics");
            //body.AppendLine();

            body.AppendLine("An der GV im Herbst werde ich mein Amt als APV-Präsident abgeben. Der Vorstand ist etwas am Überaltern.");
            body.AppendLine("Wenn du im Vorstand mitmachen möchtest, kannst du mir auch schon vor der GV Bescheid geben.");
            body.AppendLine();

            // Digitales Archiv
            if (addressDto.GoogleAccount == null)
            {
                body.Append("Der Zugang zu unserem digitalen Archiv (mit vielen Fotos) funktioniert mit einem Google-Account. ");
                body.AppendLine("Wenn du mir deinen Google-Account mitteilst, erteile ich dir gerne die Zugriffsberechtigung.");
                body.AppendLine();
            }

            // Gruss
            body.AppendLine("Herzliche Grüsse");
            body.AppendLine("Hirsch");

            return body.ToString();
        }
    }
}