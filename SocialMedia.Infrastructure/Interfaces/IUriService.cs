using SocialMedia.Core.QueryFilters;
using System;

namespace SocialMedia.Infrastructure.Interfaces
{
    public interface IUriService
    {
        Uri GetPostsPaginationUri(PostQueryFilter filter, string actionUrl);
    }
}