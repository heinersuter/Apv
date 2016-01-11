using System.Collections.Generic;
using System.Linq;

using Apv.Data.Dtos;
using Apv.Data.Dtos.Members;

namespace Apv.Data.WindowsViewer.View.Members
{
    public class EmailAddressesViewModel : MemberItemsViewModel<EmailAddressDto>
    {
        protected override ICollection<EmailAddressDto> MemberItems => Member.EmailAddresses;

        protected override EmailAddressDto CreateItem()
        {
            return new EmailAddressDto { IsDefault = MemberItems.Count == 0 };
        }

        protected override void Delete(EmailAddressDto item)
        {
            var wasDefault = item.IsDefault;
            base.Delete(item);
            if (wasDefault && MemberItems.Count > 0)
            {
                MemberItems.First().IsDefault = true;
            }
        }
    }
}
