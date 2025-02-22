using Microsoft.AspNetCore.Mvc;
using TechLibrary.Api.UseCases.Users.Register;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)] //documentação de respostas
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register(RequestUserJson request)
    {
        try
        {
            var useCase = new ResisterUserUseCase();
            var response = useCase.Execute(request);

            return Created(string.Empty, response);
        }
        catch(TechLibraryException e)
        {
            return BadRequest(new ResponseErrorMessagesJson
            {
                Errors = e.GetErrorMessages()
            });
        }
        //catch
        //{
        //    return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorMessagesJson
        //    {
        //        Errors = ["Erro desconhecido, solicite apoio do desenvolvimento"]
        //    });
        //}
        
    }
}
