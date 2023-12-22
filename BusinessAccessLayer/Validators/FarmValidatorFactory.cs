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
    public class FarmValidatorFactory : IFarmValidatorFactory
    {
        private readonly IFarmRepository _farmRepository;

        public FarmValidatorFactory(IFarmRepository farmRepository)
        {
            _farmRepository = farmRepository;
        }

        public IValidator<T> GetValidator<T>()
        {
            if (typeof(T) == typeof(FarmCreateDTO))
            {
                return new FarmCreateDTOValidator(_farmRepository) as IValidator<T>;
            }
            return null;
        }

    }
}
