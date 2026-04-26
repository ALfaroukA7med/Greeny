
namespace Greeny.BLL.Admin.ModelVM.Post
{
    public class PostListVM
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public int Votes { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string FormattedDate { get; set; } = string.Empty;

        public int CommentCount { get; set; }
    }
}