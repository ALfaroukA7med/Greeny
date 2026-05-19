using Greeny.BLL.ModelVM.Post;
using System;
using System.Collections.Generic;
using System.Text;

namespace Greeny.BLL.ModelVM.Wrapper
{
    public class CommunityVM
    {
        public List<PostListVM> Posts { get; set; } = new List<PostListVM>();
        public PostCreateVM CreatePost { get; set; } = new PostCreateVM();
    };
        
}
