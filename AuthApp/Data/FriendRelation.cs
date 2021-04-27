using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.Data
{
    public class FriendRelation
    {
        public string UserId { get; set; }
        public string FriendId { get; set; }

        public virtual AppUser User { get; set; }
        public virtual AppUser Friend { get; set; }
    }
}
