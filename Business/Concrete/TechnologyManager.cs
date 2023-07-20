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
    public class TechnologyManager : ITechnologyService
    {
        private readonly ITechnologyDal _technologyDal;

        public TechnologyManager(ITechnologyDal technologyDal)
        {
            _technologyDal = technologyDal;
        }

        public IResult Add(TechnologyDto technologyDto)
        {
            Technology technology = new Technology();
            technology.Id=technologyDto.Id;
            technology.CVId=technologyDto.CVId;
            technology.Name=technologyDto.Name;

            _technologyDal.Add(technology);
            return new SuccessResult(Messages.TechnologyAdded);
        }

        public IResult Delete(int id)
        {
            var result = _technologyDal.Get(x => x.Id == id);
            _technologyDal.Delete(result);
            return new SuccessResult(Messages.TechnologyDeleted);
        }

        public IDataResult<List<Technology>> GetAllByCVId(int id)
        {
            return new SuccessDataResult<List<Technology>>(_technologyDal
                .GetAll(x => x.CVId == id),
            Messages.TechnologyListedByCV);
        }

        public IDataResult<Technology> GetById(int id)
        {
            return new SuccessDataResult<Technology>(_technologyDal.Get(x => x.Id == id));
        }

        public IResult Update(TechnologyDto technologyDto)
        {
            Technology technology = new Technology();
            technology.Id = technologyDto.Id;
            technology.CVId = technologyDto.CVId;
            technology.Name = technologyDto.Name;

            _technologyDal.Update(technology);
            return new SuccessResult(Messages.TechnologyUpdated);
        }
    }
}
