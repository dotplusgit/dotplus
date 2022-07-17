using HospitalAPI.Core.Dtos;
using HospitalAPI.Core.Models;
using HospitalAPI.Core.Models.ServiceModel;
using HospitalAPI.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace HospitalAPI.DataAccess.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ResponseObject> ForgotPassword(string email)
        {
            ResponseObject responseObject = new();

            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    responseObject.Message = "Failed";
                    responseObject.IsValid = false;
                    responseObject.Token = null;
                    return responseObject;
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encodedToken = Encoding.UTF8.GetBytes(token);
                var validToken = WebEncoders.Base64UrlEncode(encodedToken);
                responseObject.Message = "Success";
                responseObject.IsValid = true;
                responseObject.Token = validToken;
                return responseObject;

            }
            catch (Exception ex)
            {
                Log.Error("An error occurred at ForgotPassword {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                responseObject.Message = "Error";
                responseObject.IsValid = false;
                responseObject.Data = null;
                return responseObject;
            }
        }
        public async Task<ResponseObject> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            ResponseObject responseObject = new();
            try
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
                if (user == null)
                {
                    responseObject.Message = "Failed";
                    responseObject.IsValid = false;
                    responseObject.Data = null;
                    return responseObject;
                }
                var decodedToken = WebEncoders.Base64UrlDecode(resetPasswordDto.Token);
                string normalToken = Encoding.UTF8.GetString(decodedToken);
                var result = await _userManager.ResetPasswordAsync(user, normalToken, resetPasswordDto.Password);
                if (result.Succeeded)
                {
                    responseObject.Message = "Success";
                    responseObject.IsValid = true;
                    responseObject.Data = null;
                    return responseObject;
                }
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred at ForgotPassword {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            responseObject.Message = "Failed";
            responseObject.IsValid = false;
            responseObject.Data = null;
            return responseObject;
        }
    }
}
