using Business.Abstract;
using Core.CustomAttributes;
using Core.Enums;
using Core.Sevices.AuthorizeService;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ActionFilters;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController : ControllerBase
    {
        private readonly IUniversityService _universityService;

        public UniversitiesController(IUniversityService universityService)
        {
            _universityService = universityService;
        }

        [HttpGet("getallbycvid")]
        [AuthorizeDefinition(Menu = "Universities", ActionType = ActionType.Reading,
            Definition = "Get all By CV Id")]
        public IActionResult GetAllByCVId(int candidateId)
        {
            var result = _universityService.GetAllByCVId(candidateId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        [AuthorizeDefinition(Menu = "Universities", ActionType = ActionType.Reading,
            Definition = "Get By Id")]
        public IActionResult GetById(int id)
        {
            var result = _universityService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        [AuthorizeDefinition(Menu = "Universities", ActionType = ActionType.Writing,
            Definition = "Add University")]
        public IActionResult Add(UniversityDto universityDto)
        {
            var result = _universityService.Add(universityDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        [AuthorizeDefinition(Menu = "Universities", ActionType = ActionType.Deleting,
            Definition = "Delete University")]
        public IActionResult Delete(int id)
        {
            var result = _universityService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        [AuthorizeDefinition(Menu = "Universities", ActionType = ActionType.Updating,
            Definition = "Update University")]
        public IActionResult Update(UniversityDto universityDto)
        {
            var result = _universityService.Update(universityDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
