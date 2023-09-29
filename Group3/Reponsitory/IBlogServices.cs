using Lib;

namespace Group3.Reponsitory
{
    public interface IBlogServices
    {
        Task<IEnumerable<Blogs>> GetBlogsList();
        Task<Blogs> GetBlogsById(int id);

        Task<bool> addBlogs(Blogs newBlogs);
        Task<bool> removeBlogs(int id);
        Task<bool> updateBlogs(Blogs editBlogs);
    }
}
