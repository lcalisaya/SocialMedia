﻿using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostMongoRepository : IPostRepository
    {
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = Enumerable.Range(1, 10).Select(x => new Post
            {
                PostId = x,
                UserId = x * 2,
                Description = $"Descripción del post Nº {x} del repositorio Mongo",
                Date = DateTime.Now,
                Image = $"http://imageexample.com/{x}"
            });

            await Task.Delay(10);
            return posts;
        }
    }
}
