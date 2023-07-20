using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UniversityDto
    {
        public int Id { get; set; }
        public int CVId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public EducationDegree EducationDegree { get; set; }
        public DateTime StartYear { get; set; }
        public DateTime? EndYear { get; set; }
    }
}
