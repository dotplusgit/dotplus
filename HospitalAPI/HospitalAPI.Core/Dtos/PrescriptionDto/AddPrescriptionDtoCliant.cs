using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.PrescriptionDto
{
    public class AddPrescriptionDtoCliant
    {
        public int? HospitalId { get; set; }
        public int VisitEntryId { get; set; }
       // public int? PhysicalStateId { get; set; }
        public string DoctorsObservation { get; set; }
        public string AdviceMedication { get; set; }
        public string AdviceTest { get; set; }
        public string OH { get; set; }
        public string MH { get; set; }
        public string SystemicExamination { get; set; }
        public string HistoryOfPastIllness { get; set; }
        public string FamilyHistory { get; set; }
        public string AllergicHistory { get; set; }
        public string DX { get; set; }
        public string Note { get; set; }
    }
}
