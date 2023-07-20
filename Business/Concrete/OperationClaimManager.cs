using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;

        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        public IResult Add(OperationClaimDto operationClaimDto)
        {
            IResult result = BusinessRules.Run(CheckClaimNameExists(operationClaimDto.Name));

            if (result != null)
            {
                return result;
            }

            OperationClaim operationClaim = new()
            { 
                Name = operationClaimDto.Name,
            };

            _operationClaimDal.Add(operationClaim);
            return new SuccessResult(Messages.OperationClaimAdded);
        }

        public IResult Delete(int id)
        {
            var result = _operationClaimDal.Get(x => x.Id == id);
            _operationClaimDal.Delete(result);
            return new SuccessResult(Messages.OperationClaimDeleted);
        }

        public IDataResult<List<OperationClaim>> GetAll()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll());
        }

        public IDataResult<OperationClaim> GetById(int id)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(x=> x.Id==id));
        }

        public IDataResult<OperationClaim> GetByName(string name)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(x => x.Name==name));
        }

        public IResult Update(OperationClaimDto operationClaimDto)
        {
            IResult result = BusinessRules.Run(CheckClaimNameExists(operationClaimDto.Name));

            if (result != null)
            {
                return result;
            }

            OperationClaim operationClaim = new() 
            { 
                Id = operationClaimDto.Id,
                Name = operationClaimDto.Name
            };

            _operationClaimDal.Update(operationClaim);
            return new SuccessResult(Messages.OperationClaimUpdated);
        }

        /*--- İŞ KURALLARI BURDA BAŞLIYOR ---*/
        private IResult CheckClaimNameExists(string name)
        {
            var claim = _operationClaimDal.Get(x => x.Name==name);
            if(claim != null)
                return new ErrorResult(Messages.OperationClaimAlreadyExists);
            return new SuccessResult();
        }
    }
}
