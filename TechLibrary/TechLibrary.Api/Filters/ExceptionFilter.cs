using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.Filters;

// Implementa um filtro de exceção personalizado para o ASP.NET Core
public class ExceptionFilter : IExceptionFilter
{
    // Método chamado quando uma exceção é lançada durante a execução de uma ação
    public void OnException(ExceptionContext context)
    {
        // Verifica se a exceção é do tipo TechLibraryException
        if (context.Exception is TechLibraryException techLibraryException)
        {
            // Define o código de status HTTP com base na exceção específica
            context.HttpContext.Response.StatusCode = (int)techLibraryException.GetStatusCode();

            // Define o resultado da resposta com uma lista de mensagens de erro
            context.Result = new ObjectResult(new ResponseErrorMessagesJson
            {
                Errors = techLibraryException.GetErrorMessages()
            });
        }
        else
        {
            // Define o código de status HTTP como 500 (Erro Interno do Servidor) para exceções desconhecidas
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            // Define o resultado da resposta com uma mensagem de erro genérica
            context.Result = new ObjectResult(new ResponseErrorMessagesJson
            {
                Errors = new List<string> { "Erro desconhecido, solicite apoio do desenvolvimento." }
            });
        }
    }
}
