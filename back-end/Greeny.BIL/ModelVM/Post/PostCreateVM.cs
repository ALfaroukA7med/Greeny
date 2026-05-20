using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Greeny.BLL.ModelVM.Post
{
    public class PostCreateVM
    {
        //[Required(ErrorMessage = "Content is required")]
        //[MinLength(10, ErrorMessage = "Post content must be at least 10 characters")]
        public string? UserId { get; set; }
        public string Content { get; set; }
        public string? ImagePath { get; set; }
    }
}
