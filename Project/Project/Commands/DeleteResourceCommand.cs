﻿using Project.FileHandler;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project.Commands
{
    public class DeleteResourceCommand : ICommand
    {
        private Resource resource;
        private Canvas canvas;
        public DeleteResourceCommand(Resource res, Canvas c)
        {
            resource = res;
            canvas = c;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            ResourcePoint resToDelete = null;

            foreach(ResourcePoint rp in MainWindow.resources)
            {
                if (rp.resource == resource)
                    resToDelete = rp;
            }
            if (resToDelete != null)
            {
                MainWindow.resources.Remove(resToDelete);
                ReadWrite rw = new ReadWrite();
                rw.writeToFile("../../Data/resources.json", MainWindow.resources);

                var element = canvas.InputHitTest(resToDelete.point) as UIElement;
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


                MessageBox.Show("You have successfully delete resource");
            }

        }
    }
}
