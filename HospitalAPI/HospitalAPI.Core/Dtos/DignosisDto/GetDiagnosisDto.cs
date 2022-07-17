using HospitalAPI.Core.Models.DiagnosisModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Dtos.DignosisDto
{
    public class GetDiagnosisDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientFristName { get; set; }
        public string PatientLastName { get; set; }
        public int? PrescriptionId { get; set; }
        public int? DiseasesCategoryId { get; set; }
        public string DiseasesCategory { get; set; }
        public int DiseasesId { get; set; }
        public Diseases Diseases { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
