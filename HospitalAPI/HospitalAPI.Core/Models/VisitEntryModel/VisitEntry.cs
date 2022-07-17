using System;
using System.ComponentModel.DataAnnotations;
using HospitalAPI.Core.Models.PatientModel;

namespace HospitalAPI.Core.Models.VisitEntryModel
{
    public class VisitEntry
    {

        public int Id { get; set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public DateTime Date { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int Serial { get; set; }
        [MaxLength(15)]
        public string Status { get; set; }
    }
}
