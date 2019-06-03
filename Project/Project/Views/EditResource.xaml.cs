using Microsoft.Win32;
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
        public ObservableCollection<ResourceTypeWithResources> types { get; set; }

        private string _icon;
        public string icon
        {
            get { return _icon; }
            set
            {
                if (_icon != value)
                {
                    _icon = value;
                    OnPropertyChanged();
                }
            }
        }

        public EditResource(Resource resource)
        {
            InitializeComponent();
            this.DataContext = this;
            InitializeData(resource);
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void InitializeData(Resource resource)
        {
            this.resource = resource;
            types = MainWindow.types;
            Frequency.ItemsSource = Enum.GetValues(typeof(ResourceFrequency)).Cast<ResourceFrequency>();
            Id.Text = resource.Id;
            Name.Text = resource.Name;
            Description.Text = resource.Description;
            ResourceTypeWithResources r = null;
            foreach(ResourceTypeWithResources rt in types)
            {
                if (rt.Id == resource.ResourceType.Id)
                {
                    r = rt;
                    break;
                }
            }
            ResourceType.SelectedItem = r;
            Frequency.SelectedItem = resource.Frequency;
            Icon.Source = new BitmapImage(new Uri(resource.Icon));
            icon = resource.Icon;
            Renewable.IsChecked = resource.Renewable;
            Unit.ItemsSource = Enum.GetValues(typeof(ResourceUnit)).Cast<ResourceUnit>();
            Unit.SelectedItem = resource.Unit;
            Price.Text = resource.Price;
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

            if (Id.Text.Equals("") || Name.Text.Equals("") || icon.Equals(""))
            {
                MessageBox.Show("You must fill all required fields");
                return;
            }

            foreach (ResourceTypeWithResources rt in MainWindow.types)
            {
                foreach (Resource res in rt.Resources)
                {
                    if (res.Id.Equals(Id.Text) && !Id.Text.Equals(res.Id))
                    {
                        MessageBox.Show("Id you entered is already in use. Please choose another.");
                        return;
                    }
                    else if (res.Name.Equals(Name.Text) && !Name.Text.Equals(res.Name))
                    {
                        MessageBox.Show("Name you entered is already in use. Please choose another.");
                        return;
                    }
                    else if (res.Icon.Equals(Icon.Source) && !Icon.Source.Equals(res.Icon))
                    {
                        MessageBox.Show("Icon you choose is already in use. Please choose another.");
                        return;
                    }
                }
            }

            editResource();
        }

        private void editResource()
        {
            Resource resourceToChange = null;
            ResourceType typeOfChangedResource = resource.ResourceType;
            foreach (ResourceTypeWithResources rt in MainWindow.types)
            {
                foreach (Resource res in rt.Resources)
                {
                    if (res == resource)
                    {
                        resourceToChange = res;
                        break;
                    }
                }
            }
            ResourceType resourceType = null;

            if (resourceToChange != null)
            {
                resourceToChange.Id = Id.Text;
                resourceToChange.Name = Name.Text;
                resourceToChange.Description = Description.Text;
                
                foreach(var rt in MainWindow.types)
                {
                    if (rt == ResourceType.SelectedItem)
                    {
                        resourceType = new ResourceType(rt);
                        break;
                    }
                }
                resourceToChange.ResourceType = resourceType;
                resourceToChange.Frequency = (ResourceFrequency)Frequency.SelectedItem;
                resourceToChange.Icon = icon;
                resourceToChange.Renewable = (bool)Renewable.IsChecked;
                resourceToChange.Unit = (ResourceUnit)Unit.SelectedItem;
                resourceToChange.Price = Price.Text;
            }

            if (resourceType.Id != typeOfChangedResource.Id)
            {
                Resource resourceToDelete = null;
                ResourceTypeWithResources r = null;
                foreach (ResourceTypeWithResources rt in MainWindow.types)
                {
                    foreach (Resource res in rt.Resources)
                    {
                        if (res == resource)
                        {
                            resourceToDelete = res;
                            r = rt;
                            break;
                        }
                    }
                }
                if (resourceToDelete != null && r != null)
                {
                    r.Resources.Remove(resourceToDelete);
                }
                foreach (ResourceTypeWithResources rt in MainWindow.types)
                {
                    if (rt.Id == resourceToChange.ResourceType.Id)
                    {
                        rt.Resources.Add(resourceToChange);
                    }
                }
            }
            refreshView(resourceToChange, typeOfChangedResource);
        }

        private void refreshView(Resource res, ResourceType type)
        {
            foreach (var rp in MainWindow.resources)
            {
                if (rp.resource.ResourceType.Id == type.Id && rp.resource.Id == res.Id)
                {
                    rp.resource.Id = res.Id;
                    rp.resource.Name = res.Name;
                    rp.resource.Description = res.Description;
                    rp.resource.ResourceType = res.ResourceType;
                    rp.resource.Frequency = res.Frequency; ;
                    rp.resource.Icon = res.Icon ;
                    rp.resource.Renewable = res.Renewable;
                    rp.resource.Unit = res.Unit;
                    rp.resource.Price = res.Price;
                    MainWindow.removeResourceFromMap(rp);
                    MainWindow.drawOneResource(rp);
                }
            }
            CollectionViewSource.GetDefaultView(MainWindow.types).Refresh();
            ReadWrite rw = new ReadWrite();
            rw.writeToFile("../../Data/types.json", MainWindow.types);
            rw.writeToFile("../../Data/resources.json", MainWindow.resources);
            this.Close();
            MessageBox.Show("You have successfully edited resource");
        }

        private void ChooseIcon_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            bool? res = fileDialog.ShowDialog();
            if (res.HasValue == res.Value)
            {
                icon = fileDialog.FileName;
                Icon.Source = new BitmapImage(new Uri(icon));
            }
        }

    }
}
