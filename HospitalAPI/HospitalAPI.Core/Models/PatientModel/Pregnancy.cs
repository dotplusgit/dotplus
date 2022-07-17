using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAPI.Core.Models.PatientModel
{
    public class Pregnancy
    {
        public Pregnancy()
        {
                
        }
        public Pregnancy(int? patientId,
                         DateTime firstDateOfLastPeriod, 
                         DateTime expectedDateOfDelivery, 
                         int? hospitalId, 
                         DateTime createdOn, string createdBy, 
                         DateTime updatedOn, string updatedBy,
                         DateTime? nextCheckp = null
            )
        {
            PatientId = patientId;
            FirstDateOfLastPeriod = firstDateOfLastPeriod;
            ExpectedDateOfDelivery = expectedDateOfDelivery;
            HospitalId = hospitalId;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
            UpdatedOn = updatedOn;
            UpdatedBy = updatedBy;
            NextCheckup = nextCheckp;
        }

        public int Id { get; set; }
        public int? PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }
        public DateTime FirstDateOfLastPeriod { get; set; }
        public DateTime ExpectedDateOfDelivery { get; set; }
        public int? HospitalId { get; set; }
        [ForeignKey("HospitalId")]
        public virtual Hospital Hospital { get; set; }
        public DateTime? NextCheckup { get; set; }
        public ICollection<MonthlyCheckupPregnancy> MonthlyCheckupPregnancy { get; set; }
        public DateTime CreatedOn { get; set; }
        [MaxLength(100)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(100)]
        public string UpdatedBy { get; set; }
    }
}
