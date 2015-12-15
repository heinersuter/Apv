using System;

namespace Apv.Data.TestConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //using (var context = new ApvDbContext())
            //{
            //    var member = context.Members.Include(m => m.Addresses).First();
            //    Console.WriteLine(member.Nickname);
            //    Console.WriteLine(member.Addresses.Count);
            //    member.Nickname = DateTime.Now.ToShortTimeString();
            //    member.Addresses.Add(new Address());
            //    context.SaveChanges();
            //}

            //using (var context = new ApvDbContext())
            //{
            //    var member = context.Members.Include(m => m.Addresses).First();
            //    Console.WriteLine(member.Nickname);
            //    Console.WriteLine(member.Addresses.Count);
            //}

            //using (var context = new ApvDbContext())
            //{
            //    var member = context.Members.Include(m => m.Addresses).First();
            //    Console.WriteLine(member.Nickname);
            //    Console.WriteLine(member.Addresses.Count);
            //    context.Addresses.Remove(member.Addresses.Last());
            //    context.SaveChanges();
            //}

            //using (var context = new ApvDbContext())
            //{
            //    var member = context.Members.Include(m => m.Addresses).First();
            //    Console.WriteLine(member.Nickname);
            //    Console.WriteLine(member.Addresses.Count);
            //}

            //ReadMembersFromJson();

            //    foreach (var m in context.Members.Include("Addresses"))
            //    {
            //        if (m.Nickname == "Hirsch")
            //        {
            //            m.Firstname = "Heiner";
            //        }
            //    }
            //    context.SaveChanges();
            //}

            //using (var context = new ApvDbContext())
            //{
            //    foreach (var m in context.Members.Include("Addresses"))
            //    {
            //        Console.WriteLine(m.Nickname);
            //        Console.WriteLine(m.Firstname);
            //        if (m.Addresses.Any())
            //        {
            //            Console.WriteLine($"  {m.Addresses.First().Street}");
            //        }
            //    }
            //}

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        //private static void ReadMembersFromJson()
        //{
        //    dynamic oldMembers;
        //    using (var stream = new StreamReader("members.json", Encoding.UTF8))
        //    {
        //        oldMembers = JsonConvert.DeserializeObject(stream.ReadToEnd());
        //    }

        //    using (var context = new ApvDbContext())
        //    {
        //        foreach (var m in oldMembers)
        //        {
        //            var member =
        //                context.Members.Add(new Member { Nickname = m.pfadiname, Firstname = m.vorname, Lastname = m.nachname });

        //            member.PhoneNumbers.Add(new PhoneNumber { Type = PhoneNumberType.Fixnet, Value = m.telefon });
        //            member.PhoneNumbers.Add(new PhoneNumber { Type = PhoneNumberType.Mobile, Value = m.mobile });

        //            if (!string.IsNullOrWhiteSpace((string)m.bemerkung))
        //            {
        //                member.Notes.Add(new Note { Value = m.bemerkung });
        //            }

        //            if (!string.IsNullOrWhiteSpace((string)m.email))
        //            {
        //                member.EmailAddresses.Add(new EmailAddress { Value = m.email, IsDefault = true });
        //            }
        //            if (!string.IsNullOrWhiteSpace((string)m.email2))
        //            {
        //                member.EmailAddresses.Add(new EmailAddress { Value = m.email2 });
        //            }

        //            if (!string.IsNullOrWhiteSpace((string)m.strasse) || !string.IsNullOrWhiteSpace((string)m.plz)
        //                || !string.IsNullOrWhiteSpace((string)m.ort))
        //            {
        //                member.Addresses.Add(new Address { Street = m.strasse, ZipCode = m.plz, City = m.ort });
        //            }

        //            if (!string.IsNullOrWhiteSpace((string)m.funktion))
        //            {
        //                member.Functions.Add(new Function { Value = m.funktion });
        //            }

        //            member.Gender = m.geschlecht == "m" ? MemberDetailsDto.GenderMale : MemberDetailsDto.GenderFemale;

        //            var dateString = (string)m.geburi;
        //            member.Birthdate = dateString != "0000-00-00" && dateString != null
        //                                   ? (DateTime?)DateTime.Parse(dateString, CultureInfo.InvariantCulture)
        //                                   : null;
        //        }
        //        context.SaveChanges();
        //    }
        //}
    }
}
