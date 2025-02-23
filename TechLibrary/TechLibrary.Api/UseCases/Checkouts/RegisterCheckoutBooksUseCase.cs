using TechLibrary.Api.Domain.Entities;
using TechLibrary.Api.Infraestructure.DataAccess;
using TechLibrary.Api.Services.LoggedUser;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCases.Checkouts;


public class RegisterCheckoutBooksUseCase(LoggedUserService loggedUserSevice)
{
    private const int MAX_LOAN_DAYS = 7;
    private readonly LoggedUserService _loggedUserSevice = loggedUserSevice;
    public void Execute(Guid bookId)
    {
        var dbContext = new TechLibraryDbContext();

        Validate(dbContext, bookId);
        var user = _loggedUserSevice.GetUser(dbContext);

        var entity = new Checkout
        {
            UserId = user.Id,
            BookId = bookId,
            ExpectedReturnDate = DateTime.UtcNow.AddDays(MAX_LOAN_DAYS)
        };

        dbContext.Checkouts.Add(entity);

        dbContext.SaveChanges();
    }

    private void Validate(TechLibraryDbContext dbContext, Guid bookId)
    {
        var book = dbContext.Books.FirstOrDefault(b => b.Id == bookId);
        if (book is null)
            throw new NotFoundException("Livro não encontrado.");

        //Validação de livros emprestados
        var amountNotReturnedBooks = dbContext.Checkouts
                .Count(c => c.BookId == bookId && c.ReturnedDate == null);

        if (amountNotReturnedBooks == book.Amount)
            throw new ConflictException("Livro não disponível para empréstimo.");
    }
}
