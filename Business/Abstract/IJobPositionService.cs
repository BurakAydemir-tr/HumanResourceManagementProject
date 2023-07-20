using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IJobPositionService
    {
        IDataResult<List<JobPosition>> GetAll();
        IDataResult<JobPosition> GetById(int id);
        IResult Add(JobPosition jobPosition);
        IResult Delete(int id);
        IResult Update(JobPosition jobPosition);
    }
}
