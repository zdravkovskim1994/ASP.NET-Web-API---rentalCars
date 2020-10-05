using System;
using FluentValidation;
using WebCarRental.Core.Models;

namespace WebCarRental.Core.Validation
{
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(x => x.DrvLicNumber).NotEmpty().WithMessage("DrvLicNumber is required");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required");
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required!");

        }
    }
}
