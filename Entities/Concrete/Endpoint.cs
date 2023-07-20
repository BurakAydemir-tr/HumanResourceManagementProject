using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Endpoint : IEntity
    {
        public int Id { get; set; }
        public string Menu { get; set; }
        public string ActionType { get; set; }
        public string HttpType { get; set; }
        public string Definition { get; set; }
        public string Code { get; set; }

        public ICollection<EndpointOperationClaim> EndpointOperationClaims { get; set; }
    }
}
