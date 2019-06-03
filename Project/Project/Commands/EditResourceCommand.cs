using Project.Models;
using Project.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project.Commands
{
    class EditResourceCommand : ICommand
    {
        private Resource resource;
        private Point point;
        public EditResourceCommand(Resource res, Point p)
        {
            resource = res;
            point = p;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            EditResourceOnMap dialog = new EditResourceOnMap(resource, point);
            dialog.Show();
        }
    }
}
