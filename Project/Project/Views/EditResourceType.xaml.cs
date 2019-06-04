using Microsoft.Win32;
using Project.FileHandler;
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
    /// Interaction logic for EditResourceType.xaml
    /// </summary>
    public partial class EditResourceType : Window, INotifyPropertyChanged
    {
        private ResourceTypeWithResources resourceType;


        public EditResourceType(ResourceTypeWithResources rt)
        {
            resourceType = rt;
            InitializeComponent();
            this.DataContext = this;
            Id.Text = rt.Id;
            Name.Text = rt.Name;
            FileName.Content = rt.Icon;
            Description.Text = rt.Description;
            Icon.Source = new BitmapImage(new Uri(rt.Icon));
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
        

        private string _fileName;
        public string fileName
        {
            get { return _fileName; }
            set
            {
                if (_fileName != value)
                {
                    _fileName = value;
                    OnPropertyChanged();
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

        private void ChooseIcon_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            bool? res = fileDialog.ShowDialog();
            if (res.HasValue == res.Value)
            {
                fileName = fileDialog.FileName;
                Icon.Source = new BitmapImage(new Uri(fileName));
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Id.Text.Equals("") || Name.Text.Equals("") || FileName.Content.Equals(""))
            {
                MessageBox.Show("You must fill all required fields");
                return;
            }

            foreach (ResourceTypeWithResources rt in MainWindow.types)
            {
                if (rt.Id.Equals(Id.Text) && !Id.Text.Equals(resourceType.Id))
                {
                    MessageBox.Show("Id you entered is already in use. Please choose another.");
                    return;
                }
                else if (rt.Name.Equals(Name.Text) && !Name.Text.Equals(resourceType.Name))
                {
                    MessageBox.Show("Name you entered is already in use. Please choose another.");
                    return;
                }
                else if (rt.Icon.Equals(FileName.Content) && !FileName.Content.Equals(resourceType.Icon))
                {
                    MessageBox.Show("Icon you choose is already in use. Please choose another.");
                    return;
                }
            }
            foreach (var rt in MainWindow.types)
            {
                if (rt.Id == resourceType.Id)
                {
                    rt.Id = Id.Text;
                    rt.Name = Name.Text;
                    rt.Icon = (string) FileName.Content;
                    rt.Description = Description.Text;

                    foreach(var res in rt.Resources)
                    {
                        res.ResourceType.Id = Id.Text;
                        res.ResourceType.Name = Name.Text;
                        res.ResourceType.Icon = (string)FileName.Content;
                        res.ResourceType.Description = Description.Text;
                    }

                    CollectionViewSource.GetDefaultView(MainWindow.types).Refresh();

                    foreach(var rp in MainWindow.resources)
                    {
                        if (rp.resource.ResourceType.Id == rt.Id)
                        {
                            rp.resource.ResourceType.Id = Id.Text;
                            rp.resource.ResourceType.Name = Name.Text;
                            rp.resource.ResourceType.Icon = (string)FileName.Content;
                            rp.resource.ResourceType.Description = Description.Text;
                            MainWindow.removeResourceFromMap(rp);
                            MainWindow.drawOneResource(rp);
                        }
                    }

                    ReadWrite rw = new ReadWrite();
                    rw.writeToFile("../../Data/types.json", MainWindow.types);
                    rw.writeToFile("../../Data/resources.json", MainWindow.resources);
                    this.Close();
                    MessageBox.Show("You have successfully edited resource type");
                }
            }
            


            
        }
    }
}
