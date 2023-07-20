using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.CustomAttributes;
using Core.Enums;
using Core.Sevices.AuthorizeService;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ActionFilters;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployersController : ControllerBase
    {
        private readonly IEmployerService _employerService;

        public EmployersController(IEmployerService employerService)
        {
            _employerService = employerService;
        }

        [HttpGet("getall")]
        [AuthorizeDefinition(Menu = "Employers", ActionType = ActionType.Reading,
            Definition = "Get All Employers")]
        public IActionResult GetAll()
        {
            var result=_employerService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyId")]
        [AuthorizeDefinition(Menu = "Employers", ActionType = ActionType.Reading,
            Definition = "Get by Employer id")]
        public IActionResult GetById(int id)
        {
            var result = _employerService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        [ValidationFilter(typeof(EmployerValidator))]
        [AuthorizeDefinition(Menu = "Employers", ActionType = ActionType.Writing,
            Definition = "Add Employer")]
        public IActionResult Add(Employer employer)
        {
            var result = _employerService.Add(employer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
