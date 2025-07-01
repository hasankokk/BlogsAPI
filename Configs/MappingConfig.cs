using BlogsAPI.Models.DTOs.Comment;
using BlogsAPI.Models.DTOs.Post;
using BlogsAPI.Models.DTOs.Report;
using BlogsAPI.Models.Entities;
using Mapster;

namespace BlogsAPI.Configs;

public static class MappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Post, PostDto>
            .NewConfig()
            .Map(dest => dest.Status, src => src.Status.ToString());
        TypeAdapterConfig<Comment, CommentPostDto>.NewConfig()
            .Map(dest => dest.AuthorName, src => src.Author.UserName);
        TypeAdapterConfig<Post, PostListAllDto>
            .NewConfig()
            .Map(dest => dest.AuthorName, src => src.User.UserName);
        TypeAdapterConfig<Post, PostStatusList>
            .NewConfig()
            .Map(dest => dest.AuthorName, src => src.User.UserName);
        TypeAdapterConfig<Report, ReportDto>
            .NewConfig()
            .Map(dest => dest.Comment, src => src.Comment.Content)
            .Map(dest => dest.Reporter, src => src.Reporter.UserName);
        TypeAdapterConfig<Comment, CommentDto>.NewConfig()
            .Map(dest => dest.AuthorName, src => src.Author.UserName)
            .Map(dest => dest.ReportComments, src => src.Reports.Select(r => new ReportCommentDto
            {
                Reporter = r.Reporter.UserName,
                Reason = r.Reason
            }).ToList());

    }
}