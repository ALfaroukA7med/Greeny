using Greeny.BLL.Abstraction;
using Greeny.DAL.Enums;

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

            var existingVote = await _voterepo.GetVoteAsync(userId, id).FirstOrDefaultAsync();
            if (existingVote == null) {
                existingVote = new Vote()
                {
                    PostId = post.Id,
                    UserId = userId,
                    Type = Voting.Dismiss
                };
                await _voterepo.AddAsync(userId, existingVote);
            }
            if (existingVote.Type == Voting.Dismiss)
            {
                post.Votes -= 1;
                existingVote.Type = Voting.DownVote;
            }
            else if (existingVote.Type == Voting.UpVote)
            {
                post.Votes -= 2;
                existingVote.Type = Voting.DownVote;
            }
            else
            {
                post.Votes += 1;
                existingVote.Type = Voting.Dismiss;
            }
            await _voterepo.UpdateAsync(userId, existingVote);
            await _postrepo.UpdateAsync(post);
            return Result.Success();
        }

        
        public async Task<Result> UpVote(string userId, int id)
        {
            var post = await _postrepo.GetByIdAsync(id).FirstOrDefaultAsync();
            if (post == null) return Result.Failure(PostError.NotFound);

            var existingVote = await _voterepo.GetVoteAsync(userId, id).FirstOrDefaultAsync();
            if (existingVote == null)
            {
                existingVote = new Vote()
                {
                    PostId = post.Id,
                    UserId = userId,
                    Type = Voting.Dismiss
                };
                await _voterepo.AddAsync(userId, existingVote);
            }
            if (existingVote.Type == Voting.Dismiss)
            {
                post.Votes += 1;
                existingVote.Type = Voting.UpVote;
            }
            else if (existingVote.Type == Voting.UpVote)
            {
                post.Votes -= 1;
                existingVote.Type = Voting.Dismiss;
            }
            else
            {
                post.Votes += 2;
                existingVote.Type = Voting.UpVote;
            }
            await _voterepo.UpdateAsync(userId, existingVote);
            await _postrepo.UpdateAsync(post);
            return Result.Success();

        }
    }
}
