using Project.Commands;
using Project.Models;
using Project.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public static ObservableCollection<ResourceTypeWithResources> types { get; set; }

        public static ObservableCollection<Tag> tags { get; set; }

        public static ObservableCollection<ResourcePoint> resources { get; set; }

        Point startPoint = new Point();

        public static bool addNewResourceDialog { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
            this.DataContext = this;
            LoadData();
        }

        private void InitializeData()
        {
            types = new ObservableCollection<ResourceTypeWithResources>();
            tags = new ObservableCollection<Tag>();
            resources = new ObservableCollection<ResourcePoint>();
            addNewResourceDialog = false;
        }

        public ImageSource MapImage
        {
            get
            {
                return new BitmapImage(new Uri("Resource/map.jpg"));
            }
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

        private static  Image drawResource(Resource resource, Point p)
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
            grid = tooltipInfo(grid, "Page (this is for testing)", resource.OnPage, 13);

            Image img = new Image()
            {
                Source = new BitmapImage(new Uri(resource.Icon)),
                Width = 20,
                Height = 20,
                ToolTip = grid
            };

            ContextMenu contextMenu = new ContextMenu();

            MenuItem edit = new MenuItem();
            edit.Header = "Edit";
            edit.Command = new EditResourceCommand(resource, p);

            MenuItem delete = new MenuItem();
            delete.Header = "Delete";
            Canvas canv = ((MainWindow)Application.Current.MainWindow).Cnv;
            if (resource.OnPage == (PageEnum)0)
            {
                canv = ((MainWindow)Application.Current.MainWindow).Cnv;
            }
            else if (resource.OnPage == (PageEnum)1)
            {
                canv = ((MainWindow)Application.Current.MainWindow).Cnv2;
            }
            else if (resource.OnPage == (PageEnum)2)
            {
                canv = ((MainWindow)Application.Current.MainWindow).Cnv3;
            }
            else if (resource.OnPage == (PageEnum)3)
            {
                canv = ((MainWindow)Application.Current.MainWindow).Cnv4;
            }
            else if (resource.OnPage == (PageEnum)4)            // TEST
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
                    rect.Width = 20;
                    rect.Height = 20;

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
                Image img = drawResource(rp.resource, rp.point);
                Canvas.SetLeft(img, rp.point.X);
                Canvas.SetTop(img, rp.point.Y);
            }
        } 

        public static void removeResourceFromAllMaps(ResourcePoint rp)
        {
            List<Canvas> canvases = new List<Canvas>();
            canvases.Add(((MainWindow)Application.Current.MainWindow).Cnv);
            canvases.Add(((MainWindow)Application.Current.MainWindow).Cnv2);
            canvases.Add(((MainWindow)Application.Current.MainWindow).Cnv3);
            canvases.Add(((MainWindow)Application.Current.MainWindow).Cnv4);

            foreach(var canvas in canvases)
            {
                removeElementFromCanvas(canvas, rp);
            }
        }

        public static void removeResourceFromMap(ResourcePoint rp)
        {
            Canvas canvas = getCanvas();
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
            Image img = drawResource(rp.resource, rp.point);
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
                var canvas = sender as Canvas;

                Canvas currentCanvas = getCanvas();

                switch (currentCanvas.Name)
                {
                    case "Cnv":
                        {
                            resource.OnPage = (PageEnum)0;
                            break;
                        }
                    case "Cnv2":
                        {
                            resource.OnPage = (PageEnum)1;
                            break;
                        }
                    case "Cnv3":
                        {
                            resource.OnPage = (PageEnum)2;
                            break;
                        }
                    case "Cnv4":
                        {
                            resource.OnPage = (PageEnum)3;
                            break;
                        }
                }

                Point p = new Point(e.GetPosition(currentCanvas).X, e.GetPosition(currentCanvas).Y);
                AddNewResourceDetails add = new AddNewResourceDetails(resource, p);
                add.ShowDialog();
                if (addNewResourceDialog)
                {
                    Image img = drawResource(resource, p);
                    Canvas.SetLeft(img, e.GetPosition(currentCanvas).X);
                    Canvas.SetTop(img, e.GetPosition(currentCanvas).Y);
                    

                    MessageBox.Show("You have successfully added new resource on map.");
                }
            }
        }

        private void Cnv_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }

        }

    }

}
