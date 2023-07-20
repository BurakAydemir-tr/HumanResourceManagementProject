using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IUserOperationClaimService _userOperationClaimService;
        private readonly IEndpointService _endpointService;

        public UserManager(IUserDal userDal, IUserOperationClaimService userOperationClaimService, IEndpointService endpointService)
        {
            _userDal = userDal;
            _userOperationClaimService = userOperationClaimService;
            _endpointService = endpointService;
        }

        public IResult AssignOperationClaimToUser(int userId, int[] operationClaimId)
        {
            var user=_userDal.Get(u=>u.Id == userId);
            if (user != null)
            {
                var userOperationClaims=GetListUserOperationClaims(user.Id);
                _userOperationClaimService.DeleteAll(userOperationClaims.Data);
                foreach (var id in operationClaimId)
                {
                    _userOperationClaimService.Add(new UserOperationClaimDto
                    {
                        UserId = user.Id,
                        OperationClaimId = id
                    });
                }
                return new SuccessResult("Roller kullanıcıya atandı.");
            }
            return new ErrorResult(Messages.UserNotFound);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<User> GetByEmail(string email)
        {
            return new SuccessDataResult<User>(_userDal.Get(x=>x.Email==email));
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(u=>u.Id==id));
        }

        public IDataResult<User> GetByRefreshToken(string refreshToken)
        {
            return new SuccessDataResult<User>(_userDal.Get(x=>x.RefreshToken==refreshToken));
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IDataResult<List<UserOperationClaim>> GetListUserOperationClaims(int userId)
        {
            var result = _userOperationClaimService.GetByUserId(userId);
            if (result.Success)
            {
                return new SuccessDataResult<List<UserOperationClaim>>(result.Data);
            }
            return result;
        }

        public IResult HasOperationClaimPermissionToEndpoint(string email, string code)
        {
            var user=_userDal.Get(x=>x.Email==email);
            if (user == null)
                return new ErrorResult();
            var userRoles=GetClaims(user);

            if (!userRoles.Data.Any())
                return new ErrorResult();

            var endpoint=_endpointService.GetByCode(code);
            var endpointOperationClaims = _endpointService.GetClaims(endpoint.Data);
            if (endpoint == null) return new ErrorResult();

            foreach (var userRole in userRoles.Data)
            {
                //if (endpointOperationClaims.Data.Contains(userRole))
                //{
                //    return new SuccessResult();
                //}
                foreach (var endpointRole in endpointOperationClaims.Data)
                {
                    if (userRole.Name == endpointRole.Name)
                        return new SuccessResult();
                }
            }
            return new ErrorResult();
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdate);
        }

        public IResult UpdateRefreshToken(User user, string refreshToken, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddMinutes(addOnAccessTokenDate);
                _userDal.Update(user);
                return new SuccessResult();
            }
            else
                return new ErrorResult(Messages.UserNotFound);

        }
    }
}
