using Project.FileHandler;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Project.Views
{
    class DeleteResource
    {
        private Resource resource { get; set; }
        public DeleteResource(Resource resource)
        {
            this.resource = resource;

            MessageBoxResult result = MessageBox.Show("Deleting this resource you will delete all resources on maps. Are you sure you want to perform this action?", "Delete resource", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        Resource resToDelete = null;
                        ResourceTypeWithResources rtwr = null;
                        foreach (ResourceTypeWithResources rt in MainWindow.types)
                        {
                            foreach(Resource res in rt.Resources)
                            {
                                if (res == this.resource)
                                {
                                    resToDelete = res;
                                    rtwr = rt;
                                    break;
                                }
                            }
                        }
                        if (resToDelete != null)
                        {
                            foreach(ResourceTypeWithResources rt in MainWindow.types)
                            {
                                if (rt == rtwr)
                                {
                                    rt.Resources.Remove(resToDelete);
                                }
                            }
                            List<ResourcePoint> rpsToDel = new List<ResourcePoint>();
                            foreach(ResourcePoint rp in MainWindow.resources)
                            {
                                if (rp.resource == resToDelete)
                                    rpsToDel.Add(rp);
                            }
                            if (rpsToDel.Count > 0)
                            {
                                foreach(var r in rpsToDel)
                                {
                                    MainWindow.resources.Remove(r);
                                    MainWindow.removeResourceFromMap(r);
                                }
                            }

                            CollectionViewSource.GetDefaultView(MainWindow.types).Refresh();

                            ReadWrite rw = new ReadWrite();
                            rw.writeToFile("../../Data/types.json", MainWindow.types);
                            rw.writeToFile("../../Data/resources.json", MainWindow.resources);

                            MessageBox.Show("You have successfully deleted resource called " + resource.Name + "!");
                        }
                        break;
                    }
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
