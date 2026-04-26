using System;
using System.Collections.Generic;
using System.Text;

namespace Greeny.BLL.Admin.ModelVM.Comment
{
    public class CommentListVM
    {
        public int Id { get; set; }
        public string Content { get; set; }

        // UI Friendly data
        public string AuthorName { get; set; }
        public string TimeAgo { get; set; } // e.g., "5 minutes ago"

    }
}
