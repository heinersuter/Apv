using System;
using System.Collections.Generic;
using System.Linq;

using Apv.Data.Dtos;
using Apv.Data.Model;

namespace Apv.Data.WindowsViewer.View.Members
{
    public class FunctionsViewModel : MemberItemsViewModel<FunctionDto>
    {
        public static IEnumerable<FunctionStatus> Statuses => Enum.GetValues(typeof(FunctionStatus)).Cast<FunctionStatus>();

        protected override ICollection<FunctionDto> MemberItems => Member.Functions;

        protected override FunctionDto CreateItem()
        {
            return new FunctionDto();
        }
    }
}
