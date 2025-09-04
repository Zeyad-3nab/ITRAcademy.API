using AutoMapper;
using ITR.API.DAL.Models;
using ITR.API.DAL.Repositories;
using ITRProject.API.PL.Dtos.Exam;
using ITRProject.API.PL.Dtos.UserCourse;
using ITRProject.API.PL.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITRProject.API.PL.Controllers
{
    public class UserCourseController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserCourseController(IUnitOfWork unitOfWork, IMapper mapper , UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("GetAllUserCourse")]
        public async Task<ActionResult> GetAllUserCourse()
        {
            var courses = await _unitOfWork.UserCourseRepository.GetAllAsync();
            var map = _mapper.Map<IEnumerable<ReturnUserCourseDto>>(courses);
            return Ok(map);
        }

        [Authorize]
        [HttpGet("GetAllCoursesForSignInUser")]
        public async Task<ActionResult> GetAllCoursesForUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized, "Please sign In"));


            var courses = await _unitOfWork.UserCourseRepository.GetAllCoursesToUserAsync(userId);
            var map = _mapper.Map<IEnumerable<ReturnUserCourseDto>>(courses);
            return Ok(map);
        }

        [Authorize]
        [HttpGet("GetAllFreeCoursesForSignInUser")]
        public async Task<ActionResult> GetFreeAllCoursesForUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized, "Please sign In"));


            var courses = await _unitOfWork.UserCourseRepository.GetAllFreeCoursesToUserAsync(userId);
            var map = _mapper.Map<IEnumerable<ReturnUserCourseDto>>(courses);
            return Ok(map);
        }

        [Authorize]
        [HttpGet("GetAllPaidCoursesForSignInUser")]
        public async Task<ActionResult> GetPaidAllCoursesForUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized, "Please sign In"));


            var courses = await _unitOfWork.UserCourseRepository.GetAllPaidCoursesToUserAsync(userId);
            var map = _mapper.Map<IEnumerable<ReturnUserCourseDto>>(courses);
            return Ok(map);
        }




        [Authorize]
        [HttpGet("GetAllCoursesForUser")]
        public async Task<ActionResult> GetAllCoursesForUser(string userId)
        {
            var courses = await _unitOfWork.UserCourseRepository.GetAllCoursesToUserAsync(userId);
            var map = _mapper.Map<IEnumerable<ReturnUserCourseDto>>(courses);
            return Ok(map);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("GetAllUsersForCourse")]
        public async Task<ActionResult> GetAllUsersForCourse(int courseId)
        {
            var courses = await _unitOfWork.UserCourseRepository.GetUsersInCourseAsync(courseId);
            var map = _mapper.Map<IEnumerable<ReturnUserCourseDto>>(courses);
            return Ok(map);
        }

        [Authorize]
        [HttpGet("GetUserCourseById")]
        public async Task<ActionResult> GetUserCourse(int courseId , string UserId)
        {
            var courses = await _unitOfWork.UserCourseRepository.GetUserCourseById(courseId , UserId);
            var map = _mapper.Map<IEnumerable<ReturnUserCourseDto>>(courses);
            return Ok(map);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> BuyCourse(AddUserCourseDto userCourseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationResponse(400
           , "a bad Request , You have made"
           , ModelState.Values
           .SelectMany(v => v.Errors)
           .Select(e => e.ErrorMessage)
           .ToList()));


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userCourse = await _unitOfWork.UserCourseRepository.GetUserCourseById(userCourseDto.CourseId, userId);

            if (userCourse is not null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User is added to this Course before"));


            var course = await _unitOfWork.CourseRepository.GetByIdAsync(userCourseDto.CourseId);
            if (course is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Course with this Id is not found"));


            var map = _mapper.Map<UserCourse>(userCourseDto);
            if (course.Type == CourseType.Free)
            {
                map.UserId = userId;
                map.State = UserCourseState.Accepted;
                map.StartTime = DateTime.Now;
                map.EndTime = DateTime.Now;
            }
            else
            {
                map.UserId = userId;
                map.State = UserCourseState.Pending;
                map.StartTime = DateTime.Now;
                map.EndTime = DateTime.Now;
            }

            var count = await _unitOfWork.UserCourseRepository.AddAsync(map);
            if (count > 0)
                return Ok("UserCourse is Added to user successfully");

            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save , please try again"));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut("ConfirmUserCourse")]
        public async Task<ActionResult<int>> ConfirmUserCourse(UpdateUserCourseDto userCourseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationResponse(400
           , "a bad Request , You have made"
           , ModelState.Values
           .SelectMany(v => v.Errors)
           .Select(e => e.ErrorMessage)
           .ToList()));

            var userCourse = await _unitOfWork.UserCourseRepository.GetUserCourseById(userCourseDto.CourseId, userCourseDto.UserId);
            if (userCourse is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User doesn't have this course"));

            userCourse.State = UserCourseState.Accepted;
            userCourse.StartTime = userCourseDto.StartTime;
            userCourse.EndTime = userCourseDto.EndTime;

            var count = await _unitOfWork.UserCourseRepository.UpdateAsync(userCourse);
            if (count > 0)
            {
                return Ok("UserCourse is Accepted successfully");
            }

            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save , please try again"));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete("RejectUserCourse")]
        public async Task<ActionResult<int>> RejectUserCourse(DeleteuserCourseDto userCourseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationResponse(400
           , "a bad Request , You have made"
           , ModelState.Values
           .SelectMany(v => v.Errors)
           .Select(e => e.ErrorMessage)
           .ToList()));

            var userCourse = await _unitOfWork.UserCourseRepository.GetUserCourseById(userCourseDto.CourseId, userCourseDto.UserId);
            if (userCourse is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User doesn't have this corse"));


            userCourse.State= UserCourseState.Rejected;

            var count = await _unitOfWork.UserCourseRepository.UpdateAsync(userCourse);
            if (count > 0)
            {
                return Ok("UserCourse is Rejected successfully");
            }

            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save , please try again"));
        }

    }
}
