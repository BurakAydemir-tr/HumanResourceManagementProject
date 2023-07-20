using Business.Abstract;
using Core.CustomAttributes;
using Core.Enums;
using Core.Sevices.AuthorizeService;
using Core.Sevices.MailService;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMailService _mailService;

        public UsersController(IUserService userService, IMailService mailService)
        {
            _userService = userService;
            _mailService = mailService;
        }

        [HttpGet("getall")]
        [AuthorizeDefinition(Menu = "Users", ActionType = ActionType.Reading,
            Definition = "Get All Users")]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        [AuthorizeDefinition(Menu = "Users", ActionType = ActionType.Reading,
            Definition = "Get By User Id")]
        public IActionResult GetById(int id)
        {
            var result = _userService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyemail")]
        [AuthorizeDefinition(Menu = "Users", ActionType = ActionType.Reading,
            Definition = "Get By User Email")]
        public IActionResult GetByEmail(string email)
        {
            var result = _userService.GetByEmail(email);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("SendMail")]
        [AuthorizeDefinition(Menu = "Users", ActionType = ActionType.Writing,
            Definition = "Send Email Test")]
        public IActionResult SendMailTest([FromForm]MailDto mailDto)
        {
            _mailService.SendMail(new[] {mailDto.Receiver},mailDto.Subject, mailDto.Body);
            return Ok("Mail gönderildi");
        }

        [HttpPost("AssignOperationClaimToUser")]
        [AuthorizeDefinition(Menu = "Users", ActionType = ActionType.Writing,
            Definition = "Assign OperationClaim To User")]
        public IActionResult AssignOperationClaimToUser(int userId, int[] operationClaimId)
        {
            var result = _userService.AssignOperationClaimToUser(userId,operationClaimId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetClaims")]
        [AuthorizeDefinition(Menu = "Users", ActionType = ActionType.Reading,
            Definition = "Get User Claims")]
        public IActionResult GetClaims(int userId)
        {
            var user=_userService.GetById(userId);
            if (user.Success)
            {
                var claims = _userService.GetClaims(user.Data);
                if (claims.Success)
                {
                    return Ok(claims);
                }
            }
            return BadRequest(user);

        }
    }
}
