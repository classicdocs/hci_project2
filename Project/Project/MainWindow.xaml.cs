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

        public static ObservableCollection<Resource> resources { get; set; }
        Point startPoint = new Point();

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
            resources = new ObservableCollection<Resource>();
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

        private void Cnv_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Resource resource = e.Data.GetData("myFormat") as Resource;
                resources.Add(resource);

                Label name = new Label() { Content = resource.Name };
                StackPanel stackPanel = new StackPanel();
                stackPanel.Children.Add(name);

                Image img = new Image()
                {
                    Source = new BitmapImage(new Uri(resource.Icon)),
                    Width = 20,
                    Height = 20,
                    ToolTip = stackPanel
                };
                Cnv.Children.Add(img);

                Canvas.SetLeft(img, e.GetPosition(MapImg).X);
                Canvas.SetTop(img, e.GetPosition(MapImg).Y);

                AddNewResourceDetails add = new AddNewResourceDetails();
                add.Show();
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
