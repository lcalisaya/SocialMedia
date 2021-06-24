using SocialMedia.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostService
    {
        Task AddPost(Post jsonPost);
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPost(int postId);
        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(int postId);
    }
}