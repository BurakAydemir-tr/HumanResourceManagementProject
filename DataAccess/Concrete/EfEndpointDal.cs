using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class EfEndpointDal : EfEntityRepositoryBase<Endpoint, HrmsContext>, IEndpointDal
    {
        public List<OperationClaim> GetClaims(Endpoint endpoint)
        {
            using (var context=new HrmsContext())
            {
                var result=from operationClaim in context.OperationClaims
                           join endpointOperationclaim in context.EndpointOperationClaim
                           on operationClaim.Id equals endpointOperationclaim.OperationClaimsId
                           where endpointOperationclaim.EndpointsId == endpoint.Id
                           select new OperationClaim { Id= operationClaim.Id, Name= operationClaim.Name };
                return result.ToList();
            }
        }
    }
}
