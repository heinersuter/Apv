using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Apv.Data.Dtos;
using Apv.Data.Dtos.Members;
using Apv.Data.Model;
using Apv.Data.Model.Members;

namespace Apv.Data.WindowsViewer.View.Members
{
    public class PhoneNumbersViewModel : MemberItemsViewModel<PhoneNumberDto>
    {
        public static IEnumerable<PhoneNumberType> Types => Enum.GetValues(typeof(PhoneNumberType)).Cast<PhoneNumberType>();

        protected override ICollection<PhoneNumberDto> MemberItems => Member.PhoneNumbers;

        protected override PhoneNumberDto CreateItem()
        {
            return new PhoneNumberDto { IsDefault = MemberItems.Count == 0 };
        }

        protected override void Delete(PhoneNumberDto item)
        {
            var wasDefault = item.IsDefault;
            base.Delete(item);
            if (wasDefault && MemberItems.Count > 0)
            {
                MemberItems.First().IsDefault = true;
            }
        }

        protected override void OnMemberChanged(MemberDetailsDto member)
        {
            if (member != null)
            {
                foreach (var phoneNumber in member.PhoneNumbers)
                {
                    phoneNumber.Value = FormatPhoneNumber(phoneNumber.Value);
                }
            }
            base.OnMemberChanged(member);
        }

        private static string FormatPhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            var keys = value?.Replace(" ", "");
            if (Regex.IsMatch(keys, @"\+\d{11}"))
            {
                return $"{keys[0]}{keys[1]}{keys[2]} {keys[3]}{keys[4]} {keys[5]}{keys[6]}{keys[7]} {keys[8]}{keys[9]} {keys[10]}{keys[11]}";
            }
            if (Regex.IsMatch(keys, @"0\d{8}"))
            {
                return $"+41 {keys[1]}{keys[2]} {keys[3]}{keys[4]}{keys[5]} {keys[6]}{keys[7]} {keys[8]}{keys[9]}";
            }
            return value;
        }
    }
}
