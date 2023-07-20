using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IJobAdvertisementService
    {
        IDataResult<List<JobAdvertisement>> GetAll();
        IDataResult<List<JobAdvertisement>> GetAllByActive();
        IDataResult<List<JobAdvertisement>> GetAllByActiveOrderByDate();
        IDataResult<List<JobAdvertisement>> GetAllByEmployerIdIsActive(int employerId);
        IDataResult<JobAdvertisement> GetById(int id);
        IResult Add(JobAdvertisement jobAdvertisement);
        IResult Update(JobAdvertisement jobAdvertisement);
        IResult Delete(int id);
    }
}
