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
    public interface ITechnologyService
    {
        IDataResult<Technology> GetById(int id);
        IDataResult<List<Technology>> GetAllByCVId(int id);
        IResult Add(TechnologyDto technologyDto);
        IResult Update(TechnologyDto technologyDto);
        IResult Delete(int id);
    }
}
