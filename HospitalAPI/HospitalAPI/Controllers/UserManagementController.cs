using AutoMapper;
using HospitalAPI.Core.Dtos;
using HospitalAPI.Core.Dtos.UserBasedDto;
using HospitalAPI.Core.Models;
using HospitalAPI.Core.Models.ServiceModel;
using HospitalAPI.DataAccess.Repository.IRepository;
using HospitalAPI.DataAccess.StaticData;
using HospitalAPI.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserRepository _userrepo;
        private readonly IMapper _mapper;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserManagementController(IUserRepository userrepo,
                                        IMapper mapper,
                                        RoleManager<ApplicationRole> roleManager,
                                        UserManager<ApplicationUser> userManager,
                                        IHttpContextAccessor httpContextAccessor)
        {
            _userrepo = userrepo;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        //Identity User Section
        [HttpGet("userlist")]
        public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAllUser()
        {
            var userList = await _userrepo.UserListAsync();

            return Ok(_mapper.Map<IReadOnlyList<ApplicationUser>, IReadOnlyList<UserDto>>(userList));
        }

        [HttpGet("doctorlist")]
        public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAllDoctor()
        {
            var userList = await _userrepo.DoctorListAsync();

            return Ok(_mapper.Map<IReadOnlyList<ApplicationUser>, IReadOnlyList<UserDto>>(userList));
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<UserDto>> GetUser(string id)
        {
            var user =await _userrepo.GetUser(id);
            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpPut("updateuser")]
        public async Task<ActionResult<ResponseObject>> UpdateUser(UpdateUserDto updateUser)
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var user = await _userManager.FindByIdAsync(updateUser.UserId);

            if (user != null)
            {
                user.HospitalId = updateUser.HospitalId;
                user.FirstName = updateUser.FirstName;
                user.LastName = updateUser.LastName;
                user.Email = updateUser.Email;
                user.UserName = updateUser.Email;
                user.Designation = updateUser.Designation;
                user.BMDCRegNo = updateUser.BMDCRegNo;
                user.OptionalEmail = updateUser.OptionalEmail;
                user.PhoneNumber = updateUser.PhoneNumber;
                user.JoiningDate = updateUser.JoiningDate;
                user.IsActive = updateUser.IsActive;
                user.UpdatedOn = DateTime.Now;
                user.UpdatedBy = currentuser.Email;
                user.Role = updateUser.Role;
                switch (user.IsActive)
                {
                    case false:
                        user.LockoutEnd = DateTime.Now.AddYears(1000);
                        break;
                    default:
                        user.LockoutEnd = DateTime.Now;
                        break;
                }
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) { return new ResponseObject { Message = "Error", IsValid = false }; };
            if (result.Succeeded)
            {
                switch (user.Role)
                {
                    case Role.Admin:
                        await _userManager.AddToRoleAsync(user, Role.Admin);
                        break;
                    case Role.Doctor:
                        await _userManager.AddToRoleAsync(user, Role.Doctor);
                        break;
                    case Role.Pharmacist:
                        await _userManager.AddToRoleAsync(user, Role.Pharmacist);
                        break;
                    case Role.FrontDesk:
                        await _userManager.AddToRoleAsync(user, Role.FrontDesk);
                        break;
                    default:
                        await _userManager.AddToRoleAsync(user, Role.User);
                        break;
                }
            }
            return new ResponseObject { Message = "Success", IsValid = true };
        }

        //Identity Role Section 

        [HttpPost("createrole")]
        public async Task<ActionResult> CreateRole(CreateRoleDto createRoleDto)
        {
            if(ModelState.IsValid)
            {
                ApplicationRole identityRole = new ApplicationRole
                {
                    Name = createRoleDto.RoleName,
                    IsActive = createRoleDto.IsActive
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if(result.Succeeded)
                {
                    return Ok();
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Ok();
        }
        [HttpGet("rolelist")]
        public async Task<ActionResult<IReadOnlyList<GetRoleDto>>> RoleList()
        {
            var rolelist =await _userrepo.RoleListAsync();
            
            return Ok(_mapper.Map<IReadOnlyList<ApplicationRole>, IReadOnlyList<GetRoleDto>>(rolelist));
        }
    
    }
}
