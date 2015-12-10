using System;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Text;

using Apv.Data.Model;

using Newtonsoft.Json;

namespace Apv.Data
{
    public class TestDataInitializer : DropCreateDatabaseAlways<ApvDbContext>
    {
        protected override void Seed(ApvDbContext context)
        {
            ReadMembersFromJson(context);

            base.Seed(context);
        }

        private static void ReadMembersFromJson(ApvDbContext context)
        {
            dynamic oldMembers;
            using (var stream = new StreamReader("members.json", Encoding.UTF8))
            {
                oldMembers = JsonConvert.DeserializeObject(stream.ReadToEnd());
            }

            foreach (var m in oldMembers)
            {
                var member =
                    context.Members.Add(new Member { Nickname = m.pfadiname, Firstname = m.vorname, Lastname = m.nachname });

                member.PhoneNumbers.Add(new PhoneNumber { Type = PhoneNumberType.Fixnet, Value = m.telefon });
                member.PhoneNumbers.Add(new PhoneNumber { Type = PhoneNumberType.Mobile, Value = m.mobile });

                if (!string.IsNullOrWhiteSpace((string)m.bemerkung))
                {
                    member.Notes.Add(new Note { Value = m.bemerkung });
                }

                if (!string.IsNullOrWhiteSpace((string)m.email))
                {
                    member.EmailAddresses.Add(new EmailAddress { Value = m.email, IsDefault = true });
                }
                if (!string.IsNullOrWhiteSpace((string)m.email2))
                {
                    member.EmailAddresses.Add(new EmailAddress { Value = m.email2 });
                }

                if (!string.IsNullOrWhiteSpace((string)m.strasse) || !string.IsNullOrWhiteSpace((string)m.plz)
                    || !string.IsNullOrWhiteSpace((string)m.ort))
                {
                    member.Addresses.Add(new Address { Street = m.strasse, ZipCode = m.plz, City = m.ort });
                }

                if (!string.IsNullOrWhiteSpace((string)m.funktion))
                {
                    member.Functions.Add(new Function { Value = m.funktion });
                }

                member.Gender = m.geschlecht == "m" ? Member.GenderMale : Member.GenderFemale;

                var dateString = (string)m.geburi;
                member.Birthdate = dateString != "0000-00-00" && dateString != null
                                       ? (DateTime?)DateTime.Parse(dateString, CultureInfo.InvariantCulture)
                                       : null;

                member.Communication = new Communication();
            }
            context.SaveChanges();
        }
    }
}
