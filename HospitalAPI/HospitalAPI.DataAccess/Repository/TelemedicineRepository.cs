using HospitalAPI.Core.Models.TelemedicineModel;
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
    public class TelemedicineRepository : ITelemedicineRepository
    {
        private readonly ApplicationDbContext context;

        public TelemedicineRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IReadOnlyList<Telemedicine>> TelemedicineListAsync()
        {
            IReadOnlyList<Telemedicine> telemedicines =await context.Telemedicine.OrderByDescending(t => t.Id)
                .Include(t => t.Caller)
                .Include(t => t.Receiver)
                .Take(50)
                .ToListAsync();
            return telemedicines;
        }

        public async Task<IReadOnlyList<Telemedicine>> TelemedicineListByUser(string id)
        {
            IReadOnlyList<Telemedicine> telemedicines = await context.Telemedicine.Where(t => t.CallerId == id).OrderByDescending(t => t.Id).Take(20).ToListAsync();
            return telemedicines;
        }
    }
}
