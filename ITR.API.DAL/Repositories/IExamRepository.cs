using ITR.API.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.DAL.Repositories
{
    public interface IExamRepository:IGenaricRepository<Exam>
    {
        Task<IEnumerable<Exam>> GetAllExamsForUserAsync(string userId);
    }
}
