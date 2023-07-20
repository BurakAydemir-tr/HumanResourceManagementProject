using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEmployerService
    {
        IDataResult<List<Employer>> GetAll();
        IDataResult<Employer> GetById(int id);
        IDataResult<Employer> GetByEmail(string email);
        IResult Add(Employer employer);
    }
}
