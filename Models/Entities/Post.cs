using System.ComponentModel.DataAnnotations;
using BlogsAPI.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace BlogsAPI.Models.Entities;

public class Post
{
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string Title { get; set; }
    [Required, MaxLength(100)]
    public string Summary { get; set; }
    [Required, MaxLength(350)]
    public string Content { get; set; }
    public string UserId { get; set; }
    public IdentityUser User { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public PostStatus Status { get; set; } = PostStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
}