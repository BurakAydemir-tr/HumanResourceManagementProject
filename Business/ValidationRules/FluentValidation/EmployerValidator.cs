using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class EmployerValidator:AbstractValidator<Employer>
    {
        public EmployerValidator()
        {
            RuleFor(c=>c.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(c => c.CompanyName).NotEmpty().NotNull();
            RuleFor(c => c.WebAddress).NotEmpty().NotNull();
            RuleFor(c => c.PhoneNumber).NotEmpty().NotNull().MaximumLength(11);
            RuleFor(c => c.CompanyName).MinimumLength(3);
            RuleFor(c => c.WebAddress).Matches(@"^(https?:\/\/)?www\.?[a-zA-Z0-9]+\.[a-zA-Z]{2,}$");
        }
    }
}
