using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Validators
{
    public interface IFarmValidatorFactory
    {
        IValidator<T> GetValidator<T>();
    }
}
