using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IEndpointDal : IEntityRepository<Endpoint>
    {
        List<OperationClaim> GetClaims(Endpoint endpoint);
    }
}
