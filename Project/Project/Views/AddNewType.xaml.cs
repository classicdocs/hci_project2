﻿using Microsoft.Win32;
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
using Project.Models;
using System.Web.Script.Serialization;
using System.IO;
using Project.FileHandler;

namespace Project.Views
{
    /// <summary>
    /// Interaction logic for AddNewType.xaml
    /// </summary>
    public partial class AddNewType : Window, INotifyPropertyChanged
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        private string _fileName;
        public string fileName
        {
            get {return _fileName; }
            set
            {
                if (_fileName != value)
                {
                    _fileName = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _font;
        public int font
        {
            get { return _font; }
            set
            {
                if (_font != value)
                {
                    _font = value;
                    OnPropertyChanged();
                }
            }
        }

        public AddNewType()
        {
            InitializeComponent();
            this.DataContext = this;
            id = "";
            name = "";
            fileName = "";
            description = "";

            font = 40;

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void ChooseIcon_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            bool? res = fileDialog.ShowDialog();
            if (res.HasValue == res.Value)
            {
                fileName = fileDialog.FileName;
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

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (id.Equals("") || name.Equals("") || fileName.Equals(""))
            {
                MessageBox.Show("You must fill all required fields", "Fields lacking", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ResourceTypeWithResources type = new ResourceTypeWithResources(id, name, fileName, description);

            foreach(ResourceTypeWithResources rt in MainWindow.types)
            {
                if (rt.Id == type.Id)
                {
                    MessageBox.Show("Id you entered is already in use. Please choose another.", "Invalid id", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (rt.Name == type.Name)
                {
                    MessageBox.Show("Name you entered is already in use. Please choose another.", "Invalid name", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (rt.Icon == type.Icon)
                {
                    MessageBox.Show("Icon you choose is already in use. Please choose another one.", "Invalid icon", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            MainWindow.types.Add(type);
            ReadWrite rw = new ReadWrite();
            rw.writeToFile("../../Data/types.json", MainWindow.types);

            this.Close();
            MessageBox.Show("You have successfully add new resource type.", "Added resource", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    
}
