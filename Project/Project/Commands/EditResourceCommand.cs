using Project.Models;
using Project.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Commands
{
    class EditResourceCommand : ICommand
    {
        private Resource resource;
        public EditResourceCommand(Resource res)
        {
            resource = res;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            EditResource dialog = new EditResource(resource);
            dialog.Show();
        }
    }
}
