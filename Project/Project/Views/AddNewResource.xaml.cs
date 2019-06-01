using Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
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
using Microsoft.Win32;
using System.Web.Script.Serialization;
using System.IO;

namespace Project.Views
{
    /// <summary>
    /// Interaction logic for AddNewResource.xaml
    /// </summary>
    public partial class AddNewResource : Window, INotifyPropertyChanged
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public ObservableCollection<ResourceTypeWithResources> types { get; set; }
        public ResourceType type { get; set; }
        public ResourceFrequency frequency { get; set; }
        public ResourceUnit unit { get; set; }
        public string price { get; set; }
        public bool? renewable { get; set; }
        public string _icon;
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

        public AddNewResource()
        {
            InitializeComponent();
            InitializeData(null);
            this.DataContext = this;
           
        }

        public AddNewResource(ResourceTypeWithResources r)
        {
            InitializeComponent();
            InitializeData(r);
            this.DataContext = this;

        }

        private void InitializeData(ResourceTypeWithResources resourceType)
        {
            if (resourceType == null)
                types = MainWindow.types;
            else
            {
                ObservableCollection<ResourceTypeWithResources> list = new ObservableCollection<ResourceTypeWithResources>();
                list.Add(resourceType);
                types = list;
                cmbType.SelectedItem = resourceType;
            }

            cmbFrequency.ItemsSource = Enum.GetValues(typeof(ResourceFrequency)).Cast<ResourceFrequency>();
            id = "";
            name = "";
            description = "";
            icon = "";
            price = "";
            frequency = ResourceFrequency.Infrequent;
            unit = ResourceUnit.Kilogram;
            cmbUnit.ItemsSource = Enum.GetValues(typeof(ResourceUnit)).Cast<ResourceUnit>();
            cmbUnit.SelectedItem = unit;
            cmbFrequency.SelectedItem = frequency;
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
            ResourceTypeWithResources rs =  (ResourceTypeWithResources)cmbType.SelectedItem;
            type =  new ResourceType(rs);

            frequency = (ResourceFrequency)cmbFrequency.SelectedItem;
            unit = (ResourceUnit)cmbUnit.SelectedItem;
            renewable = checkRenewable.IsChecked;

            if (id.Equals("") || name.Equals("") || price.Equals("") || type == null)
            {
                MessageBox.Show("You must fill all required fields");
                return;
            }

            bool ren = renewable.Value;
            PageEnum page = (PageEnum) 1;       // za pocetak

            Canvas currentCanvas = MainWindow.getCanvas();
            switch (currentCanvas.Name)
            {
                case "Cnv":
                {
                    page = (PageEnum)0;
                    break;
                }
                case "Cnv1":
                {
                    page = (PageEnum)1;
                    break;
                }
                case "Cnv2":
                {
                    page = (PageEnum)2;
                    break;
                }
                case "Cnv3":
                {
                    page = (PageEnum)3;
                    break;
                }
            }

            Resource resource = new Resource(id, name, description, type, frequency, unit, icon, ren, price, page);
            foreach(ResourceTypeWithResources rt in MainWindow.types)
            {
                foreach(Resource r in rt.Resources)
                {
                    if (r.Id == resource.Id)
                    {
                        MessageBox.Show("Id you entered is already in use. Please choose another.");
                        return;
                    }
                    else if (r.Name == resource.Name)
                    {
                        MessageBox.Show("Name you entered is already in use. Please choose another.");
                        return;
                    }
                    else if (r.Icon == resource.Icon)
                    {
                        MessageBox.Show("Icon you choose is already in use. Please choose another.");
                        return;
                    }
                }
            }
            
            foreach(ResourceTypeWithResources rt in MainWindow.types)
            {
                if (rt.Id.Equals(resource.ResourceType.Id) && rt.Name.Equals(resource.ResourceType.Name))
                {
                    rt.Resources.Add(resource);

                    var json = new JavaScriptSerializer().Serialize(MainWindow.types);

                    using (StreamWriter sw = new StreamWriter("../../Data/types.json"))
                    {
                        sw.Write(json);
                    }

                    break;
                }
            }
            
            this.Close();
            MessageBox.Show("You have successfully add new resource.");
        }
       

        private void ChooseIcon_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            bool? res = fileDialog.ShowDialog();
            if (res.HasValue == res.Value)
            {
                icon = fileDialog.FileName;
            }
        }
    }

   
}
