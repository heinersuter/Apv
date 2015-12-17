using System.Collections.Generic;

using Apv.Data.Dtos;

namespace Apv.Data.WindowsViewer.View.Members
{
    public class AddressesViewModel : MemberItemsViewModel<AddressDto>
    {
        protected override bool CanAdd()
        {
            return base.CanAdd() && Member?.Addresses?.Count <= 100;
        }

        protected override void Add()
        {
            Member.Addresses.Add(new AddressDto());
            base.Add();
        }

        protected override bool CanDelete(AddressDto address)
        {
            return base.CanDelete(address) && Member?.Addresses?.Count > 0;
        }

        protected override void Delete(AddressDto address)
        {
            Member.Addresses.Remove(address);
            base.Delete(address);
        }

        protected override ICollection<AddressDto> GetItems()
        {
            return Member.Addresses;
        }
    }
}
