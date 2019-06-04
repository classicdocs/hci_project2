using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project.Commands
{
    public static class DemoCommands
    {
        public static readonly RoutedUICommand AddNewResource = new RoutedUICommand(
            "Add new resource",
            "AddNewResource",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.F1,  ModifierKeys.Control ),
            }
            );
        public static readonly RoutedUICommand AddNewType = new RoutedUICommand(
            "Add new type",
            "AddNewType",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                  new KeyGesture(Key.F2,  ModifierKeys.Control ),
            }
            );
        public static readonly RoutedUICommand AddNewTag = new RoutedUICommand(
            "Add new tag",
            "AddNewTag",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                  new KeyGesture(Key.F3,  ModifierKeys.Control ),
            }
            );
        public static readonly RoutedUICommand ViewAllResources = new RoutedUICommand(
            "View all resources",
            "ViewAllResources",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                  new KeyGesture(Key.F4,  ModifierKeys.Control ),
            }
            );
        public static readonly RoutedUICommand ViewAllTypes = new RoutedUICommand(
            "View all types",
            "ViewAllTypes",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.F5,  ModifierKeys.Control ),
            }
            );
        public static readonly RoutedUICommand ViewAllTags = new RoutedUICommand(
            "View all tags",
            "ViewAllTags",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                 new KeyGesture(Key.F6,  ModifierKeys.Control ),
            }
            );
        public static readonly RoutedUICommand EditResource = new RoutedUICommand(
           "Edit resource",
           "Edit resource",
           typeof(RoutedCommands),
           new InputGestureCollection()
           {
                 new KeyGesture(Key.F7,  ModifierKeys.Control ),
           }
           );
        public static readonly RoutedUICommand EditResourceType = new RoutedUICommand(
           "Edit resource type",
           "EditResourceType",
           typeof(RoutedCommands),
           new InputGestureCollection()
           {
                 new KeyGesture(Key.F8,  ModifierKeys.Control ),
           }
           );
        public static readonly RoutedUICommand DeleteResource = new RoutedUICommand(
           "Delete resource",
           "DeleteResource",
           typeof(RoutedCommands),
           new InputGestureCollection()
           {
                 new KeyGesture(Key.F9,  ModifierKeys.Control ),
           }
           );
        public static readonly RoutedUICommand DeleteResourceType = new RoutedUICommand(
           "Delete resource type",
           "DeleteResourceType",
           typeof(RoutedCommands),
           new InputGestureCollection()
           {
                 new KeyGesture(Key.F10,  ModifierKeys.Control ),
           }
           );
        public static readonly RoutedUICommand SearchTreeView = new RoutedUICommand(
           "Search tree view",
           "SearchTreeView",
           typeof(RoutedCommands),
           new InputGestureCollection()
           {
                 new KeyGesture(Key.F11,  ModifierKeys.Control ),
           }
           );
    }
}
