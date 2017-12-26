using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParseForm.Models
{
    public class ChenListViewModel
    {
        public string UID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> PostDate { get; set; }
        public int Likes { get; set; }
        public int RepliesCount { get; set; }
    }
}
