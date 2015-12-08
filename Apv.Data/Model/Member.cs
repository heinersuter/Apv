using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apv.Data.Model
{
    public class Member
    {
        public Member()
        {
            Addresses = new List<Address>();
        }

        [Key]
        public long Id { get; set; }

        public string Nickname { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? Birthdate { get; set; }

        public MemberStatus Status { get; set; }

        public ICollection<Address> Addresses { get; private set; }
    }
}
