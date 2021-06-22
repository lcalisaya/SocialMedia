using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // Este decorador activa las validaciones a los modelos

    //La clase ControllerBase existe para trabajar con APIs
    //La clase Controller además de hacer lo mismo que ControllerBase, agrega funciones para trabajar en MVC
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        //Inyectar vía constructor: Se le pasan los objetos que de este dependan
        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts() 
        {
            //Bajo Acoplamiento y Alta cohesión: que las clases no dependan entre sí
            //Solución:Inyección de dependencias, trabajar con abstracciones interfaces
            var posts = await _postRepository.GetPosts();
            
            //Se convierte la respuesta en objetos DTO para que el usuario no tenga contacto con nuestra entidad de dominio
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

            //Retorna un status 200
            return Ok(postsDto);
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPost(int postId)
        {
            var post = await _postRepository.GetPost(postId);
            var postDto = _mapper.Map<PostDto>(post);
            return Ok(postDto);
        }

        // En este método se espera un objeto entidad que es el que se comunica con la BBDD.
        // Esto puede generar "Overposting", es decir que el usuario puede enviar más datos/objetos de los que son necesarios.
        // Ejemplo, se podría mandar a guardar un post + un comentario + un usuario 
        [HttpPost]
        public async Task<IActionResult> AddPost(PostDto jsonPost)
        {
            var post = _mapper.Map<Post>(jsonPost);
            await _postRepository.AddPost(post);
            return Ok(post);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost(int idPost, PostDto jsonPost)
        {
            var post = _mapper.Map<Post>(jsonPost);
            post.PostId = idPost;
            await _postRepository.UpdatePost(post);
            return Ok(post);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePost(int idPost)
        {
            var result = await _postRepository.DeletePost(idPost);
            return Ok(result);
        }

    }
}
