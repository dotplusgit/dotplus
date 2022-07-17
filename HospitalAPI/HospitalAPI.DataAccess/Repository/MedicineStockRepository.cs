using HospitalAPI.Core.Models.MedicineModel;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository
{
    public class MedicineStockRepository : IMedicineStockRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicineStockRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<MedicineStock>> MedicineListAsync()
        {
            var medicines = await _context.MedicineStock
                            .Include(h => h.Hospital)
                            .ToListAsync();
            return medicines;
        }
        public async Task<MedicineStock> GetMedicineById(int id)
        {
            var medicine = await _context.MedicineStock
                            .Include(h => h.Hospital)
                            .FirstOrDefaultAsync(m => m.Id == id);
            return medicine;
        }
    }
}
