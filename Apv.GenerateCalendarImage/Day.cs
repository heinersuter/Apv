using System;
using System.Globalization;

namespace Apv.GenerateCalendarImage
{
    public class Day
    {
        private static readonly CultureInfo _cultureInfo = CultureInfo.CurrentCulture;
        private static readonly Calendar _calendar = _cultureInfo.Calendar;

        public Day(DateTime dateTime)
        {
            DateTime = dateTime;
            DayOfWeek = ((int)DateTime.DayOfWeek + 7 - (int)_cultureInfo.DateTimeFormat.FirstDayOfWeek) % 7 + 1;
            Week = _calendar.GetWeekOfYear(DateTime, _cultureInfo.DateTimeFormat.CalendarWeekRule, _cultureInfo.DateTimeFormat.FirstDayOfWeek);
            Month = DateTime.Month;
        }

        public DateTime DateTime { get; }

        public int DayOfWeek { get; }

        public int Week { get; set; }

        public int Month { get; }

        public DayType? Type { get; set; }
    }
}
