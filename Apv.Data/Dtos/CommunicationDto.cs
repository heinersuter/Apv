namespace Apv.Data.Dtos
{
    public class CommunicationDto : Dto
    {
        public bool RequiresMailing { get; set; }

        public bool RequiresDepositSlip { get; set; }
    }
}
