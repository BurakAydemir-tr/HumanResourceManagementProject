using Business.Abstract;
using Core.CustomAttributes;
using Core.Enums;
using Core.Sevices.AuthorizeService;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : ControllerBase
    {
        private readonly IOperationClaimService _operationClaimService;

        public OperationClaimsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }

        [HttpGet("GetAll")]
        [AuthorizeDefinition(Menu = "OperationClaims", ActionType = ActionType.Reading,
            Definition = "Get All")]
        public IActionResult GetAll()
        {
            var result=_operationClaimService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetById")]
        [AuthorizeDefinition(Menu = "OperationClaims", ActionType = ActionType.Reading,
            Definition = "Get By Id")]
        public IActionResult GetById(int id)
        {
            var result = _operationClaimService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetByName")]
        [AuthorizeDefinition(Menu = "OperationClaims", ActionType = ActionType.Reading,
            Definition = "Get By Name")]
        public IActionResult GetByName(string name)
        {
            var result = _operationClaimService.GetByName(name);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Add")]
        [AuthorizeDefinition(Menu = "OperationClaims", ActionType = ActionType.Writing,
            Definition = "Add OperationClaim")]
        public IActionResult Add(OperationClaimDto operationClaimDto)
        {
            var result = _operationClaimService.Add(operationClaimDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("Update")]
        [AuthorizeDefinition(Menu = "OperationClaims", ActionType = ActionType.Updating,
            Definition = "Update OperationClaim")]
        public IActionResult Update(OperationClaimDto operationClaimDto)
        {
            var result = _operationClaimService.Update(operationClaimDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("Delete")]
        [AuthorizeDefinition(Menu = "OperationClaims", ActionType = ActionType.Deleting,
            Definition = "Delete OperationClaim")]
        public IActionResult Delete(int id)
        {
            var result = _operationClaimService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
