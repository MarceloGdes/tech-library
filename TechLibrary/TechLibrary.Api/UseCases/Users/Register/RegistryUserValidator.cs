using FluentValidation;
using TechLibrary.Communication.Requests;

namespace TechLibrary.Api.UseCases.Users.Register;

//Classe de validação, herdando metodos da biblioteca Fluent Validation
public class RegistryUserValidator : AbstractValidator<RequestUserJson>
{
    public RegistryUserValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage("O nome é obrigatório");
        RuleFor(request => request.Email).EmailAddress().WithMessage("O e-mail não é valido");
        RuleFor(request => request.Name).NotEmpty().WithMessage("A senha é obrigatória");
        When(request => string.IsNullOrEmpty(request.Password) == false, () =>
        {
            RuleFor(request => request.Password.Length)
            .GreaterThanOrEqualTo(6).WithMessage("A senha deve ter mais que 6 caracteres");
        });

    }
}
