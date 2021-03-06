using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMedia.Api.Responses;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController] // Este decorador activa las validaciones a los modelos

    //La clase ControllerBase existe para trabajar con APIs
    //La clase Controller además de hacer lo mismo que ControllerBase, agrega funciones para trabajar en MVC
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        //Inyectar vía constructor: Se le pasan los objetos que de este dependan
        public PostController(IPostService postService, IMapper mapper, IUriService uriService)
        {
            _postService = postService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Devuelve todos los posts
        /// </summary>
        /// <param name="filters">Se envía los filtros que se pueden aplicar a la solicitud</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetPosts))]
        //Decoradores necesarios para especificar el tipo de respuesta en la documentación
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<PostDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //Parámetros que se pasan por query string, en forma individual o en un objeto
        public IActionResult GetPosts([FromQuery]PostQueryFilter filters) 
        {
            //Bajo Acoplamiento y Alta cohesión: que las clases no dependan entre sí
            //Solución:Inyección de dependencias, trabajar con abstracciones interfaces
            var posts = _postService.GetPosts(filters);
            
            //Se convierte la respuesta en objetos DTO para que el usuario no tenga contacto con nuestra entidad de dominio
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

            //En el header del response se enviará este objeto
            var metadata = new MetaData{ 
                PageSize = posts.PageSize,
                TotalCount = posts.TotalCount,
                TotalPages = posts.TotalPages,
                CurrentPage = posts.CurrentPage,
                HasPreviousPage = posts.HasPreviousPage,
                HasNextPage = posts.HasNextPage,
                NextPageUrl = _uriService.GetPostsPaginationUri(filters, Url.RouteUrl(nameof(GetPosts))).ToString(),
                PreviousPageUrl = _uriService.GetPostsPaginationUri(filters, Url.RouteUrl(nameof(GetPosts))).ToString()
            };

            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto) 
            {
                Meta = metadata
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            //Retorna un status 200
            return Ok(response);
        }

        /// <summary>
        /// Devuelve un post
        /// </summary>
        /// <param name="postId">Es el ID de post solicitado</param>
        /// <returns></returns>
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPost(int postId)
        {
            var post = await _postService.GetPost(postId);
            var postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        /// <summary>
        /// Se guarda un post desde 0
        /// </summary>
        /// <param name="jsonPost">Son los datos del post enviados</param>
        /// <returns></returns>
        // En este método se espera un objeto entidad que es el que se comunica con la BBDD.
        // Esto puede generar "Overposting", es decir que el usuario puede enviar más datos/objetos de los que son necesarios.
        // Ejemplo, se podría mandar a guardar un post + un comentario + un usuario 
        [HttpPost]
        public async Task<IActionResult> AddPost(PostDto jsonPost)
        {
            var post = _mapper.Map<Post>(jsonPost);
            await _postService.AddPost(post);
            
            var postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        /// <summary>
        /// Se edita y guarda un post existente
        /// </summary>
        /// <param name="idPost">ID del post indicado para modificar</param>
        /// <param name="jsonPost">Datos del post que se quieren cambiar</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdatePost(int idPost, PostDto jsonPost)
        {
            var post = _mapper.Map<Post>(jsonPost);
            post.Id = idPost;
            var result = await _postService.UpdatePost(post);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Se elimina un post
        /// </summary>
        /// <param name="idPost">ID del post que se quiere eliminar</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeletePost(int idPost)
        {
            var result = await _postService.DeletePost(idPost);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
