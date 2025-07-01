using BlogsAPI.Models.DTOs.Comment;

namespace BlogsAPI.Models.DTOs.Post;

public class PostListAllDto
{
    public string AuthorName { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public string Content { get; set; }
    public string Status { get; set; }
    public List<CommentPostDto> Comments { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}