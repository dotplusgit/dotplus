using HospitalAPI.Core.Models.MedicinePurchaseAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository.IRepository
{
    public interface IMedicinePurchaseRepository
    {
        Task<MedicinePurchase> CreateMedicinePurchaseAsync();
        Task<IReadOnlyList<MedicinePurchase>> GetPatientPurchaseHistoryByPatientId(int id);
    }
}
