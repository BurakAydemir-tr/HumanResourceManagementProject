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
    public interface IOperationClaimService
    {
        IDataResult<List<OperationClaim>> GetAll();
        IDataResult<OperationClaim> GetById(int id);
        IDataResult<OperationClaim> GetByName(string name);
        IResult Add(OperationClaimDto operationClaimDto);
        IResult Delete(int id);
        IResult Update(OperationClaimDto operationClaimDto);
    }
}
