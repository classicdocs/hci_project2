using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class ResourceType
    {
        private string id { get; set; }
        private string name { get; set; }
        private string icon { get; set; }
        private string description { get; set; }

        public ResourceType() { }

        public ResourceType(string id, string name, string icon, string description)
        {
            this.id = id;
            this.name = name;
            this.icon = icon;
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
        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

    }
}
