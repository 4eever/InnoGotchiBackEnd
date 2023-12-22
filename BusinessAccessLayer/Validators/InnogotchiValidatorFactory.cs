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
    public class InnogotchiValidatorFactory : IInnogotchiValidatorFactory
    {
        private readonly IInnogotchiRepository _innogotchiRepository;

        public InnogotchiValidatorFactory(IInnogotchiRepository innogotchiRepository)
        {
            _innogotchiRepository = innogotchiRepository;
        }

        public IValidator<T> GetValidator<T>()
        {
            if (typeof(T) == typeof(InnogotchiCreateDTO))
            {
                return new InnogotchiCreateDTOValidator(_innogotchiRepository) as IValidator<T>;
            }
            return null;
        }
    }
}
