using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalAPI.Core.Models.PatientModel;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.Core.Dtos.PregnancyDto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using HospitalAPI.Core.Models;
using HospitalAPI.Extensions;
using HospitalAPI.Core.Models.ServiceModel;
using HospitalAPI.Helpers;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PregnancyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public PregnancyController(ApplicationDbContext context,
                                   UserManager<ApplicationUser> userManager,
                                   IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        // GET: api/Pregnancy
        [HttpGet]
        public async Task<ActionResult<Pagination<GetPregnancyDto>>> GetPregnancy([FromQuery] Paramps paramps, int? hospitalId)
        {
            if (paramps.PageSize > 100)
            {
                paramps.PageSize = 100;
            }

            IQueryable<Pregnancy> listOfPregnancy =  _context.Pregnancy;
            if(hospitalId != null)
            {
               listOfPregnancy = listOfPregnancy.Where(p => p.HospitalId == hospitalId);
            }
            listOfPregnancy = listOfPregnancy.OrderByDescending(p => p.NextCheckup).Include(p => p.Patient);
            var paginateddata = await PaginatedList<Pregnancy>.CreateAsync(listOfPregnancy, paramps.PageNumber ?? 1, paramps.PageSize ?? 20);
            var mappedPregnancy = _mapper.Map<PaginatedList<Pregnancy>, PaginatedList<GetPregnancyDto>>(paginateddata);
            var result = new Pagination<GetPregnancyDto>(paramps.PageNumber ?? 1, paramps.PageSize ?? 20, await listOfPregnancy.CountAsync(), mappedPregnancy);
            return Ok(result);
        }

        // GET: api/Pregnancy/5
        [HttpGet("pregnatewomanprofile/{id}")]
        public async Task<ActionResult<PregnantWomanProfile>> GetPregnantWomanProfile(int id)
        {
            int first = 0, second = 0, third = 0, fourth = 0, fifth = 0, sixth = 0, seventh = 0, eightth = 0, nineth = 0, tenth = 0;
            DateTime? firstdate = null, seconddate = null, thirddate = null, fourthdate = null, fifthdate = null, sixthdate = null, seventhdate = null, eightthdate = null, ninethdate = null, tenthdate = null;
            var pregnancy = await _context.Pregnancy.Include(p => p.Patient).Include(p=> p.MonthlyCheckupPregnancy).FirstOrDefaultAsync(p => p.Id == id);
            var today = DateTime.Now;
            if (pregnancy == null)
            {
                return NotFound();
            }
            
            foreach (MonthlyCheckupPregnancy monthlyCheckup in pregnancy.MonthlyCheckupPregnancy)
            {
                var days = (monthlyCheckup.CheckupDate - pregnancy.FirstDateOfLastPeriod).Days;
                
                if(days < 30)
                {
                    first = 1;
                    firstdate = monthlyCheckup.CheckupDate;
                }
                if (days > 30 && days <= 60)
                {
                    second = 1;
                    seconddate = monthlyCheckup.CheckupDate;
                }
                if (days > 60 && days <= 90)
                {
                    third = 1;
                    thirddate = monthlyCheckup.CheckupDate;
                }
                if (days > 90 && days <= 120)
                {
                    fourth = 1;
                    fourthdate = monthlyCheckup.CheckupDate;
                }
                if (days > 120 && days <= 150)
                {
                    fifth = 1;
                    fifthdate = monthlyCheckup.CheckupDate;
                }
                if (days > 150 && days <= 180)
                {
                    sixth = 1;
                    sixthdate = monthlyCheckup.CheckupDate;
                }
                if (days > 180 && days <= 210)
                {
                    seventh = 1;
                    seventhdate = monthlyCheckup.CheckupDate;
                }
                if (days > 210 && days <= 240)
                {
                    eightth = 1;
                    eightthdate = monthlyCheckup.CheckupDate;
                }
                if (days > 240 && days <= 270)
                {
                    nineth = 1;
                    ninethdate = monthlyCheckup.CheckupDate;
                }
                if (days > 270 && days <= 300)
                {
                    tenth = 1;
                    tenthdate = monthlyCheckup.CheckupDate;
                }

            }
            PregnantWomanProfile pregnantWomanProfile = new(pregnancy.Id, 
                                                            pregnancy.Patient.FirstName + " " + pregnancy.Patient.LastName, 
                                                            pregnancy.FirstDateOfLastPeriod, 
                                                            pregnancy.ExpectedDateOfDelivery, 
                                                            first, firstdate, 
                                                            second, seconddate,
                                                            third, thirddate, 
                                                            fourth, fourthdate, 
                                                            fifth, fifthdate, 
                                                            sixth, sixthdate, 
                                                            seventh, seventhdate, 
                                                            eightth, eightthdate, 
                                                            nineth, ninethdate, 
                                                            tenth, tenthdate );

            return pregnantWomanProfile;
        }

        // GET: api/Pregnancy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pregnancy>> GetPregnancy(int id)
        {
            var pregnancy = await _context.Pregnancy.FindAsync(id);

            if (pregnancy == null)
            {
                return NotFound();
            }

            return pregnancy;
        }

        // PUT: api/Pregnancy/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPregnancy(EditPregnancyDto editPregnancyDto)
        {
            if (!PregnancyExists(editPregnancyDto.Id))
            {
                return NotFound();
            }
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var pregnancy = await _context.Pregnancy.FirstOrDefaultAsync(p => p.Id == editPregnancyDto.Id);
            pregnancy.PatientId = editPregnancyDto.PatientId;
            pregnancy.HospitalId = editPregnancyDto.HospitalId;
            pregnancy.FirstDateOfLastPeriod = editPregnancyDto.FirstDateOfLastPeriod;
            pregnancy.ExpectedDateOfDelivery = editPregnancyDto.ExpectedDateOfDelivery;

            _context.Entry(pregnancy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
            }

            return NoContent();
        }

        // POST: api/Pregnancy
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pregnancy>> PostPregnancy(AddPregnancyDto addPregnancyDto)
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (addPregnancyDto.PatientId < 1)
            {
                return NotFound();
            }
            var patient = _context.Patient.Any(p => p.Id == addPregnancyDto.PatientId);
            if (!patient)
            {
                return NotFound();
            }

            Pregnancy pregnancy = new(addPregnancyDto.PatientId, 
                                      addPregnancyDto.FirstDateOfLastPeriod, 
                                      addPregnancyDto.ExpectedDateOfDelivery, 
                                      addPregnancyDto.HospitalId,
                                      DateTime.Now,currentuser.Email, 
                                      DateTime.Now, currentuser.Email,
                                      nextCheckp: addPregnancyDto.NextCheckup);
            _context.Pregnancy.Add(pregnancy);
            var save = await _context.SaveChangesAsync();
            if(save == 1)
            {
                MonthlyCheckupPregnancy monthlyCheckupPregnancy = new(pregnancy.Id, true, DateTime.Now, DateTime.Now, currentuser.Email);
                _context.MonthlyCheckupPregnancy.Add(monthlyCheckupPregnancy);
                await _context.SaveChangesAsync();
            }
            return Ok(new ResponseObject { Message = "success" });
        }

        [HttpPost("monthlycheckup")]
        public async Task<ActionResult<Pregnancy>> PostPregnancyMonthlyCheckup(AddMonthlyCheckup addMonthlyCheckup)
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            
                MonthlyCheckupPregnancy monthlyCheckupPregnancy = new(addMonthlyCheckup.PregnancyId, 
                                                                      true, 
                                                                      checkupDate: addMonthlyCheckup.CheckupDate, 
                                                                      DateTime.Now, 
                                                                      currentuser.Email);
                _context.MonthlyCheckupPregnancy.Add(monthlyCheckupPregnancy);
                await _context.SaveChangesAsync();
            return Ok(new ResponseObject { Message = "success" });
        }

        // DELETE: api/Pregnancy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePregnancy(int id)
        {
            var pregnancy = await _context.Pregnancy.FindAsync(id);
            if (pregnancy == null)
            {
                return NotFound();
            }

            _context.Pregnancy.Remove(pregnancy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PregnancyExists(int id)
        {
            return _context.Pregnancy.Any(e => e.Id == id);
        }
    }
}
