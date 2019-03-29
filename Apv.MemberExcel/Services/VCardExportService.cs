using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Apv.MemberExcel.Services
{
    public static class VCardExportService
    {
        public static void CreateVCardFile(string addressExcelFile)
        {
            ResizeImages();

            var addressDtos = ExcelService.ReadAddresses(addressExcelFile)
                .Where(dto => dto.Status == Status.Active)
                .ToList();

            var vCards = addressDtos.Select(CreateVCard).ToList();
            SaveToSingleFile(vCards);
            SaveToManyFiles(vCards);

            UpdatePhotoInExcel(addressExcelFile, addressDtos);
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
                var fn = vCard
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                    .First(s => s.StartsWith("FN:", StringComparison.Ordinal))
                    .Substring(3);
                var name = Name.FromVCardFn(fn);
                var filename = $"{name.ToFilename()}_{DateTime.Now:yyyy}.vcf";
                File.WriteAllText(Path.Combine(exportDirectory, filename), vCard);
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
            var photo = FindPhotoAsBase64(dto);
            if (photo != null)
            {
                stringBuilder.AppendLine($"PHOTO;TYPE=JPEG;ENCODING=b:{photo}");
            }
            stringBuilder.AppendLine($"REV:{DateTime.Now.ToUniversalTime():yyyy-MM-ddTHH:mm:ssZ}");
            stringBuilder.AppendLine("END:VCARD");

            return stringBuilder.ToString();
        }

        private static string FindPhotoAsBase64(AddressDto dto)
        {
            var expectedFile = Path.Combine(FileSystemService.ProfilePhotosDirectory, $"{Name.FromDto(dto).ToFilename()}.jpg");

            if (File.Exists(expectedFile))
            {
                using (var image = Image.FromFile(expectedFile))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        image.Save(memoryStream, image.RawFormat);
                        var imageBytes = memoryStream.ToArray();
                        var base64String = Convert.ToBase64String(imageBytes);
                        return base64String;
                    }
                }
            }

            return null;
        }

        private static void UpdatePhotoInExcel(string addressExcelFile, IList<AddressDto> dtos)
        {
            var photoFiles = Directory.GetFiles(FileSystemService.ProfilePhotosDirectory, "*.jpg");
            foreach (var photoFile in photoFiles)
            {
                var name = Name.FromProfilePhotoFile(photoFile);
                if (name != null)
                {
                    var dto = dtos.FirstOrDefault(name.IsMatch);

                    if (dto != null)
                    {
                        dto.ProfilePhoto = Path.GetFileName(photoFile);
                    }
                    else
                    {
                        Console.WriteLine($"Profile Photo '{Path.GetFileName(photoFile)}' can not be assigned to an address.");
                    }
                }
                else
                {
                    Console.WriteLine($"Profile photo filename does not match the expected naming pattern: {Path.GetFileName(photoFile)}");
                }
            }

            ExcelService.UpdateProfilePhoto(addressExcelFile, dtos);
        }

        private static void ResizeImages()
        {
            var photoFiles = Directory.GetFiles(FileSystemService.ProfilePhotosDirectory, "*.jpg");
            const int size = 128;
            var destRect = new Rectangle(0, 0, size, size);

            foreach (var file in photoFiles)
            {
                using (var destImage = new Bitmap(size, size))
                {
                    destImage.SetResolution(72.0f, 72.0f);

                    using (var image = Image.FromFile(file))
                    {
                        using (var graphics = Graphics.FromImage(destImage))
                        {
                            graphics.CompositingMode = CompositingMode.SourceCopy;
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                            using (var wrapMode = new ImageAttributes())
                            {
                                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel,
                                    wrapMode);
                            }
                        }
                    }

                    destImage.Save(file, ImageFormat.Jpeg);
                }
            }
        }
    }
}
