using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialMediaContext _context;
        public PostRepository(SocialMediaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _context.Posts.ToListAsync();
            return posts;
        }

        public async Task<Post> GetPost(int postId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == postId);
            return post;
        }

        public async Task AddPost(Post jsonPost)
        {
            _context.Posts.Add(jsonPost);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdatePost(Post post)
        { 
            var currentPost = await GetPost(post.Id);
            currentPost.Date = post.Date;
            currentPost.Description = post.Description;
            currentPost.Image = post.Image;

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
        public async Task<bool> DeletePost(int postId)
        { 
            var currentPost = await GetPost(postId);
     
            _context.Posts.Remove(currentPost);

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

    }
}
