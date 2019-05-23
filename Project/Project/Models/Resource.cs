﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Resource
    {
        private string id { get; set; }
        private string name { get; set; } 
        private string description { get; set; }
        private ResourceType type { get; set; }
        private ResourceFrequency frequency { get; set; }
        private string icon { get; set; }
        private bool renewable { get; set; }
        private bool strategicImportance { get; set; }
        private bool currentlyExploited { get; set; }
        private ResourceUnit unit { get; set; }
        private ResourcePrice price { get; set; }
        private string dateOfDiscovery { get; set; }
        private List<Tag> tags { get; set; }

        public Resource() {
            this.tags = new List<Tag>();
        }

        public Resource(string id, string name, string description, ResourceType type, ResourceFrequency frequency,
                        string icon, bool renewable, ResourcePrice price)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.type = type;
            this.frequency = frequency;
            if (icon.Equals(""))
                this.icon = type.Icon;
            this.icon = icon;
            this.renewable = renewable;
            this.price = price;
            this.tags = new List<Tag>();
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

        public ResourceType ResourceType
        {
            get { return type; }
            set { type = value; }
        }

        public bool Renewable
        {
            get { return renewable; }
            set { renewable = value; }
        }

        public ResourceFrequency Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }

        public ResourcePrice Price
        {
            get { return price; }
            set { price = value; }
        }

        public ResourceUnit Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        public string DateOfDiscovery
        {
            get { return dateOfDiscovery; }
            set { dateOfDiscovery = value; }
        }

        public bool StrategicImportance
        {
            get { return strategicImportance; }
            set { strategicImportance = value; }
        }

        public bool CurrentlyExploited
        {
            get { return currentlyExploited; }
            set { currentlyExploited = value; }
        }

        public List<Tag> Tags
        {
            get { return tags; }
            set { tags = value; }
        }
    };

    public enum ResourceFrequency
    {
        Infrequent, Common, Universal
    };

    public enum ResourceUnit
    {
        Merica, Barrel, Ton, Kilogram
    };

    public enum ResourcePrice
    {
        Dollar
    }
}
