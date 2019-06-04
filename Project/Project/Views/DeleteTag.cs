using Project.FileHandler;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.Views
{
    class DeleteTag
    {
        private Tag tag;
        public DeleteTag(Tag t)
        {
            tag = t;

            MessageBoxResult result = MessageBox.Show("By deleting this tag, you will delete all tags in all resources that have these tags. Are you sure you want to delete it?", "Delete tag", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        for(int i = 0; i < MainWindow.tags.Count; i++)
                        {
                            if (MainWindow.tags[i] == tag)
                            {
                                MainWindow.tags.RemoveAt(i);
                                break;
                            }
                        }
                        foreach(var rp in MainWindow.resources)
                        {
                           for(int i = 0; i < rp.resource.Tags.Count; i++)
                            {
                                if (rp.resource.Tags[i].Id == tag.Id)
                                {
                                    rp.resource.Tags.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                        MainWindow.removeAllResources();
                        MainWindow.drawResources();

                        ReadWrite rw = new ReadWrite();
                        rw.writeToFile("../../Data/tags.json", MainWindow.tags);
                        rw.writeToFile("../../Data/resources.json", MainWindow.resources);
                        MessageBox.Show("You have successfully deleted tag called " + tag.Name);

                        break;
                    }
                case MessageBoxResult.No:
                    break;
            }
        }
    }
}
