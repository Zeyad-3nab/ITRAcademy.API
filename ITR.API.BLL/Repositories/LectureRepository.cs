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
    internal class LectureRepository : ILectureRepository
    {
        private readonly ApplicationDbContext _context;

        public LectureRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(Lecture Entity)
        {
            await _context.Lectures.AddAsync(Entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Lecture Entity)
        {
            _context.Lectures.Remove(Entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Lecture>> GetAllAsync()
        {
            return await _context.Lectures.Include(E => E.Course).Where(l=>l.IsReady == true).ToListAsync();
        }

        public async Task<Lecture> GetByIdAsync(int id)
        {
            return await _context.Lectures.Include(E => E.Course).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Lecture> GetByIdToUserAsync(int id)
        {
            return await _context.Lectures.Include(E => E.Course).Where(e=>e.IsReady == true).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Lecture> GetLectureByUuid(string Uuid)
        {
            return await _context.Lectures.FirstOrDefaultAsync(l => l.Uuid == Uuid);
        }

        public async Task<IEnumerable<Lecture>> GetLectureForCourse(int CourseId)
        {
            return await _context.Lectures.Include(E => E.Course).Where(l => l.CourseId == CourseId &&l.IsReady == true).ToListAsync();
        }

        public async Task<IEnumerable<Lecture>> SearchByNameAsync(string Name)
        {
            return await _context.Lectures.Include(E => E.Course).Where(c => c.Name.ToUpper().Contains(Name.ToUpper())).ToListAsync();
        }

        public async Task<int> UpdateAsync(Lecture Entity)
        {
            _context.Lectures.Update(Entity);
            return await _context.SaveChangesAsync();
        }
    }
}
