using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Apv.ArchiveApi.Api
{
    public class Date
    {
        public string Value { get; set; }

        public IEnumerable<int> Years { get; set; } = new List<int>();

        public static Date Parse(string value)
        {
            if (Regex.IsMatch(value, @"^\d{1,4}$"))
            {
                return new Date { Value = value, Years = new[] { int.Parse(value) } };

            }
            return new Date { Value = value };
        }
    }
}
