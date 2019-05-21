using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    class Tag
    {
        private string id { get; set; }
        private string name { get; set; }
        private TagColor color { get; set; }
        private string description { get; set; }

        public Tag() { }

        public Tag(string id, string name, TagColor color, string description)
        {
            this.id = id;
            this.name = name;
            this.color = color;
            this.description = description;
        }
    }

    enum TagColor
    {
        Red, Blue, Yellow, Green, White
    }
}
