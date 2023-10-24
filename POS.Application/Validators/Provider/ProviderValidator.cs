using FluentValidation;
using POS.Application.Dtos.Provider.Request;

namespace POS.Application.Validators.Provider
{
    public class ProviderValidator : AbstractValidator<ProviderRequestDto>
    {
        public ProviderValidator()
        {
            RuleFor(x => x.Name)
               .NotNull().WithMessage("El campo Nombre no puede ser nulo")
               .NotEmpty().WithMessage("El campo Nombre no puede ser vacío");
        }
    }
}
