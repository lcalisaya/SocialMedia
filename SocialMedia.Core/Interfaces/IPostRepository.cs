using SocialMedia.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    //Contraro, solo se define los métodos
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPosts();
    }   
}
