using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infraestructure.DataAccess;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.UseCases.Books;

public class FilterBookUseCase
{
    private const int PAGE_SIZE = 10;
    public ResponseFilterBooksJson Execute(RequestFilterBooksJson request)
    {
        var dbContext = new TechLibraryDbContext();
        var totalCount = 0;

        var query = dbContext.Books.AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            query = query.Where(b => b.Title.Contains(request.Title));
            totalCount = dbContext.Books.Count(b => b.Title.Contains(request.Title));
        }
        else
        {
            totalCount = dbContext.Books.Count();
        }
            
        //Paginação.
        var books = query
            .OrderBy(book => book.Title).ThenBy(book => book.Author)
            .Skip(PAGE_SIZE * (request.PageNumber - 1))
            .Take(PAGE_SIZE)
            .ToList();

        return new ResponseFilterBooksJson
        {
            Pagination = new ResponsePaginationJson
            {
                PageNumber = request.PageNumber,
                TotalCount = totalCount
            },
            Books = books.Select(book => new ResponseBookJson
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author
            }).ToList()
        };
    }
}
