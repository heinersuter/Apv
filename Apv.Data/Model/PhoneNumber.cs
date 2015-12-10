namespace Apv.Data.Model
{
    public class PhoneNumber : MemberDefaultedItem
    {
        public string Value { get; set; }

        public PhoneNumberType Type { get; set; }
    }
}
