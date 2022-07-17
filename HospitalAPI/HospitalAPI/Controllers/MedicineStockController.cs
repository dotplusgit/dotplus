using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalAPI.Core.Models.MedicineModel;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.DataAccess.Repository.IRepository;
using AutoMapper;
using HospitalAPI.Core.Dtos.MedicineDto;
using HospitalAPI.Core.Models;
using Microsoft.AspNetCore.Identity;
using HospitalAPI.Extensions;
using HospitalAPI.Errors;
using HospitalAPI.Core.Models.ServiceModel;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineStockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMedicineStockRepository _medicineRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public MedicineStockController(ApplicationDbContext context,
                                  IMedicineStockRepository medicineRepo,
                                  IMapper mapper,
                                  UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _medicineRepo = medicineRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/Medicine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetMedicineStockDto>>> GetMedicine()
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var medicines = await _medicineRepo.MedicineListAsync();
            var hospitalWiseMedicine = medicines.Where(h => h.HospitalId == currentuser.HospitalId).ToList();
            return Ok(_mapper.Map<IReadOnlyList<MedicineStock>, IReadOnlyList<GetMedicineStockDto>>(hospitalWiseMedicine));
        }

        // GET: api/Medicine/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetMedicineStockDto>> GetMedicine(int id)
        {
            var medicine = await _medicineRepo.GetMedicineById(id);

            if (medicine == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GetMedicineStockDto>(medicine));
        }

        // PUT: api/Medicine/5
        [HttpPut]
        public async Task<IActionResult> PutMedicine(UpdateMedicineStockDto updateMedicine)
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var medicine = await _context.MedicineStock.FirstAsync(i => i.Id == updateMedicine.Id);
            if (medicine == null)
            {
                return NotFound(new ApiResponse(404));
            }
            try
            {
                medicine.HospitalId = currentuser.HospitalId;
                medicine.IsActive = updateMedicine.IsActive;
                medicine.Unit = updateMedicine.Unit;
                medicine.UnitPrice = updateMedicine.UnitPrice;
                medicine.UpdatedBy = currentuser.Email;
                medicine.UpdatedOn = DateTime.Now;
                _context.Entry(medicine).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }

            return Ok(new ResponseObject { Message = "success", IsValid = true});
        }

        // POST: api/Medicine
        [HttpPost]
        public async Task<ActionResult<ResponseObject>> PostMedicine(AddMedicineStockDto addMedicine)
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            try
            {
                var medicine = new MedicineStock
                {
                    HospitalId = currentuser.HospitalId,
                    Unit = addMedicine.Unit,
                    UnitPrice = addMedicine.UnitPrice,
                    CreatedBy = currentuser.Email,
                    CreatedOn = DateTime.Now,
                    UpdatedBy = currentuser.Email,
                    UpdatedOn = DateTime.Now
                };
                _context.MedicineStock.Add(medicine);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400, ex.Message));
            }
            return Ok(new ResponseObject { Message = "success", IsValid = true });
        }
        #region
        //// DELETE: api/Medicine/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMedicine(int id)
        //{
        //    var medicine = await _context.Medicine.FindAsync(id);
        //    if (medicine == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Medicine.Remove(medicine);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool MedicineExists(int id)
        //{
        //    return _context.Medicine.Any(e => e.Id == id);
        //}
        #endregion
    }
}
