using HospitalAPI.Core.Dtos.DignosisDto;
using HospitalAPI.Core.Models.DiagnosisModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.PrescriptionDto
{
    public class UpdatePrescriptionDto
    {
        public int Id { get; set; }
        public string DoctorsObservation { get; set; }
        public string AdviceMedication { get; set; }
        public string AdviceTest { get; set; }
        public string OH { get; set; }
        public string MH { get; set; }
        public string DX { get; set; }
        public string SystemicExamination { get; set; }
        public string HistoryOfPastIllness { get; set; }
        public string FamilyHistory { get; set; }
        public string AllergicHistory { get; set; }
        public DateTime? NextVisit { get; set; }
        public bool IsTelimedicine { get; set; }
        public string Note { get; set; }
        public IReadOnlyList<AddDagnosisDto> Diagnosis { get; set; }
        public IReadOnlyList<AddMedicineForPrescriptionDto> MedicineForPrescription { get; set; }
    }
}