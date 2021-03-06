﻿using System;
using System.Collections.Generic;
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

namespace Project.Demos
{
    /// <summary>
    /// Interaction logic for DragAndDrop.xaml
    /// </summary>
    public partial class DragAndDrop : Window
    {
        public DragAndDrop()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            MessageBox.Show("To exit demo mode press Esc or click on Exit button", "Demo for drag and drop");
            mePlayer.Play();
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                mePlayer.Stop();
            Close();
        }

        private void mePlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            mePlayer.Stop();
            mePlayer.Play();
        }
    }
}
