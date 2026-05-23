using Greeny.BLL.ModelVM.Comment;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Greeny.BLL.ModelVM.Post
{
    public class PostDetailsVM
    {
        // The Post Data
        public string UserId { get; set; }
        public int Id { get; set; }
        public string Content { get; set; }
        public string? ImagePath { get; set; }
        public int Votes { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string FormattedDate { get; set; } = string.Empty;
        public List<CommentListVM> Comments { get; set; }
        // for add comment
        public CommentCreateVM NewComment { get; set; } = new CommentCreateVM();
    }
}
