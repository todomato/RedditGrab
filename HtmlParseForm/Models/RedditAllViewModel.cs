using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParseForm.Models
{
    public class RedditAllViewModel
    {
        public string UID { get; set; }
        public string ID { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string MessageCount { get; set; }
        public string Url { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> PostDate { get; set; }
        public string ReplyAuthor { get; set; }
        public Nullable<System.DateTime> ReplyPostDate { get; set; }
        public string ReplyMessage { get; set; }
    }
}
