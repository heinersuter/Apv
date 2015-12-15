using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apv.Data.Model
{
    internal class Member : Item
    {
        public Member()
        {
            Addresses = new List<Address>();
            EmailAddresses = new List<EmailAddress>();
            PhoneNumbers = new List<PhoneNumber>();
            Notes = new List<Note>();
            Functions = new List<Function>();
        }

        public string Nickname { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? Birthdate { get; set; }

        public string Gender { get; set; }

        public MemberStatus Status { get; set; }

        public ICollection<Address> Addresses { get; private set; }

        public ICollection<EmailAddress> EmailAddresses { get; private set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; private set; }

        public ICollection<Note> Notes { get; private set; }

        public ICollection<Function> Functions { get; private set; }

        public Communication Communication { get; set; }
    }
}
