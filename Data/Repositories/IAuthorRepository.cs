using BlogAPI.Models;

namespace BlogAPI.Data.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAsync();
        Task<Author> GetAsync(int id);
        Task AddASync(Author author);
        Task EditAsync(Author author);
        Task DeleteAsync(int id);
    }
}