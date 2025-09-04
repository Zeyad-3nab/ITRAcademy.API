using ITR.API.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.DAL.Repositories
{
    public interface IQuestionRepository:IGenaricRepository<Question>
    {
        Task<IEnumerable<Question>> GetAllExamQuestionsAsync(int examId);
    }
}