using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Models.DiagnosisModel
{
    public class Diseases
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int? DiseasesCategoryId { get; set; }
        [ForeignKey("DiseasesCategoryId")]
        public DiseasesCategory DiseasesCategory { get; set; }
    }
}
