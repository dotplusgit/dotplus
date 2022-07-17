using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.PrescriptionDto
{
    public class AddPrescriptionFromAppDto
    {
        public int PatientId { get; set; }
        public int HospitalId { get; set; }
        public int BranchId { get; set; }
        public string DoctorsObservation { get; set; }
        public string AdviceTest { get; set; }
        public string OH { get; set; }
        public string SystemicExamination { get; set; }
        public string HistoryOfPastIllness { get; set; }
        public string FamilyHistory { get; set; }
        public string AllergicHistory { get; set; }
        public DateTime? NextVisit { get; set; }
        public bool IsTelimedicine { get; set; }
        public bool IsAfternoon { get; set; }
        public string Note { get; set; }
    }
}
