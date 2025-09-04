using AutoMapper;
using ITR.API.DAL.Models;
using ITR.API.DAL.Services;
using ITRProject.API.PL.Dtos.User;
using ITRProject.API.PL.Errors;
using ITRProject.API.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ITRProject.API.PL.Controllers
{
    
    public class AccountController : BaseController
    {


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ITokenService _TokenService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IMapper mapper,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _TokenService = tokenService;
        }




       [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("GetAll")]
        public ActionResult GetAll()
        {
            var users = _userManager.Users.Select(e => new
            {
                e.Id,
                e.UserName,
                e.Email,
                e.PhoneNumber,
                e.DateOfCreation
            }).ToList();

            return Ok(users);
        }


       [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("GetAllUsers")]
        public ActionResult GetAllUsers()
        {


            var users = _userManager.Users.Where(u => u.Role == "User").Select(e => new
            {
                e.Id,
                e.UserName,
                e.Email,
                e.PhoneNumber,
                e.DateOfCreation
            }).ToList();

            return Ok(users);
        }


       [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpGet("GetAllAdmins")]
        public ActionResult GetAllAdmins()
        {


            var users = _userManager.Users.Where(u => u.Role == "Admin").Select(e => new
            {
                e.Id,
                e.UserName,
                e.Email,
                e.PhoneNumber,
                e.DateOfCreation
            }).ToList();

            return Ok(users);
        }


        [HttpGet("GetUserById")]
        public async Task<ActionResult> GetUserById(string userId)
        {
            var user = _userManager.Users.Select(e => new
            {
                e.Id,
                e.UserName,
                e.Email,
                e.PhoneNumber,
                e.DateOfCreation
            }).FirstOrDefault(e=>e.Id ==userId);
            if (user is not null)
            {
                return Ok(user);
            }

            return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User with this Id is not found"));
        }


        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(registerDTO.Email);
                if (user is not null)
                    return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "User with this Email is found"));


                var appUser = _mapper.Map<ApplicationUser>(registerDTO);

                var result = await _userManager.CreateAsync(appUser, registerDTO.Password);    //Create Account
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, appUser.Role);

                    var ReturnedUser = new UserDto()
                    {

                        Id = appUser.Id,
                        UserName = appUser.UserName,
                        DateOfCreation = appUser.DateOfCreation,
                        role = appUser.Role,
                        Token = await _TokenService.CreateTokenAsync(appUser, _userManager)
                    };
                    return Ok(ReturnedUser);
                }

                return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest
                           , "a bad Request , You have made"
                           , result.Errors.Select(e => e.Description).ToList()));
            }

            return BadRequest(new ApiValidationResponse(400
                       , "a bad Request , You have made"
                       , ModelState.Values
                       .SelectMany(v => v.Errors)
                       .Select(e => e.ErrorMessage)
                       .ToList()));
        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDto loginDTO)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser? user = await _userManager.FindByEmailAsync(loginDTO.Email);

                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, loginDTO.Password))
                    {
                        var ReturnedUser = new UserDto()
                        {

                            Id = user.Id,
                            UserName = user.UserName,
                            DateOfCreation = user.DateOfCreation,
                            role = user.Role,
                            Token = await _TokenService.CreateTokenAsync(user, _userManager)
                        };
                        return Ok(ReturnedUser);
                    }
                    return NotFound(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Password with this Email InCorrect"));
                }
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User with this Email is not found"));
            }
            return BadRequest(new ApiValidationResponse(400
                       , "a bad Request , You have made"
                       , ModelState.Values
                       .SelectMany(v => v.Errors)
                       .Select(e => e.ErrorMessage)
                       .ToList()));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete("DeleteUser")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                if(user.Role != "User")
                    return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "You don't have an access to remove this user"));


                if (user is not null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest
                , "a bad Request , You have made"
                , result.Errors.Select(e => e.Description).ToList()));
                }
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User with this Id is not found"));
            }

            return BadRequest(new ApiValidationResponse(400
                      , "a bad Request , You have made"
                      , ModelState.Values
                      .SelectMany(v => v.Errors)
                      .Select(e => e.ErrorMessage)
                      .ToList()));
        }


        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("DeleteAdmin")]
        public async Task<ActionResult> DeleteAdmin(string id)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user.Role != "Admin")
                    return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "You don't have an access to remove this user"));


                if (user is not null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest
                , "a bad Request , You have made"
                , result.Errors.Select(e => e.Description).ToList()));
                }
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User with this Id is not found"));
            }

            return BadRequest(new ApiValidationResponse(400
                      , "a bad Request , You have made"
                      , ModelState.Values
                      .SelectMany(v => v.Errors)
                      .Select(e => e.ErrorMessage)
                      .ToList()));
        }



        [AllowAnonymous]
        [HttpPost("SendEmail")]
        public async Task<ActionResult> SendEmail([DataType(DataType.EmailAddress)] string Email)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Email);
                if (user != null)
                {
                    Random random = new Random();
                    int Code = random.Next(1000, 9999);
                    var email = new Emails()
                    {
                        To = Email,
                        Subject = "ITR Reset Password",
                        Body = $@"
                          <html>
                          <body style='font-family:Arial, sans-serif; background-color:#f4f4f4; padding:20px;'>
                              <div style='max-width:600px; margin:auto; background:#ffffff; padding:20px; border-radius:10px; box-shadow:0 2px 8px rgba(0,0,0,0.1);'>
                                  
                                  <h2 style='color:#2c3e50; text-align:center;'>ITR Password Reset</h2>
                                  
                                  <p style='font-size:16px; color:#333;'>Hello <b>{user.UserName}</b>,</p>
                                  
                                  <p style='font-size:16px; color:#333;'>
                                      You recently requested to reset your password for your <b>ITR account</b>.
                                      Please use the verification code below to reset your password:
                                  </p>
                                  
                                  <div style='text-align:center; margin:30px 0;'>
                                      <span style='font-size:28px; font-weight:bold; color:#2c3e50; letter-spacing:5px;'>{Code}</span>
                                  </div>
                                  
                                  <p style='font-size:14px; color:#555;'>
                                      Enter this code in the app to set a new password.
                                  </p>
                                  
                                  <p style='font-size:14px; color:#888;'>
                                      If you didn’t request a password reset, please ignore this email.
                                  </p>
                                  
                                  <hr style='margin:30px 0; border:none; border-top:1px solid #eee;' />
                                  <p style='font-size:12px; color:#aaa; text-align:center;'>
                                      &copy; {DateTime.Now.Year} ITR Website. All rights reserved.
                                  </p>
                              </div>
                          </body>
                          </html>
    "
                    };
                    EmailSettings.SendEmail(email);
                    return Ok(Code);
                }
                return NotFound(new ApiErrorResponse(404, "User with this Email is not found"));
            }
            return BadRequest(new ApiValidationResponse(400
                , "a bad Request , You have made"
                , ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList()));
        }

        [AllowAnonymous]
        [HttpPut("ChangePassword")]
        public async Task<ActionResult> ChangePassword([FromBody] UpdatePasswordDto updatePasswordDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(updatePasswordDTO.Email);
                if (user is not null)
                {
                    var result = await _userManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        result = await _userManager.AddPasswordAsync(user, updatePasswordDTO.Password);
                        if (result.Succeeded)
                        {
                            return Ok("changed");
                        }
                    }
                    return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest
                        , "a bad Request , You have made"
                        , ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
                }
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "User with this Email is not found"));
            }
            return BadRequest(new ApiValidationResponse(400
                , "a bad Request , You have made"
                , ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList()));
        }

    }
}
