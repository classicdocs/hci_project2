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
    /// Interaction logic for ViewAllTypes.xaml
    /// </summary>
    public partial class ViewAllTypes : Window, INotifyPropertyChanged
    {
        public static ObservableCollection<ResourceTypeWithResources> TypesWithResources { get; set; }

        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;

                OnPropertyChanged("SearchText");
                OnPropertyChanged("TypesWithResourcesSearchResult");
            }
        }

        public IEnumerable<ResourceTypeWithResources> TypesWithResourcesSearchResult
        {
            get
            {
                if (SearchText == null)
                {
                    return TypesWithResources;
                }

                return TypesWithResources.Where(x => x.Name.ToUpper().Contains(SearchText.ToUpper()) 
                || x.Id.ToUpper().Contains(SearchText.ToUpper()));                
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
