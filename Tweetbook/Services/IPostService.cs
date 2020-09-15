using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetAllAsync();

        Task<Post> GetPostByIdAsync(Guid postId);
        Task<bool> CreatePostAsync(Post post);

        Task<bool> UpdatePostAsync(Post post);

        Task<bool> DeletePostAsync(Guid postId);
    }
}