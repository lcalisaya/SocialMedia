using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostService
    {
        Task AddPost(Post jsonPost);
        PagedList<Post> GetPosts(PostQueryFilter filters);
        Task<Post> GetPost(int postId);
        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(int postId);
    }
}