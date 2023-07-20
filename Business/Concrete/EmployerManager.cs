using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
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
    public class EmployerManager : IEmployerService
    {
        private readonly IEmployerDal _employerDal;

        public EmployerManager(IEmployerDal employerDal)
        {
            _employerDal = employerDal;
        }

        public IResult Add(Employer employer)
        {
            IResult result = BusinessRules.Run(CheckPhoneNumberExist(employer.PhoneNumber),
                CheckEmailExist(employer.Email),
                CheckEmailAndWebSiteDomainSame(employer.WebAddress,employer.Email));

            if (result!=null)
            {
                return result;
            }
            _employerDal.Add(employer);
            return new SuccessResult();
        }

        public IDataResult<List<Employer>> GetAll()
        {
            return new SuccessDataResult<List<Employer>>(_employerDal.GetAll());
        }

        public IDataResult<Employer> GetById(int id)
        {
            return new SuccessDataResult<Employer>(_employerDal.Get(e=>e.Id==id));
        }

        public IDataResult<Employer> GetByEmail(string email)
        {
            return new SuccessDataResult<Employer>(_employerDal.Get(e => e.Email == email));
        }


        /*--- İŞ KURALLARI BURDA BAŞLIYOR ---*/
        private IResult CheckPhoneNumberExist(string phoneNumber)
        {
            var result=_employerDal.Get(x=>x.PhoneNumber==phoneNumber);
            if (result!=null)
            {
                return new ErrorResult(Messages.PhoneNumberAlreadyExists);
            }
            return new SuccessResult();
        }
        
        private IResult CheckEmailExist(string email)
        {
            var result=_employerDal.Get(x=>x.Email==email);
            if (result!=null)
            {
                return new ErrorResult(Messages.EmailAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckEmailAndWebSiteDomainSame(string webAddress, string email)
        {
            int indexEmail = email.IndexOf("@");
            int indexWeb=webAddress.IndexOf(".");
            string emailDomain=email.Substring(indexEmail+1);
            string webSiteDomain=webAddress.Substring(indexWeb+1);

            if (emailDomain.Equals(webSiteDomain))
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.EmailAndWebsiteDifferent);
        }

        
    }
}
