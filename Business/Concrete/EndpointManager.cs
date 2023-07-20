using Business.Abstract;
using Core.Sevices.AuthorizeService;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class EndpointManager : IEndpointService
    {
        private readonly IEndpointDal _endpointDal;
        private readonly IApplicationService _applicationService;
        private readonly IOperationClaimService _operationClaimService;
        private readonly IEndpointOperationClaimService _endpointOperationClaimService;

        public EndpointManager(IEndpointDal endpointDal, IApplicationService applicationService, IOperationClaimService operationClaimService, IEndpointOperationClaimService endpointOperationClaimService)
        {
            _endpointDal = endpointDal;
            _applicationService = applicationService;
            _operationClaimService = operationClaimService;
            _endpointOperationClaimService = endpointOperationClaimService;
        }

        public IResult Add(Endpoint endpoint)
        {
            _endpointDal.Add(endpoint);
            return new SuccessResult();
        }

        public IResult AssignRoleEndpoint(int[] operationClaimId, string code, Type type)
        {
            var endpoint = _endpointDal.GetAllByInclude(e => e.Code == code,
                        include: c => c.Include(x => x.EndpointOperationClaims)).FirstOrDefault();
            if (endpoint == null)
            {
                string menu = code.Substring(0, code.IndexOf("."));
                var action = _applicationService.GetAuthorizeDefinitionEndPoints(type).
                    FirstOrDefault(e => e.Name == menu)?.Actions.FirstOrDefault(a => a.Code == code);
                if (action != null)
                {
                    _endpointDal.Add(new Endpoint()
                    {
                        Code = action.Code,
                        Menu = menu,
                        ActionType = action.ActionType,
                        HttpType = action.HttpType,
                        Definition = action.Definition,
                    });
                    endpoint = _endpointDal.GetAllByInclude(e => e.Code == code, 
                        include: c=>c.Include(x=>x.EndpointOperationClaims)).First();
                }
                else
                    return new ErrorResult("Endpoint bulunamadı.");
            }

            if (endpoint.EndpointOperationClaims.Any())
            {
                foreach (var endpointOperationClaim in endpoint.EndpointOperationClaims)
                {
                    _endpointOperationClaimService.Delete(endpointOperationClaim);
                }
            }
            

            for (int i = 0; i < operationClaimId.Length; i++)
            {
                _endpointOperationClaimService.Add(new EndpointOperationClaim()
                {
                    EndpointsId = endpoint.Id,
                    OperationClaimsId = operationClaimId[i],
                });
            }


            return new SuccessResult("Roller Endpointlere atandı.");
        }

        public IDataResult<Endpoint> GetByCode(string code)
        {
            return new SuccessDataResult<Endpoint>(_endpointDal.GetAllByInclude(x=>x.Code==code,
                include: e=>e.Include(a=>a.EndpointOperationClaims)).First());
        }

        public IDataResult<List<OperationClaim>> GetClaims(Endpoint endpoint)
        {
            var result=_endpointDal.GetClaims(endpoint);
            if (result!=null)
            {
                return new SuccessDataResult<List<OperationClaim>>(result);
            }
            return new ErrorDataResult<List<OperationClaim>>("Endpoint e ait bir role bulunamamıştır.");
        }

        public IResult Update(Endpoint endpoint)
        {
            _endpointDal.Update(endpoint);
            return new SuccessResult();
        }
    }
}
