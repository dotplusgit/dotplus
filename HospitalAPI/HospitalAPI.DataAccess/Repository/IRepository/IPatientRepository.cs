
using HospitalAPI.Core.Models.PatientModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository.IRepository
{
    public interface IPatientRepository
    {
        IQueryable<Patient> GetPatientList(string search, string sort);
        Task<IReadOnlyList<Patient>> GetPatientForSearchList(string search);
        Task<Patient> GetPatientByPatientId(int id);
        Task<Patient> GetPatientByPatientIdForSearch(int id);
        Task<IReadOnlyList<Patient>> GetPatientWithLatestPhysicalStatList();
        Task<Patient> GetPatientWithLatestVitalByPatientId(int id);
        string UserNameByEmail(string email);
        
    }
}
