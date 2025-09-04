using ITR.API.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.DAL.Repositories
{
    public interface IUnitOfWork
    {
        public ICourseRepository CourseRepository { get; }
        public IExamRepository ExamRepository { get; }
        public IQuestionRepository QuestionRepository { get; }
        public IUserCourseRepository UserCourseRepository { get; }
        public IExamResultsRepository ExamResultsRepository { get; }
        public ILectureRepository LectureRepository { get; }
    }
}
