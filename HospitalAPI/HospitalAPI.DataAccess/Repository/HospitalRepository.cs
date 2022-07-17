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
    public class HospitalRepository : IHospitalRepository
    {
        private readonly ApplicationDbContext _context;

        public HospitalRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<Hospital>> HospitalListAsync()
        {
            var hospitals = await _context.Hospital.Include(b => b.Branch)
                                                   .Include(d => d.Division)
                                                   .Include(d => d.District)
                                                   .Include(u => u.Upazila)
                                                   .OrderBy(h => h.Name)
                                                   .ToListAsync();
            return hospitals;
        }
        public async Task<IReadOnlyList<Hospital>> HospitalListShortByNameAsync()
        {
            var hospitals = await _context.Hospital.OrderBy(h => h.Name)
                                                   .ToListAsync();
            return hospitals;
        }
        public async Task<Hospital> GetHospitalById(int id)
        {
            var hospital = await _context.Hospital.Include(b => b.Branch)
                                                  .Include(d => d.Division)
                                                  .Include(d => d.District)
                                                  .Include(u => u.Upazila)
                                                  .FirstOrDefaultAsync(b => b.Id == id);
            return hospital;
        }


    }
}
