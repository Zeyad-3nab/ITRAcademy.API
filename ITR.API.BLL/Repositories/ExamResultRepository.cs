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
    public class ExamResultRepository : IExamResultsRepository
    {
        private readonly ApplicationDbContext _context;

        public ExamResultRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<ExamResults>> GetAllExamResultsToUserAsync(string userId)
        {
            return await _context.ExamResults.Include(er=>er.User).Include(er=>er.Exam).Where(er => er.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<ExamResults>> GetAllResultsForExam(int ExamId)
        {
            return await _context.ExamResults.Include(er => er.User).Include(er => er.Exam).Where(er => er.ExamId == ExamId).ToListAsync();
        }

        public async Task<ExamResults> GetExamResultForUserInExam(int ExamId, string userId)
        {
            return await _context.ExamResults.Include(er => er.User).Include(er => er.Exam).FirstOrDefaultAsync(er => er.UserId == userId && er.ExamId == ExamId);
        }


        public async Task<int> AddAsync(ExamResults Entity)
        {
            await _context.ExamResults.AddAsync(Entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(ExamResults Entity)
        {
            _context.ExamResults.Update(Entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(ExamResults Entity)
        {
            _context.ExamResults.Remove(Entity);
            return await _context.SaveChangesAsync();
        }

    }
}
