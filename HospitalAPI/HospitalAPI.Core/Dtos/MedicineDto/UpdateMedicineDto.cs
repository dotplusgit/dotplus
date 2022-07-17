using System;

namespace HospitalAPI.Core.Dtos.MedicineDto
{
    public class UpdateMedicineDto
    {
        public int Id { get; set; }
        public string MedicineType { get; set; }
        public string BrandName { get; set; }
        public string GenericName { get; set; }
        public int ManufacturarId { get; set; }
        public bool IsActive { get; set; }
    }
}
