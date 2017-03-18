using System;
using System.Diagnostics;
using System.Linq;

namespace Apv.MemberExcel.Services
{
    [DebuggerDisplay("{Year}-{Month}-{Day}")]
    public struct Date
    {
        public Date(int year)
        {
            Year = year;
            Month = null;
            Day = null;

        }

        public Date(int month, int day)
        {
            Year = null;
            Month = month;
            Day = day;
        }

        public Date(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public Date(DateTime dateTime)
        {
            Year = dateTime.Year;
            Month = dateTime.Month;
            Day = dateTime.Day;
        }

        public int? Year { get; }

        public int? Month { get; }

        public int? Day { get; }

        public static Date Parse(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("The value must not be null", nameof(value));
            }
            if (value.Count(c => c == '.') == 1)
            {
                var numbers = value.Split('.');
                return new Date(int.Parse(numbers[1]), int.Parse(numbers[0]));
            }
            if (value.Length == 4)
            {
                return new Date(int.Parse(value));
            }
            return new Date(DateTime.Parse(value));
        }
    }
}
