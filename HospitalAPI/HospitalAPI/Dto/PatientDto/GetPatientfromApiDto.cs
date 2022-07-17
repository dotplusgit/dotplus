using HospitalAPI.DataAccess.Data;
using HospitalAPI.DataAccess.Repository.IRepository;
using System;

namespace HospitalAPI.Dto.PatientDto
{
    public class GetPatientfromApiDto
    {
        public GetPatientfromApiDto()
        {

        }
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DoB { get; set; }
        //private string patientAge { get; set; }
        //public string Age
        //{
        //    get
        //    {
        //        return string.IsNullOrEmpty(patientAge) ? patientAge : "DOB > Today";
        //    }
        //    set
        //    {
        //        this.patientAge = Calculate.Age(this.DoB);
        //    }
        //}
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public bool PrimaryMember { get; set; }
        public string MembershipRegistrationNumber { get; set; }
        public string Address { get; set; }
        public int DivisionId { get; set; }
        public string Division { get; set; }
        public int UpazilaId { get; set; }
        public string Upazila { get; set; }
        public int DistrictId { get; set; }
        public string District { get; set; }
        public string NID { get; set; }
        public string BloodGroup { get; set; }
        public int? BranchId { get; set; }
        public string BranchName { get; set; }
        public bool IsActive { get; set; }
        public string Note { get; set; }
        public string Covidvaccine { get; set; }
        public string VaccineBrand { get; set; }
        public string VaccineDose { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string UserName { get; set; }
    }
}
