using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class CV :IEntity
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string? Photo { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        #region Relations
        public Candidate Candidate { get; set; }
        public ICollection<University> Universities { get; set; }
        public ICollection<JobExperience> JobExperiences { get; set; }
        public ICollection<Technology> Technologies { get; set; }
        #endregion

    }
}
