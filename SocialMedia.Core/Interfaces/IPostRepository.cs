using SocialMedia.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    //Contrato, solo se define los métodos
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPost(int postId);
        Task AddPost(Post jsonPost);
        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(int postId);
    }   
}
