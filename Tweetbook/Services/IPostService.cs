using System;
using System.Collections.Generic;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public interface IPostService
    {
        List<Post> GetAll();

        Post GetPostById(Guid postId);
    }
}