using Microsoft.AspNetCore.Identity;

namespace BlogsAPI.Models.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string AuthorId { get; set; }
    public IdentityUser Author { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
    public ICollection<Report> Reports { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
}