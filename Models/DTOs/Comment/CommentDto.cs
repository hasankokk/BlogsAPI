using BlogsAPI.Models.DTOs.Report;

namespace BlogsAPI.Models.DTOs.Comment;

public class CommentDto
{
    public string AuthorName { get; set; }
    public string Content { get; set; }
    public List<ReportCommentDto> ReportComments { get; set; }
    public DateTime CreatedAt { get; set; }
}