using Project.FileHandler;
using Project.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for EditTag.xaml
    /// </summary>
    public partial class EditTag : Window, INotifyPropertyChanged
    {

        public EditTag() { }

        private Tag tag { get; set; }
        public EditTag(Tag tag)
        {
            this.tag = tag;
            InitializeComponent();
            Id.Text = tag.Id;
            Name.Text = tag.Name;
            Description.Text = tag.Description;
            cmbColors.ItemsSource = typeof(Colors).GetProperties();

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Id.Equals("") || Name.Equals("") || cmbColors.SelectedItem == null)
            {
                MessageBox.Show("You must fill all required fields");
                return;
            }
            Color selectedColor = (Color)(cmbColors.SelectedItem as PropertyInfo).GetValue(null, null);

            foreach (Tag t in MainWindow.tags)
            {
                if (t.Id == Id.Text && tag.Id != Id.Text)
                {
                    MessageBox.Show("Id you entered is already in use. Please choose another.");
                    return;
                }
                else if (t.Name == Name.Text && tag.Name != Name.Text)
                {
                    MessageBox.Show("Name you entered is already in use. Please choose another.");
                    return;
                }
                else if (t.Color == selectedColor.ToString() && tag.Color != selectedColor.ToString())
                {
                    MessageBox.Show("Color you choose is already in use. Please choose another.");
                    return;
                }
            }

            foreach(Tag t in MainWindow.tags)
            {
                if (t.Id == tag.Id)
                {
                    t.Id = Id.Text;
                    t.Name = Name.Text;
                    t.Description = Description.Text;
                    t.Color = selectedColor.ToString();
                }
            }

            foreach(ResourcePoint rp in MainWindow.resources)
            {
                foreach(Tag t in rp.resource.Tags)
                {
                    if (t.Id == tag.Id)
                    {
                        t.Id = Id.Text;
                        t.Name = Name.Text;
                        t.Description = Description.Text;
                        t.Color = selectedColor.ToString();
                    }
                }
            }
            MainWindow.removeAllResources();
            MainWindow.drawResources();

            ReadWrite rw = new ReadWrite();
            rw.writeToFile("../../Data/tags.json", MainWindow.tags);
            rw.writeToFile("../../Data/resources.json", MainWindow.resources);

            this.Close();
            MessageBox.Show("You have successfully edited tag.");
        }
    }
}
