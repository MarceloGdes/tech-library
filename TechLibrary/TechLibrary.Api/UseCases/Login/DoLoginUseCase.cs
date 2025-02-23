using TechLibrary.Api.Infraestructure.DataAccess;
using TechLibrary.Api.Infraestructure.Security.Cryptography;
using TechLibrary.Api.Infraestructure.Security.Tokens.Access;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCases.Login;

public class DoLoginUseCase
{
    public ResponseRegisteredUserJson Execute(RequestLoginJson request)
    {
        var dbContext = new TechLibraryDbContext();
        var tokenGenerator = new JwtTokenGenerator();

        var entity = dbContext.Users.FirstOrDefault(user => user.Email.Equals(request.Email));
        if(entity == null)
        {
            throw new InvalidLoginException();
        }

        var crypt = new BCryptAlgorithm();
        var passwordIsValid = crypt.VerifyPassword(request.Password, entity);
        if(passwordIsValid == false)
            throw new InvalidLoginException();

        return new ResponseRegisteredUserJson
        {
            Name = entity.Name,
            AcessToken = tokenGenerator.Generate(entity)
        };
    }
}
