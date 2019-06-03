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
    /// Interaction logic for ViewAllResources.xaml
    /// </summary>
    public partial class ViewAllResources : Window, INotifyPropertyChanged
    {
        public static ObservableCollection<ResourceTypeWithResources> TypesWithResources { get; set; }

        /*private ResourceUnit _unitFilter;

        public ResourceUnit UnitFilter
        {
            get { return _unitFilter; }
            set
            {
                _unitFilter = value;

                OnPropertyChanged("UnitFilter");
                OnPropertyChanged("TypesWithResourcesSearchResult");
            }
        }*/

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

        private void DoFilter(object sender, RoutedEventArgs e)
        {
            //OnPropertyChanged("UnitFilter");
            OnPropertyChanged("TypesWithResourcesSearchResult");
        }

        private void RemoveFilters(object sender, RoutedEventArgs e)
        {
            cmbUnit.SelectedItem = null;
            //OnPropertyChanged("UnitFilter");
            OnPropertyChanged("TypesWithResourcesSearchResult");
        }

        public ObservableCollection<ResourceTypeWithResources> TypesWithResourcesSearchResult
        {
            get
            {
                if (SearchText == null)
                {
                    SearchText = "";
                }
                
                ObservableCollection<ResourceTypeWithResources> result = new ObservableCollection<ResourceTypeWithResources>();
                ObservableCollection<Resource> resources = new ObservableCollection<Resource>();
                    
                foreach (ResourceTypeWithResources r in TypesWithResources)
                {
                    foreach (Resource res in r.Resources)
                    {
                        if (res.Name.ToUpper().Contains(SearchText.ToUpper()) || res.Id.ToUpper().Contains(SearchText.ToUpper()))
                        {
                            if (cmbUnit.SelectedItem == null)
                            {
                                resources.Add(res);
                            } else
                            {
                                if (res.Unit.Equals(cmbUnit.SelectedItem))
                                {
                                    resources.Add(res);
                                }
                            }                            
                        }
                    }                        

                    ResourceTypeWithResources newr = new ResourceTypeWithResources();
                    newr.Id = r.Id;
                    newr.Icon = r.Icon;
                    newr.Name = r.Name;
                    newr.Resources = resources;

                    if(resources.Count > 0)
                    {
                        result.Add(newr);
                    }

                    resources = new ObservableCollection<Resource>();
                }
                return result;                       
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

        public ViewAllResources()
        {          
            InitializeComponent();
            cmbUnit.ItemsSource = Enum.GetValues(typeof(ResourceUnit)).Cast<ResourceUnit>();
            this.DataContext = this;
            TypesWithResources = MainWindow.types;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Resource resource = (Resource)((Button)sender).Tag;
            EditResource dialog = new EditResource(resource);
            dialog.ShowDialog();
            OnPropertyChanged("TypesWithResourcesSearchResult");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
