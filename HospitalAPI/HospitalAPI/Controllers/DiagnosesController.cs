using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalAPI.Core.Models.DiagnosisModel;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.Core.Dtos.DignosisDto;
using HospitalAPI.Core.Models.ServiceModel;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DiagnosesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Diagnoses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diagnosis>>> GetDiagnosis()
        {
            return await _context.Diagnosis.ToListAsync();
        }

        // GET: api/Diagnoses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Diagnosis>> GetDiagnosis(int id)
        {
            var diagnosis = await _context.Diagnosis.FindAsync(id);

            if (diagnosis == null)
            {
                return NotFound();
            }

            return diagnosis;
        }
        [HttpGet("diseasescategory")]
        public async Task<ActionResult<IEnumerable<DiseasesCategory>>> GetDiseasesCategory()
        {
            return await _context.DiseasesCategory.ToListAsync();
        }
        [HttpGet("diseases")]
        public async Task<ActionResult<IEnumerable<Diseases>>> GetDiseases()
        {
            return await _context.Diseases.ToListAsync();
        }
        [HttpGet("diseasesbycategoryid/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Diseases>>> GetDiseasesByCategoryId(int categoryId)
        {
            return await _context.Diseases.Where(d => d.DiseasesCategoryId == categoryId).ToListAsync();
        }

        // PUT: api/Diagnoses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiagnosis(int id, Diagnosis diagnosis)
        {
            if (id != diagnosis.Id)
            {
                return BadRequest();
            }

            _context.Entry(diagnosis).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiagnosisExists(id))
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

        // POST: api/Diagnoses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Diagnosis>> PostDiagnosis(Diagnosis diagnosis)
        {
            _context.Diagnosis.Add(diagnosis);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiagnosis", new { id = diagnosis.Id }, diagnosis);
        }
        [HttpPost("adddiseases")]
        public async Task<ActionResult<Diagnosis>> PostDiseases(AddDiseasesDto addDiseasesDto)
        {
            try
            {
                var diseaseses = await _context.Diseases.Include(d => d.DiseasesCategory).FirstOrDefaultAsync(d => d.Name.ToLower() == addDiseasesDto.Name.ToLower());
                if (diseaseses == null)
                {
                    Diseases diseases = new Diseases()
                    {
                        DiseasesCategoryId = addDiseasesDto.DiseasesCategoryId,
                        IsActive = true,
                        Name = addDiseasesDto.Name
                    };
                    _context.Diseases.Add(diseases);
                    await _context.SaveChangesAsync();
                    return Ok(new ResponseObject { IsValid = true, Message = "success" });
                }
                else
                {
                    return Ok(new ResponseObject { IsValid = true, Message = "failed", Data = diseaseses.DiseasesCategory != null ? diseaseses.DiseasesCategory.Name : "others" });
                }
            }
            catch
            {
                return Ok(new ResponseObject { IsValid = true, Message = "failed", Data = "Error To Create" });
            }

        }
        // DELETE: api/Diagnoses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiagnosis(int id)
        {
            var diagnosis = await _context.Diagnosis.FindAsync(id);
            if (diagnosis == null)
            {
                return NotFound();
            }

            _context.Diagnosis.Remove(diagnosis);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiagnosisExists(int id)
        {
            return _context.Diagnosis.Any(e => e.Id == id);
        }
    }
}
