using HospitalAPI.Core.Dtos;
using HospitalAPI.Core.Models;
using HospitalAPI.Core.Models.ServiceModel;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.DataAccess.Repository.IRepository;
using HospitalAPI.DataAccess.StaticData;
using HospitalAPI.Errors;
using HospitalAPI.Extensions;
using HospitalAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;
        private readonly IAccountRepository _accountrepo;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                ITokenService tokenService,
                                 IEmailSender emailSender,
                                 IAccountRepository accountrepo,
                                 IConfiguration configuration,
                                 ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailSender = emailSender;
            _accountrepo = accountrepo;
            _configuration = configuration;
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserTokenProvederDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) { return Unauthorized(new ApiResponse(401)); }

            return new UserTokenProvederDto
            {
                UserId = user.Id,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }


        [HttpPost("registration")]
        public async Task<ActionResult<ResponseObject>> Registration(RegistrationDto registrationDto)
        {
            var currentuser = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            var user = new ApplicationUser
            {
                HospitalId = registrationDto.HospitalId,
                FirstName = registrationDto.FirstName,
                LastName = registrationDto.LastName,
                Email = registrationDto.Email,
                UserName = registrationDto.Email,
                Designation = registrationDto.Designation,
                BMDCRegNo = registrationDto.BMDCRegNo,
                OptionalEmail = registrationDto.OptionalEmail,
                PhoneNumber = registrationDto.PhoneNumber,
                JoiningDate = registrationDto.JoiningDate,
                IsActive = registrationDto.IsActive,
                CreatedBy = currentuser.Email,
                CreatedOn = DateTime.Now,
                UpdatedBy = currentuser.Email,
                UpdatedOn = DateTime.Now,
                Role = registrationDto.Role
            };

            var result = await _userManager.CreateAsync(user, registrationDto.Password);

            if(result.Succeeded)
            {
                switch(user.Role)
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
            if (!result.Succeeded) return BadRequest(new ResponseObject { Message = "Failed", IsValid = true });

            return new ResponseObject { Message = "Success"};
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserTokenProvederDto>> Login(LoginDto loginDto)
        {
            // var users = await _userManager.FindByEmailAsync(loginDto.Email);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return Unauthorized();
            }
            user.LastLoginDate = DateTime.Now;
            var lastLoginResult = await _userManager.UpdateAsync(user);
            if (!lastLoginResult.Succeeded) return BadRequest(new ResponseObject {Message = "Error to update Last Login Date" });
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            return new UserTokenProvederDto
            {
                UserId = user.Id,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        //Forgot Password
        [HttpPost("[action]/{email}")]
        public async Task<IActionResult> ForgotPassword([FromRoute] string email)
        {

            if (ModelState.IsValid)
            {
                var result = await _accountrepo.ForgotPassword(email);
                if (!result.IsValid)
                {
                    return Ok(new { Message = "Result Is Not Valid" });
                }
                var callbackUrl = $"{_configuration["AppUrl"]}/account/resetpassword?email={email}&token={result.Token}";                                                            
                await _emailSender.SendEmailAsync(
                email,
                "Reset Password",
                 "<h1>Follow the instructions to reset your password</h1>" +
                $"<p>To reset your password <a href='{callbackUrl}'>Click here</a></p>"
                );
                return Ok(new { Message = "Success" });
            }
            return BadRequest("We have Encountered an Error");
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseObject>> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            return await _accountrepo.ResetPassword(resetPasswordDto);
        }
    }


}
