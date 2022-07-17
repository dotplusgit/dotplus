using HospitalAPI.Core.Models.PatientModel;
using HospitalAPI.Core.Models.PrescriptionModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HospitalAPI.Core.Models.MedicinePurchaseAggregate
{
    public class MedicinePurchase
    {
        public MedicinePurchase()
        {

        }
        public MedicinePurchase(IReadOnlyList<MedicinePurchasePerUnit> purchaseMedicineList, 
                                int? hospitalId,
                                int? prescriptionId, 
                                string doctorId, 
                                double totalPrice, 
                                DateTime createdOn, 
                                string createdBy)
        {
            HospitalId = hospitalId;
            PrescriptionId = prescriptionId;
            DoctorId = doctorId;
            PurchaseMedicineList = purchaseMedicineList;
            TotalPrice = totalPrice;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
        }

        public int Id { get; set; }
        public int? HospitalId { get; set; }
        public virtual Hospital Hospital { get; set; }
        public int? PrescriptionId { get; set; }
        public virtual Prescription Prescription { get; set; }
        public string DoctorId { get; set; }
        public IReadOnlyList<MedicinePurchasePerUnit> PurchaseMedicineList { get; set; }
        public double TotalPrice { get; set; }
        public DateTime CreatedOn { get; set; }
        [MaxLength(200)]
        public string CreatedBy { get; set; }
    }
}
