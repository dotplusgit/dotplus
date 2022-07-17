using HospitalAPI.Core.Models.MedicinePurchaseAggregate;
using HospitalAPI.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository
{
    public class MedicinePurchaseRepository : IMedicinePurchaseRepository
    {
        public Task<MedicinePurchase> CreateMedicinePurchaseAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<MedicinePurchase>> GetPatientPurchaseHistoryByPatientId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
