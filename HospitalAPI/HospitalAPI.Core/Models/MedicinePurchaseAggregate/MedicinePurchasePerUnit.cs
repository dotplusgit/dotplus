using System.ComponentModel.DataAnnotations;

namespace HospitalAPI.Core.Models.MedicinePurchaseAggregate
{
    public class MedicinePurchasePerUnit
    {
        public MedicinePurchasePerUnit()
        {

        }

        public MedicinePurchasePerUnit(int medicineId, string brandName, string genericName, string manufacturar, double price, int quantity)
        {
            MedicineId = medicineId;
            BrandName = brandName;
            GenericName = genericName;
            Manufacturar = manufacturar;
            Price = price;
            Quantity = quantity;
        }

        public int Id { get; set; }
        public int MedicineId { get; set; }
        [MaxLength(100)]
        public string BrandName { get; set; }
        [MaxLength(100)]
        public string GenericName { get; set; }
        [MaxLength(200)]
        public string Manufacturar { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
