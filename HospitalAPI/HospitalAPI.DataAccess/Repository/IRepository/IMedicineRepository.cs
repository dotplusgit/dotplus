using HospitalAPI.Core.Models.MedicineModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository.IRepository
{
    public interface IMedicineRepository
    {
        IQueryable<Medicine> GetMedicineList(string search, string sort);
    }
}
