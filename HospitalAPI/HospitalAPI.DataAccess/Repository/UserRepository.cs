using HospitalAPI.Core.Dtos.UserBasedDto;
using HospitalAPI.Core.Models;
using HospitalAPI.Core.Models.ServiceModel;
using HospitalAPI.DataAccess.Data;
using HospitalAPI.DataAccess.Repository.IRepository;
using HospitalAPI.DataAccess.StaticData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<ApplicationUser> GetUser(string id)
        {
            var user = _userManager.Users.Include(h => h.Hospital).FirstOrDefaultAsync(u => u.Id == id);
            return await user;
        }
        public async Task<IReadOnlyList<ApplicationUser>> DoctorListAsync()
        {
            var users = _userManager.Users.Where(u => u.Designation.ToLower() == "doctor").OrderBy(u => u.FirstName).Include(h => h.Hospital);
            return await users.ToListAsync();
        }

        public async Task<IReadOnlyList<ApplicationUser>> UserListAsync()
        {
            var users = _userManager.Users.OrderBy(u => u.FirstName).Include(h=> h.Hospital);
            return await users.ToListAsync();
        }

        public async Task<IReadOnlyList<ApplicationRole>> RoleListAsync()
        {
            var role = _roleManager.Roles;
            return await role.ToListAsync();
        }

        
    }

}
