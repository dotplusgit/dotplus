using HospitalAPI.Core.Models.PatientModel.UpazilaAndDistrict;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Core.Models
{
    public class Hospital
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }
        public int? BranchId { get; set; }
        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; }
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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MM yyyy}")]
        public DateTime CreatedOn { get; set; }
        [MaxLength(100)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(100)]
        public string UpdatedBy { get; set; }
    }
}
