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

using System.Text.RegularExpressions;

namespace Project.Views
{
    /// <summary>
    /// Interaction logic for FilterSearch.xaml
    /// </summary>
    public partial class FilterSearch : Window, INotifyPropertyChanged
    {

        public ObservableCollection<Tag> tags { get; set; }
        public ResourceFrequency frequency { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private string minPrice;

        private string maxPrice;

        public string MinPrice
        {
            get { return minPrice; }
            set
            {
                minPrice = value;

                OnPropertyChanged("MinPrice");
            }
        }

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
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



        public FilterSearch()
        {
            InitializeComponent();
            InitializeData();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void InitializeData()
        {
            tags = MainWindow.tags;
            cmbTags.ItemsSource = tags;
            cmbFrequency.ItemsSource = Enum.GetValues(typeof(ResourceFrequency)).Cast<ResourceFrequency>();

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void EnterFilter_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<ResourcePoint> currentlyShown = MainWindow.searchShownResources;
            ObservableCollection<ResourcePoint> rpToSHow = new ObservableCollection<ResourcePoint>();

            Tag tag = (Tag)cmbTags.SelectedValue;
            bool isRenewable = (bool)checkRenewable.IsChecked;
            int min = 0, max = 0;


            if (cmbFrequency.SelectedValue != null)
            {
                frequency = (ResourceFrequency)cmbFrequency.SelectedValue;
            }


            try
            {
                max = Int32.Parse(maxPriceBox.Text);
            }
            catch (FormatException f)
            {
                if (!maxPriceBox.Text.Equals(""))
                {
                    MessageBox.Show("Invalid input for maximum price!", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }else
                {
                    max = Int32.MaxValue;
                }
            }

            try
            {
                min = Int32.Parse(minPriceBox.Text);
            }
            catch (FormatException f)
            {
                if (!minPriceBox.Text.Equals(""))
                {
                    MessageBox.Show("Invalid input for minimum price!", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    min = -2;
                }
            }

            OnPropertyChanged("TypesWithResourcesSearchResult");


            foreach (ResourcePoint rp in currentlyShown)
            {
                if (tag == null && cmbFrequency.SelectedValue == null)
                {
                    /*Ukoliko nije nista izabrao*/
                    if (rp.resource.Renewable == isRenewable && rp.resource.Frequency == frequency)
                    {
                        if (checkPrice(rp, min, max))
                        {
                            rpToSHow.Add(rp);
                        }
                    }
                    //MessageBox.Show("There were no filter parameters, so there are no changes on map.", "No parameters", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (tag != null && cmbFrequency.SelectedValue == null)
                {
                    /*Ukoliko je izabrao tag ali ne i frekvenciju*/
                    foreach (Tag t in rp.resource.Tags)
                    {
                        if (t.Name.Equals(tag.Name) && rp.resource.Renewable == isRenewable)
                        {
                            if (checkPrice(rp, min, max))
                            {
                                rpToSHow.Add(rp);
                            }
                        }
                    }
                }
                else if (tag != null && cmbFrequency.SelectedValue != null)
                {
                    /*Ukoliko je izabrao tag i frekvenciju*/
                    foreach (Tag t in rp.resource.Tags)
                    {
                        if (t.Name.Equals(tag.Name) && rp.resource.Renewable == isRenewable && rp.resource.Frequency == frequency)
                        {
                            if (checkPrice(rp, min, max))
                            {
                                rpToSHow.Add(rp);
                            }
                        }
                    }
                }
                else if (tag == null && cmbFrequency.SelectedValue != null)
                {
                    /*Ukoliko nije izabrao tag ali jeste frekvenciju*/
                    if (rp.resource.Renewable == isRenewable && rp.resource.Frequency == frequency)
                    {
                        if (checkPrice(rp, min, max))
                        {
                            rpToSHow.Add(rp);
                        }
                    }
                }
            }

            foreach (ResourcePoint rp in MainWindow.searchShownResources)
            {
                if (!rpToSHow.Contains(rp))
                {
                    //Canvas canvas = MainWindow.getCanvas();
                    List<Canvas> canvases = MainWindow.getAllCanvases();
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
            this.Close();

        }

        private bool checkPrice(ResourcePoint rp, int minPrice, int maxPrice)
        {
            if (rp.resource.Price == null)
            {
                return true;
            }


            if (minPrice == -1 && maxPrice == -1)
            {
                return true;
            }
            else if (minPrice != -1 && maxPrice == -1)
            {

                if (Int32.Parse(rp.resource.Price) > minPrice)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else if (minPrice != -1 && maxPrice != -1)
            {
                if (Int32.Parse(rp.resource.Price) > minPrice && Int32.Parse(rp.resource.Price) < maxPrice)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (minPrice == -1 && maxPrice != -1)
            {
                if (Int32.Parse(rp.resource.Price) < maxPrice)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
