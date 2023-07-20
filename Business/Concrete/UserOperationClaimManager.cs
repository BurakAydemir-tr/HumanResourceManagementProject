using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        public IResult Add(UserOperationClaimDto userOperationClaimDto)
        {
            UserOperationClaim userOperationClaim = new()
            {
                UserId = userOperationClaimDto.UserId,
                OperationClaimId = userOperationClaimDto.OperationClaimId,
            };
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult();
        }

        public IResult Delete(UserOperationClaimDto userOperationClaimDto)
        {
            UserOperationClaim userOperationClaim = new()
            {
                UserId = userOperationClaimDto.UserId,
                OperationClaimId = userOperationClaimDto.OperationClaimId,
            };
            _userOperationClaimDal.Delete(userOperationClaim);
            return new SuccessResult();
        }

        public IResult DeleteAll(List<UserOperationClaim> userOperationClaims)
        {
            foreach (var userOperationClaim in userOperationClaims)
            {
                _userOperationClaimDal.Delete(userOperationClaim);
            }
            return new SuccessResult();
        }

        public IDataResult<List<UserOperationClaim>> GetByOperationClaimId(int operationClaimId)
        {
            return new SuccessDataResult<List<UserOperationClaim>>(
                _userOperationClaimDal.GetAll(u => u.OperationClaimId == operationClaimId));
        }

        public IDataResult<List<UserOperationClaim>> GetByUserId(int userId)
        {
            return new SuccessDataResult<List<UserOperationClaim>>(
                _userOperationClaimDal.GetAll(u => u.UserId == userId));
        }

        public IResult Update(UserOperationClaimDto userOperationClaimDto)
        {
            UserOperationClaim userOperationClaim = new()
            {
                UserId = userOperationClaimDto.UserId,
                OperationClaimId = userOperationClaimDto.OperationClaimId,
            };
            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult();
        }
    }
}
