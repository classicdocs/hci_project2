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
            Edit = new EditResourceTypeCommand(this);
            Delete = new DeleteResourceTypeCommand(this);
        }

        public ResourceTypeWithResources(string id, string name, string icon, string description)
        {
            this.id = id;
            this.name = name;
            this.icon = icon;
            this.description = description;
            this.resources = new ObservableCollection<Resource>();
            Add = new AddResource(this);
            Edit = new EditResourceTypeCommand(this);
            Delete = new DeleteResourceTypeCommand(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }     

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
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
            get
            {
                return icon;
            }
            set
            {
                if (value != icon)
                {
                    icon = value;
                    OnPropertyChanged("Icon");
                }
            }
        }


        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public ObservableCollection<Resource> Resources
        {
            get { return resources; }
            set { resources = value; }
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

        private EditResourceTypeCommand _edit;
        [ScriptIgnore]
        public EditResourceTypeCommand Edit
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
        private DeleteResourceTypeCommand _delete;
        [ScriptIgnore]
        public DeleteResourceTypeCommand Delete
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
    public class EditResourceTypeCommand : ICommand
    {
        private ResourceTypeWithResources rt { get; set; }
        public EditResourceTypeCommand(ResourceTypeWithResources rt)
        {
            this.rt = rt;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            EditResourceType dialog = new EditResourceType(rt);
            dialog.Show();
        }
    }

    public class DeleteResourceTypeCommand : ICommand
    {
        private ResourceTypeWithResources resourceType { get; set; }
        public DeleteResourceTypeCommand(ResourceTypeWithResources rt)
        {
            resourceType = rt;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            DeleteResourceType msgBox = new DeleteResourceType(resourceType);
        }
    }
}
