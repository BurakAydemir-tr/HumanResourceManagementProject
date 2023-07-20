using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class JobPosition : IEntity
    {
        public int Id { get; set; }
        public string Position { get; set; }


        public List<JobAdvertisement> JobAdvertisements { get; set; }
    }
}
