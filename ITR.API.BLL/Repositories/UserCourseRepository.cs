using ITR.API.BLL.Data.Contexts;
using ITR.API.DAL.Models;
using ITR.API.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.BLL.Repositories
{
    public class UserCourseRepository : IUserCourseRepository
    {
        private readonly ApplicationDbContext _context;

        public UserCourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(UserCourse Entity)
        {
            await _context.UserCourse.AddAsync(Entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(UserCourse Entity)
        {
             _context.UserCourse.Remove(Entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserCourse>> GetAllAsync()
        {
            return await _context.UserCourse.Where(uc=>uc.State == UserCourseState.Pending)
                                                       .Include(uc => uc.User)
                                                       .Include(uc => uc.Course)
                                                       .ToListAsync();
        }

        public async Task<IEnumerable<UserCourse>> GetAllCoursesToUserAsync(string userId)
        {
            return await _context.UserCourse.Include(uc=>uc.User).Include(uc=>uc.Course).Where(uc => uc.UserId == userId && uc.State == UserCourseState.Accepted).ToListAsync();
        }

        public async Task<IEnumerable<UserCourse>> GetAllFreeCoursesToUserAsync(string userId)
        {
            return await _context.UserCourse.Include(uc => uc.User)
                                                        .Include(uc => uc.Course)
                                                        .Where(uc => uc.UserId == userId && uc.State == UserCourseState.Accepted && uc.Course.Type == CourseType.Free)
                                                        .ToListAsync();
        }

        public async Task<IEnumerable<UserCourse>> GetAllPaidCoursesToUserAsync(string userId)
        {
            return await _context.UserCourse.Include(uc => uc.User)
                                            .Include(uc => uc.Course)
                                            .Where(uc => uc.UserId == userId && uc.State == UserCourseState.Accepted && uc.Course.Type == CourseType.Paid)
                                            .ToListAsync();
        }

        public async Task<int> GetCountOfRejectedUserCourseAsync()
        {
           return await _context.UserCourse.Where(uc => uc.State == UserCourseState.Rejected).CountAsync();
        }

        public async Task<int> GetCountOfAcceptedUserCourseAsync()
        {
            return await _context.UserCourse.Where(uc => uc.State == UserCourseState.Accepted).CountAsync();
        }


        public async Task<UserCourse> GetUserCourseById(int CourseId, string userId)
        {
            return await _context.UserCourse.Include(uc => uc.User).Include(uc => uc.Course).FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CourseId == CourseId);
        }

        public async Task<IEnumerable<UserCourse>> GetUsersInCourseAsync(int CourseId)
        {
            return await _context.UserCourse.Include(uc => uc.User).Include(uc => uc.Course).Where(uc => uc.CourseId == CourseId && uc.State ==UserCourseState.Accepted).ToListAsync();
        }

        public async Task<int> UpdateAsync(UserCourse Entity)
        {
            _context.UserCourse.Update(Entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> NumOfFreeCourse(string userId)
        {
            return await _context.UserCourse.Where(uc => uc.UserId == userId && uc.State == UserCourseState.Accepted && uc.Course.Type == CourseType.Free).CountAsync();
        }

        public async Task<int> NumOfPaidCourse(string userId)
        {
            return await _context.UserCourse.Where(uc => uc.UserId == userId && uc.State == UserCourseState.Accepted && uc.Course.Type == CourseType.Paid).CountAsync();
        }
    }
}
