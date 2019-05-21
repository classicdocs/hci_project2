using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    class Resource
    {
        private string id { get; set; }
        private string name { get; set; } 
        private string description { get; set; }
        private Type type { get; set; }
        private ResourceFrequency frequency { get; set; }
        private string icon { get; set; }
        private bool renewable { get; set; }
        private bool strategicImportance { get; set; }
        private bool currentlyExploited { get; set; }
        private ResourceUnit unit { get; set; }
        private ResourcePrice price { get; set; }
        private string dateOfDiscovery { get; set; }
        private HashSet<Tag> tags { get; set; }

        public Resource() { }

        public Resource(string id, string name, string description, Type type, ResourceFrequency frequency,
                        string icon, bool renewable, bool strategicImportance, bool currentlyExploited, ResourceUnit unit,
                        ResourcePrice price, string dateOfDiscovery, HashSet<Tag> tags)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.type = type;
            this.frequency = frequency;
            this.icon = icon;
            this.renewable = renewable;
            this.strategicImportance = strategicImportance;
            this.currentlyExploited = currentlyExploited;
            this.unit = unit;
            this.price = price;
            this.dateOfDiscovery = dateOfDiscovery;
            this.tags = tags;
        }
    };

    enum ResourceFrequency
    {
        Infrequent, Common, Universal
    };

    enum ResourceUnit
    {
        Merica, Barrel, Ton, Kilogram
    };

    enum ResourcePrice
    {
        Dollar
    }
}
