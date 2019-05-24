using Project.FileHandler;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project.Views
{
    /// <summary>
    /// Interaction logic for AddNewResourceDetails.xaml
    /// </summary>
    public partial class AddNewResourceDetails : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Tag> tags { get; set; }
        public string dateOfDiscovery { get; set; }
        private Resource resource;
        private Point point;

        public ResourcePoint resourcePoint { get; set; }

        private List<Tag> checkedTags = new List<Tag>();

        public AddNewResourceDetails(Resource res, Point p)
        {
            resource = res;
            point = p;
            tags = MainWindow.tags;
            InitializeComponent();
            this.DataContext = this;
            Id.Content = resource.Id;
            Name.Content = resource.Name;
            Description.Content = resource.Description;
            TypeOfResource.Content = resource.ResourceType.Name;
            Icon.Source = new BitmapImage(new Uri(resource.Icon));
            Renewable.Content = resource.Renewable ? "Yes" : "No";
            Frequency.Content = resource.Frequency;
            Unit.Content = resource.Unit;
            Price.Content = resource.Price;
            dateOfDiscovery = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.Write("ASF");

            if (dateOfDiscovery.Equals(""))
                this.resource.DateOfDiscovery = "Unknown";
            else
                this.resource.DateOfDiscovery = dateOfDiscovery;
            this.resource.StrategicImportance = (bool)StrategicImportance.IsChecked;
            this.resource.CurrentlyExploited = (bool)CurrentlyExploited.IsChecked;
            this.resource.Tags = checkedTags;

            ResourcePoint rp = new ResourcePoint(resource, point);
            resourcePoint = rp;
            MainWindow.resources.Add(rp);
            ReadWrite rw = new ReadWrite();
            rw.writeToFile("../../Data/resources.json", MainWindow.resources);

            MainWindow.addNewResourceDialog = true;
            this.Close();

        }

        private void tagsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Tag item in e.RemovedItems)
            {
                checkedTags.Remove(item);
            }

            foreach (Tag item in e.AddedItems)
            {
                checkedTags.Add(item);
            }
        }
    }
}
