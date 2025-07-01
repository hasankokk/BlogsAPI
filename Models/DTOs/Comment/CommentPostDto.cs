namespace BlogsAPI.Models.DTOs.Comment;

public class CommentPostDto
{
    public string AuthorName { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}