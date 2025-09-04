using ITR.API.DAL.Models;
using ITR.API.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ITRProject.API.PL.Controllers
{
    public class StatisticsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public StatisticsController(IUnitOfWork unitOfWork , UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

       [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("GetAllAdminStatistics")]
        public async Task<ActionResult> GetAllAdminStatistics()
        {
            AdminStatistics adminStatistics = new AdminStatistics()
            {
                NumOfAdmins = await _userManager.Users.Where(u => u.Role == "Admin").CountAsync(),
                NumOfUsers = await _userManager.Users.Where(u => u.Role == "User").CountAsync(),
                NumOfRejectedOrders = await _unitOfWork.UserCourseRepository.GetCountOfRejectedUserCourseAsync(),
                NumOfConfirmedOrders = await _unitOfWork.UserCourseRepository.GetCountOfAcceptedUserCourseAsync(),
                NumOfCourses = await _unitOfWork.CourseRepository.GetCourseCount()

            };
            return Ok(adminStatistics);
        }



        [Authorize(Roles = "User")]
        [HttpGet("GetAllUserStatistics")]
        public async Task<ActionResult> GetAllUserStatistics()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            UserStatistics userStatistics = new UserStatistics()
            {
                NumOfCourses = await _unitOfWork.CourseRepository.GetCourseCount(),
                NumOfFreeCourses = await _unitOfWork.UserCourseRepository.NumOfFreeCourse(userId),
                NumOfPaidCourses = await _unitOfWork.UserCourseRepository.NumOfPaidCourse(userId),
            };
            return Ok(userStatistics);
        }
    }
}
