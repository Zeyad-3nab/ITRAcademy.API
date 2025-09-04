using AutoMapper;
using ITR.API.DAL.Models;
using ITR.API.DAL.Repositories;
using ITRProject.API.PL.Dtos.Course;
using ITRProject.API.PL.Errors;
using ITRProject.API.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITRProject.API.PL.Controllers
{
    public class CourseController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("GetAllForLandingPage")]

        public async Task<ActionResult<IEnumerable<ReturnCourseDto>>> GetAllForLandingPage()
        {
            var courses = await _unitOfWork.CourseRepository.GetAllToLandingPageAsync();
            var map = _mapper.Map<IEnumerable<ReturnCourseDto>>(courses);
            return Ok(map);
        }




        [Authorize]
        [HttpGet("GetAll")]

        public async Task<ActionResult<IEnumerable<ReturnCourseDto>>> GetAll() 
        {
            var courses = await _unitOfWork.CourseRepository.GetAllAsync();
            var map = _mapper.Map<IEnumerable<ReturnCourseDto>>(courses);
            return Ok(map);
        }


        [HttpGet("GetAllFreeCourses")]

        public async Task<ActionResult<IEnumerable<ReturnCourseDto>>> GetAllFreeCourses()
        {
            var courses = await _unitOfWork.CourseRepository.GetAllFreeAsync();
            var map = _mapper.Map<IEnumerable<ReturnCourseDto>>(courses);
            return Ok(map);
        }

        [HttpGet("GetAllPaidCourses")]

        public async Task<ActionResult<IEnumerable<ReturnCourseDto>>> GetAllPaidCourses()
        {
            var courses = await _unitOfWork.CourseRepository.GetAllPaidAsync();
            var map = _mapper.Map<IEnumerable<ReturnCourseDto>>(courses);
            return Ok(map);
        }

        [Authorize]
        [HttpGet("GetById")]

        public async Task<ActionResult<ReturnCourseDto>> GetById(int courseId)
        {
            var course = await _unitOfWork.CourseRepository.GetByIdAsync(courseId);
            if (course is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Course with this Id is not found"));


            var map = _mapper.Map<ReturnCourseDto>(course);
            return Ok(map);
        }


       [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult<int>> AddCourse(AddCourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationResponse(400
           , "a bad Request , You have made"
           , ModelState.Values
           .SelectMany(v => v.Errors)
           .Select(e => e.ErrorMessage)
           .ToList()));


            if (courseDto.Image is not null)
            {
                courseDto.ImageUrl = DocumentSettings.Upload(courseDto.Image, "Images");   // خزن الصوره وهات اسمها
            }

            var map = _mapper.Map<Course>(courseDto);
            var count = await _unitOfWork.CourseRepository.AddAsync(map);
            if (count > 0)
                return Ok("Course is Added successfully");

            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save , please try again"));
        }

       [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut]
        public async Task<ActionResult<int>> UpdateCourse(UpdateCourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationResponse(400
           , "a bad Request , You have made"
           , ModelState.Values
           .SelectMany(v => v.Errors)
           .Select(e => e.ErrorMessage)
           .ToList()));

            var course = await _unitOfWork.CourseRepository.GetByIdAsync(courseDto.Id);
            if (course is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Course with this Id is not Found"));


            if (courseDto.Image is not null)
            {
                DocumentSettings.Delete(course.ImageUrl, "Images");  //Upload Image to wwwroot
                courseDto.ImageUrl = DocumentSettings.Upload(courseDto.Image, "Images");   // خزن الصوره وهات اسمها
            }

            _mapper.Map(courseDto, course);
            var count = await _unitOfWork.CourseRepository.UpdateAsync(course);
            if (count > 0)
                return Ok("Course is updated successfully");

            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save , please try again"));
        }

       [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete]
        public async Task<ActionResult<int>> DeleteCourse(int Id)
        {
            var course = await _unitOfWork.CourseRepository.GetByIdAsync(Id);
            if (course is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Course with this Id is not found"));

            var count = await _unitOfWork.CourseRepository.DeleteAsync(course);
            if (count > 0)
            {
                DocumentSettings.Delete(course.ImageUrl, "Images");  //Upload Image to wwwroot
                return Ok("Course is Deleted successfully");
            }
            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save , please try again"));
        }



    }
}
