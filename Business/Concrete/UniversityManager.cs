using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UniversityManager : IUniversityService
    {
        private readonly IUniversityDal _universityDal;

        public UniversityManager(IUniversityDal universityDal)
        {
            _universityDal = universityDal;
        }

        public IResult Add(UniversityDto universityDto)
        {
            University university = new University();
            university.Id = universityDto.Id;
            university.CVId = universityDto.CVId;
            university.Name = universityDto.Name;
            university.Department = universityDto.Department;
            university.EducationDegree = universityDto.EducationDegree;
            university.StartYear = universityDto.StartYear;
            university.EndYear = universityDto.EndYear;

            _universityDal.Add(university);
            return new SuccessResult(Messages.UniversityAdded);
        }

        public IResult Delete(int id)
        {
            var result=_universityDal.Get(x=>x.Id == id);
            _universityDal.Delete(result);
            return new SuccessResult(Messages.UniversityDeleted);
        }

        public IDataResult<List<University>> GetAllByCVId(int id)
        {
            return new SuccessDataResult<List<University>>(_universityDal
                .GetAll(x=>x.CVId==id).OrderByDescending(x=>x.EndYear ?? DateTime.MaxValue).ToList(),
                Messages.UniversityListedByCandidate);
        }

        public IDataResult<University> GetById(int id)
        {
            return new SuccessDataResult<University>(_universityDal.Get(x=>x.Id==id));
        }

        public IResult Update(UniversityDto universityDto)
        {
            University university = new University();
            university.Id = universityDto.Id;
            university.CVId = universityDto.CVId;
            university.Name = universityDto.Name;
            university.Department = universityDto.Department;
            university.EducationDegree = universityDto.EducationDegree;
            university.StartYear = universityDto.StartYear;
            university.EndYear = universityDto.EndYear;

            _universityDal.Update(university);
            return new SuccessResult(Messages.UniversityUpdated);
        }
    }
}
