using AutoMapper;
using ITR.API.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITRProject.API.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VodController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly VodService _vodService;

        public VodController(IUnitOfWork unitOfWork, IMapper mapper, VodService vodService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _vodService = vodService;
        }


        [HttpGet("folders")]
        public async Task<IActionResult> GetFolders()
        {
            var result = await _vodService.FetchFoldersAsync();
            return Content(result, "application/json");
        }

    }
}
