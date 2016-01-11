using Apv.Data.Model.Members;

namespace Apv.Data.Dtos.Members
{
    public class FunctionDto : Dto
    {
        public string Value { get; set; }

        public FunctionStatus Status { get; set; }
    }
}
