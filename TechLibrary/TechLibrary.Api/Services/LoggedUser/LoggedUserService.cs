using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infraestructure.DataAccess;

namespace TechLibrary.Api.Services.LoggedUser;

public class LoggedUserService (HttpContext httpContext)
{
    private readonly HttpContext _httpContext = httpContext;

    public User GetUser(TechLibraryDbContext dbContext)
    {
        var authorization = _httpContext.Request.Headers.Authorization.ToString();
        var token = authorization["Bearer ".Length..].Trim();

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(
            claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
        var userId = Guid.Parse(identifier);

        return dbContext.Users.First(u => u.Id == userId);
    }
}
