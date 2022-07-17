using HospitalAPI.Core.Models;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ApplicationDbContext context;

        public BranchRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IReadOnlyList<Branch>> BranchListAsync()
        {
            var branches = await context.Branch.Include(d=>d.Division)
                                               .Include(d => d.District)
                                               .Include(u => u.Upazila)
                                               .OrderBy(b => b.Name)
                                               .ToListAsync();
            return branches;
        }

        public async Task<IReadOnlyList<Branch>> BranchListSortByNameAsync()
        {
            var branches = await context.Branch.OrderBy(b => b.Name).ToListAsync();
            return branches;
        }

        public async Task<Branch> GetBranchById(int id)
        {
            var branch = await context.Branch.Include(d => d.Division)
                                              .Include(d => d.District)
                                              .Include(u => u.Upazila)
                                              .FirstOrDefaultAsync(b => b.Id == id);
            return branch;
        }
    }
}
