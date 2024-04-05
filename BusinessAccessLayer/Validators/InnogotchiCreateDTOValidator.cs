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

            var errorMessage = "The '{PropertyName}' field cannot be empty.";

            RuleFor(innogotchi => innogotchi.FarmId)
                .NotEmpty().WithMessage(errorMessage);

            RuleFor(innogotchi => innogotchi.InnogotchiName)
                .NotEmpty().WithMessage(errorMessage)
                .MustAsync((innogotchi, InnogotchiName, cancellationToken) => InnogotchiInuqie(InnogotchiName)).WithMessage("An Innogotchi by that name already exists.");

            RuleFor(innogotchi => innogotchi.BodyNumber)
                .NotEmpty().WithMessage(errorMessage)
                .InclusiveBetween(1, 5).WithMessage("Select a body");

            RuleFor(innogotchi => innogotchi.EyesNumber)
                .NotEmpty().WithMessage(errorMessage)
                .InclusiveBetween(1, 6).WithMessage("Select eyes");

            RuleFor(innogotchi => innogotchi.NoseNumber)
                .NotEmpty().WithMessage(errorMessage)
                .InclusiveBetween(1, 6).WithMessage("Select a nose");

            RuleFor(innogotchi => innogotchi.MouthNumber)
                .NotEmpty().WithMessage(errorMessage)
                .InclusiveBetween(1, 5).WithMessage("Select a mouth");
        }

        private async Task<bool> InnogotchiInuqie(string innohotchiName)
        {
            var innogotchi = await _innogotchiRepository.GetInnogotchiByName(innohotchiName);
            return innogotchi == null;
        }
    }
}
