using ITR.API.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.DAL.Repositories
{
    public interface IUserCourseRepository
    {
        Task<IEnumerable<UserCourse>> GetAllAsync();
        Task<int> GetCountOfRejectedUserCourseAsync();
        Task<int> GetCountOfAcceptedUserCourseAsync();


        Task<int> NumOfFreeCourse(string userId);
        Task<int> NumOfPaidCourse(string userId);


        Task<IEnumerable<UserCourse>> GetAllCoursesToUserAsync(string userId);
        Task<IEnumerable<UserCourse>> GetAllFreeCoursesToUserAsync(string userId);
        Task<IEnumerable<UserCourse>> GetAllPaidCoursesToUserAsync(string userId);
        Task<IEnumerable<UserCourse>> GetUsersInCourseAsync(int CourseId);
        Task<UserCourse> GetUserCourseById(int CourseId, string userId);
        Task<int> AddAsync(UserCourse Entity);
        Task<int> UpdateAsync(UserCourse Entity);
        Task<int> DeleteAsync(UserCourse Entity);
    }
}
