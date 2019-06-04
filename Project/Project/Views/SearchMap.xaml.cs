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
    /// Interaction logic for SearchMap.xaml
    /// </summary>
    public partial class SearchMap : Window, INotifyPropertyChanged
    {
        public static ObservableCollection<ResourceTypeWithResources> types { get; set; }

        public static ObservableCollection<Tag> tags { get; set; }

        public static ObservableCollection<Resource> resources { get; set; }

        public SearchMap()
        {
            InitializeComponent();
            InitializeData();
            this.DataContext = this;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);

        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void InitializeData()
        {
            types = MainWindow.types;

            if (resources == null)
            {
                resources = new ObservableCollection<Resource>();
                foreach (ResourceTypeWithResources rtwr in types)
                {
                    foreach (Resource r in rtwr.Resources)
                    {
                        resources.Add(r);
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void EnterSearch_Click(object sender, RoutedEventArgs e)
        {

            ResourceTypeWithResources selectedType = (ResourceTypeWithResources)cmbType.SelectedValue;
            Resource selectedResource = (Resource)cmbResources.SelectedValue;

            ObservableCollection<ResourceTypeWithResources> allTypes = MainWindow.types;
            ObservableCollection<ResourcePoint> allResourcePoints = MainWindow.resources;

            ObservableCollection<Resource> resourcesToShow = new ObservableCollection<Resource>();
            ObservableCollection<ResourcePoint> resourcePointsToShow = new ObservableCollection<ResourcePoint>();

            if (selectedType == null && selectedResource == null)
            {
                /*Ukoliko nije nista izabrao*/
                MessageBox.Show("There were no search parameters, so there are no changes on map.", "No parameters", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (selectedType != null && selectedResource == null)
            {
                /*Ukoliko je izabrao samo tip*/
                foreach (ResourceTypeWithResources rtwr in allTypes)
                {
                    if (rtwr.Name.Equals(selectedType.Name))
                    {
                        resourcesToShow = rtwr.Resources;
                        break;
                    }
                }
            }
            else if (selectedResource != null)
            {
                /*Ukoliko je izabrao konkretan resurs*/
                foreach (ResourcePoint resourcePoint in allResourcePoints)
                {
                    if (resourcePoint.resource.Name == selectedResource.Name)
                    {
                        resourcePointsToShow.Add(resourcePoint);
                    }
                }
            }



            /*Smestam u resourcePointsToShow sve sto treba da ostane na mapi*/
            foreach (ResourcePoint rp in allResourcePoints)
            {
                foreach (Resource r in resourcesToShow)
                {
                    if (r.Name == rp.resource.Name)
                    {
                        resourcePointsToShow.Add(rp);
                    }
                }
            }

            foreach (ResourcePoint rp in MainWindow.resources)
            {
                if (!resourcePointsToShow.Contains(rp))
                {
                    //Canvas canvas = MainWindow.getCanvas();
                    List<Canvas> canvases = MainWindow.getAllCanvases();
                    //switch (rp.OnPage)
                    //{
                    //    case 
                    //    PageEnum.First: 
                    //        {
                    //            canvas = ((MainWindow)Application.Current.MainWindow).Cnv;
                    //            break;
                    //        }
                    //    case
                    //    PageEnum.Second:
                    //        {
                    //            canvas = ((MainWindow)Application.Current.MainWindow).Cnv2;
                    //            break;
                    //        }
                    //    case
                    //    PageEnum.Third:
                    //        {
                    //            canvas = ((MainWindow)Application.Current.MainWindow).Cnv3;
                    //            break;
                    //        }
                    //    case
                    //    PageEnum.Fourth:
                    //        {
                    //            canvas = ((MainWindow)Application.Current.MainWindow).Cnv4;
                    //            break;
                    //        }
                    //}


                    foreach (Canvas canvas in canvases)
                    {
                        var element = canvas.InputHitTest(rp.point) as UIElement;
                        UIElement parent;
                        while (element != null &&
                        (parent = VisualTreeHelper.GetParent(element) as UIElement) != canvas)
                        {
                            element = parent;
                        }

                        if (element != null)
                        {
                            canvas.Children.Remove(element);
                        }
                    }
                }
            }
            MainWindow.searchIsActive = true;
            MainWindow.showCloseBtn();
            MainWindow.searchShownResources = resourcePointsToShow;


            cmbType.SelectedValue = null;
            cmbResources.SelectedValue = null;
            this.Close();

        }



        private void types_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                ResourceTypeWithResources rtwr = (ResourceTypeWithResources)e.AddedItems[0];
                resources = rtwr.Resources;
                cmbResources.ItemsSource = resources;
            }
        }
    }
}
