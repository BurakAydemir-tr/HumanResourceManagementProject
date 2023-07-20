using Business.Abstract;
using Core.CustomAttributes;
using Core.Enums;
using Core.Sevices.AuthorizeService;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologiesController : ControllerBase
    {
        private readonly ITechnologyService _technologyService;

        public TechnologiesController(ITechnologyService technologyService)
        {
            _technologyService = technologyService;
        }

        [HttpGet("getallbycvid")]
        [AuthorizeDefinition(Menu = "Technologies", ActionType = ActionType.Reading,
            Definition = "Get All By CV Id")]
        public IActionResult GetAllByCVId(int candidateId)
        {
            var result = _technologyService.GetAllByCVId(candidateId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        [AuthorizeDefinition(Menu = "Technologies", ActionType = ActionType.Reading,
            Definition = "Get By Id")]
        public IActionResult GetById(int id)
        {
            var result = _technologyService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        [AuthorizeDefinition(Menu = "Technologies", ActionType = ActionType.Writing,
            Definition = "Add Technology")]
        public IActionResult Add(TechnologyDto technologyDto)
        {
            var result = _technologyService.Add(technologyDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        [AuthorizeDefinition(Menu = "Technologies", ActionType = ActionType.Deleting,
            Definition = "Delete Technology")]
        public IActionResult Delete(int id)
        {
            var result = _technologyService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        [AuthorizeDefinition(Menu = "Technologies", ActionType = ActionType.Updating,
            Definition = "Update Technology")]
        public IActionResult Update(TechnologyDto technologyDto)
        {
            var result = _technologyService.Update(technologyDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
