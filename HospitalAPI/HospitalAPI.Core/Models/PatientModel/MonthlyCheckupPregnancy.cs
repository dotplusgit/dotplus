using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Models.PatientModel
{
    public class MonthlyCheckupPregnancy
    {
        public MonthlyCheckupPregnancy()
        {

        }
        public MonthlyCheckupPregnancy(int pregnancyId, 
                                       bool isCheckedUp, 
                                       DateTime checkupDate, 
                                       DateTime createdOn, 
                                       string createdBy)
        {
            PregnancyId = pregnancyId;
            IsCheckedUp = isCheckedUp;
            CheckupDate = checkupDate;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
        }

        public int Id { get; set; }
        public int PregnancyId { get; set; }
        public Pregnancy Pregnancy { get; set; }
        public bool IsCheckedUp { get; set; }
        public DateTime CheckupDate { get; set; }
        public DateTime CreatedOn { get; set; }
        [MaxLength(100)]
        public string CreatedBy { get; set; }
    }
}
