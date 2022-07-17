using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalAPI.Core.Models.PatientModel.UpazilaAndDistrict;
using HospitalAPI.DataAccess.Data;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpazilaAndDistrictController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UpazilaAndDistrictController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region
        // GET: api/Divisions
        [HttpGet("division")]
        public async Task<ActionResult<IEnumerable<Division>>> GetDivision()
        {
            return await _context.Division.ToListAsync();
        }

        // GET: api/Divisions/5
        [HttpGet("division/{id}")]
        public async Task<ActionResult<Division>> GetDivision(int id)
        {
            var division = await _context.Division.FindAsync(id);

            if (division == null)
            {
                return NotFound();
            }

            return division;
        }

        // PUT: api/Divisions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("division/{id}")]
        public async Task<IActionResult> PutDivision(int id, Division division)
        {
            if (id != division.Id)
            {
                return BadRequest();
            }

            _context.Entry(division).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DivisionExists(id))
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

        // POST: api/Divisions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("division")]
        public async Task<ActionResult<Division>> PostDivision(Division division)
        {
            _context.Division.Add(division);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDivision", new { id = division.Id }, division);
        }

        // DELETE: api/Divisions/5
        [HttpDelete("division/{id}")]
        public async Task<IActionResult> DeleteDivision(int id)
        {
            var division = await _context.Division.FindAsync(id);
            if (division == null)
            {
                return NotFound();
            }

            _context.Division.Remove(division);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion


        #region

        // District*(*********)
        // GET: api/Districts
        [HttpGet("district")]
        public async Task<ActionResult<IEnumerable<District>>> GetDistrict()
        {
            return await _context.District.Include(d => d.Division).ToListAsync();
        }

        // GET: api/Districts/5
        [HttpGet("district/{id}")]
        public async Task<ActionResult<District>> GetDistrict(int id)
        {
            var district = await _context.District.Include(d => d.Division).FirstOrDefaultAsync(d => d.Id == id);

            if (district == null)
            {
                return NotFound();
            }

            return district;
        }

        // PUT: api/Districts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("district/{id}")]
        public async Task<IActionResult> PutDistrict(int id, District district)
        {
            if (id != district.Id)
            {
                return BadRequest();
            }

            _context.Entry(district).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DistrictExists(id))
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
        [HttpGet("divisionwisedistrict/{id}")]
        public async Task<ActionResult<IEnumerable<District>>> GetDistrictByDivision(int id)
        {
            return await _context.District.Where(i => i.DivisionId == id).Include(d => d.Division).ToListAsync();
        }
        // POST: api/Districts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("district")]
        public async Task<ActionResult<District>> PostDistrict(District district)
        {
            _context.District.Add(district);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDistrict", new { id = district.Id }, district);
        }

        #endregion

        #region
        //Upazila Section
        // GET: api/Upazila
        [HttpGet("upazila")]
        public async Task<ActionResult<IEnumerable<Upazila>>> GetUpazila()
        {
            return await _context.Upazila.Include(d => d.District).ToListAsync();
        }

        // GET: api/UpazilaAndDistrict/5
        [HttpGet("upazila/{id}")]
        public async Task<ActionResult<Upazila>> GetUpazila(int id)
        {
            var upazila = await _context.Upazila.Include(d=> d.District).FirstOrDefaultAsync(i => i.Id == id);

            if (upazila == null)
            {
                return NotFound();
            }

            return upazila;
        }

        // PUT: api/UpazilaAndDistrict/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("upazila/{id}")]
        public async Task<IActionResult> PutUpazila(int id, Upazila upazila)
        {
            if (id != upazila.Id)
            {
                return BadRequest();
            }

            _context.Entry(upazila).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UpazilaExists(id))
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

        // POST: api/UpazilaAndDistrict
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("upazila")]
        public async Task<ActionResult<Upazila>> PostUpazila(Upazila upazila)
        {
            _context.Upazila.Add(upazila);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUpazila", new { id = upazila.Id }, upazila);
        }

        [HttpGet("districtwiseupazila/{id}")]
        public async Task<ActionResult<IEnumerable<Upazila>>> GetUpazilaByDistrict(int id)
        {
            return await _context.Upazila.Where(i => i.DistrictId == id).Include(d => d.District).ToListAsync();
        }

        // DELETE: api/UpazilaAndDistrict/5
        [HttpDelete("upazila/{id}")]
        public async Task<IActionResult> DeleteUpazila(int id)
        {
            var upazila = await _context.Upazila.FindAsync(id);
            if (upazila == null)
            {
                return NotFound();
            }

            _context.Upazila.Remove(upazila);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion
        private bool DivisionExists(int id)
        {
            return _context.Division.Any(e => e.Id == id);
        }
        private bool UpazilaExists(int id)
        {
            return _context.Upazila.Any(e => e.Id == id);
        }
        private bool DistrictExists(int id)
        {
            return _context.District.Any(e => e.Id == id);
        }
    }
}
