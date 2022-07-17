using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalAPI.Core.Models.TelemedicineModel;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.Core.Dtos.TelemedicineDto;
using AutoMapper;
using HospitalAPI.DataAccess.Repository.IRepository;
using HospitalAPI.Core.Models.ServiceModel;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelemedicineController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITelemedicineRepository _telemedicineRepository;

        public TelemedicineController(ApplicationDbContext context, IMapper mapper, ITelemedicineRepository telemedicineRepository)
        {
            _context = context;
            _mapper = mapper;
            _telemedicineRepository = telemedicineRepository;
        }

        // GET: api/Telemedicien
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GetTelemedicineDto>>> GetTelemedicine()
        {
            var telimedecene = await _telemedicineRepository.TelemedicineListAsync();
            return Ok( _mapper.Map<IReadOnlyList<Telemedicine>, IReadOnlyList<GetTelemedicineDto>> (telimedecene));
        }

        // GET: api/Telemedicien/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<GetTelemedicineDto>>> GetTelemedicien(string id)
        {
            var telimedecene = await _telemedicineRepository.TelemedicineListByUser(id);
            return Ok(_mapper.Map<IReadOnlyList<Telemedicine>, IReadOnlyList<GetTelemedicineDto>>(telimedecene));
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTelemedicien(int id, Telemedicine telemedicien)
        //{
        //    if (id != telemedicien.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(telemedicien).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TelemedicienExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Telemedicien
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754


        [HttpPost]
        public async Task<ActionResult<ResponseObject>> PostTelemedicien(AddTelemedicineDto telemedicine)
        {
            Telemedicine newTelemedicine = new Telemedicine(telemedicine.PatietnId, telemedicine.CallerId, telemedicine.ReceiverId, telemedicine.CallingTime);
            _context.Telemedicine.Add(newTelemedicine);
            await _context.SaveChangesAsync();

            return Ok(new ResponseObject { Message = "success", IsValid = true });
        }

        //// DELETE: api/Telemedicien/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTelemedicien(int id)
        //{
        //    var telemedicien = await _context.Telemedicine.FindAsync(id);
        //    if (telemedicien == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Telemedicine.Remove(telemedicien);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TelemedicienExists(int id)
        //{
        //    return _context.Telemedicine.Any(e => e.Id == id);
        //}
    }
}
