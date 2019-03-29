using System.Text.RegularExpressions;

namespace Apv.MemberExcel.Services
{
    public class Name
    {
        public string Nickname { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public static Name FromProfilePhotoFile(string filename)
        {
            var regex = new Regex(@"\\([\w-]+)_([\w-]+)_([\w-]+)\.?\w*");
            var match = regex.Match(filename);
            if (match.Success)
            {
                return new Name
                {
                    Nickname = match.Groups[1].Value,
                    Firstname = match.Groups[2].Value,
                    Lastname = match.Groups[3].Value
                };
            }

            return null;
        }

        public static Name FromVCardFn(string fn)
        {
            var regex = new Regex(@"([\w-]+) ([\w-]+) \(([\w-]+)\)");
            var match = regex.Match(fn);
            if (match.Success)
            {
                return new Name
                {
                    Nickname = match.Groups[3].Value,
                    Firstname = match.Groups[1].Value,
                    Lastname = match.Groups[2].Value
                };
            }

            return null;
        }

        public static Name FromDto(AddressDto dto)
        {
            return new Name
            {
                Nickname = dto.Nickname,
                Firstname = dto.Firstname,
                Lastname = dto.Lastname
            };
        }

        public bool IsMatch(AddressDto dto)
        {
            return dto.Nickname == Nickname && dto.Firstname == Firstname && dto.Lastname == Lastname;
        }

        public string ToFilename()
        {
            return $"{Nickname}_{Firstname}_{Lastname}";
        }
    }
}
