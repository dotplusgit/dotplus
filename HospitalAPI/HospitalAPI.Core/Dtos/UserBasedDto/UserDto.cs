using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos
{
    public class UserDto
    {
        public string UserId { get; set; }
        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Designation { get; set; }
        public string BMDCRegNo { get; set; }
        public string OptionalEmail { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
    }
}