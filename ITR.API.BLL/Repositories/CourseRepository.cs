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
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(Course Entity)
        {
            await _context.Courses.AddAsync(Entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Course Entity)
        {
             _context.Courses.Remove(Entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllToLandingPageAsync()
        {
            return await _context.Courses.Where(e=>e.State == Activation.Active).ToListAsync();
        }


        public async Task<IEnumerable<Course>> GetAllFreeAsync()
        {
            return await _context.Courses.Where(c => c.Type == CourseType.Free).ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllPaidAsync()
        {
            return await _context.Courses.Where(c=>c.Type == CourseType.Paid).ToListAsync();
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<int> GetCourseCount()
        {
            return await _context.Courses.CountAsync();
        }

        public async Task<IEnumerable<Course>> SearchByNameAsync(string Name)
        {
            return await _context.Courses.Where(c => c.Name.ToUpper().Contains(Name.ToUpper())).ToListAsync();
        }

        public async Task<int> UpdateAsync(Course Entity)
        {
            _context.Courses.Update(Entity);
            return await _context.SaveChangesAsync();
        }
    }
}
