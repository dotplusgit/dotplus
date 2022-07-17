using HospitalAPI.Core.Models.PatientModel;
using HospitalAPI.Core.Models.VisitEntryModel;
using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalAPI.Core.Models.PhysicalStateModel
{
    public class PhysicalState
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int? VisitEntryId { get; set; }
        public virtual VisitEntry VisitEntry { get; set; }
        [MaxLength(5)]
        public string BloodPressureSystolic { get; set; }
        [MaxLength(5)]
        public string BloodPressureDiastolic { get; set; }
        [MaxLength(5)]
        public string HeartRate { get; set; }
        [MaxLength(5)]
        public string BodyTemparature { get; set; }
        public int? HeightFeet { get; set; }
        public int? HeightInches { get; set; }
        public double? Weight { get; set; }
        public double? BMI { get; set; }
        [MaxLength(5)]
        public string Waist { get; set; }
        [MaxLength(5)]
        public string Hip { get; set; }
        public double? SpO2 { get; set; }
        public double? PulseRate { get; set; }

        // General Examination
        [MaxLength(10)]
        public string Appearance { get; set; }
        [MaxLength(10)]
        public string Anemia { get; set; }
        [MaxLength(10)]
        public string Jaundice { get; set; }
        [MaxLength(10)]
        public string Dehydration { get; set; }
        [MaxLength(10)]
        public string Edema { get; set; }
        [MaxLength(10)]
        public string Cyanosis { get; set; }
        [MaxLength(10)]
        public string Heart { get; set; }
        [MaxLength(10)]
        public string Lung { get; set; }
        [MaxLength(10)]
        public string Abdomen { get; set; }
        [MaxLength(10)]
        public string KUB { get; set; }
        [MaxLength(20)]
        public string RbsFbs { get; set; }
        //*******------------------------
        public bool? IsLatest { get; set; }
        public DateTime CreatedOn { get; set; }
        [MaxLength(200)]
        public string CreatedBy { get; set; }
        public DateTime EditedOn { get; set; }
        [MaxLength(200)]
        public string EditedBy { get; set; }
    }
}
