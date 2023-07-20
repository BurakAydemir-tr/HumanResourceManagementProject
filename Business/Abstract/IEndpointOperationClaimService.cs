using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEndpointOperationClaimService
    {
        IResult Add(EndpointOperationClaim endpointOperationClaim);
        IResult Update(EndpointOperationClaim endpointOperationClaim);
        IResult Delete(EndpointOperationClaim endpointOperationClaim);
    }
}
