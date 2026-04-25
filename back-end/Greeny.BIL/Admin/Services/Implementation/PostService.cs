using Greeny.BLL.Admin.ModelVM.Post;
using Greeny.BLL.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Greeny.BLL.Admin.Services.Implementation
{
    public class PostService : IPostService
    {
        private readonly IPostRepo _postrepo;
        public PostService(IPostRepo postrepo)
        {
            _postrepo = postrepo;
        }

        public async Task<Response<bool>> CreateAsync(PostCreateVM post)
        {
            var npost = new Post()
            {
                Content = post.Content,
                ImagePath = post.ImagePath
            };

            await _postrepo.CreateAsync(npost);

            return Response<bool>.Success(true);
        }

        public async Task<Response<bool>> DeleteAsync(int id)
        {
            var post = _postrepo.GetByIdAsync(id);
        }

        public Task<Response<IEnumerable<PostCreateVM>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<PostListVM>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
