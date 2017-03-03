using System.Diagnostics;

namespace Apv.GenerateCalendarImage
{
    public class Program
    {
        private const string CalendarImageFilename = @"calendarImage.png";

        public static void Main()
        {
            var year = new Year(2017);

            year.Find(5, 12).Type = DayType.Event;
            year.Find(9, 24).Type = DayType.Event;
            year.Find(11, 17).Type = DayType.Event;
            year.Find(12, 16).Type = DayType.Event;

            year.Find(04, 29).Type = DayType.Event;
            year.Find(04, 30).Type = DayType.Event;
            year.Find(6, 25).Type = DayType.Event;
            year.Find(6, 30).Type = DayType.Event;
            year.Find(7, 1).Type = DayType.Event;

            year.Find(1, 1).Type = DayType.PublicHoliday;
            year.Find(1, 2).Type = DayType.PublicHoliday;
            year.Find(4, 14).Type = DayType.PublicHoliday;
            year.Find(4, 16).Type = DayType.PublicHoliday;
            year.Find(4, 17).Type = DayType.PublicHoliday;
            year.Find(5, 25).Type = DayType.PublicHoliday;
            year.Find(6, 5).Type = DayType.PublicHoliday;
            year.Find(8, 1).Type = DayType.PublicHoliday;
            year.Find(12, 25).Type = DayType.PublicHoliday;
            year.Find(12, 26).Type = DayType.PublicHoliday;

            var image = new Image(year);
            image.Save(CalendarImageFilename);

            Process.Start(CalendarImageFilename);
        }

    }
}
