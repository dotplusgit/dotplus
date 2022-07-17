using HospitalAPI.Core.Models.TelemedicineModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository.IRepository
{
    public interface ITelemedicineRepository
    {
        Task<IReadOnlyList<Telemedicine>> TelemedicineListAsync();
        Task<IReadOnlyList<Telemedicine>> TelemedicineListByUser(string id);
    }
}
