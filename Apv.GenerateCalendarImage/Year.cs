using System;
using System.Collections.Generic;
using System.Linq;

namespace Apv.GenerateCalendarImage
{
    public class Year
    {
        public Year(int year)
        {
            AddDays(year);
            UpdateWeekToStartWithOne();
        }

        public IEnumerable<Day> Days { get; private set; }

        public int Weeks { get; private set; }

        public Day Find(int month, int day)
        {
            return Days.Single(inner => inner.DateTime.Month == month && inner.DateTime.Day == day);
        }

        private void AddDays(int year)
        {
            var days = new List<Day>();

            for (var month = 1; month <= 12; month++)
            {
                for (var day = 1; day <= 31; day++)
                {
                    if (day <= DateTime.DaysInMonth(year, month))
                    {
                        days.Add(new Day(new DateTime(year, month, day)));
                    }
                }
            }

            Days = days;
        }

        private void UpdateWeekToStartWithOne()
        {
            var newWeek = 0;
            var lastWeek = 0;
            foreach (var day in Days)
            {
                if (lastWeek != day.Week)
                {
                    lastWeek = day.Week;
                    newWeek++;
                }
                day.Week = newWeek;
            }
            Weeks = newWeek;
        }
    }
}
