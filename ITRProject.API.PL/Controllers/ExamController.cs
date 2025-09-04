using AutoMapper;
using ITR.API.DAL.Models;
using ITR.API.DAL.Repositories;
using ITRProject.API.PL.Dtos.Course;
using ITRProject.API.PL.Dtos.Exam;
using ITRProject.API.PL.Errors;
using ITRProject.API.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITRProject.API.PL.Controllers
{
    public class ExamController : BaseController
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExamController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("GetAll")]

        public async Task<ActionResult<IEnumerable<ReturnExamDto>>> GetAll()
        {
            var exams = await _unitOfWork.ExamRepository.GetAllAsync();
            var map = _mapper.Map<IEnumerable<ReturnExamDto>>(exams);
            return Ok(map);
        }

        [Authorize]
        [HttpGet("GetAllExamsForUser")]

        public async Task<ActionResult<IEnumerable<ReturnExamDto>>> GetAllExamsForUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized, "Please sign In"));

            var exams = await _unitOfWork.ExamRepository.GetAllExamsForUserAsync(userId);
            var map = _mapper.Map<IEnumerable<ReturnExamDto>>(exams);
            return Ok(map);
        }


        [Authorize]
        [HttpGet("GetById")]
        public async Task<ActionResult<ReturnExamDto>> GetById(int examId)
        {
            var exams = await _unitOfWork.ExamRepository.GetByIdAsync(examId);
            if (exams is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Exam with this Id is not found"));

            var map = _mapper.Map<ReturnExamDto>(exams);
            return Ok(map);
        }


       [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult<int>> AddExam(ExamDto examDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationResponse(400
           , "a bad Request , You have made"
           , ModelState.Values
           .SelectMany(v => v.Errors)
           .Select(e => e.ErrorMessage)
           .ToList()));

            if (examDto.CourseId != 0)
            {
                var course = await _unitOfWork.CourseRepository.GetByIdAsync(examDto.CourseId);

                if (course is null)
                    return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Course with this Id is not Found"));
            }

            var map = _mapper.Map<Exam>(examDto);
            var count = await _unitOfWork.ExamRepository.AddAsync(map);
            if (count > 0)
                return Ok("Exam is Added successfully");

            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save , please try again"));
        }

       [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut]
        public async Task<ActionResult<int>> UpdateExam(ExamDto examDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationResponse(400
           , "a bad Request , You have made"
           , ModelState.Values
           .SelectMany(v => v.Errors)
           .Select(e => e.ErrorMessage)
           .ToList()));


            var exam = await _unitOfWork.ExamRepository.GetByIdAsync(examDto.Id);
            if (exam is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Exam with this Id is not Found"));


            var course = await _unitOfWork.CourseRepository.GetByIdAsync(examDto.CourseId);
            if (course is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Course with this Id is not Found"));


            _mapper.Map(examDto, exam);
            var count = await _unitOfWork.ExamRepository.UpdateAsync(exam);
            if (count > 0)
                return Ok("Exam is updated successfully");

            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save , please try again"));
        }

       [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete]
        public async Task<ActionResult<int>> DeleteExam(int Id)
        {
            var exam = await _unitOfWork.ExamRepository.GetByIdAsync(Id);
            if (exam is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Exam with this Id is not found"));

            var count = await _unitOfWork.ExamRepository.DeleteAsync(exam);
            if (count > 0)
            {
                return Ok("Exam is Deleted successfully");
            }
            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save , please try again"));
        }


    }
}
