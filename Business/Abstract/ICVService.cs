using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICVService
    {
        IDataResult<CV> GetById(int id);
        IDataResult<List<CV>> GetByCandidateId(int candidateId);
        Task<IResult> Add(CVDto cVDto);
        Task<IResult> Update(CVDto cVDto);
        IResult Delete(int id);
    }
}
