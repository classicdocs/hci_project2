﻿using Project.Commands;
using Project.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project.Models
{
    public class Resource : INotifyPropertyChanged
    {
        private string id { get; set; }
        private string name { get; set; } 
        private string description { get; set; }
        private ResourceType type { get; set; }
        private ResourceFrequency frequency { get; set; }
        private string icon { get; set; }
        private bool renewable { get; set;}
        private bool strategicImportance { get; set; }
        private bool currentlyExploited { get; set; }
        private ResourceUnit unit { get; set; }
        private string price { get; set; }
        private string dateOfDiscovery { get; set; }
        private List<Tag> tags { get; set; }
        


        public Resource() {
            this.tags = new List<Tag>();
            Edit = new EditCommand(this);
            Delete = new DeleteCommand(this);
        }

        public Resource(string id, string name, string description, ResourceType type, ResourceFrequency frequency, ResourceUnit unit,
                        string icon, bool renewable, string price)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.type = type;
            this.frequency = frequency;
            if (icon.Equals(""))
                this.icon = type.Icon;
            else
                this.icon = icon;
            this.renewable = renewable;
            this.unit = unit;
            this.price = price;
            this.tags = new List<Tag>();


            Edit = new EditCommand(this);
            Delete = new DeleteCommand(this);

        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
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

        public string RenewableString
        {
            get
            {
                if (renewable)
                    return "Yes";
                else
                    return "No";
            }
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

        public string Price
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

        

        private EditCommand _edit;
        [ScriptIgnore]
        public EditCommand Edit
        {
            get
            {
                return _edit;
            }
            set
            {
                if (_edit != value)
                {
                    _edit = value;
                    OnPropertyChanged("Edit");
                }
            }
        }
        private DeleteCommand _delete;
        [ScriptIgnore]
        public DeleteCommand Delete
        {
            get
            {
                return _delete;
            }
            set
            {
                if (_delete != value)
                {
                    _delete = value;
                    OnPropertyChanged("Delete");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public class EditCommand : ICommand
        {
            private Resource resource;
            public EditCommand(Resource r)
            {
                resource = r;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                EditResource dialog = new EditResource(resource);
                dialog.Show();
            }
        }

        public class DeleteCommand : ICommand
        {
            private Resource resource;
            public DeleteCommand(Resource r)
            {
                resource = r;
            }
            
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                DeleteResource dialog = new DeleteResource(resource);
            }
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
    
   
}
