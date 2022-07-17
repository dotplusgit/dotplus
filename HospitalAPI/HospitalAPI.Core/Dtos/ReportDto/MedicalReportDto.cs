using HospitalAPI.Core.Dtos.PatientDto;
using HospitalAPI.Core.Dtos.PhysicalStateDto;
using HospitalAPI.Core.Dtos.PrescriptionDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.ReportDto
{
    public class MedicalReportDto
    {
        public GetPatientDto Patient { get; set; }
        public List<VitalAndPrescriptionDto> PatientVitalAndPrescription { get; set; }
    }
}
