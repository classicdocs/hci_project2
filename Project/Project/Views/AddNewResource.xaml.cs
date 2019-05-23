﻿using Project.Models;
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
        public ResourcePrice price { get; set; }
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
            InitializeData();
            this.DataContext = this;
           
        }

        private void InitializeData()
        {
            types = MainWindow.types;
            cmbFrequency.ItemsSource = Enum.GetValues(typeof(ResourceFrequency)).Cast<ResourceFrequency>();
            cmbPrice.ItemsSource = Enum.GetValues(typeof(ResourcePrice)).Cast<ResourcePrice>();
            id = "";
            name = "";
            description = "";
            icon = "";
            frequency = ResourceFrequency.Infrequent;
            price = ResourcePrice.Dollar;
            cmbFrequency.SelectedItem = frequency;
            cmbPrice.SelectedItem = price;
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
            price = (ResourcePrice)cmbPrice.SelectedItem;
            renewable = checkRenewable.IsChecked;

            if (id.Equals("") || name.Equals("") || type == null)
            {
                MessageBox.Show("You must fill all required fields");
                return;
            }

            bool ren = renewable.Value;
            Resource resource = new Resource(id, name, description, type, frequency, icon, ren, price);
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