using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class ResourceTypeWithResources
    {
        private string id { get; set; }
        private string name { get; set; }
        private string icon { get; set; }
        private string description { get; set; }
        private ObservableCollection<Resource> resources { get; set; }

        public ResourceTypeWithResources() { }

        public ResourceTypeWithResources(string id, string name, string icon, string description)
        {
            this.id = id;
            this.name = name;
            this.icon = icon;
            this.description = description;
            this.resources = new ObservableCollection<Resource>();
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

        public ObservableCollection<Resource> Resources
        {
            get { return resources; }
            set { resources = value; }
        }

    }
}
