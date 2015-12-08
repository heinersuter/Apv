using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apv.Data.Model
{
    public class Address
    {
        [Key]
        public long Id { get; set; }

        public long MemberId { get; set; }

        [ForeignKey("MemberId")]
        public Member Member { get; set; }

        public string Street { get; set; }

        public string StreetLine2 { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string CountryCode { get; set; }
    }
}
