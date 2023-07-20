using Core.CustomAttributes;
using Core.Enums;
using Core.Sevices.AuthorizeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationServicesController : ControllerBase
    {
        readonly IApplicationService _applicationService;

        public ApplicationServicesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [AuthorizeDefinition(Menu = "ApplicationServices", ActionType = ActionType.Reading,
            Definition = "Get Application Endpoints")]
        public IActionResult GetAuthorizeDefinitionEndPoints()
        {
            var result = _applicationService.GetAuthorizeDefinitionEndPoints(typeof(Program));
            return Ok(result);
        }
    }
}
