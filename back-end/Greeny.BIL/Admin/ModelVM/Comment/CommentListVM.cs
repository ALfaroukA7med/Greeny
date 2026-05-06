

namespace Greeny.BLL.Admin.ModelVM.Comment
{
    public class CommentListVM
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;

        // UI Friendly data
        public string AuthorName { get; set; } = string.Empty;
        public string TimeAgo { get; set; } = string.Empty; // e.g., "5 minutes ago"

    }
}
