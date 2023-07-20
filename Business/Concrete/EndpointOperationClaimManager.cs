using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class EndpointOperationClaimManager : IEndpointOperationClaimService
    {
        private readonly IEndpointOperationClaimDal _endpointOperationClaimDal;

        public EndpointOperationClaimManager(IEndpointOperationClaimDal endpointOperationClaimDal)
        {
            _endpointOperationClaimDal = endpointOperationClaimDal;
        }

        public IResult Add(EndpointOperationClaim endpointOperationClaim)
        {
            _endpointOperationClaimDal.Add(endpointOperationClaim);
            return new SuccessResult();
        }

        public IResult Delete(EndpointOperationClaim endpointOperationClaim)
        {
            _endpointOperationClaimDal.Delete(endpointOperationClaim);
            return new SuccessResult();
        }

        public IResult Update(EndpointOperationClaim endpointOperationClaim)
        {
            _endpointOperationClaimDal.Update(endpointOperationClaim);
            return new SuccessResult();
        }
    }
}
