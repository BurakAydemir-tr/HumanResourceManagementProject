using Business.Abstract;
using Business.Constants;
using Core.Extensions;
using Core.Sevices.MailService;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly ICandidateService _candidateService;
        private readonly IUserService _userService;
        private readonly IEmployerService _employerService;
        private ITokenHelper _tokenHelper;
        private readonly IMailService _mailService;

        public AuthManager(ICandidateService candidateService, IUserService userService, IEmployerService employerService, ITokenHelper tokenHelper, IMailService mailService)
        {
            _candidateService = candidateService;
            _userService = userService;
            _employerService = employerService;
            _tokenHelper = tokenHelper;
            _mailService = mailService;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims=_userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, "Token oluşturuldu.");
        }

        public IDataResult<Candidate> LoginByCandidate(CandidateForLoginDto candidateForLoginDto)
        {
            var candidateToCheck=_candidateService.GetByEmail(candidateForLoginDto.Email);
            if (candidateToCheck==null)
            {
                return new ErrorDataResult<Candidate>(Messages.CandidateNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(candidateForLoginDto.Password, candidateToCheck.Data.PasswordHash))
            {
                return new ErrorDataResult<Candidate>(Messages.PasswordError);
            }
            return new SuccessDataResult<Candidate>(candidateToCheck.Data, Messages.SuccessfulLogin);
        }

        public IDataResult<Employer> LoginByEmployer(EmployerForLoginDto employerForLoginDto)
        {
            var employerToCheck = _employerService.GetByEmail(employerForLoginDto.Email);
            if (employerToCheck == null)
            {
                return new ErrorDataResult<Employer>(Messages.EmployerNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(employerForLoginDto.Password, employerToCheck.Data.PasswordHash))
            {
                return new ErrorDataResult<Employer>(Messages.PasswordError);
            }
            return new SuccessDataResult<Employer>(employerToCheck.Data, Messages.SuccessfulLogin);
        }

        
        public IDataResult<Candidate> RegisterByCandidate(CandidateForRegisterDto candidateForRegisterDto)
        {
            byte[] passwordHash;
            HashingHelper.CreatePasswordHash(candidateForRegisterDto.Password, out passwordHash);

            Candidate candidate = new Candidate();
            candidate.Email = candidateForRegisterDto.Email;
            candidate.FirstName = candidateForRegisterDto.FirstName;
            candidate.LastName = candidateForRegisterDto.LastName;
            candidate.PasswordHash=passwordHash;
            candidate.NationalId = candidateForRegisterDto.NationalId;
            candidate.BirthDate = candidateForRegisterDto.BirthDate;
            candidate.Status = true;

            _candidateService.Add(candidate);
            return new SuccessDataResult<Candidate>(candidate, Messages.CandidateAdded);
        }

        public IDataResult<Employer> RegisterByEmployer(EmployerForRegisterDto employerForRegisterDto)
        {
            byte[] passwordHash;
            HashingHelper.CreatePasswordHash(employerForRegisterDto.Password, out passwordHash);
            Employer employer = new Employer();
            employer.Email = employerForRegisterDto.Email;
            employer.PhoneNumber=employerForRegisterDto.PhoneNumber;
            employer.CompanyName=employerForRegisterDto.CompanyName;
            employer.WebAddress = employerForRegisterDto.WebAddress;
            employer.PasswordHash=passwordHash;
            employer.Status=true;
            
            _employerService.Add(employer);
            return new SuccessDataResult<Employer>(employer, Messages.EmployerAdded);
        }

        public IDataResult<AccessToken> RefreshTokenLogin(string refreshToken)
        {
            var checkToUser=_userService.GetByRefreshToken(refreshToken);
            if (checkToUser.Data != null && checkToUser.Data.RefreshTokenEndDate > DateTime.Now)
            {
                var token = CreateAccessToken(checkToUser.Data);
                _userService.UpdateRefreshToken(checkToUser.Data, token.Data.RefreshToken, token.Data.Expiration, 2);
                return token;
            }
            else
                return new ErrorDataResult<AccessToken>(Messages.UserNotFound);
        }

        public IResult UserExists(string email)
        {
            var result=_userService.GetByEmail(email);
            if (_userService.GetByEmail(email).Data != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IResult PasswordReset(string email)
        {
            var result=_userService.GetByEmail(email);
            if (result.Data==null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            string resetToken= _tokenHelper.CreateRandomToken();

            result.Data.PasswordResetToken=resetToken;
            result.Data.ResetTokenExpire=DateTime.Now.AddDays(1);
            _userService.Update(result.Data);

            _mailService.SendPasswordResetMailAsync(email, result.Data.Id.ToString(), resetToken.UrlEncode());

            return new SuccessResult(Messages.ResetPassword);
        }

        public IResult VerifyResetToken(string resetToken, string userId)
        {
            var result = _userService.GetById(Convert.ToInt32(userId));
            if (result.Data == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            resetToken = resetToken.UrlDecode();

            if (result.Data.PasswordResetToken==resetToken && result.Data.ResetTokenExpire>DateTime.Now)
            {
                return new SuccessResult();
            }
            else
                return new ErrorResult(Messages.InvalidToken);

        }

        public IResult UpdatePassword(ResetPasswordDto resetPassword)
        {
            var result = VerifyResetToken(resetPassword.ResetToken, resetPassword.UserId.ToString());
            if (result.Success)
            {
                var updateUser = _userService.GetById(resetPassword.UserId);
                if (updateUser != null)
                {
                    byte[] passwordHash;
                    HashingHelper.CreatePasswordHash(resetPassword.Password, out passwordHash);
                    updateUser.Data.PasswordHash=passwordHash;
                    updateUser.Data.PasswordResetToken = null;
                    updateUser.Data.ResetTokenExpire = null;
                    _userService.Update(updateUser.Data);
                    return new SuccessResult(Messages.PasswordUpdate);
                }
                return new ErrorResult(Messages.UserNotFound);

            }
            return new ErrorResult(result.Message);
        }
    }
}
