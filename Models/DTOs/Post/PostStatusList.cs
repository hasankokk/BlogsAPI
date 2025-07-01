namespace BlogsAPI.Models.DTOs.Post;

public class PostStatusList
{
    public string AuthorName { get; set; }
    public required string Title { get; set; }
    public required string Summary { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}