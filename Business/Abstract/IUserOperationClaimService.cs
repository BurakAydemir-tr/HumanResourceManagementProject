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
    public interface IUserOperationClaimService
    {
        IResult Add(UserOperationClaimDto userOperationClaimDto);
        IResult Update(UserOperationClaimDto userOperationClaimDto);
        IResult Delete(UserOperationClaimDto userOperationClaimDto);
        IResult DeleteAll(List<UserOperationClaim> userOperationClaims);
        IDataResult<List<UserOperationClaim>> GetByUserId(int userId);
        IDataResult<List<UserOperationClaim>> GetByOperationClaimId(int operationClaimId);
    }
}
