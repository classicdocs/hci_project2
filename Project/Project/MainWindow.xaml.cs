using Project.Commands;
using Project.Models;
using Project.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
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
    

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static ObservableCollection<ResourceTypeWithResources> types { get; set; }

        public static ObservableCollection<ResourcePoint> searchShownResources { get; set; }
        public static bool searchIsActive { get; set; }


        private bool _enable;
        public bool AddNewResourceEnabled
        {
            get
            {
                return _enable;
            }
            set
            {
                if (_enable != value)
                {
                    _enable = value;
                    OnPropertyChanged("AddNewResourceEnabled");
                }
            }
        }

        public static ObservableCollection<Tag> tags { get; set; }

        public static ObservableCollection<ResourcePoint> resources { get; set; }

        Point startPoint = new Point();

        public static bool addNewResourceDialog { get; set; }

        private string _searchText;

        private string minPrice;

        private bool FilterOn { get; set; }

        private string maxPrice;

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;

                OnPropertyChanged("SearchText");
                OnPropertyChanged("TypesSearchResult");
            }
        }

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

        public ObservableCollection<Resource> TypesSearchResult
        {
            get
            {
                if (SearchText == null)
                {
                    SearchText = "";
                }

                SearchText.Trim();

                if (SearchText.Equals("") && !FilterOn)
                {
                    RowForSearch.Height = new GridLength(5);
                }
                else
                {
                    RowForSearch.Height = new GridLength(170);
                    SearchText.Trim();
                }
                                                                           
                ObservableCollection<Resource> resources = new ObservableCollection<Resource>();

                foreach (ResourceTypeWithResources r in types)
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
                                if (cmbRenewable.SelectedItem.Equals("Renewable"))
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
                                if (Int32.Parse(res.Price) > Int32.Parse(MaxPrice))
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
                        if (canAdd)
                        {
                            resources.Add(res);
                        }
                    }

                    /*ResourceTypeWithResources newr = new ResourceTypeWithResources();
                    newr.Id = r.Id;
                    newr.Icon = r.Icon;
                    newr.Name = r.Name;
                    newr.Resources = resources;
                    */
                    /*if (resources.Count > 0)
                    {
                        resources.Add(res);
                    }*/

                    //resources = new ObservableCollection<Resource>();
                }
                return resources;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            cmbUnit.ItemsSource = Enum.GetValues(typeof(ResourceUnit)).Cast<ResourceUnit>();
            cmbFrequency.ItemsSource = Enum.GetValues(typeof(ResourceFrequency)).Cast<ResourceFrequency>();
            cmbRenewable.Items.Add("Renewable");
            cmbRenewable.Items.Add("Nonrenewable");
            InitializeData();
            this.DataContext = this;
            LoadData();
        }

        private void InitializeData()
        {
            types = new ObservableCollection<ResourceTypeWithResources>();
            types.CollectionChanged += TypesChanged;
            tags = new ObservableCollection<Tag>();
            resources = new ObservableCollection<ResourcePoint>();
            addNewResourceDialog = false;
            font = 40;
        }
        void TypesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (types.Count > 0)
                AddNewResourceEnabled = true;
            else
                AddNewResourceEnabled = false;
            OnPropertyChanged("TypesSearchResult");
        }

        private int _font;
        public int font
        {
            get { return _font; }
            set
            {
                if (_font != value)
                {
                    _font = value;
                    OnPropertyChanged();
                }
            }
        }

        public ImageSource MapImage
        {
            get
            {
                return new BitmapImage(new Uri("Resource/map.jpg"));
            }
        }

        private void ViewAllResources_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AllResources_Click(object sender, RoutedEventArgs e)
        {
            var s = new ViewAllResources();
            s.ShowDialog();
        }

        private void ViewAllTypes_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AllTypes_Click(object sender, RoutedEventArgs e)
        {
            var s = new ViewAllTypes();
            s.ShowDialog();
        }

        private void ViewAllTags_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void AllTags_Click(object sender, RoutedEventArgs e)
        {
            var s = new ViewAllTags();
            s.ShowDialog();
        }

        private void NewResource_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = AddNewResourceEnabled;
        }

        private void NewResource_Click(object sender, RoutedEventArgs e)
        {
            var s = new AddNewResource();
            s.ShowDialog();
        }
        private void NewType_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewType_Click(object sender, RoutedEventArgs e)
        {
            var s = new AddNewType();
            s.ShowDialog();
        }
        private void NewTag_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewTag_Click(object sender, RoutedEventArgs e)
        {
            var s = new AddNewTag();
            s.ShowDialog();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void LoadData()
        {
            string tags_file = "../../Data/tags.json";
            string types_file = "../../Data/types.json";
            string resources_file = "../../Data/resources.json";

            if (File.Exists(tags_file))
            {
                using (StreamReader sr = new StreamReader(tags_file))
                {
                    string s = sr.ReadToEnd();
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    tags = serializer.Deserialize<ObservableCollection<Tag>>(s);
                }
            }
            if (File.Exists(types_file))
            {
                using (StreamReader sr = new StreamReader(types_file))
                {
                    string s = sr.ReadToEnd();
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    types = serializer.Deserialize<ObservableCollection<ResourceTypeWithResources>>(s);
                    if (types.Count > 0)
                        AddNewResourceEnabled = true;
                    else
                        AddNewResourceEnabled = false;
                }
            }
            if (File.Exists(resources_file))
            {
                using (StreamReader sr = new StreamReader(resources_file))
                {
                    string s = sr.ReadToEnd();
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    resources = serializer.Deserialize<ObservableCollection<ResourcePoint>>(s);
                }
                drawResources();
            }

        }

        private static  Image drawResource(Resource resource, Point p, PageEnum onPage)
        {
            Grid grid = new Grid();
            for (int i = 0; i < 15; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = System.Windows.GridLength.Auto;
                grid.RowDefinitions.Add(rd);
            }
            ColumnDefinition cd1 = new ColumnDefinition();
            cd1.Width = System.Windows.GridLength.Auto;
            grid.ColumnDefinitions.Add(cd1);
            ColumnDefinition cd2 = new ColumnDefinition();
            grid.ColumnDefinitions.Add(cd2);
            grid = tooltipInfo(grid, "Id", resource.Id, 0);
            grid = tooltipInfo(grid, "Name", resource.Name, 1);
            grid = tooltipInfo(grid, "Description", resource.Description, 2);
            grid = tooltipInfo(grid, "Type", resource.ResourceType.Name, 3);
            grid = tooltipInfo(grid, "Frequency", resource.Frequency, 4);
            grid = tooltipInfo(grid, "Icon", resource.Icon, 5);
            grid = tooltipInfo(grid, "Renewable", resource.Renewable, 6);
            grid = tooltipInfo(grid, "Strategic importance", resource.StrategicImportance, 7);
            grid = tooltipInfo(grid, "Currently exploited", resource.CurrentlyExploited, 8);
            grid = tooltipInfo(grid, "Unit", resource.Unit, 9);
            grid = tooltipInfo(grid, "Price", resource.Price, 10);
            grid = tooltipInfo(grid, "Date of discovery", resource.DateOfDiscovery, 11);
            grid = tooltipInfo(grid, "Tags", resource.Tags, 12);
            grid = tooltipInfo(grid, "Page (this is for testing)", onPage, 13);

            Image img = new Image()
            {
                Source = new BitmapImage(new Uri(resource.Icon)),
                Width = 20,
                Height = 20,
                ToolTip = grid
            };

            ContextMenu contextMenu = new ContextMenu();
            contextMenu.FontSize = 40;
            MenuItem edit = new MenuItem();
            edit.Header = "Edit";
            edit.Command = new EditResourceCommand(resource, p);

            MenuItem delete = new MenuItem();
            delete.Header = "Delete";
            Canvas canv = ((MainWindow)Application.Current.MainWindow).Cnv;
            if (onPage == (PageEnum)0)
            {
                canv = ((MainWindow)Application.Current.MainWindow).Cnv;
            }
            else if (onPage == (PageEnum)1)
            {
                canv = ((MainWindow)Application.Current.MainWindow).Cnv2;
            }
            else if (onPage == (PageEnum)2)
            {
                canv = ((MainWindow)Application.Current.MainWindow).Cnv3;
            }
            else if (onPage == (PageEnum)3)
            {
                canv = ((MainWindow)Application.Current.MainWindow).Cnv4;
            }
            else if (onPage == (PageEnum)4)            // TEST
            {
                return null;
            }
            delete.Command = new DeleteResourceCommand(resource, p, canv);

            contextMenu.Items.Add(edit);
            contextMenu.Items.Add(delete);
            img.ContextMenu = contextMenu;


            canv.Children.Add(img);
           
            //((MainWindow)Application.Current.MainWindow).Cnv.Children.Add(img);
            return img;
        }

        private static Grid tooltipInfo(Grid grid, string label, Object content, int row)
        {
            Label l = new Label();
            grid.Children.Add(l);
            l.Content = label + ":";
            Grid.SetRow(l, row);
            Grid.SetColumn(l, 0);

            if (content is List<Tag>)
            {
                StackPanel stack = new StackPanel();
                foreach(Tag tag in (List<Tag>) content)
                {
                    StackPanel stack2 = new StackPanel();
                    stack2.Orientation = Orientation.Horizontal;
                    Label la = new Label();
                    la.Content = tag.Name;
                    Rectangle rect = new Rectangle();
                    var converter = new System.Windows.Media.BrushConverter();
                    var brush = (Brush)converter.ConvertFromString(tag.Color);
                    rect.Fill = brush;
                    rect.Width = 40;
                    rect.Height = 40;

                    stack2.Children.Add(rect);
                    stack2.Children.Add(la);
                    stack.Children.Add(stack2);
                }
                List<Tag> list = (List<Tag>)content;
                if (list.Count == 0)
                {
                    Label la = new Label();
                    la.Content = "None";
                    stack.Children.Add(la);
                }
                grid.Children.Add(stack);
                Grid.SetRow(stack, row);
                Grid.SetColumn(stack, 1);

                return grid;
            } else
            {
                
                Label l2 = new Label();
                if (content is bool)
                    l2.Content = (bool)content ? "Yes" : "No";
                else
                    l2.Content = content;
                grid.Children.Add(l2);
                Grid.SetRow(l2, row);
                Grid.SetColumn(l2, 1);

                return grid;
            }
            
        }

        public static void drawResources()
        {
            foreach(ResourcePoint rp in resources)
            {
                Image img = drawResource(rp.resource, rp.point, rp.OnPage);
                Canvas.SetLeft(img, rp.point.X);
                Canvas.SetTop(img, rp.point.Y);
            }
        }

        public static void removeAllResources()
        {
            List<Canvas> canvases = new List<Canvas>();
            canvases.Add(((MainWindow)Application.Current.MainWindow).Cnv);
            canvases.Add(((MainWindow)Application.Current.MainWindow).Cnv2);
            canvases.Add(((MainWindow)Application.Current.MainWindow).Cnv3);
            canvases.Add(((MainWindow)Application.Current.MainWindow).Cnv4);

            foreach (var canvas in canvases)
            {
                for (int i = canvas.Children.Count - 1; i >= 0; i += -1)
                {
                    UIElement Child = canvas.Children[i];
                    if (Child is Image)
                        canvas.Children.Remove(Child);
                }
            }
        }
        public static void removeResourceFromMap(ResourcePoint rp)
        {
            Canvas canvas = null;
            switch(rp.OnPage)
            {
                case PageEnum.First: canvas = ((MainWindow)Application.Current.MainWindow).Cnv; break;
                case PageEnum.Second: canvas = ((MainWindow)Application.Current.MainWindow).Cnv2; break;
                case PageEnum.Third: canvas = ((MainWindow)Application.Current.MainWindow).Cnv3; break;
                case PageEnum.Fourth: canvas = ((MainWindow)Application.Current.MainWindow).Cnv4; break;
            }
            removeElementFromCanvas(canvas, rp);
        }

        private static void removeElementFromCanvas(Canvas canvas, ResourcePoint rp)
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

        public static void drawOneResource(ResourcePoint rp)
        {
            Image img = drawResource(rp.resource, rp.point, rp.OnPage);
            Canvas.SetLeft(img, rp.point.X);
            Canvas.SetTop(img, rp.point.Y);
        }

        private void Resourse_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void Resource_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                WrapPanel stackPanel = sender as WrapPanel;

                TextBlock textBlock = (TextBlock)stackPanel.Children[1];
                string resourceName = textBlock.Text;
                Resource resource = null;
                foreach(ResourceTypeWithResources rt in types)
                {
                    foreach(Resource r in rt.Resources)
                    {
                        if (r.Name.Equals(resourceName))
                        {
                            resource = r;
                        }
                    }
                }
                if (resource != null)
                {
                    DataObject dragData = new DataObject("myFormat", resource);
                    DragDrop.DoDragDrop(stackPanel, dragData, DragDropEffects.Move);
                }
            }
        }

        public static Canvas getCanvas()
        {
            TabItem tab = (TabItem)((MainWindow)Application.Current.MainWindow).Tab.SelectedItem;
            Canvas currentCanvas = ((MainWindow)Application.Current.MainWindow).Cnv;
            switch (tab.Name)
            {
                case "First":
                    {
                        currentCanvas = ((MainWindow)Application.Current.MainWindow).Cnv;
                        break;
                    }
                case "Second":
                    {
                        currentCanvas = ((MainWindow)Application.Current.MainWindow).Cnv2;
                        break;
                    }
                case "Third":
                    {
                        currentCanvas = ((MainWindow)Application.Current.MainWindow).Cnv3;
                        break;
                    }
                case "Fourth":
                    {
                        currentCanvas = ((MainWindow)Application.Current.MainWindow).Cnv4;
                        break;
                    }
            }

            return currentCanvas;
        }

        private void Cnv_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                addNewResourceDialog = false;

                Resource resource = e.Data.GetData("myFormat") as Resource;
                //var currentCanvas = sender as Canvas;

                Canvas currentCanvas = getCanvas();
                PageEnum onPage = PageEnum.NoInstance;
                switch (currentCanvas.Name)
                {
                    case "Cnv":
                        {
                            onPage = PageEnum.First;
                            break;
                        }
                    case "Cnv2":
                        {
                            onPage = PageEnum.Second;
                            break;
                        }
                    case "Cnv3":
                        {
                            onPage = PageEnum.Third;
                            break;
                        }
                    case "Cnv4":
                        {
                            onPage = PageEnum.Fourth;
                            break;
                        }
                }

                Point p = new Point(e.GetPosition(currentCanvas).X, e.GetPosition(currentCanvas).Y);
                AddNewResourceDetails add = new AddNewResourceDetails(resource, p, onPage);
                add.ShowDialog();
                if (addNewResourceDialog)
                {
                    Image img = drawResource(resource, p, onPage);
                    Canvas.SetLeft(img, e.GetPosition(currentCanvas).X);
                    Canvas.SetTop(img, e.GetPosition(currentCanvas).Y);

                    MessageBox.Show("You have successfully added a new resource on map..", "Added resource", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        public static List<Canvas> getAllCanvases()
        {
            List<Canvas> canvases = new List<Canvas>();
            canvases.Add(((MainWindow)Application.Current.MainWindow).Cnv);
            canvases.Add(((MainWindow)Application.Current.MainWindow).Cnv2);
            canvases.Add(((MainWindow)Application.Current.MainWindow).Cnv3);
            canvases.Add(((MainWindow)Application.Current.MainWindow).Cnv4);
            return canvases;

        }

        private void Cnv_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }

        }

        private void Cnv_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender != e.Source)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }

        }

        private void ShowAdvancedSearch(object sender, RoutedEventArgs e)
        {
            AdvancedSearchFields.Visibility = Visibility.Visible;
            ShowSearch.Visibility = Visibility.Hidden;
            HideSearch.Visibility = Visibility.Visible;
            RowForFilter.Height = new GridLength(220);
        }

        private void HideAdvancedSearch(object sender, RoutedEventArgs e)
        {
            AdvancedSearchFields.Visibility = Visibility.Hidden;
            ShowSearch.Visibility = Visibility.Visible;
            HideSearch.Visibility = Visibility.Hidden;
            RowForFilter.Height = new GridLength(5);
            RemoveFilters(sender, e);
        }

        private void DoFilter(object sender, RoutedEventArgs e)
        {
            if (MaxPrice != null)
            {
                try
                {
                    int max = Int32.Parse(MaxPrice);
                }
                catch (FormatException f)
                {
                    MessageBox.Show("Invalid input for maximum price!", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    MessageBox.Show("Invalid input for minimum price!", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            FilterOn = true;

            OnPropertyChanged("TypesSearchResult");
        }

        private void RemoveFilters(object sender, RoutedEventArgs e)
        {
            cmbUnit.SelectedItem = null;
            cmbFrequency.SelectedItem = null;
            cmbRenewable.SelectedItem = null;
            MinPrice = null;
            MaxPrice = null;
            //SearchText = null;
            FilterOn = false;
            OnPropertyChanged("TypesSearchResult");
        }

        private void SearchMap_Click(object sender, RoutedEventArgs e)
        {
            var s = new SearchMap();
            s.Show();
        }

        private void closeSearch(object sender, RoutedEventArgs e)
        {
            
            if (searchShownResources != null)
            {
                foreach (ResourcePoint rp in searchShownResources)
                {
                    Canvas canvas = getCanvas();
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


                MessageBox.Show("Your search results have been removed. All resources are now on the map", "Reverted search", MessageBoxButton.OK, MessageBoxImage.Information);
                ((MainWindow)Application.Current.MainWindow).filterSearch.Visibility = Visibility.Hidden;
                ((MainWindow)Application.Current.MainWindow).closeSearchBtn.Visibility = Visibility.Hidden;
                ((MainWindow)Application.Current.MainWindow).closeSearchBtn.IsHitTestVisible = false;
                MainWindow.searchShownResources = null;
                searchIsActive = false;
                LoadData();
            }

        }

        public static void showCloseBtn()
        {
            ((MainWindow)Application.Current.MainWindow).filterSearch.Visibility = Visibility.Visible;
            ((MainWindow)Application.Current.MainWindow).closeSearchBtn.Visibility = Visibility.Visible;
            ((MainWindow)Application.Current.MainWindow).closeSearchBtn.IsHitTestVisible = true;
        }

        private void FilterSearch_Click(object sender, RoutedEventArgs e)
        {
            var s = new FilterSearch();
            s.ShowDialog();
        }


        void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (searchIsActive)
                {
                    int a = 5;
                    //this.closeSearch(null, null);
                }
            }
        }

    }

}
