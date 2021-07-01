using SocialMedia.Core.CustomEntities;

namespace SocialMedia.Api.Responses
{
  //Para manejar una estructura general en todas las respuestas de las operaciones CRUD
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public MetaData Meta { get; set; }

        public ApiResponse(T data)
        {
            Data = data;
        }
    }
}
