using System.ComponentModel.DataAnnotations;

namespace BlogsAPI.Models.DTOs.Post;

public class PostCreateDto
{
    [Required, MaxLength(50)]
    public required string Title { get; set; }
    [Required, MaxLength(100)]
    public required string Summary { get; set; }
    [Required, MaxLength(350)]
    public required string Content { get; set; }
}