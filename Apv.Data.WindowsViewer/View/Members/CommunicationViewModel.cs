using System.Collections.Generic;

using Alsolos.Commons.Wpf.Mvvm;

using Apv.Data.Dtos;

namespace Apv.Data.WindowsViewer.View.Members
{
    public class CommunicationViewModel : ViewModel
    {
        public MemberDetailsDto Member
        {
            get { return BackingFields.GetValue<MemberDetailsDto>(); }
            set { BackingFields.SetValue(value, OnMemberChanged); }
        }

        public IEnumerable<PhoneNumberDto> WhatsAppPhoneNumbers
        {
            get { return BackingFields.GetValue<IEnumerable<PhoneNumberDto>>(); }
            set { BackingFields.SetValue(value); }
        }

        private void OnMemberChanged(MemberDetailsDto obj)
        {
            WhatsAppPhoneNumbers = Member.PhoneNumbers;
        }
    }
}
