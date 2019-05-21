using Project.Models;
using Project.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static HashSet<ResourceType> types { get; set; }
        public static HashSet<Tag> tags { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            types = new HashSet<ResourceType>();
            tags = new HashSet<Tag>();
        }

        private void AllResources_Click(object sender, RoutedEventArgs e)
        {
            var s = new ViewAllResources();
            s.Show();
        }

        private void AllTypes_Click(object sender, RoutedEventArgs e)
        {
            var s = new ViewAllTypes();
            s.Show();
        }

        private void AllTags_Click(object sender, RoutedEventArgs e)
        {
            var s = new ViewAllTags();
            s.Show();
        }

        private void NewResource_Click(object sender, RoutedEventArgs e)
        {
            var s = new AddNewResource();
            s.Show();
        }

        private void NewType_Click(object sender, RoutedEventArgs e)
        {
            var s = new AddNewType();
            s.Show();
        }

        private void NewTag_Click(object sender, RoutedEventArgs e)
        {
            var s = new AddNewTag();
            s.Show();
        }

        
    }
}
