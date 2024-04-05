using BusinessAccessLayer.DTOs;
using DataAccessLayer.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Validators
{
    public class FarmCreateDTOValidator : AbstractValidator<FarmCreateDTO>
    {
        private readonly IFarmRepository _farmRepository;
        public FarmCreateDTOValidator(IFarmRepository farmRepository)
        {
            _farmRepository = farmRepository;

            var errorMessage = "The '{PropertyName}' field cannot be empty.";

            RuleFor(farm => farm.FarmName)
                .NotEmpty().WithMessage(errorMessage)
                .MustAsync((farm, FarmName, cancellationToken) => FarmNameUnique(FarmName)).WithMessage("A farm with this name already exists.");
            RuleFor(farm => farm.UserId)
                .NotEmpty().WithMessage(errorMessage)
                .MustAsync((farm, UserId, cancellationToken) => UniqieFarm(UserId)).WithMessage("The user already has a farm.");
        }

        private async Task<bool> FarmNameUnique(string farmName)
        {
            var farm = await _farmRepository.GetFarmByName(farmName);
            return farm == null;
        }

        private async Task<bool> UniqieFarm(int userId)
        {
            var farm = await _farmRepository.GetFarmByUserId(userId);
            return farm == null;
        }
    }
}
