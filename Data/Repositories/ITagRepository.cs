using BlogAPI.Models;

namespace BlogAPI.Data.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAsync();
        Task<Tag> GetAsync(int id);
        Task AddAsync(Tag tag);
        Task EditAsync(int id, Tag tag);
        Task DeleteAsync(int id);
    }
}