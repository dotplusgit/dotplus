using HospitalAPI.Core.Models.PhysicalStateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository.IRepository
{
    public interface IPhysicalStateRepository
    {
        IQueryable<PhysicalState> PhysicalStateListAsync(string search);
        Task<PhysicalState> GetPhysicalStateById(int id);
    }
}
