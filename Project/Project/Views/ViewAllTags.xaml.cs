using Project.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for ViewAllTags.xaml
    /// </summary>
    public partial class ViewAllTags : Window, INotifyPropertyChanged
    {
        public static ObservableCollection<Tag> allTags { get; set; }

        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;

                OnPropertyChanged("SearchText");
                OnPropertyChanged("allTagsSearchResult");
            }
        }

        public ObservableCollection<Tag> allTagsSearchResult
        {
            get
            {
                if (SearchText == null)
                {
                    SearchText = "";
                }

                ObservableCollection<Tag> result = new ObservableCollection<Tag>();                

                foreach (Tag t in allTags)
                {
                    bool canAdd = false;
                    if (t.Name.ToUpper().Contains(SearchText.ToUpper()) || t.Id.ToUpper().Contains(SearchText.ToUpper()))
                    {
                        canAdd = true;
                       
                        if (cmbColors.SelectedItem != null)
                        {
                            Color selectedColor = (Color)(cmbColors.SelectedItem as PropertyInfo).GetValue(null, null);                            
                            if (!t.Color.Equals(selectedColor.ToString()))
                            {
                                canAdd = false;
                            }
                        }                        
                    }                                        

                    if (canAdd)
                    {
                        result.Add(t);
                    }                                                                                                 
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

        public ViewAllTags()
        {
            InitializeComponent();
            this.DataContext = this;
            allTags = MainWindow.tags;
            cmbColors.ItemsSource = typeof(Colors).GetProperties();
        }

        private void tagsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnPropertyChanged("allTagsSearchResult");
        }

        private void Clear_Color(object sender, RoutedEventArgs e)
        {
            this.cmbColors.SelectedItem = null;
            OnPropertyChanged("allTagsSearchResult");
        }
    }
}
