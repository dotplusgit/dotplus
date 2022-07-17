using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.PatientDto
{
    public class AddPatientWithPhysicalStatDto
    {
        //patient
        public int HospitalId { get; set; }
        public int? BranchId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Day { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string Age { get; set; }
        public string MobileNumber { get; set; }
        public DateTime? DoB { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public bool PrimaryMember { get; set; }
        public string MembershipRegistrationNumber { get; set; }
        public string Address { get; set; }
        public int? DivisionId { get; set; }
        public string Division { get; set; }
        public int? UpazilaId { get; set; }
        public int? DistrictId { get; set; }
        public string NID { get; set; }
        public string BloodGroup { get; set; }
        public bool IsActive { get; set; }
        public string Note { get; set; }
        public string Covidvaccine { get; set; }
        public string VaccineBrand { get; set; }
        public string VaccineDose { get; set; }
        public DateTime? FirstDoseDate { get; set; }
        public DateTime? SecondDoseDate { get; set; }
        public DateTime? BosterDoseDate { get; set; }
        // Physical Stat
        public int? HeightFeet { get; set; }
        public int? HeightInches { get; set; }
        public double? Weight { get; set; }
        public double? BMI { get; set; }
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
        public string BloodPressureSystolic { get; set; }
        public string BloodPressureDiastolic { get; set; }
        public string HeartRate { get; set; }
        public double? PulseRate { get; set; }
        public double? SpO2 { get; set; }
        public string Waist { get; set; }
        public string Hip { get; set; }
        public bool? IsLatest { get; set; }
    }
}
