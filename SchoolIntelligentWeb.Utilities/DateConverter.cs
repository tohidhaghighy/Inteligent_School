using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolIntelligentWeb.Utilities
{
    public static class DateConverter
    {
       
        public static string Monthpersion(this int month)
        {
            string Persionmonth = "فروردین";
            if (month == 2)
            {
                Persionmonth = "اردیبهشت";
            }
            else if (month == 3)
            {
                Persionmonth = "خرداد";
            }
            else if (month == 4)
            {
                Persionmonth = "تیر";
            }
            else if (month == 5)
            {
                Persionmonth = "مرداد";
            }
            else if (month == 6)
            {
                Persionmonth = "شهریور";
            }
            else if (month == 7)
            {
                Persionmonth = "مهر";
            }
            else if (month == 8)
            {
                Persionmonth = "آبان";
            }
            else if (month == 9)
            {
                Persionmonth = "آذر";
            }
            else if (month == 10)
            {
                Persionmonth = "دی";
            }
            else if (month == 11)
            {
                Persionmonth = "بهمن";
            }
            else if (month == 12)
            {
                Persionmonth = "اسفند";
            }
            return Persionmonth;
        }

        public static string Monthpersion(this string day)
        {
            string dayofweek = "شنبه";
            if (day == "Monday")
            {
                dayofweek = "دوشنبه";
            }
            else if (day == "Tuesday")
            {
                dayofweek = "سه شنبه";
            }
            else if (day == "Wednesday")
            {
                dayofweek = "چهارشنبه";
            }
            else if (day == "Thursday")
            {
                dayofweek = "پنج شنبه";
            }
            else if (day == "Friday")
            {
                dayofweek = "جمعه";
            }
            else if (day == "Saturday")
            {
                dayofweek = "شنبه";
            }
            else if (day == "Sunday")
            {
                dayofweek = "یکشنبه";
            }

            return dayofweek;
        }
        public static string ToPersian(this DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            int year = pc.GetYear(dt);
            int month = pc.GetMonth(dt);
            int day = pc.GetDayOfMonth(dt);
            string dayweek = pc.GetDayOfWeek(dt).ToString();
            int hour = pc.GetHour(dt);
            int min = pc.GetMinute(dt);
            string monthpersion = month.Monthpersion();
            string dayofweek = dayweek.Monthpersion();
            string FinalFormat = dayofweek +" "+ day.ToString() +" "+ monthpersion + " ساعت " + min.ToString() + " : " + hour.ToString();
            return FinalFormat;
        }
        public static DateTime ToPersianDate(this DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            int year = pc.GetYear(dt);
            int month = pc.GetMonth(dt);
            int day = pc.GetDayOfMonth(dt);
            int hour = pc.GetHour(dt);
            int min = pc.GetMinute(dt);

            return new DateTime(year, month, day, hour, min, 0);
        }

        public static string ToPersianDateAndroid(this DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            string year = pc.GetYear(dt).ToString()[2]+""+pc.GetYear(dt).ToString()[3];
            int month = pc.GetMonth(dt);
            int day = pc.GetDayOfMonth(dt);
            int hour = pc.GetHour(dt);
            int min = pc.GetMinute(dt);

            return year+"/"+month+"/"+day;
        }
        private static readonly string[] pn = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" ,"",":"};
        private static readonly string[] en = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" ,"",":"};
        public static string toEnglishNumber(this string strNum)
        {
            string chash = strNum;
            for (int i = 0; i < 10; i++)
                chash = chash.Replace(pn[i], en[i]);
            return chash;
        }
        public static string toPersianNumber(this string strNum)
        {
            string chash = strNum;
            for (int i = 0; i < 10; i++)
                chash = chash.Replace(en[i], pn[i]);
            return chash;
        }

        public static DateTime tomiladi(this string strnum)
        {
            PersianCalendar pc = new PersianCalendar();

            return pc.ToDateTime(int.Parse(strnum.Substring(0, 4)), int.Parse(strnum.Substring(5, 2)), int.Parse(strnum.Substring(8, 2)), int.Parse(strnum.Substring(11, 2)), int.Parse(strnum.Substring(14, 2)), int.Parse(strnum.Substring(17, 2)), 0, 0);
        }

        public static DateTime tomiladilimited(this string strnum)
        {
            PersianCalendar pc = new PersianCalendar();
            strnum = strnum.toEnglishNumber();
            string s1 = strnum.Substring(0, 4);
            string s2 = strnum.Substring(5, 2);
            if (s2[0].ToString()=="0")
            {
                s2 = s2[1].ToString();
            }
            string s3 = strnum.Substring(8, 2);
            if (s3[0].ToString() == "0")
            {
                s3 = s3[1].ToString();
            }
            return pc.ToDateTime(int.Parse(s1), int.Parse(s2), int.Parse(s3), 0, 0, 0, 0, 0);
        }
    }
}
