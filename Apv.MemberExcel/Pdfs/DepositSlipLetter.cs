﻿using System;
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
                    AddPostscriptum(document);
                    AddSender(document, writer);

                    document.NewPage();
                }
            }
        }

        private static void AddContent(LetterGender gender, Document document, bool requiresDepositSlipUnknown)
        {
            var paragraph = new Paragraph { Font = Font11 };
            SetLeading(paragraph);

            var plural = gender == LetterGender.Family;
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
            paragraph.Add($"Bitte {(plural ? "meldet euch" : "melde dich")} bei mir, wenn {(plural ? "ihr" : "du")} das E-Mail vom 21.03.2016 nicht bekommen {(plural ? "habt" : "hast")}. ");

            document.Add(paragraph);
            document.Add(Chunk.NEWLINE);
        }
    }
}
