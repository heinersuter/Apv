using System.IO;
using System.Linq;
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
                .Where(dto => dto.Status == Status.Active && !string.IsNullOrEmpty(dto.Mobile))
                .ToList();

            foreach (var dto in addressDtos)
            {
                File.WriteAllText(
                    $"{dto.Nickname}.{dto.Firstname}.{dto.Lastname}.vcf",
                    $@"BEGIN:VCARD
VERSION:3.0
N:{dto.Lastname};{dto.Firstname};;;
FN:{dto.Firstname} {dto.Lastname}
NICKNAME:{dto.Nickname}
item1.ADR;TYPE=HOME;TYPE=pref:;;{dto.AddressLine1};{dto.City};;{dto.ZipCode};Schweiz
item1.X-ABADR:ch
TEL;TYPE=CELL;TYPE=pref;TYPE=VOICE:{dto.Mobile}
EMAIL;TYPE=HOME;TYPE=pref;TYPE=INTERNET:{dto.Email1}
PRODID:-//Apple Inc.//iCloudWeb Address Book 17C77//EN
REV:2017-04-26T15:43:00Z
END:VCARD
");
            }
        }
    }
}

// BDAY;VALUE=date:{dto.Birthdate.Value.Year}-{dto.Birthdate.Value.Month.Value.ToString("D2")}-{dto.Birthdate.Value.Day.Value.ToString("D2")}