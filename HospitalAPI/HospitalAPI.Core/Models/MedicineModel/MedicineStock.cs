using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Models.MedicineModel
{
    public class MedicineStock
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public int? MedicineId { get; set; }
        [ForeignKey("MedicineId")]
        public Medicine Medicine { get; set; }
        public double Unit { get; set; }
        public double UnitPrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        [MaxLength(200)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(200)]
        public string UpdatedBy { get; set; }
    }
}
