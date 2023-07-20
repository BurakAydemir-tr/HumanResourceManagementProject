using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class JobExperienceManager : IJobExperienceService
    {
        private readonly IJobExperienceDal _jobExperienceDal;

        public JobExperienceManager(IJobExperienceDal jobExperienceDal)
        {
            _jobExperienceDal = jobExperienceDal;
        }

        public IResult Add(JobExperienceDto jobExperienceDto)
        {
            JobExperience jobExperience = new JobExperience();
            jobExperience.Id = jobExperienceDto.Id;
            jobExperience.CVId = jobExperienceDto.CVId;
            jobExperience.CompanyName = jobExperienceDto.CompanyName;
            jobExperience.Position = jobExperienceDto.Position;
            jobExperience.StartDate = jobExperienceDto.StartDate;
            jobExperience.EndDate = jobExperienceDto.EndDate;

            _jobExperienceDal.Add(jobExperience);
            return new SuccessResult(Messages.JobExperienceAdded);
        }

        public IResult Delete(int id)
        {
            var result = _jobExperienceDal.Get(x => x.Id == id);
            _jobExperienceDal.Delete(result);
            return new SuccessResult(Messages.JobExperienceDeleted);
        }

        public IDataResult<List<JobExperience>> GetAllByCVId(int id)
        {
            return new SuccessDataResult<List<JobExperience>>(_jobExperienceDal
                .GetAll(x => x.CVId == id).OrderByDescending(x => x.EndDate ?? DateTime.MaxValue).ToList(),
            Messages.JobExperienceListedByCandidate);
        }

        public IDataResult<JobExperience> GetById(int id)
        {
            return new SuccessDataResult<JobExperience>(_jobExperienceDal.Get(x => x.Id == id));
        }

        public IResult Update(JobExperienceDto jobExperienceDto)
        {
            JobExperience jobExperience = new JobExperience();
            jobExperience.Id = jobExperienceDto.Id;
            jobExperience.CVId = jobExperienceDto.CVId;
            jobExperience.CompanyName = jobExperienceDto.CompanyName;
            jobExperience.Position = jobExperienceDto.Position;
            jobExperience.StartDate = jobExperienceDto.StartDate;
            jobExperience.EndDate = jobExperienceDto.EndDate;


            _jobExperienceDal.Update(jobExperience);
            return new SuccessResult(Messages.JobExperienceUpdated);
        }
    }
}
