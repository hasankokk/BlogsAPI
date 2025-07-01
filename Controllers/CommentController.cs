using BlogsAPI.Data;
using BlogsAPI.Models.DTOs.Comment;
using BlogsAPI.Models.Entities;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogsAPI.Controllers;

[ApiController]
[Route("comments")]
[Authorize]
public class CommentController(AppDbContext context, UserManager<IdentityUser> userManager) : ControllerBase
{
    [HttpPost]
    [Route("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddComment(int id, CommentCreateDto commentCreateDto)
    {
        var userId = userManager.GetUserId(User);
        var post = await context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == id);
        if  (post == null)
            return NotFound( new { message = "Post not found" } );
        if (userId != null)
        {
            var newComment = new Comment
            {
                Content = commentCreateDto.Content,
                AuthorId = userId,
                PostId = id,
            };
            post.Comments.Add(newComment);
            await context.SaveChangesAsync();
            var user = await userManager.FindByIdAsync(userId);
            newComment.Author = user;
            return CreatedAtAction(nameof(ViewComment), new { id = newComment.Id }, newComment.Adapt<CommentDto>());
        }
        
        return BadRequest( new  { msg = "User not found" });
    }
    
    [HttpGet]
    [Route("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ViewComment(int id)
    { 
        var userId = userManager.GetUserId(User);
        var comment = await context.Comments.Include(x => x.Author).Include(x => x.Reports).Where(x => x.Id == id && x.AuthorId == userId).FirstOrDefaultAsync();
        if (comment == null)
            return NotFound(new { msg = "Comment not found" });

        return Ok(comment.Adapt<CommentDto>());
    }

    [HttpPut]
    [Route("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateComment(int id, CommentCreateDto commentCreateDto)
    {
        var userId = userManager.GetUserId(User);
        var comment =  await context.Comments.Include(x => x.Author)
            .Where(x => x.AuthorId == userId)
            .FirstOrDefaultAsync(x => x.Id == id);
        if  (comment == null)
            return NotFound(new { msg = "Comment not found" });
        comment.Content = commentCreateDto.Content + "(Edited)";
        comment.UpdatedAt = DateTime.Now;
        await context.SaveChangesAsync();
        return NoContent();
    }
    
    [HttpDelete]
    [Route("{id:int:min(1)}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var userId = userManager.GetUserId(User); 
        var comment = await context.Comments.Include(x => x.Author).Where(x => x.Id == id && x.AuthorId == userId).FirstOrDefaultAsync();
        if (comment == null)
            return NotFound(new { msg = "Comment not found" });
        context.Comments.Remove(comment);
        await context.SaveChangesAsync();
        return Ok(new { msg = "Comment deleted" });
    }
}