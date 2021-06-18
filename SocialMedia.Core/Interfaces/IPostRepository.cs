using SocialMedia.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    //Contrato, solo se define los métodos
    public interface IPostRepository
    {
        Task<IEnumerable<Publicacion>> GetPosts();
    }   
}
