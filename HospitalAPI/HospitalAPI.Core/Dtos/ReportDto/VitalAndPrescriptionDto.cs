using HospitalAPI.Core.Dtos.PhysicalStateDto;
using HospitalAPI.Core.Dtos.PrescriptionDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.ReportDto
{
    public class VitalAndPrescriptionDto
    {
        public GetPrescriptionDto Prescription { get; set; }
        public GetPhysicalStateDto Vital { get; set; }
    }
}
