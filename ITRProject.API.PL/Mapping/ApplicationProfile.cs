using AutoMapper;
using ITR.API.DAL.Models;
using ITRProject.API.PL.Dtos.Course;
using ITRProject.API.PL.Dtos.Exam;
using ITRProject.API.PL.Dtos.ExamResult;
using ITRProject.API.PL.Dtos.Lecture;
using ITRProject.API.PL.Dtos.Question;
using ITRProject.API.PL.Dtos.User;
using ITRProject.API.PL.Dtos.UserCourse;

namespace ITRProject.API.PL.Mapping
{
    public class ApplicationProfile:Profile
    {
        public ApplicationProfile(IConfiguration configuration)
        {
            CreateMap<ApplicationUser, RegisterDto>().ReverseMap();



            CreateMap<AddCourseDto, Course>().ReverseMap();
            CreateMap<UpdateCourseDto, Course>().ReverseMap();
            CreateMap<Course, ReturnCourseDto>();



            CreateMap<AddImageQuestionDto, Question>();
            CreateMap<AddTextQuestionDto, Question>();
            CreateMap<Question, ReturnQuestionDto>().ReverseMap();



            CreateMap<AddUserCourseDto, UserCourse>();
            CreateMap<UpdateCourseDto, UserCourse>();
            CreateMap<UserCourse, ReturnUserCourseDto>()
               .ForMember(src => src.UserName, opt => opt.MapFrom(dest => dest.User.UserName))
               .ForMember(src => src.PhoneNumber, opt => opt.MapFrom(dest => dest.User.PhoneNumber))
               .ForMember(src => src.CourseName, opt => opt.MapFrom(dest => dest.Course.Name))
               .ForMember(src => src.CourseDescription, opt => opt.MapFrom(dest => dest.Course.Description))
               .ForMember(src => src.CourseDescription, opt => opt.MapFrom(dest => dest.Course.Description))
               .ForMember(src => src.CourseType, opt => opt.MapFrom(dest => dest.Course.Type))
               .ForMember(src => src.CourseState, opt => opt.MapFrom(dest => dest.Course.State))
               .ForMember(src => src.CoursePrice, opt => opt.MapFrom(dest => dest.Course.Price))
               .ForMember(src => src.CourseImageUrl, opt => opt.MapFrom(dest => dest.Course.ImageUrl));



            CreateMap<AddLectureDto, Lecture>();
            CreateMap<UpdateLectureDto, Lecture>();
            CreateMap<Lecture, ReturnLectureDto>()
            .ForMember(dest => dest.Qualities, opt => opt.Ignore())
                .ForMember(src => src.QualitiesAllowed, opt => opt.MapFrom(dest => dest.Qualities));

            CreateMap<Lecture, ReturnLecturesDto>()
                .ForMember(src => src.CourseName, opt => opt.MapFrom(dest => dest.Course.Name));




            CreateMap<AddExamResultDto, ExamResults>();
            CreateMap<UpdateExamResultDto, ExamResults>();
            CreateMap<ExamResults, ReturnExamResultDto>()
                .ForMember(src => src.UserName, opt => opt.MapFrom(dest => dest.User.UserName))
               .ForMember(src => src.PhoneNumber, opt => opt.MapFrom(dest => dest.User.PhoneNumber))
               .ForMember(src => src.UserCode, opt => opt.MapFrom(dest => dest.User.Code))
               .ForMember(src => src.ExamTitle, opt => opt.MapFrom(dest => dest.Exam.Title));




            CreateMap<ExamDto, Exam>().ReverseMap();
            CreateMap<Exam, ReturnExamDto>()
                .ForMember(src => src.CourseName, opt => opt.MapFrom(dest => dest.Course.Name));

        }
    }
}
