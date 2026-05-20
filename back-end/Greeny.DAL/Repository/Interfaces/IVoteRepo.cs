using System;
using System.Collections.Generic;
using System.Text;

namespace Greeny.DAL.Repository.Interfaces
{
    public interface IVoteRepo
    {
        IQueryable<Vote> GetVoteAsync(string userId, int postId);
        Task AddAsync(string userId, Vote vote);
        Task UpdateAsync(string userId, Vote vote);
        //Task DeleteAsync(string userId, int id);
    }
}
