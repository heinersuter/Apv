using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace Apv.GenerateCalendarImage
{
    public class Image
    {
        private const float DayRectSize = 13f;
        private const float OuterBorder = 0f;
        private const float DayRectOffset = DayRectSize / 5f;
        private const float MonthOffset = DayRectSize + DayRectOffset;

        private const float ImageWidth = 7 * DayRectSize + 6 * DayRectOffset + 2 * OuterBorder;

        private static readonly Color BackgroundColor = Color.White;
        private static readonly Brush DefaultDayBrush = Brushes.Bisque;
        private static readonly Brush EventDayBrush = Brushes.SaddleBrown;
        private static readonly Brush HoliDayBrush = new SolidBrush(Color.FromArgb(255, 214, 164));
        private static readonly Brush MonthBrush = new SolidBrush(Color.FromArgb(40, 128, 128, 128));

        private static readonly Font DayFont = new Font("Helvetica", 0.6f * DayRectSize);
        private static readonly Font MonthFont = new Font("Helvetica", 4.5f * DayRectSize, FontStyle.Bold);
        private static readonly StringFormat StringFormat = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };

        private readonly Year _year;

        public Image(Year year)
        {
            _year = year;
        }

        public void Save(string filename)
        {
            var height = _year.Weeks * DayRectSize + (_year.Weeks - 1) * DayRectOffset + 11 * MonthOffset + 2 * OuterBorder;
            using (var bitmap = new Bitmap((int)ImageWidth, (int)height))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(BackgroundColor);
                    foreach (var day in _year.Days)
                    {
                        DrawDay(day, graphics);
                    }
                    for (var i = 1; i <= 12; i++)
                    {
                        DrawMonth(i, graphics);
                    }
                }
                bitmap.Save(filename, ImageFormat.Png);
            }
        }

        private static void DrawDay(Day day, Graphics graphics)
        {
            var dayPosition = GetDayTopLeftPosition(day);

            graphics.FillRectangle(DefaultDayBrush, dayPosition.X, dayPosition.Y, DayRectSize, DayRectSize);

            if (day.Type == DayType.PublicHoliday || day.DayOfWeek == 6 || day.DayOfWeek == 7)
            {
                graphics.FillRectangle(HoliDayBrush, dayPosition.X, dayPosition.Y, DayRectSize, DayRectSize);
            }

            if (day.Type == DayType.Event)
            {
                graphics.FillRectangle(EventDayBrush, dayPosition.X, dayPosition.Y, DayRectSize, DayRectSize);
            }

            graphics.DrawString($"{day.DateTime.Day}", DayFont, Brushes.WhiteSmoke, GetDayCenter(dayPosition), StringFormat);
        }

        private void DrawMonth(int month, Graphics graphics)
        {
            var monthCenter = GetMonthCenter(month);
            var monthLetter = new DateTime(2000, month, 1).ToString("MMMM").First().ToString();
            graphics.DrawString(monthLetter, MonthFont, MonthBrush, monthCenter.X, monthCenter.Y, StringFormat);
        }

        private static PointF GetDayTopLeftPosition(Day day)
        {
            var posX = OuterBorder + (day.DayOfWeek - 1) * (DayRectOffset + DayRectSize);
            var posY = OuterBorder + (day.Week - 1) * (DayRectOffset + DayRectSize) + (day.Month - 1) * MonthOffset;
            return new PointF(posX, posY);
        }

        private static PointF GetDayCenter(PointF dayTopLeft)
        {
            return new PointF(dayTopLeft.X + DayRectSize / 2f, dayTopLeft.Y + DayRectSize / 2f);
        }

        private PointF GetMonthCenter(int month)
        {
            var firstDay = _year.Days.First(day => day.Month == month);
            var lastDay = _year.Days.Last(day => day.Month == month);
            var posY = (GetDayTopLeftPosition(firstDay).Y + GetDayTopLeftPosition(lastDay).Y) / 2f + 0.75f * DayRectSize;
            var monthCenter = new PointF(ImageWidth / 2f, posY);
            return monthCenter;
        }
    }
}
