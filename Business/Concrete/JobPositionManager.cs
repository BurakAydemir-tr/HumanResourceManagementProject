using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class JobPositionManager : IJobPositionService
    {
        private readonly IJobPositionDal _jobPositionDal;

        public JobPositionManager(IJobPositionDal jobPositionDal)
        {
            _jobPositionDal = jobPositionDal;
        }

        public IResult Add(JobPosition jobPosition)
        {
            IResult result = BusinessRules.Run(CheckJobPositionExist(jobPosition.Position));
            if (result!=null)
            {
                return result;
            }
            _jobPositionDal.Add(jobPosition);
            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            var result=_jobPositionDal.Get(x=>x.Id == id);
            if (result!=null)
            {
                _jobPositionDal.Delete(result);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IDataResult<List<JobPosition>> GetAll()
        {
            return new SuccessDataResult<List<JobPosition>>(_jobPositionDal.GetAll());
        }

        public IDataResult<JobPosition> GetById(int id)
        {
            return new SuccessDataResult<JobPosition>(_jobPositionDal.Get(x=>x.Id==id));
        }

        public IResult Update(JobPosition jobPosition)
        {
            IResult result = BusinessRules.Run(CheckJobPositionExist(jobPosition.Position));
            if (result != null)
            {
                return result;
            }
            _jobPositionDal.Update(jobPosition);
            return new SuccessResult();
        }

        /*--- İŞ KURALLARI BURDA BAŞLIYOR ---*/
        private IResult CheckJobPositionExist(string position)
        {
            var result = _jobPositionDal.Get(x => x.Position == position);
            if (result != null)
            {
                return new ErrorResult(Messages.PositionAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
