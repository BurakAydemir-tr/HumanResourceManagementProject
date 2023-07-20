using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Utilities.Business;
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
    public class CandidateManager : ICandidateService
    {
        private readonly ICandidateDal _candidateDal;

        public CandidateManager(ICandidateDal candidateDal)
        {
            _candidateDal = candidateDal;
        }

        public IResult Add(Candidate candidate)
        {
            //CandidateValidator validations = new CandidateValidator();
            //var validationResult = validations.Validate(candidate);
            //if (!validationResult.IsValid)
            //{
            //    return new ErrorResult(validationResult.Errors.ToList().ToString());
            //}

            IResult result = BusinessRules.Run(CheckOfNationalIdExist(candidate.NationalId),
                CheckEmailExist(candidate.Email));

            if (result != null)
            {
                return result;
            }

            _candidateDal.Add(candidate);
            return new SuccessResult(Messages.CandidateAdded);
        }

        public IDataResult<List<Candidate>> GetAll()
        {
            return new SuccessDataResult<List<Candidate>>(_candidateDal.GetAll());
        }

        public IDataResult<Candidate> GetById(int id)
        {
            return new SuccessDataResult<Candidate>(_candidateDal.Get(c=>c.Id==id));
        }

        public IDataResult<Candidate> GetByEmail(string email)
        {
            return new SuccessDataResult<Candidate>(_candidateDal.Get(x=>x.Email==email));
        }


        /*--- İŞ KURALLARI BURDA BAŞLIYOR ---*/
        private IResult CheckOfNationalIdExist(string nationalId)
        {
            var result=_candidateDal.Get(x=>x.NationalId==nationalId);
            if (result!=null)
            {
                return new ErrorResult(Messages.NationalIdAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckEmailExist(string email)
        {
            var result=_candidateDal.Get(x=>x.Email==email);
            if (result!=null)
            {
                return new ErrorResult(Messages.EmailAlreadyExists);
            }
            return new SuccessResult();
        }

        
    }
}
