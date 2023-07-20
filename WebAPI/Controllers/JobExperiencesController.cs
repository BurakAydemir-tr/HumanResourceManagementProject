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
    public class JobExperiencesController : ControllerBase
    {
        private readonly IJobExperienceService _jobExperiencesService;

        public JobExperiencesController(IJobExperienceService jobExperiencesService)
        {
            _jobExperiencesService = jobExperiencesService;
        }

        [HttpGet("getallbycvid")]
        [AuthorizeDefinition(Menu = "JobExperiences", ActionType = ActionType.Reading,
            Definition = "Get All By CV Id")]
        public IActionResult GetAllByCVId(int candidateId)
        {
            var result = _jobExperiencesService.GetAllByCVId(candidateId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        [AuthorizeDefinition(Menu = "JobExperiences", ActionType = ActionType.Reading,
            Definition = "Get By Id")]
        public IActionResult GetById(int id)
        {
            var result = _jobExperiencesService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        [AuthorizeDefinition(Menu = "JobExperiences", ActionType = ActionType.Writing,
            Definition = "Add JobExperience")]
        public IActionResult Add(JobExperienceDto jobExperienceDto)
        {
            var result = _jobExperiencesService.Add(jobExperienceDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        [AuthorizeDefinition(Menu = "JobExperiences", ActionType = ActionType.Deleting,
            Definition = "Delete JobExperience")]
        public IActionResult Delete(int id)
        {
            var result = _jobExperiencesService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        [AuthorizeDefinition(Menu = "JobExperiences", ActionType = ActionType.Updating,
            Definition = "Update JobExperience")]
        public IActionResult Update(JobExperienceDto jobExperienceDto)
        {
            var result = _jobExperiencesService.Update(jobExperienceDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
