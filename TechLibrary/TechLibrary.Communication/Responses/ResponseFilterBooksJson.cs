namespace TechLibrary.Communication.Responses;

public class ResponseFilterBooksJson
{   
    public ResponsePaginationJson Pagination { get; set; } = default!; //dafult! é um valor padrão que não pode ser nulo
    public List<ResponseBookJson> Books { get; set; } = [];
}
