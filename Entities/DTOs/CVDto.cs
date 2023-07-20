using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CVDto
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string Description { get; set; }
        public IFormFile? file { get; set; }
    }
}
