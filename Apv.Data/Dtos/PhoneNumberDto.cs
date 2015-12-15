using Apv.Data.Model;

namespace Apv.Data.Dtos
{
    public class PhoneNumberDto : Dto
    {
        public string Value { get; set; }

        public PhoneNumberType Type { get; set; }

        public bool IsDefault { get; set; }
    }
}
