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

        private string minPrice;

        private string maxPrice;
        
        private string _searchText;

        public string MinPrice
        {
            get { return minPrice; }
            set
            {
                minPrice = value;

                OnPropertyChanged("MinPrice");               
            }
        }

        public string MaxPrice
        {
            get { return maxPrice; }
            set
            {
                maxPrice = value;

                OnPropertyChanged("MaxPrice");
            }
        }

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
            if (MaxPrice != null)
            {
                try
                {
                    int max = Int32.Parse(MaxPrice);
                } catch (FormatException f)
                {
                    MessageBox.Show("Invalid input for maximum price!");
                    return;
                }                
            }

            if (MinPrice != null)
            {
                try
                {
                    int min = Int32.Parse(MinPrice);
                }
                catch (FormatException f)
                {
                    MessageBox.Show("Invalid input for minimum price!");
                    return;
                }
            }

            OnPropertyChanged("TypesWithResourcesSearchResult");
        }

        private void RemoveFilters(object sender, RoutedEventArgs e)
        {
            cmbUnit.SelectedItem = null;
            cmbFrequency.SelectedItem = null;
            cmbRenewable.SelectedItem = null;
            MinPrice = null;
            MaxPrice = null;
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
                        bool canAdd = false;
                        if (res.Name.ToUpper().Contains(SearchText.ToUpper()) || res.Id.ToUpper().Contains(SearchText.ToUpper()))
                        {
                            canAdd = true;                            
                            if (cmbUnit.SelectedItem != null)
                            {
                                if (!res.Unit.Equals(cmbUnit.SelectedItem))
                                {
                                    canAdd = false;
                                }
                            }

                            if (cmbFrequency.SelectedItem != null)
                            {
                                if (!res.Frequency.Equals(cmbFrequency.SelectedItem))
                                {
                                    canAdd = false;
                                }
                            }

                            if (cmbRenewable.SelectedItem != null)
                            {
                                bool renewableFilter = false;
                                if (cmbRenewable.SelectedItem.Equals("Only renewable"))
                                {
                                    renewableFilter = true;
                                }
                                if (res.Renewable != renewableFilter)
                                {
                                    canAdd = false;
                                }
                                
                            }

                            if (MaxPrice != null)
                            {
                                if(Int32.Parse(res.Price) > Int32.Parse(MaxPrice))
                                {
                                    canAdd = false;
                                }
                            }

                            if (MinPrice != null)
                            {
                                if (Int32.Parse(res.Price) < Int32.Parse(MinPrice))
                                {
                                    canAdd = false;
                                }
                            }
                        }
                        if(canAdd)
                        {
                            resources.Add(res);
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
            cmbFrequency.ItemsSource = Enum.GetValues(typeof(ResourceFrequency)).Cast<ResourceFrequency>();
            cmbRenewable.Items.Add("Only renewable");
            cmbRenewable.Items.Add("Only nonrenewable");
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
            Resource resource = (Resource)((Button)sender).Tag;
            EditResource dialog = new EditResource(resource);
            dialog.ShowDialog();
            OnPropertyChanged("TypesWithResourcesSearchResult");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Resource resource = (Resource)((Button)sender).Tag;
            DeleteResource dialog = new DeleteResource(resource);
            OnPropertyChanged("TypesWithResourcesSearchResult");
        }

        private void View_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(
                    e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }
    }
}
