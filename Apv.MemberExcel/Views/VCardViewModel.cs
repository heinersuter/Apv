using System;
using System.IO;
using System.Linq;
using System.Text;
using Alsolos.Commons.Wpf.Mvvm;
using Apv.MemberExcel.Services;

namespace Apv.MemberExcel.Views
{
    public class VCardViewModel : ViewModel
    {
        private readonly LetterViewModel _letterViewModel;

        public VCardViewModel(LetterViewModel letterViewModel)
        {
            _letterViewModel = letterViewModel;
        }

        public DelegateCommand ExecuteCommand => BackingFields.GetCommand(Execute, CanExecute);

        private bool CanExecute()
        {
            return true;
        }

        private void Execute()
        {
            var addressDtos = ExcelService.ReadAddresses(_letterViewModel.ExcelFilePath)
                .Where(dto => dto.Status == Status.Active)
                .ToList();

            var stringBuilder = new StringBuilder();
            foreach (var dto in addressDtos)
            {
                CreateVCard(stringBuilder, dto);
            }
            var vCardFile = Path.Combine(Path.GetDirectoryName(this._letterViewModel.ExcelFilePath), "APV-V-Cards.vcf");
            File.WriteAllText(vCardFile, stringBuilder.ToString());
        }

        private void CreateVCard(StringBuilder stringBuilder, AddressDto dto)
        {
            stringBuilder.AppendLine("BEGIN:VCARD");
            stringBuilder.AppendLine("VERSION:3.0");
            stringBuilder.AppendLine($"N:{dto.Lastname};{dto.Firstname};;;");
            stringBuilder.AppendLine($"FN:{dto.Firstname} {dto.Lastname} ({dto.Nickname ?? "-"})");
            stringBuilder.AppendLine($"NICKNAME:{dto.Nickname ?? "-"}");
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
            stringBuilder.AppendLine("PRODID:-//Apple Inc.//iCloudWeb Address Book 17C77//EN");
            stringBuilder.AppendLine($"REV:{DateTime.Now.ToUniversalTime():yyyy-MM-ddTHH:mm:ssZ}");
            stringBuilder.AppendLine("END:VCARD");
        }
    }
}