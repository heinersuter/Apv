using System.Collections.Generic;

using Apv.Data.Dtos;

namespace Apv.Data.WindowsViewer.View.Members
{
    public class AddressesViewModel : MemberItemsViewModel<AddressDto>
    {
        protected override bool CanAdd()
        {
            return base.CanAdd() && Member?.Addresses?.Count < 1;
        }

        protected override ICollection<AddressDto> MemberItems => Member.Addresses;

        protected override AddressDto CreateItem()
        {
            return new AddressDto();
        }
    }
}
