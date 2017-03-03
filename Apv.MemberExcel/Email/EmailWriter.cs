using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apv.MemberExcel.Services;

namespace Apv.MemberExcel.Email
{
    public class EmailWriter
    {
        public static IEnumerable<EmailDto> CreateEmails(IEnumerable<AddressDto> addressDtos)
        {
            return addressDtos.Select(CreateEmail);
        }

        private static EmailDto CreateEmail(AddressDto addressDto)
        {
            var email = new EmailDto
            {
                To = addressDto.Email1,
                Subject = "APV - Versand 2017"
            };
            email.Attachements.Add(@"Z:\APV\pdfs\GV_Protokoll_2016.pdf");

            email.Body = CreateBody(addressDto);
            return email;
        }

        private static string CreateBody(AddressDto addressDto)
        {
            var body = new StringBuilder();

            body.Append(addressDto.Gender == Gender.Male ? "Lieber " : "Liebe ");
            body.AppendLine(addressDto.Nickname ?? addressDto.Firstname);
            body.AppendLine();

            body.AppendLine("Wir haben wieder ein Jahresprogramm zusammengestellt. Du findes es als PDF im Anhang.");
            body.AppendLine("Falls du mit dem APV lieber etwas anderes machen möchtest, .");

            return body.ToString();
        }
    }
}
