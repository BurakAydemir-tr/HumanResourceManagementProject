using Business.Abstract;
using Core.CustomAttributes;
using Core.Enums;
using Core.Sevices.AuthorizeService;
using Core.Utilities.FileOperations;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CVsController : ControllerBase
    {
        private readonly ICVService _cVService;

        public CVsController(ICVService cVService)
        {
            _cVService = cVService;
        }

        [HttpGet("getbycandidateid")]
        [AuthorizeDefinition(Menu = "CVs", ActionType = ActionType.Reading,
            Definition = "Get By Candidate id")]
        public IActionResult GetByCandidateId(int candidateId)
        {
            var result = _cVService.GetByCandidateId(candidateId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        [AuthorizeDefinition(Menu = "CVs", ActionType = ActionType.Reading,
            Definition = "Get By id")]
        public IActionResult GetById(int id)
        {
            var result = _cVService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        [AuthorizeDefinition(Menu = "CVs", ActionType = ActionType.Writing,
            Definition = "Add CV")]
        public IActionResult Add([FromForm]CVDto cVDto)
        {
            var result = _cVService.Add(cVDto);
            if (result.Result.Success)
            {
                return Ok(result.Result.Message);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        [AuthorizeDefinition(Menu = "CVs", ActionType = ActionType.Deleting,
            Definition = "Delete CV")]
        public IActionResult Delete(int id)
        {
            var result = _cVService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        [AuthorizeDefinition(Menu = "CVs", ActionType = ActionType.Updating,
            Definition = "Update CV")]
        public IActionResult Update([FromForm]CVDto cVDto)
        {
            var result = _cVService.Update(cVDto);
            if (result.Result.Success)
            {
                return Ok(result.Result);
            }
            return BadRequest(result);
        }

        [HttpPost("upload")]
        [AuthorizeDefinition(Menu = "CVs", ActionType = ActionType.Writing,
            Definition = "File Upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            string path=await FileOperationsHelper.UploadSingleAsync("images", file);
            return Ok(path);
        }
    }
}
