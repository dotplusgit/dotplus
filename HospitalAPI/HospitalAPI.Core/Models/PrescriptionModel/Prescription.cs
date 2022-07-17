using HospitalAPI.Core.Models.DiagnosisModel;
using HospitalAPI.Core.Models.PatientModel;
using HospitalAPI.Core.Models.PhysicalStateModel;
using HospitalAPI.Core.Models.VisitEntryModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAPI.Core.Models.PrescriptionModel
{
    public class Prescription
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        [MaxLength(200)]
        public string DoctorId { get; set; }
        [MaxLength(40)]
        public string DoctorFirstName { get; set; }
        [MaxLength(40)]
        public string DoctorLastName { get; set; }
        [MaxLength(50)]
        public string BMDCRegNo { get; set; }
        [MaxLength(50)]
        public string OptionalEmail { get; set; }
        public int VisitEntryId { get; set; }
        public VisitEntry VisitEntry { get; set; }
        [MaxLength(2000)]
        public string DoctorsObservation { get; set; }
        public int? PhysicalStateId { get; set; }
        [ForeignKey("PhysicalStateId")]
        public virtual PhysicalState PhysicalState { get; set; }
        [MaxLength(2000)]
        public string AdviceMedication { get; set; }
        public ICollection<MedicineForPrescription> MedicineForPrescription { get; set; }
        [MaxLength(2000)]
        public string AdviceTest { get; set; }
        [MaxLength(500)]
        public string OH { get; set; }
        [MaxLength(500)]
        public string MH { get; set; }
        [MaxLength(500)]
        public string SystemicExamination { get; set; }
        [MaxLength(500)]
        public string HistoryOfPastIllness { get; set; }
        [MaxLength(500)]
        public string FamilyHistory { get; set; }
        [MaxLength(500)]
        public string AllergicHistory { get; set; }
        [MaxLength(500)]
        public string DX { get; set; }
        public ICollection<Diagnosis> Diagnosis { get; set; }
        public DateTime? NextVisit { get; set; }
        public bool IsTelimedicine { get; set; }
        public bool IsAfternoon { get; set; }
        [MaxLength(2000)]
        public string Note { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
