using Microsoft.AspNetCore.Mvc;
using SocialMedia.Infrastructure.Repositories;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPost() 
        {
            //Bajo Acoplamiento y Alta cohesión: que las clases no dependan entre sí
            //Solución:Inyección de dependencias, trabajar con abstracciones interfaces
            var posts = new PostRepository().GetPosts();
            //Retorna un status 200
            return Ok(posts);
        }

    }
}
