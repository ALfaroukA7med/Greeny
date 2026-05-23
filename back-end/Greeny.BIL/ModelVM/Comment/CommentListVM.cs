namespace Greeny.BLL.ModelVM.Comment
{
    public class CommentListVM
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; } = string.Empty;

        // UI Friendly data
        public string AuthorName { get; set; } = string.Empty;
        public string TimeAgo { get; set; } = string.Empty;// e.g., "5 minutes ago"

    }
}
