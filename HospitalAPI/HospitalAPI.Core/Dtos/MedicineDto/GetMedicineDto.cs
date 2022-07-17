using System;

namespace HospitalAPI.Core.Dtos.MedicineDto
{
    public class GetMedicineDto
    {
        public int Id { get; set; }
        public string MedicineType { get; set; }
        public string BrandName { get; set; }
        public string GenericName { get; set; }
        public string Manufacturar { get; set; }
        public string ManufacturarId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
