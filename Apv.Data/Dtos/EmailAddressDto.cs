namespace Apv.Data.Dtos
{
    public class EmailAddressDto : Dto
    {
        public string Value { get; set; }

        public string Description { get; set; }

        public bool IsDefault { get; set; }
    }
}
