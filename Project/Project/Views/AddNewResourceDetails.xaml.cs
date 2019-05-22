using System;
using System.Collections.Generic;
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
    public partial class AddNewResourceDetails : Window
    {

        //public string id { get; set; }
        //public string name { get; set; }
        //public string description { get; set; }
        //public ResourceType type { get; set; }
        //public ResourceFrequency frequency { get; set; }
        //public ResourcePrice price { get; set; }
        //public bool? renewable { get; set; }
        //public string _icon;

        public AddNewResourceDetails()
        {
            InitializeComponent();
        }

        //private void InitializeData()
        //{
        //    types = MainWindow.types;
        //    cmbFrequency.ItemsSource = Enum.GetValues(typeof(ResourceFrequency)).Cast<ResourceFrequency>();
        //    cmbPrice.ItemsSource = Enum.GetValues(typeof(ResourcePrice)).Cast<ResourcePrice>();
        //    id = "";
        //    name = "";
        //    description = "";
        //    icon = "";
        //    frequency = ResourceFrequency.Infrequent;
        //    price = ResourcePrice.Dollar;
        //    cmbFrequency.SelectedItem = frequency;
        //    cmbPrice.SelectedItem = price;
        //}

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged(string name = null)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(name));
        //    }
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
