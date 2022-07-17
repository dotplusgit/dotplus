using HospitalAPI.Core.Models.PatientModel.UpazilaAndDistrict;
using HospitalAPI.Core.Models.PhysicalStateModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Models.PatientModel
{
    public class Patient
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        [MaxLength(40)]
        public string FirstName { get; set; }
        [MaxLength(40)]
        public string LastName { get; set; }
        [MaxLength(10)]
        public string Age { get; set; }
        [MaxLength(20)]
        public string MobileNumber { get; set; }
        public DateTime? DoB { get; set; }
        [MaxLength(10)]
        public string Gender { get; set; }
        [MaxLength(15)]
        public string MaritalStatus { get; set; }
        public bool PrimaryMember { get; set; }
        [MaxLength(20)]
        public string MembershipRegistrationNumber { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }
        public int? DivisionId { get; set; }
        [ForeignKey("DivisionId")]
        public virtual Division Division { get; set; }
        public int? UpazilaId { get; set; }
        [ForeignKey("UpazilaId")]
        public virtual Upazila Upazila { get; set; }

        public int? DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }
        //Division
        [MaxLength(25)]
        public string NID { get; set; }
        [MaxLength(5)]
        public string BloodGroup { get; set; }
        public int? BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public bool IsActive { get; set; }
        [MaxLength(300)]
        public string Note { get; set; }
        [MaxLength(5)]
        public string Covidvaccine { get; set; }
        [MaxLength(15)]
        public string VaccineBrand { get; set; }
        [MaxLength(15)]
        public string VaccineDose { get; set; }
        public DateTime? FirstDoseDate { get; set; }
        public DateTime? SecondDoseDate { get; set; }
        public DateTime? BosterDoseDate { get; set; }
        public DateTime CreatedOn { get; set; }
        [MaxLength(200)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(200)]
        public string UpdatedBy { get; set; }
        public List<PhysicalState> PhysicalStates { get; set; }
    }
}
