using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Employer : User
    {
        public string CompanyName { get; set; }
        public string WebAddress { get; set; }
        public string PhoneNumber { get; set; }

        public List<JobAdvertisement> JobAdvertisements { get; set; }
    }
}
