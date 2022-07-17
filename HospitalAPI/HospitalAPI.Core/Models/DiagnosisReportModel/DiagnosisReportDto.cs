using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Models.DiagnosisReportModel
{
    public class DiagnosisReportWithSpecifcation
    {
        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public string CheckedBy { get; set; }
        public IReadOnlyList<DiagnosisReportDto> DiagnosisReport { get; set; }
        //Calculate Total
        public int UpToPreviousMonthTotal { get; set; }
        // Male
        public int MZeroToFiveTotal { get; set; }
        public int MSixToFifteenTotal { get; set; }
        public int MSixteenToThirtyTotal { get; set; }
        public int MThirtyOneToFourtyFiveTotal { get; set; }
        public int MFourtyFiveToSixtyTotal { get; set; }
        public int MSixtyPlusTotal { get; set; }
        public int MAgeGroupTotal { get; set; }
        //Female
        public int FMZeroToFiveTotal { get; set; }
        public int FMSixToFifteenTotal { get; set; }
        public int FMSixteenToThirtyTotal { get; set; }
        public int FMThirtyOneToFourtyFiveTotal { get; set; }
        public int FMFourtyFiveToSixtyTotal { get; set; }
        public int FMSixtyPlusTotal { get; set; }
        public int FMAgeGroupTotal { get; set; }
        public int ThisMonthTotal { get; set; }
        public int ComulativeUpToThisMonthTotal { get; set; }
    }
    public class DiagnosisReportDto
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Total { get; set; }
        public int UpToPreviousMonth { get; set; }
        public int ComulativeUpToThisMonth { get; set; }
        public GenderMale GenderMale { get; set; }
        public GenderFeMale GenderFemale { get; set; }
        public GenderOthers GenderOthers { get; set; }
    }
    public class GenderMale
    {
        public string Name { get; set; }
        public ReportByAge Ages { get; set; }
    }
    public class GenderFeMale
    {
        public string Name { get; set; }
        public ReportByAge Ages { get; set; }
    }
    public class GenderOthers
    {
        public string Name { get; set; }
        public ReportByAge Ages { get; set; }
    }
    public class ReportByAge
    {
        public int ZeroToFive { get; set; }
        public int SixToFifteen { get; set; }
        public int SixteenToThirty { get; set; }
        public int ThirtyOneToFourtyFive { get; set; }
        public int FourtyFiveToSixty { get; set; }
        public int SixtyPlus { get; set; }
        public int Total { get; set; }
    }
}
