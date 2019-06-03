using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Commands
{
    public static class RoutedCommands
    {
        public static readonly RoutedUICommand AddNewResource = new RoutedUICommand(
            "Add new resource",
            "AddNewResource",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.R, ModifierKeys.Control),
            }
            );
        public static readonly RoutedUICommand AddNewType = new RoutedUICommand(
            "Add new type",
            "AddNewType",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.T, ModifierKeys.Control),
            }
            );
        public static readonly RoutedUICommand AddNewTag = new RoutedUICommand(
            "Add new tag",
            "AddNewTag",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.G, ModifierKeys.Control),
            }
            );
        public static readonly RoutedUICommand ViewAllResources = new RoutedUICommand(
            "View all resources",
            "ViewAllResources",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.R, ModifierKeys.Control | ModifierKeys.Alt),
            }
            );
        public static readonly RoutedUICommand ViewAllTypes = new RoutedUICommand(
            "View all types",
            "ViewAllTypes",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.T, ModifierKeys.Control | ModifierKeys.Alt),
            }
            );
        public static readonly RoutedUICommand ViewAllTags = new RoutedUICommand(
            "View all tags",
            "ViewAllTags",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.G, ModifierKeys.Control | ModifierKeys.Alt),
            }
            );
        public static readonly RoutedUICommand DeleteResourceType = new RoutedUICommand(
            "Delete resource type",
            "DeleteResourceType",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.Delete),
            }
            );
    }
}
