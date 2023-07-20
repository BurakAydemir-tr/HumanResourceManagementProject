using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class JobAdvertisement : IEntity
    {
        public int Id { get; set; }
        public int EmployerId { get; set; }
        public int JobPositionId { get; set; }
        public string JobDescription { get; set; }
        public float? SalaryMin { get; set; }
        public float? SalaryMax { get; set; }
        public int PositionNumber { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public bool IsActive { get; set; }


        public Employer Employer { get; set; }
        public JobPosition JobPosition { get; set; }
    }
}
