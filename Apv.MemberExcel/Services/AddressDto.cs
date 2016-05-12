namespace Apv.MemberExcel.Services
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

        public Gender Gender { get; set; }

        public Date? Birthdate { get; set; }

        public bool? RequiresDepositSlip { get; set; }

        public Date? ResignDate { get; set; }

        public int? FamilyId { get; set; }

        public void SetValue(int columnIndex, string value)
        {
            switch (columnIndex)
            {
                case 1:
                    Status = value == "Aktiv" ? Status.Active : Status.Inactive;
                    break;
                case 2:
                    Nickname = value;
                    break;
                case 3:
                    Lastname = value;
                    break;
                case 4:
                    Firstname = value;
                    break;
                case 5:
                    Phone = value;
                    break;
                case 6:
                    Mobile = value;
                    break;
                case 7:
                    Comment = value;
                    break;
                case 8:
                    Email1 = value;
                    break;
                case 9:
                    Email2 = value;
                    break;
                case 10:
                    AddressLine1 = value;
                    break;
                case 11:
                    ZipCode = value;
                    break;
                case 12:
                    City = value;
                    break;
                case 13:
                    Functions = value;
                    break;
                case 14:
                    Gender = value == "m" ? Gender.Male : value == "f" ? Gender.Female : Gender.Family;
                    break;
                case 15:
                    Birthdate = Date.Parse(value);
                    break;
                case 16:
                    RequiresDepositSlip = value == "1";
                    break;
                case 17:
                    ResignDate = Date.Parse(value);
                    break;
                case 18:
                    FamilyId = int.Parse(value);
                    break;
            }
        }
    }
}
