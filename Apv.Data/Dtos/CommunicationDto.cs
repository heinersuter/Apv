namespace Apv.Data.Dtos
{
    public class CommunicationDto : Dto
    {
        public bool RequiresMailing { get; set; }

        public bool RequiresDepositSlip { get; set; }

        public bool WantsWhatsApp { get; set; }

        public long? WhatsAppPhoneNumberId { get; set; }
    }
}
