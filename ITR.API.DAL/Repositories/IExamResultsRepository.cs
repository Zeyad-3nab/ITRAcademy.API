using ITR.API.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.DAL.Repositories
{
    public interface IExamResultsRepository
    {
        Task<IEnumerable<ExamResults>> GetAllExamResultsToUserAsync(string userId);
        Task<IEnumerable<ExamResults>> GetAllResultsForExam(int ExamId);
        Task<ExamResults> GetExamResultForUserInExam(int ExamId, string userId);
        Task<int> AddAsync(ExamResults Entity);
        Task<int> UpdateAsync(ExamResults Entity);
        Task<int> DeleteAsync(ExamResults Entity);
    }
}
