using Greeny.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using static Greeny.BLL.Abstraction.Errors;

namespace Greeny.BLL.Errors
{
    public static class PostError
    {
        public static Error NotFound
            = new Error("Post.NotFound", "Post not found", ErrorType.NotFound);
    }
}
