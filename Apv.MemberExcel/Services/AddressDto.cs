namespace Apv.MemberExcel.Services
{
    public class AddressDto
    {
        public string Status { get; set; }

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

        public bool? RequiresMailing { get; set; }

        public Date? ResignDate { get; set; }

        public void SetValue(int columnIndex, string value)
        {
            switch (columnIndex)
            {
                case 1:
                    Status = value;
                    break;
                case 2:
                    Nickname = value;
                    break;
                case 3:
                    Firstname = value;
                    break;
                case 4:
                    Lastname = value;
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
                    RequiresMailing = value == "1";
                    break;
                case 18:
                    ResignDate = Date.Parse(value);
                    break;
            }
        }

        //Status
        //Pfadiname   
        //Nachname 
        //Vorname 
        //Telefon 
        //Mobile  
        //Bemerkung 
        //E-Mail 1	
        //E-Mail 2	
        //Strasse 
        //PLZ 
        //Ort 
        //Funktionen  
        //Geschlecht 
        //Geburtsdatum    
        //EZ 
        //Post    
        //Austritt 
        //Adresse bekannt 
        //E-Mail bekannt
    }
}
