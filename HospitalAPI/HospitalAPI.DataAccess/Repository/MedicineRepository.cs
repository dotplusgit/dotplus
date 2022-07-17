using System;
using HospitalAPI.Core.Models.MedicineModel;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.DataAccess.Repository.IRepository;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HospitalAPI.DataAccess.Repository
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicineRepository(ApplicationDbContext context)
        {
            _context = context;
        }
         
        public IQueryable<Medicine> GetMedicineList(string search, string sort)
        {
            IQueryable<Medicine> medicines = _context.Medicine.Where(p => p.IsActive).Include(m => m.MedicineManufacturar);
            if (!String.IsNullOrEmpty(search))
            {
                medicines = medicines.Where(s => (s.BrandName != null && s.BrandName.ToLower().Contains(search.ToLower()))
                                          || (s.GenericName != null && s.GenericName.ToLower().Contains(search.ToLower())));
            }
            switch (sort)
            {
                case "brandNameAsc":
                    medicines = medicines.OrderBy(p => p.BrandName);
                    break;
                case "brandNameDsc":
                    medicines = medicines.OrderByDescending(p => p.BrandName);
                    break;
                case "genericNameAsc":
                    medicines = medicines.OrderBy(p => p.GenericName);
                    break;
                case "dobDsc":
                    medicines = medicines.OrderByDescending(p => p.GenericName);
                    break;
                default:
                    medicines = medicines.OrderByDescending(p => p.Id);
                    break;
            }
            return medicines;
        }
    }
}

