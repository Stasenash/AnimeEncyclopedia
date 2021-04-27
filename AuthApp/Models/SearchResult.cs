using System.Collections.Generic;

namespace AuthApp.Models
{
    public class SearchResult
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Labels { get; set; }
        
        public SearchResult()
        {
            Labels = new List<string>();
        }
    }
}