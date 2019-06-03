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
    class DeleteResourceType
    {
        private ResourceTypeWithResources resourceType { get; set; }
        public DeleteResourceType(ResourceTypeWithResources rest)
        {
            resourceType = rest;

            MessageBoxResult result = MessageBox.Show("Deleting this resource type you will delete all resources with this type. Are you sure you want to perform this action?", "Delete resource type", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        ResourceTypeWithResources rtToDelete = null;
                        foreach (ResourceTypeWithResources rt in MainWindow.types)
                        {
                            if (rt.Id == resourceType.Id)
                            {
                                rtToDelete = rt;
                                break;
                            }
                        }
                        if (rtToDelete != null)
                        {
                            MainWindow.types.Remove(rtToDelete);
                            CollectionViewSource.GetDefaultView(MainWindow.types).Refresh();

                            List<ResourcePoint> rpToDelete = new List<ResourcePoint>();
                            foreach (ResourcePoint rp in MainWindow.resources)
                            {
                                if (rp.resource.ResourceType.Id == rtToDelete.Id)
                                {
                                    rpToDelete.Add(rp);
                                }
                            }
                            if (rpToDelete.Count > 0)
                            {
                                foreach(var r in rpToDelete)
                                {
                                    MainWindow.resources.Remove(r);
                                    MainWindow.removeResourceFromMap(r);
                                }
                            }
                            
                            ReadWrite rw = new ReadWrite();
                            rw.writeToFile("../../Data/types.json", MainWindow.types);
                            rw.writeToFile("../../Data/resources.json", MainWindow.resources);

                            MessageBox.Show("You have successfully deleted resource type called " + resourceType.Name + "!");
                        }
                        break;
                    }
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
