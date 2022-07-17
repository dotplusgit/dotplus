using HospitalAPI.Core.Models.PatientModel;
using HospitalAPI.Core.Models.PrescriptionModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Models.DiagnosisModel
{
    public class Diagnosis
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int? PrescriptionId { get; set; }
        [ForeignKey("PrescriptionId")]
        public Prescription Prescription { get; set; }
        public int? DiseasesCategoryId { get; set; }
        [ForeignKey("DiseasesCategoryId")]
        public DiseasesCategory DiseasesCategory { get; set; }
        public int DiseasesId { get; set; }
        public Diseases Diseases { get; set; }
        [MaxLength(100)]
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
