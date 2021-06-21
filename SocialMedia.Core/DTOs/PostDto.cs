using System;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Core.DTOs
{
  //Objetos planos solo para transmitir la información
    public class PostDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
