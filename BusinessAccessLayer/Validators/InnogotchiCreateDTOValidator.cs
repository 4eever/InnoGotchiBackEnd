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
    public class InnogotchiCreateDTOValidator : AbstractValidator<InnogotchiCreateDTO>
    {
        private readonly IInnogotchiRepository _innogotchiRepository;

        public InnogotchiCreateDTOValidator(IInnogotchiRepository innogotchiRepository)
        {
            _innogotchiRepository = innogotchiRepository;

            var errorMessage = "Поле '{PropertyName}' не может быть пустым.";

            RuleFor(innogotchi => innogotchi.FarmId)
                .NotEmpty().WithMessage(errorMessage);

            RuleFor(innogotchi => innogotchi.InnogotchiName)
                .NotEmpty().WithMessage(errorMessage)
                .MustAsync((innogotchi, InnogotchiName, cancellationToken) => InnogotchiInuqie(InnogotchiName)).WithMessage("Инноготчи с таким именем уже существует.");

            RuleFor(innogotchi =>innogotchi.BodyNumber)
                .NotEmpty().WithMessage(errorMessage)
                .InclusiveBetween(1, 5).WithMessage("Номер тела должен быть от 1 до 5");

            RuleFor(innogotchi => innogotchi.EyesNumber)
                .NotEmpty().WithMessage(errorMessage)
                .InclusiveBetween(1, 6).WithMessage("Номер глаз должен быть от 1 до 6");

            RuleFor(innogotchi => innogotchi.NoseNumber)
                .NotEmpty().WithMessage(errorMessage)
                .InclusiveBetween(1, 6).WithMessage("Номер носа должен быть от 1 до 6");

            RuleFor(innogotchi => innogotchi.MouthNumber)
                .NotEmpty().WithMessage(errorMessage)
                .InclusiveBetween(1, 5).WithMessage("Номер рта должен быть от 1 до 5");
        }

        private async Task<bool> InnogotchiInuqie(string innohotchiName)
        {
            var innogotchi = await _innogotchiRepository.GetInnogotchiByName(innohotchiName);
            return innogotchi == null;
        }
    }
}
