using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Tag
    {
        private string id { get; set; }
        private string name { get; set; }
        private string color { get; set; }
        private string description { get; set; }

        public Tag() { }

        public Tag(string id, string name, string color, string description)
        {
            this.id = id;
            this.name = name;
            this.color = color;
            this.description = description;
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
