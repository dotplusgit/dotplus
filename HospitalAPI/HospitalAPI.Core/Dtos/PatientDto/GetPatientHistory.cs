using HospitalAPI.Core.Dtos.PhysicalStateDto;
using HospitalAPI.Core.Dtos.PrescriptionDto;
using HospitalAPI.Core.Dtos.VisitEntryDto;
using HospitalAPI.Core.Models.PatientModel;
using HospitalAPI.Core.Models.PhysicalStateModel;
using HospitalAPI.Core.Models.PrescriptionModel;
using HospitalAPI.Core.Models.VisitEntryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.PatientDto
{
    public class GetPatientHistory
    {
        public GetPatientWithPhysicalStatDto Patient { get; set; }
        public List<GetVisitEntryDto> VisitEntries { get; set; }
        public List<GetPrescriptionDto> Prescription { get; set; }
        public List<GetPhysicalStateDto> PhysicalState { get; set; }
    }
}
