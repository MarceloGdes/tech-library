using System.Net;

namespace TechLibrary.Exception;
//Exceptions personalizada, evitando apresentação de informações sensiveis em tempo de execução para quem está realizando a requisição
public abstract class TechLibraryException : SystemException
{
    public abstract List<string> GetErrorMessages();
    public abstract HttpStatusCode GetStatusCode();
}
