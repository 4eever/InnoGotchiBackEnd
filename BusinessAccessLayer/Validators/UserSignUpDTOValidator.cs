using BusinessAccessLayer.DTOs;
using DataAccessLayer.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
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

            var errorMessage = "Поле '{PropertyName}' не может быть пустым.";

            RuleFor(user => user.UserEmail)
                .NotEmpty().WithMessage(errorMessage)
                .EmailAddress().WithMessage("Некорректный формат электронной почты.")
                .MustAsync((user, email, cancellationToken) => BeUniqueEmail(user.UserEmail)).WithMessage("Email уже используется.");

            RuleFor(user => user.UserPassword)
                .NotEmpty().WithMessage(errorMessage)
                .MinimumLength(8).WithMessage("Пароль должен содержать как минимум 8 символов.");

            RuleFor(user => user.UserFirstName)
                .NotEmpty().WithMessage(errorMessage)
                .Matches("^[a-zA-Z]+$").WithMessage("Поле 'UserFirstName' должно содержать только буквы.");

            RuleFor(user => user.UserLastName)
                .NotEmpty().WithMessage(errorMessage)
                .Matches("^[a-zA-Z]+$").WithMessage("Поле 'UserLastName' должно содержать только буквы.");
        }

        public async Task<bool> BeUniqueEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            return user == null;
        }
    }

}
