using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class JobAdvertisementDto
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
    }
}
