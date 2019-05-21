using Microsoft.Win32;
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

        public AddNewType()
        {
            InitializeComponent();
            this.DataContext = this;
            id = "";
            name = "";
            fileName = "";
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
                MessageBox.Show("You must fill all required fields");
                return;
            }
            ResourceType type = new ResourceType(id, name, fileName, description);
            foreach(ResourceType rt in MainWindow.types)
            {
                if (rt.Id == type.Id)
                {
                    MessageBox.Show("Id you entered is already in use. Please try again");
                    return;
                } else if (rt.Name == type.Name)
                {
                    MessageBox.Show("Name you entered is already in use. Please try again");
                    return;
                } else if (rt.Icon == type.Icon)
                {
                    MessageBox.Show("Icon you choose is already in use. Please try again");
                    return;
                }
            }
            MainWindow.types.Add(type);
            this.Close();
            MessageBox.Show("You have successfully add new resource type");
        }

    }
}
