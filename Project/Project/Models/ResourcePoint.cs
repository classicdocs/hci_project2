using Project.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Project.Models
{
    public class ResourcePoint : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public Resource resource;
        public Point point;    

        public ResourcePoint()
        {

        }

        public ResourcePoint(Resource res, Point p)
        {
            resource = res;
            point = p;
        }

        public string ResourceName
        {
            get
            {
                return resource.Name;
            }
            set
            {
                if (value != resource.Name)
                {
                    resource.Name = value;
                    OnPropertyChanged("ResourceName");
                }
            }
        }

    }
}
