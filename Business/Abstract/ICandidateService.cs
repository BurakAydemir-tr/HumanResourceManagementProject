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
    public interface ICandidateService
    {
        IDataResult<List<Candidate>> GetAll();
        IDataResult<Candidate> GetById(int id);
        IDataResult<Candidate> GetByEmail(string email);
        IResult Add(Candidate candidate);
    }
}
