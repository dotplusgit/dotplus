using System;

namespace HospitalAPI.Core.Dtos.UserBasedDto
{
    public class UpdateUserDto
    {
        public string UserId { get; set; }
        public int HospitalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public string BMDCRegNo { get; set; }
        public string OptionalEmail { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool IsActive { get; set; }
        public string UpdatedBy { get; set; }
        public string Role { get; set; }
    }
}
