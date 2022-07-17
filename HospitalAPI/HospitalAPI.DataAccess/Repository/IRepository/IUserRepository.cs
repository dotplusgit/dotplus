using HospitalAPI.Core.Dtos.UserBasedDto;
using HospitalAPI.Core.Models;
using HospitalAPI.Core.Models.ServiceModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<IReadOnlyList<ApplicationUser>> UserListAsync();
        Task<IReadOnlyList<ApplicationUser>> DoctorListAsync();
        Task<IReadOnlyList<ApplicationRole>> RoleListAsync();
        Task<ApplicationUser> GetUser(string id);
    }
}
