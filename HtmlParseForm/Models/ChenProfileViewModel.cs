using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlParseForm.Models
{
    public class ChenProfileViewModel
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public Nullable<int> Follows { get; set; }
        public string Address { get; set; }
        public string Answers { get; set; }
    }
}
