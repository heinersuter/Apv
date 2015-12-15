using System.Collections.Generic;

namespace Apv.Data.Dtos
{
    public class MemberDetailsDto : MemberDto
    {
        public MemberDetailsDto()
        {
            Addresses = new List<AddressDto>();
            EmailAddresses = new List<EmailAddressDto>();
            PhoneNumbers = new List<PhoneNumberDto>();
            Notes = new List<NoteDto>();
            Functions = new List<FunctionDto>();
        }

        public ICollection<AddressDto> Addresses { get; private set; }

        public ICollection<EmailAddressDto> EmailAddresses { get; private set; }

        public ICollection<PhoneNumberDto> PhoneNumbers { get; private set; }

        public ICollection<NoteDto> Notes { get; private set; }

        public ICollection<FunctionDto> Functions { get; private set; }

        public CommunicationDto Communication { get; set; }
    }
}
