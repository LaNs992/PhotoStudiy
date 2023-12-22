﻿using FluentValidation;
using PhotoStudiy.Services.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStudiy.Services.Validator
{
    public class PhotographModelValidator : AbstractValidator<PhotographModel>
    {
        public PhotographModelValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(2, 40).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(2, 50).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
                .NotNull().WithMessage(MessageForValidation.DefaultMessage)
                .Length(9, 9).WithMessage(MessageForValidation.LengthMessage);
        }
    }
}
