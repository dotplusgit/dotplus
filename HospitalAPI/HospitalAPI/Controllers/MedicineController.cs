using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalAPI.Core.Models.MedicineModel;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.Core.Dtos.MedicineDto;
using AutoMapper;
using HospitalAPI.Core.Models;
using Microsoft.AspNetCore.Identity;
using HospitalAPI.Extensions;
using System;
using HospitalAPI.DataAccess.Repository.IRepository;
using HospitalAPI.Helpers;
using HospitalAPI.Core.Models.ServiceModel;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMedicineRepository _medicineRepository;

        public MedicineController(ApplicationDbContext context,
                                  IMapper mapper, 
                                  UserManager<ApplicationUser> userManeger,
                                  IMedicineRepository medicineRepository)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManeger;
            _medicineRepository = medicineRepository;
        }

        // GET: api/Medicine
        [HttpGet]
        public async Task<ActionResult<Pagination<PaginatedList<GetMedicineDto>>>> GetMedicine([FromQuery] Paramps paramps)
        {
            if (paramps.PageSize > 100)
            {
                paramps.PageSize = 100;
            }
            var medicines =   _medicineRepository.GetMedicineList(paramps.SearchString, paramps.Sort);
            var paginateddata = await PaginatedList<Medicine>.CreateAsync(medicines, paramps.PageNumber ?? 1, paramps.PageSize ?? 20);

            var mappedMedicine = _mapper.Map<PaginatedList<Medicine>, PaginatedList<GetMedicineDto>>(paginateddata);
            var result = new Pagination<GetMedicineDto>(paramps.PageNumber ?? 1, paramps.PageSize ?? 20, await medicines.CountAsync(), mappedMedicine);
            return Ok(result);
        }

        // api/medicine/getmedicineforsearch
        [HttpGet("getmedicineforsearch")]
        public async Task<ActionResult<GetMedicineForPrescription>> GetMedicineForSearch(string search)
        {
            IQueryable<Medicine> medicines = _context.Medicine.Where(p => p.IsActive).Include(m => m.MedicineManufacturar);
            if (!String.IsNullOrEmpty(search))
            {
                medicines = medicines.Where(s => (s.BrandName != null && s.BrandName.ToLower().Contains(search.ToLower()))
                                          || (s.GenericName != null && s.GenericName.ToLower().Contains(search.ToLower())));
            }
            var medicineList = await medicines.Take(10).ToListAsync();
            var mappedMedicine = _mapper.Map<IReadOnlyList<Medicine>, IReadOnlyList<GetMedicineForPrescription>>(medicineList);
            return Ok(mappedMedicine);
        }

        [HttpGet("medicinesync")]
        public async Task<ActionResult<GetMedicineForSync>> GetMedicinesync()
        {
            IQueryable<Medicine> medicines = _context.Medicine.Where(p => p.IsActive);
            var medicineList = await medicines.ToListAsync();
            var mappedMedicine = _mapper.Map<IReadOnlyList<Medicine>, IReadOnlyList<GetMedicineForSync>>(medicineList);
            return Ok(mappedMedicine);
        }


        [HttpGet("manufacturarforsearch")]
        public async Task<ActionResult<MedicineManufacturar>> GetManufacturarForSearch(string search)
        {
            IQueryable<MedicineManufacturar> manufacturar = _context.MedicineManufacturar;
            if (!String.IsNullOrEmpty(search))
            {
                manufacturar = manufacturar.Where(s => (s.Name != null && s.Name.ToLower().Contains(search.ToLower())));
            }
            var manufacturarList = await manufacturar.Take(10).ToListAsync();
            // var mappedMedicine = _mapper.Map<IReadOnlyList<Medicine>, IReadOnlyList<GetMedicineForPrescription>>(medicineList);
            return Ok(manufacturarList);
        }

        // GET: api/Medicine/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetMedicineDto>> GetMedicine(int id)
        {
            var medicine = await _context.Medicine.Include(m => m.MedicineManufacturar).FirstOrDefaultAsync(i => i.Id == id);

            if (medicine == null)
            {
                return NotFound();
            }
            var mappedMedicine = _mapper.Map<GetMedicineDto>(medicine);
            return Ok(mappedMedicine);
        }

        // PUT: api/Medicine/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutMedicine(UpdateMedicineDto medicineUpdate)
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var medicine = await _context.Medicine.FindAsync(medicineUpdate.Id);
            if (medicine == null)
            {
                return NotFound();
            }
            medicine.MedicineType = medicineUpdate.MedicineType;
            medicine.BrandName = medicineUpdate.BrandName;
            medicine.MedicineManufacturarId = medicineUpdate.ManufacturarId;
            medicine.GenericName = medicineUpdate.GenericName;
            medicine.IsActive = medicineUpdate.IsActive;
            medicine.UpdatedBy = currentuser.UpdatedBy;
            medicine.UpdatedOn = DateTime.Now;

            _context.Entry(medicine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineExists(medicineUpdate.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new ResponseObject { Message = "success"});
        }

        // POST: api/Medicine
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetMedicineDto>> PostMedicine(AddMedicineDto addMedicineDto)
        {
            var medicines = await _context.Medicine.Where(m => m.BrandName == addMedicineDto.BrandName && m.MedicineType == addMedicineDto.MedicineType).ToListAsync();
            if (medicines.Any())
            {
                return Ok(new ResponseObject { Message = "exist", IsValid = true });  
            }
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var medicine = new Medicine()
            {
                BrandName = addMedicineDto.BrandName,
                GenericName = addMedicineDto.GenericName,
                MedicineManufacturarId = addMedicineDto.ManufacturarId,
                MedicineType = addMedicineDto.MedicineType,
                IsActive = true,
                CreatedBy =  currentuser.Email,
                CreatedOn = DateTime.Now,
                UpdatedBy = currentuser.Email,
                UpdatedOn = DateTime.Now
                
            };
            _context.Medicine.Add(medicine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicine", new { id = medicine.Id }, medicine);
        }

        // DELETE: api/Medicine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            var medicine = await _context.Medicine.FindAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }

            _context.Medicine.Remove(medicine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicineExists(int id)
        {
            return _context.Medicine.Any(e => e.Id == id);
        }
    }
}
