using Greeny.BLL.Admin.ModelVM.Comment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Greeny.BLL.Admin.ModelVM.Post
{
    public class PostDetailsVM
    {
        // The Post Data
        public int Id { get; set; }
        public string Content { get; set; }
        public string? ImagePath { get; set; }
        public int Votes { get; set; }
        public string AuthorName { get; set; }
        public List<CommentListVM> Comments { get; set; }
        // for add comment
        public CommentCreateVM NewComment { get; set; }
    }
}
