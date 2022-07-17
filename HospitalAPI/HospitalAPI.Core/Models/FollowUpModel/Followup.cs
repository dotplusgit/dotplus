using HospitalAPI.Core.Models.PatientModel;
using HospitalAPI.Core.Models.PrescriptionModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Models.FollowUpModel
{
    public class Followup
    {
        public Followup()
        {
        }
        public Followup(int? patientId, string applicationUserId, int? prescriptionId, int? hospitalId, DateTime followupDate, bool isFollowup)
        {
            PatientId = patientId;
            ApplicationUserId = applicationUserId;
            PrescriptionId = prescriptionId;
            HospitalId = hospitalId;
            FollowupDate = followupDate;
            IsFollowup = isFollowup;
        }

        public int Id { get; set; }
        public int? PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public int? PrescriptionId { get; set; }
        [ForeignKey("PrescriptionId")]
        public Prescription Prescription { get; set; }
        public int? HospitalId { get; set; }
        [ForeignKey("HospitalId")]
        public Hospital Hospital { get; set; }
        public DateTime FollowupDate { get; set; }
        public bool IsFollowup { get; set; } = false;
    }
}
