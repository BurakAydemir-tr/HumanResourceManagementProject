using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int id);
        IDataResult<User> GetByEmail(string email);
        IDataResult<User> GetByRefreshToken(string refreshToken);
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<List<UserOperationClaim>> GetListUserOperationClaims(int userId);
        IResult Update(User user);
        IResult UpdateRefreshToken(User user, string refreshToken, DateTime accessTokenDate, int addOnAccessTokenDate);

        IResult AssignOperationClaimToUser(int userId, int[] operationClaimId);
        IResult HasOperationClaimPermissionToEndpoint(string email, string code);

    }
}
