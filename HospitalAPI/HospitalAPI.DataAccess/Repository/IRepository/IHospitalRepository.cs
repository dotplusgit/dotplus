using HospitalAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository.IRepository
{
    public interface IHospitalRepository
    {
        Task<IReadOnlyList<Hospital>> HospitalListAsync();
        Task<IReadOnlyList<Hospital>> HospitalListShortByNameAsync();
        Task<Hospital> GetHospitalById(int id);
    }
}
