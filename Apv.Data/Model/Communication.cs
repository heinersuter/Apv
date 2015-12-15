using System;

namespace Apv.Data.Model
{
    internal class Communication
    {
        public Communication()
        {
            LastModified = DateTime.UtcNow;
        }

        public long MemberId { get; set; }

        public Member Member { get; set; }

        public bool RequiresMailing { get; set; }

        public bool RequiresDepositSlip { get; set; }

        public DateTime LastModified { get; set; }
    }
}
