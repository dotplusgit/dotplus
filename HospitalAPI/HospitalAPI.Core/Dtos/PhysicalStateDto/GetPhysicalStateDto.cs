using System;

namespace HospitalAPI.Core.Dtos.PhysicalStateDto
{
    public class GetPhysicalStateDto
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
        public int PatientId { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public int? VisitEntryId { get; set; }
        public string BloodPressureSystolic { get; set; }
        public string BloodPressureDiastolic { get; set; }
        public string HeartRate { get; set; }
        public string BodyTemparature { get; set; }
        public string Appearance { get; set; }
        public string Anemia { get; set; }
        public string Jaundice { get; set; }
        public string Dehydration { get; set; }
        public string Edema { get; set; }
        public string Cyanosis { get; set; }
        public string Heart { get; set; }
        public string Lung { get; set; }
        public string Abdomen { get; set; }
        public string KUB { get; set; }
        public string RbsFbs { get; set; }
        public int? HeightFeet { get; set; }
        public int? HeightInches { get; set; }
        public double? Weight { get; set; }
        public double? BMI { get; set; }
        public string Waist { get; set; }
        public string Hip { get; set; }
        public double? SpO2 { get; set; }
        public double? PulseRate { get; set; }
        public bool? IsLatest { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime EditedOn { get; set; }
        public string EditedBy { get; set; }
    }
}
