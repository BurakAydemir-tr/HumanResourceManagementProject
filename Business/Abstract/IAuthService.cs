using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<Candidate> RegisterByCandidate(CandidateForRegisterDto candidateForRegisterDto);
        IDataResult<Employer> RegisterByEmployer(EmployerForRegisterDto employerForRegisterDto);
        IDataResult<Candidate> LoginByCandidate(CandidateForLoginDto candidateForLoginDto);
        IDataResult<Employer> LoginByEmployer(EmployerForLoginDto employerForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
        IDataResult<AccessToken> RefreshTokenLogin(string refreshToken);
        IResult PasswordReset(string email);
        IResult VerifyResetToken(string resetToken, string userId);
        IResult UpdatePassword(ResetPasswordDto resetPassword);
    }
}
