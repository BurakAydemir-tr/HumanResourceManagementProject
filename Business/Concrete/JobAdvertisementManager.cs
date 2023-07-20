using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class JobAdvertisementManager : IJobAdvertisementService
    {
        private readonly IJobAdvertisementDal _jobAdvertisementDal;

        public JobAdvertisementManager(IJobAdvertisementDal jobAdvertisementDal)
        {
            _jobAdvertisementDal = jobAdvertisementDal;
        }

        public IResult Add(JobAdvertisement jobAdvertisement)
        {
            _jobAdvertisementDal.Add(jobAdvertisement);
            return new SuccessResult(Messages.JobAdvertisementAdded);
        }

        public IResult Delete(int id)
        {
            var result=_jobAdvertisementDal.Get(x=>x.Id == id);
            _jobAdvertisementDal.Delete(result);
            return new SuccessResult(Messages.JobAdvertisementDeleted);
        }

        public IDataResult<List<JobAdvertisement>> GetAll()
        {
            return new SuccessDataResult<List<JobAdvertisement>>(_jobAdvertisementDal.GetAll(),
                Messages.JobAdvertisementListed);
        }

        public IDataResult<List<JobAdvertisement>> GetAllByActive()
        {
            return new SuccessDataResult<List<JobAdvertisement>>(_jobAdvertisementDal.GetAll(x=>x.IsActive==true),
                Messages.JobAdvertisementListed);
        }

        public IDataResult<List<JobAdvertisement>> GetAllByActiveOrderByDate()
        {
            return new SuccessDataResult<List<JobAdvertisement>>(
                _jobAdvertisementDal.GetAll(x=>x.IsActive==true).OrderByDescending(x=>x.DeadlineDate).ToList(), Messages.JobAdvertisementListed);
        }

        public IDataResult<List<JobAdvertisement>> GetAllByEmployerIdIsActive(int employerId)
        {
            return new SuccessDataResult<List<JobAdvertisement>>(_jobAdvertisementDal
                .GetAll(x=>x.EmployerId==employerId && x.IsActive==true),
                Messages.JobAdvertisementListed);
        }

        public IDataResult<JobAdvertisement> GetById(int id)
        {
            return new SuccessDataResult<JobAdvertisement>(_jobAdvertisementDal.Get(x=>x.Id==id),
                Messages.JobAdvertisementGet);
        }

        public IResult Update(JobAdvertisement jobAdvertisement)
        {
            _jobAdvertisementDal.Update(jobAdvertisement);
            return new SuccessResult(Messages.JobAdvertisementUpdated);
        }
    }
}
