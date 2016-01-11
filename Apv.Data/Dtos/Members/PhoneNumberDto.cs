using Apv.Data.Model.Members;

namespace Apv.Data.Dtos.Members
{
    public class PhoneNumberDto : Dto
    {
        public string Value { get; set; }

        public PhoneNumberType Type { get; set; }

        public bool IsDefault { get; set; }
    }
}
