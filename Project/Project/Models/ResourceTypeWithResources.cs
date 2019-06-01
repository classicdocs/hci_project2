using Project.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Input;

namespace Project.Models
{
    public class ResourceTypeWithResources : INotifyPropertyChanged
    { 
        private string id { get; set; }
        private string name { get; set; }
        private string icon { get; set; }
        private string description { get; set; }
        private ObservableCollection<Resource> resources { get; set; }

        public ResourceTypeWithResources() {
            Add = new AddResource(this);
        }

        public ResourceTypeWithResources(string id, string name, string icon, string description)
        {
            this.id = id;
            this.name = name;
            this.icon = icon;
            this.description = description;
            this.resources = new ObservableCollection<Resource>();
            Add = new AddResource(this);
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private AddResource _add;
        [ScriptIgnore]
        public AddResource Add
        {
            get
            {
                return _add;
            }
            set
            {
                if (_add != value)
                {
                    _add = value;
                    OnPropertyChanged("Add");
                }
            }
        }

    }

    public class AddResource : ICommand
    {
        private ResourceTypeWithResources resourceTypeWithResource;
        public AddResource(ResourceTypeWithResources r)
        {
            resourceTypeWithResource = r;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            AddNewResource dialog = new AddNewResource(resourceTypeWithResource);
            dialog.Show();
        }
    }
}
