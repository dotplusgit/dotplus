using AutoMapper;
using HospitalAPI.Core.Dtos;
using HospitalAPI.Core.Dtos.HospitalDto;
using HospitalAPI.Core.Models;
using HospitalAPI.Core.Models.ServiceModel;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.DataAccess.Repository.IRepository;
using HospitalAPI.Errors;
using HospitalAPI.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IHospitalRepository _hospitalRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public HospitalController(ApplicationDbContext context,
                                  IHospitalRepository hospitalRepo,
                                  IMapper mapper,
                                  UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _hospitalRepo = hospitalRepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<HospitalGetDto>>> GetHospitalList() 
        {
            var hospitals =await _hospitalRepo.HospitalListAsync();
            return Ok(_mapper.Map<IReadOnlyList<Hospital>, IReadOnlyList<HospitalGetDto>>(hospitals));

        }
        [HttpGet("hospitallistsortbyname")]
        public async Task<ActionResult<IReadOnlyList<HospitalGetDtoShortByName>>> GetHospitalListSortByName()
        {
            var hospitals = await _hospitalRepo.HospitalListShortByNameAsync();
            return Ok(_mapper.Map<IReadOnlyList<Hospital>, IReadOnlyList<HospitalGetDtoShortByName>>(hospitals));

        }
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> GetHospital(int id)
        {
            var hospital = await _hospitalRepo.GetHospitalById(id);

            if (hospital == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper.Map<HospitalGetDto>(hospital));
        }
        [HttpPost]
        public async Task<ActionResult> PostHospital(HospitalAddDto addHospital)
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (ModelState.IsValid)
            {
                var hospital = new Hospital
                {
                    Name = addHospital.Name,
                    Address = addHospital.Address,
                    BranchId = addHospital.BranchId,
                    DivisionId = addHospital.DivisionId,
                    DistrictId = addHospital.DistrictId,
                    UpazilaId = addHospital.UpazilaId,
                    IsActive = addHospital.IsActive,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now,
                    CreatedBy = currentuser.Email,
                    UpdatedBy = currentuser.Email
                };
                context.Hospital.Add(hospital);
                await context.SaveChangesAsync();
                return Ok(new ResponseObject{ Message = "success"});
            }
            else { return BadRequest(new ApiResponse(400)); }
        }
        [HttpPut]
        public async Task<ActionResult> EditHospital(HospitalGetDto editHospital)
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            try
            {
                var hospital = await context.Hospital.FirstOrDefaultAsync(h => h.Id == editHospital.Id);
                hospital.Name = editHospital.Name;
                hospital.BranchId = editHospital.BranchId;
                hospital.DivisionId = editHospital.DivisionId;
                hospital.DistrictId = editHospital.DistrictId;
                hospital.UpazilaId = editHospital.UpazilaId;
                hospital.Address = editHospital.Address;
                hospital.IsActive = editHospital.IsActive;
                hospital.UpdatedBy = currentuser.Email;
                hospital.UpdatedOn = DateTime.Now;
                context.Hospital.Update(hospital);
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return Ok(new ResponseObject{ Message = "success" });
        }

    }
}
