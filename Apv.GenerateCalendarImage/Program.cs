using System.Diagnostics;

namespace Apv.GenerateCalendarImage
{
    public class Program
    {
        private const string CalendarImageFilename = @"calendarImage.png";

        public static void Main()
        {
            var year = new Year(2016);

            year.Find(5, 2).Type = DayType.Event;
            year.Find(5, 20).Type = DayType.Event;
            year.Find(6, 25).Type = DayType.Event;
            year.Find(9, 11).Type = DayType.Event;
            year.Find(10, 29).Type = DayType.Event;
            year.Find(11, 25).Type = DayType.Event;
            year.Find(12, 17).Type = DayType.Event;

            year.Find(1, 1).Type = DayType.PublicHoliday;
            year.Find(1, 2).Type = DayType.PublicHoliday;
            year.Find(3, 25).Type = DayType.PublicHoliday;
            year.Find(3, 27).Type = DayType.PublicHoliday;
            year.Find(3, 28).Type = DayType.PublicHoliday;
            year.Find(5, 1).Type = DayType.PublicHoliday;
            year.Find(5, 5).Type = DayType.PublicHoliday;
            year.Find(5, 16).Type = DayType.PublicHoliday;
            year.Find(8, 1).Type = DayType.PublicHoliday;
            year.Find(12, 25).Type = DayType.PublicHoliday;
            year.Find(12, 26).Type = DayType.PublicHoliday;

            var image = new Image(year);
            image.Save(CalendarImageFilename);

            Process.Start(CalendarImageFilename);
        }

    }
}
