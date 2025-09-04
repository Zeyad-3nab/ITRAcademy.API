using AutoMapper;
using ITR.API.DAL.Models;
using ITR.API.DAL.Repositories;
using ITRProject.API.PL.Dtos.ExamResult;
using ITRProject.API.PL.Dtos.UserCourse;
using ITRProject.API.PL.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITRProject.API.PL.Controllers
{
    public class ExamResultController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExamResultController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }


        [Authorize]
        [HttpGet("GetAllExamResultForSignInUser")]
        public async Task<ActionResult> GetAllExamResultForSignInUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
                return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized, "Please sign In"));


            var examResults = await _unitOfWork.ExamResultsRepository.GetAllExamResultsToUserAsync(userId);
            var map = _mapper.Map<IEnumerable<ReturnExamResultDto>>(examResults);
            return Ok(map);
        }



        [Authorize]
        [HttpGet("GetAllExamResultForUser")]
        public async Task<ActionResult> GetAllExamResultForUser(string userId)
        {
            var examResults = await _unitOfWork.ExamResultsRepository.GetAllExamResultsToUserAsync(userId);
            var map = _mapper.Map<IEnumerable<ReturnExamResultDto>>(examResults);
            return Ok(map);
        }

       [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("GetAllExamResultsForExam")]
        public async Task<ActionResult> GetAllExamResultsForExam(int ExamId)
        {
            var examResults = await _unitOfWork.ExamResultsRepository.GetAllResultsForExam(ExamId);
            var map = _mapper.Map<IEnumerable<ReturnExamResultDto>>(examResults);
            return Ok(map);
        }



        [Authorize]
        [HttpGet("GetExamResultToUserInExam")]
        public async Task<ActionResult> GetExamResultToUserInExam(int examId, string UserId)
        {
            var courses = await _unitOfWork.ExamResultsRepository.GetExamResultForUserInExam(examId, UserId);
            var map = _mapper.Map<IEnumerable<ReturnExamResultDto>>(courses);
            return Ok(map);
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> AddExamResult(AddExamResultDto examResultDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationResponse(400
           , "a bad Request , You have made"
           , ModelState.Values
           .SelectMany(v => v.Errors)
           .Select(e => e.ErrorMessage)
           .ToList()));

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var course = await _unitOfWork.ExamRepository.GetByIdAsync(examResultDto.ExamId);
            if (course is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Exam with this Id is not Found"));

            var map = _mapper.Map<ExamResults>(examResultDto);
            map.UserId = userId;

            var count = await _unitOfWork.ExamResultsRepository.AddAsync(map);
            if (count > 0)
                return Ok("Exam Result is Added to user successfully");

            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save , please try again"));
        }


        [Authorize]
        [HttpPut("UpdateExamResultToUser")]
        public async Task<ActionResult<int>> UpdateExamResult(UpdateExamResultDto examResultDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationResponse(400
           , "a bad Request , You have made"
           , ModelState.Values
           .SelectMany(v => v.Errors)
           .Select(e => e.ErrorMessage)
           .ToList()));

            var examResult = await _unitOfWork.ExamResultsRepository.GetExamResultForUserInExam(examResultDto.ExamId, examResultDto.UserId);
            if (examResult is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User doesn't Take this Exam"));

            _mapper.Map(examResultDto, examResult);
            var count = await _unitOfWork.ExamResultsRepository.UpdateAsync(examResult);
            if (count > 0)
                return Ok("Exam Result is Updated successfully");

            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save , please try again"));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete("DeleteExamResult")]
        public async Task<ActionResult<int>> DeleteExamResult(DeleteExamResultDto examResultDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationResponse(400
           , "a bad Request , You have made"
           , ModelState.Values
           .SelectMany(v => v.Errors)
           .Select(e => e.ErrorMessage)
           .ToList()));

            var examResult = await _unitOfWork.ExamResultsRepository.GetExamResultForUserInExam(examResultDto.ExamId, examResultDto.UserId);
            if (examResult is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User doesn't Take this Exam"));

            var count = await _unitOfWork.ExamResultsRepository.DeleteAsync(examResult);
            if (count > 0)
                return Ok("Exam Result is Deleted successfully");

            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save , please try again"));
        }

    }
}
