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
            var errorMessage = "Поле '{PropertyName}' не может быть пустым.";

            RuleFor(user => user.UserEmail)
                .NotEmpty().WithMessage(errorMessage)
                .EmailAddress().WithMessage("Некорректный формат электронной почты.");

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
    }
}
