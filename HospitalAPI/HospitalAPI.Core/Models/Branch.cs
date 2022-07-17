using HospitalAPI.Core.Models.PatientModel.UpazilaAndDistrict;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Models
{
    public class Branch
    {
        public int Id { get; set; }
        [MaxLength(10)]
        public string BranchCode { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }
        public int? DivisionId { get; set; }
        [ForeignKey("DivisionId")]
        public virtual Division Division { get; set; }
        public int? UpazilaId { get; set; }
        [ForeignKey("UpazilaId")]
        public virtual Upazila Upazila { get; set; }

        public int? DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        [MaxLength(100)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(100)]
        public string UpdatedBy { get; set; }
    }
}
