using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.StaticMethos
{
    public static class MonthName
    {
        public static string GetMonth(int value)
        {
            string MonthName = "";
            switch(value)
            {
                case 1:
                    MonthName = "January";
                    break;
                case 2:
                    MonthName = "February";
                    break;
                case 3:
                    MonthName = "March";
                    break;
                case 4:
                    MonthName = "April";
                    break;
                case 5:
                    MonthName = "May";
                    break;
                case 6:
                    MonthName = "June";
                    break;
                case 7:
                    MonthName = "July";
                    break;
                case 8:
                    MonthName = "August";
                    break;
                case 9:
                    MonthName = "September";
                    break;
                case 10:
                    MonthName = "October";
                    break;
                case 11:
                    MonthName = "November";
                    break;
                case 12:
                    MonthName = "December";
                    break;
                default:
                    MonthName = " ";
                    break;
            }
            return MonthName;
        }
    }
}
