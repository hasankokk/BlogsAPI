using BlogsAPI.Models.DTOs.Comment;

namespace BlogsAPI.Models.DTOs.Post;

public class PostDto
{
    public required string Title { get; set; }
    public required string Summary { get; set; }
    public required string Content { get; set; }
    public required string Status { get; set; }
    public List<CommentPostDto> Comments { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}