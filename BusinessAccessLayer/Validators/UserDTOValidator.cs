using BusinessAccessLayer.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Validators
{
    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            var errorMessage = "The '{PropertyName}' field cannot be empty.";

            RuleFor(user => user.UserEmail)
                .NotEmpty().WithMessage(errorMessage)
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(user => user.UserPassword)
                .NotEmpty().WithMessage(errorMessage)
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

            RuleFor(user => user.UserFirstName)
                .NotEmpty().WithMessage(errorMessage)
                .Matches("^[a-zA-Z]+$").WithMessage("First Name field should contain only letters of the Latin alphabet.");

            RuleFor(user => user.UserLastName)
                .NotEmpty().WithMessage(errorMessage)
                .Matches("^[a-zA-Z]+$").WithMessage("Last Name field should contain only letters of the Latin alphabet.");
        }
    }
}
