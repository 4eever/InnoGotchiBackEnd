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

            var errorMessage = "Поле '{PropertyName}' не может быть пустым.";

            RuleFor(farm => farm.FarmName)
                .NotEmpty().WithMessage(errorMessage)
                .MustAsync((farm, FarmName, cancellationToken) => FarmNameUnique(FarmName)).WithMessage("Ферма с таким именем уже существует.");
            RuleFor(farm => farm.UserId)
                .NotEmpty().WithMessage(errorMessage)
                .MustAsync((farm, UserId, cancellationToken) => UniqieFarm(UserId)).WithMessage("У пользователя уже есть ферма.");
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
