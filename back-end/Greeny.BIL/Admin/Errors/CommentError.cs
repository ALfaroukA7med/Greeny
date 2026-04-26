using Greeny.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using static Greeny.BLL.Admin.Response.Errors;

namespace Greeny.BLL.Admin.Errors
{
    public static class CommentError
    {
        public static Error NotFound
            = new Error("Post.NotFound", "Post not found", ErrorType.NotFound);
    }
}
