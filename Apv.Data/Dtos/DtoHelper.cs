﻿using Apv.Data.Model;

namespace Apv.Data.Dtos
{
    internal static class DtoHelper
    {
        public static Member FromDto(MemberDetailsDto memberDetailsDto)
        {
            var member = new Member
            {
                Id = memberDetailsDto.Id,
                Nickname = memberDetailsDto.Nickname,
                Firstname = memberDetailsDto.Firstname,
                Lastname = memberDetailsDto.Lastname,
                Birthdate = memberDetailsDto.Birthdate,
                Gender = memberDetailsDto.Gender,
                Status = memberDetailsDto.Status
            };

            foreach (var dto in memberDetailsDto.Addresses)
            {
                member.Addresses.Add(FromDto(dto));
            }

            foreach (var dto in memberDetailsDto.EmailAddresses)
            {
                member.EmailAddresses.Add(FromDto(dto));
            }

            foreach (var dto in memberDetailsDto.PhoneNumbers)
            {
                member.PhoneNumbers.Add(FromDto(dto));
            }

            foreach (var dto in memberDetailsDto.Notes)
            {
                member.Notes.Add(FromDto(dto));
            }

            foreach (var dto in memberDetailsDto.Functions)
            {
                member.Functions.Add(FromDto(dto));
            }

            return member;
        }

        private static Address FromDto(AddressDto dto)
        {
            return new Address
            {
                Id = dto.Id,
                Street = dto.Street,
                StreetLine2 = dto.StreetLine2,
                CountryCode = dto.CountryCode,
                ZipCode = dto.ZipCode,
                City = dto.City,
                IsDefault = dto.IsDefault
            };
        }

        private static EmailAddress FromDto(EmailAddressDto dto)
        {
            return new EmailAddress
            {
                Id = dto.Id,
                Value = dto.Value,
                Description = dto.Description,
                IsDefault = dto.IsDefault
            };
        }

        private static PhoneNumber FromDto(PhoneNumberDto dto)
        {
            return new PhoneNumber
            {
                Id = dto.Id,
                Value = dto.Value,
                Type = dto.Type,
                IsDefault = dto.IsDefault
            };
        }

        private static Note FromDto(NoteDto dto)
        {
            return new Note
            {
                Id = dto.Id,
                Value = dto.Value
            };
        }

        private static Function FromDto(FunctionDto dto)
        {
            return new Function
            {
                Id = dto.Id,
                Value = dto.Value
            };
        }

        public static MemberDto ToDto(Member member)
        {
            return new MemberDto
            {
                Id = member.Id,
                Nickname = member.Nickname,
                Firstname = member.Firstname,
                Lastname = member.Lastname,
                Birthdate = member.Birthdate,
                Gender = member.Gender,
                Status = member.Status
            };
        }

        public static MemberDetailsDto ToDetailsDto(Member member)
        {
            var memberDetailsDto = new MemberDetailsDto
            {
                Id = member.Id,
                Nickname = member.Nickname,
                Firstname = member.Firstname,
                Lastname = member.Lastname,
                Birthdate = member.Birthdate,
                Gender = member.Gender,
                Status = member.Status
            };

            foreach (var dto in member.Addresses)
            {
                memberDetailsDto.Addresses.Add(ToDto(dto));
            }

            foreach (var dto in member.EmailAddresses)
            {
                memberDetailsDto.EmailAddresses.Add(ToDto(dto));
            }

            foreach (var dto in member.PhoneNumbers)
            {
                memberDetailsDto.PhoneNumbers.Add(ToDto(dto));
            }

            foreach (var dto in member.Notes)
            {
                memberDetailsDto.Notes.Add(ToDto(dto));
            }

            foreach (var dto in member.Functions)
            {
                memberDetailsDto.Functions.Add(ToDto(dto));
            }

            return memberDetailsDto;
        }

        private static AddressDto ToDto(Address address)
        {
            return new AddressDto
            {
                Id = address.Id,
                Street = address.Street,
                StreetLine2 = address.StreetLine2,
                CountryCode = address.CountryCode,
                ZipCode = address.ZipCode,
                City = address.City,
                IsDefault = address.IsDefault
            };
        }

        private static EmailAddressDto ToDto(EmailAddress emailAddress)
        {
            return new EmailAddressDto
            {
                Id = emailAddress.Id,
                Value = emailAddress.Value,
                Description = emailAddress.Description,
                IsDefault = emailAddress.IsDefault
            };
        }

        private static PhoneNumberDto ToDto(PhoneNumber phoneNumber)
        {
            return new PhoneNumberDto
            {
                Id = phoneNumber.Id,
                Value = phoneNumber.Value,
                Type = phoneNumber.Type,
                IsDefault = phoneNumber.IsDefault
            };
        }

        private static NoteDto ToDto(Note note)
        {
            return new NoteDto
            {
                Id = note.Id,
                Value = note.Value
            };
        }

        private static FunctionDto ToDto(Function function)
        {
            return new FunctionDto
            {
                Id = function.Id,
                Value = function.Value
            };
        }

    }
}