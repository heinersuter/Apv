using System;

namespace Apv.Data.Model.Members
{
    public class Communication
    {
        public Communication()
        {
            LastModified = DateTime.UtcNow;
        }

        public long MemberId { get; set; }

        public Member Member { get; set; }

        public bool RequiresMailing { get; set; }

        public bool RequiresDepositSlip { get; set; }

        public bool WantsWhatsApp { get; set; }

        public long? WhatsAppPhoneNumberId { get; set; }

        public DateTime LastModified { get; set; }
    }
}
