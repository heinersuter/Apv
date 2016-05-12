using System;
using System.Linq;
using Apv.MemberExcel.Services;

namespace Apv.MemberExcel.Pdfs
{
    public class LetterAddress
    {
        public string CallingName { get; set; }

        public string AddressName { get; set; }

        public string AddressLine1 { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public Gender Gender { get; set; }

        public bool? RequiresDepositSlip { get; set; }

        public bool HasEmail { get; set; }

        public static LetterAddress Combine(IGrouping<int?, AddressDto> grouping)
        {
            AddressDto first;
            AddressDto second;
            if (grouping.First().Gender != grouping.Last().Gender)
            {
                first = grouping.Single(dto => dto.Gender == Gender.Female);
                second = grouping.Single(dto => dto.Gender == Gender.Male);
            }
            else
            {
                first = grouping.First();
                second = grouping.Last();
            }

            return Create(first, second);
        }

        public static LetterAddress Create(AddressDto dto)
        {
            return new LetterAddress
            {
                CallingName = dto.Nickname ?? dto.Firstname,
                AddressName = $"{dto.Firstname} {dto.Lastname}{(dto.Nickname != null ? " / " + dto.Nickname : string.Empty)}",
                AddressLine1 = dto.AddressLine1,
                ZipCode = dto.ZipCode,
                City = dto.City,
                Gender = dto.Gender,
                RequiresDepositSlip = dto.RequiresDepositSlip,
                HasEmail = dto.Email1 != null
            };
        }

        private static LetterAddress Create(AddressDto first, AddressDto second)
        {
            var letterAddress = new LetterAddress
            {
                CallingName = (first.Nickname ?? first.Firstname) + " & " + (second.Nickname ?? second.Firstname),
                AddressName = first.Lastname == second.Lastname
                    ? $"{first.Firstname} & {second.Firstname} {first.Lastname}"
                    : $"{first.Firstname} {first.Lastname} & {second.Firstname} {second.Lastname}",
                AddressLine1 = first.AddressLine1,
                ZipCode = first.ZipCode,
                City = first.City,
                Gender = Gender.Family,
                RequiresDepositSlip = first.RequiresDepositSlip == null && second.RequiresDepositSlip == null
                    ? (bool?)null
                    : first.RequiresDepositSlip == true || second.RequiresDepositSlip == true,
                HasEmail = first.Email1 != null || second.Email1 != null
            };
            if (first.Nickname != null && second.Nickname != null)
            {
                letterAddress.AddressName += $"{Environment.NewLine}{first.Nickname} & {second.Nickname}";
            }
            else if (first.Nickname != null)
            {
                letterAddress.AddressName += $" / {first.Nickname}";
            }
            else if (second.Nickname != null)
            {
                letterAddress.AddressName += $" / {second.Nickname}";
            }
            return letterAddress;
        }
    }
}
