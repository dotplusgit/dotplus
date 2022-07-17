using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.PatientDto
{
    public class UpdatePatientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public DateTime? DoB { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public bool PrimaryMember { get; set; }
        public string MembershipRegistrationNumber { get; set; }
        public string Address { get; set; }
        public int? DivisionId { get; set; }
        public int? UpazilaId { get; set; }
        public int? DistrictId { get; set; }
        public string NID { get; set; }
        public string BloodGroup { get; set; }
        public int BranchId { get; set; }
        public int HospitalId { get; set; }
        public bool IsActive { get; set; }
        public string Note { get; set; }
        public string Covidvaccine { get; set; }
        public string VaccineBrand { get; set; }
        public string VaccineDose { get; set; }
        public DateTime? FirstDoseDate { get; set; }
        public DateTime? SecondDoseDate { get; set; }
        public DateTime? BosterDoseDate { get; set; }
    }
}
