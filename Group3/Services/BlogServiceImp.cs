using Group3.Reponsitory;
using Microsoft.EntityFrameworkCore;
using Group3.Db;
using Lib;


namespace Group3.Services
{

    public class BlogServiceImp : IBlogServices
    {
        private DatabaseContext db;
        public BlogServiceImp(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task<bool> addBlogs(Blogs newBlog)
        {
            await db.Blogs.AddAsync(newBlog);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<Blogs> GetBlogsById(int id)
        {
            return await db.Blogs.SingleOrDefaultAsync(b=>b.ID.Equals(id));
        }

        public async Task<IEnumerable<Blogs>> GetBlogsList()
        {
            return await db.Blogs.ToListAsync();
        }

        public async Task<bool> removeBlogs(int id)
        {
            var blog = await db.Blogs.FirstOrDefaultAsync(n => n.ID.Equals( id));
            if (blog != null)
            {
                db.Blogs.Remove(blog);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> updateBlogs(Blogs editBlog)
        {
            var news = await db.Blogs.FirstOrDefaultAsync(n => n.ID == editBlog.ID);
            if (news != null)
            {
                news.Title = editBlog.Title;
                news.Summary = editBlog.Summary;
                news.Content1 = editBlog.Content1;
                news.Content2 = editBlog.Content2;
                news.Content3 = editBlog.Content3;
                news.Author = editBlog.Author;
                news.PublishDate = editBlog.PublishDate;
                news.Photo = editBlog.Photo;
                await db.SaveChangesAsync();
                return true;
            }
            else { return false; };
        }
    }
}
