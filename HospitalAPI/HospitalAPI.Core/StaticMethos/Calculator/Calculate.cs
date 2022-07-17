using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Calculator
{
    public static class Calculate
    {
        public static double BMI(int? feet, int? inch, double? Weight)
        {
            var FeetToInch = 0;
            if (Weight == null || Weight < 1)
            {
                return 0;
            }
            if ((feet == null && inch == null) || (feet < 1 && inch < 1))
            {
                return 0;
            }
            if (feet != null)
            {
                FeetToInch = (int)(feet * 12);
            }
            if (inch != null)
            {
                FeetToInch += (int)inch;
            }
            var HeightInMeter = FeetToInch * 0.0254;
            var BMI = Weight / (HeightInMeter * HeightInMeter);
            if (BMI != null)
            {
                return Math.Round(BMI.Value, 2);
            }
            else
            {
                return 0;
            }
        }
        public static string Age(DateTime Dob)
        {
            try
            {
                DateTime Now = DateTime.Now;
                int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
                DateTime PastYearDate = Dob.AddYears(Years);
                int Months = 0;
                for (int i = 1; i <= 12; i++)
                {
                    if (PastYearDate.AddMonths(i) == Now)
                    {
                        Months = i;
                        break;
                    }
                    else if (PastYearDate.AddMonths(i) >= Now)
                    {
                        Months = i - 1;
                        break;
                    }
                }
                int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;

                if (Years < 2)
                {
                    if (Months < 2)
                    {
                        if (Days < 2)
                        {
                            return String.Format("{0} Year {1} Month {2} Day", Years, Months, Days);
                        }
                        else
                        {
                            return String.Format("{0} Year {1} Month {2} Days", Years, Months, Days);
                        }
                    }
                    else
                    {
                        return String.Format("{0} Year {1} Months {2} Days", Years, Months, Days);
                    }
                }
                else
                {
                    if (Months < 2)
                    {
                        if (Days < 2)
                        {
                            return String.Format("{0} Years {1} Month {2} Day", Years, Months, Days);
                        }
                        else
                        {
                            return String.Format("{0} Years {1} Month {2} Days", Years, Months, Days);
                        }
                    }
                    else
                    {
                        return String.Format("{0} Years {1} Months {2} Days", Years, Months, Days);
                    }
                }
            }
            catch
            {
                return "0 year 0 month 0 day";
            }
        }


        public static DateTime DobFromAge(int age)
        {
            int day = 01;
            int month = 01;
            int year = DateTime.Today.Year - age;
            string DobinStr = $"{year}/{month}/{day}";
            DateTime Dob = DateTime.Parse(DobinStr);
            return Dob;
        }

        public static DateTime DOBFromDayMonthYear(int? day, int? month, int? year)
        {
            try
            {
                if (day == null)
                {
                    day = 0;
                }
                if (month == null)
                {
                    month = 0;
                }
                if (year == null)
                {
                    year = 0;
                }
                DateTime dob = DateTime.Now.AddYears((int)-year).AddMonths((int)-month).AddDays((int)-day);
                return dob;
                //DateTime today = DateTime.Now.Date;
                //int days = today.Day;
                //int months = today.Month;
                //int years = today.Year - (int)year;
                //if (day > days)
                //{
                //    int InputDaysWithPreviousMonthTotalDays = days + DateTime.DaysInMonth(today.AddMonths(-1).Year, today.AddMonths(-1).Month);
                //    days = InputDaysWithPreviousMonthTotalDays - (int)day;
                //    months--;
                //    if (month > months)
                //    {
                //        int AddAYearWithmonths = months + 12;
                //        months = AddAYearWithmonths - (int)month;
                //        years--;
                //    }
                //    else
                //    {
                //        months -= (int)month;
                //    }
                //}
                //else
                //{
                //    days -= (int)day;
                //    if (month > months)
                //    {
                //        int AddAYearWithmonths = months + 12;
                //        months = AddAYearWithmonths - (int)month;
                //        years--;
                //    }
                //    else
                //    {
                //        months -= (int)month;
                //    }
                //}
                //string DobinStr = $"{years}/{months}/{days}";
                //DateTime Dob = DateTime.Parse(DobinStr);

            }
            catch
            {
                return DateTime.Now.Date.AddYears(-1000);
            }
        }
    }
}
