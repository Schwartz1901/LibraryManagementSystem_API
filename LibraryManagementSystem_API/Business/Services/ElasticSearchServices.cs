using LibraryManagementSystem_API.Business.Abstracts;
using LibraryManagementSystem_API.DataAccess;
using LibraryManagementSystem_API.DataAccess.Entities;
using LibraryManagementSystem_API.DataAccess.Repositories.Abstract;
using LibraryManagementSystem_API.DataAccess.Repositories.Concrete;
using Microsoft.EntityFrameworkCore;
using Nest;

namespace LibraryManagementSystem_API.Business.Services
{
    public class ElasticSearchServices : IELasticSearchServices
    {

        private readonly IElasticClient _elasticClient;
        private readonly LibraryDbContext _libraryDbContext;

        public ElasticSearchServices(IElasticClient elasticClient, LibraryDbContext libraryDbContext)
        {
            _elasticClient = elasticClient;
            _libraryDbContext = libraryDbContext;
        }

        public async Task IndexDataAsync()
        {
            var books = await _libraryDbContext.Book.ToListAsync();

            foreach (var book in books)
            {
               var indexResponse =  await _elasticClient.IndexDocumentAsync(book);

                if (indexResponse.IsValid)
                {
                    Console.WriteLine($"Index entity Id: {book.BookId}");
                }
                else
                {
                    Console.WriteLine($"Index error for Id {book.BookId} : {indexResponse.Result}");
                }
            }
        }

        public async Task<ISearchResponse<BookEntity>> SearchAsync(string query)
        {
            var searchResponse = await _elasticClient.SearchAsync<BookEntity>
                (
                s => s.Query
                    (q => q.MultiMatch
                        (m => m.Query(query)
                        .Fields(f => f
                            .Field(p => p.Name)
                            .Field(p => p.Description)
                            .Field(p => p.Author)
                            )
                        )
                    )
                );

            return searchResponse;
        }
    }
}
