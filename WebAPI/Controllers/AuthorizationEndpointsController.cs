using Business.Abstract;
using Core.CustomAttributes;
using Core.Enums;
using Core.Sevices.AuthorizeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationEndpointsController : ControllerBase
    {
        private readonly IEndpointService _endpointService;

        public AuthorizationEndpointsController(IEndpointService endpointService)
        {
            _endpointService = endpointService;
        }

        [HttpPost("AssignRoleEndpoint")]
        [AuthorizeDefinition(Menu = "AuthorizationEndpoints", ActionType = ActionType.Writing,
            Definition = "Assign Role to Endpoint")]
        public IActionResult AssignRoleEndpoint(int[] operationClaimId, string code)
        {
            var result = _endpointService.AssignRoleEndpoint(operationClaimId, code, typeof(Program));
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetByCode")]
        [AuthorizeDefinition(Menu = "AuthorizationEndpoints", ActionType = ActionType.Reading,
            Definition = "Get by code")]
        public IActionResult GetByCode(string code)
        {
            var result = _endpointService.GetByCode(code);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
