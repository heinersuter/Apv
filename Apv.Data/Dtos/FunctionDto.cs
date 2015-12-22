using Apv.Data.Model;

namespace Apv.Data.Dtos
{
    public class FunctionDto : Dto
    {
        public string Value { get; set; }

        public FunctionStatus Status { get; set; }
    }
}
