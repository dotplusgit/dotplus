using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.FollowUpDto
{
    public class GetFollowUpListDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientMobileNumber { get; set; }
        public DateTime FollowupDate { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string LastVisitHospital { get; set; }
        public DateTime LastVisitDate { get; set; }
        public int? PrescriptionId { get; set; }
        public bool IsFollowup { get; set; }
    }
}
