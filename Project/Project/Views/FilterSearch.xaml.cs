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

        public FilterSearch()
        {
            InitializeComponent();
            InitializeData();
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
            e.Handled = regex.IsMatch(e.Text) ;
        }
        private void EnterFilter_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<ResourcePoint> currentlyShown = MainWindow.searchShownResources;
            ObservableCollection<ResourcePoint> rpToSHow = MainWindow.searchShownResources;

            Tag tag = (Tag) cmbTags.SelectedValue;
            bool isRenewable = (bool)checkRenewable.IsChecked;
            int minPrice = -1;
            int maxPrice = -1;

            if (cmbFrequency.SelectedValue != null)
            {
                frequency = (ResourceFrequency)cmbFrequency.SelectedValue;
            }

            if(minPriceBox.Text != null)
            {
                minPrice = Int32.Parse(minPriceBox.Text);
            }

            if (maxPriceBox.Text != null)
            {
                maxPrice = Int32.Parse(maxPriceBox.Text);
            }


            foreach (ResourcePoint rp in currentlyShown)
            {
                if (tag == null && cmbFrequency.SelectedValue == null)
                {
                    /*Ukoliko nije nista izabrao*/
                    MessageBox.Show("There were no filter parameters, so there are no changes on map.", "No parameters", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (tag != null && cmbFrequency.SelectedValue == null)
                {

                    foreach (Tag t in rp.resource.Tags)
                    {
                        if (t.Name.Equals(tag.Name) && rp.resource.Renewable == isRenewable)
                        {
                            if(checkPrice(rp, minPrice, maxPrice))
                            {
                                rpToSHow.Add(rp);
                            }
                        }
                    }
                }
                else if (tag != null && cmbFrequency.SelectedValue != null)
                {
                    foreach (Tag t in rp.resource.Tags)
                    {
                        if (t.Name.Equals(tag.Name) && rp.resource.Renewable == isRenewable && rp.resource.Frequency == frequency)
                        {
                            if (checkPrice(rp, minPrice, maxPrice))
                            {
                                rpToSHow.Add(rp);
                            }
                        }
                    }
                }
                else if (tag == null && cmbFrequency.SelectedValue != null)
                {
                    if (rp.resource.Renewable == isRenewable && rp.resource.Frequency == frequency)
                    {
                        if (checkPrice(rp, minPrice, maxPrice))
                        {
                            rpToSHow.Add(rp);
                        }
                    }
                } 
            }

            int a = 5;
        }
        private bool checkPrice(ResourcePoint rp, int minPrice, int maxPrice)
        { 
            if(rp.resource.Price == null)
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
                if(Int32.Parse(rp.resource.Price) < maxPrice)
                {
                    return true;
                }else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
