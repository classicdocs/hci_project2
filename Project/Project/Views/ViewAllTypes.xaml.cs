using Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ViewAllTypes.xaml
    /// </summary>
    public partial class ViewAllTypes : Window
    {
        public static ObservableCollection<ResourceTypeWithResources> TypesWithResources { get; set; }

        public ViewAllTypes()
        {
            InitializeComponent();
            this.DataContext = this;
            TypesWithResources = MainWindow.types;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            ResourceTypeWithResources rt = (ResourceTypeWithResources)((Button)sender).Tag;
            EditResourceType dialog = new EditResourceType(rt);
            dialog.Show();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ResourceTypeWithResources rt = (ResourceTypeWithResources)((Button)sender).Tag;
            DeleteResourceType dialog = new DeleteResourceType(rt);
        }
    }
}
