using Business.Abstract;
using Core.CustomAttributes;
using Core.Enums;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobAdvertisementsController : ControllerBase
    {
        private readonly IJobAdvertisementService _jobAdvertisementService;

        public JobAdvertisementsController(IJobAdvertisementService jobAdvertisementService)
        {
            _jobAdvertisementService = jobAdvertisementService;
        }

        [HttpGet("getbyid")]
        [AuthorizeDefinition(Menu ="JobAdvertisements", ActionType =ActionType.Reading,
            Definition = "Get JobAdvertisement By Id")]
        public IActionResult GetById(int id) 
        {
            var result=_jobAdvertisementService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        [AuthorizeDefinition(Menu = "JobAdvertisements", ActionType = ActionType.Reading,
            Definition = "Get All JobAdvertisements ")]
        public IActionResult GetAll()
        {
            var result = _jobAdvertisementService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallbyactive")]
        [AuthorizeDefinition(Menu = "JobAdvertisements", ActionType = ActionType.Reading,
            Definition = "Get JobAdvertisements By Active")]
        public IActionResult GetAllByActive()
        {
            var result = _jobAdvertisementService.GetAllByActive();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallbyemployeridisactive")]
        [AuthorizeDefinition(Menu = "JobAdvertisements", ActionType = ActionType.Reading,
            Definition = "Get JobAdvertisements By Employer Id and is active")]
        public IActionResult GetAllByEmployerIdIsActive(int employerId)
        {
            var result = _jobAdvertisementService.GetAllByEmployerIdIsActive(employerId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallbyactiveorderbydate")]
        [AuthorizeDefinition(Menu = "JobAdvertisements", ActionType = ActionType.Reading,
            Definition = "Get JobAdvertisements By Active and Orderby Date")]
        public IActionResult GetAllByActiveOrderByDate()
        {
            var result = _jobAdvertisementService.GetAllByActiveOrderByDate();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        [AuthorizeDefinition(Menu = "JobAdvertisements", ActionType = ActionType.Writing,
            Definition = "Add JobAdvertisement")]
        public IActionResult Add(JobAdvertisementDto jobAdvertisementDto)
        {
            JobAdvertisement jobAdvertisement = new JobAdvertisement();
            jobAdvertisement.Id = jobAdvertisementDto.Id;
            jobAdvertisement.EmployerId = jobAdvertisementDto.EmployerId;
            jobAdvertisement.JobPositionId= jobAdvertisementDto.JobPositionId;
            jobAdvertisement.SalaryMin = jobAdvertisementDto.SalaryMin;
            jobAdvertisement.SalaryMax = jobAdvertisementDto.SalaryMax;
            jobAdvertisement.DeadlineDate = jobAdvertisementDto.DeadlineDate;
            jobAdvertisement.JobDescription = jobAdvertisementDto.JobDescription;
            jobAdvertisement.PositionNumber = jobAdvertisementDto.PositionNumber;
            jobAdvertisement.IsActive = true;

            var result = _jobAdvertisementService.Add(jobAdvertisement);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        [AuthorizeDefinition(Menu = "JobAdvertisements", ActionType = ActionType.Updating,
            Definition = "Update JobAdvertisement")]
        public IActionResult Update(JobAdvertisementDto jobAdvertisementDto)
        {
            JobAdvertisement jobAdvertisement = new JobAdvertisement();
            jobAdvertisement.Id = jobAdvertisementDto.Id;
            jobAdvertisement.EmployerId = jobAdvertisementDto.EmployerId;
            jobAdvertisement.JobPositionId = jobAdvertisementDto.JobPositionId;
            jobAdvertisement.SalaryMin = jobAdvertisementDto.SalaryMin;
            jobAdvertisement.SalaryMax = jobAdvertisementDto.SalaryMax;
            jobAdvertisement.DeadlineDate = jobAdvertisementDto.DeadlineDate;
            jobAdvertisement.JobDescription = jobAdvertisementDto.JobDescription;
            jobAdvertisement.PositionNumber = jobAdvertisementDto.PositionNumber;
            jobAdvertisement.IsActive = jobAdvertisementDto.IsActive;

            var result=_jobAdvertisementService.Update(jobAdvertisement);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        [AuthorizeDefinition(Menu = "JobAdvertisements", ActionType = ActionType.Deleting,
            Definition = "Delete JobAdvertisement")]
        public IActionResult Delete(int id)
        {
            var result=_jobAdvertisementService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
