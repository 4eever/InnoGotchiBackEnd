using BusinessAccessLayer.DTOs;
using DataAccessLayer.Repositories;
using FluentValidation;

namespace BusinessAccessLayer.Validators
{
    public class UserLogInDTOValidator : AbstractValidator<UserLogInDTO>
    {
        private readonly IUserRepository _userRepository;

        public UserLogInDTOValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            var errorMessage = "Поле '{PropertyName}' не может быть пустым.";

            RuleFor(user => user.UserEmail)
                .NotEmpty().WithMessage(errorMessage)
                .EmailAddress().WithMessage("Некорректный формат электронной почты.")
                .MustAsync((user, email, cancellationToken) => BeUniqueEmail(user.UserEmail)).WithMessage("Пользователя с таким email не существует");

            RuleFor(user => user.UserPassword)
                .NotEmpty().WithMessage(errorMessage)
                .MinimumLength(8).WithMessage("Пароль должен содержать как минимум 8 символов.")
                .MustAsync((user, password, cancellationToken) => BeCorrectPassword(user.UserPassword)).WithMessage("Неверный пароль.");
        }

        private async Task<bool> BeUniqueEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            return user != null;
        }

        private async Task<bool> BeCorrectPassword(string password)
        {
            var user = await _userRepository.GetUserByEmail(password);
            return user == null;
        }
    }

}
