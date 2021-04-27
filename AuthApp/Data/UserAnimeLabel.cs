using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AuthApp.Data;
using JikanDotNet;
using Microsoft.AspNetCore.Identity;

namespace AuthApp.Models
{
    public partial class UserAnimeLabel
    {
        public string UserId { get; set; }
        
        public virtual AppUser User { get; set; }
        
        public long AnimeId { get; set; }
        
        public int LabelId { get; set; }

        public virtual AnimeLabel Label { get; set; }
    }
}
