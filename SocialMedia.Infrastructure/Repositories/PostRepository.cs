﻿using Microsoft.EntityFrameworkCore;
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
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == postId);
            return post;
        }

        public async Task AddPost(Post jsonPost)
        {
            _context.Posts.Add(jsonPost);
            await _context.SaveChangesAsync();
        }
  }
}
