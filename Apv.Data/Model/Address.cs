namespace Apv.Data.Model
{
    public class Address : MemberDefaultedItem
    {
        public string Street { get; set; }

        public string StreetLine2 { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string CountryCode { get; set; }
    }
}
