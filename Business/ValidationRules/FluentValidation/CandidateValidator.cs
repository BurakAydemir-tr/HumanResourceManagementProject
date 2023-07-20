using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CandidateValidator:AbstractValidator<Candidate>
    {
        public CandidateValidator()
        {
            RuleFor(c=>c.FirstName).NotEmpty();
            RuleFor(c=>c.LastName).NotEmpty();
            RuleFor(c=>c.Email).NotEmpty();
            RuleFor(c=>c.NationalId).NotEmpty();
            RuleFor(c => c.BirthDate).NotEmpty();
            RuleFor(c => c.FirstName).MinimumLength(3);
            RuleFor(c => c.LastName).MinimumLength(3);
            RuleFor(c => c.Email).EmailAddress();
            RuleFor(c => c.NationalId).Length(11);
        }
    }
}
