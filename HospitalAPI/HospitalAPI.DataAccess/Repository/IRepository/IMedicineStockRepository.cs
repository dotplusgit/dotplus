using HospitalAPI.Core.Models.MedicineModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository.IRepository
{
    public interface IMedicineStockRepository
    {
        Task<IReadOnlyList<MedicineStock>> MedicineListAsync();
        Task<MedicineStock> GetMedicineById(int id);
    }
}
