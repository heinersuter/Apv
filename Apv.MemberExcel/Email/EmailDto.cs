using System.Collections.Generic;

namespace Apv.MemberExcel.Email
{
    public class EmailDto
    {
        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public IList<string> Attachements { get; } = new List<string>();
    }
}
