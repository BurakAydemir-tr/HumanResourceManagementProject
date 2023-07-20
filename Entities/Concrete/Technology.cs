using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Technology : IEntity
    {
        public int Id { get; set; }
        public int CVId { get; set; }
        public string Name { get; set; }

        public CV CV { get; set; }
    }
}
