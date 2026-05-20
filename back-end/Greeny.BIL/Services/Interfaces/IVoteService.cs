using Greeny.BLL.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Greeny.BLL.Services.Interfaces
{
    public interface IVoteService
    {
        
        public Task<Result> UpVote(string userId, int id);
        public Task<Result> DownVote(string userId, int id);
        public Task<Result> RemoveVote(string userId, int id);
    }
}
