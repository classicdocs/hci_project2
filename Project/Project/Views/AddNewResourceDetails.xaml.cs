using Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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
        public string dateOfDiscovery { get; set; }
        private Resource resource;
        private Point point;

        public AddNewResourceDetails(Resource res, Point p)
        {
            resource = res;
            point = p;
            InitializeComponent();
            this.DataContext = this;

            Id.Content = resource.Id;
            Name.Content = resource.Name;
            Description.Content = resource.Description;
            TypeOfResource.Content = resource.ResourceType.Name;
            Icon.Source = new BitmapImage(new Uri(resource.Icon));
            Renewable.Content = resource.Renewable ? "Yes" : "No";
            Frequency.Content = resource.Frequency; 
            cmbUnit.ItemsSource = Enum.GetValues(typeof(ResourceUnit)).Cast<ResourceUnit>();
            cmbUnit.SelectedItem = ResourceUnit.Kilogram;
            cmbTags.ItemsSource = MainWindow.tags;
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
            if (dateOfDiscovery.Equals(""))
            {
                MessageBox.Show("You must fill all required fields.");
            }

            this.resource.Unit = (ResourceUnit) cmbUnit.SelectedItem;
            this.resource.DateOfDiscovery = dateOfDiscovery;
            this.resource.StrategicImportance = (bool) StrategicImportance.IsChecked;
            this.resource.CurrentlyExploited = (bool) CurrentlyExploited.IsChecked;

            ResourcePoint rp = new ResourcePoint(resource, point);
            MainWindow.resources.Add(rp);
            this.Close();
            MessageBox.Show("You have successfully add new resource on map.");
        }
    }
}
