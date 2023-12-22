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
    public class UserValidatorFactory : IUserValidatorFactory
    {
        private readonly IUserRepository _userRepository;

        public UserValidatorFactory(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IValidator<T> GetValidator<T>()
        {
            if (typeof(T) == typeof(UserSignUpDTO))
            {
                return new UserSignUpDTOValidator(_userRepository) as IValidator<T>;
            }
            else if (typeof(T) == typeof(UserLogInDTO))
            {
                return new UserLogInDTOValidator(_userRepository) as IValidator<T>;
            }
            else if (typeof(T) == typeof(UserDTO))
            {
                return new UserDTOValidator() as IValidator<T>;
            }
            else
            return null;
        }
    }
}
