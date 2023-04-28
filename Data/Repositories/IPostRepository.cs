using BlogAPI.Models;

namespace BlogAPI.Data.Repositories
{
    public interface IPostRepository 
    {
        Task<IEnumerable<Post>> GetPostAsync(int pageSize, int pageNumbe);
        Task<Post> GetPostAsync(int id);
        Task AddAsync(Post post);
        Task EditAsync(Post post);
        Task DeleteAsync(int id);
    }
}