using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalAPI.Core.Models.MedicinePurchaseAggregate;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.Core.Dtos.MedicinePurchaseDto;
using HospitalAPI.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using HospitalAPI.Core.Models;
using HospitalAPI.Extensions;
using HospitalAPI.Core.Models.ServiceModel;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinePurchaseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPrescriptionRepository _prescriptionRepo;
        private readonly IMedicineStockRepository _medicineRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public MedicinePurchaseController(ApplicationDbContext context,
                                          IPrescriptionRepository prescriptionRepo, 
                                          IMedicineStockRepository medicineRepo,
                                          UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _prescriptionRepo = prescriptionRepo;
            _medicineRepo = medicineRepo;
            _userManager = userManager;
        }

        // GET: api/MedicinePurchase
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicinePurchase>>> GetMedicinePurchase()
        {
            return await _context.MedicinePurchase.Include(m => m.PurchaseMedicineList).ToListAsync();
        }

        // GET: api/MedicinePurchase/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicinePurchase>> GetMedicinePurchase(int id)
        {
            var medicinePurchase = await _context.MedicinePurchase.FindAsync(id);

            if (medicinePurchase == null)
            {
                return NotFound();
            }

            return medicinePurchase;
        }

        // PUT: api/MedicinePurchase/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicinePurchase(int id, MedicinePurchase medicinePurchase)
        {
            if (id != medicinePurchase.Id)
            {
                return BadRequest();
            }

            _context.Entry(medicinePurchase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicinePurchaseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //// POST: api/MedicinePurchase
        //[HttpPost]
        //public async Task<ActionResult<ResponseObject>> PostMedicinePurchase(MedicinePurchaseDto medicinePurchaseDto)
        //{
        //    try
        //    {
        //        var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
        //        if (medicinePurchaseDto.PrescriptionId == null)
        //        {
        //            var itemList = new List<MedicinePurchasePerUnit>();
        //            foreach (var item in medicinePurchaseDto.PurchaseMedicineList)
        //            {
        //                var medicine = await _medicineRepo.GetMedicineById(item.MedicineId);
        //                if (medicine == null)
        //                {
        //                    continue;
        //                }
        //                medicine.Unit -= item.Quantity;
        //                _context.Medicine.Update(medicine);
        //                await _context.SaveChangesAsync();
        //                var quantity = item.Quantity;
        //                var medicinePurchasePerUnit = new MedicinePurchasePerUnit(medicine.Id,
        //                                                                          medicine.BrandName,
        //                                                                          medicine.GenericName,
        //                                                                          medicine.Manufacturar,
        //                                                                          medicine.UnitPrice,
        //                                                                          quantity);
        //                itemList.Add(medicinePurchasePerUnit);
        //            }

        //            var totalPrices = itemList.Sum(item => item.Price * item.Quantity);
        //            var medicinePurchases = new MedicinePurchase(itemList, null,
        //                                                        null, null,
        //                                                        totalPrices, DateTime.Now, currentuser.Email);
        //            _context.MedicinePurchase.Add(medicinePurchases);
        //            await _context.SaveChangesAsync();

        //            return Ok(new ResponseObject { IsValid = true, Message = "success" });

        //        }
        //        var prescription = await _prescriptionRepo.GetPriscriptionById((int)medicinePurchaseDto.PrescriptionId);
        //        var items = new List<MedicinePurchasePerUnit>();
        //        foreach (var item in medicinePurchaseDto.PurchaseMedicineList)
        //        {
        //            var medicine = await _medicineRepo.GetMedicineById(item.MedicineId);
        //            if (medicine == null)
        //            {
        //                continue;
        //            }
        //            medicine.Unit -= item.Quantity;
        //            _context.Medicine.Update(medicine);
        //            await _context.SaveChangesAsync();
        //            var quantity = item.Quantity;
        //            var medicinePurchasePerUnit = new MedicinePurchasePerUnit(medicine.Id,
        //                                                                      medicine.BrandName,
        //                                                                      medicine.GenericName,
        //                                                                      medicine.Manufacturar,
        //                                                                      medicine.UnitPrice,
        //                                                                      quantity);
        //            items.Add(medicinePurchasePerUnit);
        //        }

        //        var totalPrice = items.Sum(item => item.Price * item.Quantity);
        //        if (prescription == null)
        //        {
        //            var medicinePurchases = new MedicinePurchase(items, null,
        //                                                    null, null,
        //                                                    totalPrice, DateTime.Now, currentuser.Email);
        //            _context.MedicinePurchase.Add(medicinePurchases);
        //            await _context.SaveChangesAsync();

        //            return Ok(new ResponseObject { IsValid = true, Message = "success" });
        //        }

        //        var medicinePurchase = new MedicinePurchase(items, prescription.HospitalId,
        //                                                    prescription.Id, prescription.DoctorId,
        //                                                    totalPrice, DateTime.Now, currentuser.Email);
        //        _context.MedicinePurchase.Add(medicinePurchase);
        //        await _context.SaveChangesAsync();

        //        return Ok(new ResponseObject { IsValid = true, Message = "success" });
        //    }
        //    catch(Exception ex)
        //    {
        //        return Ok(new ResponseObject { IsValid = false, Message = "faild", Data=ex.Message.ToString()});
        //    }
            
        //}

        // DELETE: api/MedicinePurchase/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicinePurchase(int id)
        {
            var medicinePurchase = await _context.MedicinePurchase.FindAsync(id);
            if (medicinePurchase == null)
            {
                return NotFound();
            }

            _context.MedicinePurchase.Remove(medicinePurchase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicinePurchaseExists(int id)
        {
            return _context.MedicinePurchase.Any(e => e.Id == id);
        }
    }
}
