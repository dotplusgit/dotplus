using HospitalAPI.Core.Calculator;
using HospitalAPI.Core.Dtos.DignosisDto;
using HospitalAPI.Core.Dtos.PatientDto;
using HospitalAPI.Core.Dtos.PhysicalStateDto;
using HospitalAPI.Core.Models.PatientModel;
using System;
using System.Collections.Generic;

namespace HospitalAPI.Core.Dtos.PrescriptionDto
{
    public class GetPrescriptionDto
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
        public int PatientId { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientBloodGroup { get; set; }
        public DateTime PatientDob { get; set; }
        private string patientage { get; set; }

        public string PatientAge
        {
            get
            {
                return patientage;
            }
            set
            {
                patientage = Calculate.Age(PatientDob);
            }
        }
        public string PatientMobile { get; set; }
        public string PatientGender { get; set; }
        public ICollection<GetMedicineForPrescriptionDto> MedicineForPrescription { get; set; }
        public GetPhysicalStateDto PhysicalStat { get; set; }
        public ICollection<GetDiagnosisDto> Diagnosis { get; set; }
        public string DoctorId { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string BMDCRegNo { get; set; }
        public string OptionalEmail { get; set; }
        public int VisitEntryId { get; set; }
        public string DoctorsObservation { get; set; }
        //public int PhysicalStateId { get; set; }
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
        public string Covidvaccine { get; set; }
        public string VaccineBrand { get; set; }
        public string VaccineDose { get; set; }
        public string Note { get; set; }
        public bool IsTelimedicine { get; set; }
        public bool IsAfternoon { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }

}
