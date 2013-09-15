using System;

namespace FamilyFinance.Models.service
{
    public class DateService
    {
        public static string GetMonthYearDate(int year, int month)
        {
            return new DateTime(year, month, 1).ToLongDateString().Replace("01 ", "");
        }

        public static int PreviousMonth(int year, int month)
        {
            return month == 1 ? 12 : month - 1;
        }

        public static int PreviousYear(int year, int month)
        {
            return month == 1 ? year - 1 : year;
        }

        public static int NextMonth(int year, int month)
        {
            return month == 12 ? 1 : month + 1;
        }

        public static int NextYear(int year, int month)
        {
            return month == 12 ? year + 1 : year;
        }
    }
}