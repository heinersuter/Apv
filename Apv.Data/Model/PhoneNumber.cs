namespace Apv.Data.Model
{
    internal class PhoneNumber : MemberDefaultedItem
    {
        public string Value { get; set; }

        public PhoneNumberType Type { get; set; }
    }
}
