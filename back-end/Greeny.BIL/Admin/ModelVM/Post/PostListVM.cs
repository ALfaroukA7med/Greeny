using System;
using System.Collections.Generic;
using System.Text;

namespace Greeny.BLL.Admin.ModelVM.Post
{
    public class PostListVM
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string? ImagePath { get; set; }
        public int Votes { get; set; }
        public string AuthorName { get; set; }
        public string FormattedDate { get; set; }

        public int CommentCount { get; set; }
    }
}
