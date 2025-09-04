using ITR.API.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.DAL.Repositories
{
    public interface ILectureRepository:IGenaricRepository<Lecture>
    {
        Task<Lecture> GetLectureByUuid(string Uuid);

        Task<IEnumerable<Lecture>> GetLectureForCourse(int CourseId);
        Task<Lecture> GetByIdToUserAsync(int id);
        //Task<Lecture> GetLectureByUuid(string Uuid);
    }
}
