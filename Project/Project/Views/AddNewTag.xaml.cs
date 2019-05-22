﻿using Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
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
    /// Interaction logic for AddNewTag.xaml
    /// </summary>
    public partial class AddNewTag : Window, INotifyPropertyChanged
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public AddNewTag()
        {
            InitializeComponent();
            this.DataContext = this;
            id = "";
            name = "";
            description = "";
            cmbColors.ItemsSource = typeof(Colors).GetProperties();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (id.Equals("") || name.Equals("") || cmbColors == null)
            {
                MessageBox.Show("You must fill all required fields");
                return;
            }
            Color selectedColor = (Color)(cmbColors.SelectedItem as PropertyInfo).GetValue(null, null);

            Tag tag = new Tag(id, name, selectedColor.ToString(),description);
            foreach (Tag t in MainWindow.tags)
            {
                if (t.Id == tag.Id)
                {
                    MessageBox.Show("Id you entered is already in use. Please choose another.");
                    return;
                }
                else if (t.Name == tag.Name)
                {
                    MessageBox.Show("Name you entered is already in use. Please choose another.");
                    return;
                }
                else if (t.Color == tag.Color)
                {
                    MessageBox.Show("Color you choose is already in use. Please choose another.");
                    return;
                }
            }
            MainWindow.tags.Add(tag);
            var json = new JavaScriptSerializer().Serialize(MainWindow.tags);

            using (StreamWriter sw = new StreamWriter("../../Data/tags.json"))
            {
                sw.Write(json);
            }
            this.Close();
            MessageBox.Show("You have successfully add new resource type.");
        }
    }
}
