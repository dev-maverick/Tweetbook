using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Tweetbook.Contracts.V1;
using Tweetbook.Contracts.V1.Requests;
using Tweetbook.Contracts.V1.Responses;
using Tweetbook.Domain;
using Tweetbook.Services;

namespace Tweetbook.Controllers.V1
{
    public class PostsController:Controller
    {
        private readonly IPostService _posts;

        public PostsController(IPostService posts)
        {
            _posts = posts;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult Get()
        {
            return Ok(_posts.GetAll());
        }
        
        [HttpGet(ApiRoutes.Posts.Get)]
        public IActionResult Get([FromRoute] Guid postId)
        {
            var post = _posts.GetPostById(postId);

            if (post == null)
                return NotFound();
            
            return Ok(post);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post {Id = postRequest.Id};

            if (post.Id != Guid.Empty)
                post.Id = Guid.NewGuid();
            
            _posts.GetAll().Add(post);
            
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());

            var response = new PostResponse {Id = post.Id};
            return Created(locationUri, response);
        }
    }
}