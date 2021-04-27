using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AuthApp.Data
{
    public class AppUser : IdentityUser
    {
        public virtual ICollection<FriendRelation> IAmFriendsWith { get; set; }

        public virtual ICollection<FriendRelation> AreFriendsWithMe { get; set; }
    }
}