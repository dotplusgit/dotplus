using HospitalAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository.IRepository
{
    public interface IBranchRepository
    {
        Task<IReadOnlyList<Branch>> BranchListAsync();
        Task<IReadOnlyList<Branch>> BranchListSortByNameAsync();
        Task<Branch> GetBranchById(int id);
    }
}
