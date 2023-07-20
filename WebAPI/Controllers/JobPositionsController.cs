using Business.Abstract;
using Core.CustomAttributes;
using Core.Enums;
using Core.Sevices.AuthorizeService;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPositionsController : ControllerBase
    {
        private readonly IJobPositionService _jobPositionService;

        public JobPositionsController(IJobPositionService jobPositionService)
        {
            _jobPositionService = jobPositionService;
        }

        [HttpGet("getall")]
        [AuthorizeDefinition(Menu = "JobPositions", ActionType = ActionType.Reading,
            Definition = "Get All")]
        public IActionResult GetAll()
        {
            var result = _jobPositionService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        [AuthorizeDefinition(Menu = "JobPositions", ActionType = ActionType.Reading,
            Definition = "Get By Id")]
        public IActionResult GetById(int id)
        {
            var result = _jobPositionService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        [AuthorizeDefinition(Menu = "JobPositions", ActionType = ActionType.Writing,
            Definition = "Add JobPosition")]
        public IActionResult Add(JobPosition jobPosition)
        {
            var result = _jobPositionService.Add(jobPosition);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        [AuthorizeDefinition(Menu = "JobPositions", ActionType = ActionType.Updating,
            Definition = "Update JobPosition")]
        public IActionResult Update(JobPosition jobPosition)
        {
            var result = _jobPositionService.Update(jobPosition);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        [AuthorizeDefinition(Menu = "JobPositions", ActionType = ActionType.Deleting,
            Definition = "Delete JobPosition")]
        public IActionResult Delete(int id)
        {
            var result = _jobPositionService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
