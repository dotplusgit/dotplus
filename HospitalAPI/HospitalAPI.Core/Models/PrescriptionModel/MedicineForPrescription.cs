using HospitalAPI.Core.Models.MedicineModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAPI.Core.Models.PrescriptionModel
{
    public class MedicineForPrescription
    {
        public int Id { get; set; }
        public int? MedicineId { get; set; }
        [ForeignKey("MedicineId")]
        public Medicine Medicine { get; set; }
        public int? PrescriptionId { get; set; }
        [ForeignKey("PrescriptionId")]
        public Prescription Prescription { get; set; }
        [MaxLength(60)]
        public string Dose { get; set; }
        [MaxLength(60)]
        public string Time { get; set; }
        [MaxLength(100)]
        public string Comment { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
