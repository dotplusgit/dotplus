using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int HospitalId { get; set; }
        [MaxLength(200)]
        public string HospitalName { get; set; }
        public Hospital Hospital { get; set; }
        [MaxLength(40)]
        public string FirstName { get; set; }
        [MaxLength(40)]
        public string LastName { get; set; }
        [MaxLength(40)]
        public string Designation { get; set; }
        [MaxLength(20)]
        public string BMDCRegNo { get; set; }
        [MaxLength(50)]
        public string OptionalEmail { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        [MaxLength(100)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(100)]
        public string UpdatedBy { get; set; }
        [MaxLength(20)]
        public string Role { get; set; }

    }
}
