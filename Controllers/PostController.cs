using BlogsAPI.Data;
using BlogsAPI.Models.DTOs.Post;
using BlogsAPI.Models.Entities;
using BlogsAPI.Models.Enums;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogsAPI.Controllers;

[ApiController]
[Route("posts")]
[Authorize]
public class PostController(AppDbContext context, UserManager<IdentityUser> userManager) : ControllerBase
{
    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddPost(PostCreateDto dto)
    {
        var userId = userManager.GetUserId(User);
        if (userId != null)
        {
            var user = await userManager.FindByIdAsync(userId);
            var newPost = dto.Adapt<Post>();
            newPost.UserId = userId;
            if (user != null && (await userManager.IsInRoleAsync(user, "Admin") ||
                                 await userManager.IsInRoleAsync(user, "Moderator")))
            {
                newPost.Status = PostStatus.Approved;
            }
            
            await context.Posts.AddAsync(newPost);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(ViewPost), new { id = newPost.Id }, newPost.Adapt<PostDto>());
        }
        return BadRequest(new { msg = "User not found" });
    }


    [HttpGet]
    [Route("{id:int:min(1)}")]
    [ProducesResponseType<PostDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ViewPost(int id)
    {
        var userId = userManager.GetUserId(User);
        var post = await context.Posts.Include(x => x.Comments).ThenInclude(x => x.Author).Where(x => x.UserId == userId).Where(x => x.Id == id).FirstOrDefaultAsync();
        if  (post == null)
            return NotFound();
        return Ok(post.Adapt<PostDto>());
    }

    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    [ProducesResponseType<PostListAllDto[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ViewPosts()
    {
        var posts = await context.Posts
            .Include(x => x.User)
            .Include(x => x.Comments)
                .ThenInclude(x => x.Author)
            .Where(x => x.Status == PostStatus.Approved)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();;
        return Ok(posts.Adapt<PostListAllDto[]>());
    }
}