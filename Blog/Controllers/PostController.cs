using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Blog.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class PostController : ControllerBase
    {
        public PostDbContext _context;
        public PostController(PostDbContext context) 
        {
            _context = context;
        }

        [HttpGet]
        [Route("/home")]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = _context.Posts;

            return Ok(posts);
        }

        [HttpPost]
        [Route("/create-post")]
        public async Task<IActionResult> Create(PostsModel posts)
        {
            _context.Posts.Add(posts);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Create), new { id = posts.Id }, posts);
        }

        [HttpDelete]
        [Route("/delete-post/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);

            await _context.SaveChangesAsync();

            return Ok("Post Deleted Successfully");
        }

        [HttpGet]
        [Route("/posts/{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPut]
        [Route("/posts/{id}")]
        public async Task<IActionResult> Update(PostsModel post, int id)
        {
            var oldPost = await _context.Posts.FindAsync(id);
            if (oldPost == null)
            {
                return NotFound();
            }

            oldPost.Title = post.Title;
            oldPost.Description = post.Description;
            oldPost.Author = post.Author;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok("Post Updated Successfully");
        }
    }
}
