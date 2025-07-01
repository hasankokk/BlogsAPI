using BlogsAPI.Data;
using BlogsAPI.Models.DTOs.Post;
using BlogsAPI.Models.Enums;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogsAPI.Controllers;

[ApiController]
[Route("Moderator")]
[Authorize(Roles = "Admin, Moderator")]
public class ModeratorController(AppDbContext context) : ControllerBase
{
    [HttpPut]
    [Route("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SetStatusPost(int id, PostStatus status)
    {
        var posts = await context.Posts
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        if (posts == null)
            return BadRequest( new { msg = "Post not found"} );
        posts.Status = status;
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete]
    [Route("deletecomment/{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var comment = await context.Comments.FindAsync(id);
        if (comment == null)
            return NotFound();
        context.Comments.Remove(comment);
        await context.SaveChangesAsync();
        return Ok( new  { msg = "Comment deleted"});
    }

    [HttpGet]
    [Route("posts/")]
    [ProducesResponseType<PostDto[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuthorPosts(string id)
    {
        var posts = await context.Posts.Include(x => x.User).Include(x => x.Comments).ThenInclude(x => x.Author).Where(x => x.UserId == id).ToListAsync();
        return Ok(posts.Adapt<PostDto[]>());
    }
    
    [HttpGet]
    [Route("posts/all")]
    [ProducesResponseType<PostDto[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetStatusPosts(string filter)
    {
        if (!Enum.TryParse<PostStatus>(filter, true, out var parsedStatus))
        {
            return BadRequest(new { msg = "Invalid status value." });
        }
        var posts = await context.Posts.Include(x => x.User).Include(x => x.Comments).ThenInclude(x => x.Author).Where(x => x.Status == parsedStatus).OrderByDescending(x => x.CreatedAt).ToListAsync();
        return Ok(posts.Adapt<PostStatusList[]>());
    }
}