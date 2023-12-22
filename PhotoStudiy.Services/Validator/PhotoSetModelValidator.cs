using FluentValidation;
using PhotoStudiy.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Validator
{
    public class PhotoSetModelValidator : AbstractValidator<PhotoSetModel>
    {
        public PhotoSetModelValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(2, 40).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(2, 200).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(1, 9).WithMessage(MessageForValidation.LengthMessage);
        }
    }
}
