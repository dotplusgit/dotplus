using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.ReportDto
{
    public class PrescriptionCountAccordingToBranchBetweenTwoDatesDto
    {
        public int BranchId { get; set; }
        public string DoctorsId { get; set; }
        public string DoctorsName { get; set; }
        public string BranchName { get; set; }
        public int TotalMalePatient { get; set; }
        public int TotalFemalePatient { get; set; }
        public int Telimedicine { get; set; }
        public int TotalPatient { get; set; }
    }

    public class PrescriptionCountAccordingToBranchBetweenTwoDatesDtoTotals
    {
        public int TotalMale { get; set; }
        public int TotalFeMale { get; set; }
        public int Telimedicine { get; set; }
        public int Total { get; set; }
        public List<PrescriptionCountAccordingToBranchBetweenTwoDatesDto> PatientsCountAccordingToBranchBetweenTwoDatesDto { get; set; }

    }
}
