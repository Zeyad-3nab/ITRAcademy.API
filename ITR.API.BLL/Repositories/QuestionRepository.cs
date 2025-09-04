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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(Question Entity)
        {
            await _context.Questions.AddAsync(Entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Question Entity)
        {
            _context.Questions.Remove(Entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Question>> GetAllAsync()
        {
            return await _context.Questions.Include(q => q.Choices).ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetAllExamQuestionsAsync(int examId)
        {
            return await _context.Questions.Include(q => q.Choices).Where(q => q.ExamId == examId).ToListAsync();
        }

        public async Task<Question> GetByIdAsync(int id)
        {
            return await _context.Questions.Include(q=>q.Choices).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Question>> SearchByNameAsync(string Name)
        {
            return await _context.Questions.Include(q => q.Choices).Where(c => c.Content.ToUpper().Contains(Name.ToUpper())).ToListAsync();
        }

        public async Task<int> UpdateAsync(Question Entity)
        {
            _context.Questions.Update(Entity);
            return await _context.SaveChangesAsync();
        }
    }
}
