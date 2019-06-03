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
        private PageEnum onPage { get; set; }

        public ResourcePoint()
        {

        }

        public ResourcePoint(Resource res, Point p, PageEnum onPage)
        {
            resource = res;
            point = p;
            this.onPage = onPage;
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

        public PageEnum OnPage
        {
            get { return onPage; }
            set { onPage = value; }
        }
    }

    public enum PageEnum
    {
        First, Second, Third, Fourth, NoInstance
    };
}
