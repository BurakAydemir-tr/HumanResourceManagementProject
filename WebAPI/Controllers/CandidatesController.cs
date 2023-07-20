using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.CustomAttributes;
using Core.Enums;
using Core.Sevices.AuthorizeService;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ActionFilters;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet("getall")]
        [AuthorizeDefinition(Menu = "Candidates", ActionType = ActionType.Reading,
            Definition = "Get All Candidates")]
        public IActionResult GetAll()
        {
            var result=_candidateService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        [AuthorizeDefinition(Menu = "Candidates", ActionType = ActionType.Reading,
            Definition = "Get by id Candidate")]
        public IActionResult GetById(int id)
        {
            var result=_candidateService.GetById(id);
            if (result.Success) 
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        //[ValidationFilter(typeof(CandidateValidator))]
        [AuthorizeDefinition(Menu = "Candidates", ActionType = ActionType.Writing,
            Definition = "Add Candidate")]
        public IActionResult Add(Candidate candidate)
        {
            
            var result=_candidateService.Add(candidate);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
