using FluentValidation.Results;
using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infraestructure.DataAccess;
using TechLibrary.Api.Infraestructure.Security.Cryptography;
using TechLibrary.Api.Infraestructure.Security.Tokens.Access;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCases.Users.Register;

public class ResisterUserUseCase
{
    public ResponseRegisteredUserJson Execute(RequestUserJson request)
    {

        var dbContext = new TechLibraryDbContext();
        Validate(request, dbContext);

        var crypt = new BCryptAlgorithm();

        var entity = new User
        {
            Email = request.Email,
            Name = request.Name,
            Password = crypt.HashPassword(request.Password)
        };

        dbContext.Users.Add(entity);
        dbContext.SaveChanges();

        var tokenGenerator = new JwtTokenGenerator();

        return new ResponseRegisteredUserJson
        {
            Name = entity.Name,
            AcessToken = tokenGenerator.Generate(entity)
        };
    }

    private void Validate(RequestUserJson request, TechLibraryDbContext dbContext)
    {
        var validator = new RegistryUserValidator();
        var result = validator.Validate(request);

        var existUserWithEmail = dbContext.Users.Any(user => user.Email.Equals(request.Email));
        if(existUserWithEmail)
            result.Errors.Add(new ValidationFailure("E-mail", "E-mail já cadastrado."));

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidatioException(errorMessages);
        } 
    }
}
