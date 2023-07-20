using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class OperationClaim :IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<UserOperationClaim> UserOperationClaims { get; set; }
        public ICollection<EndpointOperationClaim> EndpointOperationClaims { get; set; }
    }
}
