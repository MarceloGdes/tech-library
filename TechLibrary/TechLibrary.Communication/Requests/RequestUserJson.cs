namespace TechLibrary.Communication.Requests;
public class RequestUserJson
{
    public string Name { get; set; } = string.Empty; //Deixa por padrão um string vazia, para não dar erro de null
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
}

