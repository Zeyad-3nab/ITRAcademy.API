using ITR.API.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.DAL.Repositories
{
    public interface ICourseRepository:IGenaricRepository<Course>
    {
        Task<IEnumerable<Course>> GetAllToLandingPageAsync();
        Task<IEnumerable<Course>> GetAllFreeAsync();
        Task<IEnumerable<Course>> GetAllPaidAsync();
        Task<int> GetCourseCount();
    }
}
