using FluentValidation;
using POS.Application.Dtos.Client.Request;

namespace POS.Application.Validators.Client
{
    public class ClientValidator : AbstractValidator<ClientRequestDto>
    {
        public ClientValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("El campo Nombre no puede ser nulo")
                .NotEmpty().WithMessage("El campo Nombre no puede ser vacío");
        }
    }
}
