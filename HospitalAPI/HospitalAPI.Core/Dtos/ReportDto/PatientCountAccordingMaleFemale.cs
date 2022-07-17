using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.ReportDto
{
    public class PatientCountAccordingMaleFemale
    {
        public List<PatientCountAccordingToBranchBetweenTwoDatesDto> MalePatient { get; set; }
        public List<PatientCountAccordingToBranchBetweenTwoDatesDto> FeMalePatient { get; set; }
    }
}
