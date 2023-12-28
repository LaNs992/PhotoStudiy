using FluentValidation;
using PhotoStudiy.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Validator
{
    public class RecvisitModelValidator : AbstractValidator<RecvisitModel>
    {
        public RecvisitModelValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(2, 40).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(1, 200).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => (int)x.Amount)
  .InclusiveBetween(1, 200).WithMessage(MessageForValidation.InclusiveBetweenMessage);
        }
    }
}
