using System;
using System.Collections.Generic;
using System.Text;

namespace Greeny.DAL.Repository.Implementation
{
    public class VoteRepo : IVoteRepo
    {
        private readonly GreenyDbContext _context;
        public VoteRepo(GreenyDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(string userId, Vote vote)
        {
            await _context.Votes.AddAsync(vote);
            await _context.SaveChangesAsync();
        }

        //public async Task DeleteAsync(string userId, int id)
        //{
        //    await _context.Votes
        //        .Where(v => v.Id == id )
        //        .ExecuteUpdateAsync(setter => setter
        //        .SetProperty(v => v.isDeleted, true));
        //}

        public IQueryable<Vote> GetVoteAsync(string userId, int postId)
        {
            return _context.Votes
            .Where(v => v.UserId == userId && v.PostId == postId);
        }

        public async Task UpdateAsync(string userId, Vote vote)
        {
            await _context.Votes
                .Where(v => v.Id == vote.Id && vote.UserId == userId)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(v => v.Type, vote.Type));
        }
    }
}
