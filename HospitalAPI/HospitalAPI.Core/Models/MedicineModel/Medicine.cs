using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalAPI.Core.Models.MedicineModel
{
    public class Medicine
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string MedicineType { get; set; }
        [MaxLength(40)]
        public string BrandName { get; set; }
        [MaxLength(50)]
        public string GenericName { get; set; }
        public int? MedicineManufacturarId { get; set; }
        [ForeignKey("MedicineManufacturarId")]
        public MedicineManufacturar MedicineManufacturar { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        [MaxLength(200)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(200)]
        public string UpdatedBy { get; set; }
    }
}
