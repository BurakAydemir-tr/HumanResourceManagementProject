using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEndpointService
    {
        IResult Add (Endpoint endpoint);
        IResult Update(Endpoint endpoint);
        IResult AssignRoleEndpoint(int[] operationClaimId, string code, Type type);
        IDataResult<Endpoint> GetByCode(string code);
        IDataResult<List<OperationClaim>> GetClaims(Endpoint endpoint);
    }
}
