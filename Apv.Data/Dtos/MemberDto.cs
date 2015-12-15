using System;

using Apv.Data.Model;

namespace Apv.Data.Dtos
{
    public class MemberDto : Dto
    {
        public const string GenderMale = "Male";

        public const string GenderFemale = "Female";

        public string Nickname { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public DateTime? Birthdate { get; set; }

        public string Gender { get; set; }

        public MemberStatus Status { get; set; }
    }
}
