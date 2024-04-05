using BusinessAccessLayer.DTOs;
using DataAccessLayer.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Validators
{
    public class UserSignUpDTOValidator : AbstractValidator<UserSignUpDTO>
    {
        private readonly IUserRepository _userRepository;

        public UserSignUpDTOValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            var errorMessage = "The '{PropertyName}' field cannot be empty.";

            RuleFor(user => user.UserEmail)
                .NotEmpty().WithMessage(errorMessage)
                .EmailAddress().WithMessage("Invalid email format.")
                .MustAsync((user, email, cancellationToken) => BeUniqueEmail(user.UserEmail)).WithMessage("Email is already in use.");

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

        public async Task<bool> BeUniqueEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            return user == null;
        }
    }

}
