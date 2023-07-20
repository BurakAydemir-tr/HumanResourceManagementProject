using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class EndpointOperationClaim : IEntity
    {
        public int EndpointsId { get; set; }
        public int OperationClaimsId { get; set; }

        public Endpoint Endpoint { get; set; }
        public OperationClaim OperationClaim { get; set; }
    }
}
