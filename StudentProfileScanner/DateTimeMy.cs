using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentProfileScanner
{
    class DateTimeMy
    {
        public int seconds;
        public int minutes;
        public int hours;
        public int days;
        public int months;
        public int years;
        public PMorAM postORanteMerediem;

        public enum PMorAM { AM, PM, NULL }

        public DateTimeMy(int _seconds, int _minutes, int _hours, int _days, int _months, int _years, PMorAM _postORanteMerediem)
        {
            seconds = _seconds;
            minutes = _minutes;
            hours = _hours;
            days = _days;
            months = _months;
            years = _years;
            postORanteMerediem = _postORanteMerediem;
        }
        public static DateTimeMy GetDateTimeFromString(string stringToParse)
        {
            int _seconds = 0;
            int _minutes = 0;
            int _hours = 0;
            int _months = 0;
            int _days = 0;
            int _years = 0;
            PMorAM _postORanteMerediem = PMorAM.NULL;


            if (stringToParse.Split('.').Length == 0)
            {
                string date = stringToParse.Split(' ')[0];
                string time = stringToParse.Split(' ')[1];

                _seconds = Convert.ToInt32(time.Split(':')[2]);
                _minutes = Convert.ToInt32(time.Split(':')[1]);
                _hours = Convert.ToInt32(time.Split(':')[0]);

                _months = Convert.ToInt32(date.Split('/')[0]);
                _days = Convert.ToInt32(date.Split('/')[1]);
                _years = Convert.ToInt32(date.Split('/')[2]);

                if (stringToParse.Split(' ')[2] == "PM")
                    _postORanteMerediem = PMorAM.PM;
                else
                    _postORanteMerediem = PMorAM.AM;
            }
            else
            {
                _seconds = Convert.ToInt32(stringToParse.Split('.')[0].Split(':')[2]);
                _minutes = Convert.ToInt32(stringToParse.Split('.')[0].Split(':')[1]);
                _hours = Convert.ToInt32(stringToParse.Split('.')[0].Split(':')[0]);
            }

            return new DateTimeMy(_seconds, _minutes, _hours, _days, _months, _years, _postORanteMerediem);
        }
        public static DateTimeMy Add2DateTimes(DateTimeMy dateTime1, DateTimeMy dateTime2)
        {
            int finalSeconds = 0;
            int finalMinutes = 0;
            int finalHours = 0;
            int finalDays = 0;
            int finalMonths = 0;
            int finalYears = 0;

            finalSeconds = dateTime1.seconds + dateTime2.seconds;
            if (finalSeconds >= 60)
            {
                finalSeconds -= 60;
                finalMinutes++;
            }

            finalMinutes += dateTime1.minutes + dateTime2.minutes;
            if (finalMinutes >= 60)
            {
                finalMinutes -= 60;
                finalHours++;
            }

            finalHours += dateTime1.hours + dateTime2.hours;
            if (finalHours >= 24)
            {
                finalHours -= 24;
                finalDays++;
            }

            finalDays += dateTime1.days + dateTime2.days;
            if (finalDays >= 30)
            {
                finalDays -= 30;
                finalMonths++;
            }

            finalMonths += dateTime1.months + dateTime2.months;
            if (finalMonths >= 12)
            {
                finalMonths -= 12;
                finalYears++;
            }

            finalYears += dateTime1.years + dateTime2.years;

            return new DateTimeMy(finalSeconds, finalMinutes, finalHours, finalDays, finalMonths, finalYears, PMorAM.NULL);
        }
        public static int ConvertToSeconds(DateTimeMy dateTime)
        {
            int seconds = 0;

            seconds += dateTime.seconds + dateTime.minutes * 60 + dateTime.hours * 3600 + dateTime.days * 86400 + dateTime.months * 2592000 + dateTime.years * 31104000;

            return seconds;
        }
        public static DateTimeMy ConvertSeconds2DateTimeMy(int finalSeconds)
        {
            int finalMinutes = 0;
            int finalHours = 0;
            int finalDays = 0;
            int finalMonths = 0;
            int finalYears = 0;

            while (finalSeconds >= 60)
            {
                finalSeconds -= 60;
                finalMinutes++;
            }
            while (finalMinutes >= 60)
            {
                finalMinutes -= 60;
                finalHours++;
            }
            while (finalHours >= 24)
            {
                finalHours -= 24;
                finalDays++;
            }
            while (finalDays >= 30)
            {
                finalDays -= 30;
                finalMonths++;
            }
            while (finalMonths >= 12)
            {
                finalMonths -= 12;
                finalYears++;
            }

            return new DateTimeMy(finalSeconds, finalMinutes, finalHours, finalDays, finalMonths, finalYears, PMorAM.NULL);
        }
    }
}
