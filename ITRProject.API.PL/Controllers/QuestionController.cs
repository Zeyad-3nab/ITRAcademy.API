using AutoMapper;
using ITR.API.DAL.Models;
using ITR.API.DAL.Repositories;
using ITRProject.API.PL.Dtos.Exam;
using ITRProject.API.PL.Dtos.Question;
using ITRProject.API.PL.Errors;
using ITRProject.API.PL.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace ITRProject.API.PL.Controllers
{
    public class QuestionController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuestionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ReturnQuestionDto>>> GetAll()
        {
            var questions = await _unitOfWork.QuestionRepository.GetAllAsync();
            var map = _mapper.Map<IEnumerable<ReturnQuestionDto>>(questions);
            return Ok(map);
        }

        [Authorize]
        [HttpGet("GetAllExamQuestions")]
        public async Task<ActionResult<IEnumerable<ReturnQuestionDto>>> GetAllExamQuestions(int examId)
        {
            var questions = await _unitOfWork.QuestionRepository.GetAllExamQuestionsAsync(examId);
            var map = _mapper.Map<IEnumerable<ReturnQuestionDto>>(questions);
            return Ok(map);
        }

        [Authorize]
        [HttpGet("GetById")]
        public async Task<ActionResult<ReturnQuestionDto>> GetById(int questionId)
        {
            var question = await _unitOfWork.QuestionRepository.GetByIdAsync(questionId);
            if (question is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Question with this Id is not found"));

            var map = _mapper.Map<ReturnQuestionDto>(question);
            return Ok(map);
        }


       [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost("AddTextQuestion")]
        public async Task<ActionResult<int>> AddTextQuestion(AddTextQuestionDto questionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationResponse(400
           , "a bad Request , You have made"
           , ModelState.Values
           .SelectMany(v => v.Errors)
           .Select(e => e.ErrorMessage)
           .ToList()));


            var exam = await _unitOfWork.ExamRepository.GetByIdAsync(questionDto.ExamId);
            if (exam is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Exam with this Id is not Found"));

            var map = _mapper.Map<Question>(questionDto);
            var count = await _unitOfWork.QuestionRepository.AddAsync(map);
            if (count > 0)
                return Ok("Question is Added successfully");

            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save , please try again"));
        }



       [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost("AddImageQuestion")]
        public async Task<ActionResult> AddImageQuestion([FromForm] AddImageQuestionDto questionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiValidationResponse(
                    400,
                    "a bad Request , You have made",
                    ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                ));

            var exam = await _unitOfWork.ExamRepository.GetByIdAsync(questionDto.ExamId);
            if (exam is null)
                return NotFound(new ApiErrorResponse(
                    StatusCodes.Status404NotFound,
                    "Exam with this Id is not Found"
                ));

            // لو فيه صورة
            if (questionDto.ContentImage is not null)
                questionDto.Content = DocumentSettings.Upload(questionDto.ContentImage, "Images");

            var map = _mapper.Map<Question>(questionDto);

            var choices = new List<Choice>();

            // الحالة الأولى: Choices جت Indexed form-data
            if (questionDto.Choices is not null && questionDto.Choices.Any())
                choices.AddRange(questionDto.Choices);

            // الحالة الثانية: ChoicesJson جت كـ String JSON
            if (!string.IsNullOrWhiteSpace(questionDto.ChoicesJson))
            {
                try
                {
                    var jsonChoices = JsonSerializer.Deserialize<List<Choice>>(questionDto.ChoicesJson);
                    if (jsonChoices is not null)
                        choices.AddRange(jsonChoices);
                }
                catch (Exception ex)
                {
                    return BadRequest(new ApiErrorResponse(
                        StatusCodes.Status400BadRequest,
                        $"Invalid JSON format for Choices: {ex.Message}"
                    ));
                }
            }

            map.Choices = choices;

            var count = await _unitOfWork.QuestionRepository.AddAsync(map);
            if (count > 0)
                return Ok("Question is Added successfully");

            return BadRequest(new ApiErrorResponse(
                StatusCodes.Status400BadRequest,
                "Error in save , please try again"
            ));
        }


       [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpDelete]
        public async Task<ActionResult<int>> DeleteQuestion(int Id)
        {
            var question = await _unitOfWork.QuestionRepository.GetByIdAsync(Id);
            if (question is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Question with this Id is not found"));

            var count = await _unitOfWork.QuestionRepository.DeleteAsync(question);
            if (count > 0)
            {
                return Ok("Question is Deleted successfully");
            }
            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save , please try again"));
        }
    }
}
