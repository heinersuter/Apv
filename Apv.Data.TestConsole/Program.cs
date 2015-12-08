using System;
using System.Linq;

namespace Apv.Data.TestConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new ApvDbContext())
            {
                foreach (var m in context.Members.Include("Addresses"))
                {
                    if (m.Nickname == "Hirsch")
                    {
                        m.Firstname = "Heiner";
                    }
                }
                context.SaveChanges();
            }

            using (var context = new ApvDbContext())
            {
                foreach (var m in context.Members.Include("Addresses"))
                {
                    Console.WriteLine(m.Nickname);
                    Console.WriteLine(m.Firstname);
                    if (m.Addresses.Any())
                    {
                        Console.WriteLine($"  {m.Addresses.First().Street}");
                    }
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
