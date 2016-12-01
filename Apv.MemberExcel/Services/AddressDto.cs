﻿namespace Apv.MemberExcel.Services
{
    public class AddressDto
    {
        public Status Status { get; set; }

        public string Nickname { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string Comment { get; set; }

        public string Email1 { get; set; }

        public string Email2 { get; set; }

        public string AddressLine1 { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string Functions { get; set; }

        public string FunctionsScouts { get; set; }

        public Gender Gender { get; set; }

        public Date? Birthdate { get; set; }

        public PaymentType? Payment { get; set; }

        public Date? ResignDate { get; set; }

        public int? FamilyId { get; set; }
    }
}
