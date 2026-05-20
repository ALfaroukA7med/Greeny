using System;
using System.Collections.Generic;
using System.Text;

namespace Greeny.DAL.Entities
{
    public class Vote
    {
        public int Id { get; set; } // Primary Key
        public int PostId { get; set; }
        public string UserId { get; set; }
        public bool IsUpvote { get; set; } // true = Upvote, false = Downvote
        public bool isDeleted { get; set; } = false;
    }
}
