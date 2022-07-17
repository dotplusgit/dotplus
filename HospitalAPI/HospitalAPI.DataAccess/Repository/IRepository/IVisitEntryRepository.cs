using HospitalAPI.Core.Models.VisitEntryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.DataAccess.Repository.IRepository
{
    public interface IVisitEntryRepository
    {
        IQueryable<VisitEntry> GetVisitEntryList(string search);
        Task<IReadOnlyList<VisitEntry>> GetTodaysActiveVisitEntryList();
        Task<VisitEntry> GetVisitEntryById(int id);
        Task<IReadOnlyList<VisitEntry>> GetVisitEntryByStatus(string status);
    }
}
