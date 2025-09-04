using ITR.API.BLL.Data.Contexts;
using ITR.API.DAL.Models;
using ITR.API.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.BLL.Repositories
{
    public class ExamRepository:IExamRepository
    {
        private readonly ApplicationDbContext _context;

        public ExamRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(Exam Entity)
        {
            await _context.Exams.AddAsync(Entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Exam Entity)
        {
            _context.Exams.Remove(Entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Exam>> GetAllAsync()
        {
            return await _context.Exams.Include(E=>E.Course).ToListAsync();
        }

        public async Task<Exam> GetByIdAsync(int id)
        {
            return await _context.Exams.Include(E => E.Course).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Exam>> SearchByNameAsync(string Name)
        {
            return await _context.Exams.Include(E => E.Course).Where(c => c.Title.ToUpper().Contains(Name.ToUpper())).ToListAsync();
        }

        public async Task<int> UpdateAsync(Exam Entity)
        {
            _context.Exams.Update(Entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Exam>> GetAllExamsForUserAsync(string userId)
        {
            var exams = await _context.UserCourse
                .Where(uc => uc.UserId == userId)
                .SelectMany(uc => uc.Course.Exams) // يجيب الامتحانات الخاصة بالكورسات
                .Union(_context.Exams.Where(e => e.ForAll)) // يضيف الامتحانات العامة
                .Where(e => e.State == Activation.Active && e.StartTime <= DateTime.Now && e.EndTime >= DateTime.Now)
                .Include(e=>e.Course)
                .ToListAsync();

            return exams;
        }
    }
}