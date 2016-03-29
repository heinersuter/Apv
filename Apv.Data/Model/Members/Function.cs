using System;

namespace Apv.Data.Model.Members
{
    public class Function : MemberItem
    {
        public string Value { get; set; }

        public FunctionStatus Status { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}
