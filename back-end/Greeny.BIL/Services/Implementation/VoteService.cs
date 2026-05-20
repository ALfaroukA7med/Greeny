using Greeny.BLL.Abstraction;
using Greeny.DAL.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Greeny.BLL.Services.Implementation
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepo _voterepo;
        private readonly IPostRepo _postrepo;
        public VoteService(IVoteRepo voteRepo, IPostRepo postRepo)
        {
            _voterepo = voteRepo;
            _postrepo = postRepo;
        }

        public async Task<Result> DownVote(string userId, int id)
        {
            var post = await _postrepo.GetByIdAsync(id).FirstOrDefaultAsync();
            if (post == null) return Result.Failure(PostError.NotFound);

            var existingVote = _voterepo.GetVoteAsync(userId, id).FirstOrDefault();

            if (existingVote != null)
            {
                if (!existingVote.IsUpvote)
                {
                    post.Votes += 1;
                    await _voterepo.DeleteAsync(userId , id);
                }
                else
                {
                    // Switch from upvote to downvote (-2 score change)
                    existingVote.IsUpvote = false;
                    post.Votes -= 2;
                    await _voterepo.UpdateAsync(userId, existingVote);
                }
            }
            else
            {
                // Brand new downvote (-1 score change)
                var newVote = new Vote { UserId = userId, PostId = id, IsUpvote = false };
                post.Votes -= 1;
                await _voterepo.AddAsync(userId , newVote);
            }

            return Result.Success();
        }

        public async Task<Result> RemoveVote(string userId, int id)
        {
            var post = _postrepo.GetByIdAsync(id).FirstOrDefault();
            if (post == null) return Result.Failure(PostError.NotFound);

            var existingVote = _voterepo.GetVoteAsync(userId, id).FirstOrDefault();
            if (existingVote == null) return Result.Success(); // No vote exists anyway

            // Pure dismissal manual track
            if (existingVote.IsUpvote) post.Votes  -= 1;
            else post.Votes += 1;

            await _voterepo.DeleteAsync(userId, id);

            return Result.Success();
        }

        public async Task<Result> UpVote(string userId, int id)
        {
            var post = _postrepo.GetByIdAsync(id).FirstOrDefault();
            if (post == null) return Result.Failure(PostError.NotFound);

            var existingVote = _voterepo.GetVoteAsync(userId, id).FirstOrDefault();

            if (existingVote != null)
            {
                if (existingVote.IsUpvote)
                {
                    // --- DISMISS UPVOTE ---
                    // User clicked upvote again. Undo it entirely!
                    post.Votes -= 1;
                    await _voterepo.DeleteAsync(userId, id);
                }
                else
                {
                    // Switch from downvote to upvote (+2 score change)
                    existingVote.IsUpvote = true;
                    post.Votes += 2;
                    await _voterepo.UpdateAsync(userId, existingVote);
                }
            }
            else
            {
                // Brand new upvote (+1 score change)
                var newVote = new Vote { UserId = userId, PostId = id, IsUpvote = true };
                post.Votes += 1;
                await _voterepo.AddAsync(userId, newVote);
            }
            await _voterepo.GetVoteAsync(post);
            return Result.Success();
        }
    }
}
