using ITR.API.BLL.Data.Contexts;
using ITR.API.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.BLL.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _Context;
        private ICourseRepository _courseRepository;
        private IQuestionRepository _questionRepository;
        private IExamRepository _examRepository;
        private IUserCourseRepository _userCourseRepository;
        private IExamResultsRepository _examResultsRepository;
        private ILectureRepository _lectureRepository;
        public UnitOfWork(ApplicationDbContext context)
        {
            _Context = context;
            _courseRepository = new CourseRepository(_Context);
            _examRepository = new ExamRepository(_Context);
            _questionRepository = new QuestionRepository(_Context);
            _examResultsRepository = new ExamResultRepository(_Context);
            _userCourseRepository = new UserCourseRepository(_Context);
            _lectureRepository = new LectureRepository(_Context);
            
        }

        public ICourseRepository CourseRepository => _courseRepository;
        public IExamRepository ExamRepository => _examRepository;
        public IQuestionRepository QuestionRepository => _questionRepository;
        public IUserCourseRepository UserCourseRepository => _userCourseRepository;
        public IExamResultsRepository ExamResultsRepository => _examResultsRepository;

        public ILectureRepository LectureRepository => _lectureRepository;
    }
}
