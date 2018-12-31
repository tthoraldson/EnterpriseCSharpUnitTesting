using System;
using System.Collections.Generic;

namespace FF.Data.Models
{
    public partial class Vote
    {
        public int VoteId { get; set; }
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public bool UpVote { get; set; }
        public int AddedBy { get; set; }
        public System.DateTime AddedWhen { get; set; }
        public int UpdatedBy { get; set; }
        public System.DateTime UpdatedWhen { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public virtual Review Review { get; set; }
        public virtual User User { get; set; }
    }
}
