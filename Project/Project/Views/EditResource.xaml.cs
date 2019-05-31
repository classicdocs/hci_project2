using Project.FileHandler;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for EditResource.xaml
    /// </summary>
    public partial class EditResource : Window, INotifyPropertyChanged
    {
        private Resource resource { get; set; }

        public ObservableCollection<Tag> tags { get; set; }
        public string dateOfDiscovery { get; set; }

        private List<Tag> checkedTags = new List<Tag>();
       
        public EditResource(Resource resource)
        {
            InitializeComponent();
            this.DataContext = this;
            InitializeData(resource);
        }

        private void InitializeData(Resource resource)
        {
            this.resource = resource;
            tags = MainWindow.tags;
            Id.Content = resource.Id;
            Name.Content = resource.Name;
            Description.Content = resource.Description;
            TypeOfResource.Content = resource.ResourceType.Name;
            Icon.Source = new BitmapImage(new Uri(resource.Icon));
            Renewable.Content = resource.Renewable ? "Yes" : "No";
            Frequency.Content = resource.Frequency;
            StrategicImportance.IsChecked = resource.StrategicImportance;
            CurrentlyExploited.IsChecked = resource.CurrentlyExploited;
            Unit.Content = resource.Unit;
            Price.Content = resource.Price;
            dateOfDiscovery = resource.DateOfDiscovery;
            foreach (Tag tag in tags) {
                foreach(Tag tag2 in resource.Tags)
                {
                    if (tag.Id == tag2.Id)
                    {
                        tagsList.SelectedItems.Add(tag);
                    }

                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.resource.StrategicImportance = (bool)StrategicImportance.IsChecked;
            this.resource.CurrentlyExploited = (bool)CurrentlyExploited.IsChecked;
            this.resource.Tags = checkedTags;
            if (dateOfDiscovery.Equals(""))
                this.resource.DateOfDiscovery = "Unknown";
            else
                this.resource.DateOfDiscovery = dateOfDiscovery;
            foreach(ResourcePoint rp in MainWindow.resources)
            {
                if (rp.resource.Id == resource.Id)
                {
                    rp.resource = resource;
                    ReadWrite rw = new ReadWrite();
                    rw.writeToFile("../../Data/resources.json", MainWindow.resources);
                    MainWindow.drawResources();
                    this.Close();
                    MessageBox.Show("You have successfully edited resource");
                }
            }
            
        }

    }
}
