using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.ReportDto
{
    public class WeeklyPatientCountDto
    {
        public WeeklyPatientCountDto()
        {

        }
        public WeeklyPatientCountDto(int count, String lastDate)
        {
            Count = count;
            LastDate = lastDate;
        }

        public int Count { get; set; }
        public String LastDate { get; set; }
    }

    public class HomePageReportDto
    {
        public HomePageReportDto()
        {

        }

        public HomePageReportDto(string monthName, int totalData, List<WeeklyPatientCountDto> weeklyDataCounts)
        {
            MonthName = monthName;
            TotalData = totalData;
            WeeklyDataCounts = weeklyDataCounts;
        }

        public string MonthName { get; set; }
        public int TotalData { get; set; }
        public List<WeeklyPatientCountDto> WeeklyDataCounts { get; set; }
    }
}
