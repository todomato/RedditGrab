using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParseForm.Models
{
    public class ChenReplyViewModel
    {
        public string UID { get; set; }
        public string ListUID { get; set; }
        public string Author { get; set; }
        public System.DateTime PostDate { get; set; }
        public string Message { get; set; }
    }
}
