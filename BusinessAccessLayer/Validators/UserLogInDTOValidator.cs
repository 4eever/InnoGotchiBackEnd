using BusinessAccessLayer.DTOs;
using DataAccessLayer.Repositories;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;

namespace BusinessAccessLayer.Validators
{
    public class UserLogInDTOValidator : AbstractValidator<UserLogInDTO>
    {
        private readonly IUserRepository _userRepository;

        private bool _isValidEmail=true;

        public UserLogInDTOValidator(IUserRepository userRepository, bool isValidEmail)
        {
            _userRepository = userRepository;
            _isValidEmail = isValidEmail;

            var errorMessage = "The '{PropertyName}' field cannot be empty.";

            RuleFor(user => user.UserEmail)
                .NotEmpty().WithMessage(errorMessage)
                .EmailAddress().WithMessage("Invalid email format.")
                .MustAsync((user, email, cancellationToken) => BeUniqueEmail(user.UserEmail)).WithMessage("User with this email does not exist.");

            RuleFor(user => user.UserPassword)
                .NotEmpty().WithMessage(errorMessage)
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MustAsync((user, password, cancellationToken) => BeCorrectPassword(user.UserEmail, user.UserPassword)).WithMessage("Incorrect password.")
                .When(_ => _isValidEmail);
        }

        private async Task<bool> BeUniqueEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if(user == null) _isValidEmail = false;

            return user != null;
        }

        private async Task<bool> BeCorrectPassword(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return false;
            }

            return user.UserPassword == password;
        }
    }

}
