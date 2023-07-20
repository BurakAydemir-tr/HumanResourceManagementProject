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
    public interface IUniversityService
    {
        IDataResult<University> GetById(int id);
        IDataResult<List<University>> GetAllByCVId(int id);
        IResult Add(UniversityDto universityDto);
        IResult Update(UniversityDto universityDto);
        IResult Delete(int id);

    }
}
