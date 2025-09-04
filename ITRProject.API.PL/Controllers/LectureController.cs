using AutoMapper;
using ITR.API.DAL.Models;
using ITR.API.DAL.Repositories;
using ITRProject.API.PL.Dtos.Exam;
using ITRProject.API.PL.Dtos.Lecture;
using ITRProject.API.PL.Dtos.Vod;
using ITRProject.API.PL.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ITRProject.API.PL.Controllers
{
    public class LectureController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly VodService _vodService;

        public LectureController(IUnitOfWork unitOfWork , IMapper mapper , VodService vodService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _vodService = vodService;
        }



        [Authorize]
        [HttpGet("GetAllLecture")]
        public async Task<ActionResult<ReturnLecturesDto>> GetAllLecture()
        {

            var lecture = await _unitOfWork.LectureRepository.GetAllAsync();

            var map = _mapper.Map<IEnumerable<ReturnLecturesDto>>(lecture);

            return Ok(map);
        }

        [Authorize]
        [HttpGet("GetAllLecturesForCourse")]
        public async Task<ActionResult<ReturnLecturesDto>> GetAllLecturesForCourse(int CourseId)
        {

            var course = _unitOfWork.CourseRepository.GetByIdAsync(CourseId);
            if(course is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Course with this Id is not Found"));

            var lecture = await _unitOfWork.LectureRepository.GetLectureForCourse(CourseId);

            var map = _mapper.Map<IEnumerable<ReturnLecturesDto>>(lecture);

            return Ok(map);
        }


        [Authorize]
        [HttpGet("GetLecture")]
        public async Task<ActionResult<ReturnLectureDto>> GetLecture(int lectureId)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationResponse(400,
                    "a bad Request , You have made",
                    ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()));

            var lecture = await _unitOfWork.LectureRepository.GetByIdToUserAsync(lectureId);

            var map = _mapper.Map<ReturnLectureDto>(lecture);

            try
            {
                var signResponseJson = await _vodService.SignLectureAsync(lecture.Uuid);
                map.Qualities = signResponseJson.qualities;
            }
            catch
            {
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "This Lecture Isn't Available Now"));
            }
            

            return Ok(map);
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult<int>> AddLecture(AddLectureDto lectureDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationResponse(400
           , "a bad Request , You have made"
           , ModelState.Values
           .SelectMany(v => v.Errors)
           .Select(e => e.ErrorMessage)
           .ToList()));

            if (lectureDto.CourseId != 0)
            {
                var course = await _unitOfWork.CourseRepository.GetByIdAsync(lectureDto.CourseId);

                if (course is null)
                    return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Course with this Id is not Found"));
            }

            var map = _mapper.Map<Lecture>(lectureDto);
            map.Uuid = Guid.NewGuid().ToString();


            var count = await _unitOfWork.LectureRepository.AddAsync(map);
            if (count > 0)
            {
                try
                {

                  var result = await _vodService.ProcessLectureAsync(map.Uuid, map.FolderId, lectureDto.Qualities);
                }
                catch
                {
                    return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "This Lecture Under Processing"));
                }

                return Ok("Lecture is Added successfully");
            }

            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save , please try again"));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPut]
        public async Task<ActionResult<int>> UpdateLecture(UpdateLectureDto lectureDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationResponse(400
           , "a bad Request , You have made"
           , ModelState.Values
           .SelectMany(v => v.Errors)
           .Select(e => e.ErrorMessage)
           .ToList()));

            var lecture = await _unitOfWork.LectureRepository.GetByIdAsync(lectureDto.Id);
            if(lecture is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Course with this Id is not Found"));

                var course = await _unitOfWork.CourseRepository.GetByIdAsync(lectureDto.CourseId);

                if (course is null)
                    return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Course with this Id is not Found"));


            _mapper.Map(lectureDto, lecture);


            var count = await _unitOfWork.LectureRepository.UpdateAsync(lecture);
            if (count > 0)
            {
                return Ok("Lecture is Updated successfully");
            }

            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save , please try again"));
        }


        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete]
        public async Task<ActionResult<int>> DeleteLecture(int Id)
        {
            var lecture = await _unitOfWork.LectureRepository.GetByIdAsync(Id);
            if (lecture is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Lecture with this Id is not found"));

            var count = await _unitOfWork.LectureRepository.DeleteAsync(lecture);
            if (count > 0)
            {
                return Ok("Lecture is Deleted successfully");
            }
            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save , please try again"));
        }



        [HttpPost("lms/webhook")]
        public async Task<ActionResult<int>> UpdateWeebHook([FromBody]UpdateWebHookDto updateWebHookDto)
        {

            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationResponse(400
           , "a bad Request , You have made"
           , ModelState.Values
           .SelectMany(v => v.Errors)
           .Select(e => e.ErrorMessage)
           .ToList()));

            var lecture = await _unitOfWork.LectureRepository.GetLectureByUuid(updateWebHookDto.video);
            if(lecture is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Lecture with this video is not found"));

            lecture.Image = updateWebHookDto.image;
            lecture.IsReady = true;
            lecture.Qualities = JsonSerializer.Serialize(new Dictionary<string, Dictionary<string, object>>(updateWebHookDto.qualities));

            var result = await _unitOfWork.LectureRepository.UpdateAsync(lecture);

            if(result > 0)
            return Ok();

            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save , please try again"));
        }

    }
}