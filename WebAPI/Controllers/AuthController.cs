using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("LoginByCandidate")]
        public IActionResult LoginByCandidate(CandidateForLoginDto candidateForLoginDto)
        {
            var candidateToLogin=_authService.LoginByCandidate(candidateForLoginDto);
            if (!candidateToLogin.Success)
            {
                return BadRequest(candidateToLogin.Message);
            }

            var result = _authService.CreateAccessToken(candidateToLogin.Data);
            var updateRefreshToken = _userService.UpdateRefreshToken(candidateToLogin.Data, result.Data.RefreshToken, result.Data.Expiration, 2);
            if (result.Success && updateRefreshToken.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("LoginByEmployer")]
        public IActionResult LoginByEmployer(EmployerForLoginDto employerForLoginDto)
        {
            var employerToLogin = _authService.LoginByEmployer(employerForLoginDto);
            if (!employerToLogin.Success)
            {
                return BadRequest(employerToLogin.Message);
            }

            var result = _authService.CreateAccessToken(employerToLogin.Data);
            var updateRefreshToken=_userService.UpdateRefreshToken(employerToLogin.Data,result.Data.RefreshToken, result.Data.Expiration, 2);
            if (result.Success && updateRefreshToken.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("RegisterToCandidate")]
        public IActionResult RegisterToCandidate(CandidateForRegisterDto candidateForRegisterDto)
        {
            var candidateExists = _authService.UserExists(candidateForRegisterDto.Email);
            if (!candidateExists.Success)
            {
                return BadRequest(candidateExists.Message);
            }

            var registerResult=_authService.RegisterByCandidate(candidateForRegisterDto);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (registerResult.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(registerResult.Message);
        }

        [HttpPost("RegisterToEmployer")]
        public IActionResult RegisterToEmployer(EmployerForRegisterDto employerForRegisterDto)
        {
            var employerExists = _authService.UserExists(employerForRegisterDto.Email);
            if (!employerExists.Success)
            {
                return BadRequest(employerExists.Message);
            }

            var registerResult = _authService.RegisterByEmployer(employerForRegisterDto);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (registerResult.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(registerResult.Message);
        }


        [HttpPost("RefreshTokenLogin")]
        public IActionResult RefreshTokenLogin([FromForm] string refreshToken)
        {
            var loginResult = _authService.RefreshTokenLogin(refreshToken);
            if (loginResult.Success)
            {
                return Ok(loginResult);
            }
            return BadRequest(loginResult.Message);
        }


        [HttpPost("PasswordReset")]
        public IActionResult PasswordReset(string email)
        {
            var result=_authService.PasswordReset(email);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("VerifyResetToken")]
        public IActionResult VerifyResetToken(string resetToken, string userId)
        {
            var result = _authService.VerifyResetToken(resetToken, userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("UpdatePassword")]
        public IActionResult UpdatePassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var result = _authService.UpdatePassword(resetPasswordDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //private void SetRefreshTokenToCookie(string refreshToken)
        //{
        //    CookieOptions cookieOptions = new() { HttpOnly = true, Expires = DateTime.Now.AddDays(7) };
        //    Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        //}
    }
}
