using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IJobExperienceService
    {
        IDataResult<JobExperience> GetById(int id);
        IDataResult<List<JobExperience>> GetAllByCVId(int id);
        IResult Add(JobExperienceDto jobExperienceDto);
        IResult Update(JobExperienceDto jobExperienceDto);
        IResult Delete(int id);
    }
}
