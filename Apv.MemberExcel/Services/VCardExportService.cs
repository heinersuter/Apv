﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Apv.MemberExcel.Services
{
    public static class VCardExportService
    {
        public static void CreateVCardFile(string addressExcelFile)
        {
            var addressDtos = ExcelService.ReadAddresses(addressExcelFile)
                .Where(dto => dto.Status == Status.Active)
                .ToList();

            var vCards = addressDtos.Select(CreateVCard).ToList();
            SaveToSingleFile(vCards);
            SaveToManyFiles(vCards);

            CheckForUnusedPhotos(addressDtos);
        }

        private static void SaveToSingleFile(IEnumerable<string> vCards)
        {
            var stringBuilder = new StringBuilder();
            vCards.Aggregate(stringBuilder, (builder, s) => builder.Append(s));

            var exportFile = Path.Combine(FileSystemService.CurrentDocsDirectory, $"vCards_Alle_{DateTime.Now:yyyy}.vcf");
            File.WriteAllText(exportFile, stringBuilder.ToString());
        }

        private static void SaveToManyFiles(IEnumerable<string> vCards)
        {
            var exportDirectory = Path.Combine(FileSystemService.CurrentDocsDirectory, "vCards-Einzeln");
            if (Directory.Exists(exportDirectory))
            {
                foreach (var file in Directory.GetFiles(exportDirectory))
                {
                    File.Delete(file);
                }
            }

            Directory.CreateDirectory(exportDirectory);
            foreach (var vCard in vCards)
            {
                var filename = vCard.Split(new[] { Environment.NewLine }, StringSplitOptions.None)[3].Substring(3);
                filename = $"{filename.Replace(' ', '-')}_{DateTime.Now:yyyy}";
                File.WriteAllText(Path.Combine(exportDirectory, $"{filename}.vcf"), vCard);
            }
        }

        private static string CreateVCard(AddressDto dto)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("BEGIN:VCARD");
            stringBuilder.AppendLine("VERSION:3.0");
            stringBuilder.AppendLine($"N:{dto.Lastname};{dto.Firstname};;;");
            stringBuilder.AppendLine($"FN:{dto.Firstname} {dto.Lastname} ({dto.Nickname ?? "-"})");
            stringBuilder.AppendLine($"NICKNAME:{dto.Nickname ?? "-"}");
            stringBuilder.AppendLine("ORG:APV Blaustein Gränichen;");
            if (dto.AddressLine1 != null)
            {
                stringBuilder.AppendLine($"item1.ADR;TYPE=HOME;TYPE=pref:;;{dto.AddressLine1};{dto.City};;{dto.ZipCode};Schweiz");
            }
            stringBuilder.AppendLine("item1.X-ABADR:ch");
            if (dto.Birthdate?.Year != null && dto.Birthdate.Value.Month != null && dto.Birthdate.Value.Day != null)
            {
                stringBuilder.AppendLine($"BDAY;VALUE=date:{dto.Birthdate.Value.Year}-{dto.Birthdate.Value.Month.Value:D2}-{dto.Birthdate.Value.Day.Value:D2}");
            }
            if (dto.Mobile != null)
            {
                stringBuilder.AppendLine($"TEL;TYPE=CELL;TYPE=pref;TYPE=VOICE:{dto.Mobile}");
            }
            if (dto.Phone != null)
            {
                stringBuilder.AppendLine($"TEL;TYPE=HOME;TYPE=VOICE:{dto.Phone}");
            }
            if (dto.Email1 != null)
            {
                stringBuilder.AppendLine($"EMAIL;TYPE=HOME;TYPE=pref;TYPE=INTERNET:{dto.Email1}");
            }
            var photo = FindPhoto(dto);
            if (photo != null)
            {
                stringBuilder.AppendLine($"PHOTO;TYPE=JPEG;ENCODING=b:{photo}");
            }
            stringBuilder.AppendLine($"REV:{DateTime.Now.ToUniversalTime():yyyy-MM-ddTHH:mm:ssZ}");
            stringBuilder.AppendLine("END:VCARD");

            return stringBuilder.ToString();
        }

        private static string FindPhoto(AddressDto dto)
        {
            var expectedFile = Path.Combine(FileSystemService.ProfilePhotosDirectory, $"{dto.Firstname}-{dto.Lastname}-({dto.Nickname}).jpg");

            if (File.Exists(expectedFile))
            {
                using (var image = Image.FromFile(expectedFile))
                {
                    using (var m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        var base64String = Convert.ToBase64String(imageBytes);
                        return base64String;
                    }
                }
            }

            return null;
        }

        private static void CheckForUnusedPhotos(IList<AddressDto> dtos)
        {
            var photoFiles = Directory.GetFiles(FileSystemService.ProfilePhotosDirectory, "*.jpg");
            var photos = photoFiles.Select(ParsePhotoFileName);
            foreach (var (firstname, lastname, nickname) in photos)
            {
                if (!dtos.Any(dto => dto.Firstname == firstname && dto.Lastname == lastname && dto.Nickname == nickname))
                {
                    Console.WriteLine($"Photo '{firstname}-{lastname}-({nickname}).jpg' can not be assigned to an address.");
                }
            }
        }

        private static (string Firstname, string Lastname, string Nickname) ParsePhotoFileName(string filename)
        {
            var regex = new Regex(@"\\(\w+)-(\w+)-\((\w+)\)");
            var match = regex.Match(filename);
            if (match.Success)
            {
                return (match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value);
            }
            return (null, null, null);
        }
    }
}
