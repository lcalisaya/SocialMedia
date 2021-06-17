using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository
    {
        public IEnumerable<Post> GetPosts()
        {
            //Para simular una consulta a la BBDD
            var posts = Enumerable.Range(1, 10).Select(x => new Post 
            { 
                PostId = x,
                UserId = x * 2,
                Description = $"Descripción del post Nº {x}",
                Date = DateTime.Now,
                Image = $"http://imageexample.com/{x}"
            });

            return posts;
        }
    }
}
