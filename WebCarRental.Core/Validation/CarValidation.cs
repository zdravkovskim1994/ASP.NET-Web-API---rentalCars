using System;
using FluentValidation;
using WebCarRental.Core.Models;

namespace WebCarRental.Core.Validation
{
    public class CarValidation : AbstractValidator<Car>
    {
        public CarValidation()
        {
            RuleFor(x => x.TagNumber).NotEmpty().WithMessage("Car-TagNumber is required!");
            RuleFor(x => x.Model).NotEmpty().WithMessage("Model name is required!");
            RuleFor(x => x.CarYear).NotEmpty().WithMessage("Car-year is required!");
            //RuleFor(x => x.AirConditioner).NotEmpty().WithMessage("Air conditioner is required!");
            RuleFor(x => x.Daily).NotEmpty().WithMessage("Daily is required!");
            RuleFor(x => x.Monthly).NotEmpty().WithMessage("Monthly is required!");

            RuleFor(x => x.TagNumber).Must(TagNumberLong).WithMessage("Tag number must be equal 10");


        }

        private bool TagNumberLong(string number)
        {
            if (string.IsNullOrWhiteSpace(number)) return false;

            if (number.Length > 11)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
