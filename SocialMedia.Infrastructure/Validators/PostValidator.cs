using SocialMedia.Core.DTOs;
using System;
using FluentValidation;

namespace SocialMedia.Infrastructure.Validators
{
    public class PostValidator : AbstractValidator<PostDto>
    {
        public PostValidator()
        {
            RuleFor(post => post.Description)
              .NotNull()
              .Length(10,500);

            RuleFor(post => post.Date)
              .NotNull()
              .LessThan(DateTime.Now);
        }
    }
}
