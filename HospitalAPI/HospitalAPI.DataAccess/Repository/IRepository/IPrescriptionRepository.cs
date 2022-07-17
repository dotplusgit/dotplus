using HospitalAPI.Core.Models.PrescriptionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository.IRepository
{
    public interface IPrescriptionRepository
    {
        IQueryable<Prescription> PrescriptionListAsync(string sort, string search);
        Task<Prescription> GetPriscriptionById(int id);
        Task<IReadOnlyList<Prescription>> GetPriscriptionForReportById(int id);
    }
}
