using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AuthApp.Models
{
    public partial class AnimeLabel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserAnimeLabel> UserAnimeLabels { get; set; }
    }
}
